import { useState } from "react";
import { Link, useParams } from "react-router-dom";
import { useQuery, useMutation } from "@tanstack/react-query";
import { fetchQuiz, submitQuiz } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { ArrowLeft, CheckCircle2, XCircle, Star, Flame, Award, PartyPopper } from "lucide-react";

function isTextQuestion(type: string) {
  return type === "FillInTheBlank";
}

export default function QuizPage() {
  const { id } = useParams<{ id: string }>();
  const lessonId = Number(id);
  const { token, isAuthenticated } = useAuth();
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
    onSuccess: (data) => setResult(data),
  });

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

  if (error || !quiz) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <Link
          to={`/lessons/${lessonId}`}
          className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to lesson
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
        <Link
          to={`/lessons/${lessonId}`}
          className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to lesson
        </Link>
        <div className="duo-card text-center py-8">
          <p className="text-neutral-700 font-bold mb-4">
            You must be signed in to take this quiz.
          </p>
          <Link to="/login" className="duo-btn3d duo-btn3d-green inline-flex">
            Sign in
          </Link>
        </div>
      </div>
    );
  }

  if (result) {
    const scorePercent = Math.round((result.score / result.totalQuestions) * 100);
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <Link
          to={`/lessons/${lessonId}`}
          className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to lesson
        </Link>

        {/* Result hero */}
        <div className="text-center py-2">
          <div
            className={`w-24 h-24 rounded-3xl flex items-center justify-center mx-auto mb-4 duo-pop ${
              result.passed
                ? "bg-[#58CC02] shadow-[0_5px_0_#46A302]"
                : "bg-[#FF4B4B] shadow-[0_5px_0_#EA2B2B]"
            }`}
          >
            {result.passed ? (
              <PartyPopper className="w-12 h-12 text-white" />
            ) : (
              <XCircle className="w-12 h-12 text-white" />
            )}
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
            <p className="text-3xl font-black text-neutral-900 mb-1">
              {result.score}/{result.totalQuestions}
            </p>
            <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">
              Score · {scorePercent}%
            </p>
          </div>
          <div className="duo-card text-center">
            <div className="flex items-center justify-center gap-1.5">
              <Star className="w-6 h-6 text-[#FFC800]" fill="#FFC800" />
              <p className="text-3xl font-black text-neutral-900">
                +{result.xpEarned}
              </p>
            </div>
            <p className="text-xs font-black text-neutral-400 uppercase tracking-wide mt-1">
              XP earned (total {result.totalXp})
            </p>
          </div>
        </div>

        {result.newBadges.length > 0 && (
          <div className="duo-card bg-[#FFF8E1] !border-[#FFC800]/40 flex items-center gap-3">
            <Award className="w-8 h-8 text-[#FFC800] shrink-0" />
            <p className="font-black text-[#946800]">
              New badges: {result.newBadges.join(", ")}
            </p>
          </div>
        )}

        <div className="space-y-3">
          {result.questionResults.map((qr, i) => (
            <div
              key={qr.questionId}
              className={`flex items-center gap-3 p-4 rounded-2xl border-2 ${
                qr.isCorrect
                  ? "bg-[#EAF8DC] border-[#C0DD97]"
                  : "bg-[#FFEFEF] border-[#F09595]"
              }`}
            >
              {qr.isCorrect ? (
                <CheckCircle2 className="w-6 h-6 text-[#46A302] shrink-0" />
              ) : (
                <XCircle className="w-6 h-6 text-[#CC3A3A] shrink-0" />
              )}
              <span className="font-black text-neutral-900">
                Question {i + 1} — {qr.isCorrect ? "Correct" : "Incorrect"}
              </span>
            </div>
          ))}
        </div>
      </div>
    );
  }

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
      <Link
        to={`/lessons/${lessonId}`}
        className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm"
      >
        <ArrowLeft className="w-5 h-5" />
        Back to lesson
      </Link>

      <div className="text-center">
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-3">{quiz.title}</h1>
        <div className="duo-progress-track max-w-xs mx-auto">
          <div
            className="duo-progress-fill"
            style={{ width: `${(answeredCount / quiz.questions.length) * 100}%` }}
          />
        </div>
        <p className="text-xs font-black text-neutral-400 uppercase tracking-wide mt-2">
          {answeredCount} of {quiz.questions.length} answered
        </p>
      </div>

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
              <p className="duo-badge duo-badge-purple mb-4">
                Output prediction
              </p>
            )}
            {isTextQuestion(question.type) ? (
              <input
                type="text"
                value={textAnswers[question.id] ?? ""}
                onChange={(e) =>
                  setTextAnswers((prev) => ({ ...prev, [question.id]: e.target.value }))
                }
                placeholder="Type your answer"
                className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-mono font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
              />
            ) : (
              <div className="space-y-3">
                {question.options.map((option) => (
                  <label
                    key={option.id}
                    className={`flex items-center gap-3 p-4 rounded-2xl border-2 cursor-pointer transition-colors ${
                      optionAnswers[question.id] === option.id
                        ? "border-[#58CC02] bg-[#58CC02]/10"
                        : "border-[#e5e5e5] bg-neutral-50 hover:border-[#58CC02]/30 hover:bg-neutral-100"
                    }`}
                  >
                    <input
                      type="radio"
                      name={`question-${question.id}`}
                      checked={optionAnswers[question.id] === option.id}
                      onChange={() =>
                        setOptionAnswers((prev) => ({ ...prev, [question.id]: option.id }))
                      }
                      className="accent-[#58CC02] w-4 h-4"
                    />
                    <span className="font-mono text-sm font-bold text-neutral-800">
                      {option.text}
                    </span>
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
    </div>
  );
}