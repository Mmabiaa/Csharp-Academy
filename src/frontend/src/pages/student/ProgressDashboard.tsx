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
          <Link to="/login" className="text-[#58CC02] font-black hover:underline">Sign in</Link>{" "}
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
            <div key={i} className="duo-card animate-pulse"><div className="h-32" /></div>
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
    {
      label: "Total XP",
      value: data.totalXp,
      icon: Star,
      bg: "bg-[#FFF8E1]",
      shadow: "shadow-[0_4px_0_#FFE999]",
      iconColor: "text-[#FFC800]",
      fill: "#FFC800",
      animClass: "duo-progress-star",
    },
    {
      label: "Courses enrolled",
      value: data.coursesEnrolled,
      icon: BookOpen,
      bg: "bg-[#DDF4FF]",
      shadow: "shadow-[0_4px_0_#B3E6FF]",
      iconColor: "text-[#1CB0F6]",
      fill: undefined,
      animClass: "duo-progress-book",
    },
    {
      label: "Lessons done",
      value: data.lessonsCompleted,
      icon: CheckCircle,
      bg: "bg-[#EAF8DC]",
      shadow: "shadow-[0_4px_0_#C2EFA0]",
      iconColor: "text-[#58CC02]",
      fill: "#58CC02",
      animClass: "duo-progress-check",
    },
    {
      label: "Challenges",
      value: data.challengesCompleted,
      icon: Trophy,
      bg: "bg-[#FFF1E0]",
      shadow: "shadow-[0_4px_0_#FFD9A8]",
      iconColor: "text-[#FF9600]",
      fill: "#FF9600",
      animClass: "duo-progress-trophy",
    },
  ];

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <style>{`
        @keyframes duo-star-spin {
          0%, 100% { transform: scale(1) rotate(0deg); }
          40% { transform: scale(1.3) rotate(20deg); }
          70% { transform: scale(0.92) rotate(-5deg); }
        }
        @keyframes duo-book-flip {
          0%, 100% { transform: scaleX(1) rotate(0deg); }
          30% { transform: scaleX(0.85) rotate(-6deg); }
          65% { transform: scaleX(1.08) rotate(3deg); }
        }
        @keyframes duo-check-pop {
          0%, 100% { transform: scale(1); }
          40% { transform: scale(1.25); }
          70% { transform: scale(0.93); }
        }
        @keyframes duo-trophy-wobble {
          0%, 100% { transform: rotate(0deg) scale(1); }
          25% { transform: rotate(-10deg) scale(1.1); }
          50% { transform: rotate(10deg) scale(1.1); }
          75% { transform: rotate(-4deg) scale(1.04); }
        }
        @keyframes duo-book-flip-row {
          0%, 100% { transform: scaleX(1) rotate(0deg); }
          30% { transform: scaleX(0.85) rotate(-5deg); }
          65% { transform: scaleX(1.07) rotate(3deg); }
        }
        .duo-stat-card:hover .duo-progress-star   { animation: duo-star-spin    0.5s ease-in-out; }
        .duo-stat-card:hover .duo-progress-book   { animation: duo-book-flip    0.5s ease-in-out; }
        .duo-stat-card:hover .duo-progress-check  { animation: duo-check-pop    0.4s ease-in-out; }
        .duo-stat-card:hover .duo-progress-trophy { animation: duo-trophy-wobble 0.55s ease-in-out; }
        .duo-course-row:hover .duo-course-book    { animation: duo-book-flip-row 0.5s ease-in-out; }
      `}</style>

      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 flex items-center gap-2">
          <BarChart3 className="w-7 h-7 text-[#58CC02]" />
          My progress
        </h1>
        <p className="text-neutral-600 font-bold">Track your learning journey across all courses</p>
      </div>

      {/* Stats Grid */}
      <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
        {stats.map((s) => {
          const Icon = s.icon;
          return (
            <div key={s.label} className="duo-card text-center duo-stat-card">
              <div className={`w-14 h-14 ${s.bg} ${s.shadow} rounded-2xl flex items-center justify-center mx-auto mb-3`}>
                <Icon
                  className={`w-7 h-7 ${s.iconColor} ${s.animClass}`}
                  fill={s.fill}
                />
              </div>
              <p className="text-3xl font-black text-neutral-900 mb-1">{s.value}</p>
              <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">{s.label}</p>
            </div>
          );
        })}
      </div>

      {/* Course Progress */}
      <div className="space-y-4">
        <h2 className="text-lg font-black text-neutral-900">Course progress</h2>
        {data.courses.length === 0 ? (
          <div className="duo-panel text-center py-10">
            <div className="w-14 h-14 bg-neutral-100 rounded-2xl flex items-center justify-center mx-auto mb-3">
              <BookOpen className="w-7 h-7 text-neutral-400" />
            </div>
            <p className="text-neutral-500 font-bold mb-4">You haven't enrolled in any courses yet.</p>
            <Link to="/courses" className="duo-btn3d duo-btn3d-green inline-flex">Browse courses</Link>
          </div>
        ) : (
          data.courses.map((c) => (
            <Link
              key={c.courseId}
              to={`/courses/${c.courseId}`}
              className="duo-card duo-card-hover hover:border-[#58CC02] block duo-course-row"
            >
              <div className="flex items-center gap-3 mb-3">
                <div className="w-10 h-10 bg-[#58CC02] rounded-xl flex items-center justify-center shrink-0 shadow-[0_3px_0_#46A302]">
                  <BookOpen className="w-5 h-5 text-white duo-course-book" />
                </div>
                <div className="flex-1 flex justify-between items-center">
                  <h3 className="font-black text-neutral-900">{c.courseTitle}</h3>
                  <span className="text-lg font-black text-[#46A302]">{c.completionPercentage.toFixed(0)}%</span>
                </div>
              </div>
              <div className="duo-progress-track !h-3 mb-2">
                <div className="duo-progress-fill" style={{ width: `${c.completionPercentage}%` }} />
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