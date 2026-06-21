import { useState, useRef, useEffect } from "react";
import { useSearchParams } from "react-router-dom";
import { askAssistant } from "../../lib/api";
import { Send, RotateCcw } from "lucide-react";
import ReactMarkdown from "react-markdown";
import remarkGfm from "remark-gfm";

interface Message {
  role: "user" | "assistant";
  content: string;
}

const OWL_SVG = (
  <svg width="28" height="28" viewBox="0 0 28 28" fill="none" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
    <ellipse cx="14" cy="16" rx="9" ry="10" fill="#58CC02" />
    <ellipse cx="14" cy="14" rx="7" ry="8" fill="#89E219" />
    <circle cx="10.5" cy="13" r="3" fill="white" />
    <circle cx="17.5" cy="13" r="3" fill="white" />
    <circle cx="10.5" cy="13.5" r="1.5" fill="#1A1A1A" />
    <circle cx="17.5" cy="13.5" r="1.5" fill="#1A1A1A" />
    <ellipse cx="14" cy="17" rx="2" ry="1.2" fill="#FFC800" />
    <path d="M8 7 C8 4 10 3 14 3 C18 3 20 4 20 7 L18 9 C18 7 16 6 14 6 C12 6 10 7 10 9 Z" fill="#58CC02" />
    <ellipse cx="9" cy="20" rx="2.5" ry="1.2" fill="#46A302" />
    <ellipse cx="19" cy="20" rx="2.5" ry="1.2" fill="#46A302" />
  </svg>
);

const SUGGESTED_QUESTIONS = [
  "What is a class in C#?",
  "How do loops work?",
  "Explain async/await",
  "What are generics?",
];

