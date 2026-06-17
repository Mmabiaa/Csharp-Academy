import { useState } from "react";
import { useMutation, useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchChallenges, runCode, submitChallenge } from "../lib/api";
import { useAuth } from "../context/AuthContext";
import CodeEditor from "../components/CodeEditor";

const difficultyColor: Record<string, string> = {
  Easy: "bg-emerald-900/50 text-emerald-400",
  Medium: "bg-amber-900/50 text-amber-400",
  Hard: "bg-red-900/50 text-red-400",
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
      setMessage(result.message + (result.xpEarned > 0 ? ` +${result.xpEarned} XP` : ""));
    },
    onError: (err: Error) => setMessage(err.message),
  });

  const filtered = (challenges ?? []).filter(
    (c) => filter === "All" || c.difficulty === filter
  );

  if (isLoading) return <div className="max-w-6xl mx-auto px-4 py-12 text-slate-400">Loading challenges...</div>;

  return (
    <div className="max-w-6xl mx-auto px-4 py-10">
      <div className="mb-8">
        <h1 className="text-3xl font-bold">Coding Challenges</h1>
        <p className="text-slate-400 mt-1">
          Replit-style coding puzzles — solve them in the editor and earn XP.
        </p>
      </div>

      <div className="flex gap-2 mb-6">
        {["All", "Easy", "Medium", "Hard"].map((d) => (
          <button
            key={d}
            onClick={() => setFilter(d)}
            className={`px-4 py-1.5 rounded-full text-sm font-medium ${
              filter === d ? "bg-indigo-600 text-white" : "bg-slate-800 text-slate-400 hover:text-white"
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
              className={`w-full text-left p-4 rounded-xl border transition-colors ${
                selected?.id === c.id
                  ? "border-indigo-500 bg-indigo-900/20"
                  : "border-slate-800 bg-slate-900 hover:border-slate-700"
              }`}
            >
              <div className="flex items-center justify-between gap-2">
                <p className="font-medium">{c.title}</p>
                <span className={`text-xs px-2 py-0.5 rounded ${difficultyColor[c.difficulty] ?? ""}`}>
                  {c.difficulty}
                </span>
              </div>
              <p className="text-xs text-slate-500 mt-1">{c.tags} · +{c.xpReward} XP</p>
            </button>
          ))}
        </div>

        {selected && (
          <div className="lg:col-span-3 rounded-xl bg-slate-900 border border-slate-800 p-6 space-y-4">
            <div>
              <h2 className="text-xl font-semibold">{selected.title}</h2>
              <p className="text-slate-400 mt-2">{selected.description}</p>
            </div>

            <CodeEditor value={code || selected.starterCode} onChange={setCode} rows={16} />

            <div className="flex flex-wrap gap-3">
              <button
                onClick={async () => {
                  const result = await runCode(code || selected.starterCode);
                  setOutput(result.success ? result.output : result.error ?? "Error");
                }}
                className="px-4 py-2 rounded-lg bg-slate-700 hover:bg-slate-600 text-sm font-medium"
              >
                Run
              </button>
              {isAuthenticated ? (
                <button
                  onClick={() => submitMutation.mutate()}
                  disabled={submitMutation.isPending}
                  className="px-4 py-2 rounded-lg bg-indigo-600 hover:bg-indigo-500 text-sm font-medium disabled:opacity-50"
                >
                  {submitMutation.isPending ? "Checking..." : "Submit Solution"}
                </button>
              ) : (
                <Link to="/login" className="px-4 py-2 rounded-lg bg-indigo-600 text-sm font-medium">
                  Sign in to submit
                </Link>
              )}
              <button
                onClick={() => setShowHint(!showHint)}
                className="px-4 py-2 rounded-lg border border-amber-700 text-amber-400 text-sm"
              >
                {showHint ? "Hide Hint" : "Hint"}
              </button>
            </div>

            {showHint && (
              <p className="text-sm text-amber-300 bg-amber-900/20 p-3 rounded-lg">💡 {selected.hint}</p>
            )}

            {output && (
              <pre className="bg-slate-800 p-3 rounded-lg text-sm font-mono text-emerald-400 whitespace-pre-wrap">
                {output}
              </pre>
            )}
            {message && (
              <p className={`text-sm ${message.includes("solved") || message.includes("XP") ? "text-emerald-400" : "text-red-400"}`}>
                {message}
              </p>
            )}
          </div>
        )}
      </div>
    </div>
  );
}
