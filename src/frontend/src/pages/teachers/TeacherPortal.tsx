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
  const [gradingId, setGradingId] = useState<number | null>(null);
  const [grade, setGrade] = useState(100);
  const [feedback, setFeedback] = useState("");
  const [selectedAssignment, setSelectedAssignment] = useState<number | null>(null);
  const [message, setMessage] = useState("");

  const [form, setForm] = useState({
    title: "",
    description: "",
    instructions: "",
    courseId: 1,
    maxPoints: 100,
    requiresCode: true,
    dueDate: "",
  });

  const { data: dashboard } = useQuery({
    queryKey: ["teacher-dashboard"],
    queryFn: () => fetchTeacherDashboard(token!),
    enabled: !!token && isTeacher,
  });

  const { data: assignments } = useQuery({
    queryKey: ["teaching-assignments"],
    queryFn: () => fetchTeachingAssignments(token!),
    enabled: !!token && isTeacher,
  });

  const { data: submissions } = useQuery({
    queryKey: ["assignment-submissions", selectedAssignment],
    queryFn: () => fetchAssignmentSubmissions(selectedAssignment!, token!),
    enabled: !!token && selectedAssignment !== null,
  });

  const createMutation = useMutation({
    mutationFn: () =>
      createAssignment(token!, {
        ...form,
        dueDate: form.dueDate || undefined,
      }),
    onSuccess: () => {
      setMessage("Assignment created.");
      setShowForm(false);
      queryClient.invalidateQueries({ queryKey: ["teaching-assignments"] });
      queryClient.invalidateQueries({ queryKey: ["teacher-dashboard"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  const gradeMutation = useMutation({
    mutationFn: () => gradeSubmission(gradingId!, grade, feedback, token!),
    onSuccess: () => {
      setMessage("Submission graded.");
      setGradingId(null);
      queryClient.invalidateQueries({ queryKey: ["assignment-submissions", selectedAssignment] });
      queryClient.invalidateQueries({ queryKey: ["teacher-dashboard"] });
    },
    onError: (err: Error) => setMessage(err.message),
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
        <button
          onClick={() => setShowForm(!showForm)}
          className={showForm ? "duo-btn3d duo-btn3d-white" : "duo-btn3d duo-btn3d-green"}
        >
          {showForm ? (
            "Cancel"
          ) : (
            <>
              <Plus className="w-4 h-4" />
              New assignment
            </>
          )}
        </button>
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

      {showForm && (
        <div className="duo-card duo-pop space-y-4">
          <h2 className="font-black text-lg text-neutral-900">Create assignment</h2>
          {(["title", "description", "instructions"] as const).map((field) => (
            <div key={field}>
              <label className="block text-xs font-black text-neutral-500 uppercase tracking-wider mb-1">
                {field.charAt(0).toUpperCase() + field.slice(1)}
              </label>
              <input
                value={form[field]}
                onChange={(e) => setForm({ ...form, [field]: e.target.value })}
                placeholder={field.charAt(0).toUpperCase() + field.slice(1)}
                className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
              />
            </div>
          ))}
          <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
            <div>
              <label className="block text-xs font-black text-neutral-500 uppercase tracking-wider mb-1">
                Course ID
              </label>
              <input
                type="number"
                value={form.courseId}
                onChange={(e) => setForm({ ...form, courseId: Number(e.target.value) })}
                className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
                placeholder="Course ID"
              />
            </div>
            <div>
              <label className="block text-xs font-black text-neutral-500 uppercase tracking-wider mb-1">
                Due Date
              </label>
              <input
                type="date"
                value={form.dueDate}
                onChange={(e) => setForm({ ...form, dueDate: e.target.value })}
                className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
              />
            </div>
            <div>
              <label className="block text-xs font-black text-neutral-500 uppercase tracking-wider mb-1">
                Max Points
              </label>
              <input
                type="number"
                value={form.maxPoints}
                onChange={(e) => setForm({ ...form, maxPoints: Number(e.target.value) })}
                className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
              />
            </div>
          </div>
          <div className="flex items-center gap-2">
            <input
              type="checkbox"
              id="requiresCode"
              checked={form.requiresCode}
              onChange={(e) => setForm({ ...form, requiresCode: e.target.checked })}
              className="w-5 h-5 accent-[#58CC02]"
            />
            <label htmlFor="requiresCode" className="text-sm font-bold text-neutral-700">
              Requires code
            </label>
          </div>
          <button
            onClick={() => createMutation.mutate()}
            disabled={!form.title || createMutation.isPending}
            className="duo-btn3d duo-btn3d-green disabled:opacity-50"
          >
            {createMutation.isPending ? "Creating..." : "Create"}
          </button>
        </div>
      )}

      <div className="grid lg:grid-cols-2 gap-6">
        <div className="duo-card !p-0 overflow-hidden">
          <div className="px-5 py-4 border-b-2 border-[#f0f0f0] font-black text-neutral-900">
            Your assignments
          </div>
          <ul className="divide-y-2 divide-[#f0f0f0]">
            {(assignments ?? []).map((a) => (
              <li key={a.id}>
                <button
                  onClick={() => setSelectedAssignment(a.id)}
                  className={`w-full text-left px-5 py-4 hover:bg-neutral-50 transition-colors ${
                    selectedAssignment === a.id ? "bg-[#DDF4FF]" : ""
                  }`}
                >
                  <p className="font-black text-neutral-900">{a.title}</p>
                  <p className="text-xs font-bold text-neutral-400 mt-1">
                    {a.submissionCount} submissions · {a.maxPoints} pts
                    {a.dueDate && ` · Due ${new Date(a.dueDate).toLocaleDateString()}`}
                  </p>
                </button>
              </li>
            ))}
            {!assignments?.length && (
              <li className="px-5 py-8 text-neutral-500 text-sm text-center font-semibold">
                No assignments yet.
              </li>
            )}
          </ul>
        </div>

        <div className="duo-card !p-0 overflow-hidden">
          <div className="px-5 py-4 border-b-2 border-[#f0f0f0] font-black text-neutral-900">
            Submissions
          </div>
          {!selectedAssignment ? (
            <p className="px-5 py-8 text-neutral-500 text-sm text-center font-semibold">
              Select an assignment to view submissions.
            </p>
          ) : (
            <ul className="divide-y-2 divide-[#f0f0f0] max-h-96 overflow-y-auto">
              {(submissions ?? []).map((s) => (
                <li key={s.id} className="px-5 py-4">
                  <div className="flex justify-between items-start gap-2">
                    <div className="flex-1">
                      <p className="font-black text-neutral-900">
                        {s.studentName || `User #${s.userId}`}
                      </p>
                      <p className="text-xs font-bold text-neutral-400">
                        {new Date(s.submittedAt).toLocaleString()}
                      </p>
                      <pre className="mt-3 text-xs font-mono bg-neutral-50 border-2 border-[#e5e5e5] p-3 rounded-2xl overflow-x-auto max-h-24">
                        {s.content}
                      </pre>
                      {s.grade !== null && (
                        <p className="mt-3">
                          <span className="duo-badge duo-badge-green">
                            Grade: {s.grade} — {s.feedback}
                          </span>
                        </p>
                      )}

                      {s.attachments?.length > 0 && (
                        <div className="mt-3 flex flex-wrap gap-2">
                          {s.attachments.map((file) => (
                            <a
                              key={file.id}
                              href={getFileUrl(file.fileUrl)}
                              target="_blank"
                              rel="noopener noreferrer"
                              className="duo-badge duo-badge-gray hover:border-[#58CC02] transition-colors truncate max-w-[150px]"
                              title={file.fileName}
                            >
                              <Paperclip className="w-3 h-3" />
                              {file.fileName}
                            </a>
                          ))}
                        </div>
                      )}
                    </div>
                    {s.status === "Submitted" && (
                      <button
                        onClick={() => setGradingId(s.id)}
                        className="duo-btn3d duo-btn3d-yellow !px-3 !py-2 !text-xs shrink-0"
                      >
                        Grade
                      </button>
                    )}
                  </div>
                </li>
              ))}
              {!submissions?.length && (
                <li className="px-5 py-8 text-neutral-500 text-sm text-center font-semibold">
                  No submissions yet.
                </li>
              )}
            </ul>
          )}
        </div>
      </div>

      {gradingId && (
        <div className="fixed inset-0 bg-black/60 flex items-center justify-center p-4 z-50">
          <div className="duo-card duo-pop w-full max-w-md space-y-4">
            <h3 className="font-black text-lg text-neutral-900">Grade submission</h3>
            <div>
              <label className="block text-xs font-black text-neutral-500 uppercase tracking-wider mb-1">
                Grade
              </label>
              <input
                type="number"
                value={grade}
                onChange={(e) => setGrade(Number(e.target.value))}
                className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
                placeholder="Grade"
              />
            </div>
            <div>
              <label className="block text-xs font-black text-neutral-500 uppercase tracking-wider mb-1">
                Feedback
              </label>
              <textarea
                value={feedback}
                onChange={(e) => setFeedback(e.target.value)}
                rows={3}
                className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
                placeholder="Feedback"
              />
            </div>
            <div className="flex gap-3">
              <button
                onClick={() => gradeMutation.mutate()}
                disabled={gradeMutation.isPending}
                className="flex-1 duo-btn3d duo-btn3d-green disabled:opacity-50"
              >
                <Send className="w-4 h-4" />
                Submit grade
              </button>
              <button
                onClick={() => setGradingId(null)}
                className="duo-btn3d duo-btn3d-white"
              >
                Cancel
              </button>
            </div>
          </div>
        </div>
      )}

      {message && (
        <div className="flex items-center gap-2 p-4 rounded-2xl font-bold text-sm bg-[#D7FFB8] text-[#46A302]">
          <Sparkles className="w-5 h-5 shrink-0" />
          <span className="font-black">{message}</span>
        </div>
      )}
    </div>
  );
}