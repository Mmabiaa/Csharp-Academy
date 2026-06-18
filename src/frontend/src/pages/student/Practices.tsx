import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchAllPractices } from "../../lib/api";

const difficultyLabel = (d: number) =>
  d === 1 ? "Beginner" : d === 2 ? "Intermediate" : "Advanced";

const difficultyColor = (d: number) =>
  d === 1
    ? "bg-[#58CC02] text-white"
    : d === 2
    ? "bg-[#FFC800] text-neutral-900"
    : "bg-[#FF4B4B] text-white";

export default function Practices() {
  const { data: exercises, isLoading, error } = useQuery({
    queryKey: ["practices"],
    queryFn: fetchAllPractices,
  });

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Coding Practices
        </h1>
        <p className="text-neutral-600 font-semibold">
          Hands-on C# exercises tied to lessons. Write code, run it, and earn XP!
        </p>
      </div>

      {isLoading && (
        <div className="space-y-4">
          <div className="duo-card animate-pulse">
            <div className="h-20" />
          </div>
          <div className="duo-card animate-pulse">
            <div className="h-20" />
          </div>
        </div>
      )}
      {error && (
        <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
          <p className="text-[#FF4B4B] font-black">Failed to load practices.</p>
        </div>
      )}

      {!isLoading && exercises && (
        <div className="space-y-4">
          {exercises.length === 0 ? (
            <div className="duo-card">
              <p className="text-neutral-600 font-semibold">
                No practices available yet.
              </p>
            </div>
          ) : (
            exercises.map((ex) => (
              <Link
                key={ex.id}
                to={`/lessons/${ex.lessonId}?tab=practice`}
                className="duo-card block hover:border-[#58CC02] hover:translate-y-[-2px] transition-all"
              >
                <div className="flex justify-between items-start gap-4">
                  <div>
                    <p className="font-black text-neutral-900">{ex.title}</p>
                    <p className="text-sm font-semibold text-neutral-600 mt-1">
                      {ex.instructions}
                    </p>
                    <p className="text-xs font-bold text-neutral-500 mt-2">
                      {ex.courseTitle} · {ex.lessonTitle}
                    </p>
                  </div>
                  <span
                    className={`text-xs font-bold px-3 py-1.5 rounded-full shrink-0 ${difficultyColor(
                      ex.difficulty
                    )}`}
                  >
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
