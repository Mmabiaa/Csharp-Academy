import { useQuery } from '@tanstack/react-query';
import { Link } from 'react-router-dom';
import { fetchCourses } from '../lib/api';
import { BookOpen, Clock, Star } from 'lucide-react';

const levelConfig: Record<string, { color: string; bg: string; border: string }> = {
  Beginner: {
    color: 'text-emerald-700',
    bg: 'bg-emerald-50',
    border: 'border-emerald-200'
  },
  Intermediate: {
    color: 'text-amber-700',
    bg: 'bg-amber-50',
    border: 'border-amber-200'
  },
  Advanced: {
    color: 'text-red-700',
    bg: 'bg-red-50',
    border: 'border-red-200'
  }
};

const courseGradients = [
  'from-blue-400 to-indigo-500',
  'from-purple-400 to-pink-500',
  'from-emerald-400 to-teal-500',
  'from-amber-400 to-orange-500',
  'from-rose-400 to-red-500'
];

export default function Courses() {
  const { data: courses, isLoading, error } = useQuery({
    queryKey: ['courses'],
    queryFn: fetchCourses,
  });

  if (isLoading) {
    return (
      <div className="max-w-7xl mx-auto">
        <div className="animate-pulse space-y-6">
          <div className="h-10 bg-slate-200 rounded-lg w-1/3" />
          <div className="h-6 bg-slate-200 rounded-lg w-1/2" />
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 mt-10">
            {[1, 2, 3, 4, 5, 6].map((i) => (
              <div key={i} className="h-72 bg-slate-200 rounded-2xl" />
            ))}
          </div>
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="max-w-7xl mx-auto py-12 text-center">
        <div className="bg-red-50 border border-red-200 rounded-2xl p-8 max-w-md mx-auto">
          <p className="text-red-600 font-medium mb-2">Failed to load courses</p>
          <p className="text-red-500 text-sm">Please try again later</p>
        </div>
      </div>
    );
  }

  return (
    <div className="max-w-7xl mx-auto">
      <div className="mb-10">
        <h1 className="text-4xl font-bold text-slate-900 mb-3">
          Learning Paths
        </h1>
        <p className="text-lg text-slate-600 max-w-2xl">
          Structured courses with sections, topics, videos, and hands-on practice to master C# and .NET.
        </p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
        {courses?.map((course, index) => {
          const config = levelConfig[course.level] || levelConfig.Beginner;
          const gradient = courseGradients[index % courseGradients.length];
          
          return (
            <Link
              key={course.id}
              to={`/courses/${course.id}`}
              className="group bg-white rounded-2xl border border-slate-200 shadow-sm hover:shadow-xl hover:-translate-y-2 transition-all duration-300 overflow-hidden"
            >
              <div className={`h-40 bg-gradient-to-br ${gradient} relative`}>
                <div className="absolute inset-0 bg-black/10" />
                <div className="absolute bottom-4 left-4 right-4 flex items-center justify-between">
                  <div className="flex items-center gap-2">
                    <BookOpen className="w-5 h-5 text-white" />
                    <span className="text-white/90 text-sm font-medium">
                      {course.lessonCount || 0} lessons
                    </span>
                  </div>
                  <div className="flex items-center gap-1.5">
                    <Star className="w-4 h-4 fill-yellow-300 text-yellow-300" />
                    <span className="text-white font-semibold text-sm">4.8</span>
                  </div>
                </div>
              </div>

              <div className="p-6">
                <div className="flex items-center gap-3 mb-4">
                  <span className={`px-3 py-1 rounded-full text-xs font-semibold ${config.bg} ${config.color} border ${config.border}`}>
                    {course.level}
                  </span>
                  <span className="flex items-center gap-1 text-slate-500 text-sm">
                    <Clock className="w-4 h-4" />
                    {course.estimatedHours}h
                  </span>
                </div>

                <h2 className="text-xl font-bold text-slate-900 mb-3 group-hover:text-blue-600 transition-colors">
                  {course.title}
                </h2>
                
                <p className="text-slate-600 text-sm leading-relaxed line-clamp-3 mb-5">
                  {course.description}
                </p>

                <div className="flex items-center justify-between pt-4 border-t border-slate-100">
                  <span className="text-sm text-slate-500">Start Learning</span>
                  <div className="w-8 h-8 rounded-full bg-blue-50 flex items-center justify-center group-hover:bg-blue-600 transition-colors">
                    <svg
                      className="w-4 h-4 text-blue-600 group-hover:text-white transition-colors"
                      fill="none"
                      stroke="currentColor"
                      viewBox="0 0 24 24"
                    >
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
                    </svg>
                  </div>
                </div>
              </div>
            </Link>
          );
        })}
      </div>
    </div>
  );
}
