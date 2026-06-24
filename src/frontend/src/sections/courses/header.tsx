import { Sparkles } from "lucide-react";

export default function CoursesHeader() {
  return (
    <div className="mb-2">
      <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 flex items-center gap-2">
        <Sparkles className="w-7 h-7 text-[#CE82FF]" />
        All learning paths
      </h1>
      <p className="text-neutral-600 font-bold">Choose your path and start learning</p>
    </div>
  );
}
