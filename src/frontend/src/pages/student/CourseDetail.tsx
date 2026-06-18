import { useState } from "react";
import { Link, useParams } from "react-router-dom";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { fetchCourseById, enrollInCourse, fetchCourseProgress } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { ArrowLeft, BookOpen, Clock, CheckCircle2 } from "lucide-react";

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
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-10 w-32 bg-neutral-200 rounded-xl" />
        <div className="duo-card animate-pulse">
          <div className="h-24" />
        </div>
        <div className="duo-card animate-pulse">
          <div className="h-40" />
        </div>
      </div>
    );
  }

  if (error || !course) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <Link
          to="/courses"
          className="inline-flex items-center gap-2 text-neutral-600 font-bold hover:text-[#58CC02]"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to courses
        </Link>
        <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
          <p className="text-[#FF4B4B] font-black">Course not found.</p>
        </div>
      </div>
    );
  }

  const completedSet = new Set(progress?.completedLessonIds ?? []);
  const totalLessons = course.modules.reduce((n, m) => n + m.lessons.length, 0);

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      {/* Back Button */}
      <Link
        to="/courses"
        className="inline-flex items-center gap-2 text-neutral-600 font-bold hover:text-[#58CC02] text-sm"
      >
        <ArrowLeft className="w-5 h-5" />
        Back to courses
      </Link>

      {/* Course Info Card */}
      <div className="duo-card">
        <div className="flex flex-wrap gap-2 mb-4">
          <span className="px-3 py-1 bg-neutral-100 text-neutral-700 rounded-xl text-xs font-bold">
            {course.level}
          </span>
          <span className="px-3 py-1 bg-neutral-100 text-neutral-700 rounded-xl text-xs font-bold flex items-center gap-1">
            <Clock className="w-4 h-4" />
            {course.estimatedHours} hours
          </span>
          <span className="px-3 py-1 bg-neutral-100 text-neutral-700 rounded-xl text-xs font-bold">
            {totalLessons} lessons
          </span>
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          {course.title}
        </h1>
        <p className="text-neutral-600 font-semibold text-sm leading-relaxed mb-6">
          {course.description}
        </p>

        {/* Progress Bar */}
        {progress?.isEnrolled && (
          <div className="mb-6">
            <div className="flex justify-between text-sm font-bold text-neutral-700 mb-2">
              <span>Your progress</span>
              <span>{progress.completionPercentage.toFixed(0)}%</span>
            </div>
            <div className="w-full bg-neutral-100 rounded-full h-3">
              <div
                className="bg-[#58CC02] h-3 rounded-full transition-all"
                style={{ width: `${progress.completionPercentage}%` }}
              />
            </div>
          </div>
        )}

        {/* Enroll Section */}
        <div>
          {isAuthenticated ? (
            progress?.isEnrolled ? (
              <div className="flex items-center gap-2 text-[#58CC02] font-black">
                <CheckCircle2 className="w-5 h-5" />
                Enrolled
              </div>
            ) : (
              <div className="flex flex-col gap-3">
                <button
                  onClick={() => enrollMutation.mutate()}
                  disabled={enrollMutation.isPending}
                  className="duo-btn duo-btn-primary"
                >
                  {enrollMutation.isPending ? "Enrolling..." : "Enroll for Free"}
                </button>
                {enrollMessage && (
                  <span
                    className={`text-sm font-bold ${
                      enrollMessage.includes("Success") ? "text-[#58CC02]" : "text-[#FF4B4B]"
                    }`}
                  >
                    {enrollMessage}
                  </span>
                )}
              </div>
            )
          ) : (
            <p className="text-neutral-600 font-semibold text-sm">
              <Link
                to="/login"
                className="text-[#58CC02] font-black hover:underline"
              >
                Sign in
              </Link>{" "}
              to enroll and track progress.
            </p>
          )}
        </div>
      </div>

      {/* Course Curriculum */}
      <div className="space-y-4">
        <h2 className="text-lg font-black text-neutral-900">Course Curriculum</h2>
        {course.modules.map((module) => {
          const moduleDone = module.lessons.every((l) => completedSet.has(l.id));
          const isOpen = expandedModule === module.id || expandedModule === null;
          return (
            <div key={module.id} className="duo-card overflow-hidden">
              <button
                onClick={() => setExpandedModule(expandedModule === module.id ? null : module.id)}
                className="w-full flex items-center justify-between p-5 text-left hover:bg-neutral-50 transition-colors"
              >
                <div>
                  <h3 className="font-black text-neutral-900">{module.title}</h3>
                  <p className="text-xs font-bold text-neutral-500 mt-1">
                    {module.lessons.length} topics
                  </p>
                </div>
                <div className="flex items-center gap-3">
                  {moduleDone && (
                    <span className="text-[#58CC02] text-sm font-black flex items-center gap-1">
                      <CheckCircle2 className="w-4 h-4" />
                      Complete
                    </span>
                  )}
                  <span className="text-neutral-400 text-xl font-bold">
                    {isOpen ? "▲" : "▼"}
                  </span>
                </div>
              </button>

              {isOpen && (
                <div className="border-t border-[#e5e5e5] px-5 pb-4">
                  {module.learningObjectives && (
                    <p className="text-xs font-bold text-neutral-500 py-3 border-b border-[#e5e5e5]">
                      Objectives: {module.learningObjectives}
                    </p>
                  )}
                  <ul className="divide-y divide-neutral-100">
                    {module.lessons.map((lesson) => {
                      const done = completedSet.has(lesson.id);
                      const icon = lessonTypeIcon[lesson.type ?? ""] ?? "📄";
                      return (
                        <li key={lesson.id}>
                          <Link
                            to={`/lessons/${lesson.id}`}
                            className="flex items-center gap-3 py-3 hover:bg-neutral-50 rounded-lg px-2 -mx-2 transition-colors"
                          >
                            <span
                              className={`w-8 h-8 rounded-full text-xs flex items-center justify-center shrink-0 font-black ${
                                done
                                  ? "bg-[#58CC02] text-white"
                                  : "bg-neutral-100 text-neutral-500"
                              }`}
                            >
                              {done ? "✓" : icon}
                            </span>
                            <div className="flex-1 min-w-0">
                              <span
                                className={`font-bold ${
                                  done ? "text-[#58CC02]" : "text-neutral-800"
                                }`}
                              >
                                {lesson.title}
                              </span>
                              {lesson.durationMinutes ? (
                                <span className="text-xs font-bold text-neutral-400 ml-2">
                                  {lesson.durationMinutes} min
                                </span>
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
