import { Link } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { fetchCourses } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import {
  ArrowRight,
  BookOpen,
  Trophy,
  Flame,
  Calendar,
  Clock,
} from "lucide-react";

export default function Home() {
  const { isAuthenticated } = useAuth();
  const { data: courses, isLoading } = useQuery({
    queryKey: ["courses"],
    queryFn: fetchCourses,
  });

  const weekDays = ["M", "T", "W", "T", "F", "S", "S"];
  const todayIndex = new Date().getDay();

  return (
    <div className="space-y-8 pb-24 md:pb-0">
      {/* Top Section */}
      <div className="flex items-start justify-between mb-6">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1">
            {isAuthenticated ? "Let's keep learning!" : "Welcome!"}
          </h1>
          <p className="text-neutral-600 font-semibold">
            {isAuthenticated
              ? "Continue your journey today"
              : "Start learning C# today"}
          </p>
        </div>
      </div>

      {/* Streak Section */}
      <div className="duo-card mb-8">
        <div className="flex items-center gap-3 mb-4">
          <Flame className="w-8 h-8 text-[#FF9600]" fill="#FF9600" />
          <div>
            <h2 className="font-black text-xl text-neutral-900">
              5 Day Streak
            </h2>
            <p className="text-sm font-semibold text-neutral-600">
              Keep it up!
            </p>
          </div>
        </div>
        <div className="flex items-center justify-between gap-1">
          {weekDays.map((day, index) => (
            <div
              key={index}
              className={`w-11 h-11 rounded-xl flex items-center justify-center text-sm font-bold transition-all ${
                index < todayIndex
                  ? "bg-[#58CC02] text-white"
                  : index === todayIndex
                  ? "bg-[#1CB0F6] text-white animate-pulse"
                  : "bg-neutral-100 text-neutral-400"
              }`}
            >
              {day}
            </div>
          ))}
        </div>
      </div>

      {/* Continue Learning */}
      {courses && courses.length > 0 && (
        <Link
          to={`/courses/${courses[0].id}`}
          className="duo-card mb-8 hover:translate-y-[-2px] transition-all block"
        >
          <div className="flex items-center justify-between">
            <div className="flex items-center gap-4">
              <div className="w-12 h-12 bg-[#58CC02] rounded-xl flex items-center justify-center">
                <BookOpen className="w-7 h-7 text-white" />
              </div>
              <div>
                <h3 className="font-black text-lg text-neutral-900">
                  {courses[0].title}
                </h3>
                <p className="text-sm font-semibold text-neutral-600">
                  {courses[0].description}
                </p>
              </div>
            </div>
            <ArrowRight className="w-5 h-5 text-neutral-400" />
          </div>
        </Link>
      )}

      {/* All Courses */}
      <div>
        <h2 className="font-black text-lg text-neutral-900 mb-4">
          All Courses
        </h2>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          {isLoading ? (
            Array.from({ length: 4 }).map((_, index) => (
              <div
                key={index}
                className="duo-card animate-pulse"
              >
                <div className="h-9 w-1/2 bg-neutral-200 rounded-lg mb-3" />
                <div className="h-5 w-full bg-neutral-200 rounded mb-2" />
                <div className="h-5 w-2/3 bg-neutral-200 rounded" />
              </div>
            ))
          ) : (
            courses?.map((course) => (
              <Link
                key={course.id}
                to={`/courses/${course.id}`}
                className="duo-card hover:border-[#58CC02] hover:translate-y-[-2px] transition-all"
              >
                <div className="flex items-center justify-between mb-2">
                  <span className="px-3 py-1 bg-neutral-100 rounded-lg text-xs font-bold text-neutral-700">
                    {course.level}
                  </span>
                  <span className="text-xs font-bold text-neutral-600 flex items-center gap-1">
                    <Clock className="w-4 h-4" />
                    {course.estimatedHours}h
                  </span>
                </div>
                <h3 className="text-lg font-black text-neutral-900 mb-1">
                  {course.title}
                </h3>
                <p className="text-sm font-semibold text-neutral-600">
                  {course.description}
                </p>
              </Link>
            ))
          )}
        </div>
      </div>

      {/* Challenges Section */}
      <div>
        <h2 className="font-black text-lg text-neutral-900 mb-4">
          Coding Challenges
        </h2>
        <Link
          to="/challenges"
          className="duo-card bg-[#FFC800]/10 border-[#FFC800]/30 hover:border-[#FFC800]/50 hover:translate-y-[-2px] transition-all"
        >
          <div className="flex items-center justify-between">
            <div className="flex items-center gap-4">
              <div className="w-12 h-12 bg-[#FFC800] rounded-xl flex items-center justify-center">
                <Trophy className="w-7 h-7 text-white" />
              </div>
              <div>
                <h3 className="font-black text-lg text-neutral-900">
                  Test your skills
                </h3>
                <p className="text-sm font-semibold text-neutral-700">
                  Solve problems and earn XP!
                </p>
              </div>
            </div>
            <ArrowRight className="w-5 h-5 text-[#FFC800]" />
          </div>
        </Link>
      </div>
    </div>
  );
}
