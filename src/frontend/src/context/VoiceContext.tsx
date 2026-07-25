import React, { createContext, useContext, useState, useEffect, useRef, useCallback, useMemo } from "react";
import { setVoiceRecognitionEnabled as apiSetVoiceRecognitionEnabled } from "../lib/api";
import { useAuth } from "./AuthContext";

interface VoiceContextType {
  isVoiceRecognitionEnabled: boolean;
  setVoiceRecognitionEnabled: (enabled: boolean) => Promise<void>;
  isListening: boolean;
  startListening: () => void;
  stopListening: () => void;
  toggleListening: () => void;
  supported: boolean;
  lastTranscript: string;
  registerWakeHandler: (handler: (transcript: string) => void) => () => void;
}

const VoiceContext = createContext<VoiceContextType | undefined>(undefined);

const WAKE_WORDS = ["hello", "hi", "c# academy", "c sharp academy", "i need help", "need help", "i'm stuck", "im stuck"];

function matchWakeWord(text: string): boolean {
  const lower = text.toLowerCase().trim();
  if (!lower) return false;
  return WAKE_WORDS.some((w) => lower.includes(w));
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

export function VoiceProvider({ children }: { children: React.ReactNode }) {
  const { user, token, isAuthenticated, updateUserSettings } = useAuth();

  const [isVoiceRecognitionEnabled, setIsVoiceRecognitionEnabledState] = useState<boolean>(() => {
    if (typeof window === "undefined") return false;
    const saved = window.localStorage.getItem(VOICE_PREFERENCE_KEY);
    if (saved !== null) return saved === "1";
    return user?.voiceRecognitionEnabled ?? false;
  });

  useEffect(() => {
    if (typeof user?.voiceRecognitionEnabled === "boolean") {
      setIsVoiceRecognitionEnabledState(user.voiceRecognitionEnabled);
    }
  }, [user?.voiceRecognitionEnabled]);

  const supported = useMemo<boolean>(() => {
    if (typeof window === "undefined") return false;
    return !!(window.SpeechRecognition || window.webkitSpeechRecognition);
  }, []);

  const [isListening, setIsListening] = useState(false);
  const [lastTranscript, setLastTranscript] = useState("");
  const recognitionRef = useRef<SpeechRecognition | null>(null);
  const wakeHandlersRef = useRef<Array<(transcript: string) => void>>([]);
  const restartTimerRef = useRef<number | null>(null);
  const isUserStartedRef = useRef(false);

  const registerWakeHandler = useCallback((handler: (transcript: string) => void) => {
    wakeHandlersRef.current.push(handler);
    return () => {
      const idx = wakeHandlersRef.current.indexOf(handler);
      if (idx >= 0) wakeHandlersRef.current.splice(idx, 1);
    };
  }, []);

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
      if (isWake && finalText) {
        wakeHandlersRef.current.forEach((h) => {
          try { h(finalText.trim()); } catch (e) { /* ignore */ }
        });
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

  useEffect(() => {
    if (isVoiceRecognitionEnabled && isAuthenticated && user?.voiceRecognitionEnabled) {
      // auto-start passive listening once user enables (browsers may block without gesture on some pages)
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
    };
    window.addEventListener("keydown", onKey);
    return () => window.removeEventListener("keydown", onKey);
  }, [isVoiceRecognitionEnabled, toggleListening]);

  useEffect(() => {
    return () => {
      stopListening();
      if (restartTimerRef.current !== null) window.clearTimeout(restartTimerRef.current);
    };
  }, [stopListening]);

  return (
    <VoiceContext.Provider
      value={{
        isVoiceRecognitionEnabled,
        setVoiceRecognitionEnabled,
        isListening,
        startListening,
        stopListening,
        toggleListening,
        supported,
        lastTranscript,
        registerWakeHandler,
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
