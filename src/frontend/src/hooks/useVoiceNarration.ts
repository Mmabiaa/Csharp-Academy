import { useCallback, useEffect, useRef, useState } from "react";
import { getPreferredVoice, subscribeToPreferredVoice } from "../lib/voicePreference";

export function useVoiceNarration() {
  const [speaking, setSpeaking] = useState(false);
  const utteranceRef = useRef<SpeechSynthesisUtterance | null>(null);
  const preferredVoiceRef = useRef<SpeechSynthesisVoice | null>(getPreferredVoice());

  useEffect(() => {
    return subscribeToPreferredVoice((voice) => {
      preferredVoiceRef.current = voice;
    });
  }, []);

  const stop = useCallback(() => {
    window.speechSynthesis.cancel();
    setSpeaking(false);
  }, []);

  const speak = useCallback((text: string) => {
    if (!text.trim() || !("speechSynthesis" in window)) return;

    stop();
    const utterance = new SpeechSynthesisUtterance(text);
    utterance.rate = 0.95;
    utterance.pitch = 1.1;
    if (preferredVoiceRef.current) utterance.voice = preferredVoiceRef.current;
    utterance.onend = () => setSpeaking(false);
    utterance.onerror = () => setSpeaking(false);
    utteranceRef.current = utterance;
    setSpeaking(true);
    window.speechSynthesis.speak(utterance);
  }, [stop]);

  return { speak, stop, speaking, supported: "speechSynthesis" in window };
}