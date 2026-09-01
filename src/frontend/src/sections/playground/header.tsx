import { FlaskConical } from "lucide-react";

export default function PlaygroundHeader() {
  return (
    <div>
      <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 flex items-center gap-2">
        <FlaskConical className="w-7 h-7 text-[#CE82FF]" />
        C# playground
      </h1>
      <p className="text-neutral-600 font-bold">
        Write and run C# code snippets. Use{" "}
        <code className="bg-neutral-100 px-2 py-1 rounded-lg text-sm font-black text-neutral-700">Console.ReadLine()</code>{" "}
        to read user input interactively.
      </p>
    </div>
  );
}
