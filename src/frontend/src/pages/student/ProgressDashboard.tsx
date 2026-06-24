import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchUserProgressSummary } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { BarChart3, Lock } from "lucide-react";
import { useEffect } from "react";

// ─── Lottie web-component type declaration ────────────────────────────────────
declare global {
  namespace JSX {
    interface IntrinsicElements {
      "lottie-player": React.DetailedHTMLProps<
        React.HTMLAttributes<HTMLElement> & {
          src?: string;
          background?: string;
          speed?: string;
          loop?: boolean;
          autoplay?: boolean;
          style?: React.CSSProperties;
        },
        HTMLElement
      >;
    }
  }
}

// ─── Reusable Lottie icon ─────────────────────────────────────────────────────
function LottieIcon({
  src,
  size = 40,
  speed = 1,
}: {
  src: string;
  size?: number;
  speed?: number;
}) {
  return (
    <lottie-player
      src={src}
      background="transparent"
      speed={String(speed)}
      style={{ width: size, height: size }}
      loop
      autoplay
    />
  );
}

// ─── Animation paths ──────────────────────────────────────────────────────────
const ANIM = {
  xp: "/animations/award.json",
  courses: "/animations/Books stack.json",
  lessons: "/animations/Check Mark.json",
  challenges: "/animations/trophy.json",
  courseRow: "/animations/Book.json",
} as const;

// ─── Stat card config ─────────────────────────────────────────────────────────
const STATS = [
  {
    key: "totalXp",
    label: "Total XP",
    src: ANIM.xp,
    speed: 0.8,
    bg: "bg-[#FFF8E1]",
    shadow: "shadow-[0_4px_0_#FFE999]",
  },
  {
    key: "coursesEnrolled",
    label: "Courses enrolled",
    src: ANIM.courses,
    speed: 0.7,
    bg: "bg-[#DDF4FF]",
    shadow: "shadow-[0_4px_0_#B3E6FF]",
  },
  {
    key: "lessonsCompleted",
    label: "Lessons done",
    src: ANIM.lessons,
    speed: 0.9,
    bg: "bg-[#EAF8DC]",
    shadow: "shadow-[0_4px_0_#C2EFA0]",
  },
  {
    key: "challengesCompleted",
    label: "Challenges",
    src: ANIM.challenges,
    speed: 0.7,
    bg: "bg-[#FFF1E0]",
    shadow: "shadow-[0_4px_0_#FFD9A8]",
  },
] as const;

export default function ProgressDashboard() {
  const { token, isAuthenticated } = useAuth();

  useEffect(() => {
    import("@lottiefiles/lottie-player");
  }, []);

  const { data, isLoading, error } = useQuery({
    queryKey: ["progress-summary"],
    queryFn: () => fetchUserProgressSummary(token!),
    enabled: !!token,
  });

  // ── Guards ────────────────────────────────────────────────────────────────
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
  // ── Render ────────────────────────────────────────────────────────────────
  return (
    <div className="space-y-6 pb-24 md:pb-0">

      {/* ── Page header — BarChart3 kept as Lucide ── */}
      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 flex items-center gap-2">
          <BarChart3 className="w-7 h-7 text-[#58CC02]" />
          My progress
        </h1>
        <p className="text-neutral-600 font-bold">
          Track your learning journey across all courses
        </p>
      </div>

      {/* ── Stats grid ── */}
      <div className="grid grid-cols-2 lg:grid-cols-4 gap-4">
        {STATS.map((s) => {
          const value = data[s.key as keyof typeof data] as number;
          return (
            <div key={s.label} className="duo-card text-center duo-stat-card">
              <div
                className={`w-16 h-16 ${s.bg} ${s.shadow} rounded-2xl flex items-center justify-center mx-auto mb-3 overflow-hidden`}
              >
                <LottieIcon src={s.src} size={52} speed={s.speed} />
              </div>
              <p className="text-3xl font-black text-neutral-900 mb-1">{value}</p>
              <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">
                {s.label}
              </p>
            </div>
          );
        })}
      </div>

      {/* ── Course progress ── */}
      <div className="space-y-4">
        <h2 className="text-lg font-black text-neutral-900">Course progress</h2>

        {data.courses.length === 0 ? (
          <div className="duo-panel text-center py-10">
            <div className="w-14 h-14 bg-neutral-100 rounded-2xl flex items-center justify-center mx-auto mb-3 overflow-hidden">
              <LottieIcon src={ANIM.courseRow} size={48} speed={0.8} />
            </div>
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
              <div className="flex items-center gap-3 mb-3">
                <div className="w-10 h-10 bg-[#58CC02] rounded-xl flex items-center justify-center shrink-0 shadow-[0_3px_0_#46A302] overflow-hidden">
                  <LottieIcon src={ANIM.courseRow} size={36} speed={0.9} />
                </div>
                <div className="flex-1 flex justify-between items-center">
                  <h3 className="font-black text-neutral-900">{c.courseTitle}</h3>
                  <span className="text-lg font-black text-[#46A302]">
                    {c.completionPercentage.toFixed(0)}%
                  </span>
                </div>
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
