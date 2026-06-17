import { useState } from "react";
import { Link, useParams } from "react-router-dom";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { fetchCourseById, enrollInCourse, fetchCourseProgress } from "../lib/api";
import { useAuth } from "../context/AuthContext";

const lessonTypeIcon: Record<string, string> = {
  Reading: "📖",
  Video: "🎬",
  Practice: "💻",
  Interactive: "🎮",
  Quiz: "❓",
};

export default function CourseDetail() {
  const { id } = useParams<{ id: string }>();
  const courseId = Number(id);
  const { token, isAuthenticated } = useAuth();
  const queryClient = useQueryClient();
  const [enrollMessage, setEnrollMessage] = useState("");
  const [expandedModule, setExpandedModule] = useState<number | null>(null);

  const { data: course, isLoading, error } = useQuery({
    queryKey: ["course", courseId],
    queryFn: () => fetchCourseById(courseId),
    enabled: !isNaN(courseId),
  });

  const { data: progress } = useQuery({
    queryKey: ["course-progress", courseId],
    queryFn: () => fetchCourseProgress(courseId, token!),
    enabled: isAuthenticated && !!token && !isNaN(courseId),
  });

  const enrollMutation = useMutation({
    mutationFn: () => enrollInCourse(courseId, token!),
    onSuccess: () => {
      setEnrollMessage("Successfully enrolled!");
      queryClient.invalidateQueries({ queryKey: ["course-progress", courseId] });
      queryClient.invalidateQueries({ queryKey: ["progress-summary"] });
    },
    onError: (err: Error) => setEnrollMessage(err.message),
  });

  if (isLoading) {
    return <div className="max-w-4xl mx-auto px-4 py-12 text-slate-400">Loading course...</div>;
  }

  if (error || !course) {
    return (
      <div className="max-w-4xl mx-auto px-4 py-12">
        <p className="text-red-400">Course not found.</p>
        <Link to="/courses" className="text-indigo-400 hover:underline mt-4 inline-block">Back to courses</Link>
      </div>
    );
  }

  const completedSet = new Set(progress?.completedLessonIds ?? []);
  const totalLessons = course.modules.reduce((n, m) => n + m.lessons.length, 0);

  return (
    <div className="max-w-4xl mx-auto px-4 py-10">
      <Link to="/courses" className="text-indigo-400 hover:underline text-sm mb-4 inline-block">
        &larr; All courses
      </Link>

      <div className="rounded-2xl border border-slate-800 bg-slate-900 p-8 mb-8">
        <div className="flex flex-wrap gap-2 mb-3">
          <span className="text-xs px-2.5 py-1 rounded-full bg-indigo-900/50 text-indigo-400">{course.level}</span>
          <span className="text-xs px-2.5 py-1 rounded-full bg-slate-800 text-slate-400">{course.estimatedHours} hours</span>
          <span className="text-xs px-2.5 py-1 rounded-full bg-slate-800 text-slate-400">{totalLessons} lessons</span>
        </div>
        <h1 className="text-3xl font-bold mb-3">{course.title}</h1>
        <p className="text-slate-400 leading-relaxed">{course.description}</p>

        {progress?.isEnrolled && (
          <div className="mt-6">
            <div className="flex justify-between text-sm text-slate-400 mb-2">
              <span>Your progress</span>
              <span>{progress.completionPercentage.toFixed(0)}%</span>
            </div>
            <div className="w-full bg-slate-800 rounded-full h-2.5">
              <div
                className="bg-gradient-to-r from-indigo-500 to-violet-500 h-2.5 rounded-full transition-all"
                style={{ width: `${progress.completionPercentage}%` }}
              />
            </div>
          </div>
        )}

        <div className="mt-6">
          {isAuthenticated ? (
            progress?.isEnrolled ? (
              <span className="text-emerald-400 font-medium text-sm">✓ Enrolled</span>
            ) : (
              <div className="flex items-center gap-4">
                <button
                  onClick={() => enrollMutation.mutate()}
                  disabled={enrollMutation.isPending}
                  className="px-6 py-2.5 rounded-xl bg-indigo-600 hover:bg-indigo-500 disabled:opacity-50 font-medium"
                >
                  {enrollMutation.isPending ? "Enrolling..." : "Enroll for Free"}
                </button>
                {enrollMessage && (
                  <span className={enrollMessage.includes("Success") ? "text-emerald-400 text-sm" : "text-red-400 text-sm"}>
                    {enrollMessage}
                  </span>
                )}
              </div>
            )
          ) : (
            <p className="text-slate-400 text-sm">
              <Link to="/login" className="text-indigo-400 hover:underline">Sign in</Link> to enroll and track progress.
            </p>
          )}
        </div>
      </div>

      <div className="space-y-4">
        <h2 className="text-lg font-semibold text-slate-300">Course Curriculum</h2>
        {course.modules.map((module) => {
          const moduleDone = module.lessons.every((l) => completedSet.has(l.id));
          const isOpen = expandedModule === module.id || expandedModule === null;
          return (
            <div key={module.id} className="rounded-xl border border-slate-800 bg-slate-900 overflow-hidden">
              <button
                onClick={() => setExpandedModule(expandedModule === module.id ? null : module.id)}
                className="w-full flex items-center justify-between p-5 text-left hover:bg-slate-800/50 transition-colors"
              >
                <div>
                  <h3 className="font-semibold">{module.title}</h3>
                  <p className="text-sm text-slate-400 mt-0.5">{module.lessons.length} topics</p>
                </div>
                <div className="flex items-center gap-3">
                  {moduleDone && <span className="text-emerald-400 text-sm">✓ Complete</span>}
                  <span className="text-slate-500">{isOpen ? "▲" : "▼"}</span>
                </div>
              </button>

              {isOpen && (
                <div className="border-t border-slate-800 px-5 pb-4">
                  {module.learningObjectives && (
                    <p className="text-xs text-slate-500 py-3 border-b border-slate-800">
                      Objectives: {module.learningObjectives}
                    </p>
                  )}
                  <ul className="divide-y divide-slate-800/50">
                    {module.lessons.map((lesson) => {
                      const done = completedSet.has(lesson.id);
                      const icon = lessonTypeIcon[lesson.type ?? ""] ?? "📄";
                      return (
                        <li key={lesson.id}>
                          <Link
                            to={`/lessons/${lesson.id}`}
                            className="flex items-center gap-3 py-3 hover:bg-slate-800/30 rounded-lg px-2 -mx-2 transition-colors"
                          >
                            <span className={`w-7 h-7 rounded-full text-xs flex items-center justify-center shrink-0 ${
                              done ? "bg-emerald-900/50 text-emerald-400" : "bg-slate-800 text-slate-400"
                            }`}>
                              {done ? "✓" : icon}
                            </span>
                            <div className="flex-1 min-w-0">
                              <span className={done ? "text-emerald-300" : "text-slate-200"}>{lesson.title}</span>
                              {lesson.durationMinutes ? (
                                <span className="text-xs text-slate-500 ml-2">{lesson.durationMinutes} min</span>
                              ) : null}
                            </div>
                          </Link>
                        </li>
                      );
                    })}
                  </ul>
                </div>
              )}
            </div>
          );
        })}
      </div>
    </div>
  );
}
