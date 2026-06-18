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
  const { isAuthenticated } = useAuth();
  const { data: courses, isLoading } = useQuery({
    queryKey: ["courses"],
    queryFn: fetchCourses,
  });

  const weekDays = ["M", "T", "W", "T", "F", "S", "S"];
  const todayIndex = new Date().getDay();
  // Convert JS Sunday(0)-based index to a Monday-first index for the strip
  const mondayFirstIndex = (todayIndex + 6) % 7;

  return (
    <div className="space-y-8 pb-24 md:pb-0">
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
              1,240
            </div>
          </div>
        )}
      </div>

      {/* Streak Section — mascot-style banner */}
      <div className="duo-panel relative overflow-hidden">
        <div className="flex items-center gap-3 mb-5">
          <div className="w-14 h-14 rounded-2xl bg-[#FFF1C2] flex items-center justify-center shrink-0 duo-bounce">
            <Flame className="w-8 h-8 text-[#FF9600]" fill="#FF9600" />
          </div>
          <div>
            <h2 className="font-black text-xl text-neutral-900">
              5 day streak!
            </h2>
            <p className="text-sm font-bold text-neutral-500">
              You're on fire — don't break it today
            </p>
          </div>
        </div>
        <div className="flex items-center justify-between gap-2">
          {weekDays.map((day, index) => (
            <div
              key={index}
              className={`duo-day ${
                index < mondayFirstIndex
                  ? "duo-day-done"
                  : index === mondayFirstIndex
                  ? "duo-day-today"
                  : "duo-day-future"
              }`}
            >
              {index < mondayFirstIndex && <Flame className="w-3.5 h-3.5" fill="currentColor" />}
              <span>{day}</span>
            </div>
          ))}
        </div>
      </div>

      {/* Continue Learning */}
      {courses && courses.length > 0 && (
        <Link
          to={`/courses/${courses[0].id}`}
          className="duo-card duo-card-hover mb-2 block border-2 border-[#58CC02]/30 bg-[#58CC02]/5"
        >
          <div className="flex items-center justify-between gap-4">
            <div className="flex items-center gap-4 min-w-0">
              <div className="w-14 h-14 bg-[#58CC02] rounded-2xl flex items-center justify-center shrink-0 shadow-[0_4px_0_#46A302]">
                <BookOpen className="w-7 h-7 text-white" />
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
          <Sparkles className="w-5 h-5 text-[#CE82FF]" />
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
                className="duo-card duo-card-hover hover:border-[#58CC02]"
              >
                <div className="flex items-center justify-between mb-3">
                  <div className="w-11 h-11 bg-[#1CB0F6] rounded-xl flex items-center justify-center shadow-[0_3px_0_#1899D6]">
                    <BookOpen className="w-6 h-6 text-white" />
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
          <Trophy className="w-5 h-5 text-[#FFC800]" />
          Coding challenges
        </h2>
        <Link
          to="/challenges"
          className="duo-card duo-card-hover block border-2 border-[#FFC800]/40 bg-[#FFF8E1]"
        >
          <div className="flex items-center justify-between gap-4">
            <div className="flex items-center gap-4">
              <div className="w-14 h-14 bg-[#FFC800] rounded-2xl flex items-center justify-center shrink-0 shadow-[0_4px_0_#E6B400]">
                <Trophy className="w-7 h-7 text-white" />
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