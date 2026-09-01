import React, { createContext, useContext, useState, useEffect, useRef, useCallback, useMemo } from "react";
import {
  setVoiceRecognitionEnabled as apiSetVoiceRecognitionEnabled,
  setVoiceFeedbackEnabled as apiSetVoiceFeedbackEnabled,
} from "../lib/api";
import { useAuth } from "./AuthContext";
import { getPreferredVoice, subscribeToPreferredVoice } from "../lib/voicePreference";

export interface NavCommand {
  raw: string;
  target: string;
  path: string | null;
  displayName: string;
  actionType: "navigate" | "open" | "go" | "scroll";
}

interface VoiceContextType {
  isVoiceRecognitionEnabled: boolean;
  setVoiceRecognitionEnabled: (enabled: boolean) => Promise<void>;
  isVoiceFeedbackEnabled: boolean;
  setVoiceFeedbackEnabled: (enabled: boolean) => Promise<void>;
  isListening: boolean;
  startListening: () => void;
  stopListening: () => void;
  toggleListening: () => void;
  supported: boolean;
  lastTranscript: string;
  lastNavigation: NavCommand | null;
  registerWakeHandler: (handler: (transcript: string) => void) => () => void;
  registerNavigationHandler: (handler: (cmd: NavCommand) => void) => () => void;
  speakFeedback: (text: string, opts?: { priority?: boolean }) => void;
  stopFeedback: () => void;
  isSpeakingFeedback: boolean;
}

const VoiceContext = createContext<VoiceContextType | undefined>(undefined);

const WAKE_WORDS = ["hello", "hi", "c# academy", "c sharp academy", "i need help", "need help", "i'm stuck", "im stuck", "okay academy", "ok academy"];

const NAV_PHRASES = [
  { re: /\b(go to|navigate to|open|show|take me to|switch to|display|go)\s+(?:the\s+)?(.+?)(?:\s+page|\s+screen|\s+section)?\s*$/i, group: 2 },
  { re: /\b(home|dashboard|courses?|practices?|challenges?|playground|assistant|leaderboard|classrooms?|progress|profile|assignments?|analytics|login|register|teacher|admin)\b/i, group: 1 },
  { re: /^(courses?|practices?|challenges?|playground|assistant|leaderboard|classrooms?|progress|profile|assignments?|analytics|teacher|admin)$/i, group: 1 },
];

const ACTION_WORDS = ["navigate to", "go to", "open", "show", "take me to", "switch to", "display", "go "];

