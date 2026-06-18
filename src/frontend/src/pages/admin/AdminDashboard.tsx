import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { createCourse, fetchAdminDashboard } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Users, BookOpen, Trophy, FileText, Zap } from "lucide-react";

export default function AdminDashboard() {
  const { token, isAdmin } = useAuth();
  const queryClient = useQueryClient();
  const [showForm, setShowForm] = useState(false);
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [level, setLevel] = useState("Beginner");
  const [hours, setHours] = useState(8);
  const [message, setMessage] = useState("");

  const { data, isLoading, error } = useQuery({
    queryKey: ["admin-dashboard"],
    queryFn: () => fetchAdminDashboard(token!),
    enabled: !!token && isAdmin,
  });

  const createMutation = useMutation({
    mutationFn: () => createCourse(token!, { title, description, level, estimatedHours: hours }),
    onSuccess: () => {
      setMessage("Course created successfully.");
      setShowForm(false);
      setTitle("");
      setDescription("");
      queryClient.invalidateQueries({ queryKey: ["admin-dashboard"] });
      queryClient.invalidateQueries({ queryKey: ["courses"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  if (!token) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
            Admin Panel
          </h1>
          <p className="text-neutral-600 font-semibold">
            <Link to="/login" className="text-[#58CC02] font-black hover:underline">Sign in</Link> as admin to access this panel.
          </p>
        </div>
      </div>
    );
  }

  if (!isAdmin) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
            Admin Panel
          </h1>
          <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
            <p className="text-[#FF4B4B] font-black">Admin access required. Demo: admin@academy.com / Admin123!</p>
          </div>
        </div>
      </div>
    );
  }

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-16 w-64 bg-neutral-200 rounded-xl" />
        <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-5 gap-4">
          {[1, 2, 3, 4, 5].map((i) => (
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
          <p className="text-[#FF4B4B] font-black">Failed to load dashboard.</p>
        </div>
      </div>
    );
  }

  const stats = [
    { label: "Users", value: data.totalUsers, icon: Users, color: "text-[#1CB0F6]" },
    { label: "Courses", value: data.totalCourses, icon: BookOpen, color: "text-[#58CC02]" },
    { label: "Enrollments", value: data.totalEnrollments, icon: Users, color: "text-[#FFC800]" },
    { label: "Assignments", value: data.totalAssignments, icon: FileText, color: "text-[#FF9600]" },
    { label: "Challenges", value: data.totalChallenges, icon: Trophy, color: "text-purple-600" },
  ];

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
            Admin Panel
          </h1>
          <p className="text-neutral-600 font-semibold">Platform overview and course management</p>
        </div>
        <button
          onClick={() => setShowForm(!showForm)}
          className="duo-btn duo-btn-primary"
        >
          {showForm ? "Cancel" : "+ New Course"}
        </button>
      </div>

      <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-5 gap-4">
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

      {showForm && (
        <div className="duo-card space-y-4">
          <h2 className="text-lg font-black text-neutral-900">Create Course</h2>
          <input
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Course title"
            className="w-full px-4 py-3 rounded-xl border border-[#e5e5e5] font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
          />
          <textarea
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Description"
            rows={3}
            className="w-full px-4 py-3 rounded-xl border border-[#e5e5e5] font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
          />
          <div className="flex flex-wrap gap-4">
            <select
              value={level}
              onChange={(e) => setLevel(e.target.value)}
              className="px-4 py-3 rounded-xl border border-[#e5e5e5] font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
            >
              <option>Beginner</option>
              <option>Intermediate</option>
              <option>Advanced</option>
            </select>
            <input
              type="number"
              value={hours}
              onChange={(e) => setHours(Number(e.target.value))}
              className="w-24 px-4 py-3 rounded-xl border border-[#e5e5e5] font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
            />
            <span className="text-neutral-500 self-center text-sm font-bold">estimated hours</span>
          </div>
          <button
            onClick={() => createMutation.mutate()}
            disabled={!title || createMutation.isPending}
            className="duo-btn duo-btn-primary"
          >
            {createMutation.isPending ? "Creating..." : "Create Course"}
          </button>
        </div>
      )}

      <div className="duo-card overflow-hidden">
        <div className="px-6 py-4 border-b border-[#e5e5e5] font-black text-neutral-900">
          Recent Users
        </div>
        <table className="w-full text-sm">
          <thead className="text-neutral-500 border-b border-[#e5e5e5] font-black">
            <tr>
              <th className="text-left px-6 py-3">Name</th>
              <th className="text-left px-6 py-3">Email</th>
              <th className="text-right px-6 py-3 flex items-center justify-end gap-1">
                <Zap className="w-4 h-4" />
                XP
              </th>
            </tr>
          </thead>
          <tbody>
            {data.recentUsers.map((u) => (
              <tr key={u.id} className="border-b border-[#e5e5e5]/50 hover:bg-neutral-50 transition-colors">
                <td className="px-6 py-3 font-semibold text-neutral-900">{u.name}</td>
                <td className="px-6 py-3 text-neutral-600 font-semibold">{u.email}</td>
                <td className="px-6 py-3 text-right text-[#FFC800] font-black">{u.xp}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {message && (
        <div className="duo-card bg-[#58CC02]/10 border-[#58CC02]/30">
          <p className="text-[#58CC02] font-black">{message}</p>
        </div>
      )}
    </div>
  );
}
