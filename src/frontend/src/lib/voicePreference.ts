// Shared speech-synthesis voice preference.
// Both the Learning Companion's spoken greetings and the lesson's "Listen"
// narration import this so they always pick the exact same system voice —
// one narrator across the whole app, not two independently-guessed ones.

let cachedVoice: SpeechSynthesisVoice | null = null;
let cacheReady = false;

function selectFemaleVoice(voices: SpeechSynthesisVoice[]): SpeechSynthesisVoice | null {
  if (!voices.length) return null;
  return (
    voices.find((v) => /female|samantha|victoria|zira|google us english/i.test(v.name)) ||
    voices.find((v) => v.lang.startsWith("en")) ||
    voices[0]
  );
}

/**
 * Returns the shared preferred voice, refreshing the cache if the voice
 * list has changed (voices load asynchronously in most browsers).
 */
export function getPreferredVoice(): SpeechSynthesisVoice | null {
  if (typeof window === "undefined" || !("speechSynthesis" in window)) return null;
  const voices = window.speechSynthesis.getVoices();
  if (!cacheReady && voices.length) {
    cachedVoice = selectFemaleVoice(voices);
    cacheReady = true;
  }
  return cachedVoice;
}

/**
 * Subscribes to the browser's voiceschanged event and calls onReady once a
 * preferred voice is available. Returns an unsubscribe function.
 */
export function subscribeToPreferredVoice(onReady: (voice: SpeechSynthesisVoice | null) => void): () => void {
  if (typeof window === "undefined" || !("speechSynthesis" in window)) {
    onReady(null);
    return () => {};
  }
  const load = () => {
    const voices = window.speechSynthesis.getVoices();
    if (voices.length) {
      cachedVoice = selectFemaleVoice(voices);
      cacheReady = true;
      onReady(cachedVoice);
    }
  };
  load();
  window.speechSynthesis.addEventListener("voiceschanged", load);
  return () => window.speechSynthesis.removeEventListener("voiceschanged", load);
}