import { useState } from "react";
import { Link } from "react-router-dom";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import {
  fetchClassrooms,
  createClassroom,
  joinClassroom,
  fetchCourses,
  uploadAttachment,
  deleteAttachment,
  getFileUrl,
} from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import {
  Download,
  Trash2,
  UploadCloud,
  Users,
  CheckCircle2,
  XCircle,
  Plus,
  X,
  KeyRound,
  FileText,
  BookOpen,
} from "lucide-react";

export default function Classrooms() {
  const { token, isTeacher } = useAuth();
  const queryClient = useQueryClient();
  const [showCreate, setShowCreate] = useState(false);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [courseId, setCourseId] = useState<number | "">("");
  const [joinCode, setJoinCode] = useState("");
  const [message, setMessage] = useState("");
  const [isSuccess, setIsSuccess] = useState(false);

  const { data: classrooms, isLoading } = useQuery({
    queryKey: ["classrooms"],
    queryFn: () => fetchClassrooms(token!),
    enabled: !!token,
  });

  const { data: courses } = useQuery({
    queryKey: ["courses"],
    queryFn: fetchCourses,
    enabled: isTeacher && showCreate,
  });

  const createMutation = useMutation({
    mutationFn: () =>
      createClassroom(token!, {
        name,
        description,
        courseId: courseId === "" ? null : courseId,
      }),
    onSuccess: () => {
      setIsSuccess(true);
      setMessage("Classroom created!");
      setShowCreate(false);
      setName("");
      setDescription("");
      setCourseId("");
      queryClient.invalidateQueries({ queryKey: ["classrooms"] });
    },
    onError: (err: Error) => {
      setIsSuccess(false);
      setMessage(err.message);
    },
  });

  const joinMutation = useMutation({
    mutationFn: () => joinClassroom(token!, joinCode),
    onSuccess: () => {
      setIsSuccess(true);
      setMessage("Joined classroom successfully!");
      setJoinCode("");
      queryClient.invalidateQueries({ queryKey: ["classrooms"] });
    },
    onError: (err: Error) => {
      setIsSuccess(false);
      setMessage(err.message);
    },
  });

  if (!token) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2 flex items-center gap-2">
            <Users className="w-7 h-7 text-[#CE82FF]" />
            Classrooms
          </h1>
          <p className="text-neutral-600 font-bold">
            <Link to="/login" className="text-[#58CC02] font-black hover:underline">
              Sign in
            </Link>{" "}
            to manage classrooms.
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 flex items-center gap-2">
            <Users className="w-7 h-7 text-[#CE82FF]" />
            Classrooms
          </h1>
          <p className="text-neutral-600 font-bold">Join or create study groups</p>
        </div>
        {isTeacher && (
          <button
            onClick={() => setShowCreate(!showCreate)}
            className={showCreate ? "duo-btn3d duo-btn3d-white" : "duo-btn3d duo-btn3d-green"}
          >
            {showCreate ? (
              <>
                <X className="w-4 h-4" />
                Cancel
              </>
            ) : (
              <>
                <Plus className="w-4 h-4" />
                Create classroom
              </>
            )}
          </button>
        )}
      </div>

      {showCreate && isTeacher && (
        <form
          onSubmit={(e) => {
            e.preventDefault();
            createMutation.mutate();
          }}
          className="duo-card space-y-4 duo-pop"
        >
          <div>
            <label className="block text-sm font-black text-neutral-700 mb-1">Name</label>
            <input
              required
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="Enter classroom name"
              className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
            />
          </div>
          <div>
            <label className="block text-sm font-black text-neutral-700 mb-1">Description</label>
            <textarea
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="Describe your classroom"
              className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
              rows={3}
            />
          </div>
          <div>
            <label className="block text-sm font-black text-neutral-700 mb-1">
              Linked course (optional)
            </label>
            <select
              value={courseId}
              onChange={(e) => setCourseId(e.target.value ? Number(e.target.value) : "")}
              className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
            >
              <option value="">None</option>
              {courses?.map((c) => (
                <option key={c.id} value={c.id}>
                  {c.title}
                </option>
              ))}
            </select>
          </div>
          <button
            type="submit"
            disabled={createMutation.isPending}
            className="duo-btn3d duo-btn3d-green"
          >
            {createMutation.isPending ? "Creating..." : "Create classroom"}
          </button>
        </form>
      )}

      {!isTeacher && (
        <form
          onSubmit={(e) => {
            e.preventDefault();
            joinMutation.mutate();
          }}
          className="duo-card flex flex-wrap gap-3 items-end"
        >
          <div className="flex-1 min-w-[200px]">
            <label className="block text-sm font-black text-neutral-700 mb-1 flex items-center gap-1.5">
              <KeyRound className="w-4 h-4 text-neutral-400" />
              Join code
            </label>
            <input
              required
              value={joinCode}
              onChange={(e) => setJoinCode(e.target.value.toUpperCase())}
              placeholder="Enter join code"
              className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-mono font-black text-neutral-800 uppercase focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
            />
          </div>
          <button
            type="submit"
            disabled={joinMutation.isPending}
            className="duo-btn3d bg-[#CE82FF] text-white shadow-[0_4px_0_#A568CC]"
          >
            {joinMutation.isPending ? "Joining..." : "Join"}
          </button>
        </form>
      )}

      {message && (
        <div
          className={`flex items-center gap-2 p-4 rounded-2xl font-black text-sm ${isSuccess ? "bg-[#D7FFB8] text-[#46A302]" : "bg-[#FFDFE0] text-[#CC3A3A]"
            }`}
        >
          {isSuccess ? (
            <CheckCircle2 className="w-5 h-5 shrink-0" />
          ) : (
            <XCircle className="w-5 h-5 shrink-0" />
          )}
          {message}
        </div>
      )}

      {isLoading ? (
        <div className="space-y-4">
          <div className="duo-card animate-pulse">
            <div className="h-40" />
          </div>
          <div className="duo-card animate-pulse">
            <div className="h-40" />
          </div>
        </div>
      ) : !classrooms?.length ? (
        <div className="duo-panel text-center py-12">
          <div className="w-16 h-16 rounded-full bg-[#EEEDFE] flex items-center justify-center mx-auto mb-4">
            <Users className="w-8 h-8 text-[#7F77DD]" />
          </div>
          <p className="font-black text-neutral-900 text-lg">
            {isTeacher ? "No classrooms yet" : "No classrooms joined yet"}
          </p>
          <p className="text-sm font-bold text-neutral-500 mt-1">
            {isTeacher ? "Create one to get started." : "Ask your teacher for a join code."}
          </p>
        </div>
      ) : (
        <div className="space-y-4">
          {classrooms.map((c) => (
            <div key={c.id} className="duo-card">
              <div className="flex flex-wrap justify-between items-start gap-3 mb-2">
                <h2 className="text-xl font-black text-neutral-900">{c.name}</h2>
                {isTeacher && (
                  <span className="duo-badge duo-badge-gray font-mono">
                    code: {c.joinCode}
                  </span>
                )}
              </div>
              <p className="text-neutral-600 font-bold mb-3">
                {c.description || "No description"}
              </p>
              <div className="flex flex-wrap items-center gap-2">
                <span className="duo-badge duo-badge-blue">{c.teacherName}</span>
                {c.courseTitle && <span className="duo-badge duo-badge-gray">{c.courseTitle}</span>}
                <span className="duo-badge duo-badge-green">
                  {c.memberCount} member{c.memberCount !== 1 ? "s" : ""}
                </span>
                <Link
                  to={`/assignments?classId=${c.id}`}
                  className="duo-badge duo-badge-blue hover:bg-[#1CB0F6] hover:text-white transition-colors cursor-pointer flex items-center gap-1"
                >
                  <BookOpen className="w-3 h-3" />
                  View Assignments
                </Link>
              </div>

              {c.members.length > 0 && isTeacher && (
                <ul className="mt-4 border-t-2 border-[#e5e5e5] pt-3 space-y-1.5">
                  {c.members.map((m) => (
                    <li
                      key={m.userId}
                      className="text-sm font-bold text-neutral-700 flex items-center gap-2"
                    >
                      <div className="w-6 h-6 rounded-full bg-[#DDF4FF] flex items-center justify-center text-[10px] font-black text-[#1899D6] shrink-0">
                        {m.name.charAt(0).toUpperCase()}
                      </div>
                      {m.name}{" "}
                      <span className="text-neutral-400 font-bold">({m.email})</span>
                    </li>
                  ))}
                </ul>
              )}

              {/* Classroom Files Section */}
              <div className="mt-6 border-t-2 border-[#e5e5e5] pt-4">
                <div className="flex items-center justify-between mb-4">
                  <h3 className="font-black text-neutral-900 flex items-center gap-2">
                    <FileText className="w-4 h-4 text-neutral-400" />
                    Classroom files
                  </h3>
                  {isTeacher && (
                    <div className="relative">
                      <input
                        type="file"
                        id={`file-upload-${c.id}`}
                        className="hidden"
                        onChange={async (e) => {
                          const file = e.target.files?.[0];
                          if (file) {
                            try {
                              await uploadAttachment(token!, file, { classroomId: c.id });
                              queryClient.invalidateQueries({ queryKey: ["classrooms"] });
                            } catch (err: any) {
                              setIsSuccess(false);
                              setMessage(err.message);
                            }
                          }
                        }}
                      />
                      <label
                        htmlFor={`file-upload-${c.id}`}
                        className="cursor-pointer duo-btn3d duo-btn3d-white !px-4 !py-2.5 !text-xs"
                      >
                        <UploadCloud className="w-4 h-4" />
                        Upload file
                      </label>
                    </div>
                  )}
                </div>

                {c.attachments?.length > 0 ? (
                  <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
                    {c.attachments.map((file) => (
                      <div
                        key={file.id}
                        className="flex items-center justify-between p-4 bg-neutral-50 rounded-2xl border-2 border-[#e5e5e5]"
                      >
                        <div className="flex items-center gap-3 overflow-hidden">
                          <div className="w-10 h-10 flex items-center justify-center bg-white rounded-xl border-2 border-[#e5e5e5] text-neutral-500 font-black text-[10px] shrink-0">
                            {file.fileType.toUpperCase()}
                          </div>
                          <div className="overflow-hidden">
                            <p className="text-sm font-black text-neutral-900 truncate">
                              {file.fileName}
                            </p>
                            <p className="text-xs font-bold text-neutral-500">
                              {(file.fileSize / 1024 / 1024).toFixed(2)} MB
                            </p>
                          </div>
                        </div>
                        <div className="flex gap-1 shrink-0">
                          <a
                            href={getFileUrl(file.fileUrl)}
                            target="_blank"
                            rel="noopener noreferrer"
                            className="p-2 text-neutral-400 hover:text-[#1CB0F6] hover:bg-white rounded-xl transition-colors"
                            title="Download"
                          >
                            <Download className="w-5 h-5" />
                          </a>
                          {isTeacher && (
                            <button
                              onClick={async () => {
                                if (confirm("Delete this file?")) {
                                  try {
                                    await deleteAttachment(token!, file.id);
                                    queryClient.invalidateQueries({ queryKey: ["classrooms"] });
                                  } catch (err: any) {
                                    setIsSuccess(false);
                                    setMessage(err.message);
                                  }
                                }
                              }}
                              className="p-2 text-neutral-400 hover:text-[#FF4B4B] hover:bg-white rounded-xl transition-colors"
                              title="Delete"
                            >
                              <Trash2 className="w-5 h-5" />
                            </button>
                          )}
                        </div>
                      </div>
                    ))}
                  </div>
                ) : (
                  <p className="text-sm font-bold text-neutral-400">No files shared yet.</p>
                )}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}