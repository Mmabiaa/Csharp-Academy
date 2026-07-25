import { useEffect, useMemo, useRef, useState } from "react";
import { useQuery } from "@tanstack/react-query";
import ReactMarkdown from "react-markdown";
import remarkGfm from "remark-gfm";
import {
  Send,
  Volume2,
  Pause,
  Play,
  RotateCcw,
  X,
  ChevronDown,
  Gauge,
  BookOpen,
  Brain,
  Sparkles,
  MessageCircle,
  CheckCircle2,
  XCircle,
  RefreshCw,
  Trophy,
  Heart,
  Loader2,
  AlertTriangle,
} from "lucide-react";
import type { LessonCompanionData, CompanionMiniQuiz } from "./companionData";
import { getAnswerFromData, getSuggestedQuestions } from "./companionData";
import {
  splitLectureContent,
  useLectureNarration,
  type LectureSpeed,
} from "./useLectureNarration";
import { useSound } from "../../context/SoundContext";
import { fetchLessonCompanionData } from "../../lib/api";

export interface LessonCompanionProps {
  lessonId: number;
  lessonTitle: string;
  lessonContent: string;
  lessonVoiceSummary?: string;
  lessonBestPractices?: string;
  lessonCompleted?: boolean;
  onCelebrateLessonComplete?: () => void;
  onPracticeFailed?: () => void;
  onPracticeSucceeded?: () => void;
  practiceFailCount?: number;
  practiceSuccessCount?: number;
  tutorialStep?: number;
  tutorialTotalSteps?: number;
}

type Mood = "idle" | "wave" | "thinking" | "happy" | "celebrate" | "concerned" | "speaking";
type Tab = "chat" | "lecture" | "quiz";

interface ChatMessage {
  role: "user" | "assistant";
  content: string;
  id: number;
}

let chatIdCounter = 1;

const EMPTY_COMPANION_DATA: LessonCompanionData = {
  encouragements: ["You can do this! 💪"],
  celebrations: ["🎉 Great job!"],
  greetings: ["Hi! Let me load the lesson details for you..."],
  conceptExplanations: {},
  questionAnswers: [],
  examples: [],
  miniQuizzes: [],
  practiceHints: [],
};

