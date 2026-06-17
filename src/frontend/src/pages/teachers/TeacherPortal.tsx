import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import {
  createAssignment,
  fetchAssignmentSubmissions,
  fetchTeacherDashboard,
  fetchTeachingAssignments,
  gradeSubmission,
} from "../lib/api";
import { useAuth } from "../context/AuthContext";

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
      <div className="max-w-6xl mx-auto px-4 py-12 text-slate-400">
        <Link to="/login" className="text-indigo-400 hover:underline">Sign in</Link> as a teacher.
      </div>
    );
  }

  if (!isTeacher) {
    return (
      <div className="max-w-6xl mx-auto px-4 py-12 text-slate-400">
        Teacher access required. Demo: teacher@academy.com / Teacher123!
      </div>
    );
  }

  return (
    <div className="max-w-6xl mx-auto px-4 py-10">
      <div className="flex flex-wrap items-center justify-between gap-4 mb-8">
        <div>
          <h1 className="text-3xl font-bold">Teacher Portal</h1>
          <p className="text-slate-400 mt-1">Manage assignments and grade student work</p>
        </div>
        <button
          onClick={() => setShowForm(!showForm)}
          className="px-4 py-2 rounded-lg bg-indigo-600 hover:bg-indigo-500 text-sm font-medium"
        >
          {showForm ? "Cancel" : "+ New Assignment"}
        </button>
      </div>

      {dashboard && (
        <div className="grid grid-cols-3 gap-4 mb-8">
          {[
            { label: "Classrooms", value: dashboard.classroomCount },
            { label: "Assignments", value: dashboard.assignmentCount },
            { label: "Pending Grading", value: dashboard.pendingGrading },
          ].map((s) => (
            <div key={s.label} className="rounded-xl bg-slate-900 border border-slate-800 p-5 text-center">
              <p className="text-3xl font-bold text-indigo-400">{s.value}</p>
              <p className="text-sm text-slate-400">{s.label}</p>
            </div>
          ))}
        </div>
      )}

      {showForm && (
        <div className="rounded-xl bg-slate-900 border border-slate-800 p-6 mb-8 space-y-3">
          <h2 className="font-semibold">Create Assignment</h2>
          {(["title", "description", "instructions"] as const).map((field) => (
            <input
              key={field}
              value={form[field]}
              onChange={(e) => setForm({ ...form, [field]: e.target.value })}
              placeholder={field.charAt(0).toUpperCase() + field.slice(1)}
              className="w-full px-4 py-2 rounded-lg bg-slate-800 border border-slate-700"
            />
          ))}
          <div className="flex flex-wrap gap-3">
            <input
              type="number"
              value={form.courseId}
              onChange={(e) => setForm({ ...form, courseId: Number(e.target.value) })}
              className="w-24 px-4 py-2 rounded-lg bg-slate-800 border border-slate-700"
              placeholder="Course ID"
            />
            <input
              type="date"
              value={form.dueDate}
              onChange={(e) => setForm({ ...form, dueDate: e.target.value })}
              className="px-4 py-2 rounded-lg bg-slate-800 border border-slate-700"
            />
            <label className="flex items-center gap-2 text-sm text-slate-300">
              <input
                type="checkbox"
                checked={form.requiresCode}
                onChange={(e) => setForm({ ...form, requiresCode: e.target.checked })}
              />
              Requires code
            </label>
          </div>
          <button
            onClick={() => createMutation.mutate()}
            disabled={!form.title || createMutation.isPending}
            className="px-6 py-2 rounded-lg bg-indigo-600 hover:bg-indigo-500 disabled:opacity-50"
          >
            Create
          </button>
        </div>
      )}

      <div className="grid lg:grid-cols-2 gap-6">
        <div className="rounded-xl bg-slate-900 border border-slate-800">
          <div className="px-5 py-4 border-b border-slate-800 font-semibold">Your Assignments</div>
          <ul className="divide-y divide-slate-800">
            {(assignments ?? []).map((a) => (
              <li key={a.id}>
                <button
                  onClick={() => setSelectedAssignment(a.id)}
                  className={`w-full text-left px-5 py-4 hover:bg-slate-800/50 ${
                    selectedAssignment === a.id ? "bg-indigo-900/30" : ""
                  }`}
                >
                  <p className="font-medium">{a.title}</p>
                  <p className="text-sm text-slate-400">
                    {a.submissionCount} submissions · {a.maxPoints} pts
                    {a.dueDate && ` · Due ${new Date(a.dueDate).toLocaleDateString()}`}
                  </p>
                </button>
              </li>
            ))}
            {!assignments?.length && (
              <li className="px-5 py-8 text-slate-500 text-sm text-center">No assignments yet.</li>
            )}
          </ul>
        </div>

        <div className="rounded-xl bg-slate-900 border border-slate-800">
          <div className="px-5 py-4 border-b border-slate-800 font-semibold">Submissions</div>
          {!selectedAssignment ? (
            <p className="px-5 py-8 text-slate-500 text-sm text-center">Select an assignment to view submissions.</p>
          ) : (
            <ul className="divide-y divide-slate-800 max-h-96 overflow-y-auto">
              {(submissions ?? []).map((s) => (
                <li key={s.id} className="px-5 py-4">
                  <div className="flex justify-between items-start gap-2">
                    <div>
                      <p className="font-medium">{s.studentName || `User #${s.userId}`}</p>
                      <p className="text-xs text-slate-500">{new Date(s.submittedAt).toLocaleString()}</p>
                      <pre className="mt-2 text-xs bg-slate-800 p-2 rounded overflow-x-auto max-h-24">{s.content}</pre>
                      {s.grade !== null && (
                        <p className="mt-2 text-sm text-emerald-400">Grade: {s.grade} — {s.feedback}</p>
                      )}
                    </div>
                    {s.status === "Submitted" && (
                      <button
                        onClick={() => setGradingId(s.id)}
                        className="shrink-0 px-3 py-1 text-xs rounded bg-amber-600 hover:bg-amber-500"
                      >
                        Grade
                      </button>
                    )}
                  </div>
                </li>
              ))}
              {!submissions?.length && (
                <li className="px-5 py-8 text-slate-500 text-sm text-center">No submissions yet.</li>
              )}
            </ul>
          )}
        </div>
      </div>

      {gradingId && (
        <div className="fixed inset-0 bg-black/60 flex items-center justify-center p-4 z-50">
          <div className="bg-slate-900 border border-slate-700 rounded-xl p-6 w-full max-w-md space-y-4">
            <h3 className="font-semibold">Grade Submission</h3>
            <input
              type="number"
              value={grade}
              onChange={(e) => setGrade(Number(e.target.value))}
              className="w-full px-4 py-2 rounded-lg bg-slate-800 border border-slate-700"
              placeholder="Grade"
            />
            <textarea
              value={feedback}
              onChange={(e) => setFeedback(e.target.value)}
              rows={3}
              className="w-full px-4 py-2 rounded-lg bg-slate-800 border border-slate-700"
              placeholder="Feedback"
            />
            <div className="flex gap-3">
              <button
                onClick={() => gradeMutation.mutate()}
                disabled={gradeMutation.isPending}
                className="flex-1 py-2 rounded-lg bg-indigo-600 hover:bg-indigo-500"
              >
                Submit Grade
              </button>
              <button onClick={() => setGradingId(null)} className="px-4 py-2 rounded-lg border border-slate-700">
                Cancel
              </button>
            </div>
          </div>
        </div>
      )}

      {message && <p className="mt-4 text-sm text-emerald-400">{message}</p>}
    </div>
  );
}
