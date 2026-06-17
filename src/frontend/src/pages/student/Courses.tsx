import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchCourses } from "../../lib/api";
import { BookOpen, Clock, ArrowRight } from "lucide-react";

export default function Courses() {
  const { data: courses, isLoading, error } = useQuery({
    queryKey: ["courses"],
    queryFn: fetchCourses,
  });

  if (isLoading) {
    return (
      <div className="space-y-10">
        <div className="space-y-4">
          <div className="h-12 bg-gray-200 rounded w-1/3 animate-pulse" />
          <div className="h-6 bg-gray-200 rounded w-1/2 animate-pulse" />
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {[1, 2, 3, 4, 5, 6].map((i) => (
            <div
              key={i}
              className="card h-72 animate-pulse"
            />
          ))}
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="text-center py-12">
        <div className="card max-w-md mx-auto">
          <p className="text-gray-700 font-medium mb-2">Failed to load courses</p>
          <p className="text-gray-500 text-sm">Please try again later</p>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-10">
      <div className="space-y-4">
        <div className="text-xs uppercase tracking-widest text-gray-500 font-semibold">
          Courses
        </div>
        <h1 className="text-5xl font-serif font-bold tracking-tight text-black">
          Learning Paths
        </h1>
        <p className="text-lg text-gray-600 max-w-2xl">
          Structured courses with sections, lessons, and hands-on practice to master C#.
        </p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
        {courses?.map((course) => {
          return (
            <Link
              key={course.id}
              to={`/courses/${course.id}`}
              className="card card-hover group"
            >
              <div className="flex items-start justify-between mb-4">
                <div className="w-12 h-12 rounded bg-gray-100 flex items-center justify-center">
                  <BookOpen className="w-6 h-6" />
                </div>
                <div className="flex items-center gap-2 text-sm text-gray-500">
                  <Clock className="w-4 h-4" />
                  {course.estimatedHours}h
                </div>
              </div>
              <div className="mb-3">
                <span className="inline-block px-3 py-1 text-xs font-medium uppercase tracking-wider border border-gray-200 rounded-full text-gray-600">
                  {course.level}
                </span>
              </div>
              <h2 className="text-xl font-serif font-semibold mb-3 group-hover:text-gray-700 transition-colors">
                {course.title}
              </h2>
              <p className="text-gray-600 text-sm leading-relaxed line-clamp-3 mb-5">
                {course.description}
              </p>
              <div className="pt-4 border-t border-gray-100 flex items-center justify-between">
                <span className="text-sm text-gray-500">Start Learning</span>
                <div className="w-10 h-10 rounded-full bg-gray-100 flex items-center justify-center group-hover:bg-black group-hover:text-white transition-all">
                  <ArrowRight className="w-5 h-5" />
                </div>
              </div>
            </Link>
          );
        })}
      </div>
    </div>
  );
}
