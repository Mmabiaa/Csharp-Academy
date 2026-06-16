import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchAnalyticsDashboard } from "../lib/api";
import { useAuth } from "../context/AuthContext";

export default function Analytics() {
  const { token, isTeacher } = useAuth();

  const { data, isLoading, error } = useQuery({
    queryKey: ["analytics"],
    queryFn: () => fetchAnalyticsDashboard(token!),
    enabled: !!token && isTeacher,
  });

  if (!token) {
    return (
      <div className="max-w-5xl mx-auto px-4 py-12">
        <p className="text-gray-600">
          <Link to="/login" className="text-blue-600 hover:underline">Sign in</Link> to view analytics.
        </p>
      </div>
    );
  }

  if (!isTeacher) {
    return (
      <div className="max-w-5xl mx-auto px-4 py-12">
        <p className="text-gray-600">Analytics are available to teachers only.</p>
      </div>
    );
  }

  if (isLoading) {
    return <div className="max-w-5xl mx-auto px-4 py-12"><p>Loading analytics...</p></div>;
  }

  if (error || !data) {
    return <div className="max-w-5xl mx-auto px-4 py-12"><p className="text-red-600">Failed to load analytics.</p></div>;
  }

  const stats = [
    { label: "Total Users", value: data.totalUsers, color: "text-blue-600" },
    { label: "Enrollments", value: data.totalEnrollments, color: "text-green-600" },
    { label: "Quiz Attempts", value: data.totalQuizAttempts, color: "text-purple-600" },
    { label: "Classrooms", value: data.totalClassrooms, color: "text-orange-600" },
    { label: "Certificates", value: data.certificatesIssued, color: "text-indigo-600" },
    { label: "Avg Completion", value: `${data.averageCourseCompletion.toFixed(1)}%`, color: "text-teal-600" },
    { label: "Quiz Pass Rate", value: `${data.quizPassRate.toFixed(1)}%`, color: "text-pink-600" },
  ];

  return (
    <div className="max-w-5xl mx-auto px-4 py-12">
      <h1 className="text-3xl font-bold text-gray-900 mb-8">Analytics Dashboard</h1>

      <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 gap-4 mb-10">
        {stats.map((s) => (
          <div key={s.label} className="bg-white p-4 rounded-lg shadow-md text-center">
            <p className={`text-2xl font-bold ${s.color}`}>{s.value}</p>
            <p className="text-sm text-gray-600">{s.label}</p>
          </div>
        ))}
      </div>

      <section>
        <h2 className="text-xl font-semibold text-gray-900 mb-4">Course Performance</h2>
        {data.courseStats.length === 0 ? (
          <p className="text-gray-500">No course data yet.</p>
        ) : (
          <div className="space-y-4">
            {data.courseStats.map((course) => (
              <div key={course.courseId} className="bg-white p-4 rounded-lg shadow-md">
                <div className="flex justify-between items-center mb-2">
                  <p className="font-medium text-gray-900">{course.courseTitle}</p>
                  <span className="text-sm text-gray-600">{course.enrollmentCount} enrolled</span>
                </div>
                <div className="w-full bg-gray-200 rounded-full h-2">
                  <div
                    className="bg-blue-600 h-2 rounded-full"
                    style={{ width: `${Math.min(course.averageCompletion, 100)}%` }}
                  />
                </div>
                <p className="text-xs text-gray-500 mt-1">{course.averageCompletion.toFixed(1)}% avg completion</p>
              </div>
            ))}
          </div>
        )}
      </section>
    </div>
  );
}
