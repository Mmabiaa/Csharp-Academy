import { useState } from "react";
import { Link, useParams } from "react-router-dom";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { fetchCourseById, enrollInCourse, fetchCourseProgress } from "../lib/api";
import { useAuth } from "../context/AuthContext";

export default function CourseDetail() {
  const { id } = useParams<{ id: string }>();
  const courseId = Number(id);
  const { token, isAuthenticated } = useAuth();
  const queryClient = useQueryClient();
  const [enrollMessage, setEnrollMessage] = useState("");

  const { data: course, isLoading, error } = useQuery({
    queryKey: ["course", courseId],
    queryFn: () => fetchCourseById(courseId),
    enabled: !isNaN(courseId),
  });

  const { data: progress } = useQuery({
    queryKey: ["course-progress", courseId],
    queryFn: () => fetchCourseProgress(courseId, token!),
    enabled: isAuthenticated && !!token && !isNaN(courseId),
  });

  const enrollMutation = useMutation({
    mutationFn: () => enrollInCourse(courseId, token!),
    onSuccess: () => {
      setEnrollMessage("Successfully enrolled in this course!");
      queryClient.invalidateQueries({ queryKey: ["course-progress", courseId] });
    },
    onError: (err: Error) => setEnrollMessage(err.message),
  });

  if (isLoading) {
    return (
      <div className="max-w-4xl mx-auto px-4 py-12">
        <p>Loading course...</p>
      </div>
    );
  }

  if (error || !course) {
    return (
      <div className="max-w-4xl mx-auto px-4 py-12">
        <p className="text-red-600">Course not found.</p>
        <Link to="/courses" className="text-blue-600 hover:underline mt-4 inline-block">Back to courses</Link>
      </div>
    );
  }

  const completedSet = new Set(progress?.completedLessonIds ?? []);

  return (
    <div className="max-w-4xl mx-auto px-4 py-12">
      <Link to="/courses" className="text-blue-600 hover:underline text-sm mb-4 inline-block">&larr; Back to courses</Link>
      <h1 className="text-4xl font-bold text-gray-900 mb-2">{course.title}</h1>
      <p className="text-lg text-gray-600 mb-4">{course.description}</p>

      {progress?.isEnrolled && (
        <div className="mb-6">
          <div className="flex justify-between text-sm text-gray-600 mb-1">
            <span>Your progress</span>
            <span>{progress.completionPercentage}%</span>
          </div>
          <div className="w-full bg-gray-200 rounded-full h-2">
            <div
              className="bg-blue-600 h-2 rounded-full transition-all"
              style={{ width: `${progress.completionPercentage}%` }}
            />
          </div>
        </div>
      )}

      <div className="mb-8">
        {isAuthenticated ? (
          progress?.isEnrolled ? (
            <span className="text-green-600 font-medium">You are enrolled in this course</span>
          ) : (
            <div className="flex items-center gap-4">
              <button
                onClick={() => enrollMutation.mutate()}
                disabled={enrollMutation.isPending}
                className="bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 disabled:opacity-50 font-medium"
              >
                {enrollMutation.isPending ? "Enrolling..." : "Enroll in Course"}
              </button>
              {enrollMessage && (
                <span className={enrollMessage.includes("Success") ? "text-green-600" : "text-red-600"}>
                  {enrollMessage}
                </span>
              )}
            </div>
          )
        ) : (
          <p className="text-gray-600">
            <Link to="/login" className="text-blue-600 hover:underline">Sign in</Link> to enroll in this course.
          </p>
        )}
      </div>

      <div className="space-y-6">
        {course.modules.map((module) => (
          <div key={module.id} className="bg-white rounded-lg shadow-md p-6">
            <h2 className="text-xl font-semibold text-gray-900 mb-1">{module.title}</h2>
            <p className="text-gray-600 mb-4">{module.description}</p>
            <ul className="space-y-2">
              {module.lessons.map((lesson) => {
                const done = completedSet.has(lesson.id);
                return (
                  <li key={lesson.id}>
                    <Link
                      to={`/lessons/${lesson.id}`}
                      className="flex items-center gap-3 p-2 rounded-md hover:bg-gray-50 transition-colors"
                    >
                      <span className={`w-6 h-6 rounded-full text-xs flex items-center justify-center font-medium ${
                        done ? "bg-green-100 text-green-700" : "bg-blue-100 text-blue-700"
                      }`}>
                        {done ? "✓" : lesson.order}
                      </span>
                      <span className={done ? "text-green-700" : "text-gray-700"}>{lesson.title}</span>
                    </Link>
                  </li>
                );
              })}
            </ul>
          </div>
        ))}
      </div>
    </div>
  );
}
