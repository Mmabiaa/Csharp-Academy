import { Link } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { fetchCourses } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import {
  ArrowRight,
  BookOpen,
  Trophy,
  Flame,
  Clock,
  Gem,
  Sparkles,
} from "lucide-react";

export default function Home() {
  const { isAuthenticated, user } = useAuth();
  const { data: courses, isLoading } = useQuery({
    queryKey: ["courses"],
    queryFn: fetchCourses,
  });

  const weekDays = ["M", "T", "W", "T", "F", "S", "S"];
  const todayIndex = new Date().getDay();
  const mondayFirstIndex = (todayIndex + 6) % 7;
  const streakDays = user?.currentStreak ?? 0;
  const userXp = user?.xp ?? 0;

  return (
    <div className="space-y-8 pb-24 md:pb-0">
      <style>{`
        @keyframes duo-flame-dance {
          0%, 100% { transform: rotate(-8deg) scale(1); }
          25% { transform: rotate(8deg) scale(1.1); }
          50% { transform: rotate(-5deg) scale(1.05); }
          75% { transform: rotate(6deg) scale(1.08); }
        }
        @keyframes duo-book-flip {
          0%, 100% { transform: scaleX(1) rotate(0deg); }
          30% { transform: scaleX(0.85) rotate(-6deg); }
          60% { transform: scaleX(1.08) rotate(3deg); }
        }
        @keyframes duo-trophy-wobble {
          0%, 100% { transform: rotate(0deg) scale(1); }
          25% { transform: rotate(-10deg) scale(1.1); }
          50% { transform: rotate(10deg) scale(1.1); }
          75% { transform: rotate(-5deg) scale(1.05); }
        }
        @keyframes duo-sparkle-pop {
          0%, 100% { transform: scale(1) rotate(0deg); opacity: 1; }
          40% { transform: scale(1.25) rotate(15deg); opacity: 0.9; }
          70% { transform: scale(0.95) rotate(-5deg); opacity: 1; }
        }

        /* Continuous ambient icon animations — always running, hover no longer required */
        .duo-home-flame-icon { animation: duo-flame-dance 2.4s ease-in-out infinite; }
        .duo-book-icon       { animation: duo-book-flip 2.8s ease-in-out infinite; }
        .duo-trophy-icon     { animation: duo-trophy-wobble 2.8s ease-in-out infinite; }
        .duo-course-book     { animation: duo-book-flip 2.6s ease-in-out infinite; }
        .duo-sparkle-icon    { animation: duo-sparkle-pop 2.4s ease-in-out infinite; }
        .duo-header-trophy   { animation: duo-trophy-wobble 3s ease-in-out infinite; }

        /* Stagger repeated course-link icons so they don't pulse in lockstep */
        .duo-course-link:nth-child(2n) .duo-course-book { animation-delay: 0.3s; }
        .duo-course-link:nth-child(3n) .duo-course-book { animation-delay: 0.6s; }

        @media (prefers-reduced-motion: reduce) {
          .duo-home-flame-icon, .duo-book-icon, .duo-trophy-icon,
          .duo-course-book, .duo-sparkle-icon, .duo-header-trophy {
            animation: none;
          }
        }
      `}</style>

      {/* Top Section */}
      <div className="flex items-start justify-between gap-4 mb-2">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1">
            {isAuthenticated ? "Let's keep learning! 🦉" : "Welcome!"}
          </h1>
          <p className="text-neutral-600 font-bold">
            {isAuthenticated
              ? "Continue your journey today"
              : "Start learning C# today"}
          </p>
        </div>
        {isAuthenticated && (
          <div className="hidden sm:flex items-center gap-2 shrink-0">
            <div className="duo-xp-pill !text-sm !px-3 !py-2">
              <Gem className="w-4 h-4" />
              {userXp.toLocaleString()}
            </div>
          </div>
        )}
      </div>

      {/* Streak Section */}
      <div
        className="duo-panel relative overflow-hidden"
        style={{
          background: "linear-gradient(135deg, #FFFBEA 0%, #FFF3CC 55%, #FFE7A8 100%)",
        }}
      >
        <Flame
          className="absolute -right-6 -top-6 w-40 h-40 text-[#FF9600] opacity-10 rotate-12"
          fill="currentColor"
        />

        <div className="relative flex items-center gap-4 mb-6">
          {/* Animated flame bubble */}
          <div className="w-16 h-16 rounded-full bg-[#FF9600] flex items-center justify-center shrink-0 shadow-[0_4px_0_#CC7800]">
            <Flame
              className="w-9 h-9 text-white duo-home-flame-icon"
              fill="white"
            />
          </div>
          <div>
            <div className="flex items-baseline gap-2">
              <span className="text-4xl font-black text-[#CC7800] leading-none">
                {streakDays}
              </span>
              <span className="font-black text-lg text-neutral-900">day streak!</span>
            </div>
            <p className="text-sm font-bold text-neutral-600 mt-1">
              You're on fire — don't break it today
            </p>
          </div>
        </div>

        <p className="relative text-xs font-black text-[#946800]/70 uppercase tracking-wider mb-3">
          This week
        </p>
        <div className="relative flex items-center justify-between gap-2">
          {weekDays.map((day, index) => {
            const isActive = index <= mondayFirstIndex && index > mondayFirstIndex - streakDays;
            const isToday = index === mondayFirstIndex;
            return (
              <div
                key={index}
                className={`duo-day !rounded-full !w-11 !h-11 !gap-0 ${isActive ? "duo-day-done" : isToday ? "duo-day-today" : "duo-day-future"
                  }`}
              >
                {isActive ? (
                  <Flame className="w-4.5 h-4.5" fill="currentColor" />
                ) : (
                  <span className="text-xs">{day}</span>
                )}
              </div>
            );
          })}
        </div>
      </div>

      {/* Continue Learning */}
      {courses && courses.length > 0 && (
        <Link
          to={`/courses/${courses[0].id}`}
          className="duo-card duo-card-hover mb-2 block border-2 border-[#58CC02]/30 bg-[#58CC02]/5 duo-continue-card"
        >
          <div className="flex items-center justify-between gap-4">
            <div className="flex items-center gap-4 min-w-0">
              <div className="w-14 h-14 bg-[#58CC02] rounded-2xl flex items-center justify-center shrink-0 shadow-[0_4px_0_#46A302]">
                <BookOpen className="w-7 h-7 text-white duo-book-icon" />
              </div>
              <div className="min-w-0">
                <p className="text-xs font-black uppercase tracking-wide text-[#46A302] mb-0.5">
                  Continue learning
                </p>
                <h3 className="font-black text-lg text-neutral-900 truncate">
                  {courses[0].title}
                </h3>
                <p className="text-sm font-bold text-neutral-500 truncate">
                  {courses[0].description}
                </p>
              </div>
            </div>
            <button className="duo-btn3d duo-btn3d-green !px-5 !py-3 shrink-0">
              Jump in
              <ArrowRight className="w-4 h-4" />
            </button>
          </div>
        </Link>
      )}

      {/* All Courses */}
      <div>
        <h2 className="font-black text-lg text-neutral-900 mb-4 flex items-center gap-2">
          <Sparkles className="w-5 h-5 text-[#CE82FF] duo-sparkle-icon" />
          All courses
        </h2>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          {isLoading ? (
            Array.from({ length: 4 }).map((_, index) => (
              <div key={index} className="duo-card animate-pulse">
                <div className="h-10 w-10 bg-neutral-200 rounded-xl mb-3" />
                <div className="h-5 w-full bg-neutral-200 rounded mb-2" />
                <div className="h-5 w-2/3 bg-neutral-200 rounded" />
              </div>
            ))
          ) : (
            courses?.map((course) => (
              <Link
                key={course.id}
                to={`/courses/${course.id}`}
                className="duo-card duo-card-hover hover:border-[#58CC02] duo-course-link"
              >
                <div className="flex items-center justify-between mb-3">
                  <div className="w-11 h-11 bg-[#1CB0F6] rounded-xl flex items-center justify-center shadow-[0_3px_0_#1899D6]">
                    <BookOpen className="w-6 h-6 text-white duo-course-book" />
                  </div>
                  <span className="duo-badge duo-badge-gray">
                    <Clock className="w-3 h-3" />
                    {course.estimatedHours}h
                  </span>
                </div>
                <h3 className="text-lg font-black text-neutral-900 mb-1">
                  {course.title}
                </h3>
                <p className="text-sm font-bold text-neutral-500 mb-3">
                  {course.description}
                </p>
                <span className="duo-badge duo-badge-blue">{course.level}</span>
              </Link>
            ))
          )}
        </div>
      </div>

      {/* Challenges Section */}
      <div>
        <h2 className="font-black text-lg text-neutral-900 mb-4 flex items-center gap-2">
          <Trophy className="w-5 h-5 text-[#FFC800] duo-header-trophy" />
          Coding challenges
        </h2>
        <Link
          to="/challenges"
          className="duo-card duo-card-hover block border-2 border-[#FFC800]/40 bg-[#FFF8E1] duo-challenge-card"
        >
          <div className="flex items-center justify-between gap-4">
            <div className="flex items-center gap-4">
              <div className="w-14 h-14 bg-[#FFC800] rounded-2xl flex items-center justify-center shrink-0 shadow-[0_4px_0_#E6B400]">
                <Trophy className="w-7 h-7 text-white duo-trophy-icon" />
              </div>
              <div>
                <h3 className="font-black text-lg text-neutral-900">
                  Test your skills
                </h3>
                <p className="text-sm font-bold text-neutral-600">
                  Solve problems and earn XP!
                </p>
              </div>
            </div>
            <ArrowRight className="w-5 h-5 text-[#946800] shrink-0" />
          </div>
        </Link>
      </div>
    </div>
  );
}