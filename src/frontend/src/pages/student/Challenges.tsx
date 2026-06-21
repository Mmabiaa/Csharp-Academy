import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchChallenges, submitChallenge, fetchUserProgressSummary } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { useNotifications } from "../../context/NotificationContext";
import { useSound } from "../../context/SoundContext";
import CodeEditor from "../../components/CodeEditor";
import ConsolePanel from "../../components/ConsolePanel";
import { Trophy, Play, Lightbulb, CheckCircle2, XCircle, Gem, Zap, Flame, Sword } from "lucide-react";

const difficultyBadge: Record<string, string> = {
  Easy: "duo-badge-green",
  Medium: "duo-badge-yellow",
  Hard: "duo-badge-red",
};

const difficultyIcon: Record<string, { icon: React.ElementType; bg: string; shadow: string; animClass: string }> = {
  Easy: { icon: Zap, bg: "bg-[#58CC02]", shadow: "shadow-[0_2px_0_#46A302]", animClass: "duo-diff-easy" },
  Medium: { icon: Flame, bg: "bg-[#FFC800]", shadow: "shadow-[0_2px_0_#E6B400]", animClass: "duo-diff-medium" },
  Hard: { icon: Sword, bg: "bg-[#FF4B4B]", shadow: "shadow-[0_2px_0_#CC3A3A]", animClass: "duo-diff-hard" },
};

