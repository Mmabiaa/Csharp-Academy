import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { Link, useParams, useSearchParams } from "react-router-dom";
import ReactMarkdown from "react-markdown";
import { useState, useEffect } from "react";
import {
  fetchLesson,
  completeLesson,
  generateQuiz,
  fetchTutorialSteps,
  fetchLessonExercises,
  submitPractice,
  runCode,
  fetchLessonVideos,
} from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { useVoiceNarration } from "../../hooks/useVoiceNarration";
import CodeEditor from "../../components/CodeEditor";
import VideoPlayer from "../../components/VideoPlayer";
import { ArrowLeft, Volume2, VolumeX, CheckCircle2 } from "lucide-react";

type Tab = "read" | "video" | "tutorial" | "practice" | "practices";

export default function LessonPage() {
  const { id } = useParams<{ id: string }>();
  const lessonId = Number(id);
  const [searchParams, setSearchParams] = useSearchParams();
  const initialTab = (searchParams.get("tab") as Tab) || "read";
  const [tab, setTab] = useState<Tab>(initialTab);
  const [tutorialStep, setTutorialStep] = useState(0);
  const [message, setMessage] = useState("");
  const [practiceCode, setPracticeCode] = useState("");
  const [practiceOutput, setPracticeOutput] = useState("");
  const [showHint, setShowHint] = useState(false);
  const [selectedExercise, setSelectedExercise] = useState<number | null>(null);

  const { token, isAuthenticated, isTeacher } = useAuth();
  const queryClient = useQueryClient();
  const { speak, stop, speaking, supported } = useVoiceNarration();

  const { data: lesson, isLoading, error } = useQuery({
    queryKey: ["lesson", lessonId, token],
    queryFn: () => fetchLesson(lessonId, token ?? undefined),
    enabled: !isNaN(lessonId),
  });

  const { data: tutorialSteps } = useQuery({
    queryKey: ["tutorial", lessonId],
    queryFn: () => fetchTutorialSteps(lessonId),
    enabled: !isNaN(lessonId) && (tab === "tutorial" || lesson?.hasTutorial),
  });

  const { data: exercises } = useQuery({
    queryKey: ["exercises", lessonId],
    queryFn: () => fetchLessonExercises(lessonId),
    enabled: !isNaN(lessonId) && (tab === "practice" || lesson?.hasPractice),
  });

  const { data: videos } = useQuery({
    queryKey: ["lesson-videos", lessonId],
    queryFn: () => fetchLessonVideos(lessonId),
    enabled: !isNaN(lessonId) && (tab === "video" || lesson?.hasVideos),
  });

  const activeExercise =
    exercises?.find((e) => e.id === selectedExercise) ?? exercises?.[0];

  useEffect(() => {
    if (activeExercise && !practiceCode) {
      setPracticeCode(activeExercise.starterCode);
    }
  }, [activeExercise, practiceCode]);

  const completeMutation = useMutation({
    mutationFn: () => completeLesson(lessonId, token!),
    onSuccess: (result) => {
      const badgeMsg =
        result.newBadges.length > 0
          ? ` Badges earned: ${result.newBadges.join(", ")}`
          : "";
      let msg = `Lesson complete! +${result.xpEarned} XP (Total: ${result.totalXp}). Streak: ${result.currentStreak} days.${badgeMsg}`;
      if (result.courseCompleted && result.certificateCode) {
        msg += ` 🎓 Course completed! Certificate: ${result.certificateCode}`;
      }
      setMessage(msg);
      queryClient.invalidateQueries({ queryKey: ["lesson", lessonId] });
      queryClient.invalidateQueries({ queryKey: ["course-progress"] });
      queryClient.invalidateQueries({ queryKey: ["profile"] });
      queryClient.invalidateQueries({ queryKey: ["certificates"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  const generateMutation = useMutation({
    mutationFn: () => generateQuiz(lessonId, token!),
    onSuccess: (data) => {
      setMessage(
        `Quiz generated with ${data.questionCount} questions${
          data.usedAi ? " (AI)" : ""
        }.`
      );
      queryClient.invalidateQueries({ queryKey: ["lesson", lessonId] });
      queryClient.invalidateQueries({ queryKey: ["quiz", lessonId] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  const practiceMutation = useMutation({
    mutationFn: () =>
      submitPractice(activeExercise!.id, practiceCode, token!),
    onSuccess: (result) => {
      setPracticeOutput(result.output);
      setMessage(
        result.message +
          (result.xpEarned > 0 ? ` +${result.xpEarned} XP` : "")
      );
      if (result.passed)
        queryClient.invalidateQueries({ queryKey: ["profile"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  const switchTab = (t: Tab) => {
    setTab(t);
    setSearchParams(t === "read" ? {} : { tab: t });
    setMessage("");
  };

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-10 w-40 bg-neutral-200 rounded-xl" />
        <div className="duo-card animate-pulse">
          <div className="h-40" />
        </div>
      </div>
    );
  }

  if (error || !lesson) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <Link
          to={`/courses/${lesson?.courseId}`}
          className="inline-flex items-center gap-2 text-neutral-600 font-bold hover:text-[#58CC02] text-sm"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to course
        </Link>
        <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
          <p className="text-[#FF4B4B] font-black">Lesson not found.</p>
        </div>
      </div>
    );
  }

  const voiceText =
    lesson.voiceSummary ||
    lesson.content.replace(/[#*`]/g, "").slice(0, 500);

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      {/* Back Button & Header */}
      <div>
        <Link
          to={`/courses/${lesson.courseId}`}
          className="inline-flex items-center gap-2 text-neutral-600 font-bold hover:text-[#58CC02] text-sm mb-4"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to {lesson.courseTitle}
        </Link>
        <p className="text-xs font-bold text-neutral-500 uppercase tracking-wider mb-1">
          {lesson.moduleTitle}
        </p>
        <div className="flex flex-wrap items-center justify-between gap-4">
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900">
            {lesson.title}
          </h1>
          {supported && voiceText && (
            <button
              onClick={() => (speaking ? stop() : speak(voiceText))}
              className={`flex items-center gap-2 px-4 py-2 rounded-xl font-bold text-sm ${
                speaking
                  ? "bg-[#FF4B4B]/10 text-[#FF4B4B] border border-[#FF4B4B]/30"
                  : "bg-[#1CB0F6]/10 text-[#1CB0F6] border border-[#1CB0F6]/30 hover:bg-[#1CB0F6]/20"
              }`}
            >
              {speaking ? <VolumeX className="w-4 h-4" /> : <Volume2 className="w-4 h-4" />}
              {speaking ? "Stop" : "Listen"}
            </button>
          )}
        </div>
      </div>

      {/* Tabs */}
      <div className="flex flex-wrap gap-2 mb-6 border-b border-[#e5e5e5] pb-1">
        {(["read", "video", "tutorial", "practice", "practices"] as Tab[]).map(
          (t) => {
            if (t === "video" && !lesson.hasVideos) return null;
            if (t === "tutorial" && !lesson.hasTutorial) return null;
            if (t === "practice" && !lesson.hasPractice) return null;
            if (t === "practices" && !lesson.bestPractices) return null;
            const labels: Record<Tab, string> = {
              read: "Lesson",
              video: "Video",
              tutorial: "Tutorial",
              practice: "Practice",
              practices: "Best Practices",
            };
            return (
              <button
                key={t}
                onClick={() => switchTab(t)}
                className={`px-4 py-2 rounded-t-xl text-sm font-bold transition-colors ${
                  tab === t
                    ? "bg-[#58CC02] text-white shadow-[0_2px_0_#46A301]"
                    : "text-neutral-600 hover:bg-neutral-100"
                }`}
              >
                {labels[t]}
              </button>
            );
          }
        )}
      </div>

      {/* Tab Content */}
      {tab === "read" && (
        <article className="duo-card lesson-content">
          <ReactMarkdown>{lesson.content}</ReactMarkdown>
        </article>
      )}

      {tab === "video" && videos && (
        <div className="space-y-4">
          {videos.length === 0 ? (
            <div className="duo-card">
              <p className="text-neutral-600 font-semibold">No videos for this lesson.</p>
            </div>
          ) : (
            videos.map((v) => (
              <div key={v.id} className="duo-card">
                <div className="flex justify-between items-center mb-4">
                  <h2 className="font-black text-neutral-900">{v.title}</h2>
                  <span className="text-xs font-bold text-neutral-500">
                    {v.durationMinutes} min · {v.provider}
                  </span>
                </div>
                <VideoPlayer embedUrl={v.embedUrl} title={v.title} />
              </div>
            ))
          )}
        </div>
      )}

      {tab === "tutorial" && tutorialSteps && (
        <div className="duo-card">
          {tutorialSteps.length === 0 ? (
            <p className="text-neutral-600 font-semibold">No tutorial steps for this lesson.</p>
          ) : (
            <>
              <p className="text-xs font-bold text-neutral-500 uppercase tracking-wider mb-4">
                Step {tutorialStep + 1} of {tutorialSteps.length}
              </p>
              <h2 className="text-xl font-black text-neutral-900 mb-3">
                {tutorialSteps[tutorialStep].title}
              </h2>
              <p className="text-neutral-700 font-semibold mb-4">
                {tutorialSteps[tutorialStep].content}
              </p>
              {tutorialSteps[tutorialStep].codeSample && (
                <pre className="bg-neutral-100 border border-[#e5e5e5] p-4 rounded-xl text-sm font-mono mb-6 overflow-x-auto">
                  {tutorialSteps[tutorialStep].codeSample}
                </pre>
              )}
              <div className="flex gap-3">
                <button
                  disabled={tutorialStep === 0}
                  onClick={() => setTutorialStep((s) => s - 1)}
                  className="duo-btn duo-btn-disabled disabled:opacity-50"
                >
                  Previous
                </button>
                {tutorialStep < tutorialSteps.length - 1 ? (
                  <button
                    onClick={() => setTutorialStep((s) => s + 1)}
                    className="duo-btn duo-btn-secondary"
                  >
                    Next
                  </button>
                ) : (
                  <button
                    onClick={() =>
                      switchTab(lesson.hasPractice ? "practice" : "read")
                    }
                    className="duo-btn duo-btn-primary"
                  >
                    {lesson.hasPractice ? "Try Practice →" : "Finish"}
                  </button>
                )}
              </div>
            </>
          )}
        </div>
      )}

      {tab === "practice" && exercises && (
        <div className="duo-card space-y-4">
          {!isAuthenticated ? (
            <p className="text-neutral-600 font-semibold">
              <Link
                to="/login"
                className="text-[#58CC02] font-black hover:underline"
              >
                Sign in
              </Link>{" "}
              to submit practices.
            </p>
          ) : exercises.length === 0 ? (
            <p className="text-neutral-600 font-semibold">No coding exercises for this lesson yet.</p>
          ) : (
            <>
              {exercises.length > 1 && (
                <div className="flex gap-2 flex-wrap">
                  {exercises.map((ex) => (
                    <button
                      key={ex.id}
                      onClick={() => {
                        setSelectedExercise(ex.id);
                        setPracticeCode(ex.starterCode);
                        setShowHint(false);
                      }}
                      className={`text-sm px-3 py-1.5 rounded-xl font-bold ${
                        activeExercise?.id === ex.id
                          ? "bg-[#58CC02] text-white"
                          : "bg-neutral-100 text-neutral-700 hover:bg-neutral-200"
                      }`}
                    >
                      {ex.title}
                    </button>
                  ))}
                </div>
              )}
              {activeExercise && (
                <>
                  <h2 className="text-lg font-black text-neutral-900">
                    {activeExercise.title}
                  </h2>
                  <p className="text-neutral-600 font-semibold">
                    {activeExercise.instructions}
                  </p>
                  <CodeEditor
                    value={practiceCode || activeExercise.starterCode}
                    onChange={setPracticeCode}
                    rows={12}
                  />
                  <div className="flex flex-wrap gap-3">
                    <button
                      onClick={async () => {
                        const result = await runCode(
                          practiceCode || activeExercise.starterCode
                        );
                        setPracticeOutput(
                          result.success ? result.output : result.error ?? "Error"
                        );
                      }}
                      className="duo-btn duo-btn-secondary"
                    >
                      Run Code
                    </button>
                    <button
                      onClick={() => practiceMutation.mutate()}
                      disabled={practiceMutation.isPending}
                      className="duo-btn duo-btn-primary"
                    >
                      {practiceMutation.isPending ? "Checking..." : "Submit Solution"}
                    </button>
                    <button
                      onClick={() => setShowHint(!showHint)}
                      className="duo-btn bg-[#FFC800] shadow-[0_4px_0_#CC9A00] text-neutral-900"
                    >
                      {showHint ? "Hide Hint" : "Show Hint"}
                    </button>
                  </div>
                  {showHint && (
                    <div className="bg-[#FFC800]/10 border border-[#FFC800]/30 p-4 rounded-xl">
                      <p className="text-sm font-bold text-neutral-800">
                        💡 {activeExercise.hint}
                      </p>
                    </div>
                  )}
                  {practiceOutput && (
                    <pre className="bg-neutral-100 border border-[#e5e5e5] p-4 rounded-xl text-sm font-mono whitespace-pre-wrap">
                      {practiceOutput}
                    </pre>
                  )}
                </>
              )}
            </>
          )}
        </div>
      )}

      {tab === "practices" && lesson.bestPractices && (
        <article className="duo-card lesson-content border-l-4 border-[#FFC800]">
          <h2 className="text-xl font-black text-neutral-900 mb-4">Best Practices</h2>
          <ReactMarkdown>{lesson.bestPractices}</ReactMarkdown>
        </article>
      )}

      {/* Action Buttons */}
      {tab === "read" && (
        <div className="duo-card">
          <div className="flex flex-wrap items-center gap-3">
            {isAuthenticated ? (
              <>
                {lesson.isCompleted ? (
                  <div className="flex items-center gap-2 text-[#58CC02] font-black">
                    <CheckCircle2 className="w-5 h-5" />
                    Completed
                  </div>
                ) : (
                  <button
                    onClick={() => completeMutation.mutate()}
                    disabled={completeMutation.isPending}
                    className="duo-btn duo-btn-primary"
                  >
                    {completeMutation.isPending ? "Saving..." : "Mark as Complete"}
                  </button>
                )}
                {lesson.hasQuiz && (
                  <Link
                    to={`/lessons/${lessonId}/quiz`}
                    className="duo-btn bg-purple-600 shadow-[0_4px_0_#4C1D95]"
                  >
                    Take Quiz
                  </Link>
                )}
                {lesson.hasTutorial && (
                  <button
                    onClick={() => {
                      setTutorialStep(0);
                      switchTab("tutorial");
                    }}
                    className="duo-btn duo-btn-secondary"
                  >
                    Start Tutorial
                  </button>
                )}
                {lesson.hasPractice && (
                  <button
                    onClick={() => switchTab("practice")}
                    className="duo-btn bg-[#FFC800] shadow-[0_4px_0_#CC9A00] text-neutral-900"
                  >
                    Code Practice
                  </button>
                )}
                {isTeacher && (
                  <button
                    onClick={() => generateMutation.mutate()}
                    disabled={generateMutation.isPending}
                    className="duo-btn bg-[#FF9600] shadow-[0_4px_0_#CC7800]"
                  >
                    {generateMutation.isPending
                      ? "Generating..."
                      : lesson.hasQuiz
                      ? "Regenerate Quiz (AI)"
                      : "Generate Quiz (AI)"}
                  </button>
                )}
                <Link
                  to={`/assistant?lesson=${encodeURIComponent(lesson.title)}`}
                  className="duo-btn duo-btn-secondary"
                >
                  Ask AI Tutor
                </Link>
              </>
            ) : (
              <p className="text-neutral-600 font-semibold">
                <Link
                  to="/login"
                  className="text-[#58CC02] font-black hover:underline"
                >
                  Sign in
                </Link>{" "}
                to track progress.
              </p>
            )}
          </div>
        </div>
      )}

      {/* Message */}
      {message && (
        <div
          className={`duo-card border-2 ${
            message.includes("complete") ||
            message.includes("XP") ||
            message.includes("Correct") ||
            message.includes("generated")
              ? "bg-[#58CC02]/10 border-[#58CC02]/30"
              : "bg-[#FF4B4B]/10 border-[#FF4B4B]/30"
          }`}
        >
          <p
            className={`text-sm font-black ${
              message.includes("complete") ||
              message.includes("XP") ||
              message.includes("Correct") ||
              message.includes("generated")
                ? "text-[#58CC02]"
                : "text-[#FF4B4B]"
            }`}
          >
            {message}
          </p>
        </div>
      )}
    </div>
  );
}
