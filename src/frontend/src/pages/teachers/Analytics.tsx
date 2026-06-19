import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchAnalyticsDashboard, fetchTeachingAnalytics } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Users, BookOpen, Award, GraduationCap, TrendingUp, Lock, ShieldAlert, XCircle } from "lucide-react";

export default function Analytics() {
  const { token, isTeacher } = useAuth();

  const { data, isLoading, error } = useQuery({
    queryKey: ["analytics", isTeacher ? "teaching" : "general"],
    queryFn: () => isTeacher ? fetchTeachingAnalytics(token!) : fetchAnalyticsDashboard(token!),
    enabled: !!token,
  });

  if (!token) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-6">Analytics</h1>
          <div className="duo-panel text-center py-12">
            <div className="w-16 h-16 rounded-full bg-[#DDF4FF] flex items-center justify-center mx-auto mb-4">
              <Lock className="w-8 h-8 text-[#1CB0F6]" />
            </div>
            <p className="text-neutral-600 font-bold">
              <Link to="/login" className="text-[#58CC02] font-black hover:underline">
                Sign in
              </Link>{" "}
              to view analytics.
            </p>
          </div>
        </div>
      </div>
    );
  }

  if (!isTeacher) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-6">Analytics</h1>
          <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-12">
            <div className="w-16 h-16 rounded-full bg-[#FFDFE0] flex items-center justify-center mx-auto mb-4">
              <ShieldAlert className="w-8 h-8 text-[#FF4B4B]" />
            </div>
            <p className="text-[#CC3A3A] font-black text-lg">Analytics are available to teachers only</p>
          </div>
        </div>
      </div>
    );
  }

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-9 w-64 bg-neutral-200 rounded-2xl animate-pulse" />
        <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 gap-4">
          {[1, 2, 3, 4].map((i) => (
            <div key={i} className="duo-card animate-pulse">
              <div className="h-24" />
            </div>
          ))}
        </div>
      </div>
    );
  }

  if (error || !data) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900">Analytics</h1>
        <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-12">
          <div className="w-16 h-16 rounded-full bg-[#FFDFE0] flex items-center justify-center mx-auto mb-4">
            <XCircle className="w-8 h-8 text-[#FF4B4B]" />
          </div>
          <p className="text-[#CC3A3A] font-black text-lg">Failed to load analytics</p>
        </div>
      </div>
    );
  }

  const stats = [
    { label: "Total Users", value: data.totalUsers, icon: Users, color: "#1CB0F6", bg: "#DDF4FF" },
    { label: "Enrollments", value: data.totalEnrollments, icon: BookOpen, color: "#46A302", bg: "#D7FFB8" },
    { label: "Quiz Attempts", value: data.totalQuizAttempts, icon: Award, color: "#946800", bg: "#FFF1C2" },
    { label: "Classrooms", value: data.totalClassrooms, icon: GraduationCap, color: "#CC7800", bg: "#FFE9D2" },
    { label: "Certificates", value: data.certificatesIssued, icon: Award, color: "#534AB7", bg: "#EEEDFE" },
    {
      label: "Avg Completion",
      value: `${data.averageCourseCompletion.toFixed(1)}%`,
      icon: TrendingUp,
      color: "#0A8CCF",
      bg: "#DDF4FF",
    },
  ];

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div>
        <p className="text-xs font-black text-neutral-400 uppercase tracking-wider mb-1">Teacher tools</p>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900">Analytics dashboard</h1>
      </div>

      <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 gap-4">
        {stats.map((s) => (
          <div key={s.label} className="duo-card duo-card-hover p-4 text-center">
            <div
              className="w-10 h-10 rounded-xl flex items-center justify-center mx-auto mb-3"
              style={{ background: s.bg }}
            >
              <s.icon className="w-6 h-6" style={{ color: s.color }} />
            </div>
            <p className="text-2xl font-black text-neutral-900 mb-1">{s.value}</p>
            <p className="text-xs font-black text-neutral-500 uppercase tracking-wider">{s.label}</p>
          </div>
        ))}
      </div>

      <section>
        <h2 className="text-xl font-black text-neutral-900 mb-4">Course performance</h2>
        {data.courseStats.length === 0 ? (
          <div className="duo-panel text-center py-10">
            <p className="text-neutral-500 font-bold">No course data yet.</p>
          </div>
        ) : (
          <div className="space-y-4">
            {data.courseStats.map((course) => (
              <div key={course.courseId} className="duo-card duo-card-hover">
                <div className="flex justify-between items-center mb-3">
                  <p className="font-black text-neutral-900">{course.courseTitle}</p>
                  <span className="duo-badge duo-badge-blue">{course.enrollmentCount} enrolled</span>
                </div>
                <div className="duo-progress-track">
                  <div
                    className="duo-progress-fill"
                    style={{ width: `${Math.min(course.averageCompletion, 100)}%` }}
                  />
                </div>
                <p className="text-xs font-black text-neutral-500 mt-2">
                  {course.averageCompletion.toFixed(1)}% avg completion
                </p>
              </div>
            ))}
          </div>
        )}
      </section>
    </div>
  );
}