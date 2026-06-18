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
import { GraduationCap, FileText, CheckCircle2, Send } from "lucide-react";

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
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
            Teacher Portal
          </h1>
          <p className="text-neutral-600 font-semibold">
            <Link to="/login" className="text-[#58CC02] font-black hover:underline">Sign in</Link> as a teacher.
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
            Teacher Portal
          </h1>
          <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
            <p className="text-[#FF4B4B] font-black">
              Teacher access required. Demo: teacher@academy.com / Teacher123!
            </p>
          </div>
        </div>
      </div>
    );
  }

  const stats = [
    { label: "Classrooms", value: dashboard?.classroomCount ?? 0, icon: GraduationCap, color: "text-[#1CB0F6]" },
    { label: "Assignments", value: dashboard?.assignmentCount ?? 0, icon: FileText, color: "text-[#58CC02]" },
    { label: "Pending Grading", value: dashboard?.pendingGrading ?? 0, icon: CheckCircle2, color: "text-[#FFC800]" },
  ];

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
            Teacher Portal
          </h1>
          <p className="text-neutral-600 font-semibold">
            Manage assignments and grade student work
          </p>
        </div>
        <button
          onClick={() => setShowForm(!showForm)}
          className="duo-btn duo-btn-primary"
        >
          {showForm ? "Cancel" : "+ New Assignment"}
        </button>
      </div>

      {dashboard && (
        <div className="grid grid-cols-3 gap-4">
          {stats.map((s) => (
            <div key={s.label} className="duo-card p-5 text-center">
              <div className="w-12 h-12 bg-neutral-100 rounded-xl flex items-center justify-center mx-auto mb-3">
                <s.icon className={`w-6 h-6 ${s.color}`} />
              </div>
              <p className="text-3xl font-black text-neutral-900 mb-1">{s.value}</p>
              <p className="text-xs font-bold text-neutral-500 uppercase tracking-wider">{s.label}</p>
            </div>
          ))}
        </div>
      )}

      {showForm && (
        <div className="duo-card space-y-4">
          <h2 className="font-black text-lg text-neutral-900">Create Assignment</h2>
          {(["title", "description", "instructions"] as const).map((field) => (
            <div key={field}>
              <label className="block text-sm font-black text-neutral-700 mb-1">
                {field.charAt(0).toUpperCase() + field.slice(1)}
              </label>
              <input
                value={form[field]}
                onChange={(e) => setForm({ ...form, [field]: e.target.value })}
                placeholder={field.charAt(0).toUpperCase() + field.slice(1)}
                className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
              />
            </div>
          ))}
          <div className="grid grid-cols-1 md:grid-cols-3 gap-3">
            <div>
              <label className="block text-sm font-black text-neutral-700 mb-1">
                Course ID
              </label>
              <input
                type="number"
                value={form.courseId}
                onChange={(e) => setForm({ ...form, courseId: Number(e.target.value) })}
                className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
                placeholder="Course ID"
              />
            </div>
            <div>
              <label className="block text-sm font-black text-neutral-700 mb-1">
                Due Date
              </label>
              <input
                type="date"
                value={form.dueDate}
                onChange={(e) => setForm({ ...form, dueDate: e.target.value })}
                className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
              />
            </div>
            <div>
              <label className="block text-sm font-black text-neutral-700 mb-1">
                Max Points
              </label>
              <input
                type="number"
                value={form.maxPoints}
                onChange={(e) => setForm({ ...form, maxPoints: Number(e.target.value) })}
                className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
              />
            </div>
          </div>
          <div className="flex items-center gap-2">
            <input
              type="checkbox"
              id="requiresCode"
              checked={form.requiresCode}
              onChange={(e) => setForm({ ...form, requiresCode: e.target.checked })}
              className="w-5 h-5 text-[#58CC02]"
            />
            <label htmlFor="requiresCode" className="text-sm font-bold text-neutral-700">
              Requires code
            </label>
          </div>
          <button
            onClick={() => createMutation.mutate()}
            disabled={!form.title || createMutation.isPending}
            className="duo-btn duo-btn-primary"
          >
            Create
          </button>
        </div>
      )}

      <div className="grid lg:grid-cols-2 gap-6">
        <div className="duo-card overflow-hidden">
          <div className="px-5 py-4 border-b border-[#e5e5e5] font-black text-neutral-900">
            Your Assignments
          </div>
          <ul className="divide-y divide-[#e5e5e5]">
            {(assignments ?? []).map((a) => (
              <li key={a.id}>
                <button
                  onClick={() => setSelectedAssignment(a.id)}
                  className={`w-full text-left px-5 py-4 hover:bg-neutral-50 transition-colors ${selectedAssignment === a.id ? "bg-[#E6F7FF]" : ""
                    }`}
                >
                  <p className="font-black text-neutral-900">{a.title}</p>
                  <p className="text-xs font-bold text-neutral-500">
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

        <div className="duo-card overflow-hidden">
          <div className="px-5 py-4 border-b border-[#e5e5e5] font-black text-neutral-900">
            Submissions
          </div>
          {!selectedAssignment ? (
            <p className="px-5 py-8 text-neutral-500 text-sm text-center font-semibold">
              Select an assignment to view submissions.
            </p>
          ) : (
            <ul className="divide-y divide-[#e5e5e5] max-h-96 overflow-y-auto">
              {(submissions ?? []).map((s) => (
                <li key={s.id} className="px-5 py-4">
                  <div className="flex justify-between items-start gap-2">
                    <div className="flex-1">
                      <p className="font-black text-neutral-900">
                        {s.studentName || `User #${s.userId}`}
                      </p>
                      <p className="text-xs font-bold text-neutral-500">
                        {new Date(s.submittedAt).toLocaleString()}
                      </p>
                      <pre className="mt-3 text-xs font-mono bg-neutral-100 p-3 rounded-xl overflow-x-auto max-h-24">
                        {s.content}
                      </pre>
                      {s.grade !== null && (
                        <p className="mt-3 text-sm font-black text-[#58CC02]">
                          Grade: {s.grade} — {s.feedback}
                        </p>
                      )}

                      {s.attachments?.length > 0 && (
                        <div className="mt-3 flex flex-wrap gap-2">
                          {s.attachments.map(file => (
                            <a
                              key={file.id}
                              href={getFileUrl(file.fileUrl)}
                              target="_blank"
                              rel="noopener noreferrer"
                              className="text-xs font-black px-3 py-1.5 rounded-xl bg-neutral-100 border border-[#e5e5e5] hover:border-[#58CC02] text-neutral-800 transition-colors truncate max-w-[150px]"
                              title={file.fileName}
                            >
                              📎 {file.fileName}
                            </a>
                          ))}
                        </div>
                      )}
                    </div>
                    {s.status === "Submitted" && (
                      <button
                        onClick={() => setGradingId(s.id)}
                        className="shrink-0 px-3 py-1.5 text-xs font-black rounded-xl bg-[#FFC800] text-neutral-900"
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
          <div className="duo-card w-full max-w-md space-y-4">
            <h3 className="font-black text-lg text-neutral-900">Grade Submission</h3>
            <div>
              <label className="block text-sm font-black text-neutral-700 mb-1">
                Grade
              </label>
              <input
                type="number"
                value={grade}
                onChange={(e) => setGrade(Number(e.target.value))}
                className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
                placeholder="Grade"
              />
            </div>
            <div>
              <label className="block text-sm font-black text-neutral-700 mb-1">
                Feedback
              </label>
              <textarea
                value={feedback}
                onChange={(e) => setFeedback(e.target.value)}
                rows={3}
                className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
                placeholder="Feedback"
              />
            </div>
            <div className="flex gap-3">
              <button
                onClick={() => gradeMutation.mutate()}
                disabled={gradeMutation.isPending}
                className="flex-1 duo-btn duo-btn-primary"
              >
                <Send className="w-4 h-4 mr-2" />
                Submit Grade
              </button>
              <button
                onClick={() => setGradingId(null)}
                className="px-4 py-3 rounded-xl border border-[#e5e5e5] font-black text-neutral-700"
              >
                Cancel
              </button>
            </div>
          </div>
        </div>
      )}

      {message && (
        <div className="duo-card bg-[#58CC02]/10 border-[#58CC02]/30">
          <p className="text-[#58CC02] font-black">{message}</p>
        </div>
      )}
    </div>
  );
}
