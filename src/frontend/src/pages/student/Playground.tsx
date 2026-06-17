import { useState } from "react";
import { runCode } from "../../lib/api";

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
    <div className="max-w-4xl mx-auto px-4 py-12">
      <h1 className="text-3xl font-bold text-gray-900 mb-2">C# Playground</h1>
      <p className="text-gray-600 mb-6">
        Write and run C# code snippets. Use <code className="bg-gray-100 px-1 rounded">Console.WriteLine()</code> for output.
      </p>

      <textarea
        value={code}
        onChange={(e) => setCode(e.target.value)}
        rows={12}
        className="w-full font-mono text-sm bg-gray-900 text-green-400 p-4 rounded-lg mb-4 focus:outline-none focus:ring-2 focus:ring-blue-500"
        spellCheck={false}
      />

      <button
        onClick={handleRun}
        disabled={loading}
        className="bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 disabled:opacity-50 font-medium mb-4"
      >
        {loading ? "Running..." : "Run Code"}
      </button>

      {(output || error) && (
        <div className={`p-4 rounded-lg font-mono text-sm ${error ? "bg-red-50 text-red-800 border border-red-200" : "bg-gray-100 text-gray-800"}`}>
          <p className="font-sans font-medium mb-2">{error ? "Error" : "Output"}</p>
          <pre className="whitespace-pre-wrap">{error || output}</pre>
        </div>
      )}
    </div>
  );
}