export default function Assistant() {
  const [searchParams] = useSearchParams();
  const lessonContext = searchParams.get("lesson") ?? undefined;
  const [messages, setMessages] = useState<Message[]>([]);
  const [input, setInput] = useState("");
  const [loading, setLoading] = useState(false);
  const [aiStatus, setAiStatus] = useState<"online" | "offline" | "error">("offline");
  const [xp, setXp] = useState(0);
  const [streak, setStreak] = useState(0);
  const bottomRef = useRef<HTMLDivElement>(null);
  const inputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    bottomRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);

  const sendMessage = async (text: string) => {
    if (!text.trim() || loading) return;
    setInput("");
    setMessages((prev) => [...prev, { role: "user", content: text.trim() }]);
    setLoading(true);

    try {
      const response = await askAssistant(text.trim(), lessonContext);
      setAiStatus(response.usedAiProvider ? "online" : response.error ? "error" : "offline");
      setMessages((prev) => [...prev, { role: "assistant", content: response.reply }]);
      setXp((prev) => prev + 10);
      setStreak((prev) => prev + 1);
    } catch {
      setMessages((prev) => [
        ...prev,
        { role: "assistant", content: "Oops! Something went wrong. Try again?" },
      ]);
    } finally {
      setLoading(false);
      inputRef.current?.focus();
    }
  };

  const handleSend = (e: React.FormEvent) => {
    e.preventDefault();
    sendMessage(input);
  };

  const handleClear = () => {
    setMessages([]);
    setStreak(0);
    inputRef.current?.focus();
  };

  const isEmpty = messages.length === 0;

  return (
    <>
      <style>{`
        .duo-ai-wrap {
          display: flex;
          flex-direction: column;
          height: calc(100vh - 6rem);
          padding-bottom: 6rem;
          font-family: "DIN Round Pro", "Nunito", "Trebuchet MS", system-ui, sans-serif;
          max-width: 780px;
          margin: 0 auto;
          width: 100%;
        }
        @media (min-width: 768px) {
          .duo-ai-wrap { height: calc(100vh - 4rem); padding-bottom: 0; }
        }

        /* ── Top bar ── */
        .duo-topbar {
          display: flex;
          align-items: center;
          justify-content: space-between;
          padding: 0 0 16px;
          flex-shrink: 0;
        }
        .duo-topbar-title {
          display: flex;
          align-items: center;
          gap: 10px;
        }
        .duo-owl-badge {
          width: 44px; height: 44px;
          background: #58CC02;
          border-radius: 16px;
          box-shadow: 0 4px 0 #46A302;
          display: flex;
          align-items: center;
          justify-content: center;
          flex-shrink: 0;
        }
        .duo-title-text h1 {
          font-size: 22px;
          font-weight: 800;
          color: #3C3C3C;
          line-height: 1.1;
          margin: 0;
          letter-spacing: -0.3px;
        }
        .duo-title-text p {
          font-size: 13px;
          font-weight: 700;
          color: #AFAFAF;
          margin: 0;
          text-transform: uppercase;
          letter-spacing: 0.5px;
        }
        .duo-stats {
          display: flex;
          align-items: center;
          gap: 10px;
        }
        .duo-stat-pill {
          display: flex;
          align-items: center;
          gap: 5px;
          padding: 6px 12px;
          border-radius: 100px;
          font-size: 13px;
          font-weight: 800;
          border: 2px solid;
        }
        .duo-stat-xp {
          background: #FFF8E0;
          color: #C47500;
          border-color: #FFC800;
        }
        .duo-stat-streak {
          background: #FFF0E8;
          color: #CC5500;
          border-color: #FF9600;
        }
        .duo-stat-live {
          background: #E8FBD8;
          color: #4A9B00;
          border-color: #58CC02;
        }

        /* ── Chat scroll area ── */
        .duo-chat-area {
          flex: 1;
          overflow-y: auto;
          padding: 8px 4px;
          scroll-behavior: smooth;
        }
        .duo-chat-area::-webkit-scrollbar { width: 6px; }
        .duo-chat-area::-webkit-scrollbar-thumb {
          background: #E5E5E5;
          border-radius: 99px;
        }

        /* ── Empty state ── */
        .duo-empty {
          display: flex;
          flex-direction: column;
          align-items: center;
          justify-content: center;
          height: 100%;
          text-align: center;
          padding: 24px;
          gap: 20px;
        }
        .duo-empty-owl {
          width: 80px; height: 80px;
          background: linear-gradient(145deg, #89E219, #58CC02);
          border-radius: 28px;
          box-shadow: 0 6px 0 #46A302;
          display: flex;
          align-items: center;
          justify-content: center;
          animation: duo-owl-idle 3s ease-in-out infinite;
        }
        @keyframes duo-owl-idle {
          0%, 100% { transform: translateY(0) rotate(-2deg); }
          50% { transform: translateY(-6px) rotate(2deg); }
        }
        .duo-empty h2 {
          font-size: 22px;
          font-weight: 800;
          color: #3C3C3C;
          margin: 0;
        }
        .duo-empty p {
          font-size: 15px;
          font-weight: 600;
          color: #AFAFAF;
          margin: 0;
          max-width: 280px;
          line-height: 1.5;
        }
        .duo-suggest-grid {
          display: grid;
          grid-template-columns: 1fr 1fr;
          gap: 8px;
          width: 100%;
          max-width: 400px;
        }
        .duo-suggest-btn {
          background: white;
          border: 2px solid #E5E5E5;
          border-radius: 16px;
          padding: 10px 14px;
          font-size: 13px;
          font-weight: 700;
          color: #3C3C3C;
          cursor: pointer;
          text-align: left;
          transition: all 0.15s;
          line-height: 1.3;
          box-shadow: 0 3px 0 #E5E5E5;
        }
        .duo-suggest-btn:hover {
          border-color: #1CB0F6;
          color: #1CB0F6;
          box-shadow: 0 3px 0 #1899D6;
          transform: translateY(-1px);
        }
        .duo-suggest-btn:active {
          transform: translateY(2px);
          box-shadow: none;
        }

        /* ── Messages ── */
        .duo-msg-row {
          display: flex;
          margin-bottom: 12px;
          animation: duo-pop-in 0.2s cubic-bezier(0.34, 1.56, 0.64, 1) forwards;
          opacity: 0;
        }
        @keyframes duo-pop-in {
          from { opacity: 0; transform: translateY(10px) scale(0.96); }
          to { opacity: 1; transform: translateY(0) scale(1); }
        }
        .duo-msg-row.user { justify-content: flex-end; }
        .duo-msg-row.assistant { justify-content: flex-start; }

        .duo-avatar {
          width: 36px; height: 36px;
          border-radius: 12px;
          display: flex;
          align-items: center;
          justify-content: center;
          flex-shrink: 0;
          margin-top: 2px;
        }
        .duo-avatar.owl {
          background: #58CC02;
          box-shadow: 0 3px 0 #46A302;
        }
        .duo-avatar.user-av {
          background: #CE82FF;
          box-shadow: 0 3px 0 #A568CC;
          font-size: 15px;
          font-weight: 800;
          color: white;
        }

        .duo-bubble {
          max-width: 78%;
          padding: 12px 16px;
          font-size: 15px;
          font-weight: 600;
          line-height: 1.55;
          word-break: break-word;
        }
        .duo-bubble.owl-bubble {
          background: white;
          border: 2px solid #E5E5E5;
          border-radius: 4px 20px 20px 20px;
          color: #3C3C3C;
          box-shadow: 0 3px 0 #E5E5E5;
          margin-left: 8px;
        }
        .duo-bubble.user-bubble {
          background: #1CB0F6;
          border: 2px solid #1899D6;
          border-radius: 20px 4px 20px 20px;
          color: white;
          box-shadow: 0 3px 0 #1899D6;
          margin-right: 8px;
          white-space: pre-wrap;
        }

        /* XP flash on message receive */
        .duo-xp-toast {
          position: fixed;
          top: 80px;
          right: 24px;
          background: #FFC800;
          color: #7A5000;
          font-size: 14px;
          font-weight: 800;
          padding: 8px 16px;
          border-radius: 100px;
          box-shadow: 0 4px 0 #D4A000;
          animation: duo-xp-float 1.5s ease-out forwards;
          pointer-events: none;
          z-index: 99;
        }
        @keyframes duo-xp-float {
          0% { opacity: 1; transform: translateY(0) scale(1); }
          60% { opacity: 1; transform: translateY(-20px) scale(1.1); }
          100% { opacity: 0; transform: translateY(-40px) scale(0.9); }
        }

        /* ── Typing indicator ── */
        .duo-typing {
          display: flex;
          align-items: flex-start;
          gap: 8px;
          margin-bottom: 12px;
        }
        .duo-typing-dots {
          background: white;
          border: 2px solid #E5E5E5;
          border-radius: 4px 20px 20px 20px;
          box-shadow: 0 3px 0 #E5E5E5;
          padding: 14px 20px;
          display: flex;
          align-items: center;
          gap: 5px;
        }
        .duo-dot {
          width: 7px; height: 7px;
          border-radius: 50%;
          background: #AFAFAF;
          animation: duo-bounce 1.2s ease-in-out infinite;
        }
        .duo-dot:nth-child(2) { animation-delay: 0.15s; }
        .duo-dot:nth-child(3) { animation-delay: 0.3s; }
        @keyframes duo-bounce {
          0%, 60%, 100% { transform: translateY(0); background: #AFAFAF; }
          30% { transform: translateY(-6px); background: #58CC02; }
        }

        /* ── Input bar ── */
        .duo-input-bar {
          display: flex;
          gap: 10px;
          align-items: center;
          flex-shrink: 0;
          padding-top: 12px;
          border-top: 2px solid #F0F0F0;
        }
        .duo-input {
          flex: 1;
          padding: 14px 18px;
          font-size: 15px;
          font-weight: 600;
          font-family: inherit;
          color: #3C3C3C;
          background: white;
          border: 2px solid #E5E5E5;
          border-radius: 16px;
          outline: none;
          transition: border-color 0.15s, box-shadow 0.15s;
          box-shadow: 0 3px 0 #E5E5E5;
        }
        .duo-input::placeholder { color: #AFAFAF; font-weight: 600; }
        .duo-input:focus {
          border-color: #1CB0F6;
          box-shadow: 0 3px 0 #1899D6, 0 0 0 4px rgba(28,176,246,0.12);
        }
        .duo-send-btn {
          display: flex;
          align-items: center;
          gap: 6px;
          padding: 14px 20px;
          font-size: 15px;
          font-weight: 800;
          font-family: inherit;
          background: #1CB0F6;
          color: white;
          border: 2px solid #1899D6;
          border-radius: 16px;
          box-shadow: 0 4px 0 #1899D6;
          cursor: pointer;
          transition: all 0.1s;
          white-space: nowrap;
          letter-spacing: 0.2px;
        }
        .duo-send-btn:hover:not(:disabled) {
          background: #0EA5E9;
          transform: translateY(-1px);
          box-shadow: 0 5px 0 #1899D6;
        }
        .duo-send-btn:active:not(:disabled) {
          transform: translateY(3px);
          box-shadow: 0 1px 0 #1899D6;
        }
        .duo-send-btn:disabled {
          background: #E5E5E5;
          border-color: #CCCCCC;
          box-shadow: 0 4px 0 #CCCCCC;
          color: #AFAFAF;
          cursor: not-allowed;
        }
        .duo-clear-btn {
          display: flex;
          align-items: center;
          justify-content: center;
          width: 44px; height: 44px;
          background: white;
          border: 2px solid #E5E5E5;
          border-radius: 12px;
          box-shadow: 0 3px 0 #E5E5E5;
          cursor: pointer;
          color: #AFAFAF;
          transition: all 0.1s;
          flex-shrink: 0;
        }
        .duo-clear-btn:hover {
          border-color: #FF4B4B;
          color: #FF4B4B;
          box-shadow: 0 3px 0 #CC3333;
          transform: translateY(-1px);
        }
        .duo-clear-btn:active {
          transform: translateY(2px);
          box-shadow: none;
        }

        /* ── Markdown inside owl bubble ── */
        .duo-md p { margin: 0 0 8px; }
        .duo-md p:last-child { margin-bottom: 0; }
        .duo-md ul, .duo-md ol { margin: 6px 0 10px 18px; padding: 0; }
        .duo-md li { margin-bottom: 4px; }
        .duo-md h1, .duo-md h2, .duo-md h3 {
          font-size: 15px;
          font-weight: 800;
          color: #3C3C3C;
          margin: 12px 0 6px;
        }
        .duo-md h1:first-child, .duo-md h2:first-child, .duo-md h3:first-child { margin-top: 0; }
        .duo-md strong { font-weight: 800; color: #3C3C3C; }
        .duo-md em { font-style: italic; }
        .duo-md code {
          font-family: "Fira Code", "Cascadia Code", "Consolas", monospace;
          font-size: 13px;
          background: #F0F4FF;
          color: #4B5FCC;
          border: 1px solid #D8DEFF;
          border-radius: 6px;
          padding: 1px 6px;
        }
        .duo-md pre {
          background: #1E2030;
          border-radius: 12px;
          padding: 14px 16px;
          margin: 10px 0;
          overflow-x: auto;
          border: 2px solid #2D3050;
          box-shadow: 0 3px 0 #12141E;
        }
        .duo-md pre code {
          background: none;
          border: none;
          padding: 0;
          color: #A9B1D6;
          font-size: 13px;
          line-height: 1.65;
        }
        .duo-md blockquote {
          border-left: 3px solid #58CC02;
          margin: 8px 0;
          padding: 6px 12px;
          background: #F4FBE8;
          border-radius: 0 8px 8px 0;
          color: #3C6300;
          font-style: italic;
        }
        .duo-md hr {
          border: none;
          border-top: 2px solid #F0F0F0;
          margin: 12px 0;
        }
        .duo-md a { color: #1CB0F6; text-decoration: underline; }

        /* Lesson context banner */
        .duo-context-banner {
          display: flex;
          align-items: center;
          gap: 8px;
          background: #FFF8E0;
          border: 2px solid #FFC800;
          border-radius: 14px;
          padding: 10px 14px;
          font-size: 13px;
          font-weight: 700;
          color: #7A5000;
          margin-bottom: 12px;
          flex-shrink: 0;
        }
        .duo-context-icon {
          width: 22px; height: 22px;
          background: #FFC800;
          border-radius: 8px;
          display: flex;
          align-items: center;
          justify-content: center;
          font-size: 12px;
          flex-shrink: 0;
        }

        /* Status dot */
        .duo-status-row {
          display: flex;
          align-items: center;
          gap: 5px;
          font-size: 12px;
          font-weight: 700;
        }
        .duo-status-dot {
          width: 7px; height: 7px;
          border-radius: 50%;
        }
      `}</style>

      <div className="duo-ai-wrap">
        {/* Top bar */}
        <div className="duo-topbar">
          <div className="duo-topbar-title">
            <div className="duo-owl-badge">
              {OWL_SVG}
            </div>
            <div className="duo-title-text">
              <h1>AI Tutor</h1>
              <p>C# Learning Assistant</p>
            </div>
          </div>
          <div className="duo-stats">
            {xp > 0 && (
              <div className="duo-stat-pill duo-stat-xp">
                <span>⚡</span>
                <span>{xp} XP</span>
              </div>
            )}
            {streak > 1 && (
              <div className="duo-stat-pill duo-stat-streak">
                <span>🔥</span>
                <span>{streak}</span>
              </div>
            )}
            <div className="duo-stat-live" style={{
              display: "flex",
              alignItems: "center",
              gap: 5,
              padding: "6px 12px",
              borderRadius: 100,
              fontSize: 12,
              fontWeight: 800,
              border: "2px solid",
              borderColor: aiStatus === "online" ? "#58CC02" : aiStatus === "error" ? "#FFC800" : "#E5E5E5",
              background: aiStatus === "online" ? "#E8FBD8" : aiStatus === "error" ? "#FFF8E0" : "#F7F7F7",
              color: aiStatus === "online" ? "#4A9B00" : aiStatus === "error" ? "#C47500" : "#AFAFAF",
            }}>
              <span style={{
                width: 7, height: 7, borderRadius: "50%",
                background: aiStatus === "online" ? "#58CC02" : aiStatus === "error" ? "#FFC800" : "#AFAFAF",
                display: "inline-block",
              }} />
              {aiStatus === "online" ? "AI Online" : aiStatus === "error" ? "Degraded" : "Offline"}
            </div>
          </div>
        </div>

        {/* Lesson context banner */}
        {lessonContext && (
          <div className="duo-context-banner">
            <div className="duo-context-icon">📖</div>
            <span>Currently studying: <strong>{lessonContext}</strong></span>
          </div>
        )}

        {/* Chat area */}
        <div className="duo-chat-area">
          {isEmpty ? (
            <div className="duo-empty">
              <div className="duo-empty-owl">
                <svg width="48" height="48" viewBox="0 0 28 28" fill="none" xmlns="http://www.w3.org/2000/svg" aria-hidden="true">
                  <ellipse cx="14" cy="16" rx="9" ry="10" fill="white" fillOpacity="0.3" />
                  <ellipse cx="14" cy="14" rx="7" ry="8" fill="white" fillOpacity="0.5" />
                  <circle cx="10.5" cy="13" r="3" fill="white" />
                  <circle cx="17.5" cy="13" r="3" fill="white" />
                  <circle cx="10.5" cy="13.5" r="1.5" fill="#3C3C3C" />
                  <circle cx="17.5" cy="13.5" r="1.5" fill="#3C3C3C" />
                  <ellipse cx="14" cy="17" rx="2" ry="1.2" fill="#FFC800" />
                  <path d="M8 7 C8 4 10 3 14 3 C18 3 20 4 20 7 L18 9 C18 7 16 6 14 6 C12 6 10 7 10 9 Z" fill="white" fillOpacity="0.4" />
                  <ellipse cx="9" cy="20" rx="2.5" ry="1.2" fill="white" fillOpacity="0.3" />
                  <ellipse cx="19" cy="20" rx="2.5" ry="1.2" fill="white" fillOpacity="0.3" />
                </svg>
              </div>
              <div>
                <h2>Ask me anything!</h2>
              </div>
              <p>I'm your C# tutor. Ask about variables, classes, loops — whatever you're stuck on.</p>
              <div className="duo-suggest-grid">
                {SUGGESTED_QUESTIONS.map((q) => (
                  <button key={q} className="duo-suggest-btn" onClick={() => sendMessage(q)}>
                    {q}
                  </button>
                ))}
              </div>
            </div>
          ) : (
            <>
              {messages.map((msg, i) => (
                <div key={i} className={`duo-msg-row ${msg.role}`}>
                  {msg.role === "assistant" && (
                    <div className="duo-avatar owl">{OWL_SVG}</div>
                  )}
                  <div className={`duo-bubble ${msg.role === "assistant" ? "owl-bubble" : "user-bubble"}`}>
                    {msg.role === "assistant" ? (
                      <div className="duo-md">
                        <ReactMarkdown remarkPlugins={[remarkGfm]}>
                          {msg.content}
                        </ReactMarkdown>
                      </div>
                    ) : (
                      msg.content
                    )}
                  </div>
                  {msg.role === "user" && (
                    <div className="duo-avatar user-av">U</div>
                  )}
                </div>
              ))}

              {loading && (
                <div className="duo-typing">
                  <div className="duo-avatar owl">{OWL_SVG}</div>
                  <div className="duo-typing-dots">
                    <div className="duo-dot" />
                    <div className="duo-dot" />
                    <div className="duo-dot" />
                  </div>
                </div>
              )}
              <div ref={bottomRef} />
            </>
          )}
        </div>

        {/* Input bar */}
        <div className="duo-input-bar">
          {!isEmpty && (
            <button
              className="duo-clear-btn"
              onClick={handleClear}
              title="Clear conversation"
              aria-label="Clear conversation"
            >
              <RotateCcw size={16} />
            </button>
          )}
          <form onSubmit={handleSend} style={{ display: "flex", flex: 1, gap: 10 }}>
            <input
              ref={inputRef}
              type="text"
              value={input}
              onChange={(e) => setInput(e.target.value)}
              placeholder="Ask about C#..."
              className="duo-input"
              autoComplete="off"
            />
            <button
              type="submit"
              disabled={loading || !input.trim()}
              className="duo-send-btn"
            >
              <Send size={16} />
              Send
            </button>
          </form>
        </div>
      </div>
    </>
  );
}