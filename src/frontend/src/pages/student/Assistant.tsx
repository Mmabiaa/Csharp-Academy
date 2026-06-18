import { useState, useRef, useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { askAssistant } from "../../lib/api";
import { Send, Bot, User, MessageSquare } from "lucide-react";

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

  const statusDot =
    aiStatus === "online" ? "bg-[#58CC02]" : aiStatus === "error" ? "bg-[#FFC800]" : "bg-neutral-400";
  const statusText =
    aiStatus === "online" ? "text-[#46A302]" : aiStatus === "error" ? "text-[#946800]" : "text-neutral-500";

  return (
    <div className="flex flex-col h-[calc(100vh-6rem)] md:h-[calc(100vh-4rem)] pb-24 md:pb-0">
      <div className="mb-4">
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2 flex items-center gap-2">
          <MessageSquare className="w-7 h-7 text-[#1CB0F6]" />
          AI learning assistant
        </h1>
        <p className="text-neutral-600 font-bold mb-1">
          {lessonContext ? `Context: ${lessonContext}` : "Ask questions about C# concepts, syntax, and best practices."}
        </p>
        <p className={`text-xs font-black flex items-center gap-1.5 ${statusText}`}>
          <span className={`w-2 h-2 rounded-full ${statusDot}`} />
          {statusMessage || "Checking AI status..."}
        </p>
      </div>

      <div className="flex-1 overflow-y-auto duo-card mb-4 space-y-4">
        {messages.map((msg, i) => (
          <div key={i} className={`flex ${msg.role === "user" ? "justify-end" : "justify-start"}`}>
            <div
              className={`max-w-[85%] flex items-end gap-2 ${
                msg.role === "user" ? "flex-row-reverse" : "flex-row"
              }`}
            >
              <div
                className={`w-8 h-8 rounded-full flex items-center justify-center flex-shrink-0 ${
                  msg.role === "assistant" ? "bg-[#58CC02] shadow-[0_2px_0_#46A302]" : "bg-[#CE82FF] shadow-[0_2px_0_#A568CC]"
                }`}
              >
                {msg.role === "assistant" ? (
                  <Bot className="w-4 h-4 text-white" />
                ) : (
                  <User className="w-4 h-4 text-white" />
                )}
              </div>
              <div
                className={`px-5 py-3 rounded-2xl text-sm font-bold whitespace-pre-wrap ${
                  msg.role === "user"
                    ? "bg-[#1CB0F6] text-white rounded-br-md shadow-[0_3px_0_#1899D6]"
                    : "bg-neutral-100 text-neutral-800 rounded-bl-md shadow-[0_3px_0_#e5e5e5]"
                }`}
              >
                {msg.content}
              </div>
            </div>
          </div>
        ))}
        {loading && (
          <div className="flex justify-start">
            <div className="flex items-end gap-2">
              <div className="w-8 h-8 rounded-full bg-[#58CC02] shadow-[0_2px_0_#46A302] flex items-center justify-center flex-shrink-0">
                <Bot className="w-4 h-4 text-white" />
              </div>
              <div className="px-5 py-3 rounded-2xl rounded-bl-md bg-neutral-100 text-neutral-500 font-black text-sm">
                Thinking...
              </div>
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
          className="flex-1 px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#1CB0F6] focus:ring-4 focus:ring-[#1CB0F6]/10 transition-all"
        />
        <button
          type="submit"
          disabled={loading || !input.trim()}
          className="duo-btn3d duo-btn3d-blue !px-5"
        >
          <Send className="w-4 h-4" />
          Send
        </button>
      </form>
    </div>
  );
}