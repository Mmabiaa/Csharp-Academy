import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchUserProgressSummary } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Trophy, BookOpen, CheckCircle, Star, BarChart3, Lock } from "lucide-react";

export default function ProgressDashboard() {
  const { token, isAuthenticated } = useAuth();

  const { data, isLoading, error } = useQuery({
    queryKey: ["progress-summary"],
    queryFn: () => fetchUserProgressSummary(token!),
    enabled: !!token,
  });

  if (!isAuthenticated) {
    return (
      <div className="max-w-lg mx-auto py-12 text-center">
        <div className="w-16 h-16 rounded-full bg-[#DDF4FF] flex items-center justify-center mx-auto mb-4">
          <Lock className="w-8 h-8 text-[#1CB0F6]" />
        </div>
        <p className="text-neutral-600 font-bold">
          <Link to="/login" className="text-[#58CC02] font-black hover:underline">
            Sign in
          </Link>{" "}
          to track your learning progress.
        </p>
      </div>
    );
  }

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-16 w-64 bg-neutral-200 rounded-xl animate-pulse" />
        <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
          {[1, 2, 3, 4].map((i) => (
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
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900">My progress</h1>
        <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-10">
          <p className="text-[#CC3A3A] font-black text-lg">Couldn't load your progress</p>
        </div>
      </div>
    );
  }

  const stats = [
    { label: "Total XP", value: data.totalXp, icon: Star, bg: "bg-[#FFF8E1]", color: "text-[#FFC800]" },
    { label: "Courses enrolled", value: data.coursesEnrolled, icon: BookOpen, bg: "bg-[#DDF4FF]", color: "text-[#1CB0F6]" },
    { label: "Lessons done", value: data.lessonsCompleted, icon: CheckCircle, bg: "bg-[#EAF8DC]", color: "text-[#58CC02]" },
    { label: "Challenges", value: data.challengesCompleted, icon: Trophy, bg: "bg-[#FFF1E0]", color: "text-[#FF9600]" },
  ];

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 flex items-center gap-2">
          <BarChart3 className="w-7 h-7 text-[#58CC02]" />
          My progress
        </h1>
        <p className="text-neutral-600 font-bold">
          Track your learning journey across all courses
        </p>
      </div>

      <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
        {stats.map((s) => (
          <div key={s.label} className="duo-card text-center">
            <div className={`w-12 h-12 ${s.bg} rounded-2xl flex items-center justify-center mx-auto mb-3`}>
              <s.icon className={`w-6 h-6 ${s.color}`} fill={s.color.includes("#") ? s.color : undefined} />
            </div>
            <p className="text-3xl font-black text-neutral-900 mb-1">{s.value}</p>
            <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">{s.label}</p>
          </div>
        ))}
      </div>

      <div className="space-y-4">
        <h2 className="text-lg font-black text-neutral-900">Course progress</h2>
        {data.courses.length === 0 ? (
          <div className="duo-panel text-center py-10">
            <p className="text-neutral-500 font-bold mb-4">
              You haven't enrolled in any courses yet.
            </p>
            <Link to="/courses" className="duo-btn3d duo-btn3d-green inline-flex">
              Browse courses
            </Link>
          </div>
        ) : (
          data.courses.map((c) => (
            <Link
              key={c.courseId}
              to={`/courses/${c.courseId}`}
              className="duo-card duo-card-hover hover:border-[#58CC02] block"
            >
              <div className="flex justify-between items-center mb-3">
                <h3 className="font-black text-neutral-900">{c.courseTitle}</h3>
                <span className="text-lg font-black text-[#46A302]">
                  {c.completionPercentage.toFixed(0)}%
                </span>
              </div>
              <div className="duo-progress-track !h-3 mb-2">
                <div
                  className="duo-progress-fill"
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