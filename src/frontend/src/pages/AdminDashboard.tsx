import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { createCourse, fetchAdminDashboard } from "../lib/api";
import { useAuth } from "../context/AuthContext";

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
      <div className="max-w-6xl mx-auto px-4 py-12">
        <p className="text-slate-400">
          <Link to="/login" className="text-indigo-400 hover:underline">Sign in</Link> as admin to access this panel.
        </p>
      </div>
    );
  }

  if (!isAdmin) {
    return (
      <div className="max-w-6xl mx-auto px-4 py-12">
        <p className="text-slate-400">Admin access required. Demo: admin@academy.com / Admin123!</p>
      </div>
    );
  }

  if (isLoading) return <div className="max-w-6xl mx-auto px-4 py-12 text-slate-400">Loading admin panel...</div>;
  if (error || !data) return <div className="max-w-6xl mx-auto px-4 py-12 text-red-400">Failed to load dashboard.</div>;

  const stats = [
    { label: "Users", value: data.totalUsers, color: "from-blue-500 to-cyan-500" },
    { label: "Courses", value: data.totalCourses, color: "from-violet-500 to-purple-500" },
    { label: "Enrollments", value: data.totalEnrollments, color: "from-emerald-500 to-teal-500" },
    { label: "Assignments", value: data.totalAssignments, color: "from-amber-500 to-orange-500" },
    { label: "Challenges", value: data.totalChallenges, color: "from-pink-500 to-rose-500" },
  ];

  return (
    <div className="max-w-6xl mx-auto px-4 py-10">
      <div className="flex flex-wrap items-center justify-between gap-4 mb-8">
        <div>
          <h1 className="text-3xl font-bold text-white">Admin Panel</h1>
          <p className="text-slate-400 mt-1">Platform overview and course management</p>
        </div>
        <button
          onClick={() => setShowForm(!showForm)}
          className="px-4 py-2 rounded-lg bg-indigo-600 hover:bg-indigo-500 text-sm font-medium"
        >
          {showForm ? "Cancel" : "+ New Course"}
        </button>
      </div>

      <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-5 gap-4 mb-10">
        {stats.map((s) => (
          <div key={s.label} className="rounded-xl bg-slate-900 border border-slate-800 p-5">
            <p className={`text-3xl font-bold bg-gradient-to-r ${s.color} bg-clip-text text-transparent`}>
              {s.value}
            </p>
            <p className="text-sm text-slate-400 mt-1">{s.label}</p>
          </div>
        ))}
      </div>

      {showForm && (
        <div className="rounded-xl bg-slate-900 border border-slate-800 p-6 mb-8 space-y-4">
          <h2 className="text-lg font-semibold">Create Course</h2>
          <input
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            placeholder="Course title"
            className="w-full px-4 py-2 rounded-lg bg-slate-800 border border-slate-700 text-white"
          />
          <textarea
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Description"
            rows={3}
            className="w-full px-4 py-2 rounded-lg bg-slate-800 border border-slate-700 text-white"
          />
          <div className="flex gap-4">
            <select
              value={level}
              onChange={(e) => setLevel(e.target.value)}
              className="px-4 py-2 rounded-lg bg-slate-800 border border-slate-700 text-white"
            >
              <option>Beginner</option>
              <option>Intermediate</option>
              <option>Advanced</option>
            </select>
            <input
              type="number"
              value={hours}
              onChange={(e) => setHours(Number(e.target.value))}
              className="w-24 px-4 py-2 rounded-lg bg-slate-800 border border-slate-700 text-white"
            />
            <span className="text-slate-400 self-center text-sm">estimated hours</span>
          </div>
          <button
            onClick={() => createMutation.mutate()}
            disabled={!title || createMutation.isPending}
            className="px-6 py-2 rounded-lg bg-indigo-600 hover:bg-indigo-500 disabled:opacity-50 font-medium"
          >
            {createMutation.isPending ? "Creating..." : "Create Course"}
          </button>
        </div>
      )}

      <div className="rounded-xl bg-slate-900 border border-slate-800 overflow-hidden">
        <div className="px-6 py-4 border-b border-slate-800">
          <h2 className="font-semibold">Recent Users</h2>
        </div>
        <table className="w-full text-sm">
          <thead className="text-slate-400 border-b border-slate-800">
            <tr>
              <th className="text-left px-6 py-3">Name</th>
              <th className="text-left px-6 py-3">Email</th>
              <th className="text-right px-6 py-3">XP</th>
            </tr>
          </thead>
          <tbody>
            {data.recentUsers.map((u) => (
              <tr key={u.id} className="border-b border-slate-800/50 hover:bg-slate-800/30">
                <td className="px-6 py-3">{u.name}</td>
                <td className="px-6 py-3 text-slate-400">{u.email}</td>
                <td className="px-6 py-3 text-right text-indigo-400">{u.xp}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {message && <p className="mt-4 text-sm text-emerald-400">{message}</p>}
    </div>
  );
}
