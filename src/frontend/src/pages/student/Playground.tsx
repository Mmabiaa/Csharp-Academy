import { useState } from "react";
import { runCode } from "../../lib/api";
import CodeEditor from "../../components/CodeEditor";

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
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          C# Playground
        </h1>
        <p className="text-neutral-600 font-semibold">
          Write and run C# code snippets. Use{" "}
          <code className="bg-neutral-100 px-2 py-1 rounded text-sm font-bold">
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
          className="duo-btn duo-btn-primary"
        >
          {loading ? "Running..." : "Run Code"}
        </button>

        {(output || error) && (
          <div
            className={`p-4 rounded-xl font-mono text-sm border ${
              error
                ? "bg-[#FF4B4B]/10 border-[#FF4B4B]/30 text-[#FF4B4B]"
                : "bg-neutral-100 border-[#e5e5e5] text-neutral-800"
            }`}
          >
            <p className="font-sans font-black mb-2">
              {error ? "Error" : "Output"}
            </p>
            <pre className="whitespace-pre-wrap">{error || output}</pre>
          </div>
        )}
      </div>
    </div>
  );
}
