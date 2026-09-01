import { Link } from "react-router-dom";
import { Clock, BookOpen, Code2, Layers, Zap, Globe } from "lucide-react";

const levelBadge: Record<string, string> = {
  Beginner: "duo-badge-green",
  Intermediate: "duo-badge-yellow",
  Advanced: "duo-badge-red",
};

const cardAccent = [
  { bg: "bg-[#58CC02]", shadow: "shadow-[0_4px_0_#46A302]", icon: BookOpen },
  { bg: "bg-[#1CB0F6]", shadow: "shadow-[0_4px_0_#1899D6]", icon: Code2 },
  { bg: "bg-[#CE82FF]", shadow: "shadow-[0_3px_0_#A568CC]", icon: Layers },
  { bg: "bg-[#FF9600]", shadow: "shadow-[0_4px_0_#CC7800]", icon: Zap },
  { bg: "bg-[#FF4B4B]", shadow: "shadow-[0_4px_0_#CC3A3A]", icon: Globe },
];

interface Course {
  id: number;
  title: string;
  description: string;
  estimatedHours: number;
  level: string;
}

interface CourseGridProps {
  courses?: Course[];
  isLoading: boolean;
}

export default function CourseGrid({ courses, isLoading }: CourseGridProps) {
  return (
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
                <div className={`w-12 h-12 rounded-2xl flex items-center justify-center ${accent.bg} ${accent.shadow} transition-transform`}>
                  <Icon className={`w-6 h-6 text-white ${isZap ? "duo-course-icon-zap" : "duo-course-icon"}`} />
                </div>
                <span className="duo-badge duo-badge-gray">
                  <Clock className="w-3 h-3" />
                  {course.estimatedHours}h
                </span>
              </div>
              <h3 className="text-lg font-black text-neutral-900 mb-1">{course.title}</h3>
              <p className="text-sm font-bold text-neutral-500 mb-4">{course.description}</p>
              <span className={`duo-badge ${badgeClass}`}>{course.level}</span>
            </Link>
          );
        })
      )}
    </div>
  );
}
