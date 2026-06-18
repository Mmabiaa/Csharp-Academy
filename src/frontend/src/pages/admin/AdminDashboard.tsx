import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { createCourse, fetchAdminDashboard } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Users, BookOpen, Trophy, FileText, Zap, Plus, Lock, ShieldAlert, Sparkles, XCircle } from "lucide-react";

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
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-6">Admin Panel</h1>
          <div className="duo-panel text-center py-12">
            <div className="w-16 h-16 rounded-full bg-[#DDF4FF] flex items-center justify-center mx-auto mb-4">
              <Lock className="w-8 h-8 text-[#1CB0F6]" />
            </div>
            <p className="text-neutral-600 font-bold">
              <Link to="/login" className="text-[#58CC02] font-black hover:underline">
                Sign in
              </Link>{" "}
              as admin to access this panel.
            </p>
          </div>
        </div>
      </div>
    );
  }

  if (!isAdmin) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-6">Admin Panel</h1>
          <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-12">
            <div className="w-16 h-16 rounded-full bg-[#FFDFE0] flex items-center justify-center mx-auto mb-4">
              <ShieldAlert className="w-8 h-8 text-[#FF4B4B]" />
            </div>
            <p className="text-[#CC3A3A] font-black text-lg">Admin access required</p>
            <p className="text-neutral-500 font-bold text-sm mt-1">
              Demo: admin@academy.com / Admin123!
            </p>
          </div>
        </div>
      </div>
    );
  }

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-9 w-64 bg-neutral-200 rounded-2xl animate-pulse" />
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
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900">Admin Panel</h1>
        <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-12">
          <div className="w-16 h-16 rounded-full bg-[#FFDFE0] flex items-center justify-center mx-auto mb-4">
            <XCircle className="w-8 h-8 text-[#FF4B4B]" />
          </div>
          <p className="text-[#CC3A3A] font-black text-lg">Failed to load dashboard</p>
        </div>
      </div>
    );
  }

  const stats = [
    { label: "Users", value: data.totalUsers, icon: Users, color: "#1CB0F6", bg: "#DDF4FF" },
    { label: "Courses", value: data.totalCourses, icon: BookOpen, color: "#46A302", bg: "#D7FFB8" },
    { label: "Enrollments", value: data.totalEnrollments, icon: Users, color: "#946800", bg: "#FFF1C2" },
    { label: "Assignments", value: data.totalAssignments, icon: FileText, color: "#CC7800", bg: "#FFE9D2" },
    { label: "Challenges", value: data.totalChallenges, icon: Trophy, color: "#534AB7", bg: "#EEEDFE" },
  ];

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <p className="text-xs font-black text-neutral-400 uppercase tracking-wider mb-1">
            Platform overview
          </p>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900">Admin Panel</h1>
          <p className="text-neutral-500 font-bold mt-1">Manage courses and keep an eye on growth</p>
        </div>
        <button
          onClick={() => setShowForm(!showForm)}
          className={showForm ? "duo-btn3d duo-btn3d-white" : "duo-btn3d duo-btn3d-green"}
        >
          {showForm ? (
            "Cancel"
          ) : (
            <>
              <Plus className="w-4 h-4" />
              New course
            </>
          )}
        </button>
      </div>

      <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-5 gap-4">
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

      {showForm && (
        <div className="duo-card duo-pop space-y-4">
          <h2 className="text-lg font-black text-neutral-900">Create course</h2>
          <input
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Course title"
            className="w-full px-4 py-3 rounded-2xl border-2 border-[#e5e5e5] font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
          />
          <textarea
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Description"
            rows={3}
            className="w-full px-4 py-3 rounded-2xl border-2 border-[#e5e5e5] font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
          />
          <div className="flex flex-wrap gap-4">
            <select
              value={level}
              onChange={(e) => setLevel(e.target.value)}
              className="px-4 py-3 rounded-2xl border-2 border-[#e5e5e5] font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
            >
              <option>Beginner</option>
              <option>Intermediate</option>
              <option>Advanced</option>
            </select>
            <input
              type="number"
              value={hours}
              onChange={(e) => setHours(Number(e.target.value))}
              className="w-28 px-4 py-3 rounded-2xl border-2 border-[#e5e5e5] font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
            />
            <span className="text-neutral-500 self-center text-sm font-bold">estimated hours</span>
          </div>
          <button
            onClick={() => createMutation.mutate()}
            disabled={!title || createMutation.isPending}
            className="duo-btn3d duo-btn3d-green disabled:opacity-50"
          >
            {createMutation.isPending ? "Creating..." : "Create course"}
          </button>
        </div>
      )}

      <div className="duo-card !p-0 overflow-hidden">
        <div className="px-6 py-4 border-b-2 border-[#f0f0f0] font-black text-neutral-900">
          Recent users
        </div>
        <table className="w-full text-sm">
          <thead className="text-neutral-400 border-b-2 border-[#f0f0f0]">
            <tr>
              <th className="text-left px-6 py-3 text-xs font-black uppercase tracking-wider">Name</th>
              <th className="text-left px-6 py-3 text-xs font-black uppercase tracking-wider">Email</th>
              <th className="text-right px-6 py-3 text-xs font-black uppercase tracking-wider">XP</th>
            </tr>
          </thead>
          <tbody>
            {data.recentUsers.map((u) => (
              <tr key={u.id} className="border-b border-[#f0f0f0] hover:bg-neutral-50 transition-colors">
                <td className="px-6 py-3 font-bold text-neutral-900">{u.name}</td>
                <td className="px-6 py-3 text-neutral-500 font-semibold">{u.email}</td>
                <td className="px-6 py-3 text-right">
                  <span className="duo-xp-pill">
                    <Zap className="w-3 h-3" />
                    {u.xp}
                  </span>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {message && (
        <div className="flex items-center gap-2 p-4 rounded-2xl font-bold text-sm bg-[#D7FFB8] text-[#46A302]">
          <Sparkles className="w-5 h-5 shrink-0" />
          <span className="font-black">{message}</span>
        </div>
      )}
    </div>
  );
}