import { useQuery } from '@tanstack/react-query';
import { Link } from 'react-router-dom';
import { fetchCourses } from '../lib/api';

const levelColor: Record<string, string> = {
  Beginner: 'bg-emerald-900/50 text-emerald-400',
  Intermediate: 'bg-amber-900/50 text-amber-400',
  Advanced: 'bg-red-900/50 text-red-400',
};

export default function Courses() {
  const { data: courses, isLoading, error } = useQuery({
    queryKey: ['courses'],
    queryFn: fetchCourses,
  });

  if (isLoading) {
    return (
      <div className="max-w-7xl mx-auto px-4 py-12">
        <p className="text-slate-400">Loading courses...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="max-w-7xl mx-auto px-4 py-12">
        <p className="text-red-400">Failed to load courses. Please try again later.</p>
      </div>
    );
  }

  return (
    <div className="max-w-7xl mx-auto px-4 py-10">
      <div className="mb-10">
        <h1 className="text-3xl font-bold mb-2">Learning Paths</h1>
        <p className="text-slate-400">
          Structured courses with sections, topics, videos, and hands-on practice.
        </p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        {courses?.map((course) => (
          <Link
            key={course.id}
            to={`/courses/${course.id}`}
            className="group rounded-2xl border border-slate-800 bg-slate-900 p-6 hover:border-indigo-700 transition-all hover:-translate-y-0.5"
          >
            <div className="flex items-center gap-2 mb-3">
              <span className={`text-xs px-2.5 py-1 rounded-full font-medium ${levelColor[course.level] ?? 'bg-slate-800 text-slate-400'}`}>
                {course.level}
              </span>
              <span className="text-xs text-slate-500">{course.estimatedHours}h</span>
            </div>
            <h2 className="text-xl font-semibold mb-2 group-hover:text-indigo-300 transition-colors">
              {course.title}
            </h2>
            <p className="text-slate-400 text-sm leading-relaxed line-clamp-3">{course.description}</p>
            <p className="text-xs text-slate-500 mt-4">{course.lessonCount} lessons</p>
          </Link>
        ))}
      </div>
    </div>
  );
}