export const PAGE_ALIASES: Record<string, { path: string; display: string }> = {
  home: { path: "/", display: "Home" },
  dashboard: { path: "/", display: "Home" },
  learn: { path: "/", display: "Home" },
  main: { path: "/", display: "Home" },
  landing: { path: "/", display: "Home" },
  courses: { path: "/courses", display: "Courses" },
  course: { path: "/courses", display: "Courses" },
  catalog: { path: "/courses", display: "Courses" },
  lessons: { path: "/courses", display: "Courses" },
  curriculum: { path: "/courses", display: "Courses" },
  practices: { path: "/practices", display: "Practice" },
  practice: { path: "/practices", display: "Practice" },
  exercises: { path: "/practices", display: "Practice" },
  coding: { path: "/practices", display: "Practice" },
  challenges: { path: "/challenges", display: "Challenges" },
  challenge: { path: "/challenges", display: "Challenges" },
  puzzles: { path: "/challenges", display: "Challenges" },
  playground: { path: "/playground", display: "Code Playground" },
  editor: { path: "/playground", display: "Code Playground" },
  code: { path: "/playground", display: "Code Playground" },
  repl: { path: "/playground", display: "Code Playground" },
  assistant: { path: "/assistant", display: "AI Assistant" },
  ai: { path: "/assistant", display: "AI Assistant" },
  tutor: { path: "/assistant", display: "AI Assistant" },
  chat: { path: "/assistant", display: "AI Assistant" },
  help: { path: "/assistant", display: "AI Assistant" },
  leaderboard: { path: "/leaderboard", display: "Leaderboard" },
  rankings: { path: "/leaderboard", display: "Leaderboard" },
  ranks: { path: "/leaderboard", display: "Leaderboard" },
  scoreboard: { path: "/leaderboard", display: "Leaderboard" },
  classrooms: { path: "/classrooms", display: "Classrooms" },
  classroom: { path: "/classrooms", display: "Classrooms" },
  classes: { path: "/classrooms", display: "Classrooms" },
  groups: { path: "/classrooms", display: "Classrooms" },
  progress: { path: "/progress", display: "Progress Dashboard" },
  stats: { path: "/progress", display: "Progress Dashboard" },
  "progress dashboard": { path: "/progress", display: "Progress Dashboard" },
  statistics: { path: "/progress", display: "Progress Dashboard" },
  profile: { path: "/profile", display: "Profile" },
  me: { path: "/profile", display: "Profile" },
  "my profile": { path: "/profile", display: "Profile" },
  account: { path: "/profile", display: "Profile" },
  settings: { path: "/profile", display: "Profile" },
  assignments: { path: "/assignments", display: "Assignments" },
  assignment: { path: "/assignments", display: "Assignments" },
  homework: { path: "/assignments", display: "Assignments" },
  tasks: { path: "/assignments", display: "Assignments" },
  analytics: { path: "/analytics", display: "Analytics" },
  metrics: { path: "/analytics", display: "Analytics" },
  reports: { path: "/analytics", display: "Analytics" },
  login: { path: "/login", display: "Login" },
  "sign in": { path: "/login", display: "Login" },
  signin: { path: "/login", display: "Login" },
  register: { path: "/register", display: "Register" },
  "sign up": { path: "/register", display: "Register" },
  signup: { path: "/register", display: "Register" },
  teacher: { path: "/teacher", display: "Teacher Portal" },
  "teacher portal": { path: "/teacher", display: "Teacher Portal" },
  educator: { path: "/teacher", display: "Teacher Portal" },
  admin: { path: "/admin", display: "Admin Dashboard" },
  "admin panel": { path: "/admin", display: "Admin Dashboard" },
  manage: { path: "/admin", display: "Admin Dashboard" },
  backend: { path: "/admin", display: "Admin Dashboard" },
};

function matchWakeWord(text: string): boolean {
  const lower = text.toLowerCase().trim();
  if (!lower) return false;
  return WAKE_WORDS.some((w) => lower.includes(w));
}

function extractNavigationTarget(text: string): { target: string; actionType: NavCommand["actionType"] } | null {
  const lower = text.toLowerCase().trim();
  if (!lower) return null;

  let actionType: NavCommand["actionType"] = "navigate";
  if (/\bopen\b/.test(lower)) actionType = "open";
  else if (/\bgo(?:\s+to)?\b/.test(lower)) actionType = "go";
  else if (/\bscroll\b/.test(lower)) actionType = "scroll";

  for (const rule of NAV_PHRASES) {
    const m = lower.match(rule.re);
    if (m && m[rule.group]) {
      const target = m[rule.group].trim().replace(/[?.!,]/g, "");
      return { target, actionType };
    }
  }

  const hasAction = ACTION_WORDS.some((a) => lower.includes(a));
  if (hasAction) {
    for (const alias of Object.keys(PAGE_ALIASES)) {
      if (lower.includes(alias)) {
        return { target: alias, actionType };
      }
    }
  }

  return null;
}

function resolveNavigation(target: string): { path: string | null; displayName: string } {
  const cleaned = target.toLowerCase().trim();
  const exact = PAGE_ALIASES[cleaned];
  if (exact) return { path: exact.path, displayName: exact.display };

  for (const [alias, info] of Object.entries(PAGE_ALIASES)) {
    if (cleaned.includes(alias) || alias.includes(cleaned)) {
      return { path: info.path, displayName: info.display };
    }
  }

  return { path: null, displayName: target };
}