export default function LessonCompanion(props: LessonCompanionProps) {
  const {
    lessonId,
    lessonTitle,
    lessonContent,
    lessonVoiceSummary,
    lessonBestPractices,
    lessonCompleted,
    onPracticeFailed,
    onPracticeSucceeded,
    practiceFailCount = 0,
    practiceSuccessCount = 0,
    tutorialStep = 0,
    tutorialTotalSteps = 0,
  } = props;

  const { playSound } = useSound();

  const [open, setOpen] = useState(false);
  const [tab, setTab] = useState<Tab>("chat");
  const [mood, setMood] = useState<Mood>("wave");
  const [bubbleText, setBubbleText] = useState<string | null>(null);

  const { data: companionDataRaw, isLoading, error } = useQuery({
    queryKey: ["lesson-companion-data", lessonId] as const,
    queryFn: () => fetchLessonCompanionData(lessonId),
    staleTime: 10 * 60 * 1000,
    retry: 2,
    retryDelay: (attempt) => Math.min(1000 * attempt, 5000),
  });

  const companionData: LessonCompanionData = useMemo(() => {
    if (companionDataRaw) {
      const withBestPractices: LessonCompanionData = {
        ...companionDataRaw,
        conceptExplanations: { ...(companionDataRaw.conceptExplanations || {}) },
      };
      if (lessonBestPractices) {
        withBestPractices.conceptExplanations["best practices"] = lessonBestPractices;
      }
      return withBestPractices;
    }
    return EMPTY_COMPANION_DATA;
  }, [companionDataRaw, lessonBestPractices]);

  useEffect(() => {
    const timer = setTimeout(() => setMood("idle"), 3200);
    const greetings = companionData.greetings?.length
      ? companionData.greetings
      : EMPTY_COMPANION_DATA.greetings;
    const g = greetings[Math.floor(Math.random() * greetings.length)];
    setBubbleText(g);
    const bubbleTimer = setTimeout(() => setBubbleText(null), 5000);
    return () => {
      clearTimeout(timer);
      clearTimeout(bubbleTimer);
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [lessonTitle, companionData.greetings?.length]);

  useEffect(() => {
    if (!open || bubbleText) return;
    const encouragements = companionData.encouragements?.length
      ? companionData.encouragements
      : EMPTY_COMPANION_DATA.encouragements;
    if (practiceFailCount > 0 && practiceFailCount % 3 === 0) {
      setMood("concerned");
      setBubbleText(
        "Hmm, this one is tricky. Want me to break it down step by step? 💡"
      );
      setTimeout(() => setBubbleText(null), 5500);
      onPracticeFailed?.();
    } else if (practiceSuccessCount > 0) {
      const msg = encouragements[
        Math.floor(Math.random() * encouragements.length)
      ];
      setMood("happy");
      setBubbleText(msg);
      setTimeout(() => {
        setBubbleText(null);
        setMood("idle");
      }, 4200);
      onPracticeSucceeded?.();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [practiceFailCount, practiceSuccessCount]);

  useEffect(() => {
    if (lessonCompleted) {
      setMood("celebrate");
      const celebrations = companionData.celebrations?.length
        ? companionData.celebrations
        : EMPTY_COMPANION_DATA.celebrations;
      const c = celebrations[Math.floor(Math.random() * celebrations.length)];
      setBubbleText(c);
      setTimeout(() => {
        setBubbleText(null);
        setMood("idle");
      }, 7000);
    }
  }, [lessonCompleted, companionData.celebrations]);

  useEffect(() => {
    if (!tutorialTotalSteps || !open || bubbleText) return;
    const mid = Math.floor(tutorialTotalSteps / 2);
    if (tutorialStep === mid && mid > 0) {
      const msg = "Halfway through the tutorial — you're doing great! 🔥";
      setBubbleText(msg);
      setTimeout(() => setBubbleText(null), 4500);
    }
  }, [tutorialStep, tutorialTotalSteps, open, bubbleText]);

  return (
    <div
      className="duo-companion-root"
      style={{ zIndex: 40 }}
      aria-live="polite"
    >
      <style>{STYLES}</style>

      {bubbleText && (
        <div
          className={`duo-companion-bubble ${
            open ? "duo-companion-bubble-hidden" : ""
          }`}
          onClick={() => {
            setBubbleText(null);
            setOpen(true);
            playSound("click");
          }}
        >
          <span>{bubbleText}</span>
          <span className="duo-companion-bubble-arrow" />
        </div>
      )}

      <button
        type="button"
        onClick={() => {
          setOpen((o) => !o);
          setBubbleText(null);
          playSound("click");
        }}
        aria-label={open ? "Close learning companion" : "Open learning companion"}
        className={`duo-companion-fab duo-companion-mood-${mood} ${
          open ? "duo-companion-fab-open" : ""
        }`}
      >
        <OwlSvg mood={mood} />
        <span className="duo-companion-glow" aria-hidden="true" />
      </button>

      {open && (
        <div className="duo-companion-panel" role="dialog" aria-label="Learning companion">
          <div className="duo-companion-header">
            <div className="duo-companion-header-title">
              <div className="duo-companion-title-avatar">
                <OwlSvg mood="happy" small />
              </div>
              <div>
                <h3>Learning Companion</h3>
                <p>
                  <BookOpen className="w-3 h-3 inline-block mr-1" />
                  Studying: <strong>{lessonTitle}</strong>
                </p>
              </div>
            </div>
            <button
              type="button"
              className="duo-companion-close"
              onClick={() => {
                setOpen(false);
                playSound("click");
              }}
              aria-label="Close"
            >
              <X size={18} />
            </button>
          </div>

          <div className="duo-companion-tabs" role="tablist">
            {([
              ["chat", "Chat", MessageCircle],
              ["lecture", "Listen", Volume2],
              ["quiz", "Quiz", Brain],
            ] as const).map(([key, label, Icon]) => (
              <button
                key={key}
                role="tab"
                aria-selected={tab === key}
                onClick={() => {
                  setTab(key);
                  playSound("click");
                }}
                className={`duo-companion-tab ${
                  tab === key ? "duo-companion-tab-active" : ""
                }`}
              >
                <Icon size={16} />
                <span>{label}</span>
              </button>
            ))}
          </div>

          {tab === "chat" && (
            isLoading ? (
              <LoadingState label="Warming up my C# tutor brain..." />
            ) : error ? (
              <ErrorState error={error} />
            ) : (
              <ChatTab
                companionData={companionData}
                lessonTitle={lessonTitle}
                setMood={setMood}
              />
            )
          )}
          {tab === "lecture" && (
            <LectureTab
              lessonContent={lessonContent}
              lessonVoiceSummary={lessonVoiceSummary}
              setMood={setMood}
            />
          )}
          {tab === "quiz" && (
            isLoading ? (
              <LoadingState label="Loading your mini-quiz..." />
            ) : error ? (
              <ErrorState error={error} />
            ) : (
              <QuizTab
                quizzes={companionData.miniQuizzes}
                setMood={setMood}
              />
            )
          )}
        </div>
      )}
    </div>
  );
}

function LoadingState({ label }: { label: string }) {
  return (
    <div className="duo-companion-loading">
      <Loader2 className="w-8 h-8 animate-spin" aria-hidden="true" />
      <p>{label}</p>
      <p className="duo-companion-loading-sub">
        Lesson content is being matched on the server — give it a second 🦉
      </p>
    </div>
  );
}

function ErrorState({ error }: { error: unknown }) {
  const msg = error instanceof Error ? error.message : "Could not reach the server.";
  return (
    <div className="duo-companion-loading duo-companion-error">
      <AlertTriangle className="w-8 h-8" aria-hidden="true" />
      <p>
        <strong>Whoops!</strong> I couldn't load your tutor data.
      </p>
      <p className="duo-companion-loading-sub">{msg}</p>
      <p className="duo-companion-loading-sub">
        Make sure the backend server is running and refresh the page.
      </p>
    </div>
  );
}

/* =========================================================================
   CHAT TAB
   ========================================================================= */
function ChatTab({
  companionData,
  lessonTitle,
  setMood,
}: {
  companionData: LessonCompanionData;
  lessonTitle: string;
  setMood: (m: Mood) => void;
}) {
  const [messages, setMessages] = useState<ChatMessage[]>([]);
  const [input, setInput] = useState("");
  const [loading, setLoading] = useState(false);
  const [chatXp, setChatXp] = useState(0);
  const { playSound } = useSound();
  const bottomRef = useRef<HTMLDivElement>(null);
  const suggested = useMemo(() => getSuggestedQuestions(companionData), [companionData]);

  useEffect(() => {
    bottomRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages, loading]);

  const handleSend = (rawText?: string) => {
    const text = (rawText ?? input).trim();
    if (!text || loading) return;
    setInput("");
    setLoading(true);
    setMood("thinking");

    const userMsg: ChatMessage = {
      role: "user",
      content: text,
      id: chatIdCounter++,
    };
    setMessages((prev) => [...prev, userMsg]);

    setTimeout(() => {
      const reply = getAnswerFromData(text, companionData, lessonTitle);
      const assistantMsg: ChatMessage = {
        role: "assistant",
        content: reply,
        id: chatIdCounter++,
      };
      setMessages((prev) => [...prev, assistantMsg]);
      setChatXp((prev) => prev + 5);
      setLoading(false);
      setMood("happy");
      playSound("success");
      setTimeout(() => setMood("idle"), 1500);
    }, 550 + Math.random() * 300);
  };

  const empty = messages.length === 0;

  return (
    <div className="duo-companion-tab-body duo-companion-chat">
      <div className="duo-companion-chat-area">
        {empty ? (
          <div className="duo-companion-empty">
            <div className="duo-companion-empty-owl">
              <OwlSvg mood="happy" small />
            </div>
            <h4>Ask me anything!</h4>
            <p>
              I'm your personal tutor for this lesson. Here are some things I
              can help you with:
            </p>
            <div className="duo-companion-suggest-grid">
              {suggested.map((q) => (
                <button
                  key={q}
                  type="button"
                  onClick={() => handleSend(q)}
                  className="duo-companion-suggest-btn"
                >
                  {q}
                </button>
              ))}
            </div>
          </div>
        ) : (
          <>
            {messages.map((m) => (
              <div
                key={m.id}
                className={`duo-msg-row ${m.role}`}
              >
                {m.role === "assistant" && (
                  <div className="duo-avatar owl">
                    <OwlSvg mood="idle" tiny />
                  </div>
                )}
                <div
                  className={`duo-bubble ${
                    m.role === "assistant" ? "owl-bubble" : "user-bubble"
                  }`}
                >
                  {m.role === "assistant" ? (
                    <div className="duo-md">
                      <ReactMarkdown remarkPlugins={[remarkGfm]}>
                        {m.content}
                      </ReactMarkdown>
                    </div>
                  ) : (
                    m.content
                  )}
                </div>
                {m.role === "user" && (
                  <div className="duo-avatar user-av">U</div>
                )}
              </div>
            ))}
            {loading && (
              <div className="duo-typing">
                <div className="duo-avatar owl">
                  <OwlSvg mood="thinking" tiny />
                </div>
                <div className="duo-typing-dots">
                  <span className="duo-dot" />
                  <span className="duo-dot" />
                  <span className="duo-dot" />
                </div>
              </div>
            )}
            <div ref={bottomRef} />
          </>
        )}
      </div>

      {chatXp > 0 && (
        <div className="duo-companion-xpbar">
          <Sparkles className="w-4 h-4 text-[#946800]" />
          <span>
            +{chatXp} XP earned from this chat session
          </span>
        </div>
      )}

      <form
        onSubmit={(e) => {
          e.preventDefault();
          handleSend();
        }}
        className="duo-input-bar"
      >
        <input
          type="text"
          className="duo-input"
          placeholder="Ask about this lesson..."
          value={input}
          onChange={(e) => setInput(e.target.value)}
        />
        <button
          type="submit"
          className="duo-send-btn"
          disabled={loading || !input.trim()}
        >
          <Send size={16} />
        </button>
      </form>
    </div>
  );
}

/* =========================================================================
   LECTURE TAB
   ========================================================================= */
function LectureTab({
  lessonContent,
  lessonVoiceSummary,
  setMood,
}: {
  lessonContent: string;
  lessonVoiceSummary?: string;
  setMood: (m: Mood) => void;
}) {
  const { playSound } = useSound();
  const source = lessonVoiceSummary?.trim()
    ? lessonVoiceSummary
    : lessonContent;

  const { cleanText, segments } = useMemo(
    () => splitLectureContent(source),
    [source]
  );

  const { state, play, pause, stop, replay, setSpeed, jumpToSegment } =
    useLectureNarration(cleanText, segments);

  useEffect(() => {
    if (state.speaking) setMood("speaking");
    else if (!state.paused) setMood("idle");
  }, [state.speaking, state.paused, setMood]);

  const speedOptions: LectureSpeed[] = [0.75, 1, 1.25, 1.5];

  const previewSegments = segments.slice(0, 30);

  return (
    <div className="duo-companion-tab-body duo-companion-lecture">
      <div className="duo-companion-lecture-progress">
        <div className="duo-companion-lecture-progress-row">
          <span className="duo-companion-lecture-progress-label">
            {Math.round(state.progressPercent)}% · Segment{" "}
            {Math.min(state.currentSegmentIndex + 1, state.totalSegments)} of{" "}
            {state.totalSegments}
          </span>
        </div>
        <div className="duo-progress-track">
          <div
            className="duo-progress-fill"
            style={{ width: `${state.progressPercent}%` }}
          />
        </div>
      </div>

      <div className="duo-companion-lecture-transcript">
        {previewSegments.length === 0 ? (
          <div className="duo-companion-empty-small">
            Nothing to narrate yet. Content will appear here! 🔊
          </div>
        ) : (
          previewSegments.map((seg) => (
            <button
              key={seg.index}
              type="button"
              onClick={() => {
                jumpToSegment(seg.index);
                playSound("click");
              }}
              className={`duo-companion-segment ${
                state.currentSegmentIndex === seg.index &&
                (state.speaking || state.paused)
                  ? "duo-companion-segment-active"
                  : state.currentSegmentIndex > seg.index
                  ? "duo-companion-segment-done"
                  : ""
              }`}
            >
              {seg.text}
            </button>
          ))
        )}
        {segments.length > previewSegments.length && (
          <div className="duo-companion-lecture-more">
            …+{segments.length - previewSegments.length} more segments
          </div>
        )}
      </div>

      <div className="duo-companion-lecture-controls">
        <div className="duo-companion-lecture-main">
          <button
            type="button"
            className="duo-companion-round-btn duo-companion-ghost-btn"
            onClick={() => {
              replay();
              playSound("click");
            }}
            aria-label="Replay"
            disabled={!state.supported}
          >
            <RotateCcw size={18} />
          </button>

          <button
            type="button"
            className={`duo-companion-play-btn ${
              state.speaking
                ? "duo-companion-playing"
                : state.paused
                ? "duo-companion-paused"
                : ""
            }`}
            onClick={() => {
              if (state.speaking) pause();
              else play();
              playSound("click");
            }}
            disabled={!state.supported}
            aria-label={state.speaking ? "Pause" : "Play"}
          >
            {state.speaking ? <Pause size={22} /> : <Play size={22} />}
          </button>

          <button
            type="button"
            className="duo-companion-round-btn duo-companion-ghost-btn"
            onClick={() => {
              stop();
              playSound("click");
            }}
            aria-label="Stop"
            disabled={!state.supported || (!state.speaking && !state.paused)}
          >
            <XCircle size={18} />
          </button>
        </div>

        <div className="duo-companion-lecture-bottom">
          <div className="duo-companion-speed-group">
            <Gauge size={14} className="text-[#AFAFAF]" />
            {speedOptions.map((s) => (
              <button
                key={s}
                type="button"
                onClick={() => {
                  setSpeed(s);
                  playSound("click");
                }}
                className={`duo-companion-speed-btn ${
                  state.speed === s ? "duo-companion-speed-active" : ""
                }`}
              >
                {s}×
              </button>
            ))}
          </div>
          {!state.supported && (
            <span className="duo-companion-lecture-hint">
              ⚠️ Voice not supported in this browser
            </span>
          )}
        </div>
      </div>
    </div>
  );
}

/* =========================================================================
   QUIZ TAB
   ========================================================================= */
function QuizTab({
  quizzes,
  setMood,
}: {
  quizzes: CompanionMiniQuiz[];
  setMood: (m: Mood) => void;
}) {
  const { playSound } = useSound();
  const [index, setIndex] = useState(0);
  const [selected, setSelected] = useState<number | null>(null);
  const [revealed, setRevealed] = useState(false);
  const [correct, setCorrect] = useState(0);
  const [answered, setAnswered] = useState(0);
  const [finished, setFinished] = useState(false);

  const q = quizzes[index];
  const total = quizzes.length;

  const reset = () => {
    setIndex(0);
    setSelected(null);
    setRevealed(false);
    setCorrect(0);
    setAnswered(0);
    setFinished(false);
  };

  const submit = () => {
    if (selected === null) return;
    const isRight = selected === q.correctIndex;
    setRevealed(true);
    setAnswered((a) => a + 1);
    if (isRight) {
      setCorrect((c) => c + 1);
      setMood("happy");
      playSound("success");
    } else {
      setMood("concerned");
      playSound("error");
    }
    setTimeout(() => setMood("idle"), 1600);
  };

  const next = () => {
    setRevealed(false);
    setSelected(null);
    if (index + 1 >= total) {
      setFinished(true);
      setMood("celebrate");
      playSound("complete");
    } else {
      setIndex((i) => i + 1);
    }
  };

  if (total === 0) {
    return (
      <div className="duo-companion-tab-body duo-companion-quiz">
        <div className="duo-companion-empty-small">
          No mini-quizzes available for this lesson yet. Check back later! 📝
        </div>
      </div>
    );
  }

  return (
    <div className="duo-companion-tab-body duo-companion-quiz">
      <div className="duo-companion-quiz-header">
        <div className="duo-companion-quiz-stats">
          <Trophy size={14} className="text-[#FFC800]" />
          <span>{correct} / {answered || 0} correct</span>
        </div>
        <div className="duo-progress-track" style={{ height: 10 }}>
          <div
            className="duo-progress-fill"
            style={{ width: `${((index + (revealed ? 1 : 0)) / total) * 100}%` }}
          />
        </div>
        <span className="duo-companion-quiz-count">
          Q {Math.min(index + 1, total)} of {total}
        </span>
      </div>

      {finished ? (
        <div className="duo-companion-quiz-finished">
          <div className="duo-companion-quiz-trophy">
            <Trophy size={46} />
          </div>
          <h4>Mini-quiz complete!</h4>
          <p className="duo-companion-quiz-score">
            You got <strong>{correct}</strong> out of <strong>{total}</strong> correct
          </p>
          <div className="duo-companion-quiz-hearts">
            {Array.from({ length: total }).map((_, i) => (
              <Heart
                key={i}
                size={22}
                className={i < correct ? "" : "opacity-30"}
                fill={i < correct ? "#FF4B4B" : "none"}
                color={i < correct ? "#FF4B4B" : "#AFAFAF"}
              />
            ))}
          </div>
          <p className="duo-companion-quiz-feedback">
            {correct === total
              ? "💯 Perfect score! You truly mastered this material."
              : correct >= total * 0.6
              ? "👍 Nice job! Review the questions you missed and try again."
              : "💪 Good first attempt — reread the lesson and try again!"}
          </p>
          <button
            type="button"
            onClick={() => {
              reset();
              playSound("click");
            }}
            className="duo-btn3d duo-btn3d-blue"
          >
            <RefreshCw size={16} />
            Try again
          </button>
        </div>
      ) : (
        <>
          <div className="duo-companion-quiz-question">
            <strong>Q{index + 1}.</strong> {q.question}
          </div>
          <div className="duo-companion-quiz-options">
            {q.options.map((opt, i) => {
              const isSelected = selected === i;
              const isCorrect = revealed && i === q.correctIndex;
              const isWrong = revealed && isSelected && i !== q.correctIndex;
              return (
                <button
                  key={i}
                  type="button"
                  disabled={revealed}
                  onClick={() => setSelected(i)}
                  className={`duo-companion-quiz-opt ${
                    isSelected && !revealed ? "duo-companion-quiz-opt-selected" : ""
                  } ${isCorrect ? "duo-companion-quiz-opt-correct" : ""} ${
                    isWrong ? "duo-companion-quiz-opt-wrong" : ""
                  }`}
                >
                  <span className="duo-companion-quiz-opt-letter">
                    {String.fromCharCode(65 + i)}
                  </span>
                  <span className="duo-companion-quiz-opt-text">{opt}</span>
                  {isCorrect && <CheckCircle2 size={18} className="text-[#58CC02]" />}
                  {isWrong && <XCircle size={18} className="text-[#FF4B4B]" />}
                </button>
              );
            })}
          </div>

          {revealed && (
            <div
              className={`duo-companion-quiz-explanation ${
                selected === q.correctIndex
                  ? "duo-companion-quiz-explanation-correct"
                  : "duo-companion-quiz-explanation-wrong"
              }`}
            >
              {selected === q.correctIndex ? (
                <div className="flex items-start gap-2">
                  <CheckCircle2 size={18} className="mt-0.5 shrink-0" />
                  <div>
                    <p className="font-black mb-1">Correct!</p>
                    <ReactMarkdown remarkPlugins={[remarkGfm]}>
                      {q.explanation}
                    </ReactMarkdown>
                  </div>
                </div>
              ) : (
                <div className="flex items-start gap-2">
                  <XCircle size={18} className="mt-0.5 shrink-0" />
                  <div>
                    <p className="font-black mb-1">Not quite!</p>
                    <ReactMarkdown remarkPlugins={[remarkGfm]}>
                      {q.explanation}
                    </ReactMarkdown>
                  </div>
                </div>
              )}
            </div>
          )}

          <div className="duo-companion-quiz-actions">
            {!revealed ? (
              <button
                type="button"
                className="duo-btn3d duo-btn3d-green w-full"
                onClick={() => {
                  submit();
                  playSound("click");
                }}
                disabled={selected === null}
              >
                <CheckCircle2 size={16} />
                Submit answer
              </button>
            ) : (
              <button
                type="button"
                className="duo-btn3d duo-btn3d-blue w-full"
                onClick={() => {
                  next();
                  playSound("click");
                }}
              >
                {index + 1 >= total ? (
                  <>
                    <Trophy size={16} />
                    Finish quiz
                  </>
                ) : (
                  <>
                    Next question
                    <ChevronDown
                      size={16}
                      style={{ transform: "rotate(-90deg)" }}
                    />
                  </>
                )}
              </button>
            )}
          </div>
        </>
      )}
    </div>
  );
}

/* =========================================================================
   OWL SVG — multiple moods
   ========================================================================= */
function OwlSvg({
  mood = "idle",
  small = false,
  tiny = false,
}: {
  mood?: Mood;
  small?: boolean;
  tiny?: boolean;
}) {
  const size = tiny ? 22 : small ? 36 : 46;

  // Eye + beak tweaks per mood
  const eyeY = mood === "thinking" || mood === "concerned" ? 12.5 : 13;
  const pupilOffsetY =
    mood === "happy" || mood === "celebrate" || mood === "speaking" ? -0.2 : 0;
  const beakScale =
    mood === "happy" || mood === "celebrate" ? 1.15 : mood === "concerned" ? 0.92 : 1;
  const browsY =
    mood === "concerned"
      ? 6.6
      : mood === "thinking"
      ? 8.2
      : mood === "speaking"
      ? 9.5
      : 9;
  const browAngle =
    mood === "concerned" ? -10 : mood === "thinking" ? 8 : 0;

  const wingColor = "rgba(255,255,255,0.32)";
  const bodyWhite = "rgba(255,255,255,0.52)";
  const eyeWhite = "white";
  const pupil = "#3C3C3C";
  const beakColor = "#FFC800";

  return (
    <svg
      width={size}
      height={size}
      viewBox="0 0 28 28"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      aria-hidden="true"
      className="duo-companion-owl-svg"
    >
      <ellipse cx="14" cy="16.5" rx="9" ry="10" fill={wingColor} />
      <ellipse cx="14" cy="14.5" rx="7.2" ry="8.4" fill={bodyWhite} />

      {/* Tufts */}
      <path
        d="M8 7.2 C8 4.8 9.5 3.6 12 3.2 C12.5 3.14 13 3.12 13.5 3.12 L12.5 5 C11.2 5.2 10 5.7 9 6.5 Z"
        fill={wingColor}
      />
      <path
        d="M20 7.2 C20 4.8 18.5 3.6 16 3.2 C15.5 3.14 15 3.12 14.5 3.12 L15.5 5 C16.8 5.2 18 5.7 19 6.5 Z"
        fill={wingColor}
      />

      {/* Brows (per mood) */}
      <g
        transform={`translate(0 ${browsY - 9}) rotate(${browAngle} 10.5 9)`}
        opacity={mood === "concerned" || mood === "thinking" ? 0.9 : 0}
      >
        <path
          d="M7.5 10.2 Q10.5 8.5 13.5 10.2"
          stroke={pupil}
          strokeWidth="1.4"
          strokeLinecap="round"
          fill="none"
        />
        <path
          d="M14.5 10.2 Q17.5 8.5 20.5 10.2"
          stroke={pupil}
          strokeWidth="1.4"
          strokeLinecap="round"
          fill="none"
          transform={`scale(${mood === "concerned" ? "-1 1" : "1 1"} 17.5 10)`}
        />
      </g>

      {/* Happy — closed smiling eyes */}
      {(mood === "happy" || mood === "celebrate") && (
        <>
          <path
            d="M8.4 13 Q10.5 15.5 12.6 13"
            stroke={pupil}
            strokeWidth="1.6"
            strokeLinecap="round"
            fill="none"
          />
          <path
            d="M15.4 13 Q17.5 15.5 19.6 13"
            stroke={pupil}
            strokeWidth="1.6"
            strokeLinecap="round"
            fill="none"
          />
        </>
      )}

      {/* Regular eyes (not happy/celebrate) */}
      {mood !== "happy" && mood !== "celebrate" && (
        <>
          <circle cx="10.5" cy={eyeY} r={mood === "concerned" ? 2.4 : 2.7} fill={eyeWhite} />
          <circle cx="17.5" cy={eyeY} r={mood === "concerned" ? 2.4 : 2.7} fill={eyeWhite} />
          <circle cx={10.5 + (mood === "thinking" ? 0.7 : 0)} cy={eyeY + pupilOffsetY} r={1.35} fill={pupil} />
          <circle cx={17.5 + (mood === "thinking" ? 0.7 : 0)} cy={eyeY + pupilOffsetY} r={1.35} fill={pupil} />
          <circle cx={10.5 - 0.4} cy={eyeY - 0.6 + pupilOffsetY} r={0.45} fill="white" />
          <circle cx={17.5 - 0.4} cy={eyeY - 0.6 + pupilOffsetY} r={0.45} fill="white" />
        </>
      )}

      {/* Beak */}
      <g
        transform={`translate(14 17) scale(${beakScale}) translate(-14 -17)`}
        style={{ transformOrigin: "14px 17px" }}
      >
        {mood === "speaking" ? (
          <ellipse cx="14" cy="17.3" rx="2.1" ry="1.8" fill={beakColor} />
        ) : (
          <path d="M12 16.6 L14 18 L16 16.6 Q14 18.8 12 16.6 Z" fill={beakColor} />
        )}
      </g>

      {/* Wings */}
      <ellipse
        cx={mood === "celebrate" ? 6.8 : 7.5}
        cy={mood === "celebrate" ? 14.5 : 20}
        rx="2.3"
        ry="1.1"
        fill={wingColor}
        transform={mood === "celebrate" ? "rotate(-30 6.8 14.5)" : "rotate(0)"}
      />
      <ellipse
        cx={mood === "celebrate" ? 21.2 : 20.5}
        cy={mood === "celebrate" ? 14.5 : 20}
        rx="2.3"
        ry="1.1"
        fill={wingColor}
        transform={mood === "celebrate" ? "rotate(30 21.2 14.5)" : "rotate(0)"}
      />

      {/* Speech/think indicator dot */}
      {(mood === "speaking" || mood === "thinking") && (
        <circle
          cx="22"
          cy="7"
          r="2"
          fill={mood === "thinking" ? "#1CB0F6" : "#58CC02"}
          className="duo-companion-think-dot"
        />
      )}
    </svg>
  );
}

/* =========================================================================
   STYLES (scoped via .duo-companion- prefix)
   ========================================================================= */
const STYLES = `
.duo-companion-root {
  position: fixed;
  right: 18px;
  bottom: 22px;
  font-family: "DIN Round Pro", "Nunito", "Trebuchet MS", system-ui, sans-serif;
}
@media (min-width: 768px) {
  .duo-companion-root { right: 30px; bottom: 28px; }
}

/* ─── Floating Action Button (Owl) ─── */
.duo-companion-fab {
  position: relative;
  width: 68px;
  height: 68px;
  border-radius: 24px;
  border: 2px solid #46A302;
  background: linear-gradient(145deg, #89E219, #58CC02);
  box-shadow: 0 6px 0 #46A302, 0 12px 24px rgba(88, 204, 2, 0.25);
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  color: inherit;
  padding: 0;
  transition: transform 0.08s ease, box-shadow 0.08s ease, border-radius 0.2s ease;
  overflow: visible;
  animation: duo-companion-idle 3s ease-in-out infinite;
}
.duo-companion-fab-open {
  border-radius: 18px;
  background: linear-gradient(145deg, #1CB0F6, #0EA5E9);
  border-color: #1899D6;
  box-shadow: 0 6px 0 #1899D6, 0 12px 24px rgba(28, 176, 246, 0.28);
  animation: none;
}
.duo-companion-fab:active { transform: translateY(4px); box-shadow: 0 2px 0 #46A302; }
.duo-companion-fab-open:active { box-shadow: 0 2px 0 #1899D6; }
.duo-companion-fab:hover:not(:active) { transform: translateY(-1px); }

.duo-companion-glow {
  position: absolute;
  inset: -6px;
  border-radius: 30px;
  background: radial-gradient(closest-side, rgba(88,204,2,0.25), transparent 70%);
  pointer-events: none;
  opacity: 0.8;
  animation: duo-companion-glow 2.8s ease-in-out infinite;
}
.duo-companion-fab-open .duo-companion-glow {
  background: radial-gradient(closest-side, rgba(28,176,246,0.28), transparent 70%);
}

@keyframes duo-companion-idle {
  0%, 100% { transform: translateY(0) rotate(-1.2deg); }
  50% { transform: translateY(-5px) rotate(1.2deg); }
}
@keyframes duo-companion-glow {
  0%, 100% { opacity: 0.5; transform: scale(0.96); }
  50% { opacity: 1; transform: scale(1.08); }
}

/* Mood-specific tweaks */
.duo-companion-mood-wave { animation: duo-companion-wave 1.6s ease-in-out 2; }
@keyframes duo-companion-wave {
  0%, 100% { transform: translateY(0) rotate(-3deg); }
  25% { transform: translateY(-8px) rotate(4deg); }
  50% { transform: translateY(-3px) rotate(-2deg); }
  75% { transform: translateY(-6px) rotate(3deg); }
}
.duo-companion-mood-happy .duo-companion-owl-svg { animation: duo-companion-bop 0.6s ease-in-out 2; }
.duo-companion-mood-celebrate { animation: duo-companion-jump 0.55s cubic-bezier(.34,1.56,.64,1) 3; }
.duo-companion-mood-speaking .duo-companion-owl-svg { animation: duo-companion-bop 1.4s ease-in-out infinite; }
.duo-companion-mood-concerned { animation: duo-companion-shake 0.5s ease-in-out 2; }
.duo-companion-mood-thinking .duo-companion-owl-svg { animation: duo-companion-think 1.2s ease-in-out infinite; }
@keyframes duo-companion-bop {
  0%, 100% { transform: translateY(0); }
  50% { transform: translateY(-3px); }
}
@keyframes duo-companion-jump {
  0%, 100% { transform: translateY(0) rotate(0deg); }
  40% { transform: translateY(-14px) rotate(-4deg); }
  70% { transform: translateY(-2px) rotate(3deg); }
}
@keyframes duo-companion-shake {
  0%, 100% { transform: translateX(0); }
  25% { transform: translateX(-3px) rotate(-2deg); }
  75% { transform: translateX(3px) rotate(2deg); }
}
@keyframes duo-companion-think {
  0%, 100% { transform: rotate(-3deg); }
  50% { transform: rotate(4deg); }
}
.duo-companion-think-dot { animation: duo-companion-pulse 1s ease-in-out infinite; }
@keyframes duo-companion-pulse {
  0%, 100% { opacity: 0.4; transform: scale(0.9); }
  50% { opacity: 1; transform: scale(1.2); }
}

/* ─── Floating Speech Bubble ─── */
.duo-companion-bubble {
  position: absolute;
  right: 84px;
  bottom: 14px;
  max-width: 260px;
  background: white;
  color: #3C3C3C;
  border: 2px solid #E5E5E5;
  box-shadow: 0 4px 0 #E5E5E5, 0 8px 18px rgba(0,0,0,0.06);
  border-radius: 18px 18px 4px 18px;
  padding: 10px 14px;
  font-size: 14px;
  font-weight: 700;
  line-height: 1.35;
  cursor: pointer;
  animation: duo-pop-in 0.25s cubic-bezier(0.34, 1.56, 0.64, 1) forwards;
}
.duo-companion-bubble-hidden { display: none; }
.duo-companion-bubble-arrow {
  position: absolute;
  right: -9px;
  bottom: 16px;
  width: 12px;
  height: 12px;
  background: white;
  border-right: 2px solid #E5E5E5;
  border-top: 2px solid #E5E5E5;
  transform: rotate(45deg);
}
@keyframes duo-pop-in {
  from { opacity: 0; transform: translateY(10px) scale(0.96); }
  to { opacity: 1; transform: translateY(0) scale(1); }
}
@media (min-width: 768px) {
  .duo-companion-bubble { right: 98px; bottom: 20px; max-width: 300px; }
}

/* ─── Panel ─── */
.duo-companion-panel {
  position: absolute;
  right: 0;
  bottom: 86px;
  width: min(92vw, 380px);
  height: min(78vh, 620px);
  background: white;
  border: 2px solid #E5E5E5;
  border-radius: 24px;
  box-shadow: 0 10px 0 #E5E5E5, 0 26px 50px rgba(0,0,0,0.14);
  display: flex;
  flex-direction: column;
  overflow: hidden;
  animation: duo-pop-in 0.22s cubic-bezier(0.34, 1.56, 0.64, 1) forwards;
  transform-origin: bottom right;
}
@media (max-width: 420px) {
  .duo-companion-panel { width: calc(100vw - 28px); right: -4px; }
}

/* ─── Header ─── */
.duo-companion-header {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  padding: 14px 14px 12px;
  background: linear-gradient(180deg, #F4FBE8 0%, #FFFFFF 100%);
  border-bottom: 2px solid #E5E5E5;
  gap: 8px;
}
.duo-companion-header-title {
  display: flex;
  align-items: center;
  gap: 10px;
  min-width: 0;
  flex: 1;
}
.duo-companion-title-avatar {
  width: 44px; height: 44px;
  background: #58CC02;
  border-radius: 16px;
  box-shadow: 0 4px 0 #46A302;
  display: flex; align-items: center; justify-content: center;
  flex-shrink: 0;
}
.duo-companion-header-title h3 {
  margin: 0;
  font-size: 16px;
  font-weight: 800;
  color: #3C3C3C;
  line-height: 1.15;
}
.duo-companion-header-title p {
  margin: 2px 0 0;
  font-size: 11px;
  font-weight: 700;
  color: #AFAFAF;
  text-transform: uppercase;
  letter-spacing: 0.4px;
  line-height: 1.3;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}
.duo-companion-header-title p strong {
  color: #4B4B4B;
  text-transform: none;
  letter-spacing: 0;
}
.duo-companion-close {
  width: 34px; height: 34px;
  border-radius: 12px;
  background: white;
  border: 2px solid #E5E5E5;
  box-shadow: 0 3px 0 #E5E5E5;
  color: #AFAFAF;
  display: flex; align-items: center; justify-content: center;
  cursor: pointer;
  flex-shrink: 0;
  transition: all 0.1s;
}
.duo-companion-close:hover {
  border-color: #FF4B4B;
  color: #FF4B4B;
  box-shadow: 0 3px 0 #CC3333;
  transform: translateY(-1px);
}
.duo-companion-close:active { transform: translateY(2px); box-shadow: 0 0 0 #E5E5E5; }

/* ─── Tabs ─── */
.duo-companion-tabs {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  padding: 8px;
  gap: 6px;
  background: #F7F7F7;
  border-bottom: 2px solid #E5E5E5;
}
.duo-companion-tab {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 6px;
  padding: 10px 8px;
  border-radius: 14px;
  border: 2px solid transparent;
  background: transparent;
  color: #AFAFAF;
  font-weight: 800;
  font-size: 12px;
  text-transform: uppercase;
  letter-spacing: 0.4px;
  cursor: pointer;
  transition: all 0.12s ease;
  font-family: inherit;
}
.duo-companion-tab:hover {
  background: white;
  color: #4B4B4B;
}
.duo-companion-tab-active {
  background: white;
  border-color: #E5E5E5;
  box-shadow: 0 3px 0 #E5E5E5;
  color: #1899D6;
}

/* ─── Tab body base ─── */
.duo-companion-tab-body {
  flex: 1;
  display: flex;
  flex-direction: column;
  min-height: 0;
  overflow: hidden;
}

/* ─── Chat tab ─── */
.duo-companion-chat-area {
  flex: 1;
  overflow-y: auto;
  padding: 10px 10px 6px;
  scroll-behavior: smooth;
}
.duo-companion-chat-area::-webkit-scrollbar { width: 6px; }
.duo-companion-chat-area::-webkit-scrollbar-thumb {
  background: #E5E5E5;
  border-radius: 99px;
}

.duo-companion-chat .duo-msg-row {
  display: flex;
  margin-bottom: 10px;
  animation: duo-pop-in 0.22s cubic-bezier(0.34, 1.56, 0.64, 1) forwards;
  opacity: 0;
}
.duo-companion-chat .duo-msg-row.user { justify-content: flex-end; }
.duo-companion-chat .duo-msg-row.assistant { justify-content: flex-start; }
.duo-companion-chat .duo-avatar {
  width: 30px; height: 30px;
  border-radius: 10px;
  display: flex; align-items: center; justify-content: center;
  flex-shrink: 0;
  margin-top: 2px;
}
.duo-companion-chat .duo-avatar.owl {
  background: #58CC02;
  box-shadow: 0 3px 0 #46A302;
}
.duo-companion-chat .duo-avatar.user-av {
  background: #CE82FF;
  box-shadow: 0 3px 0 #A568CC;
  font-size: 12px;
  font-weight: 800;
  color: white;
}
.duo-companion-chat .duo-bubble {
  max-width: 78%;
  padding: 10px 14px;
  font-size: 13.5px;
  font-weight: 600;
  line-height: 1.5;
  word-break: break-word;
}
.duo-companion-chat .duo-bubble.owl-bubble {
  background: white;
  border: 2px solid #E5E5E5;
  border-radius: 4px 18px 18px 18px;
  color: #3C3C3C;
  box-shadow: 0 3px 0 #E5E5E5;
  margin-left: 6px;
}
.duo-companion-chat .duo-bubble.user-bubble {
  background: #1CB0F6;
  border: 2px solid #1899D6;
  border-radius: 18px 4px 18px 18px;
  color: white;
  box-shadow: 0 3px 0 #1899D6;
  margin-right: 6px;
  white-space: pre-wrap;
}

.duo-companion-chat .duo-typing {
  display: flex; align-items: flex-start; gap: 6px; margin-bottom: 10px;
}
.duo-companion-chat .duo-typing-dots {
  background: white;
  border: 2px solid #E5E5E5;
  border-radius: 4px 18px 18px 18px;
  box-shadow: 0 3px 0 #E5E5E5;
  padding: 12px 18px;
  display: flex; align-items: center; gap: 5px;
}
.duo-companion-chat .duo-dot {
  width: 6px; height: 6px; border-radius: 50%; background: #AFAFAF;
  animation: duo-bounce 1.2s ease-in-out infinite;
}
.duo-companion-chat .duo-dot:nth-child(2) { animation-delay: 0.15s; }
.duo-companion-chat .duo-dot:nth-child(3) { animation-delay: 0.3s; }
@keyframes duo-bounce {
  0%, 60%, 100% { transform: translateY(0); background: #AFAFAF; }
  30% { transform: translateY(-5px); background: #58CC02; }
}

.duo-md p { margin: 0 0 8px; }
.duo-md p:last-child { margin-bottom: 0; }
.duo-md ul, .duo-md ol { margin: 4px 0 8px 18px; padding: 0; }
.duo-md li { margin-bottom: 2px; }
.duo-md h1, .duo-md h2, .duo-md h3 {
  font-size: 14px; font-weight: 800; color: #3C3C3C; margin: 10px 0 4px;
}
.duo-md h1:first-child, .duo-md h2:first-child, .duo-md h3:first-child { margin-top: 0; }
.duo-md strong { font-weight: 800; color: #3C3C3C; }
.duo-md em { font-style: italic; }
.duo-md code {
  font-family: "Fira Code", "Cascadia Code", "Consolas", monospace;
  font-size: 12px;
  background: #F0F4FF;
  color: #4B5FCC;
  border: 1px solid #D8DEFF;
  border-radius: 6px;
  padding: 1px 6px;
}
.duo-md pre {
  background: #1E2030;
  border-radius: 12px;
  padding: 10px 14px;
  margin: 8px 0;
  overflow-x: auto;
  border: 2px solid #2D3050;
  box-shadow: 0 3px 0 #12141E;
}
.duo-md pre code {
  background: none; border: none; padding: 0; color: #A9B1D6;
  font-size: 12px; line-height: 1.55;
}
.duo-md blockquote {
  border-left: 3px solid #58CC02;
  margin: 6px 0;
  padding: 5px 10px;
  background: #F4FBE8;
  border-radius: 0 8px 8px 0;
  color: #3C6300;
  font-style: italic;
  font-size: 13px;
}
.duo-md table {
  width: 100%;
  border-collapse: collapse;
  margin: 8px 0;
  font-size: 12px;
}
.duo-md th, .duo-md td {
  border: 1px solid #E5E5E5;
  padding: 4px 8px;
  text-align: left;
}
.duo-md th {
  background: #F7F7F7;
  font-weight: 800;
}

/* ─── Chat empty ─── */
.duo-companion-empty {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 12px;
  padding: 20px 12px 10px;
  text-align: center;
}
.duo-companion-empty-owl {
  width: 64px; height: 64px;
  background: linear-gradient(145deg, #89E219, #58CC02);
  border-radius: 22px;
  box-shadow: 0 5px 0 #46A302;
  display: flex; align-items: center; justify-content: center;
  animation: duo-companion-idle 3s ease-in-out infinite;
}
.duo-companion-empty h4 {
  font-size: 18px; font-weight: 800; color: #3C3C3C; margin: 0;
}
.duo-companion-empty p {
  font-size: 13px; font-weight: 600; color: #777777;
  margin: 0; max-width: 280px; line-height: 1.45;
}
.duo-companion-suggest-grid {
  width: 100%;
  display: grid;
  grid-template-columns: 1fr;
  gap: 8px;
  margin-top: 6px;
}
.duo-companion-suggest-btn {
  text-align: left;
  background: white;
  border: 2px solid #E5E5E5;
  border-radius: 14px;
  padding: 9px 12px;
  font-size: 12.5px;
  font-weight: 700;
  color: #3C3C3C;
  cursor: pointer;
  box-shadow: 0 3px 0 #E5E5E5;
  transition: all 0.12s;
  line-height: 1.35;
  font-family: inherit;
}
.duo-companion-suggest-btn:hover {
  border-color: #1CB0F6;
  color: #1CB0F6;
  box-shadow: 0 3px 0 #1899D6;
  transform: translateY(-1px);
}
.duo-companion-suggest-btn:active {
  transform: translateY(2px);
  box-shadow: 0 0 0 #E5E5E5;
}

/* ─── XP bar (chat) ─── */
.duo-companion-xpbar {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 6px;
  padding: 7px 10px;
  background: #FFF8E0;
  border-top: 2px solid #FFE69A;
  color: #946800;
  font-size: 12px;
  font-weight: 800;
}

/* ─── Chat input ─── */
.duo-companion-chat .duo-input-bar {
  display: flex;
  gap: 8px;
  align-items: center;
  padding: 10px;
  border-top: 2px solid #F0F0F0;
  background: white;
  flex-shrink: 0;
}
.duo-companion-chat .duo-input {
  flex: 1;
  padding: 11px 14px;
  font-size: 13.5px;
  font-weight: 600;
  font-family: inherit;
  color: #3C3C3C;
  background: white;
  border: 2px solid #E5E5E5;
  border-radius: 14px;
  outline: none;
  transition: border-color 0.15s, box-shadow 0.15s;
  box-shadow: 0 3px 0 #E5E5E5;
}
.duo-companion-chat .duo-input::placeholder { color: #AFAFAF; font-weight: 600; }
.duo-companion-chat .duo-input:focus {
  border-color: #1CB0F6;
  box-shadow: 0 3px 0 #1899D6, 0 0 0 4px rgba(28,176,246,0.12);
}
.duo-companion-chat .duo-send-btn {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 44px; height: 44px;
  background: #1CB0F6;
  color: white;
  border: 2px solid #1899D6;
  border-radius: 14px;
  box-shadow: 0 4px 0 #1899D6;
  cursor: pointer;
  transition: all 0.1s;
  flex-shrink: 0;
  padding: 0;
}
.duo-companion-chat .duo-send-btn:hover:not(:disabled) {
  background: #0EA5E9;
  transform: translateY(-1px);
  box-shadow: 0 5px 0 #1899D6;
}
.duo-companion-chat .duo-send-btn:active:not(:disabled) {
  transform: translateY(3px);
  box-shadow: 0 1px 0 #1899D6;
}
.duo-companion-chat .duo-send-btn:disabled {
  background: #E5E5E5;
  border-color: #CCCCCC;
  box-shadow: 0 4px 0 #CCCCCC;
  color: #AFAFAF;
  cursor: not-allowed;
}

/* ─── Lecture tab ─── */
.duo-companion-lecture { }
.duo-companion-lecture-progress {
  padding: 12px 12px 8px;
  border-bottom: 2px solid #F0F0F0;
  background: #FAFBFF;
}
.duo-companion-lecture-progress-row {
  display: flex; align-items: center; justify-content: space-between;
  margin-bottom: 6px;
}
.duo-companion-lecture-progress-label {
  font-size: 11px; font-weight: 800; color: #777777;
  text-transform: uppercase; letter-spacing: 0.3px;
}

.duo-companion-lecture-transcript {
  flex: 1;
  overflow-y: auto;
  padding: 10px;
  display: flex;
  flex-direction: column;
  gap: 6px;
}
.duo-companion-lecture-transcript::-webkit-scrollbar { width: 6px; }
.duo-companion-lecture-transcript::-webkit-scrollbar-thumb {
  background: #E5E5E5; border-radius: 99px;
}
.duo-companion-segment {
  display: block;
  width: 100%;
  text-align: left;
  background: white;
  border: 2px solid #E5E5E5;
  border-radius: 12px;
  padding: 8px 12px;
  font-size: 13px;
  font-weight: 600;
  line-height: 1.5;
  color: #4B4B4B;
  cursor: pointer;
  transition: all 0.12s ease;
  font-family: inherit;
}
.duo-companion-segment:hover {
  border-color: #1CB0F6;
  color: #1899D6;
}
.duo-companion-segment-active {
  background: #FFF8E0;
  border-color: #FFC800;
  color: #7A5000;
  font-weight: 700;
  box-shadow: 0 0 0 4px rgba(255, 200, 0, 0.15);
}
.duo-companion-segment-done {
  color: #949494;
  background: #FAFAFA;
}
.duo-companion-lecture-more {
  text-align: center;
  padding: 6px;
  font-size: 11px;
  color: #AFAFAF;
  font-weight: 700;
}
.duo-companion-empty-small {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 30px 20px;
  text-align: center;
  color: #777777;
  font-weight: 700;
  font-size: 13px;
  line-height: 1.5;
}

.duo-companion-lecture-controls {
  flex-shrink: 0;
  padding: 12px;
  border-top: 2px solid #F0F0F0;
  background: #F7F7F7;
  display: flex;
  flex-direction: column;
  gap: 10px;
}
.duo-companion-lecture-main {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 14px;
}
.duo-companion-round-btn {
  width: 44px; height: 44px;
  border-radius: 14px;
  display: flex; align-items: center; justify-content: center;
  cursor: pointer;
  transition: all 0.1s;
  padding: 0;
  font-family: inherit;
}
.duo-companion-round-btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}
.duo-companion-ghost-btn {
  background: white;
  color: #777777;
  border: 2px solid #E5E5E5;
  box-shadow: 0 3px 0 #E5E5E5;
}
.duo-companion-ghost-btn:hover:not(:disabled) {
  border-color: #1CB0F6;
  color: #1CB0F6;
  box-shadow: 0 3px 0 #1899D6;
  transform: translateY(-1px);
}
.duo-companion-ghost-btn:active:not(:disabled) {
  transform: translateY(2px); box-shadow: 0 0 0 #E5E5E5;
}
.duo-companion-play-btn {
  width: 60px; height: 60px;
  border-radius: 20px;
  display: flex; align-items: center; justify-content: center;
  border: 2px solid #46A302;
  background: linear-gradient(145deg, #89E219, #58CC02);
  color: white;
  box-shadow: 0 5px 0 #46A302;
  cursor: pointer;
  transition: all 0.08s ease;
  padding: 0;
}
.duo-companion-play-btn:hover:not(:disabled) { transform: translateY(-1px); }
.duo-companion-play-btn:active:not(:disabled) {
  transform: translateY(4px);
  box-shadow: 0 1px 0 #46A302;
}
.duo-companion-play-btn:disabled {
  opacity: 0.5; cursor: not-allowed;
}
.duo-companion-playing {
  background: linear-gradient(145deg, #FF7065, #FF4B4B);
  border-color: #EA2B2B;
  box-shadow: 0 5px 0 #EA2B2B;
}
.duo-companion-playing:active:not(:disabled) {
  box-shadow: 0 1px 0 #EA2B2B;
}
.duo-companion-paused {
  background: linear-gradient(145deg, #FFD44D, #FFC800);
  border-color: #E6B400;
  box-shadow: 0 5px 0 #E6B400;
  color: #4B3A00;
}
.duo-companion-paused:active:not(:disabled) {
  box-shadow: 0 1px 0 #E6B400;
}

.duo-companion-lecture-bottom {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 10px;
  flex-wrap: wrap;
}
.duo-companion-speed-group {
  display: inline-flex;
  align-items: center;
  gap: 4px;
  background: white;
  border: 2px solid #E5E5E5;
  border-radius: 12px;
  padding: 4px;
  box-shadow: 0 3px 0 #E5E5E5;
}
.duo-companion-speed-btn {
  background: transparent;
  border: none;
  padding: 5px 8px;
  border-radius: 8px;
  font-size: 11.5px;
  font-weight: 800;
  color: #777777;
  cursor: pointer;
  font-family: inherit;
  transition: all 0.1s;
}
.duo-companion-speed-btn:hover { color: #1CB0F6; }
.duo-companion-speed-active {
  background: #1CB0F6;
  color: white !important;
  box-shadow: 0 2px 0 #1899D6;
}
.duo-companion-lecture-hint {
  font-size: 11px; color: #C47500; font-weight: 800;
}

/* ─── Quiz tab ─── */
.duo-companion-quiz { }
.duo-companion-quiz-header {
  padding: 12px;
  border-bottom: 2px solid #F0F0F0;
  background: #FFF8E0;
  display: grid;
  grid-template-columns: 1fr auto;
  grid-template-rows: auto auto;
  row-gap: 6px;
  column-gap: 10px;
  align-items: center;
}
.duo-companion-quiz-stats {
  display: inline-flex;
  align-items: center;
  gap: 5px;
  color: #C47500;
  font-size: 12px;
  font-weight: 800;
}
.duo-companion-quiz-count {
  font-size: 11px;
  color: #946800;
  font-weight: 800;
  text-align: right;
  text-transform: uppercase;
  letter-spacing: 0.3px;
}
.duo-companion-quiz-header .duo-progress-track {
  grid-column: 1 / -1;
}

.duo-companion-quiz-question {
  padding: 16px 14px 8px;
  font-size: 14.5px;
  font-weight: 700;
  line-height: 1.45;
  color: #3C3C3C;
}
.duo-companion-quiz-question strong {
  color: #58CC02;
  margin-right: 6px;
}

.duo-companion-quiz-options {
  padding: 6px 14px 0;
  display: flex;
  flex-direction: column;
  gap: 8px;
  flex: 1;
  overflow-y: auto;
}
.duo-companion-quiz-opt {
  display: flex;
  align-items: center;
  gap: 10px;
  text-align: left;
  background: white;
  border: 2px solid #E5E5E5;
  border-radius: 16px;
  padding: 10px 12px;
  cursor: pointer;
  font-size: 13px;
  font-weight: 600;
  color: #3C3C3C;
  box-shadow: 0 3px 0 #E5E5E5;
  transition: all 0.1s;
  font-family: inherit;
}
.duo-companion-quiz-opt:disabled { cursor: default; }
.duo-companion-quiz-opt:not(:disabled):hover {
  border-color: #1CB0F6;
  color: #1899D6;
  transform: translateY(-1px);
}
.duo-companion-quiz-opt:active:not(:disabled) {
  transform: translateY(2px);
  box-shadow: 0 0 0 #E5E5E5;
}
.duo-companion-quiz-opt-letter {
  width: 26px; height: 26px;
  border-radius: 8px;
  border: 2px solid #E5E5E5;
  background: #F7F7F7;
  display: flex; align-items: center; justify-content: center;
  flex-shrink: 0;
  font-weight: 900;
  font-size: 12px;
  color: #777777;
}
.duo-companion-quiz-opt-text {
  flex: 1;
  min-width: 0;
  line-height: 1.35;
}
.duo-companion-quiz-opt-selected {
  background: #E8F5FF;
  border-color: #1CB0F6 !important;
  color: #1899D6 !important;
  box-shadow: 0 3px 0 #1899D6 !important;
}
.duo-companion-quiz-opt-selected .duo-companion-quiz-opt-letter {
  background: #1CB0F6;
  border-color: #1899D6;
  color: white;
}
.duo-companion-quiz-opt-correct {
  background: #E8FBD8 !important;
  border-color: #58CC02 !important;
  color: #3C6300 !important;
  box-shadow: 0 3px 0 #46A302 !important;
}
.duo-companion-quiz-opt-correct .duo-companion-quiz-opt-letter {
  background: #58CC02;
  border-color: #46A302;
  color: white;
}
.duo-companion-quiz-opt-wrong {
  background: #FFEFEF !important;
  border-color: #FF4B4B !important;
  color: #CC3A3A !important;
  box-shadow: 0 3px 0 #CC3A3A !important;
}
.duo-companion-quiz-opt-wrong .duo-companion-quiz-opt-letter {
  background: #FF4B4B;
  border-color: #CC3A3A;
  color: white;
}

.duo-companion-quiz-explanation {
  margin: 12px 14px 0;
  padding: 10px 12px;
  border-radius: 14px;
  font-size: 12.5px;
  line-height: 1.5;
  font-weight: 600;
  color: #3C3C3C;
}
.duo-companion-quiz-explanation-correct {
  background: #E8FBD8;
  border: 2px solid #B4E582;
  color: #3C6300;
}
.duo-companion-quiz-explanation-wrong {
  background: #FFEFEF;
  border: 2px solid #FFC2C2;
  color: #8F2A2A;
}

.duo-companion-quiz-actions {
  flex-shrink: 0;
  padding: 12px 14px;
  border-top: 2px solid #F0F0F0;
  background: white;
}
.duo-companion-quiz-actions .duo-btn3d {
  width: 100%;
  padding: 12px 20px;
  font-size: 13px;
  letter-spacing: 0.2px;
  font-family: inherit;
}

/* ─── Quiz finished ─── */
.duo-companion-quiz-finished {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 24px 20px;
  gap: 10px;
  text-align: center;
}
.duo-companion-quiz-trophy {
  width: 88px; height: 88px;
  border-radius: 28px;
  background: linear-gradient(145deg, #FFD44D, #FFC800);
  box-shadow: 0 6px 0 #E6B400;
  display: flex; align-items: center; justify-content: center;
  color: white;
  margin-bottom: 6px;
  animation: duo-companion-jump 1s cubic-bezier(.34,1.56,.64,1) 2;
}
.duo-companion-quiz-finished h4 {
  margin: 0;
  font-size: 20px;
  font-weight: 900;
  color: #3C3C3C;
}
.duo-companion-quiz-score {
  margin: 0;
  font-size: 14px;
  color: #4B4B4B;
  font-weight: 600;
}
.duo-companion-quiz-score strong {
  color: #1899D6;
}
.duo-companion-quiz-hearts {
  display: inline-flex;
  gap: 4px;
  margin: 4px 0;
}
.duo-companion-quiz-feedback {
  margin: 4px 0 8px;
  font-size: 13px;
  color: #777777;
  font-weight: 700;
  line-height: 1.45;
  max-width: 280px;
}
.duo-companion-loading {
  padding: 32px 20px;
  text-align: center;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 8px;
  color: #3C3C3C;
}
.duo-companion-loading p {
  margin: 0;
  font-weight: 800;
  font-size: 15px;
}
.duo-companion-loading-sub {
  font-size: 13px !important;
  font-weight: 500 !important;
  color: #777777 !important;
  line-height: 1.5;
  max-width: 280px;
}
.duo-companion-error {
  color: #EA2B2B;
}
.duo-companion-error strong { color: #C31818; }
`;
