import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchAllPractices } from "../../lib/api";
import { Code2, Dumbbell, ArrowRight } from "lucide-react";
import { useSound } from "../../context/SoundContext";

const difficultyLabel = (d: number) =>
  d === 1 ? "Beginner" : d === 2 ? "Intermediate" : "Advanced";

const difficultyBadge = (d: number) =>
  d === 1 ? "duo-badge-green" : d === 2 ? "duo-badge-yellow" : "duo-badge-red";

const difficultyDot = (d: number) =>
  d === 1 ? "bg-[#58CC02]" : d === 2 ? "bg-[#FFC800]" : "bg-[#FF4B4B]";

export default function Practices() {
  const { playSound } = useSound();
  const {
    data: exercises,
    isLoading,
    error,
  } = useQuery({
    queryKey: ["practices"],
    queryFn: fetchAllPractices,
  });

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 flex items-center gap-2">
          <Dumbbell className="w-7 h-7 text-[#1CB0F6]" />
          Coding practices
        </h1>
        <p className="text-neutral-600 font-bold">
          Hands-on C# exercises tied to lessons. Write code, run it, and earn XP!
        </p>
      </div>

      {isLoading && (
        <div className="space-y-3">
          {Array.from({ length: 3 }).map((_, i) => (
            <div key={i} className="duo-card animate-pulse">
              <div className="h-20" />
            </div>
          ))}
        </div>
      )}

      {error && (
        <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-10">
          <p className="text-[#CC3A3A] font-black text-lg">
            Couldn't load practices
          </p>
          <p className="text-sm font-bold text-[#CC3A3A]/70 mt-1">
            Check your connection and try refreshing the page.
          </p>
        </div>
      )}

      {!isLoading && exercises && (
        <div className="space-y-3">
          {exercises.length === 0 ? (
            <div className="duo-panel text-center py-12">
              <div className="w-16 h-16 rounded-full bg-[#DDF4FF] flex items-center justify-center mx-auto mb-4">
                <Code2 className="w-8 h-8 text-[#1CB0F6]" />
              </div>
              <p className="font-black text-neutral-900 text-lg">
                No practices yet
              </p>
              <p className="text-sm font-bold text-neutral-500 mt-1">
                New exercises are on the way — check back soon!
              </p>
            </div>
          ) : (
            exercises.map((ex) => (
              <Link
                key={ex.id}
                to={`/lessons/${ex.lessonId}?tab=practice`}
                className="duo-card duo-card-hover block hover:border-[#1CB0F6]"
                onClick={() => playSound("click")}
              >
                <div className="flex items-start gap-4">
                  <div
                    className={`w-11 h-11 rounded-xl flex items-center justify-center shrink-0 ${difficultyDot(
                      ex.difficulty
                    )} shadow-[0_3px_0_rgba(0,0,0,0.15)]`}
                  >
                    <Code2 className="w-5 h-5 text-white" />
                  </div>
                  <div className="flex-1 min-w-0">
                    <div className="flex items-start justify-between gap-3">
                      <p className="font-black text-neutral-900">{ex.title}</p>
                      <span
                        className={`duo-badge ${difficultyBadge(
                          ex.difficulty
                        )} shrink-0`}
                      >
                        {difficultyLabel(ex.difficulty)}
                      </span>
                    </div>
                    <p className="text-sm font-bold text-neutral-500 mt-1">
                      {ex.instructions}
                    </p>
                    <div className="flex items-center justify-between mt-3">
                      <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">
                        {ex.courseTitle} · {ex.lessonTitle}
                      </p>
                      <ArrowRight className="w-4 h-4 text-neutral-300 shrink-0" />
                    </div>
                  </div>
                </div>
              </Link>
            ))
          )}
        </div>
      )}
    </div>
  );
}