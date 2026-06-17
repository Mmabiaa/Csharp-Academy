import { useState } from "react";
import { Link, useParams } from "react-router-dom";
import { useQuery, useMutation } from "@tanstack/react-query";
import { fetchQuiz, submitQuiz } from "../lib/api";
import { useAuth } from "../context/AuthContext";

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
    return <div className="max-w-2xl mx-auto px-4 py-12"><p>Loading quiz...</p></div>;
  }

  if (error || !quiz) {
    return (
      <div className="max-w-2xl mx-auto px-4 py-12">
        <p className="text-red-600">No quiz found for this lesson.</p>
        <Link to={`/lessons/${lessonId}`} className="text-blue-600 hover:underline mt-4 inline-block">Back to lesson</Link>
      </div>
    );
  }

  if (!isAuthenticated) {
    return (
      <div className="max-w-2xl mx-auto px-4 py-12">
        <p className="text-gray-600 mb-4">You must be signed in to take this quiz.</p>
        <Link to="/login" className="text-blue-600 hover:underline">Sign in</Link>
      </div>
    );
  }

  if (result) {
    return (
      <div className="max-w-2xl mx-auto px-4 py-12">
        <h1 className="text-3xl font-bold text-gray-900 mb-4">Quiz Results</h1>
        <div className={`p-6 rounded-lg mb-6 ${result.passed ? "bg-green-50 border border-green-200" : "bg-red-50 border border-red-200"}`}>
          <p className="text-2xl font-semibold mb-2">
            {result.passed ? "Passed!" : "Not quite — keep studying!"}
          </p>
          <p className="text-gray-700">Score: {result.score} / {result.totalQuestions}</p>
          {result.xpEarned > 0 && <p className="text-gray-700">+{result.xpEarned} XP (Total: {result.totalXp})</p>}
          {result.newBadges.length > 0 && (
            <p className="text-gray-700 mt-2">New badges: {result.newBadges.join(", ")}</p>
          )}
        </div>
        <ul className="space-y-3 mb-6">
          {result.questionResults.map((qr) => (
            <li key={qr.questionId} className={`p-3 rounded ${qr.isCorrect ? "bg-green-50" : "bg-red-50"}`}>
              {qr.isCorrect ? "✓ Correct" : "✗ Incorrect"}
            </li>
          ))}
        </ul>
        <Link to={`/lessons/${lessonId}`} className="text-blue-600 hover:underline">Back to lesson</Link>
      </div>
    );
  }

  const allAnswered = quiz.questions.every((q) =>
    isTextQuestion(q.type)
      ? (textAnswers[q.id]?.trim().length ?? 0) > 0
      : optionAnswers[q.id] !== undefined
  );

  return (
    <div className="max-w-2xl mx-auto px-4 py-12">
      <Link to={`/lessons/${lessonId}`} className="text-blue-600 hover:underline text-sm mb-4 inline-block">
        &larr; Back to lesson
      </Link>
      <h1 className="text-3xl font-bold text-gray-900 mb-8">{quiz.title}</h1>

      <div className="space-y-8">
        {quiz.questions.map((question, index) => (
          <div key={question.id} className="bg-white p-6 rounded-lg shadow-md">
            <p className="font-medium text-gray-900 mb-1">{index + 1}. {question.text}</p>
            {question.type === "OutputPrediction" && (
              <p className="text-xs text-purple-600 mb-3">Output prediction — select the correct output</p>
            )}
            {isTextQuestion(question.type) ? (
              <input
                type="text"
                value={textAnswers[question.id] ?? ""}
                onChange={(e) => setTextAnswers((prev) => ({ ...prev, [question.id]: e.target.value }))}
                placeholder="Type your answer"
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 font-mono"
              />
            ) : (
              <div className="space-y-2 mt-4">
                {question.options.map((option) => (
                  <label
                    key={option.id}
                    className={`flex items-center gap-3 p-3 rounded-md border cursor-pointer transition-colors ${
                      optionAnswers[question.id] === option.id
                        ? "border-blue-500 bg-blue-50"
                        : "border-gray-200 hover:border-gray-300"
                    }`}
                  >
                    <input
                      type="radio"
                      name={`question-${question.id}`}
                      checked={optionAnswers[question.id] === option.id}
                      onChange={() => setOptionAnswers((prev) => ({ ...prev, [question.id]: option.id }))}
                      className="text-blue-600"
                    />
                    <span className="font-mono text-sm">{option.text}</span>
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
        className="mt-8 w-full bg-blue-600 text-white py-3 rounded-md hover:bg-blue-700 disabled:opacity-50 font-medium"
      >
        {submitMutation.isPending ? "Submitting..." : "Submit Quiz"}
      </button>
    </div>
  );
}
