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

  const activeExercise = exercises?.find((e) => e.id === selectedExercise) ?? exercises?.[0];

  useEffect(() => {
    if (activeExercise && !practiceCode) {
      setPracticeCode(activeExercise.starterCode);
    }
  }, [activeExercise, practiceCode]);

  const completeMutation = useMutation({
    mutationFn: () => completeLesson(lessonId, token!),
    onSuccess: (result) => {
      const badgeMsg = result.newBadges.length > 0 ? ` Badges earned: ${result.newBadges.join(", ")}` : "";
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
      setMessage(`Quiz generated with ${data.questionCount} questions${data.usedAi ? " (AI)" : ""}.`);
      queryClient.invalidateQueries({ queryKey: ["lesson", lessonId] });
      queryClient.invalidateQueries({ queryKey: ["quiz", lessonId] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  const practiceMutation = useMutation({
    mutationFn: () => submitPractice(activeExercise!.id, practiceCode, token!),
    onSuccess: (result) => {
      setPracticeOutput(result.output);
      setMessage(result.message + (result.xpEarned > 0 ? ` +${result.xpEarned} XP` : ""));
      if (result.passed) queryClient.invalidateQueries({ queryKey: ["profile"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  const switchTab = (t: Tab) => {
    setTab(t);
    setSearchParams(t === "read" ? {} : { tab: t });
    setMessage("");
  };

  if (isLoading) {
    return <div className="max-w-3xl mx-auto px-4 py-12"><p>Loading lesson...</p></div>;
  }

  if (error || !lesson) {
    return (
      <div className="max-w-3xl mx-auto px-4 py-12">
        <p className="text-red-600">Lesson not found.</p>
        <Link to="/courses" className="text-blue-600 hover:underline mt-4 inline-block">Back to courses</Link>
      </div>
    );
  }

  const voiceText = lesson.voiceSummary || lesson.content.replace(/[#*`]/g, "").slice(0, 500);

  return (
    <div className="max-w-4xl mx-auto px-4 py-10">
      <Link to={`/courses/${lesson.courseId}`} className="text-blue-600 hover:underline text-sm mb-4 inline-block">
        &larr; Back to {lesson.courseTitle}
      </Link>
      <p className="text-sm text-gray-500 mb-1">{lesson.moduleTitle}</p>
      <div className="flex flex-wrap items-center justify-between gap-4 mb-6">
        <h1 className="text-3xl font-bold text-gray-900">{lesson.title}</h1>
        {supported && voiceText && (
          <button
            onClick={() => (speaking ? stop() : speak(voiceText))}
            className={`text-sm px-3 py-1.5 rounded-md font-medium ${
              speaking ? "bg-red-100 text-red-700" : "bg-indigo-100 text-indigo-700 hover:bg-indigo-200"
            }`}
          >
            {speaking ? "⏹ Stop" : "🔊 Listen"}
          </button>
        )}
      </div>

      <div className="flex flex-wrap gap-2 mb-6 border-b border-slate-800 pb-2">
        {(["read", "video", "tutorial", "practice", "practices"] as Tab[]).map((t) => {
          if (t === "video" && !lesson.hasVideos) return null;
          if (t === "tutorial" && !lesson.hasTutorial) return null;
          if (t === "practice" && !lesson.hasPractice) return null;
          if (t === "practices") return null;
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
              className={`px-4 py-2 rounded-t-lg text-sm font-medium ${
                tab === t ? "bg-slate-900 border border-b-0 border-slate-700 text-indigo-400" : "text-slate-400 hover:text-white"
              }`}
            >
              {labels[t]}
            </button>
          );
        })}
        {lesson.bestPractices && (
          <button
            onClick={() => switchTab("practices")}
            className={`px-4 py-2 rounded-t-lg text-sm font-medium ${
              tab === "practices" ? "bg-slate-900 border border-b-0 border-slate-700 text-indigo-400" : "text-slate-400 hover:text-white"
            }`}
          >
            Best Practices
          </button>
        )}
      </div>

      {tab === "read" && (
        <article className="lesson-content bg-slate-900 border border-slate-800 p-6 rounded-xl mb-8">
          <ReactMarkdown>{lesson.content}</ReactMarkdown>
        </article>
      )}

      {tab === "video" && videos && (
        <div className="space-y-6 mb-8">
          {videos.length === 0 ? (
            <p className="text-slate-500">No videos for this lesson.</p>
          ) : (
            videos.map((v) => (
              <div key={v.id} className="rounded-xl bg-slate-900 border border-slate-800 p-5 space-y-3">
                <div className="flex justify-between items-center">
                  <h2 className="font-semibold">{v.title}</h2>
                  <span className="text-xs text-slate-500">{v.durationMinutes} min · {v.provider}</span>
                </div>
                <VideoPlayer embedUrl={v.embedUrl} title={v.title} />
              </div>
            ))
          )}
        </div>
      )}

      {tab === "tutorial" && tutorialSteps && (
        <div className="bg-white p-6 rounded-lg shadow-md mb-8">
          {tutorialSteps.length === 0 ? (
            <p className="text-gray-500">No tutorial steps for this lesson.</p>
          ) : (
            <>
              <p className="text-sm text-gray-500 mb-4">
                Step {tutorialStep + 1} of {tutorialSteps.length}
              </p>
              <h2 className="text-xl font-semibold text-gray-900 mb-3">{tutorialSteps[tutorialStep].title}</h2>
              <p className="text-gray-700 mb-4">{tutorialSteps[tutorialStep].content}</p>
              {tutorialSteps[tutorialStep].codeSample && (
                <pre className="bg-gray-900 text-green-400 p-4 rounded-lg text-sm font-mono mb-4 overflow-x-auto">
                  {tutorialSteps[tutorialStep].codeSample}
                </pre>
              )}
              <div className="flex gap-3">
                <button
                  disabled={tutorialStep === 0}
                  onClick={() => setTutorialStep((s) => s - 1)}
                  className="px-4 py-2 border rounded-md disabled:opacity-50"
                >
                  Previous
                </button>
                {tutorialStep < tutorialSteps.length - 1 ? (
                  <button
                    onClick={() => setTutorialStep((s) => s + 1)}
                    className="px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700"
                  >
                    Next
                  </button>
                ) : (
                  <button
                    onClick={() => switchTab(lesson.hasPractice ? "practice" : "read")}
                    className="px-4 py-2 bg-green-600 text-white rounded-md hover:bg-green-700"
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
        <div className="bg-white p-6 rounded-lg shadow-md mb-8 space-y-4">
          {!isAuthenticated ? (
            <p className="text-gray-600">
              <Link to="/login" className="text-blue-600 hover:underline">Sign in</Link> to submit practices.
            </p>
          ) : exercises.length === 0 ? (
            <p className="text-gray-500">No coding exercises for this lesson yet.</p>
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
                      className={`text-sm px-3 py-1 rounded ${
                        activeExercise?.id === ex.id ? "bg-emerald-600 text-white" : "bg-gray-100 text-gray-700"
                      }`}
                    >
                      {ex.title}
                    </button>
                  ))}
                </div>
              )}
              {activeExercise && (
                <>
                  <h2 className="text-lg font-semibold">{activeExercise.title}</h2>
                  <p className="text-gray-600">{activeExercise.instructions}</p>
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
                      className="px-4 py-2 bg-gray-700 text-white rounded-md hover:bg-gray-800"
                    >
                      Run Code
                    </button>
                    <button
                      onClick={() => practiceMutation.mutate()}
                      disabled={practiceMutation.isPending}
                      className="px-4 py-2 bg-emerald-600 text-white rounded-md hover:bg-emerald-700 disabled:opacity-50"
                    >
                      {practiceMutation.isPending ? "Checking..." : "Submit Solution"}
                    </button>
                    <button
                      onClick={() => setShowHint(!showHint)}
                      className="px-4 py-2 border border-amber-300 text-amber-800 rounded-md hover:bg-amber-50"
                    >
                      {showHint ? "Hide Hint" : "Show Hint"}
                    </button>
                  </div>
                  {showHint && (
                    <p className="text-sm text-amber-800 bg-amber-50 p-3 rounded">💡 {activeExercise.hint}</p>
                  )}
                  {practiceOutput && (
                    <pre className="bg-gray-100 p-3 rounded text-sm font-mono whitespace-pre-wrap">{practiceOutput}</pre>
                  )}
                </>
              )}
            </>
          )}
        </div>
      )}

      {tab === "practices" && lesson.bestPractices && (
        <article className="bg-white p-6 rounded-lg shadow-md mb-8 border-l-4 border-amber-400">
          <h2 className="text-xl font-semibold text-gray-900 mb-4">Best Practices</h2>
          <ReactMarkdown>{lesson.bestPractices}</ReactMarkdown>
        </article>
      )}

      {tab === "read" && (
        <div className="flex flex-wrap items-center gap-4">
          {isAuthenticated ? (
            <>
              {lesson.isCompleted ? (
                <span className="text-green-600 font-medium">✓ Completed</span>
              ) : (
                <button
                  onClick={() => completeMutation.mutate()}
                  disabled={completeMutation.isPending}
                  className="bg-green-600 text-white px-6 py-2 rounded-md hover:bg-green-700 disabled:opacity-50 font-medium"
                >
                  {completeMutation.isPending ? "Saving..." : "Mark as Complete"}
                </button>
              )}
              {lesson.hasQuiz && (
                <Link
                  to={`/lessons/${lessonId}/quiz`}
                  className="bg-purple-600 text-white px-6 py-2 rounded-md hover:bg-purple-700 font-medium"
                >
                  Take Quiz
                </Link>
              )}
              {lesson.hasTutorial && (
                <button
                  onClick={() => { setTutorialStep(0); switchTab("tutorial"); }}
                  className="bg-teal-600 text-white px-6 py-2 rounded-md hover:bg-teal-700 font-medium"
                >
                  Start Tutorial
                </button>
              )}
              {lesson.hasPractice && (
                <button
                  onClick={() => switchTab("practice")}
                  className="bg-emerald-600 text-white px-6 py-2 rounded-md hover:bg-emerald-700 font-medium"
                >
                  Code Practice
                </button>
              )}
              {isTeacher && (
                <button
                  onClick={() => generateMutation.mutate()}
                  disabled={generateMutation.isPending}
                  className="bg-amber-600 text-white px-6 py-2 rounded-md hover:bg-amber-700 disabled:opacity-50 font-medium"
                >
                  {generateMutation.isPending ? "Generating..." : lesson.hasQuiz ? "Regenerate Quiz (AI)" : "Generate Quiz (AI)"}
                </button>
              )}
              <Link
                to={`/assistant?lesson=${encodeURIComponent(lesson.title)}`}
                className="bg-indigo-600 text-white px-6 py-2 rounded-md hover:bg-indigo-700 font-medium"
              >
                Ask AI Tutor
              </Link>
            </>
          ) : (
            <p className="text-gray-600">
              <Link to="/login" className="text-blue-600 hover:underline">Sign in</Link> to track progress.
            </p>
          )}
        </div>
      )}

      {message && (
        <p className={`mt-4 text-sm ${
          message.includes("complete") || message.includes("XP") || message.includes("Correct") || message.includes("generated")
            ? "text-green-600" : "text-red-600"
        }`}>
          {message}
        </p>
      )}
    </div>
  );
}
