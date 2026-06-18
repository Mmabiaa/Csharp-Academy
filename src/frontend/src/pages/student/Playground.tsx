import { useState } from "react";
import { runCode } from "../../lib/api";
import CodeEditor from "../../components/CodeEditor";
import { Play, FlaskConical, AlertTriangle } from "lucide-react";

const defaultCode = `// Try C# expressions and Console.WriteLine
Console.WriteLine("Hello, C# Academy!");

int a = 10;
int b = 20;
a + b`;

export default function Playground() {
  const [code, setCode] = useState(defaultCode);
  const [output, setOutput] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const handleRun = async () => {
    setLoading(true);
    setOutput("");
    setError("");
    try {
      const result = await runCode(code);
      if (result.success) {
        setOutput(result.output);
      } else {
        setError(result.error ?? "Execution failed");
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to run code");
    } finally {
      setLoading(false);
    }
  };

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
            Console.WriteLine()
          </code>{" "}
          for output.
        </p>
      </div>

      <div className="duo-card space-y-4">
        <CodeEditor value={code} onChange={setCode} rows={16} />
        <button
          onClick={handleRun}
          disabled={loading}
          className="duo-btn3d duo-btn3d-green"
        >
          <Play className="w-4 h-4" />
          {loading ? "Running..." : "Run code"}
        </button>

        {(output || error) && (
          <div
            className={`p-4 rounded-2xl font-mono text-sm border-2 ${
              error
                ? "bg-[#FFEFEF] border-[#FF4B4B]/40 text-[#CC3A3A]"
                : "bg-neutral-900 border-neutral-800 text-[#7FE787]"
            }`}
          >
            <p className="font-sans font-black mb-2 flex items-center gap-2 uppercase tracking-wide text-xs">
              {error ? (
                <>
                  <AlertTriangle className="w-4 h-4" />
                  Error
                </>
              ) : (
                "Output"
              )}
            </p>
            <pre className="whitespace-pre-wrap">{error || output}</pre>
          </div>
        )}
      </div>
    </div>
  );
}