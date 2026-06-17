import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchUserProgressSummary } from "../lib/api";
import { useAuth } from "../context/AuthContext";

export default function ProgressDashboard() {
  const { token, isAuthenticated } = useAuth();

  const { data, isLoading, error } = useQuery({
    queryKey: ["progress-summary"],
    queryFn: () => fetchUserProgressSummary(token!),
    enabled: !!token,
  });

  if (!isAuthenticated) {
    return (
      <div className="max-w-5xl mx-auto px-4 py-12 text-slate-400">
        <Link to="/login" className="text-indigo-400 hover:underline">Sign in</Link> to track your learning progress.
      </div>
    );
  }

  if (isLoading) return <div className="max-w-5xl mx-auto px-4 py-12 text-slate-400">Loading progress...</div>;
  if (error || !data) return <div className="max-w-5xl mx-auto px-4 py-12 text-red-400">Failed to load progress.</div>;

  const stats = [
    { label: "Total XP", value: data.totalXp, icon: "⚡" },
    { label: "Courses", value: data.coursesEnrolled, icon: "📚" },
    { label: "Lessons Done", value: data.lessonsCompleted, icon: "✓" },
    { label: "Challenges", value: data.challengesCompleted, icon: "🏆" },
  ];

  return (
    <div className="max-w-5xl mx-auto px-4 py-10">
      <h1 className="text-3xl font-bold mb-2">My Progress</h1>
      <p className="text-slate-400 mb-8">Track your learning journey across all courses — inspired by Khan Academy.</p>

      <div className="grid grid-cols-2 lg:grid-cols-4 gap-4 mb-10">
        {stats.map((s) => (
          <div key={s.label} className="rounded-xl bg-slate-900 border border-slate-800 p-5 text-center">
            <p className="text-2xl mb-1">{s.icon}</p>
            <p className="text-3xl font-bold text-indigo-400">{s.value}</p>
            <p className="text-sm text-slate-400">{s.label}</p>
          </div>
        ))}
      </div>

      <div className="space-y-4">
        <h2 className="text-lg font-semibold">Course Progress</h2>
        {data.courses.length === 0 ? (
          <div className="rounded-xl bg-slate-900 border border-slate-800 p-8 text-center">
            <p className="text-slate-400 mb-4">You haven't enrolled in any courses yet.</p>
            <Link to="/courses" className="px-6 py-2 rounded-lg bg-indigo-600 hover:bg-indigo-500 inline-block">
              Browse Courses
            </Link>
          </div>
        ) : (
          data.courses.map((c) => (
            <Link
              key={c.courseId}
              to={`/courses/${c.courseId}`}
              className="block rounded-xl bg-slate-900 border border-slate-800 p-5 hover:border-indigo-700 transition-colors"
            >
              <div className="flex justify-between items-center mb-3">
                <h3 className="font-medium">{c.courseTitle}</h3>
                <span className="text-sm text-indigo-400">{c.completionPercentage.toFixed(0)}%</span>
              </div>
              <div className="w-full bg-slate-800 rounded-full h-2.5 mb-2">
                <div
                  className="bg-gradient-to-r from-indigo-500 to-violet-500 h-2.5 rounded-full transition-all"
                  style={{ width: `${c.completionPercentage}%` }}
                />
              </div>
              <p className="text-xs text-slate-500">
                {c.completedLessons} of {c.totalLessons} lessons completed
              </p>
            </Link>
          ))
        )}
      </div>
    </div>
  );
}
