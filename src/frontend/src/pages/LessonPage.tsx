import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { Link, useParams } from "react-router-dom";
import ReactMarkdown from "react-markdown";
import { fetchLesson, completeLesson } from "../lib/api";
import { useAuth } from "../context/AuthContext";
import { useState } from "react";

export default function LessonPage() {
  const { id } = useParams<{ id: string }>();
  const lessonId = Number(id);
  const { token, isAuthenticated } = useAuth();
  const queryClient = useQueryClient();
  const [message, setMessage] = useState("");

  const { data: lesson, isLoading, error } = useQuery({
    queryKey: ["lesson", lessonId, token],
    queryFn: () => fetchLesson(lessonId, token ?? undefined),
    enabled: !isNaN(lessonId),
  });

  const completeMutation = useMutation({
    mutationFn: () => completeLesson(lessonId, token!),
    onSuccess: (result) => {
      const badgeMsg = result.newBadges.length > 0 ? ` Badges earned: ${result.newBadges.join(", ")}` : "";
      setMessage(
        `Lesson complete! +${result.xpEarned} XP (Total: ${result.totalXp}). Streak: ${result.currentStreak} days.${badgeMsg}`
      );
      queryClient.invalidateQueries({ queryKey: ["lesson", lessonId] });
      queryClient.invalidateQueries({ queryKey: ["course-progress"] });
      queryClient.invalidateQueries({ queryKey: ["profile"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

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

  return (
    <div className="max-w-3xl mx-auto px-4 py-12">
      <Link to={`/courses/${lesson.courseId}`} className="text-blue-600 hover:underline text-sm mb-4 inline-block">
        &larr; Back to {lesson.courseTitle}
      </Link>
      <p className="text-sm text-gray-500 mb-1">{lesson.moduleTitle}</p>
      <h1 className="text-3xl font-bold text-gray-900 mb-6">{lesson.title}</h1>

      <article className="lesson-content bg-white p-6 rounded-lg shadow-md mb-8">
        <ReactMarkdown>{lesson.content}</ReactMarkdown>
      </article>

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
          </>
        ) : (
          <p className="text-gray-600">
            <Link to="/login" className="text-blue-600 hover:underline">Sign in</Link> to track progress.
          </p>
        )}
      </div>

      {message && (
        <p className={`mt-4 text-sm ${message.includes("complete") || message.includes("XP") ? "text-green-600" : "text-red-600"}`}>
          {message}
        </p>
      )}
    </div>
  );
}
