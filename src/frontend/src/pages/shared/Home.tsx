import { Link } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { fetchCourses, fetchChallenges, fetchLeaderboard, type Course, type Challenge, type LeaderboardEntry } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import {
  BookOpen,
  Trophy,
  TrendingUp,
  Users,
  ArrowRight,
} from "lucide-react";

export default function Home() {
  const { isAuthenticated, user } = useAuth();
  const { data: courses, isLoading: coursesLoading } = useQuery<Course[], Error>({
    queryKey: ["courses"],
    queryFn: () => fetchCourses(),
  });
  const { data: challenges, isLoading: challengesLoading } = useQuery<Challenge[], Error>({
    queryKey: ["challenges"],
    queryFn: () => fetchChallenges(),
  });
  const { data: leaderboard, isLoading: leaderboardLoading } = useQuery<LeaderboardEntry[], Error>({
    queryKey: ["leaderboard"],
    queryFn: () => fetchLeaderboard(),
  });

  const getGreeting = () => {
    const hour = new Date().getHours();
    if (hour < 12) return "Good morning";
    if (hour < 18) return "Good afternoon";
    return "Good evening";
  };

  const stats = [
    {
      label: "Available Courses",
      value: courses?.length || "—",
      icon: BookOpen,
      href: "/courses",
    },
    {
      label: "Coding Challenges",
      value: challenges?.length || "—",
      icon: Trophy,
      href: "/challenges",
    },
    {
      label: "Active Students",
      value: "—",
      icon: Users,
      href: "/leaderboard",
    },
    {
      label: "XP Earned",
      value: user?.xp || 0,
      icon: TrendingUp,
      href: "/progress",
      authOnly: true,
    },
  ];

  return (
    <div className="space-y-12">
      {/* Header */}
      <div className="flex flex-col gap-4">
        <div className="text-xs uppercase tracking-widest text-gray-500 font-semibold">
          Dashboard
        </div>
        <div className="flex items-baseline gap-3">
          <h1 className="text-5xl font-serif font-bold tracking-tight text-black">
            {getGreeting()},{" "}
            {isAuthenticated ? user?.firstName || "Learner" : "Learner"}.
          </h1>
          <span className="text-4xl font-serif italic text-gray-600">
            Welcome.
          </span>
        </div>
        <p className="text-gray-600 text-lg max-w-2xl">
          Start learning C# today. Build practical projects, practice coding
          challenges, and track your progress.
        </p>
      </div>

      {/* Stats Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        {stats
          .filter((s) => !s.authOnly || isAuthenticated)
          .map((stat: {
            label: string;
            value: number | string;
            icon: React.ComponentType<{ className?: string }>;
            href: string;
            authOnly?: boolean;
          }, index: number) => {
            const Icon = stat.icon;
            return (
              <Link
                key={index}
                to={stat.href}
                className="card card-hover group"
              >
                <div className="flex items-start justify-between">
                  <div className="w-12 h-12 rounded bg-gray-100 flex items-center justify-center group-hover:bg-black group-hover:text-white transition-all">
                    <Icon className="w-6 h-6" />
                  </div>
                  <ArrowRight className="w-5 h-5 text-gray-400 group-hover:translate-x-1 group-hover:text-black transition-all" />
                </div>
                <div className="mt-6">
                  <div className="text-4xl font-serif font-bold text-black">
                    {stat.value}
                  </div>
                  <div className="text-sm text-gray-500 uppercase tracking-wider mt-1">
                    {stat.label}
                  </div>
                </div>
              </Link>
            );
          })}
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
        {/* Featured Courses */}
        <div className="lg:col-span-2 space-y-6">
          <div className="flex items-center justify-between">
            <h2 className="text-2xl font-serif font-semibold">
              Featured Courses
            </h2>
            <Link
              to="/courses"
              className="text-sm font-medium text-gray-600 hover:text-black flex items-center gap-2"
            >
              Browse all <ArrowRight className="w-4 h-4" />
            </Link>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            {coursesLoading ? (
              [1, 2].map((i: number) => (
                <div key={i} className="card animate-pulse">
                  <div className="h-32 bg-gray-100 rounded mb-4" />
                  <div className="h-5 bg-gray-100 rounded w-3/4 mb-2" />
                  <div className="h-4 bg-gray-100 rounded w-full mb-1" />
                  <div className="h-4 bg-gray-100 rounded w-2/3" />
                </div>
              ))
            ) : courses?.slice(0, 4).map((course: Course) => (
              <Link
                key={course.id}
                to={`/courses/${course.id}`}
                className="card card-hover"
              >
                <div className="flex items-center justify-between mb-4">
                  <span className="inline-block px-3 py-1 text-xs font-medium uppercase tracking-wider border border-gray-200 rounded-full text-gray-600">
                    {course.level}
                  </span>
                  <span className="text-sm text-gray-500">
                    {course.estimatedHours}h
                  </span>
                </div>
                <h3 className="text-xl font-serif font-semibold mb-2">
                  {course.title}
                </h3>
                <p className="text-gray-600 text-sm line-clamp-2">
                  {course.description}
                </p>
              </Link>
            ))}
          </div>
        </div>

        {/* Leaderboard */}
        <div className="space-y-6">
          <div className="flex items-center justify-between">
            <h2 className="text-2xl font-serif font-semibold">Leaderboard</h2>
            <Link
              to="/leaderboard"
              className="text-sm font-medium text-gray-600 hover:text-black flex items-center gap-2"
            >
              See all <ArrowRight className="w-4 h-4" />
            </Link>
          </div>

          <div className="card">
            {leaderboardLoading ? (
              <div className="space-y-4">
                {[1, 2, 3].map((i: number) => (
                  <div key={i} className="flex items-center gap-4 animate-pulse">
                    <div className="w-8 h-8 bg-gray-100 rounded-full" />
                    <div className="flex-1">
                      <div className="h-4 bg-gray-100 rounded w-1/2 mb-1" />
                      <div className="h-3 bg-gray-100 rounded w-1/4" />
                    </div>
                    <div className="w-16 h-4 bg-gray-100 rounded" />
                  </div>
                ))}
              </div>
            ) : (
              <div className="space-y-4">
                {leaderboard?.slice(0, 5).map((entry: LeaderboardEntry, index: number) => (
                  <div
                    key={entry.userId}
                    className="flex items-center justify-between py-2"
                  >
                    <div className="flex items-center gap-4">
                      <div
                        className={`w-8 h-8 rounded-full flex items-center justify-center font-bold text-sm ${
                          index === 0
                            ? "bg-black text-white"
                            : "bg-gray-100 text-gray-700"
                        }`}
                      >
                        {index + 1}
                      </div>
                      <div>
                        <div className="text-sm font-medium">
                          {entry.displayName}
                        </div>
                        <div className="text-xs text-gray-500">
                          {entry.xp} XP
                        </div>
                      </div>
                    </div>
                  </div>
                ))}
                {(!leaderboard || leaderboard.length === 0) && (
                  <p className="text-gray-500 text-sm text-center py-8">
                    No leaderboard data yet.
                  </p>
                )}
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
