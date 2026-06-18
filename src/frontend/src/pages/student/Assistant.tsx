import { useState, useRef, useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { askAssistant } from "../../lib/api";
import { Send, Bot, User, MessageSquare, Sparkles } from "lucide-react";

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
      <style>{`
        @keyframes duo-bot-pulse {
          0%, 100% { transform: scale(1) rotate(0deg); }
          40% { transform: scale(1.15) rotate(-6deg); }
          70% { transform: scale(1.07) rotate(4deg); }
        }
        @keyframes duo-bot-think {
          0%, 100% { transform: translateY(0); }
          30% { transform: translateY(-4px); }
          60% { transform: translateY(-1px); }
        }
        @keyframes duo-dot-bounce {
          0%, 80%, 100% { transform: translateY(0); opacity: 0.5; }
          40% { transform: translateY(-6px); opacity: 1; }
        }
        @keyframes duo-send-fly {
          0%, 100% { transform: translateX(0) translateY(0) rotate(0deg); }
          50% { transform: translateX(3px) translateY(-3px) rotate(-20deg); }
        }
        .duo-bot-avatar { animation: duo-bot-pulse 3s ease-in-out infinite; }
        .duo-thinking .duo-bot-avatar { animation: duo-bot-think 0.8s ease-in-out infinite; }
        .duo-send-btn:hover .duo-send-icon { animation: duo-send-fly 0.4s ease-in-out; }

        /* Typing indicator dots */
        .duo-dot { display: inline-block; width: 7px; height: 7px; border-radius: 50%; background: #a0a0a0; }
        .duo-dot:nth-child(1) { animation: duo-dot-bounce 1.2s ease-in-out 0s infinite; }
        .duo-dot:nth-child(2) { animation: duo-dot-bounce 1.2s ease-in-out 0.2s infinite; }
        .duo-dot:nth-child(3) { animation: duo-dot-bounce 1.2s ease-in-out 0.4s infinite; }

        @keyframes duo-msg-pop {
          0% { opacity: 0; transform: translateY(8px) scale(0.97); }
          100% { opacity: 1; transform: translateY(0) scale(1); }
        }
        .duo-msg { animation: duo-msg-pop 0.25s ease-out forwards; }
      `}</style>

      {/* Header */}
      <div className="mb-4">
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2 flex items-center gap-3">
          <div className="w-10 h-10 bg-[#1CB0F6] rounded-2xl flex items-center justify-center shadow-[0_3px_0_#1899D6]">
            <MessageSquare className="w-5 h-5 text-white" />
          </div>
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

      {/* Chat area */}
      <div className="flex-1 overflow-y-auto duo-card mb-4 space-y-4 p-4">
        {messages.map((msg, i) => (
          <div
            key={i}
            className={`flex duo-msg ${msg.role === "user" ? "justify-end" : "justify-start"}`}
          >
            <div className={`max-w-[85%] flex items-end gap-2 ${msg.role === "user" ? "flex-row-reverse" : "flex-row"}`}>
              {/* Avatar bubble */}
              <div
                className={`w-9 h-9 rounded-2xl flex items-center justify-center flex-shrink-0 ${
                  msg.role === "assistant"
                    ? "bg-[#58CC02] shadow-[0_3px_0_#46A302]"
                    : "bg-[#CE82FF] shadow-[0_3px_0_#A568CC]"
                }`}
              >
                {msg.role === "assistant" ? (
                  <Bot className="w-4 h-4 text-white duo-bot-avatar" />
                ) : (
                  <User className="w-4 h-4 text-white" />
                )}
              </div>

              {/* Bubble */}
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

        {/* Typing indicator */}
        {loading && (
          <div className="flex justify-start">
            <div className="flex items-end gap-2 duo-thinking">
              <div className="w-9 h-9 rounded-2xl bg-[#58CC02] shadow-[0_3px_0_#46A302] flex items-center justify-center flex-shrink-0">
                <Bot className="w-4 h-4 text-white duo-bot-avatar" />
              </div>
              <div className="px-5 py-3.5 rounded-2xl rounded-bl-md bg-neutral-100 shadow-[0_3px_0_#e5e5e5] flex items-center gap-1.5">
                <span className="duo-dot" />
                <span className="duo-dot" />
                <span className="duo-dot" />
              </div>
            </div>
          </div>
        )}
        <div ref={bottomRef} />
      </div>

      {/* Input */}
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
          className="duo-btn3d duo-btn3d-blue !px-5 duo-send-btn"
        >
          <Send className="w-4 h-4 duo-send-icon" />
          Send
        </button>
      </form>
    </div>
  );
}