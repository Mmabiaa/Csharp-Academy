export default function AssistantStyles() {
  return (
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
  );
}
