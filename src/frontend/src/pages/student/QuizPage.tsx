import { useState } from "react";
import { Link, useParams } from "react-router-dom";
import { useQuery, useMutation } from "@tanstack/react-query";
import { fetchQuiz, submitQuiz } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { ArrowLeft, CheckCircle2, XCircle } from "lucide-react";

function isTextQuestion(type: string) {
  return type === "FillInTheBlank";
}

export default function QuizPage() {
  const { id } = useParams<{ id: string }>();
  const lessonId = Number(id);
  const { token, isAuthenticated } = useAuth();
  const [optionAnswers, setOptionAnswers] = useState<Record<number, number>>({});
  const [textAnswers, setTextAnswers] = useState<Record<number, string>>({});
  const [result, setResult] = useState<Awaited<
    ReturnType<typeof submitQuiz>
  > | null>(null);

  const { data: quiz, isLoading, error } = useQuery({
    queryKey: ["quiz", lessonId],
    queryFn: () => fetchQuiz(lessonId),
    enabled: !isNaN(lessonId),
  });

  const submitMutation = useMutation({
    mutationFn: () =>
      submitQuiz(lessonId, optionAnswers, textAnswers, token!),
    onSuccess: (data) => setResult(data),
  });

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

  if (error || !quiz) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <Link
          to={`/lessons/${lessonId}`}
          className="inline-flex items-center gap-2 text-neutral-600 font-bold hover:text-[#58CC02] text-sm"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to lesson
        </Link>
        <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
          <p className="text-[#FF4B4B] font-black">No quiz found for this lesson.</p>
        </div>
      </div>
    );
  }

  if (!isAuthenticated) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <Link
          to={`/lessons/${lessonId}`}
          className="inline-flex items-center gap-2 text-neutral-600 font-bold hover:text-[#58CC02] text-sm"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to lesson
        </Link>
        <div className="duo-card">
          <p className="text-neutral-700 font-semibold mb-4">
            You must be signed in to take this quiz.
          </p>
          <Link to="/login" className="duo-btn duo-btn-primary">
            Sign in
          </Link>
        </div>
      </div>
    );
  }

  if (result) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <Link
          to={`/lessons/${lessonId}`}
          className="inline-flex items-center gap-2 text-neutral-600 font-bold hover:text-[#58CC02] text-sm"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to lesson
        </Link>
        <div className="text-center">
          <h1 className="text-3xl font-black text-neutral-900 mb-2">
            Quiz Results
          </h1>
        </div>
        <div
          className={`duo-card border-2 ${
            result.passed
              ? "bg-[#58CC02]/10 border-[#58CC02]/30"
              : "bg-[#FF4B4B]/10 border-[#FF4B4B]/30"
          }`}
        >
          <div className="flex items-center gap-3 mb-3">
            {result.passed ? (
              <CheckCircle2 className="w-10 h-10 text-[#58CC02]" />
            ) : (
              <XCircle className="w-10 h-10 text-[#FF4B4B]" />
            )}
            <p className="text-2xl font-black text-neutral-900">
              {result.passed ? "Passed!" : "Not quite — keep studying!"}
            </p>
          </div>
          <p className="text-neutral-700 font-semibold mb-2">
            Score: {result.score} / {result.totalQuestions}
          </p>
          {result.xpEarned > 0 && (
            <p className="text-neutral-700 font-semibold">
              +{result.xpEarned} XP (Total: {result.totalXp})
            </p>
          )}
          {result.newBadges.length > 0 && (
            <p className="text-neutral-700 font-semibold mt-2">
              New badges: {result.newBadges.join(", ")}
            </p>
          )}
        </div>
        <div className="space-y-3">
          {result.questionResults.map((qr) => (
            <div
              key={qr.questionId}
              className={`duo-card border-2 ${
                qr.isCorrect
                  ? "bg-[#58CC02]/10 border-[#58CC02]/30"
                  : "bg-[#FF4B4B]/10 border-[#FF4B4B]/30"
              }`}
            >
              <div className="flex items-center gap-2">
                {qr.isCorrect ? (
                  <CheckCircle2 className="w-5 h-5 text-[#58CC02]" />
                ) : (
                  <XCircle className="w-5 h-5 text-[#FF4B4B]" />
                )}
                <span className="font-black text-neutral-900">
                  {qr.isCorrect ? "Correct" : "Incorrect"}
                </span>
              </div>
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

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <Link
        to={`/lessons/${lessonId}`}
        className="inline-flex items-center gap-2 text-neutral-600 font-bold hover:text-[#58CC02] text-sm"
      >
        <ArrowLeft className="w-5 h-5" />
        Back to lesson
      </Link>
      <div className="text-center">
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900">
          {quiz.title}
        </h1>
      </div>
      <div className="space-y-4">
        {quiz.questions.map((question, index) => (
          <div key={question.id} className="duo-card">
            <p className="font-black text-neutral-900 mb-3">
              {index + 1}. {question.text}
            </p>
            {question.type === "OutputPrediction" && (
              <p className="text-xs font-bold text-purple-600 mb-4">
                Output prediction — select the correct output
              </p>
            )}
            {isTextQuestion(question.type) ? (
              <input
                type="text"
                value={textAnswers[question.id] ?? ""}
                onChange={(e) =>
                  setTextAnswers((prev) => ({
                    ...prev,
                    [question.id]: e.target.value,
                  }))
                }
                placeholder="Type your answer"
                className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-mono font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
              />
            ) : (
              <div className="space-y-3">
                {question.options.map((option) => (
                  <label
                    key={option.id}
                    className={`flex items-center gap-3 p-4 rounded-xl border-2 cursor-pointer transition-colors ${
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
                        setOptionAnswers((prev) => ({
                          ...prev,
                          [question.id]: option.id,
                        }))
                      }
                      className="text-[#58CC02]"
                    />
                    <span className="font-mono text-sm font-semibold text-neutral-800">
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
        className="w-full duo-btn duo-btn-primary"
      >
        {submitMutation.isPending ? "Submitting..." : "Submit Quiz"}
      </button>
    </div>
  );
}
