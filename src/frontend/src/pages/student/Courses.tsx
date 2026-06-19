import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchCourses } from "../../lib/api";
import { BookOpen, Clock, Sparkles, Code2, Layers, Zap, Globe } from "lucide-react";

const levelBadge: Record<string, string> = {
  Beginner: "duo-badge-green",
  Intermediate: "duo-badge-yellow",
  Advanced: "duo-badge-red",
};

const cardAccent = [
  {
    bg: "bg-[#58CC02]",
    shadow: "shadow-[0_4px_0_#46A302]",
    icon: BookOpen,
    glow: "#58CC02",
  },
  {
    bg: "bg-[#1CB0F6]",
    shadow: "shadow-[0_4px_0_#1899D6]",
    icon: Code2,
    glow: "#1CB0F6",
  },
  {
    bg: "bg-[#CE82FF]",
    shadow: "shadow-[0_4px_0_#A568CC]",
    icon: Layers,
    glow: "#CE82FF",
  },
  {
    bg: "bg-[#FF9600]",
    shadow: "shadow-[0_4px_0_#CC7800]",
    icon: Zap,
    glow: "#FF9600",
  },
  {
    bg: "bg-[#FF4B4B]",
    shadow: "shadow-[0_4px_0_#CC3A3A]",
    icon: Globe,
    glow: "#FF4B4B",
  },
];

export default function Courses() {
  const { data: courses, isLoading } = useQuery({
    queryKey: ["courses"],
    queryFn: fetchCourses,
  });

  return (
    <div className="space-y-8 pb-24 md:pb-0">
      <style>{`
        @keyframes duo-icon-bounce {
          0%, 100% { transform: translateY(0) scale(1); }
          40% { transform: translateY(-5px) scale(1.08); }
          70% { transform: translateY(-2px) scale(1.04); }
        }
        @keyframes duo-icon-spin {
          0% { transform: rotate(0deg) scale(1); }
          50% { transform: rotate(12deg) scale(1.12); }
          100% { transform: rotate(0deg) scale(1); }
        }

        /* Continuous ambient icon animations — always running, hover no longer required */
        .duo-course-icon {
          animation: duo-icon-bounce 2.6s ease-in-out infinite;
        }
        .duo-course-icon-zap {
          animation: duo-icon-spin 2.2s ease-in-out infinite;
        }

        /* Stagger so cards don't all bounce in lockstep */
        .duo-course-card:nth-child(2n) .duo-course-icon,
        .duo-course-card:nth-child(2n) .duo-course-icon-zap { animation-delay: 0.3s; }
        .duo-course-card:nth-child(3n) .duo-course-icon,
        .duo-course-card:nth-child(3n) .duo-course-icon-zap { animation-delay: 0.6s; }
        .duo-course-card:nth-child(4n) .duo-course-icon,
        .duo-course-card:nth-child(4n) .duo-course-icon-zap { animation-delay: 0.9s; }

        @media (prefers-reduced-motion: reduce) {
          .duo-course-icon, .duo-course-icon-zap {
            animation: none;
          }
        }
      `}</style>

      <div className="mb-2">
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 flex items-center gap-2">
          <Sparkles className="w-7 h-7 text-[#CE82FF]" />
          All learning paths
        </h1>
        <p className="text-neutral-600 font-bold">
          Choose your path and start learning
        </p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
        {isLoading ? (
          Array.from({ length: 4 }).map((_, index) => (
            <div key={index} className="duo-card animate-pulse">
              <div className="h-11 w-11 bg-neutral-200 rounded-xl mb-3" />
              <div className="h-7 w-1/2 bg-neutral-200 rounded mb-2" />
              <div className="h-5 w-full bg-neutral-200 rounded mb-1" />
              <div className="h-5 w-2/3 bg-neutral-200 rounded" />
            </div>
          ))
        ) : (
          courses?.map((course, idx) => {
            const accent = cardAccent[idx % cardAccent.length];
            const badgeClass = levelBadge[course.level] ?? "duo-badge-gray";
            const Icon = accent.icon;
            const isZap = accent.icon === Zap;
            return (
              <Link
                key={course.id}
                to={`/courses/${course.id}`}
                className="duo-card duo-card-hover hover:border-[#58CC02] duo-course-card"
              >
                <div className="flex items-center justify-between mb-3">
                  {/* Animated icon bubble — same anatomy as the streak flame */}
                  <div
                    className={`w-12 h-12 rounded-2xl flex items-center justify-center ${accent.bg} ${accent.shadow} transition-transform`}
                  >
                    <Icon
                      className={`w-6 h-6 text-white ${isZap ? "duo-course-icon-zap" : "duo-course-icon"}`}
                    />
                  </div>
                  <span className="duo-badge duo-badge-gray">
                    <Clock className="w-3 h-3" />
                    {course.estimatedHours}h
                  </span>
                </div>
                <h3 className="text-lg font-black text-neutral-900 mb-1">
                  {course.title}
                </h3>
                <p className="text-sm font-bold text-neutral-500 mb-4">
                  {course.description}
                </p>
                <span className={`duo-badge ${badgeClass}`}>{course.level}</span>
              </Link>
            );
          })
        )}
      </div>
    </div>
  );
}