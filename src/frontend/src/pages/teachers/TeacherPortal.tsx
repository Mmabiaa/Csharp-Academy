import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import {
  createAssignment,
  fetchAssignmentSubmissions,
  fetchTeacherDashboard,
  fetchTeachingAssignments,
  gradeSubmission,
  getFileUrl,
} from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { GraduationCap, FileText, CheckCircle2, Send, Lock, ShieldAlert, Plus, Paperclip, Sparkles } from "lucide-react";

export default function TeacherPortal() {
  const { token, isTeacher } = useAuth();
  const queryClient = useQueryClient();
  const [showForm, setShowForm] = useState(false);
  const { data: dashboard } = useQuery({
    queryKey: ["teacher-dashboard"],
    queryFn: () => fetchTeacherDashboard(token!),
    enabled: !!token && isTeacher,
  });

  if (!token) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-6">Teacher Portal</h1>
          <div className="duo-panel text-center py-12">
            <div className="w-16 h-16 rounded-full bg-[#DDF4FF] flex items-center justify-center mx-auto mb-4">
              <Lock className="w-8 h-8 text-[#1CB0F6]" />
            </div>
            <p className="text-neutral-600 font-bold">
              <Link to="/login" className="text-[#58CC02] font-black hover:underline">
                Sign in
              </Link>{" "}
              as a teacher.
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
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-6">Teacher Portal</h1>
          <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-12">
            <div className="w-16 h-16 rounded-full bg-[#FFDFE0] flex items-center justify-center mx-auto mb-4">
              <ShieldAlert className="w-8 h-8 text-[#FF4B4B]" />
            </div>
            <p className="text-[#CC3A3A] font-black text-lg">Teacher access required</p>
            <p className="text-neutral-500 font-bold text-sm mt-1">
              Demo: teacher@academy.com / Teacher123!
            </p>
          </div>
        </div>
      </div>
    );
  }

  const stats = [
    { label: "Classrooms", value: dashboard?.classroomCount ?? 0, icon: GraduationCap, color: "#1CB0F6", bg: "#DDF4FF" },
    { label: "Assignments", value: dashboard?.assignmentCount ?? 0, icon: FileText, color: "#46A302", bg: "#D7FFB8" },
    { label: "Pending Grading", value: dashboard?.pendingGrading ?? 0, icon: CheckCircle2, color: "#946800", bg: "#FFF1C2" },
  ];

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">Teacher Portal</h1>
          <p className="text-neutral-500 font-bold">Manage assignments and grade student work</p>
        </div>
        <Link
          to="/assignments"
          className="duo-btn3d duo-btn3d-green"
        >
          <Plus className="w-4 h-4" />
          Manage assignments
        </Link>
      </div>

      {dashboard && (
        <div className="grid grid-cols-3 gap-4">
          {stats.map((s) => (
            <div key={s.label} className="duo-card duo-card-hover p-5 text-center">
              <div
                className="w-12 h-12 rounded-xl flex items-center justify-center mx-auto mb-3"
                style={{ background: s.bg }}
              >
                <s.icon className="w-6 h-6" style={{ color: s.color }} />
              </div>
              <p className="text-3xl font-black text-neutral-900 mb-1">{s.value}</p>
              <p className="text-xs font-black text-neutral-500 uppercase tracking-wider">{s.label}</p>
            </div>
          ))}
        </div>
      )}

      <div className="grid lg:grid-cols-2 gap-6">
        <div className="duo-card !p-0 overflow-hidden">
          <div className="px-5 py-4 border-b-2 border-[#f0f0f0] flex justify-between items-center">
            <span className="font-black text-neutral-900">Pending Grading</span>
            <Link to="/assignments" className="text-xs font-black text-[#1CB0F6] hover:underline uppercase tracking-wider">View all</Link>
          </div>
          <ul className="divide-y-2 divide-[#f0f0f0]">
            {dashboard?.recentAssignments?.filter(a => a.ungradedCount > 0).map((a) => (
              <li key={a.id} className="px-5 py-4 flex justify-between items-center">
                <div>
                  <p className="font-black text-neutral-900">{a.title}</p>
                  <p className="text-xs font-bold text-neutral-400 mt-1">
                    {a.ungradedCount} submissions waiting
                  </p>
                </div>
                <Link to="/assignments" className="duo-btn3d duo-btn3d-yellow !px-3 !py-1.5 !text-xs">
                  Grade
                </Link>
              </li>
            ))}
            {(!dashboard?.recentAssignments || dashboard.recentAssignments.length === 0) && (
              <li className="px-5 py-8 text-neutral-500 text-sm text-center font-semibold">
                All caught up!
              </li>
            )}
          </ul>
        </div>

        <div className="duo-card !p-0 overflow-hidden">
          <div className="px-5 py-4 border-b-2 border-[#f0f0f0] flex justify-between items-center">
            <span className="font-black text-neutral-900">Your Classrooms</span>
            <Link to="/classrooms" className="text-xs font-black text-[#1CB0F6] hover:underline uppercase tracking-wider">Manage</Link>
          </div>
          <div className="p-8 text-center text-neutral-500 font-bold text-sm">
            <GraduationCap className="w-12 h-12 mx-auto mb-4 text-neutral-200" />
            Use the Classrooms tab to manage students.
          </div>
        </div>
      </div>
    </div>
  );
}