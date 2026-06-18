import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchCourses } from "../../lib/api";
import { BookOpen, Clock } from "lucide-react";

export default function Courses() {
  const { data: courses, isLoading } = useQuery({
    queryKey: ["courses"],
    queryFn: fetchCourses,
  });

  return (
    <div className="space-y-8 pb-24 md:pb-0">
      <div className="mb-6">
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1">
          All Learning Paths
        </h1>
        <p className="text-neutral-600 font-semibold">
          Choose your path and start learning
        </p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        {isLoading ? (
          Array.from({ length: 4 }).map((_, index) => (
            <div
              key={index}
              className="duo-card animate-pulse"
            >
              <div className="h-10 w-10 bg-neutral-200 rounded-lg mb-3" />
              <div className="h-7 w-1/2 bg-neutral-200 rounded mb-2" />
              <div className="h-5 w-full bg-neutral-200 rounded mb-1" />
              <div className="h-5 w-2/3 bg-neutral-200 rounded" />
            </div>
          ))
        ) : (
          courses?.map((course) => (
            <Link
              key={course.id}
              to={`/courses/${course.id}`}
              className="duo-card hover:border-[#58CC02] hover:translate-y-[-2px] transition-all"
            >
              <div className="flex items-center justify-between mb-3">
                <div className="w-10 h-10 bg-[#58CC02] rounded-lg flex items-center justify-center">
                  <BookOpen className="w-6 h-6 text-white" />
                </div>
                <div className="flex items-center gap-1 px-3 py-1 bg-neutral-100 rounded-lg">
                  <Clock className="w-4 h-4 text-neutral-500" />
                  <span className="text-xs font-bold text-neutral-700">
                    {course.estimatedHours}h
                  </span>
                </div>
              </div>
              <h3 className="text-lg font-black text-neutral-900 mb-1">
                {course.title}
              </h3>
              <p className="text-sm font-semibold text-neutral-600 mb-3">
                {course.description}
              </p>
              <span className="inline-block px-3 py-1 bg-neutral-100 rounded-lg text-xs font-bold text-neutral-700">
                {course.level}
              </span>
            </Link>
          ))
        )}
      </div>
    </div>
  );
}
