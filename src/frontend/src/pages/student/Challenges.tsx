import { useState } from "react";
import { useMutation, useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchChallenges, runCode, submitChallenge } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import CodeEditor from "../../components/CodeEditor";

const difficultyColor: Record<string, string> = {
  Easy: "bg-[#58CC02] text-white",
  Medium: "bg-[#FFC800] text-neutral-900",
  Hard: "bg-[#FF4B4B] text-white",
};

export default function Challenges() {
  const { token, isAuthenticated } = useAuth();
  const [selectedId, setSelectedId] = useState<number | null>(null);
  const [code, setCode] = useState("");
  const [output, setOutput] = useState("");
  const [message, setMessage] = useState("");
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
      setMessage(
        result.message +
          (result.xpEarned > 0 ? ` +${result.xpEarned} XP` : "")
      );
    },
    onError: (err: Error) => setMessage(err.message),
  });

  const filtered = (challenges ?? []).filter(
    (c) => filter === "All" || c.difficulty === filter
  );

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-16 w-64 bg-neutral-200 rounded-xl" />
        <div className="grid lg:grid-cols-5 gap-6">
          <div className="lg:col-span-2 space-y-2">
            <div className="h-24 bg-neutral-200 rounded-xl" />
            <div className="h-24 bg-neutral-200 rounded-xl" />
            <div className="h-24 bg-neutral-200 rounded-xl" />
          </div>
          <div className="lg:col-span-3 bg-neutral-200 rounded-xl" />
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Coding Challenges
        </h1>
        <p className="text-neutral-600 font-semibold">
          Solve coding puzzles and earn XP!
        </p>
      </div>

      <div className="flex flex-wrap gap-2">
        {["All", "Easy", "Medium", "Hard"].map((d) => (
          <button
            key={d}
            onClick={() => setFilter(d)}
            className={`px-4 py-2 rounded-xl font-bold text-sm transition-all ${
              filter === d
                ? "bg-[#58CC02] text-white shadow-[0_4px_0_#46A301]"
                : "bg-white border border-[#e5e5e5] text-neutral-700 hover:border-[#58CC02]/30"
            }`}
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
              className={`w-full text-left p-5 rounded-xl border transition-colors ${
                selected?.id === c.id
                  ? "border-[#58CC02] bg-[#58CC02]/10"
                  : "border-[#e5e5e5] bg-white hover:border-[#58CC02]/30 hover:bg-neutral-50"
              }`}
            >
              <div className="flex items-center justify-between gap-2 mb-1">
                <p className="font-black text-neutral-900">{c.title}</p>
                <span
                  className={`text-xs px-3 py-1 rounded-full font-bold ${
                    difficultyColor[c.difficulty] ?? ""
                  }`}
                >
                  {c.difficulty}
                </span>
              </div>
              <p className="text-xs font-bold text-neutral-500">
                {c.tags} · +{c.xpReward} XP
              </p>
            </button>
          ))}
        </div>

        {selected && (
          <div className="lg:col-span-3 duo-card space-y-4">
            <div>
              <h2 className="text-xl font-black text-neutral-900 mb-2">
                {selected.title}
              </h2>
              <p className="text-neutral-700 font-semibold">
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
                  const result = await runCode(
                    code || selected.starterCode
                  );
                  setOutput(
                    result.success ? result.output : result.error ?? "Error"
                  );
                }}
                className="duo-btn duo-btn-secondary"
              >
                Run
              </button>
              {isAuthenticated ? (
                <button
                  onClick={() => submitMutation.mutate()}
                  disabled={submitMutation.isPending}
                  className="duo-btn duo-btn-primary"
                >
                  {submitMutation.isPending
                    ? "Checking..."
                    : "Submit Solution"}
                </button>
              ) : (
                <Link to="/login" className="duo-btn duo-btn-primary">
                  Sign in to submit
                </Link>
              )}
              <button
                onClick={() => setShowHint(!showHint)}
                className="duo-btn bg-[#FFC800] shadow-[0_4px_0_#CC9A00] text-neutral-900"
              >
                {showHint ? "Hide Hint" : "Hint"}
              </button>
            </div>

            {showHint && (
              <div className="bg-[#FFC800]/10 border border-[#FFC800]/30 p-4 rounded-xl">
                <p className="text-sm font-bold text-neutral-800">
                  💡 {selected.hint}
                </p>
              </div>
            )}

            {output && (
              <pre className="bg-neutral-100 border border-[#e5e5e5] p-4 rounded-xl text-sm font-mono whitespace-pre-wrap">
                {output}
              </pre>
            )}
            {message && (
              <p
                className={`text-sm font-black ${
                  message.includes("solved") || message.includes("XP")
                    ? "text-[#58CC02]"
                    : "text-[#FF4B4B]"
                }`}
              >
                {message}
              </p>
            )}
          </div>
        )}
      </div>
    </div>
  );
}