export function parseNavigationCommand(transcript: string): NavCommand | null {
  const extracted = extractNavigationTarget(transcript);
  if (!extracted) return null;

  const resolved = resolveNavigation(extracted.target);
  return {
    raw: transcript,
    target: extracted.target,
    path: resolved.path,
    displayName: resolved.displayName,
    actionType: extracted.actionType,
  };
}

declare global {
  interface Window {
    SpeechRecognition?: new () => SpeechRecognition;
    webkitSpeechRecognition?: new () => SpeechRecognition;
  }
}

interface SpeechRecognition extends EventTarget {
  continuous: boolean;
  interimResults: boolean;
  lang: string;
  start(): void;
  stop(): void;
  abort(): void;
  onresult: ((ev: SpeechRecognitionEvent) => void) | null;
  onerror: ((ev: SpeechRecognitionErrorEvent) => void) | null;
  onend: (() => void) | null;
  onstart: (() => void) | null;
}

interface SpeechRecognitionEvent extends Event {
  results: SpeechRecognitionResultList;
}

interface SpeechRecognitionErrorEvent extends Event {
  error: string;
  message?: string;
}

const VOICE_PREFERENCE_KEY = "duo-voice-recognition-enabled";
const VOICE_FEEDBACK_KEY = "duo-voice-feedback-enabled";

