import { useState } from "react";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import {
  fetchTeachingAssignments,
  fetchClassroomAssignments,
  fetchAssignmentSubmissions,
  gradeSubmission,
  submitAssignment,
  uploadAttachment,
  getFileUrl,
  deleteAttachment,
  createAssignment,
  fetchCourses
} from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import CodeEditor from "../../components/CodeEditor";
import {
  FileText,
  CheckCircle2,
  AlertCircle,
  UploadCloud,
  Trash2,
  X,
  Star,
  Download,
  Lock,
  Sparkles,
  Plus,
  Send,
  Users,
  Paperclip,
  ChevronRight,
  Clock,
  Briefcase
} from "lucide-react";

export default function Assignments() {
  const { user, token, isTeacher, isAdmin, isAuthenticated } = useAuth();
  const queryClient = useQueryClient();
  const [activeId, setActiveId] = useState<number | null>(null);
  const [content, setContent] = useState("");
  const [message, setMessage] = useState("");

  // Teacher management states
  const [showCreateForm, setShowCreateForm] = useState(false);
  const [gradingId, setGradingId] = useState<number | null>(null);
  const [grade, setGrade] = useState(100);
  const [feedback, setFeedback] = useState("");
  const [createForm, setCreateForm] = useState({
    title: "",
    description: "",
    instructions: "",
    courseId: 1,
    maxPoints: 100,
    requiresCode: true,
    dueDate: "",
  });

  const { data: assignments, isLoading } = useQuery({
    queryKey: ["assignments", isTeacher ? "teaching" : "classroom"],
    queryFn: () => isTeacher ? fetchTeachingAssignments(token!) : fetchClassroomAssignments(token!),
    enabled: !!token,
  });

  const active = assignments?.find((a) => a.id === activeId) ?? assignments?.[0];

  const { data: submissions } = useQuery({
    queryKey: ["assignment-submissions", active?.id],
    queryFn: () => fetchAssignmentSubmissions(active!.id, token!),
    enabled: !!token && isTeacher && !!active?.id,
  });

  const { data: courses } = useQuery({
    queryKey: ["courses-minimal"],
    queryFn: () => fetchCourses(),
    enabled: !!token && isTeacher && showCreateForm,
  });

  const submitMutation = useMutation({
    mutationFn: () => submitAssignment(active!.id, content, token!),
    onSuccess: () => {
      setMessage("Assignment submitted successfully!");
      queryClient.invalidateQueries({ queryKey: ["assignments"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  const createMutation = useMutation({
    mutationFn: () => createAssignment(token!, {
      ...createForm,
      dueDate: createForm.dueDate || undefined
    }),
    onSuccess: () => {
      setMessage("Assignment created successfully!");
      setShowCreateForm(false);
      queryClient.invalidateQueries({ queryKey: ["assignments"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  const gradeMutation = useMutation({
    mutationFn: () => gradeSubmission(gradingId!, grade, feedback, token!),
    onSuccess: () => {
      setMessage("Submission graded.");
      setGradingId(null);
      queryClient.invalidateQueries({ queryKey: ["assignment-submissions"] });
      queryClient.invalidateQueries({ queryKey: ["assignments"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  if (!isAuthenticated) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900">Assignments</h1>
        <div className="duo-panel text-center py-12">
          <Lock className="w-12 h-12 text-[#1CB0F6] mx-auto mb-4" />
          <p className="text-neutral-600 font-bold">Please <Link to="/login" className="text-[#58CC02] font-black hover:underline">sign in</Link> to view assignments.</p>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 flex items-center gap-2">
            <Briefcase className="w-7 h-7 text-[#FF9600]" />
            {isTeacher ? "Assignment Management" : "Classroom Assignments"}
          </h1>
          <p className="text-neutral-500 font-bold">
            {isTeacher ? "Create and grade work for your students" : "Complete assignments from your classrooms"}
          </p>
        </div>
        {isTeacher && (
          <button
            onClick={() => setShowCreateForm(!showCreateForm)}
            className={`duo-btn3d ${showCreateForm ? 'duo-btn3d-white' : 'duo-btn3d-green'}`}
          >
            {showCreateForm ? <X className="w-4 h-4" /> : <Plus className="w-4 h-4" />}
            {showCreateForm ? "Cancel" : "New Assignment"}
          </button>
        )}
      </div>

      {showCreateForm && (
        <div className="duo-card duo-pop space-y-6">
          <h2 className="text-xl font-black text-neutral-900">Create New Assignment</h2>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div className="space-y-2">
              <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Title</label>
              <input
                value={createForm.title}
                onChange={e => setCreateForm({ ...createForm, title: e.target.value })}
                className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                placeholder="Give it a name"
              />
            </div>
            <div className="space-y-2">
              <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Course</label>
              <select
                value={createForm.courseId}
                onChange={e => setCreateForm({ ...createForm, courseId: Number(e.target.value) })}
                className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
              >
                {courses?.map(c => <option key={c.id} value={c.id}>{c.title}</option>)}
              </select>
            </div>
          </div>
          <div className="space-y-2">
            <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Description</label>
            <textarea
              value={createForm.description}
              onChange={e => setCreateForm({ ...createForm, description: e.target.value })}
              rows={2}
              className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
              placeholder="Short summary"
            />
          </div>
          <div className="space-y-2">
            <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Detailed Instructions</label>
            <textarea
              value={createForm.instructions}
              onChange={e => setCreateForm({ ...createForm, instructions: e.target.value })}
              rows={4}
              className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
              placeholder="Mark-by-mark instructions"
            />
          </div>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            <div className="space-y-2">
              <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Due Date</label>
              <input
                type="date"
                value={createForm.dueDate}
                onChange={e => setCreateForm({ ...createForm, dueDate: e.target.value })}
                className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
              />
            </div>
            <div className="space-y-2">
              <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Max Points</label>
              <input
                type="number"
                value={createForm.maxPoints}
                onChange={e => setCreateForm({ ...createForm, maxPoints: Number(e.target.value) })}
                className="w-full px-5 py-3 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
              />
            </div>
          </div>
          <label className="flex items-center gap-3 cursor-pointer">
            <input
              type="checkbox"
              checked={createForm.requiresCode}
              onChange={e => setCreateForm({ ...createForm, requiresCode: e.target.checked })}
              className="w-5 h-5 rounded-lg accent-[#58CC02]"
            />
            <span className="font-black text-neutral-700">Requires Code Submission</span>
          </label>
          <button
            disabled={createMutation.isPending}
            onClick={() => createMutation.mutate()}
            className="w-full duo-btn3d duo-btn3d-green"
          >
            {createMutation.isPending ? "Creating..." : "Save Assignment"}
          </button>
        </div>
      )}

      <div className="grid lg:grid-cols-3 gap-6">
        {/* Sidebar: List of Assignments */}
        <div className="space-y-3">
          {isLoading ? (
            Array.from({ length: 3 }).map((_, i) => (
              <div key={i} className="duo-card animate-pulse h-24" />
            ))
          ) : assignments?.length ? (
            assignments.map((a) => {
              const isActive = active?.id === a.id;
              const isLate = !isTeacher && a.dueDate && new Date(a.dueDate) < new Date() && !a.mySubmission;
              return (
                <button
                  key={a.id}
                  onClick={() => {
                    setActiveId(a.id);
                    setContent(a.mySubmission?.content ?? "");
                    setMessage("");
                  }}
                  className={`w-full text-left p-4 rounded-2xl border-2 transition-all ${isActive
                    ? "border-[#58CC02] bg-[#58CC02]/5 -translate-y-1 shadow-[0_4px_0_#e5e5e5]"
                    : "border-[#e5e5e5] bg-white hover:border-[#1CB0F6]/50 hover:-translate-y-0.5"
                    }`}
                >
                  <div className="flex justify-between items-start mb-2">
                    <p className="font-black text-neutral-900 truncate flex-1">{a.title}</p>
                    {isTeacher && (
                      <span className="text-[10px] font-black text-neutral-400 uppercase tracking-wider ml-2 shrink-0">
                        {a.submissionCount} Submissions
                      </span>
                    )}
                  </div>
                  <div className="flex gap-2">
                    {isTeacher ? (
                      <span className="duo-badge duo-badge-blue">
                        {a.courseTitle || "General"}
                      </span>
                    ) : (
                      a.mySubmission ? (
                        <span className={`duo-badge ${a.mySubmission.status === 'Graded' ? 'duo-badge-green' : 'duo-badge-yellow'}`}>
                          {a.mySubmission.status === "Graded" ? `Graded: ${a.mySubmission.grade}` : "Submitted"}
                        </span>
                      ) : isLate ? (
                        <span className="duo-badge duo-badge-red">Overdue</span>
                      ) : (
                        <span className="duo-badge duo-badge-yellow">Pending</span>
                      )
                    )}
                  </div>
                </button>
              );
            })
          ) : (
            <div className="duo-panel text-center py-10">
              <p className="text-neutral-500 font-bold">No assignments available in your classrooms.</p>
            </div>
          )}
        </div>

        {/* Main Content Area */}
        <div className="lg:col-span-2 space-y-6">
          {active ? (
            <>
              <div className="duo-card space-y-5">
                <div className="flex justify-between items-start">
                  <div>
                    <h2 className="text-2xl font-black text-neutral-900">{active.title}</h2>
                    <div className="flex gap-4 mt-2 text-xs font-black text-neutral-400 uppercase tracking-widest">
                      <span className="flex items-center gap-1"><Clock className="w-3.5 h-3.5" /> Due: {active.dueDate ? new Date(active.dueDate).toLocaleDateString() : 'N/A'}</span>
                      <span className="flex items-center gap-1"><Star className="w-3.5 h-3.5" /> {active.maxPoints} pts</span>
                    </div>
                  </div>
                  {!isTeacher && active.mySubmission?.status === "Graded" && (
                    <div className="w-16 h-16 rounded-full bg-[#D7FFB8] border-4 border-white shadow-lg flex items-center justify-center flex-col shrink-0">
                      <span className="text-xs font-black text-[#46A302]">SCORE</span>
                      <span className="text-xl font-black text-neutral-900">{active.mySubmission.grade}</span>
                    </div>
                  )}
                </div>

                <div className="p-5 bg-neutral-50 rounded-3xl border-2 border-neutral-100">
                  <h3 className="text-[10px] font-black text-neutral-400 uppercase tracking-[0.2em] mb-3 flex items-center gap-2">
                    <FileText className="w-4 h-4" /> Description
                  </h3>
                  <p className="text-sm font-bold text-neutral-700 leading-relaxed italic border-l-4 border-[#e5e5e5] pl-4">
                    {active.description}
                  </p>
                  <p className="text-sm font-semibold text-neutral-800 mt-4 whitespace-pre-wrap">
                    {active.instructions}
                  </p>
                </div>

                {/* Submissions Section for Teachers */}
                {isTeacher && (
                  <div className="pt-6 border-t-2 border-neutral-100 space-y-4">
                    <h3 className="text-lg font-black text-neutral-900 flex items-center gap-2">
                      <Users className="w-5 h-5 text-[#CE82FF]" />
                      Student Submissions
                    </h3>
                    <div className="space-y-3">
                      {submissions?.map(s => (
                        <div key={s.id} className="p-4 bg-white rounded-2xl border-2 border-neutral-100 hover:border-[#1CB0F6] transition-colors">
                          <div className="flex justify-between items-start mb-3">
                            <div className="flex items-center gap-3">
                              <div className="w-10 h-10 rounded-full bg-[#DDF4FF] flex items-center justify-center font-black text-[#1CB0F6]">
                                {s.studentName?.charAt(0) || "U"}
                              </div>
                              <div>
                                <p className="font-black text-neutral-900">{s.studentName || "Anonymous Student"}</p>
                                <p className="text-xs font-bold text-neutral-400">{new Date(s.submittedAt).toLocaleString()}</p>
                              </div>
                            </div>
                            {s.status === "Submitted" ? (
                              <button
                                onClick={() => {
                                  setGradingId(s.id);
                                  setGrade(active.maxPoints);
                                }}
                                className="duo-btn3d duo-btn3d-yellow !px-4 !py-2 !text-xs"
                              >
                                Grade
                              </button>
                            ) : (
                              <span className="duo-badge duo-badge-green">Graded: {s.grade}</span>
                            )}
                          </div>
                          {s.content && (
                            <pre className="text-xs font-mono bg-neutral-900 text-[#7FE787] p-4 rounded-xl overflow-x-auto max-h-48">
                              {s.content}
                            </pre>
                          )}
                          {s.attachments?.length > 0 && (
                            <div className="mt-3 flex flex-wrap gap-2">
                              {s.attachments.map(file => (
                                <a key={file.id} href={getFileUrl(file.fileUrl)} target="_blank" className="flex items-center gap-2 px-3 py-1.5 bg-neutral-100 rounded-xl text-xs font-black text-neutral-600 hover:bg-[#DDF4FF] transition-colors">
                                  <Paperclip className="w-3 h-3" /> {file.fileName}
                                </a>
                              ))}
                            </div>
                          )}
                        </div>
                      ))}
                      {!submissions?.length && (
                        <div className="bg-neutral-50 p-8 rounded-3xl text-center">
                          <p className="text-sm font-bold text-neutral-400">No submissions yet for this assignment.</p>
                        </div>
                      )}
                    </div>
                  </div>
                )}

                {/* Submission/Status Section for Students */}
                {!isTeacher && (
                  <div className="pt-6 border-t-2 border-neutral-100 space-y-4">
                    {active.mySubmission ? (
                      <div className="space-y-4">
                        <div className={`p-5 rounded-3xl border-2 ${active.mySubmission.status === 'Graded' ? 'bg-[#F2FFE3] border-[#58CC02]/20' : 'bg-[#FFF8E1] border-[#FFC800]/20'}`}>
                          <h3 className="text-xs font-black uppercase tracking-widest mb-3 flex items-center gap-2">
                            {active.mySubmission.status === 'Graded' ? <CheckCircle2 className="w-4 h-4 text-[#46A302]" /> : <Clock className="w-4 h-4 text-[#946800]" />}
                            Your Submission ({active.mySubmission.status})
                          </h3>
                          {active.mySubmission.feedback && (
                            <p className="text-sm font-bold text-neutral-700 bg-white/50 p-4 rounded-2xl border-2 border-white/50 mb-4 shadow-sm">
                              "{active.mySubmission.feedback}"
                            </p>
                          )}
                          <pre className="text-xs font-mono bg-neutral-900 text-[#7FE787] p-4 rounded-xl overflow-x-auto">
                            {active.mySubmission.content}
                          </pre>
                        </div>
                      </div>
                    ) : (
                      <div className="space-y-4 animate-pop">
                        <h3 className="text-lg font-black text-neutral-900">Your Submission</h3>
                        {active.requiresCode ? (
                          <div className="border-2 border-[#e5e5e5] rounded-3xl overflow-hidden shadow-inner">
                            <CodeEditor value={content} onChange={setContent} rows={12} />
                          </div>
                        ) : (
                          <textarea
                            value={content}
                            onChange={e => setContent(e.target.value)}
                            className="w-full px-5 py-4 rounded-3xl border-2 border-[#e5e5e5] focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all font-bold text-neutral-800"
                            placeholder="Write your answer here..."
                            rows={8}
                          />
                        )}

                        <div className="flex flex-wrap gap-3">
                          <button
                            disabled={!content.trim() || submitMutation.isPending}
                            onClick={() => submitMutation.mutate()}
                            className="flex-1 duo-btn3d duo-btn3d-green"
                          >
                            <Send className="w-4 h-4" />
                            {submitMutation.isPending ? "Submitting..." : "Submit Assignment"}
                          </button>

                          <div className="relative">
                            <input
                              type="file"
                              id="assignment-file-sub"
                              className="hidden"
                              onChange={async (e) => {
                                const file = e.target.files?.[0];
                                if (file) {
                                  try {
                                    await uploadAttachment(token!, file, { assignmentId: active.id });
                                    queryClient.invalidateQueries({ queryKey: ["assignments"] });
                                  } catch (err: any) { setMessage(err.message); }
                                }
                              }}
                            />
                            <label htmlFor="assignment-file-sub" className="cursor-pointer duo-btn3d duo-btn3d-white">
                              <UploadCloud className="w-4 h-4" />
                              Attach File
                            </label>
                          </div>
                        </div>

                        {active.attachments?.some(a => a.uploadedById === user?.userId) && (
                          <div className="grid grid-cols-1 sm:grid-cols-2 gap-2 mt-4">
                            {active.attachments.filter(a => a.uploadedById === user?.userId).map(file => (
                              <div key={file.id} className="flex items-center justify-between p-3 bg-neutral-50 rounded-2xl border-2 border-neutral-100">
                                <span className="text-xs font-black text-neutral-800 truncate px-2">{file.fileName}</span>
                                <button
                                  onClick={async () => {
                                    if (confirm("Remove file?")) {
                                      await deleteAttachment(token!, file.id);
                                      queryClient.invalidateQueries({ queryKey: ["assignments"] });
                                    }
                                  }}
                                  className="p-1.5 text-neutral-400 hover:text-[#FF4B4B] transition-colors"
                                >
                                  <Trash2 className="w-4 h-4" />
                                </button>
                              </div>
                            ))}
                          </div>
                        )}
                      </div>
                    )}
                  </div>
                )}
              </div>
            </>
          ) : (
            <div className="duo-panel text-center py-20 bg-white shadow-xl">
              <div className="w-20 h-20 bg-neutral-50 rounded-full flex items-center justify-center mx-auto mb-6">
                <FileText className="w-10 h-10 text-neutral-300" />
              </div>
              <h3 className="text-xl font-black text-neutral-900">Select an assignment</h3>
              <p className="text-sm font-bold text-neutral-500 mt-2">Pick something from the list to get started.</p>
            </div>
          )}
        </div>
      </div>

      {/* Grading Modal */}
      {gradingId && (
        <div className="fixed inset-0 z-[100] flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm">
          <div className="bg-white w-full max-w-md rounded-[32px] overflow-hidden shadow-2xl animate-pop">
            <div className="px-8 py-6 border-b-2 border-neutral-100 flex items-center justify-between">
              <h2 className="text-xl font-black text-neutral-900">Grade Submission</h2>
              <button onClick={() => setGradingId(null)} className="p-2 hover:bg-neutral-100 rounded-full transition-colors">
                <X className="w-6 h-6 text-neutral-400" />
              </button>
            </div>
            <div className="p-8 space-y-6">
              <div className="space-y-2">
                <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Grade (Out of {active?.maxPoints || 100})</label>
                <input
                  type="number"
                  value={grade}
                  onChange={e => setGrade(Number(e.target.value))}
                  className="w-full px-5 py-4 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold text-2xl text-center"
                />
              </div>
              <div className="space-y-2">
                <label className="text-xs font-black text-neutral-400 uppercase tracking-widest pl-1">Feedback</label>
                <textarea
                  value={feedback}
                  onChange={e => setFeedback(e.target.value)}
                  rows={4}
                  className="w-full px-5 py-4 rounded-2xl bg-neutral-50 border-2 border-transparent focus:border-[#58CC02] focus:bg-white transition-all font-bold"
                  placeholder="What did the student do well? What can they improve?"
                />
              </div>
              <button
                disabled={gradeMutation.isPending}
                onClick={() => gradeMutation.mutate()}
                className="w-full duo-btn3d duo-btn3d-green"
              >
                {gradeMutation.isPending ? "Submitting Grade..." : "Submit Grade"}
              </button>
            </div>
          </div>
        </div>
      )}

      {message && (
        <div className="flex items-center gap-2 p-4 rounded-2xl font-bold text-sm bg-[#D7FFB8] text-[#46A302] fixed bottom-6 right-6 shadow-2xl animate-pop z-[200]">
          <Sparkles className="w-5 h-5 shrink-0" />
          <span className="font-black">{message}</span>
        </div>
      )}
    </div>
  );
}