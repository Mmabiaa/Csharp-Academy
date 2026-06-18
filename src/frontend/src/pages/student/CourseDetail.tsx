import { useState } from "react";
import { Link, useParams } from "react-router-dom";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { fetchCourseById, enrollInCourse, fetchCourseProgress } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import {
  ArrowLeft,
  Clock,
  CheckCircle2,
  Lock,
  Star,
  ChevronUp,
  ChevronDown,
} from "lucide-react";

const lessonTypeIcon: Record<string, string> = {
  Reading: "📖",
  Video: "🎬",
  Practice: "💻",
  Interactive: "🎮",
  Quiz: "❓",
};

const levelBadge: Record<string, string> = {
  Beginner: "duo-badge-green",
  Intermediate: "duo-badge-yellow",
  Advanced: "duo-badge-red",
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
        <div className="h-6 w-32 bg-neutral-200 rounded-xl animate-pulse" />
        <div className="duo-card animate-pulse">
          <div className="h-32" />
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
          className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm"
        >
          <ArrowLeft className="w-5 h-5" />
          Back to courses
        </Link>
        <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-12">
          <div className="w-16 h-16 rounded-full bg-[#FFDFE0] flex items-center justify-center mx-auto mb-4">
            <Lock className="w-8 h-8 text-[#FF4B4B]" />
          </div>
          <p className="text-[#CC3A3A] font-black text-lg">Course not found</p>
          <p className="text-sm font-bold text-[#CC3A3A]/70 mt-1">
            It may have moved or no longer exists.
          </p>
        </div>
      </div>
    );
  }

  const completedSet = new Set(progress?.completedLessonIds ?? []);
  const totalLessons = course.modules.reduce((n, m) => n + m.lessons.length, 0);
  const completedLessons = course.modules.reduce(
    (n, m) => n + m.lessons.filter((l) => completedSet.has(l.id)).length,
    0
  );

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      {/* Back Button */}
      <Link
        to="/courses"
        className="inline-flex items-center gap-2 text-neutral-500 font-bold hover:text-[#58CC02] text-sm"
      >
        <ArrowLeft className="w-5 h-5" />
        Back to courses
      </Link>

      {/* Course Info Card */}
      <div className="duo-card">
        <div className="flex flex-wrap gap-2 mb-4">
          <span className={`duo-badge ${levelBadge[course.level] ?? "duo-badge-gray"}`}>
            {course.level}
          </span>
          <span className="duo-badge duo-badge-gray">
            <Clock className="w-3 h-3" />
            {course.estimatedHours} hours
          </span>
          <span className="duo-badge duo-badge-gray">
            <Star className="w-3 h-3" />
            {totalLessons} lessons
          </span>
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          {course.title}
        </h1>
        <p className="text-neutral-600 font-bold text-sm leading-relaxed mb-6">
          {course.description}
        </p>

        {/* Progress Bar */}
        {progress?.isEnrolled && (
          <div className="mb-6">
            <div className="flex justify-between text-sm font-black text-neutral-700 mb-2">
              <span>
                {completedLessons}/{totalLessons} lessons complete
              </span>
              <span className="text-[#46A302]">
                {progress.completionPercentage.toFixed(0)}%
              </span>
            </div>
            <div className="duo-progress-track">
              <div
                className="duo-progress-fill"
                style={{ width: `${progress.completionPercentage}%` }}
              />
            </div>
          </div>
        )}

        {/* Enroll Section */}
        <div>
          {isAuthenticated ? (
            progress?.isEnrolled ? (
              <div className="inline-flex items-center gap-2 text-[#46A302] font-black bg-[#D7FFB8] px-4 py-2 rounded-xl">
                <CheckCircle2 className="w-5 h-5" />
                Enrolled
              </div>
            ) : (
              <div className="flex flex-col gap-3">
                <button
                  onClick={() => enrollMutation.mutate()}
                  disabled={enrollMutation.isPending}
                  className="duo-btn3d duo-btn3d-green w-full sm:w-auto"
                >
                  {enrollMutation.isPending ? "Enrolling..." : "Start course — it's free"}
                </button>
                {enrollMessage && (
                  <span
                    className={`text-sm font-black ${
                      enrollMessage.includes("Success")
                        ? "text-[#46A302]"
                        : "text-[#FF4B4B]"
                    }`}
                  >
                    {enrollMessage}
                  </span>
                )}
              </div>
            )
          ) : (
            <p className="text-neutral-600 font-bold text-sm">
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
        <h2 className="text-lg font-black text-neutral-900">Course curriculum</h2>
        {course.modules.map((module) => {
          const moduleDone = module.lessons.every((l) => completedSet.has(l.id));
          const isOpen = expandedModule === module.id || expandedModule === null;
          return (
            <div key={module.id} className="duo-card !p-0 overflow-hidden">
              <button
                onClick={() =>
                  setExpandedModule(expandedModule === module.id ? null : module.id)
                }
                className="w-full flex items-center justify-between p-5 text-left hover:bg-neutral-50 transition-colors"
              >
                <div className="flex items-center gap-3 min-w-0">
                  <div
                    className={`w-10 h-10 rounded-full flex items-center justify-center shrink-0 font-black text-sm ${
                      moduleDone
                        ? "bg-[#FFC800] text-[#946800] shadow-[0_3px_0_#E6B400]"
                        : "bg-neutral-100 text-neutral-400 border-2 border-neutral-200"
                    }`}
                  >
                    {moduleDone ? <CheckCircle2 className="w-5 h-5" /> : <Star className="w-5 h-5" />}
                  </div>
                  <div className="min-w-0">
                    <h3 className="font-black text-neutral-900 truncate">{module.title}</h3>
                    <p className="text-xs font-bold text-neutral-400 mt-0.5">
                      {module.lessons.length} topics
                    </p>
                  </div>
                </div>
                <div className="flex items-center gap-3 shrink-0">
                  {moduleDone && (
                    <span className="duo-badge duo-badge-green hidden sm:inline-flex">
                      Complete
                    </span>
                  )}
                  {isOpen ? (
                    <ChevronUp className="w-5 h-5 text-neutral-400" />
                  ) : (
                    <ChevronDown className="w-5 h-5 text-neutral-400" />
                  )}
                </div>
              </button>

              {isOpen && (
                <div className="border-t-2 border-[#e5e5e5] px-5 pb-4">
                  {module.learningObjectives && (
                    <p className="text-xs font-bold text-neutral-400 py-3 border-b border-[#e5e5e5]">
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
                            className="flex items-center gap-3 py-3 hover:bg-neutral-50 rounded-xl px-2 -mx-2 transition-colors"
                          >
                            <span
                              className={`duo-node ${done ? "duo-node-done" : "duo-node-todo"} !w-9 !h-9 !text-base`}
                            >
                              {done ? <CheckCircle2 className="w-4 h-4" /> : icon}
                            </span>
                            <div className="flex-1 min-w-0">
                              <span
                                className={`font-bold ${
                                  done ? "text-[#46A302]" : "text-neutral-800"
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