export function VoiceProvider({ children }: { children: React.ReactNode }) {
  const { user, token, isAuthenticated, updateUserSettings } = useAuth();

  const [isVoiceRecognitionEnabled, setIsVoiceRecognitionEnabledState] = useState<boolean>(() => {
    if (typeof window === "undefined") return false;
    const saved = window.localStorage.getItem(VOICE_PREFERENCE_KEY);
    if (saved !== null) return saved === "1";
    return user?.voiceRecognitionEnabled ?? false;
  });

  const [isVoiceFeedbackEnabled, setIsVoiceFeedbackEnabledState] = useState<boolean>(() => {
    if (typeof window === "undefined") return true;
    const saved = window.localStorage.getItem(VOICE_FEEDBACK_KEY);
    if (saved !== null) return saved === "1";
    return user?.voiceFeedbackEnabled ?? true;
  });

  useEffect(() => {
    if (typeof user?.voiceRecognitionEnabled === "boolean") {
      setIsVoiceRecognitionEnabledState(user.voiceRecognitionEnabled);
    }
  }, [user?.voiceRecognitionEnabled]);

  useEffect(() => {
    if (typeof user?.voiceFeedbackEnabled === "boolean") {
      setIsVoiceFeedbackEnabledState(user.voiceFeedbackEnabled);
    }
  }, [user?.voiceFeedbackEnabled]);

  const supported = useMemo<boolean>(() => {
    if (typeof window === "undefined") return false;
    return !!(window.SpeechRecognition || window.webkitSpeechRecognition);
  }, []);

  const ttsSupported = useMemo<boolean>(() => {
    if (typeof window === "undefined") return false;
    return "speechSynthesis" in window;
  }, []);

  const [isListening, setIsListening] = useState(false);
  const [lastTranscript, setLastTranscript] = useState("");
  const [lastNavigation, setLastNavigation] = useState<NavCommand | null>(null);
  const [isSpeakingFeedback, setIsSpeakingFeedback] = useState(false);
  const recognitionRef = useRef<SpeechRecognition | null>(null);
  const wakeHandlersRef = useRef<Array<(transcript: string) => void>>([]);
  const navHandlersRef = useRef<Array<(cmd: NavCommand) => void>>([]);
  const restartTimerRef = useRef<number | null>(null);
  const isUserStartedRef = useRef(false);
  const preferredVoiceRef = useRef<SpeechSynthesisVoice | null>(null);
  const utteranceRef = useRef<SpeechSynthesisUtterance | null>(null);

  useEffect(() => {
    if (!ttsSupported) return;
    return subscribeToPreferredVoice((voice) => {
      preferredVoiceRef.current = voice;
    });
  }, [ttsSupported]);

  const registerWakeHandler = useCallback((handler: (transcript: string) => void) => {
    wakeHandlersRef.current.push(handler);
    return () => {
      const idx = wakeHandlersRef.current.indexOf(handler);
      if (idx >= 0) wakeHandlersRef.current.splice(idx, 1);
    };
  }, []);

  const registerNavigationHandler = useCallback((handler: (cmd: NavCommand) => void) => {
    navHandlersRef.current.push(handler);
    return () => {
      const idx = navHandlersRef.current.indexOf(handler);
      if (idx >= 0) navHandlersRef.current.splice(idx, 1);
    };
  }, []);

  const speakFeedback = useCallback((text: string, _opts?: { priority?: boolean }) => {
    if (!isVoiceFeedbackEnabled || !ttsSupported || !text.trim()) return;
    try {
      window.speechSynthesis.cancel();
      const utterance = new SpeechSynthesisUtterance(text);
      utterance.rate = 0.98;
      utterance.pitch = 1.08;
      utterance.volume = 0.9;
      if (preferredVoiceRef.current) utterance.voice = preferredVoiceRef.current;
      utterance.onstart = () => setIsSpeakingFeedback(true);
      utterance.onend = () => setIsSpeakingFeedback(false);
      utterance.onerror = () => setIsSpeakingFeedback(false);
      utteranceRef.current = utterance;
      window.speechSynthesis.speak(utterance);
    } catch {
      setIsSpeakingFeedback(false);
    }
  }, [isVoiceFeedbackEnabled, ttsSupported]);

  const stopFeedback = useCallback(() => {
    if (!ttsSupported) return;
    try {
      window.speechSynthesis.cancel();
    } catch {
      /* ignore */
    }
    setIsSpeakingFeedback(false);
  }, [ttsSupported]);

  const initRecognition = useCallback((): SpeechRecognition | null => {
    if (!supported || typeof window === "undefined") return null;
    const Ctor = window.SpeechRecognition || window.webkitSpeechRecognition;
    if (!Ctor) return null;
    const r = new Ctor();
    r.continuous = true;
    r.interimResults = true;
    r.lang = "en-US";

    r.onresult = (event: SpeechRecognitionEvent) => {
      let finalText = "";
      let interimText = "";
      for (let i = event.resultIndex; i < event.results.length; i++) {
        const result = event.results[i];
        if (result.isFinal) {
          finalText += result[0].transcript;
        } else {
          interimText += result[0].transcript;
        }
      }
      if (finalText) setLastTranscript(finalText.trim());
      const toCheck = (finalText + (finalText ? " " : "") + interimText).trim();
      if (!toCheck) return;

      const lower = toCheck.toLowerCase();
      const isWake = WAKE_WORDS.some((w) => lower.includes(w));

      if (finalText) {
        const navCmd = parseNavigationCommand(finalText);
        if (navCmd && navCmd.path) {
          setLastNavigation(navCmd);
          navHandlersRef.current.forEach((h) => {
            try { h(navCmd); } catch (e) { /* ignore */ }
          });
        }

        if (isWake) {
          wakeHandlersRef.current.forEach((h) => {
            try { h(finalText.trim()); } catch (e) { /* ignore */ }
          });
        }
      }
    };

    r.onerror = (e: SpeechRecognitionErrorEvent) => {
      console.debug("Speech recognition error:", e.error);
    };

    r.onend = () => {
      setIsListening(false);
      if (recognitionRef.current && isUserStartedRef.current && isVoiceRecognitionEnabled) {
        if (restartTimerRef.current !== null) window.clearTimeout(restartTimerRef.current);
        restartTimerRef.current = window.setTimeout(() => {
          try {
            recognitionRef.current?.start();
            setIsListening(true);
          } catch {
            /* already started */
          }
        }, 400);
      }
    };

    r.onstart = () => {
      setIsListening(true);
    };

    return r;
  }, [supported, isVoiceRecognitionEnabled]);

  const startListening = useCallback(() => {
    if (!supported) return;
    isUserStartedRef.current = true;
    if (!recognitionRef.current) {
      recognitionRef.current = initRecognition();
    }
    try {
      recognitionRef.current?.start();
    } catch {
      /* already started */
    }
  }, [supported, initRecognition]);

  const stopListening = useCallback(() => {
    isUserStartedRef.current = false;
    if (restartTimerRef.current !== null) {
      window.clearTimeout(restartTimerRef.current);
      restartTimerRef.current = null;
    }
    try {
      recognitionRef.current?.stop();
      recognitionRef.current?.abort();
    } catch {
      /* ignore */
    }
    setIsListening(false);
  }, []);

  const toggleListening = useCallback(() => {
    if (isListening) stopListening();
    else startListening();
  }, [isListening, startListening, stopListening]);

  const setVoiceRecognitionEnabled = useCallback(async (enabled: boolean) => {
    setIsVoiceRecognitionEnabledState(enabled);
    if (typeof window !== "undefined") {
      window.localStorage.setItem(VOICE_PREFERENCE_KEY, enabled ? "1" : "0");
    }
    if (isAuthenticated && token) {
      try {
        await apiSetVoiceRecognitionEnabled(token, enabled);
        updateUserSettings({ ...(user ?? {} as any), voiceRecognitionEnabled: enabled } as any);
      } catch (err) {
        console.error("Failed to persist voice preference:", err);
      }
    }
    if (!enabled) stopListening();
  }, [isAuthenticated, token, user, updateUserSettings, stopListening]);

  const setVoiceFeedbackEnabled = useCallback(async (enabled: boolean) => {
    setIsVoiceFeedbackEnabledState(enabled);
    if (typeof window !== "undefined") {
      window.localStorage.setItem(VOICE_FEEDBACK_KEY, enabled ? "1" : "0");
    }
    if (isAuthenticated && token) {
      try {
        await apiSetVoiceFeedbackEnabled(token, enabled);
        updateUserSettings({ ...(user ?? {} as any), voiceFeedbackEnabled: enabled } as any);
      } catch (err) {
        console.error("Failed to persist voice feedback preference:", err);
      }
    }
    if (!enabled) stopFeedback();
  }, [isAuthenticated, token, user, updateUserSettings, stopFeedback]);

  useEffect(() => {
    if (isVoiceRecognitionEnabled && isAuthenticated && user?.voiceRecognitionEnabled) {
      const t = window.setTimeout(() => startListening(), 600);
      return () => window.clearTimeout(t);
    } else {
      stopListening();
    }
  }, [isVoiceRecognitionEnabled, isAuthenticated, user?.voiceRecognitionEnabled, startListening, stopListening]);

  useEffect(() => {
    const onKey = (e: KeyboardEvent) => {
      if (e.altKey && (e.key === "m" || e.key === "M")) {
        e.preventDefault();
        if (!isVoiceRecognitionEnabled) return;
        toggleListening();
      }
      if (e.altKey && e.shiftKey && (e.key === "s" || e.key === "S")) {
        e.preventDefault();
        stopFeedback();
      }
    };
    window.addEventListener("keydown", onKey);
    return () => window.removeEventListener("keydown", onKey);
  }, [isVoiceRecognitionEnabled, toggleListening, stopFeedback]);

  useEffect(() => {
    return () => {
      stopListening();
      stopFeedback();
      if (restartTimerRef.current !== null) window.clearTimeout(restartTimerRef.current);
    };
  }, [stopListening, stopFeedback]);

  return (
    <VoiceContext.Provider
      value={{
        isVoiceRecognitionEnabled,
        setVoiceRecognitionEnabled,
        isVoiceFeedbackEnabled,
        setVoiceFeedbackEnabled,
        isListening,
        startListening,
        stopListening,
        toggleListening,
        supported,
        lastTranscript,
        lastNavigation,
        registerWakeHandler,
        registerNavigationHandler,
        speakFeedback,
        stopFeedback,
        isSpeakingFeedback,
      }}
    >
      {children}
    </VoiceContext.Provider>
  );
}

export function useVoice() {
  const ctx = useContext(VoiceContext);
  if (ctx === undefined) throw new Error("useVoice must be used within a VoiceProvider");
  return ctx;
}
