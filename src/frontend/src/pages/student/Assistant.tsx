import { useState, useRef, useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { askAssistant } from "../lib/api";

interface Message {
  role: "user" | "assistant";
  content: string;
}

export default function Assistant() {
  const [searchParams] = useSearchParams();
  const lessonContext = searchParams.get("lesson") ?? undefined;
  const [messages, setMessages] = useState<Message[]>([
    { role: "assistant", content: "Hi! I'm your C# tutor. Ask me anything about C# programming." },
  ]);
  const [input, setInput] = useState("");
  const [loading, setLoading] = useState(false);
  const [aiStatus, setAiStatus] = useState<"online" | "offline" | "error">("offline");
  const [statusMessage, setStatusMessage] = useState("");
  const bottomRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    bottomRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);

  const handleSend = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!input.trim() || loading) return;

    const userMessage = input.trim();
    setInput("");
    setMessages((prev) => [...prev, { role: "user", content: userMessage }]);
    setLoading(true);

    try {
      const response = await askAssistant(userMessage, lessonContext);
      setAiStatus(response.usedAiProvider ? "online" : response.error ? "error" : "offline");
      setStatusMessage(
        response.usedAiProvider
          ? "Powered by Google Gemini"
          : response.error ?? "Offline tutor mode — set GEMINI_API_KEY in src/backend/.env"
      );
      setMessages((prev) => [...prev, { role: "assistant", content: response.reply }]);
    } catch {
      setMessages((prev) => [
        ...prev,
        { role: "assistant", content: "Sorry, I couldn't process that request. Please try again." },
      ]);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-3xl mx-auto px-4 py-12 flex flex-col h-[calc(100vh-8rem)]">
      <h1 className="text-3xl font-bold text-gray-900 mb-2">AI Learning Assistant</h1>
      <p className="text-gray-600 mb-2">
        {lessonContext ? `Context: ${lessonContext}` : "Ask questions about C# concepts, syntax, and best practices."}
      </p>
      <p className={`text-xs mb-4 ${
        aiStatus === "online" ? "text-green-600" : aiStatus === "error" ? "text-amber-600" : "text-gray-500"
      }`}>
        {aiStatus === "online" ? "🟢" : aiStatus === "error" ? "🟡" : "⚪"} {statusMessage || "Checking AI status..."}
      </p>

      <div className="flex-1 overflow-y-auto bg-white rounded-lg shadow-md p-4 mb-4 space-y-4">
        {messages.map((msg, i) => (
          <div key={i} className={`flex ${msg.role === "user" ? "justify-end" : "justify-start"}`}>
            <div
              className={`max-w-[80%] px-4 py-2 rounded-lg text-sm whitespace-pre-wrap ${
                msg.role === "user"
                  ? "bg-blue-600 text-white"
                  : "bg-gray-100 text-gray-800"
              }`}
            >
              {msg.content}
            </div>
          </div>
        ))}
        {loading && <p className="text-gray-400 text-sm">Thinking...</p>}
        <div ref={bottomRef} />
      </div>

      <form onSubmit={handleSend} className="flex gap-2">
        <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          placeholder="Ask about C#..."
          className="flex-1 px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
        />
        <button
          type="submit"
          disabled={loading || !input.trim()}
          className="bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 disabled:opacity-50 font-medium"
        >
          Send
        </button>
      </form>
    </div>
  );
}
