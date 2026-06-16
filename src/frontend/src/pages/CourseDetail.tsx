import { useState } from "react";
import { Link, useParams } from "react-router-dom";
import { useQuery, useMutation } from "@tanstack/react-query";
import { fetchCourseById, enrollInCourse } from "../lib/api";
import { useAuth } from "../context/AuthContext";

export default function CourseDetail() {
  const { id } = useParams<{ id: string }>();
  const courseId = Number(id);
  const { token, isAuthenticated } = useAuth();
  const [enrollMessage, setEnrollMessage] = useState("");

  const { data: course, isLoading, error } = useQuery({
    queryKey: ["course", courseId],
    queryFn: () => fetchCourseById(courseId),
    enabled: !isNaN(courseId),
  });

  const enrollMutation = useMutation({
    mutationFn: () => enrollInCourse(courseId, token!),
    onSuccess: () => setEnrollMessage("Successfully enrolled in this course!"),
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

  return (
    <div className="max-w-4xl mx-auto px-4 py-12">
      <Link to="/courses" className="text-blue-600 hover:underline text-sm mb-4 inline-block">&larr; Back to courses</Link>
      <h1 className="text-4xl font-bold text-gray-900 mb-2">{course.title}</h1>
      <p className="text-lg text-gray-600 mb-8">{course.description}</p>

      <div className="mb-8">
        {isAuthenticated ? (
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
              {module.lessons.map((lesson) => (
                <li key={lesson.id} className="flex items-center gap-2 text-gray-700">
                  <span className="w-6 h-6 rounded-full bg-blue-100 text-blue-700 text-xs flex items-center justify-center font-medium">
                    {lesson.order}
                  </span>
                  {lesson.title}
                </li>
              ))}
            </ul>
          </div>
        ))}
      </div>
    </div>
  );
}
