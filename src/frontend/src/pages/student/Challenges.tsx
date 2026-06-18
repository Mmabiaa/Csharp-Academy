import { useState } from "react";
import { useMutation, useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchChallenges, runCode, submitChallenge } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import CodeEditor from "../../components/CodeEditor";
import { Trophy, Play, Lightbulb, CheckCircle2, XCircle, Gem } from "lucide-react";

const difficultyBadge: Record<string, string> = {
  Easy: "duo-badge-green",
  Medium: "duo-badge-yellow",
  Hard: "duo-badge-red",
};

export default function Challenges() {
  const { token, isAuthenticated } = useAuth();
  const [selectedId, setSelectedId] = useState<number | null>(null);
  const [code, setCode] = useState("");
  const [output, setOutput] = useState("");
  const [message, setMessage] = useState("");
  const [isSuccess, setIsSuccess] = useState(false);
  const [showHint, setShowHint] = useState(false);
  const [filter, setFilter] = useState("All");

  const { data: challenges, isLoading } = useQuery({
    queryKey: ["challenges"],
    queryFn: fetchChallenges,
  });

  const selected = challenges?.find((c) => c.id === selectedId) ?? challenges?.[0];

  const submitMutation = useMutation({
    mutationFn: () => submitChallenge(selected!.id, code, token!),
    onSuccess: (result) => {
      setOutput(result.output);
      setIsSuccess(result.xpEarned > 0);
      setMessage(
        result.message + (result.xpEarned > 0 ? ` +${result.xpEarned} XP` : "")
      );
    },
    onError: (err: Error) => {
      setIsSuccess(false);
      setMessage(err.message);
    },
  });

  const filtered = (challenges ?? []).filter(
    (c) => filter === "All" || c.difficulty === filter
  );

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-16 w-64 bg-neutral-200 rounded-xl animate-pulse" />
        <div className="grid lg:grid-cols-5 gap-6">
          <div className="lg:col-span-2 space-y-2">
            <div className="h-24 bg-neutral-200 rounded-2xl animate-pulse" />
            <div className="h-24 bg-neutral-200 rounded-2xl animate-pulse" />
            <div className="h-24 bg-neutral-200 rounded-2xl animate-pulse" />
          </div>
          <div className="lg:col-span-3 bg-neutral-200 rounded-2xl animate-pulse" />
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div className="flex items-start justify-between gap-4">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 flex items-center gap-2">
            <Trophy className="w-7 h-7 text-[#FFC800]" />
            Coding challenges
          </h1>
          <p className="text-neutral-600 font-bold">
            Solve coding puzzles and earn XP!
          </p>
        </div>
      </div>

      <div className="flex flex-wrap gap-2">
        {["All", "Easy", "Medium", "Hard"].map((d) => (
          <button
            key={d}
            onClick={() => setFilter(d)}
            className={
              filter === d
                ? "duo-btn3d duo-btn3d-green !px-4 !py-2 !text-xs"
                : "duo-btn3d duo-btn3d-white !px-4 !py-2 !text-xs"
            }
          >
            {d}
          </button>
        ))}
      </div>

      <div className="grid lg:grid-cols-5 gap-6">
        <div className="lg:col-span-2 space-y-2 max-h-[70vh] overflow-y-auto pr-1">
          {filtered.map((c) => (
            <button
              key={c.id}
              onClick={() => {
                setSelectedId(c.id);
                setCode(c.starterCode);
                setOutput("");
                setMessage("");
                setShowHint(false);
              }}
              className={`w-full text-left p-5 rounded-2xl border-2 transition-all ${
                selected?.id === c.id
                  ? "border-[#58CC02] bg-[#58CC02]/10"
                  : "border-[#e5e5e5] bg-white hover:border-[#58CC02]/40 hover:bg-neutral-50"
              }`}
            >
              <div className="flex items-center justify-between gap-2 mb-2">
                <p className="font-black text-neutral-900">{c.title}</p>
                <span className={`duo-badge ${difficultyBadge[c.difficulty] ?? "duo-badge-gray"}`}>
                  {c.difficulty}
                </span>
              </div>
              <div className="flex items-center justify-between">
                <p className="text-xs font-bold text-neutral-400">{c.tags}</p>
                <span className="duo-xp-pill">
                  <Gem className="w-3 h-3" />+{c.xpReward}
                </span>
              </div>
            </button>
          ))}
        </div>

        {selected && (
          <div className="lg:col-span-3 duo-card space-y-4">
            <div>
              <h2 className="text-xl font-black text-neutral-900 mb-2">
                {selected.title}
              </h2>
              <p className="text-neutral-600 font-bold text-sm leading-relaxed">
                {selected.description}
              </p>
            </div>

            <CodeEditor
              value={code || selected.starterCode}
              onChange={setCode}
              rows={16}
            />

            <div className="flex flex-wrap gap-3">
              <button
                onClick={async () => {
                  const result = await runCode(code || selected.starterCode);
                  setOutput(result.success ? result.output : result.error ?? "Error");
                }}
                className="duo-btn3d duo-btn3d-white"
              >
                <Play className="w-4 h-4" />
                Run
              </button>
              {isAuthenticated ? (
                <button
                  onClick={() => submitMutation.mutate()}
                  disabled={submitMutation.isPending}
                  className="duo-btn3d duo-btn3d-green"
                >
                  {submitMutation.isPending ? "Checking..." : "Submit solution"}
                </button>
              ) : (
                <Link to="/login" className="duo-btn3d duo-btn3d-green">
                  Sign in to submit
                </Link>
              )}
              <button
                onClick={() => setShowHint(!showHint)}
                className="duo-btn3d duo-btn3d-yellow"
              >
                <Lightbulb className="w-4 h-4" />
                {showHint ? "Hide hint" : "Hint"}
              </button>
            </div>

            {showHint && (
              <div className="bg-[#FFF8E1] border-2 border-[#FFC800]/40 p-4 rounded-2xl flex gap-3">
                <Lightbulb className="w-5 h-5 text-[#946800] shrink-0 mt-0.5" />
                <p className="text-sm font-bold text-[#946800]">{selected.hint}</p>
              </div>
            )}

            {output && (
              <pre className="bg-neutral-900 text-neutral-100 border-2 border-neutral-800 p-4 rounded-2xl text-sm font-mono whitespace-pre-wrap">
                {output}
              </pre>
            )}
            {message && (
              <div
                className={`flex items-center gap-2 p-3 rounded-xl font-black text-sm ${
                  isSuccess
                    ? "bg-[#D7FFB8] text-[#46A302]"
                    : "bg-[#FFDFE0] text-[#CC3A3A]"
                }`}
              >
                {isSuccess ? (
                  <CheckCircle2 className="w-5 h-5 shrink-0" />
                ) : (
                  <XCircle className="w-5 h-5 shrink-0" />
                )}
                {message}
              </div>
            )}
          </div>
        )}
      </div>
    </div>
  );
}