export default function Challenges() {
  const { token, isAuthenticated, refreshUser } = useAuth();
  const { showNotification } = useNotifications();
  const { playSound } = useSound();
  const queryClient = useQueryClient();
  const [selectedId, setSelectedId] = useState<number | null>(null);
  const [code, setCode] = useState("");
  const [output, setOutput] = useState("");
  const [message, setMessage] = useState("");
  const [isSuccess, setIsSuccess] = useState(false);
  const [showHint, setShowHint] = useState(false);
  const [filter, setFilter] = useState("All");
  const [runTrigger, setRunTrigger] = useState(0);

  const { data: challenges, isLoading } = useQuery({
    queryKey: ["challenges"],
    queryFn: fetchChallenges,
  });

  const { data: progress } = useQuery({
    queryKey: ["progress-summary"],
    queryFn: () => fetchUserProgressSummary(token!),
    enabled: !!token,
  });

  const selected = challenges?.find((c) => c.id === selectedId) ?? challenges?.[0];

  const submitMutation = useMutation({
    mutationFn: () => submitChallenge(selected!.id, code, token!),
    onSuccess: (result) => {
      setOutput(result.output);
      setIsSuccess(result.passed);
      if (result.passed) {
        playSound("success");
      } else {
        playSound("error");
      }
      setMessage(result.message + (result.xpEarned > 0 ? ` +${result.xpEarned} XP` : ""));
      if (result.passed) {
        showNotification({
          type: "achievement",
          title: "Challenge Solved!",
          message: "Masterful work! This puzzle didn't stand a chance.",
          xpEarned: result.xpEarned,
        });
        queryClient.invalidateQueries({ queryKey: ["progress-summary"] });
        refreshUser();
      }
    },
    onError: (err: Error) => {
      setIsSuccess(false);
      playSound("error");
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
            {[1, 2, 3].map((i) => <div key={i} className="h-24 bg-neutral-200 rounded-2xl animate-pulse" />)}
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
            <div className="w-9 h-9 bg-[#FFC800] rounded-2xl flex items-center justify-center shadow-[0_3px_0_#E6B400]">
              <Trophy className="w-5 h-5 text-white duo-trophy-header" />
            </div>
            Coding challenges
          </h1>
          <p className="text-neutral-600 font-bold">Solve coding puzzles and earn XP!</p>
        </div>
      </div>

      <div className="flex flex-wrap gap-2">
        {["All", "Easy", "Medium", "Hard"].map((d) => (
          <button
            key={d}
            onClick={() => { setFilter(d); playSound("click"); }}
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
          {filtered.map((c) => {
            const diff = difficultyIcon[c.difficulty] ?? difficultyIcon["Easy"];
            const DiffIcon = diff.icon;
            const isSolved = progress?.solvedChallengeIds?.includes(c.id);

            return (
              <button
                key={c.id}
                onClick={() => {
                  setSelectedId(c.id);
                  setCode(c.starterCode);
                  setOutput("");
                  setMessage("");
                  setShowHint(false);
                  playSound("click");
                }}
                className={`w-full text-left p-4 rounded-2xl border-2 transition-all duo-challenge-item ${selected?.id === c.id
                  ? "border-[#58CC02] bg-[#58CC02]/10"
                  : isSolved
                    ? "border-[#D7FFB8] bg-[#F7FFF0]"
                    : "border-[#e5e5e5] bg-white hover:border-[#58CC02]/40 hover:bg-neutral-50"
                  }`}
              >
                <div className="flex items-center gap-3 mb-2">
                  <div className={`w-9 h-9 rounded-xl flex items-center justify-center shrink-0 ${isSolved ? "bg-[#58CC02] shadow-[0_2px_0_#46A302]" : diff.bg + " " + diff.shadow}`}>
                    {isSolved ? (
                      <CheckCircle2 className="w-5 h-5 text-white" />
                    ) : (
                      <DiffIcon className={`w-4 h-4 text-white ${diff.animClass}`} />
                    )}
                  </div>
                  <div className="flex-1 min-w-0">
                    <div className="flex items-center justify-between gap-2">
                      <p className={`font-black truncate ${isSolved ? "text-[#46A302]" : "text-neutral-900"}`}>{c.title}</p>
                      <span className={`duo-badge ${isSolved ? "duo-badge-green" : (difficultyBadge[c.difficulty] ?? "duo-badge-gray")} shrink-0`}>
                        {isSolved ? "Solved" : c.difficulty}
                      </span>
                    </div>
                  </div>
                </div>
                <div className="flex items-center justify-between pl-12">
                  <p className="text-xs font-bold text-neutral-400">{c.tags}</p>
                  <span className="duo-xp-pill">
                    <Gem className="w-3 h-3" />+{c.xpReward}
                  </span>
                </div>
              </button>
            );
          })}
        </div>

        {selected && (
          <div className="lg:col-span-3 duo-card space-y-4">
            <div className="flex items-start gap-3">
              {(() => {
                const diff = difficultyIcon[selected.difficulty] ?? difficultyIcon["Easy"];
                const DiffIcon = diff.icon;
                return (
                  <div className={`w-12 h-12 rounded-2xl flex items-center justify-center shrink-0 ${diff.bg} ${diff.shadow}`}>
                    <DiffIcon className="w-6 h-6 text-white" />
                  </div>
                );
              })()}
              <div>
                <div className="flex items-center gap-3 mb-1">
                  <h2 className="text-xl font-black text-neutral-900">{selected.title}</h2>
                  <div className="flex items-center gap-2">
                    {progress?.solvedChallengeIds?.includes(selected.id) && (
                      <span className="flex items-center gap-1 text-[10px] font-black uppercase tracking-widest text-[#58CC02] bg-[#58CC02]/10 px-2 py-0.5 rounded-lg border border-[#58CC02]/30">
                        <CheckCircle2 className="w-3 h-3" />
                        Solved
                      </span>
                    )}
                    <span className="duo-xp-pill !text-[10px] !px-2 !py-0.5">
                      <Gem className="w-3 h-3" />
                      +{selected.xpReward} XP
                    </span>
                  </div>
                </div>
                <p className="text-neutral-600 font-bold text-sm leading-relaxed">{selected.description}</p>
              </div>
            </div>

            <CodeEditor value={code || selected.starterCode} onChange={setCode} rows={16} />

            <div className="flex flex-wrap gap-3">
              <button
                onClick={() => { setRunTrigger((n) => n + 1); playSound("click"); }}
                className="duo-btn3d duo-btn3d-white"
              >
                <Play className="w-4 h-4" />
                Run
              </button>
              {isAuthenticated ? (
                <button
                  onClick={() => { playSound("click"); submitMutation.mutate(); }}
                  disabled={submitMutation.isPending}
                  className="duo-btn3d duo-btn3d-green"
                >
                  {submitMutation.isPending ? "Checking..." : "Submit solution"}
                </button>
              ) : (
                <Link to="/login" className="duo-btn3d duo-btn3d-green">Sign in to submit</Link>
              )}
              <button onClick={() => { playSound("click"); setShowHint(!showHint); }} className="duo-btn3d duo-btn3d-yellow">
                <Lightbulb className="w-4 h-4" />
                {showHint ? "Hide hint" : "Hint"}
              </button>
            </div>

            {showHint && (
              <div className="bg-[#FFF8E1] border-2 border-[#FFC800]/40 p-4 rounded-2xl flex gap-3">
                <div className="w-8 h-8 bg-[#FFC800] rounded-xl flex items-center justify-center shrink-0 shadow-[0_2px_0_#E6B400]">
                  <Lightbulb className="w-4 h-4 text-white" />
                </div>
                <p className="text-sm font-bold text-[#946800]">{selected.hint}</p>
              </div>
            )}

            <ConsolePanel
              code={code || selected.starterCode}
              runTrigger={runTrigger}
              onResult={(out) => setOutput(out)}
            />

            {output && (
              <pre className="bg-neutral-900 text-neutral-100 border-2 border-neutral-800 p-4 rounded-2xl text-sm font-mono whitespace-pre-wrap">
                {output}
              </pre>
            )}
            {message && (
              <div className={`flex items-center gap-2 p-3 rounded-xl font-black text-sm ${isSuccess ? "bg-[#D7FFB8] text-[#46A302]" : "bg-[#FFDFE0] text-[#CC3A3A]"
                }`}>
                {isSuccess ? <CheckCircle2 className="w-5 h-5 shrink-0" /> : <XCircle className="w-5 h-5 shrink-0" />}
                {message}
              </div>
            )}
          </div>
        )}
      </div>
    </div>
  );
}