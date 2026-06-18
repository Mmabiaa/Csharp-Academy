import { useState, useRef, useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { askAssistant } from "../../lib/api";
import { Send, Bot, User } from "lucide-react";

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
    <div className="flex flex-col h-[calc(100vh-6rem)] md:h-[calc(100vh-4rem)] pb-24 md:pb-0">
      <div className="mb-4">
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          AI Learning Assistant
        </h1>
        <p className="text-neutral-600 font-semibold mb-1">
          {lessonContext ? `Context: ${lessonContext}` : "Ask questions about C# concepts, syntax, and best practices."}
        </p>
        <p className={`text-xs font-bold ${
          aiStatus === "online" ? "text-[#58CC02]" : aiStatus === "error" ? "text-[#FFC800]" : "text-neutral-500"
        }`}>
          {aiStatus === "online" ? "🟢" : aiStatus === "error" ? "🟡" : "⚪"} {statusMessage || "Checking AI status..."}
        </p>
      </div>

      <div className="flex-1 overflow-y-auto duo-card mb-4 space-y-4">
        {messages.map((msg, i) => (
          <div key={i} className={`flex ${msg.role === "user" ? "justify-end" : "justify-start"}`}>
            <div
              className={`max-w-[85%] px-5 py-3 rounded-2xl text-sm font-semibold whitespace-pre-wrap flex items-start gap-2 ${
                msg.role === "user"
                  ? "bg-[#1CB0F6] text-white rounded-tr-sm shadow-[0_4px_0_#0A8CCF]"
                  : "bg-neutral-100 text-neutral-800 rounded-tl-sm shadow-[0_4px_0_#E5E5E5]"
              }`}
            >
              {msg.role === "assistant" && (
                <div className="w-8 h-8 bg-[#58CC02] rounded-full flex items-center justify-center flex-shrink-0">
                  <Bot className="w-4 h-4 text-white" />
                </div>
              )}
              {msg.content}
              {msg.role === "user" && (
                <div className="w-8 h-8 bg-purple-600 rounded-full flex items-center justify-center flex-shrink-0">
                  <User className="w-4 h-4 text-white" />
                </div>
              )}
            </div>
          </div>
        ))}
        {loading && (
          <div className="flex justify-start">
            <div className="max-w-[85%] px-5 py-3 rounded-2xl bg-neutral-100 text-neutral-600 font-bold text-sm rounded-tl-sm">
              Thinking...
            </div>
          </div>
        )}
        <div ref={bottomRef} />
      </div>

      <form onSubmit={handleSend} className="flex gap-3">
        <input
          type="text"
          value={input}
          onChange={(e) => setInput(e.target.value)}
          placeholder="Ask about C#..."
          className="flex-1 px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#1CB0F6] focus:ring-4 focus:ring-[#1CB0F6]/10 transition-all"
        />
        <button
          type="submit"
          disabled={loading || !input.trim()}
          className="duo-btn duo-btn-secondary flex items-center gap-2"
        >
          <Send className="w-4 h-4" />
          Send
        </button>
      </form>
    </div>
  );
}
