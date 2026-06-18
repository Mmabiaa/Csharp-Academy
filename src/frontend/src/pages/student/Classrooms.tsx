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
import { Download, Trash2, UploadCloud } from "lucide-react";

export default function Classrooms() {
  const { token, isTeacher } = useAuth();
  const queryClient = useQueryClient();
  const [showCreate, setShowCreate] = useState(false);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [courseId, setCourseId] = useState<number | "">("");
  const [joinCode, setJoinCode] = useState("");
  const [message, setMessage] = useState("");

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
      setMessage("Classroom created!");
      setShowCreate(false);
      setName("");
      setDescription("");
      setCourseId("");
      queryClient.invalidateQueries({ queryKey: ["classrooms"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  const joinMutation = useMutation({
    mutationFn: () => joinClassroom(token!, joinCode),
    onSuccess: () => {
      setMessage("Joined classroom successfully!");
      setJoinCode("");
      queryClient.invalidateQueries({ queryKey: ["classrooms"] });
    },
    onError: (err: Error) => setMessage(err.message),
  });

  if (!token) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
            Classrooms
          </h1>
          <p className="text-neutral-600 font-semibold">
            <Link to="/login" className="text-[#58CC02] font-black hover:underline">Sign in</Link> to manage classrooms.
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
            Classrooms
          </h1>
          <p className="text-neutral-600 font-semibold">
            Join or create study groups
          </p>
        </div>
        {isTeacher && (
          <button
            onClick={() => setShowCreate(!showCreate)}
            className="duo-btn duo-btn-primary"
          >
            {showCreate ? "Cancel" : "Create Classroom"}
          </button>
        )}
      </div>

      {showCreate && isTeacher && (
        <form
          onSubmit={(e) => {
            e.preventDefault();
            createMutation.mutate();
          }}
          className="duo-card space-y-4"
        >
          <div>
            <label className="block text-sm font-black text-neutral-700 mb-1">
              Name
            </label>
            <input
              required
              value={name}
              onChange={(e) => setName(e.target.value)}
              placeholder="Enter classroom name"
              className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
            />
          </div>
          <div>
            <label className="block text-sm font-black text-neutral-700 mb-1">
              Description
            </label>
            <textarea
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              placeholder="Describe your classroom"
              className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
              rows={3}
            />
          </div>
          <div>
            <label className="block text-sm font-black text-neutral-700 mb-1">
              Linked Course (optional)
            </label>
            <select
              value={courseId}
              onChange={(e) => setCourseId(e.target.value ? Number(e.target.value) : "")}
              className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
            >
              <option value="">None</option>
              {courses?.map((c) => (
                <option key={c.id} value={c.id}>{c.title}</option>
              ))}
            </select>
          </div>
          <button
            type="submit"
            disabled={createMutation.isPending}
            className="duo-btn duo-btn-primary"
          >
            {createMutation.isPending ? "Creating..." : "Create"}
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
            <label className="block text-sm font-black text-neutral-700 mb-1">
              Join Code
            </label>
            <input
              required
              value={joinCode}
              onChange={(e) => setJoinCode(e.target.value.toUpperCase())}
              placeholder="Enter join code"
              className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-mono font-black text-neutral-800 uppercase focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
            />
          </div>
          <button
            type="submit"
            disabled={joinMutation.isPending}
            className="duo-btn bg-purple-600 shadow-[0_4px_0_#4C1D95]"
          >
            {joinMutation.isPending ? "Joining..." : "Join"}
          </button>
        </form>
      )}

      {message && (
        <div className={`duo-card border-2 ${
          message.includes("success") || message.includes("created") 
            ? "bg-[#58CC02]/10 border-[#58CC02]/30" 
            : "bg-[#FF4B4B]/10 border-[#FF4B4B]/30"
        }`}>
          <p className={`text-sm font-black ${
            message.includes("success") || message.includes("created") 
              ? "text-[#58CC02]" 
              : "text-[#FF4B4B]"
          }`}>
            {message}
          </p>
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
        <div className="duo-card text-center py-8">
          <p className="text-neutral-600 font-semibold">
            {isTeacher ? "No classrooms yet. Create one to get started." : "You haven't joined any classrooms yet."}
          </p>
        </div>
      ) : (
        <div className="space-y-4">
          {classrooms.map((c) => (
            <div key={c.id} className="duo-card">
              <div className="flex flex-wrap justify-between items-start gap-3 mb-2">
                <h2 className="text-xl font-black text-neutral-900">{c.name}</h2>
                {isTeacher && (
                  <span className="text-sm font-mono font-black bg-neutral-100 text-neutral-800 px-3 py-1 rounded-xl">
                    Code: {c.joinCode}
                  </span>
                )}
              </div>
              <p className="text-neutral-600 font-semibold mb-2">
                {c.description || "No description"}
              </p>
              <p className="text-xs font-bold text-neutral-500">
                Teacher: {c.teacherName}
                {c.courseTitle && ` · Course: ${c.courseTitle}`}
                · {c.memberCount} member{c.memberCount !== 1 ? "s" : ""}
              </p>
              {c.members.length > 0 && isTeacher && (
                <ul className="mt-4 border-t border-[#e5e5e5] pt-3 space-y-1">
                  {c.members.map((m) => (
                    <li key={m.userId} className="text-sm font-semibold text-neutral-700">
                      {m.name} ({m.email})
                    </li>
                  ))}
                </ul>
              )}

              {/* Classroom Files Section */}
              <div className="mt-6 border-t border-[#e5e5e5] pt-4">
                <div className="flex items-center justify-between mb-4">
                  <h3 className="font-black text-neutral-900 flex items-center gap-2">
                    Classroom Files
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
                              setMessage(err.message);
                            }
                          }
                        }}
                      />
                      <label
                        htmlFor={`file-upload-${c.id}`}
                        className="cursor-pointer duo-btn bg-neutral-200 text-neutral-800 shadow-[0_4px_0_#9CA3AF] flex items-center gap-2"
                      >
                        <UploadCloud className="w-4 h-4" />
                        Upload File
                      </label>
                    </div>
                  )}
                </div>

                {c.attachments?.length > 0 ? (
                  <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
                    {c.attachments.map((file) => (
                      <div
                        key={file.id}
                        className="flex items-center justify-between p-4 bg-neutral-50 rounded-xl border border-[#e5e5e5]"
                      >
                        <div className="flex items-center gap-3 overflow-hidden">
                          <div className="p-2 bg-white rounded-xl border border-[#e5e5e5] text-neutral-500 font-black text-xs">
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
                        <div className="flex gap-2">
                          <a
                            href={getFileUrl(file.fileUrl)}
                            target="_blank"
                            rel="noopener noreferrer"
                            className="p-2 text-neutral-500 hover:text-[#1CB0F6] transition-colors"
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
                                    setMessage(err.message);
                                  }
                                }
                              }}
                              className="p-2 text-neutral-500 hover:text-[#FF4B4B] transition-colors"
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
                  <p className="text-sm font-semibold text-neutral-500 italic">
                    No files shared yet.
                  </p>
                )}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
