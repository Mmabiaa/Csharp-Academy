import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchMyAssignments, submitAssignment, uploadAttachment, getFileUrl, deleteAttachment } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import CodeEditor from "../../components/CodeEditor";
import { FileText, CheckCircle2, AlertCircle, UploadCloud, Trash2, Download } from "lucide-react";

export default function Assignments() {
  const { user, token, isAuthenticated } = useAuth();
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
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
            Assignments
          </h1>
          <p className="text-neutral-600 font-semibold">
            <Link to="/login" className="text-[#58CC02] font-black hover:underline">Sign in</Link> to view assignments.
          </p>
        </div>
      </div>
    );
  }

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-16 w-64 bg-neutral-200 rounded-xl" />
        <div className="grid lg:grid-cols-3 gap-6">
          <div className="space-y-2">
            <div className="duo-card animate-pulse">
              <div className="h-32" />
            </div>
          </div>
          <div className="lg:col-span-2">
            <div className="duo-card animate-pulse">
              <div className="h-96" />
            </div>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Assignments
        </h1>
        <p className="text-neutral-600 font-semibold">
          Submit your work and track grades from your teachers.
        </p>
      </div>

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
                className={`w-full text-left p-4 rounded-xl border transition-all ${active?.id === a.id
                  ? "border-[#58CC02] bg-[#58CC02]/10 translate-y-[-2px]"
                  : "border-[#e5e5e5] bg-white hover:border-[#58CC02]/50 hover:translate-y-[-2px]"
                  }`}
              >
                <p className="font-black text-neutral-900">{a.title}</p>
                <p className="text-xs font-bold text-neutral-500 mt-1">{a.courseTitle}</p>
                <div className="flex gap-2 mt-2">
                  {submitted ? (
                    <span className="text-xs font-black px-3 py-1.5 rounded-xl bg-[#58CC02]/10 text-[#58CC00] border border-[#58CC02]/30">
                      {submitted.status === "Graded" ? `Graded: ${submitted.grade}` : "Submitted"}
                    </span>
                  ) : isLate ? (
                    <span className="text-xs font-black px-3 py-1.5 rounded-xl bg-[#FF4B4B]/10 text-[#FF4B4B] border border-[#FF4B4B]/30">
                      Overdue
                    </span>
                  ) : (
                    <span className="text-xs font-black px-3 py-1.5 rounded-xl bg-[#FFC800]/10 text-[#FFC800] border border-[#FFC800]/30">
                      Pending
                    </span>
                  )}
                </div>
              </button>
            );
          })}
          {!assignments?.length && (
            <div className="duo-card text-center py-8">
              <p className="text-neutral-600 font-semibold">
                No assignments available. Join a classroom or enroll in a course.
              </p>
            </div>
          )}
        </div>

        {active && (
          <div className="lg:col-span-2 duo-card space-y-4">
            <div>
              <h2 className="text-xl font-black text-neutral-900">{active.title}</h2>
              <p className="text-neutral-600 font-semibold mt-1">{active.description}</p>
              {active.dueDate && (
                <p className="text-xs font-bold text-neutral-500 mt-2">
                  Due: {new Date(active.dueDate).toLocaleDateString()} · {active.maxPoints} points
                </p>
              )}
            </div>

            <div className="border border-[#e5e5e5] rounded-xl p-4">
              <h3 className="text-sm font-black text-neutral-800 mb-2 flex items-center gap-2">
                <FileText className="w-4 h-4" />
                Instructions
              </h3>
              <p className="text-sm font-semibold text-neutral-700 whitespace-pre-wrap mb-4">{active.instructions}</p>

              {active.attachments?.filter(a => a.uploadedById !== user?.userId).length > 0 && (
                <div className="space-y-2 border-t border-[#e5e5e5] pt-3">
                  <p className="text-xs font-black text-neutral-500 uppercase tracking-wider">Instruction Files</p>
                  <div className="grid grid-cols-1 sm:grid-cols-2 gap-2">
                    {active.attachments.filter(a => a.uploadedById !== user?.userId).map(file => (
                      <div key={file.id} className="flex items-center justify-between p-3 bg-neutral-50 rounded-xl border border-[#e5e5e5]">
                        <span className="text-sm font-black text-neutral-800 truncate max-w-[150px]">{file.fileName}</span>
                        <a
                          href={getFileUrl(file.fileUrl)}
                          target="_blank"
                          rel="noopener noreferrer"
                          className="text-[#1CB0F6] font-black text-xs flex items-center gap-1"
                        >
                          <Download className="w-4 h-4" />
                          Download
                        </a>
                      </div>
                    ))}
                  </div>
                </div>
              )}
            </div>

            {active.mySubmission?.status === "Graded" ? (
              <div className="border border-[#58CC02]/30 bg-[#58CC02]/10 rounded-xl p-4">
                <div className="flex items-center gap-2 mb-2">
                  <CheckCircle2 className="w-5 h-5 text-[#58CC02]" />
                  <p className="font-black text-[#58CC02]">
                    Grade: {active.mySubmission.grade} / {active.maxPoints}
                  </p>
                </div>
                {active.mySubmission.feedback && (
                  <p className="text-sm font-semibold text-neutral-700 mt-2">{active.mySubmission.feedback}</p>
                )}
                <pre className="mt-3 text-xs font-mono bg-white p-3 rounded-xl border border-[#e5e5e5] overflow-x-auto">{active.mySubmission.content}</pre>
              </div>
            ) : active.mySubmission ? (
              <div className="border border-[#e5e5e5] rounded-xl p-4">
                <div className="flex items-center gap-2 mb-4">
                  <AlertCircle className="w-5 h-5 text-[#FFC800]" />
                  <p className="text-sm font-black text-neutral-800">Submitted — awaiting grade</p>
                </div>
                <pre className="mb-4 text-xs font-mono bg-neutral-50 p-3 rounded-xl border border-[#e5e5e5] overflow-x-auto">{active.mySubmission.content}</pre>

                {active.mySubmission.attachments?.length > 0 && (
                  <div className="space-y-2">
                    <p className="text-xs font-black text-neutral-500 uppercase tracking-wider">Attachments</p>
                    {active.mySubmission.attachments.map(file => (
                      <div key={file.id} className="flex items-center justify-between p-3 bg-neutral-50 rounded-xl border border-[#e5e5e5]">
                        <span className="text-sm font-black text-neutral-800 truncate">{file.fileName}</span>
                        <a
                          href={getFileUrl(file.fileUrl)}
                          target="_blank"
                          rel="noopener noreferrer"
                          className="text-[#1CB0F6] font-black text-xs flex items-center gap-1"
                        >
                          <Download className="w-4 h-4" />
                          Download
                        </a>
                      </div>
                    ))}
                  </div>
                )}
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
                    className="w-full px-4 py-3 rounded-xl border border-[#e5e5e5] font-mono text-sm font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
                  />
                )}

                <div className="space-y-4">
                  <div className="flex flex-wrap gap-3 items-center">
                    <button
                      onClick={() => submitMutation.mutate()}
                      disabled={(!content.trim() && !active.attachments?.length) || submitMutation.isPending}
                      className="duo-btn duo-btn-primary"
                    >
                      {submitMutation.isPending ? "Submitting..." : "Submit Assignment"}
                    </button>

                    <div className="relative">
                      <input
                        type="file"
                        id="assignment-file"
                        className="hidden"
                        onChange={async (e) => {
                          const file = e.target.files?.[0];
                          if (file && active) {
                            try {
                              await uploadAttachment(token!, file, { assignmentId: active.id });
                              queryClient.invalidateQueries({ queryKey: ["my-assignments"] });
                            } catch (err: any) {
                              setMessage(err.message);
                            }
                          }
                        }}
                      />
                      <label
                        htmlFor="assignment-file"
                        className="cursor-pointer duo-btn bg-neutral-200 text-neutral-800 shadow-[0_4px_0_#9CA3AF] flex items-center gap-2"
                      >
                        <UploadCloud className="w-4 h-4" />
                        Attach File
                      </label>
                    </div>
                  </div>

                  {active.attachments?.some(a => a.uploadedById === user?.userId) && (
                    <div className="space-y-2">
                      <p className="text-xs font-black text-neutral-500 uppercase tracking-wider">Your Attachments</p>
                      <div className="grid grid-cols-1 sm:grid-cols-2 gap-2">
                        {active.attachments.filter(a => a.uploadedById === user?.userId).map(file => (
                          <div key={file.id} className="flex items-center justify-between p-3 bg-neutral-50 rounded-xl border border-[#e5e5e5]">
                            <span className="text-sm font-black text-neutral-800 truncate max-w-[150px]">{file.fileName}</span>
                            <div className="flex gap-2">
                              <a
                                href={getFileUrl(file.fileUrl)}
                                target="_blank"
                                rel="noopener noreferrer"
                                className="text-[#1CB0F6] font-black text-xs flex items-center gap-1"
                              >
                                <Download className="w-4 h-4" />
                                View
                              </a>
                              <button
                                onClick={async () => {
                                  if (confirm("Remove this file?")) {
                                    try {
                                      await deleteAttachment(token!, file.id);
                                      queryClient.invalidateQueries({ queryKey: ["my-assignments"] });
                                    } catch (err: any) {
                                      setMessage(err.message);
                                    }
                                  }
                                }}
                                className="text-[#FF4B4B] font-black text-xs flex items-center gap-1"
                              >
                                <Trash2 className="w-4 h-4" />
                                Remove
                              </button>
                            </div>
                          </div>
                        ))}
                      </div>
                    </div>
                  )}
                </div>
              </>
            )}
          </div>
        )}
      </div>

      {message && (
        <div className="duo-card bg-[#58CC02]/10 border-[#58CC02]/30">
          <p className="text-[#58CC02] font-black">{message}</p>
        </div>
      )}
    </div>
  );
}
