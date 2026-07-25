import { useCallback, useEffect, useRef, useState } from "react";

export type LectureSpeed = 0.75 | 1 | 1.25 | 1.5;

interface Segment {
  text: string;
  charStart: number;
  charEnd: number;
  index: number;
}

export interface LectureState {
  speaking: boolean;
  paused: boolean;
  currentSegmentIndex: number;
  totalSegments: number;
  speed: LectureSpeed;
  supported: boolean;
  highlightedRange: [number, number] | null;
  progressPercent: number;
}

export function useLectureNarration(fullText: string, segments: Segment[]) {
  const utteranceRef = useRef<SpeechSynthesisUtterance | null>(null);
  const currentIndexRef = useRef(0);
  const pendingResumeRef = useRef(false);
  const [state, setState] = useState<LectureState>({
    speaking: false,
    paused: false,
    currentSegmentIndex: 0,
    totalSegments: segments.length,
    speed: 1,
    supported: typeof window !== "undefined" && "speechSynthesis" in window,
    highlightedRange: null,
    progressPercent: 0,
  });

  useEffect(() => {
    return () => {
      if (typeof window !== "undefined" && "speechSynthesis" in window) {
        window.speechSynthesis.cancel();
      }
    };
  }, []);

  useEffect(() => {
    setState((prev) => ({
      ...prev,
      totalSegments: segments.length,
      currentSegmentIndex:
        prev.currentSegmentIndex >= segments.length ? 0 : prev.currentSegmentIndex,
    }));
  }, [segments.length]);

  const cancel = useCallback(() => {
    if (typeof window === "undefined" || !("speechSynthesis" in window)) return;
    window.speechSynthesis.cancel();
    currentIndexRef.current = 0;
    pendingResumeRef.current = false;
    utteranceRef.current = null;
    setState((prev) => ({
      ...prev,
      speaking: false,
      paused: false,
      currentSegmentIndex: 0,
      highlightedRange: null,
      progressPercent: 0,
    }));
  }, []);

  const computeProgress = (segIdx: number, segs: Segment[]) => {
    if (segs.length === 0) return 0;
    const last = segs[segIdx] ?? segs[segs.length - 1];
    if (!last) return 0;
    return Math.min(100, Math.max(0, (last.charEnd / Math.max(1, fullText.length)) * 100));
  };

  const speakSegment = useCallback(
    (index: number, speed: LectureSpeed) => {
      if (!segments[index]) {
        setState((prev) => ({
          ...prev,
          speaking: false,
          paused: false,
          highlightedRange: null,
          progressPercent: 100,
          currentSegmentIndex: prev.totalSegments,
        }));
        return;
      }

      const seg = segments[index];
      const utter = new SpeechSynthesisUtterance(seg.text);
      utter.rate = speed;
      utter.pitch = 1.05;

      utter.onstart = () => {
        setState((prev) => ({
          ...prev,
          speaking: true,
          paused: false,
          currentSegmentIndex: index,
          highlightedRange: [seg.charStart, seg.charEnd],
          progressPercent: computeProgress(index, segments),
        }));
      };

      utter.onend = () => {
        const next = index + 1;
        if (pendingResumeRef.current) {
          pendingResumeRef.current = false;
        }
        if (next < segments.length) {
          currentIndexRef.current = next;
          requestAnimationFrame(() => speakSegment(next, speed));
        } else {
          setState((prev) => ({
            ...prev,
            speaking: false,
            paused: false,
            highlightedRange: null,
            progressPercent: 100,
            currentSegmentIndex: segments.length,
          }));
        }
      };

      utter.onerror = () => {
        setState((prev) => ({
          ...prev,
          speaking: false,
          paused: false,
          highlightedRange: null,
        }));
      };

      utteranceRef.current = utter;
      if (typeof window !== "undefined" && "speechSynthesis" in window) {
        window.speechSynthesis.speak(utter);
      }
    },
    [segments, fullText.length]
  );

  const play = useCallback(() => {
    if (!state.supported) return;
    if (typeof window === "undefined" || !("speechSynthesis" in window)) return;

    if (pendingResumeRef.current || state.paused) {
      window.speechSynthesis.resume();
      pendingResumeRef.current = false;
      setState((prev) => ({ ...prev, speaking: true, paused: false }));
      return;
    }

    const startIndex =
      currentIndexRef.current < segments.length ? currentIndexRef.current : 0;
    cancel();
    currentIndexRef.current = startIndex;
    setState((prev) => ({ ...prev, currentSegmentIndex: startIndex }));
    speakSegment(startIndex, state.speed);
  }, [state.supported, state.paused, state.speed, segments.length, cancel, speakSegment]);

  const pause = useCallback(() => {
    if (!state.supported) return;
    if (typeof window === "undefined" || !("speechSynthesis" in window)) return;
    window.speechSynthesis.pause();
    pendingResumeRef.current = true;
    setState((prev) => ({ ...prev, speaking: false, paused: true }));
  }, [state.supported]);

  const replay = useCallback(() => {
    if (!state.supported) return;
    const idx = Math.max(0, currentIndexRef.current - 1);
    cancel();
    currentIndexRef.current = idx;
    setState((prev) => ({ ...prev, currentSegmentIndex: idx }));
    setTimeout(() => speakSegment(idx, state.speed), 20);
  }, [state.supported, state.speed, cancel, speakSegment]);

  const setSpeed = useCallback(
    (speed: LectureSpeed) => {
      const wasSpeaking = state.speaking;
      setState((prev) => ({ ...prev, speed }));
      if (wasSpeaking && state.supported) {
        cancel();
        currentIndexRef.current = state.currentSegmentIndex;
        setTimeout(() => speakSegment(state.currentSegmentIndex, speed), 20);
      }
    },
    [state.speaking, state.supported, state.currentSegmentIndex, cancel, speakSegment]
  );

  const jumpToSegment = useCallback(
    (index: number) => {
      if (!state.supported) return;
      const idx = Math.min(Math.max(0, index), segments.length - 1);
      cancel();
      currentIndexRef.current = idx;
      setState((prev) => ({ ...prev, currentSegmentIndex: idx }));
      setTimeout(() => speakSegment(idx, state.speed), 20);
    },
    [state.supported, state.speed, segments.length, cancel, speakSegment]
  );

  return { state, play, pause, stop: cancel, replay, setSpeed, jumpToSegment } as const;
}

