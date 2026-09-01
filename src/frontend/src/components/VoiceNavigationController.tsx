import { useEffect, useState, useRef } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { useVoice, NavCommand, PAGE_ALIASES } from "../context/VoiceContext";
import { Mic, MicOff, Volume2, X, Command } from "lucide-react";

function buildConfirmationMessage(cmd: NavCommand): string {
  const name = cmd.displayName;
  switch (cmd.actionType) {
    case "open":
      return `Opening ${name} page.`;
    case "go":
      return `Going to ${name}.`;
    default:
      return `Navigating to ${name} page.`;
  }
}

function VoiceStatusIndicator() {
  const {
    isVoiceRecognitionEnabled,
    isListening,
    isVoiceFeedbackEnabled,
    isSpeakingFeedback,
    lastTranscript,
    lastNavigation,
    toggleListening,
    stopFeedback,
    supported,
  } = useVoice();

  const [showToast, setShowToast] = useState(false);
  const timerRef = useRef<number | null>(null);
  const [toastText, setToastText] = useState<string>("");
  const [toastKind, setToastKind] = useState<"nav" | "transcript" | "error">("transcript");

  const prevNavRef = useRef<NavCommand | null>(null);
  const prevTransRef = useRef<string>("");

  useEffect(() => {
    if (lastNavigation && lastNavigation !== prevNavRef.current) {
      prevNavRef.current = lastNavigation;
      setToastKind("nav");
      if (lastNavigation.path) {
        setToastText(`→ ${lastNavigation.displayName}`);
      } else {
        setToastText(`❓ "${lastNavigation.target}" not recognized`);
        setToastKind("error");
      }
      setShowToast(true);
      if (timerRef.current !== null) window.clearTimeout(timerRef.current);
      timerRef.current = window.setTimeout(() => setShowToast(false), 2800);
    } else if (lastTranscript && lastTranscript !== prevTransRef.current) {
      prevTransRef.current = lastTranscript;
      if (lastNavigation && lastNavigation.raw === lastTranscript) return;
      setToastKind("transcript");
      setToastText(`🎙️ ${lastTranscript}`);
      setShowToast(true);
      if (timerRef.current !== null) window.clearTimeout(timerRef.current);
      timerRef.current = window.setTimeout(() => setShowToast(false), 3200);
    }
  }, [lastTranscript, lastNavigation]);

  useEffect(() => {
    return () => {
      if (timerRef.current !== null) window.clearTimeout(timerRef.current);
    };
  }, []);

  if (!isVoiceRecognitionEnabled) return null;
  if (!supported) return null;

  return (
    <div className="fixed z-[9998] bottom-20 right-4 md:bottom-6 md:right-6 pointer-events-none">
      <div
        className={`pointer-events-auto flex items-center gap-2 px-3 py-2 rounded-2xl border-2 transition-all shadow-lg ${
          isListening
            ? "bg-[#FF4B4B] border-[#CC3A3A] text-white shadow-[0_3px_0_#CC3A3A]"
            : "bg-white border-[#e5e5e5] text-neutral-500 shadow-[0_3px_0_#e5e5e5]"
        }`}
        title={isListening ? "Voice navigation listening (Alt+M to toggle)" : "Voice navigation idle"}
      >
        <button
          onClick={toggleListening}
          className="flex items-center gap-2 font-black text-xs uppercase tracking-wide transition-opacity hover:opacity-80"
        >
          <div className="relative">
            {isListening ? (
              <div className="relative">
                <span className="absolute inset-0 rounded-full bg-white/30 animate-ping" />
                <Mic className="w-4 h-4 relative" />
              </div>
            ) : (
              <MicOff className="w-4 h-4" />
            )}
          </div>
          <span className="hidden sm:inline">{isListening ? "Listening" : "Paused"}</span>
          <span className="inline-flex md:hidden items-center gap-1 px-1.5 py-0.5 rounded-full bg-white/20 text-[10px]">
            <Command className="w-2.5 h-2.5" />M
          </span>
        </button>
        {isSpeakingFeedback && isVoiceFeedbackEnabled && (
          <button
            onClick={stopFeedback}
            className="flex items-center justify-center w-6 h-6 rounded-full bg-white/20 hover:bg-white/30 transition-colors"
            title="Silence voice feedback (Alt+Shift+S)"
          >
            <Volume2 className="w-3.5 h-3.5 animate-pulse" />
          </button>
        )}
      </div>

      <div
        className={`mt-2 transition-all duration-300 ease-out pointer-events-none ${
          showToast
            ? "opacity-100 translate-y-0"
            : "opacity-0 translate-y-2"
        }`}
      >
        <div
          className={`inline-flex items-center gap-2 px-3 py-2 rounded-2xl border-2 shadow-lg max-w-[280px] ${
            toastKind === "nav"
              ? "bg-[#58CC02] border-[#46A302] text-white shadow-[0_3px_0_#46A302]"
              : toastKind === "error"
              ? "bg-white border-[#FF4B4B] text-[#CC3A3A] shadow-[0_3px_0_#FF4B4B]/40"
              : "bg-white border-[#e5e5e5] text-neutral-700 shadow-[0_3px_0_#e5e5e5]"
          }`}
        >
          {toastKind === "nav" ? (
            <div className="w-5 h-5 rounded-full bg-white/25 flex items-center justify-center flex-shrink-0">
              <Command className="w-3 h-3" />
            </div>
          ) : toastKind === "error" ? (
            <X className="w-4 h-4 flex-shrink-0" />
          ) : (
            <Mic className="w-4 h-4 text-[#FF4B4B] flex-shrink-0" />
          )}
          <span className="font-bold text-xs truncate">{toastText}</span>
        </div>
      </div>
    </div>
  );
}

export default function VoiceNavigationController() {
  const { registerNavigationHandler, speakFeedback, isVoiceRecognitionEnabled } = useVoice();
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    if (!isVoiceRecognitionEnabled) return;

    const unregister = registerNavigationHandler((cmd: NavCommand) => {
      if (!cmd.path) {
        speakFeedback(`Sorry, I didn't recognize the page name: ${cmd.displayName}. Try saying navigate to courses, or open profile.`);
        return;
      }

      if (location.pathname === cmd.path) {
        speakFeedback(`You're already on the ${cmd.displayName} page.`);
        return;
      }

      speakFeedback(buildConfirmationMessage(cmd));
      const navPath = cmd.path;
      setTimeout(() => navigate(navPath), 120);
    });

    return unregister;
  }, [registerNavigationHandler, navigate, location.pathname, isVoiceRecognitionEnabled, speakFeedback]);

  useEffect(() => {
    const allKeys = Object.values(PAGE_ALIASES);
    const keys = allKeys.map(p => p.displayName).filter((v, i, a) => a.indexOf(v) === i);
    console.debug(
      `%c🎙️ Voice Nav Ready%c\nTry:%c\n  "navigate to courses"\n  "open profile"\n  "go to playground"\n  "show leaderboard"\n  "go home"\n\nSupported pages: ${keys.join(", ")}\nShortcut: Alt+M = toggle mic, Alt+Shift+S = silence TTS`,
      "color:#58CC02;font-weight:bold;font-size:12px;",
      "color:#4b4b4b;",
      "color:#1CB0F6;font-family:monospace;"
    );
  }, []);

  return <VoiceStatusIndicator />;
}
