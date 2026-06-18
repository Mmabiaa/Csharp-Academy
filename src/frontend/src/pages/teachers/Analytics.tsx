import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchAnalyticsDashboard } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Users, BookOpen, Award, GraduationCap, TrendingUp } from "lucide-react";

export default function Analytics() {
  const { token, isTeacher } = useAuth();

  const { data, isLoading, error } = useQuery({
    queryKey: ["analytics"],
    queryFn: () => fetchAnalyticsDashboard(token!),
    enabled: !!token && isTeacher,
  });

  if (!token) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
            Analytics
          </h1>
          <p className="text-neutral-600 font-semibold">
            <Link to="/login" className="text-[#58CC02] font-black hover:underline">Sign in</Link> to view analytics.
          </p>
        </div>
      </div>
    );
  }

  if (!isTeacher) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
            Analytics
          </h1>
          <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
            <p className="text-[#FF4B4B] font-black">Analytics are available to teachers only.</p>
          </div>
        </div>
      </div>
    );
  }

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-16 w-64 bg-neutral-200 rounded-xl" />
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
        <div className="h-16 w-64 bg-neutral-200 rounded-xl" />
        <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
          <p className="text-[#FF4B4B] font-black">Failed to load analytics.</p>
        </div>
      </div>
    );
  }

  const stats = [
    { label: "Total Users", value: data.totalUsers, icon: Users, color: "text-[#1CB0F6]" },
    { label: "Enrollments", value: data.totalEnrollments, icon: BookOpen, color: "text-[#58CC02]" },
    { label: "Quiz Attempts", value: data.totalQuizAttempts, icon: Award, color: "text-[#FFC800]" },
    { label: "Classrooms", value: data.totalClassrooms, icon: GraduationCap, color: "text-[#FF9600]" },
    { label: "Certificates", value: data.certificatesIssued, icon: Award, color: "text-purple-600" },
    { label: "Avg Completion", value: `${data.averageCourseCompletion.toFixed(1)}%`, icon: TrendingUp, color: "text-teal-600" },
  ];

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Analytics Dashboard
        </h1>
      </div>

      <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 gap-4">
        {stats.map((s) => (
          <div key={s.label} className="duo-card p-4 text-center">
            <div className="w-10 h-10 bg-neutral-100 rounded-xl flex items-center justify-center mx-auto mb-3">
              <s.icon className={`w-6 h-6 ${s.color}`} />
            </div>
            <p className="text-2xl font-black text-neutral-900 mb-1">{s.value}</p>
            <p className="text-xs font-bold text-neutral-500 uppercase tracking-wider">{s.label}</p>
          </div>
        ))}
      </div>

      <section>
        <h2 className="text-xl font-black text-neutral-900 mb-4">Course Performance</h2>
        {data.courseStats.length === 0 ? (
          <div className="duo-card text-center py-8">
            <p className="text-neutral-600 font-semibold">No course data yet.</p>
          </div>
        ) : (
          <div className="space-y-4">
            {data.courseStats.map((course) => (
              <div key={course.courseId} className="duo-card">
                <div className="flex justify-between items-center mb-2">
                  <p className="font-black text-neutral-900">{course.courseTitle}</p>
                  <span className="text-sm font-bold text-neutral-600">{course.enrollmentCount} enrolled</span>
                </div>
                <div className="w-full bg-neutral-200 rounded-full h-3">
                  <div
                    className="bg-[#58CC02] h-3 rounded-full transition-all"
                    style={{ width: `${Math.min(course.averageCompletion, 100)}%` }}
                  />
                </div>
                <p className="text-xs font-bold text-neutral-500 mt-1">{course.averageCompletion.toFixed(1)}% avg completion</p>
              </div>
            ))}
          </div>
        )}
      </section>
    </div>
  );
}
