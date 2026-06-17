import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchAllPractices } from "../../lib/api";

const difficultyLabel = (d: number) => (d === 1 ? "Beginner" : d === 2 ? "Intermediate" : "Advanced");

export default function Practices() {
  const { data: exercises, isLoading, error } = useQuery({
    queryKey: ["practices"],
    queryFn: fetchAllPractices,
  });

  return (
    <div className="max-w-4xl mx-auto px-4 py-12">
      <h1 className="text-3xl font-bold text-gray-900 mb-2">Coding Practices</h1>
      <p className="text-gray-600 mb-8">
        Hands-on C# exercises tied to lessons. Write code, run it, and earn XP when you pass.
      </p>

      {isLoading && <p>Loading practices...</p>}
      {error && <p className="text-red-600">Failed to load practices.</p>}

      {!isLoading && exercises && (
        <div className="space-y-4">
          {exercises.length === 0 ? (
            <p className="text-gray-500">No practices available yet.</p>
          ) : (
            exercises.map((ex) => (
              <Link
                key={ex.id}
                to={`/lessons/${ex.lessonId}?tab=practice`}
                className="block bg-white p-5 rounded-lg shadow-md hover:shadow-lg transition-shadow border-l-4 border-emerald-500"
              >
                <div className="flex justify-between items-start gap-4">
                  <div>
                    <p className="font-semibold text-gray-900">{ex.title}</p>
                    <p className="text-sm text-gray-600 mt-1">{ex.instructions}</p>
                    <p className="text-xs text-gray-500 mt-2">
                      {ex.courseTitle} · {ex.lessonTitle}
                    </p>
                  </div>
                  <span className="text-xs font-medium bg-emerald-100 text-emerald-800 px-2 py-1 rounded shrink-0">
                    {difficultyLabel(ex.difficulty)}
                  </span>
                </div>
              </Link>
            ))
          )}
        </div>
      )}
    </div>
  );
}
