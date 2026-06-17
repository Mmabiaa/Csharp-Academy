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
      <div className="max-w-3xl mx-auto px-4 py-12">
        <p className="text-gray-600">
          <Link to="/login" className="text-blue-600 hover:underline">Sign in</Link> to manage classrooms.
        </p>
      </div>
    );
  }

  return (
    <div className="max-w-4xl mx-auto px-4 py-12">
      <div className="flex items-center justify-between mb-8">
        <h1 className="text-3xl font-bold text-gray-900">Classrooms</h1>
        {isTeacher && (
          <button
            onClick={() => setShowCreate(!showCreate)}
            className="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700 font-medium"
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
          className="bg-white p-6 rounded-lg shadow-md mb-8 space-y-4"
        >
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Name</label>
            <input
              required
              value={name}
              onChange={(e) => setName(e.target.value)}
              className="w-full px-3 py-2 border border-gray-300 rounded-md"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Description</label>
            <textarea
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              className="w-full px-3 py-2 border border-gray-300 rounded-md"
              rows={3}
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Linked Course (optional)</label>
            <select
              value={courseId}
              onChange={(e) => setCourseId(e.target.value ? Number(e.target.value) : "")}
              className="w-full px-3 py-2 border border-gray-300 rounded-md"
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
            className="bg-green-600 text-white px-6 py-2 rounded-md hover:bg-green-700 disabled:opacity-50"
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
          className="bg-white p-6 rounded-lg shadow-md mb-8 flex gap-2"
        >
          <input
            required
            value={joinCode}
            onChange={(e) => setJoinCode(e.target.value.toUpperCase())}
            placeholder="Enter join code"
            className="flex-1 px-3 py-2 border border-gray-300 rounded-md font-mono uppercase"
          />
          <button
            type="submit"
            disabled={joinMutation.isPending}
            className="bg-purple-600 text-white px-6 py-2 rounded-md hover:bg-purple-700 disabled:opacity-50"
          >
            Join
          </button>
        </form>
      )}

      {message && (
        <p className={`mb-4 text-sm ${message.includes("success") || message.includes("created") ? "text-green-600" : "text-red-600"}`}>
          {message}
        </p>
      )}

      {isLoading ? (
        <p>Loading classrooms...</p>
      ) : !classrooms?.length ? (
        <p className="text-gray-500">
          {isTeacher ? "No classrooms yet. Create one to get started." : "You haven't joined any classrooms yet."}
        </p>
      ) : (
        <div className="space-y-4">
          {classrooms.map((c) => (
            <div key={c.id} className="bg-white p-6 rounded-lg shadow-md">
              <div className="flex justify-between items-start mb-2">
                <h2 className="text-xl font-semibold text-gray-900">{c.name}</h2>
                {isTeacher && (
                  <span className="text-sm font-mono bg-gray-100 px-2 py-1 rounded">Code: {c.joinCode}</span>
                )}
              </div>
              <p className="text-gray-600 mb-2">{c.description || "No description"}</p>
              <p className="text-sm text-gray-500">
                Teacher: {c.teacherName}
                {c.courseTitle && ` · Course: ${c.courseTitle}`}
                · {c.memberCount} member{c.memberCount !== 1 ? "s" : ""}
              </p>
              {c.members.length > 0 && isTeacher && (
                <ul className="mt-4 border-t pt-3 space-y-1">
                  {c.members.map((m) => (
                    <li key={m.userId} className="text-sm text-gray-700">
                      {m.name} ({m.email})
                    </li>
                  ))}
                </ul>
              )}

              {/* Classroom Files Section */}
              <div className="mt-6 border-t pt-4">
                <div className="flex items-center justify-between mb-4">
                  <h3 className="font-semibold text-gray-900 flex items-center gap-2">
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
                        className="cursor-pointer bg-gray-100 hover:bg-gray-200 text-gray-700 px-3 py-1 rounded text-sm font-medium border border-gray-300"
                      >
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
                        className="flex items-center justify-between p-3 bg-gray-50 rounded-lg border border-gray-200 group"
                      >
                        <div className="flex items-center gap-3 overflow-hidden">
                          <div className="p-2 bg-white rounded border border-gray-200 text-gray-400">
                            {file.fileType.toUpperCase()}
                          </div>
                          <div className="overflow-hidden">
                            <p className="text-sm font-medium text-gray-900 truncate">
                              {file.fileName}
                            </p>
                            <p className="text-xs text-gray-500">
                              {(file.fileSize / 1024 / 1024).toFixed(2)} MB
                            </p>
                          </div>
                        </div>
                        <div className="flex gap-2">
                          <a
                            href={getFileUrl(file.fileUrl)}
                            target="_blank"
                            rel="noopener noreferrer"
                            className="p-1 text-gray-500 hover:text-blue-600 transition-colors"
                            title="Download"
                          >
                            <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 16v1a2 2 0 002 2h12a2 2 0 002-2v-1m-4-4l-4 4m0 0l-4-4m4 4V4" />
                            </svg>
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
                              className="p-1 text-gray-500 hover:text-red-600 transition-colors"
                              title="Delete"
                            >
                              <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                              </svg>
                            </button>
                          )}
                        </div>
                      </div>
                    ))}
                  </div>
                ) : (
                  <p className="text-sm text-gray-500 italic">No files shared yet.</p>
                )}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
