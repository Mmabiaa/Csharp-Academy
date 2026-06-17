import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchMyAssignments, submitAssignment } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import CodeEditor from "../../components/CodeEditor";

export default function Assignments() {
  const { token, isAuthenticated } = useAuth();
  const queryClient = useQueryClient();
  const [activeId, setActiveId] = useState<number | null>(null);
  const [content, setContent] = useState("");
  const [message, setMessage] = useState("");

  const { data: assignments, isLoading } = useQuery({
    queryKey: ["my-assignments"],
    queryFn: () => fetchMyAssignments(token!),
    enabled: !!token,
  });

  const active = assignments?.find((a) => a.id === activeId) ?? assignments?.[0];

  const submitMutation = useMutation({
    mutationFn: () => submitAssignment(active!.id, content, token!),
    onSuccess: () => {
      setMessage("Assignment submitted successfully!");
      queryClient.invalidateQueries({ queryKey: ["my-assignments"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  if (!isAuthenticated) {
    return (
      <div className="max-w-4xl mx-auto px-4 py-12 text-slate-400">
        <Link to="/login" className="text-indigo-400 hover:underline">Sign in</Link> to view assignments.
      </div>
    );
  }

  if (isLoading) return <div className="max-w-4xl mx-auto px-4 py-12 text-slate-400">Loading assignments...</div>;

  return (
    <div className="max-w-5xl mx-auto px-4 py-10">
      <h1 className="text-3xl font-bold mb-2">Assignments</h1>
      <p className="text-slate-400 mb-8">Submit your work and track grades from your teachers.</p>

      <div className="grid lg:grid-cols-3 gap-6">
        <div className="space-y-2">
          {(assignments ?? []).map((a) => {
            const submitted = a.mySubmission;
            const isLate = a.dueDate && new Date(a.dueDate) < new Date() && !submitted;
            return (
              <button
                key={a.id}
                onClick={() => {
                  setActiveId(a.id);
                  setContent(submitted?.content ?? "");
                  setMessage("");
                }}
                className={`w-full text-left p-4 rounded-xl border transition-colors ${
                  active?.id === a.id
                    ? "border-indigo-500 bg-indigo-900/20"
                    : "border-slate-800 bg-slate-900 hover:border-slate-700"
                }`}
              >
                <p className="font-medium">{a.title}</p>
                <p className="text-xs text-slate-400 mt-1">{a.courseTitle}</p>
                <div className="flex gap-2 mt-2">
                  {submitted ? (
                    <span className="text-xs px-2 py-0.5 rounded bg-emerald-900/50 text-emerald-400">
                      {submitted.status === "Graded" ? `Graded: ${submitted.grade}` : "Submitted"}
                    </span>
                  ) : isLate ? (
                    <span className="text-xs px-2 py-0.5 rounded bg-red-900/50 text-red-400">Overdue</span>
                  ) : (
                    <span className="text-xs px-2 py-0.5 rounded bg-amber-900/50 text-amber-400">Pending</span>
                  )}
                </div>
              </button>
            );
          })}
          {!assignments?.length && (
            <p className="text-slate-500 text-sm p-4">No assignments available. Join a classroom or enroll in a course.</p>
          )}
        </div>

        {active && (
          <div className="lg:col-span-2 rounded-xl bg-slate-900 border border-slate-800 p-6 space-y-4">
            <div>
              <h2 className="text-xl font-semibold">{active.title}</h2>
              <p className="text-slate-400 mt-1">{active.description}</p>
              {active.dueDate && (
                <p className="text-sm text-slate-500 mt-2">
                  Due: {new Date(active.dueDate).toLocaleDateString()} · {active.maxPoints} points
                </p>
              )}
            </div>

            <div className="rounded-lg bg-slate-800/50 p-4">
              <h3 className="text-sm font-medium text-slate-300 mb-2">Instructions</h3>
              <p className="text-sm text-slate-400 whitespace-pre-wrap">{active.instructions}</p>
            </div>

            {active.mySubmission?.status === "Graded" ? (
              <div className="rounded-lg border border-emerald-800 bg-emerald-900/20 p-4">
                <p className="text-emerald-400 font-medium">Grade: {active.mySubmission.grade} / {active.maxPoints}</p>
                {active.mySubmission.feedback && (
                  <p className="text-sm text-slate-300 mt-2">{active.mySubmission.feedback}</p>
                )}
                <pre className="mt-3 text-xs bg-slate-800 p-3 rounded overflow-x-auto">{active.mySubmission.content}</pre>
              </div>
            ) : active.mySubmission ? (
              <div className="rounded-lg border border-slate-700 p-4">
                <p className="text-amber-400 text-sm">Submitted — awaiting grade</p>
                <pre className="mt-2 text-xs bg-slate-800 p-3 rounded overflow-x-auto">{active.mySubmission.content}</pre>
              </div>
            ) : (
              <>
                {active.requiresCode ? (
                  <CodeEditor value={content} onChange={setContent} rows={14} />
                ) : (
                  <textarea
                    value={content}
                    onChange={(e) => setContent(e.target.value)}
                    rows={10}
                    placeholder="Write your submission..."
                    className="w-full px-4 py-3 rounded-lg bg-slate-800 border border-slate-700 font-mono text-sm"
                  />
                )}
                <button
                  onClick={() => submitMutation.mutate()}
                  disabled={!content.trim() || submitMutation.isPending}
                  className="px-6 py-2 rounded-lg bg-indigo-600 hover:bg-indigo-500 disabled:opacity-50 font-medium"
                >
                  {submitMutation.isPending ? "Submitting..." : "Submit Assignment"}
                </button>
              </>
            )}
          </div>
        )}
      </div>

      {message && <p className="mt-4 text-sm text-emerald-400">{message}</p>}
    </div>
  );
}
