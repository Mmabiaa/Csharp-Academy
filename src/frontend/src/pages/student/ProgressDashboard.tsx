import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchUserProgressSummary } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Trophy, BookOpen, CheckCircle, Star } from "lucide-react";

export default function ProgressDashboard() {
  const { token, isAuthenticated } = useAuth();

  const { data, isLoading, error } = useQuery({
    queryKey: ["progress-summary"],
    queryFn: () => fetchUserProgressSummary(token!),
    enabled: !!token,
  });

  if (!isAuthenticated) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
            My Progress
          </h1>
          <p className="text-neutral-600 font-semibold">
            <Link to="/login" className="text-[#58CC02] font-black hover:underline">Sign in</Link> to track your learning progress.
          </p>
        </div>
      </div>
    );
  }

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-16 w-64 bg-neutral-200 rounded-xl" />
        <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
          {[1,2,3,4].map(i => (
            <div key={i} className="duo-card animate-pulse">
              <div className="h-32" />
            </div>
          ))}
        </div>
      </div>
    );
  }
  
  if (error || !data) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-16 w-64 bg-neutral-200 rounded-xl" />
        <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
          <p className="text-[#FF4B4B] font-black">Failed to load progress.</p>
        </div>
      </div>
    );
  }

  const stats = [
    { label: "Total XP", value: data.totalXp, icon: Star, color: "text-[#FFC800]" },
    { label: "Courses Enrolled", value: data.coursesEnrolled, icon: BookOpen, color: "text-[#1CB0F6]" },
    { label: "Lessons Done", value: data.lessonsCompleted, icon: CheckCircle, color: "text-[#58CC02]" },
    { label: "Challenges", value: data.challengesCompleted, icon: Trophy, color: "text-[#FF9600]" },
  ];

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          My Progress
        </h1>
        <p className="text-neutral-600 font-semibold">
          Track your learning journey across all courses
        </p>
      </div>

      <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
        {stats.map((s) => (
          <div key={s.label} className="duo-card p-5 text-center">
            <div className="w-12 h-12 bg-neutral-100 rounded-xl flex items-center justify-center mx-auto mb-3">
              <s.icon className={`w-6 h-6 ${s.color}`} fill={s.color.includes("#") ? s.color : undefined} />
            </div>
            <p className="text-3xl font-black text-neutral-900 mb-1">{s.value}</p>
            <p className="text-xs font-bold text-neutral-500 uppercase tracking-wider">{s.label}</p>
          </div>
        ))}
      </div>

      <div className="space-y-4">
        <h2 className="text-lg font-black text-neutral-900">Course Progress</h2>
        {data.courses.length === 0 ? (
          <div className="duo-card text-center py-8">
            <p className="text-neutral-600 font-semibold mb-4">
              You haven't enrolled in any courses yet.
            </p>
            <Link to="/courses" className="duo-btn duo-btn-primary">
              Browse Courses
            </Link>
          </div>
        ) : (
          data.courses.map((c) => (
            <Link
              key={c.courseId}
              to={`/courses/${c.courseId}`}
              className="duo-card hover:border-[#58CC02] hover:translate-y-[-2px] transition-all block"
            >
              <div className="flex justify-between items-center mb-3">
                <h3 className="font-black text-neutral-900">{c.courseTitle}</h3>
                <span className="text-lg font-black text-[#58CC02]">
                  {c.completionPercentage.toFixed(0)}%
                </span>
              </div>
              <div className="w-full bg-neutral-100 rounded-full h-3 mb-2">
                <div
                  className="bg-[#58CC02] h-3 rounded-full transition-all"
                  style={{ width: `${c.completionPercentage}%` }}
                />
              </div>
              <p className="text-xs font-bold text-neutral-500">
                {c.completedLessons} of {c.totalLessons} lessons completed
              </p>
            </Link>
          ))
        )}
      </div>
    </div>
  );
}
