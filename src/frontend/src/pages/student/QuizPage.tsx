import { useState } from "react";
import { Link, useParams } from "react-router-dom";
import { useQuery, useMutation } from "@tanstack/react-query";
import { fetchQuiz, submitQuiz } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { useNotifications } from "../../context/NotificationContext";
import { ArrowLeft, CheckCircle2, XCircle, Star, Award, PartyPopper, GraduationCap } from "lucide-react";

function isTextQuestion(type: string) {
  return type === "FillInTheBlank";
}

export default function QuizPage() {
  const { id } = useParams<{ id: string }>();
  const lessonId = Number(id);
  const { token, isAuthenticated } = useAuth();
  const { showNotification } = useNotifications();
  const [optionAnswers, setOptionAnswers] = useState<Record<number, number>>({});
  const [textAnswers, setTextAnswers] = useState<Record<number, string>>({});
  const [result, setResult] = useState<Awaited<ReturnType<typeof submitQuiz>> | null>(null);

  const { data: quiz, isLoading, error } = useQuery({
    queryKey: ["quiz", lessonId],
    queryFn: () => fetchQuiz(lessonId),
    enabled: !isNaN(lessonId),
  });

  const submitMutation = useMutation({
    mutationFn: () => submitQuiz(lessonId, optionAnswers, textAnswers, token!),
    onSuccess: (data) => {
      setResult(data);
      if (data.passed) {
        showNotification({
          type: "congrats",
          title: "Quiz Conquered!",
          message: `You nailed it! ${data.score}/${data.totalQuestions} correct.`,
          xpEarned: data.xpEarned,
          badges: data.newBadges,
        });
      }
    },
  });

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-6 w-40 bg-neutral-200 rounded-xl animate-pulse" />
        <div className="duo-card animate-pulse"><div className="h-40" /></div>
      </div>
    );
  }

  if (error || !quiz) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <Link to={`/lessons/${lessonId}`} className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm">
          <ArrowLeft className="w-5 h-5" />Back to lesson
        </Link>
        <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-10">
          <p className="text-[#CC3A3A] font-black text-lg">No quiz found for this lesson.</p>
        </div>
      </div>
    );
  }

  if (!isAuthenticated) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <Link to={`/lessons/${lessonId}`} className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm">
          <ArrowLeft className="w-5 h-5" />Back to lesson
        </Link>
        <div className="duo-card text-center py-8">
          <p className="text-neutral-700 font-bold mb-4">You must be signed in to take this quiz.</p>
          <Link to="/login" className="duo-btn3d duo-btn3d-green inline-flex">Sign in</Link>
        </div>
      </div>
    );
  }

  /* ── Results screen ── */
  if (result) {
    const scorePercent = Math.round((result.score / result.totalQuestions) * 100);
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <Link to={`/lessons/${lessonId}`} className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm">
          <ArrowLeft className="w-5 h-5" />Back to lesson
        </Link>

        {/* Result hero */}
        <div className="text-center py-4">
          <div className={`w-24 h-24 rounded-3xl flex items-center justify-center mx-auto mb-4 duo-pop ${result.passed ? "bg-[#58CC02] shadow-[0_5px_0_#46A302]" : "bg-[#FF4B4B] shadow-[0_5px_0_#EA2B2B]"
            }`}>
            {result.passed
              ? <PartyPopper className="w-12 h-12 text-white duo-result-icon-pass" />
              : <XCircle className="w-12 h-12 text-white duo-result-icon-fail" />}
          </div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1">
            {result.passed ? "Quiz complete!" : "Not quite — try again"}
          </h1>
          <p className="text-neutral-500 font-bold">
            {result.passed ? "Great work, keep the streak going" : "Review the lesson and give it another shot"}
          </p>
        </div>

        {/* Score stats */}
        <div className="grid grid-cols-2 gap-4">
          <div className="duo-card text-center">
            <div className="w-12 h-12 bg-[#DDF4FF] rounded-2xl flex items-center justify-center mx-auto mb-3 shadow-[0_3px_0_#B3E6FF]">
              <GraduationCap className="w-6 h-6 text-[#1CB0F6] duo-result-grad" />
            </div>
            <p className="text-3xl font-black text-neutral-900 mb-1">{result.score}/{result.totalQuestions}</p>
            <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">Score · {scorePercent}%</p>
          </div>
          <div className="duo-card text-center">
            <div className="w-12 h-12 bg-[#FFF8E1] rounded-2xl flex items-center justify-center mx-auto mb-3 shadow-[0_3px_0_#FFE999]">
              <Star className="w-6 h-6 text-[#FFC800] duo-result-star" fill="#FFC800" />
            </div>
            <p className="text-3xl font-black text-neutral-900 mb-1">+{result.xpEarned}</p>
            <p className="text-xs font-black text-neutral-400 uppercase tracking-wide mt-1">XP earned</p>
          </div>
        </div>

        {result.newBadges.length > 0 && (
          <div className="duo-card bg-[#FFF8E1] !border-[#FFC800]/40 flex items-center gap-3">
            <div className="w-10 h-10 bg-[#FFC800] rounded-2xl flex items-center justify-center shrink-0 shadow-[0_3px_0_#E6B400]">
              <Award className="w-5 h-5 text-white duo-result-award" />
            </div>
            <p className="font-black text-[#946800]">New badges: {result.newBadges.join(", ")}</p>
          </div>
        )}

        <div className="space-y-3">
          {result.questionResults.map((qr, i) => (
            <div key={qr.questionId} className={`flex items-center gap-3 p-4 rounded-2xl border-2 ${qr.isCorrect ? "bg-[#EAF8DC] border-[#C0DD97]" : "bg-[#FFEFEF] border-[#F09595]"
              }`}>
              {qr.isCorrect
                ? <CheckCircle2 className="w-6 h-6 text-[#46A302] shrink-0" />
                : <XCircle className="w-6 h-6 text-[#CC3A3A] shrink-0" />}
              <span className="font-black text-neutral-900">
                Question {i + 1} — {qr.isCorrect ? "Correct" : "Incorrect"}
              </span>
            </div>
          ))}
        </div>

        <Link to={`/lessons/${lessonId}`} className="duo-btn3d duo-btn3d-green w-full !py-4 !text-base">
          Back to lesson
        </Link>

        <style>{`
          @keyframes duo-party-pop {
            0%,100% { transform: scale(1) rotate(0deg); }
            25%     { transform: scale(1.2) rotate(-8deg); }
            50%     { transform: scale(1.1) rotate(6deg); }
            75%     { transform: scale(1.15) rotate(-3deg); }
          }
          @keyframes duo-shake-no {
            0%,100% { transform: rotate(0deg); }
            20%     { transform: rotate(-12deg); }
            40%     { transform: rotate(12deg); }
            60%     { transform: rotate(-8deg); }
            80%     { transform: rotate(8deg); }
          }
          @keyframes duo-star-burst {
            0%,100% { transform: scale(1) rotate(0deg); }
            40%     { transform: scale(1.3) rotate(20deg); }
            70%     { transform: scale(0.93) rotate(-5deg); }
          }
          @keyframes duo-grad-bounce {
            0%,100% { transform: translateY(0) scale(1); }
            40%     { transform: translateY(-5px) scale(1.1); }
            70%     { transform: translateY(-1px) scale(1.05); }
          }
          @keyframes duo-award-swing {
            0%,100% { transform: rotate(0deg); }
            30%     { transform: rotate(-12deg); }
            65%     { transform: rotate(10deg); }
          }
          .duo-result-icon-pass { animation: duo-party-pop  1.8s ease-in-out infinite; }
          .duo-result-icon-fail { animation: duo-shake-no   1.5s ease-in-out infinite; }
          .duo-result-star      { animation: duo-star-burst 2s   ease-in-out infinite; }
          .duo-result-grad      { animation: duo-grad-bounce 2.2s ease-in-out infinite; }
          .duo-result-award     { animation: duo-award-swing 2.4s ease-in-out infinite; }
        `}</style>
      </div>
    );
  }

  /* ── Quiz questions ── */
  const allAnswered = quiz.questions.every((q) =>
    isTextQuestion(q.type)
      ? (textAnswers[q.id]?.trim().length ?? 0) > 0
      : optionAnswers[q.id] !== undefined
  );
  const answeredCount = quiz.questions.filter((q) =>
    isTextQuestion(q.type)
      ? (textAnswers[q.id]?.trim().length ?? 0) > 0
      : optionAnswers[q.id] !== undefined
  ).length;

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <Link to={`/lessons/${lessonId}`} className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm">
        <ArrowLeft className="w-5 h-5" />Back to lesson
      </Link>

      {/* Header + progress */}
      <div className="text-center">
        <div className="w-14 h-14 bg-[#CE82FF] rounded-2xl flex items-center justify-center mx-auto mb-3 shadow-[0_4px_0_#A568CC]">
          <GraduationCap className="w-7 h-7 text-white duo-quiz-grad" />
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-3">{quiz.title}</h1>
        <div className="duo-progress-track max-w-xs mx-auto">
          <div className="duo-progress-fill" style={{ width: `${(answeredCount / quiz.questions.length) * 100}%` }} />
        </div>
        <p className="text-xs font-black text-neutral-400 uppercase tracking-wide mt-2">
          {answeredCount} of {quiz.questions.length} answered
        </p>
      </div>

      {/* Questions */}
      <div className="space-y-4">
        {quiz.questions.map((question, index) => (
          <div key={question.id} className="duo-card">
            <p className="font-black text-neutral-900 mb-3 flex items-start gap-2">
              <span className="w-6 h-6 rounded-full bg-neutral-100 text-neutral-500 text-xs flex items-center justify-center shrink-0 font-black">
                {index + 1}
              </span>
              {question.text}
            </p>
            {question.type === "OutputPrediction" && (
              <p className="duo-badge duo-badge-purple mb-4">Output prediction</p>
            )}
            {isTextQuestion(question.type) ? (
              <input
                type="text"
                value={textAnswers[question.id] ?? ""}
                onChange={(e) => setTextAnswers((prev) => ({ ...prev, [question.id]: e.target.value }))}
                placeholder="Type your answer"
                className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-mono font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
              />
            ) : (
              <div className="space-y-3">
                {question.options.map((option) => (
                  <label
                    key={option.id}
                    className={`flex items-center gap-3 p-4 rounded-2xl border-2 cursor-pointer transition-colors ${optionAnswers[question.id] === option.id
                        ? "border-[#58CC02] bg-[#58CC02]/10"
                        : "border-[#e5e5e5] bg-neutral-50 hover:border-[#58CC02]/30 hover:bg-neutral-100"
                      }`}
                  >
                    <input
                      type="radio"
                      name={`question-${question.id}`}
                      checked={optionAnswers[question.id] === option.id}
                      onChange={() => setOptionAnswers((prev) => ({ ...prev, [question.id]: option.id }))}
                      className="accent-[#58CC02] w-4 h-4"
                    />
                    <span className="font-mono text-sm font-bold text-neutral-800">{option.text}</span>
                  </label>
                ))}
              </div>
            )}
          </div>
        ))}
      </div>

      <button
        onClick={() => submitMutation.mutate()}
        disabled={!allAnswered || submitMutation.isPending}
        className="w-full duo-btn3d duo-btn3d-green"
      >
        {submitMutation.isPending ? "Submitting..." : "Submit quiz"}
      </button>

      <style>{`
        @keyframes duo-grad-bounce {
          0%,100% { transform: translateY(0) scale(1); }
          40%     { transform: translateY(-5px) scale(1.12); }
          70%     { transform: translateY(-1px) scale(1.05); }
        }
        .duo-quiz-grad { animation: duo-grad-bounce 2s ease-in-out infinite; }
      `}</style>
    </div>
  );
}