export function splitLectureContent(rawContent: string): {
  cleanText: string;
  segments: Segment[];
} {
  let clean = rawContent
    .replace(/```[\s\S]*?```/g, "")
    .replace(/`([^`]+)`/g, "$1")
    .replace(/!\[[^\]]*\]\([^)]+\)/g, "")
    .replace(/\[([^\]]+)\]\([^)]+\)/g, "$1")
    .replace(/^#{1,6}\s*/gm, "")
    .replace(/[*_~]{1,3}([^*_~]+)[*_~]{1,3}/g, "$1")
    .replace(/^\s*[-*+]\s+/gm, "• ")
    .replace(/^\s*>\s?/gm, "")
    .replace(/---{2,}/g, ". ")
    .replace(/<[^>]+>/g, "")
    .replace(/\r\n/g, "\n");

  clean = clean.replace(/\n{3,}/g, "\n\n").trim();

  const sentences: string[] = [];
  const sentenceRegex = /[^.!?。！？\n]+[.!?。！？]?|[.!?。！？]|\n+/g;
  let match;
  let buffer = "";
  while ((match = sentenceRegex.exec(clean)) !== null) {
    const piece = match[0];
    if (piece === "\n" || piece === "\n\n") {
      if (buffer.trim()) {
        sentences.push(buffer.trim());
        buffer = "";
      }
    } else if (/[.!?。！？]$/.test(piece.trim()) || piece.trim() === piece && piece.length < 2) {
      buffer += piece;
      if (buffer.trim()) {
        sentences.push(buffer.trim());
        buffer = "";
      }
    } else {
      buffer += piece;
    }
  }
  if (buffer.trim()) sentences.push(buffer.trim());

  const segments: Segment[] = [];
  let charCursor = 0;
  const text = clean;
  let idx = 0;
  for (const s of sentences) {
    if (!s) continue;
    const start = text.indexOf(s, charCursor);
    const charStart = start >= 0 ? start : charCursor;
    const charEnd = charStart + s.length;
    segments.push({ text: s, charStart, charEnd, index: idx });
    charCursor = charEnd;
    idx++;
  }

  return { cleanText: clean, segments };
}
