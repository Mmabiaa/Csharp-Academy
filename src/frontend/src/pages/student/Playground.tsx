import { useState } from "react";
import CodeEditor from "../../components/CodeEditor";
import ConsolePanel from "../../components/ConsolePanel";
import { Play, FlaskConical } from "lucide-react";

const defaultCode = `Console.Write("What is your name? ");
string name = Console.ReadLine()!;
Console.WriteLine($"Hello, {name}! Welcome to C# Academy.");`;

export default function Playground() {
  const [code, setCode] = useState(defaultCode);
  const [runTrigger, setRunTrigger] = useState(0);

  const handleRun = () => setRunTrigger((n) => n + 1);

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 flex items-center gap-2">
          <FlaskConical className="w-7 h-7 text-[#CE82FF]" />
          C# playground
        </h1>
        <p className="text-neutral-600 font-bold">
          Write and run C# code snippets. Use{" "}
          <code className="bg-neutral-100 px-2 py-1 rounded-lg text-sm font-black text-neutral-700">
            Console.ReadLine()
          </code>{" "}
          to read user input interactively.
        </p>
      </div>

      <div className="duo-card space-y-4">
        <CodeEditor value={code} onChange={setCode} rows={16} />

        <button
          onClick={handleRun}
          className="duo-btn3d duo-btn3d-green"
          id="playground-run-btn"
        >
          <Play className="w-4 h-4" />
          Run code
        </button>

        <ConsolePanel code={code} runTrigger={runTrigger} />
      </div>
    </div>
  );
}