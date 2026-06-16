import { useQuery } from '@tanstack/react-query';
import { fetchCourses, type Course } from '../lib/api';

export default function Courses() {
  const { data: courses, isLoading, error } = useQuery({
    queryKey: ['courses'],
    queryFn: fetchCourses,
  });

  if (isLoading) {
    return (
      <div className="max-w-7xl mx-auto px-4 py-12">
        <h1 className="text-4xl font-bold text-gray-900 mb-8">Courses</h1>
        <p>Loading courses...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className="max-w-7xl mx-auto px-4 py-12">
        <h1 className="text-4xl font-bold text-gray-900 mb-8">Courses</h1>
        <p className="text-red-600">Failed to load courses. Please try again later.</p>
      </div>
    );
  }

  return (
    <div className="max-w-7xl mx-auto px-4 py-12">
      <h1 className="text-4xl font-bold text-gray-900 mb-8">Courses</h1>
      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        {courses?.map((course) => (
          <div key={course.id} className="bg-white p-6 rounded-lg shadow-md hover:shadow-lg transition-shadow">
            <h2 className="text-2xl font-semibold mb-2 text-gray-900">{course.title}</h2>
            <p className="text-gray-600">{course.description}</p>
          </div>
        ))}
      </div>
    </div>
  );
}
