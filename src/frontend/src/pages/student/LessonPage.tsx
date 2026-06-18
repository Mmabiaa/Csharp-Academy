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
import {
  ArrowLeft,
  Volume2,
  VolumeX,
  CheckCircle2,
  XCircle,
  Lightbulb,
  Play,
  Sparkles,
  Wand2,
  MessageSquare,
  GraduationCap,
  ChevronLeft,
  ChevronRight,
  ShieldCheck,
  Lock,
} from "lucide-react";

type Tab = "read" | "video" | "tutorial" | "practice" | "practices";

const tabIcon: Record<Tab, string> = {
  read: "📖",
  video: "🎬",
  tutorial: "🧭",
  practice: "💻",
  practices: "✅",
};

export default function LessonPage() {
  const { id } = useParams<{ id: string }>();
  const lessonId = Number(id);
  const [searchParams, setSearchParams] = useSearchParams();
  const initialTab = (searchParams.get("tab") as Tab) || "read";
  const [tab, setTab] = useState<Tab>(initialTab);
  const [tutorialStep, setTutorialStep] = useState(0);
  const [message, setMessage] = useState("");
  const [isSuccess, setIsSuccess] = useState(true);
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
      setIsSuccess(true);
      const badgeMsg =
        result.newBadges.length > 0
          ? ` Badges earned: ${result.newBadges.join(", ")}`
          : "";
      let msg = `Lesson complete! +${result.xpEarned} XP (Total: ${result.totalXp}). Streak: ${result.currentStreak} days.${badgeMsg}`;
      if (result.courseCompleted && result.certificateCode) {
        msg += ` Course completed! Certificate: ${result.certificateCode}`;
      }
      setMessage(msg);
      queryClient.invalidateQueries({ queryKey: ["lesson", lessonId] });
      queryClient.invalidateQueries({ queryKey: ["course-progress"] });
      queryClient.invalidateQueries({ queryKey: ["profile"] });
      queryClient.invalidateQueries({ queryKey: ["certificates"] });
    },
    onError: (err: Error) => {
      setIsSuccess(false);
      setMessage(err.message);
    },
  });

  const generateMutation = useMutation({
    mutationFn: () => generateQuiz(lessonId, token!),
    onSuccess: (data) => {
      setIsSuccess(true);
      setMessage(
        `Quiz generated with ${data.questionCount} questions${data.usedAi ? " (AI)" : ""}.`
      );
      queryClient.invalidateQueries({ queryKey: ["lesson", lessonId] });
      queryClient.invalidateQueries({ queryKey: ["quiz", lessonId] });
    },
    onError: (err: Error) => {
      setIsSuccess(false);
      setMessage(err.message);
    },
  });

  const practiceMutation = useMutation({
    mutationFn: () => submitPractice(activeExercise!.id, practiceCode, token!),
    onSuccess: (result) => {
      setPracticeOutput(result.output);
      setIsSuccess(result.passed);
      setMessage(
        result.message + (result.xpEarned > 0 ? ` +${result.xpEarned} XP` : "")
      );
      if (result.passed) queryClient.invalidateQueries({ queryKey: ["profile"] });
    },
    onError: (err: Error) => {
      setIsSuccess(false);
      setMessage(err.message);
    },
  });

  const switchTab = (t: Tab) => {
    setTab(t);
    setSearchParams(t === "read" ? {} : { tab: t });
    setMessage("");
  };

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-6 w-40 bg-neutral-200 rounded-xl animate-pulse" />
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
          className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to course
        </Link>
        <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-12">
          <div className="w-16 h-16 rounded-full bg-[#FFDFE0] flex items-center justify-center mx-auto mb-4">
            <Lock className="w-8 h-8 text-[#FF4B4B]" />
          </div>
          <p className="text-[#CC3A3A] font-black text-lg">Lesson not found</p>
        </div>
      </div>
    );
  }

  const voiceText = lesson.voiceSummary || lesson.content.replace(/[#*`]/g, "").slice(0, 500);

  const availableTabs = (["read", "video", "tutorial", "practice", "practices"] as Tab[]).filter(
    (t) => {
      if (t === "video") return lesson.hasVideos;
      if (t === "tutorial") return lesson.hasTutorial;
      if (t === "practice") return lesson.hasPractice;
      if (t === "practices") return !!lesson.bestPractices;
      return true;
    }
  );

  const labels: Record<Tab, string> = {
    read: "Lesson",
    video: "Video",
    tutorial: "Tutorial",
    practice: "Practice",
    practices: "Best practices",
  };

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      {/* Back Button & Header */}
      <div>
        <Link
          to={`/courses/${lesson.courseId}`}
          className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm mb-4"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to {lesson.courseTitle}
        </Link>
        <p className="text-xs font-black text-neutral-400 uppercase tracking-wider mb-1">
          {lesson.moduleTitle}
        </p>
        <div className="flex flex-wrap items-center justify-between gap-4">
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900">{lesson.title}</h1>
          {supported && voiceText && (
            <button
              onClick={() => (speaking ? stop() : speak(voiceText))}
              className={
                speaking
                  ? "duo-btn3d !px-4 !py-2 !text-xs bg-[#FF4B4B] text-white shadow-[0_3px_0_#EA2B2B]"
                  : "duo-btn3d duo-btn3d-blue !px-4 !py-2 !text-xs"
              }
            >
              {speaking ? <VolumeX className="w-4 h-4" /> : <Volume2 className="w-4 h-4" />}
              {speaking ? "Stop" : "Listen"}
            </button>
          )}
        </div>
      </div>

      {/* Tabs */}
      <div className="flex flex-wrap gap-2">
        {availableTabs.map((t) => (
          <button
            key={t}
            onClick={() => switchTab(t)}
            className={
              tab === t
                ? "duo-btn3d duo-btn3d-green !px-4 !py-2.5 !text-xs"
                : "duo-btn3d duo-btn3d-white !px-4 !py-2.5 !text-xs"
            }
          >
            <span aria-hidden="true">{tabIcon[t]}</span>
            {labels[t]}
          </button>
        ))}
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
            <div className="duo-panel text-center py-10">
              <p className="text-neutral-500 font-bold">No videos for this lesson.</p>
            </div>
          ) : (
            videos.map((v) => (
              <div key={v.id} className="duo-card">
                <div className="flex justify-between items-center mb-4">
                  <h2 className="font-black text-neutral-900">{v.title}</h2>
                  <span className="duo-badge duo-badge-gray">
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
            <p className="text-neutral-500 font-bold">No tutorial steps for this lesson.</p>
          ) : (
            <>
              <div className="flex items-center gap-2 mb-4">
                {tutorialSteps.map((_, i) => (
                  <div
                    key={i}
                    className={`h-2 flex-1 rounded-full ${
                      i <= tutorialStep ? "bg-[#58CC02]" : "bg-neutral-100"
                    }`}
                  />
                ))}
              </div>
              <p className="text-xs font-black text-neutral-400 uppercase tracking-wider mb-4">
                Step {tutorialStep + 1} of {tutorialSteps.length}
              </p>
              <h2 className="text-xl font-black text-neutral-900 mb-3">
                {tutorialSteps[tutorialStep].title}
              </h2>
              <p className="text-neutral-600 font-bold mb-4 leading-relaxed">
                {tutorialSteps[tutorialStep].content}
              </p>
              {tutorialSteps[tutorialStep].codeSample && (
                <pre className="bg-neutral-900 text-[#7FE787] border-2 border-neutral-800 p-4 rounded-2xl text-sm font-mono mb-6 overflow-x-auto">
                  {tutorialSteps[tutorialStep].codeSample}
                </pre>
              )}
              <div className="flex gap-3">
                <button
                  disabled={tutorialStep === 0}
                  onClick={() => setTutorialStep((s) => s - 1)}
                  className="duo-btn3d duo-btn3d-white disabled:opacity-50"
                >
                  <ChevronLeft className="w-4 h-4" />
                  Previous
                </button>
                {tutorialStep < tutorialSteps.length - 1 ? (
                  <button
                    onClick={() => setTutorialStep((s) => s + 1)}
                    className="duo-btn3d duo-btn3d-blue"
                  >
                    Next
                    <ChevronRight className="w-4 h-4" />
                  </button>
                ) : (
                  <button
                    onClick={() => switchTab(lesson.hasPractice ? "practice" : "read")}
                    className="duo-btn3d duo-btn3d-green"
                  >
                    {lesson.hasPractice ? "Try practice" : "Finish"}
                    <ChevronRight className="w-4 h-4" />
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
            <p className="text-neutral-600 font-bold">
              <Link to="/login" className="text-[#58CC02] font-black hover:underline">
                Sign in
              </Link>{" "}
              to submit practices.
            </p>
          ) : exercises.length === 0 ? (
            <p className="text-neutral-500 font-bold">No coding exercises for this lesson yet.</p>
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
                      className={
                        activeExercise?.id === ex.id
                          ? "duo-btn3d duo-btn3d-green !px-3 !py-2 !text-xs"
                          : "duo-btn3d duo-btn3d-white !px-3 !py-2 !text-xs"
                      }
                    >
                      {ex.title}
                    </button>
                  ))}
                </div>
              )}
              {activeExercise && (
                <>
                  <h2 className="text-lg font-black text-neutral-900">{activeExercise.title}</h2>
                  <p className="text-neutral-600 font-bold">{activeExercise.instructions}</p>
                  <CodeEditor
                    value={practiceCode || activeExercise.starterCode}
                    onChange={setPracticeCode}
                    rows={12}
                  />
                  <div className="flex flex-wrap gap-3">
                    <button
                      onClick={async () => {
                        const result = await runCode(practiceCode || activeExercise.starterCode);
                        setPracticeOutput(result.success ? result.output : result.error ?? "Error");
                      }}
                      className="duo-btn3d duo-btn3d-white"
                    >
                      <Play className="w-4 h-4" />
                      Run code
                    </button>
                    <button
                      onClick={() => practiceMutation.mutate()}
                      disabled={practiceMutation.isPending}
                      className="duo-btn3d duo-btn3d-green"
                    >
                      {practiceMutation.isPending ? "Checking..." : "Submit solution"}
                    </button>
                    <button
                      onClick={() => setShowHint(!showHint)}
                      className="duo-btn3d duo-btn3d-yellow"
                    >
                      <Lightbulb className="w-4 h-4" />
                      {showHint ? "Hide hint" : "Show hint"}
                    </button>
                  </div>
                  {showHint && (
                    <div className="bg-[#FFF8E1] border-2 border-[#FFC800]/40 p-4 rounded-2xl flex gap-3">
                      <Lightbulb className="w-5 h-5 text-[#946800] shrink-0 mt-0.5" />
                      <p className="text-sm font-bold text-[#946800]">{activeExercise.hint}</p>
                    </div>
                  )}
                  {practiceOutput && (
                    <pre className="bg-neutral-900 text-neutral-100 border-2 border-neutral-800 p-4 rounded-2xl text-sm font-mono whitespace-pre-wrap">
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
        <article className="duo-card lesson-content border-l-4 border-[#FFC800] flex gap-3 items-start">
          <div className="flex-1">
            <h2 className="text-xl font-black text-neutral-900 mb-4 flex items-center gap-2">
              <ShieldCheck className="w-5 h-5 text-[#FFC800]" />
              Best practices
            </h2>
            <ReactMarkdown>{lesson.bestPractices}</ReactMarkdown>
          </div>
        </article>
      )}

      {/* Action Buttons */}
      {tab === "read" && (
        <div className="duo-card">
          <div className="flex flex-wrap items-center gap-3">
            {isAuthenticated ? (
              <>
                {lesson.isCompleted ? (
                  <div className="inline-flex items-center gap-2 text-[#46A302] font-black bg-[#D7FFB8] px-4 py-2.5 rounded-2xl">
                    <CheckCircle2 className="w-5 h-5" />
                    Completed
                  </div>
                ) : (
                  <button
                    onClick={() => completeMutation.mutate()}
                    disabled={completeMutation.isPending}
                    className="duo-btn3d duo-btn3d-green"
                  >
                    {completeMutation.isPending ? "Saving..." : "Mark as complete"}
                  </button>
                )}
                {lesson.hasQuiz && (
                  <Link
                    to={`/lessons/${lessonId}/quiz`}
                    className="duo-btn3d bg-[#CE82FF] text-white shadow-[0_4px_0_#A568CC]"
                  >
                    <GraduationCap className="w-4 h-4" />
                    Take quiz
                  </Link>
                )}
                {lesson.hasTutorial && (
                  <button
                    onClick={() => {
                      setTutorialStep(0);
                      switchTab("tutorial");
                    }}
                    className="duo-btn3d duo-btn3d-blue"
                  >
                    Start tutorial
                  </button>
                )}
                {lesson.hasPractice && (
                  <button onClick={() => switchTab("practice")} className="duo-btn3d duo-btn3d-yellow">
                    Code practice
                  </button>
                )}
                {isTeacher && (
                  <button
                    onClick={() => generateMutation.mutate()}
                    disabled={generateMutation.isPending}
                    className="duo-btn3d bg-[#FF9600] text-white shadow-[0_4px_0_#CC7800]"
                  >
                    <Wand2 className="w-4 h-4" />
                    {generateMutation.isPending
                      ? "Generating..."
                      : lesson.hasQuiz
                      ? "Regenerate quiz (AI)"
                      : "Generate quiz (AI)"}
                  </button>
                )}
                <Link
                  to={`/assistant?lesson=${encodeURIComponent(lesson.title)}`}
                  className="duo-btn3d duo-btn3d-white"
                >
                  <MessageSquare className="w-4 h-4" />
                  Ask AI tutor
                </Link>
              </>
            ) : (
              <p className="text-neutral-600 font-bold">
                <Link to="/login" className="text-[#58CC02] font-black hover:underline">
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
          className={`flex items-start gap-2 p-4 rounded-2xl font-bold text-sm ${
            isSuccess ? "bg-[#D7FFB8] text-[#46A302]" : "bg-[#FFDFE0] text-[#CC3A3A]"
          }`}
        >
          {isSuccess ? (
            <Sparkles className="w-5 h-5 shrink-0 mt-0.5" />
          ) : (
            <XCircle className="w-5 h-5 shrink-0 mt-0.5" />
          )}
          <span className="font-black">{message}</span>
        </div>
      )}
    </div>
  );
}