import { useEffect, useMemo, useRef, useState } from "react";
import { useQuery } from "@tanstack/react-query";
import ReactMarkdown from "react-markdown";
import remarkGfm from "remark-gfm";
import {
  Send,
  Volume2,
  VolumeX,
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
  GraduationCap,
  Mic,
  MicOff,
  FileText,
} from "lucide-react";
import type { LessonCompanionData, CompanionMiniQuiz } from "./companionData";
import { getAnswerFromData, getSuggestedQuestions } from "./companionData";
import {
  splitLectureContent,
  useLectureNarration,
  type LectureSpeed,
} from "./useLectureNarration";
import { fetchLessonCompanionData } from "../../lib/api";
import { getPreferredVoice, subscribeToPreferredVoice } from "../../lib/voicePreference";
import { useVoice } from "../../context/VoiceContext";

export interface LessonCompanionProps {
  lessonId?: number;
  lessonTitle?: string;
  lessonContent?: string;
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
  pageTitle?: string;
  pageContext?: string;
  genericSuggestedQuestions?: string[];
}

type Mood = "idle" | "wave" | "thinking" | "happy" | "celebrate" | "concerned" | "speaking";
type Tab = "chat" | "lecture" | "quiz";

interface ChatMessage {
  role: "user" | "assistant";
  content: string;
  id: number;
}

let chatIdCounter = 1;

const GENERIC_GREETINGS: string[] = [
  "Hi there! I'm here to help with your learning journey.",
  "Welcome! Feel free to ask me anything about C sharp or programming.",
  "Good day! How can I assist your studies today?",
  "Hello! Ready to dive into some C sharp learning?",
];

const GENERIC_ENCOURAGEMENTS: string[] = [
  "You're doing great — keep going!",
  "Every expert was once a beginner. You're on the right path.",
  "Consistency is key. Keep practicing and you'll master it.",
  "Learning a language takes time. Be patient with yourself.",
  "Mistakes are just stepping stones to understanding.",
];

const CSHARP_HELP: Record<string, string> = {
  "c#": "**C# (C-Sharp)** is a modern, object-oriented programming language developed by Microsoft. Key features include:\n\n• **Type-safe**: Strong typing prevents many common errors\n• **Garbage collected**: Automatic memory management\n• **OOP & FP**: Supports both object-oriented and functional styles\n• **Cross-platform**: Runs on Windows, macOS, Linux via .NET\n• **Rich ecosystem**: ASP.NET for web, Unity for games, MAUI for mobile\n\nStart by exploring the **Courses** page to begin structured learning!",
  "variables": "**Variables in C#** store data of specific types. Examples:\n\n```csharp\nint age = 25;\nstring name = \"Alice\";\nbool isActive = true;\ndouble price = 19.99;\nvar inferred = \"works too\"; // Type inferred\n```\n\nCommon types: `int`, `string`, `bool`, `double`, `decimal`, `var`",
  "classes": "**Classes** are blueprints for creating objects in C#:\n\n```csharp\npublic class Student\n{\n    public string Name { get; set; }\n    public int Age { get; set; }\n\n    public void Study()\n    {\n        Console.WriteLine($\"{Name} is studying!\");\n    }\n}\n\n// Usage\nvar student = new Student { Name = \"Alex\", Age = 20 };\nstudent.Study();\n```\n\nC# supports: inheritance, interfaces, abstract classes, records, and more.",
  "loops": "**Loops in C#** let you repeat code:\n\n```csharp\n// for loop\nfor (int i = 0; i < 5; i++) { /* code */ }\n\n// foreach loop\nforeach (var item in collection) { /* code */ }\n\n// while loop\nwhile (condition) { /* code */ }\n\n// do-while loop\ndo { /* code */ } while (condition);\n```\n\nUse `break` to exit, `continue` to skip to the next iteration.",
  "async": "**Async/await in C#** enables non-blocking I/O:\n\n```csharp\npublic async Task<string> FetchDataAsync(string url)\n{\n    using var client = new HttpClient();\n    return await client.GetStringAsync(url);\n}\n\n// Call it:\nvar data = await FetchDataAsync(\"https://api.example.com\");\n```\n\nAlways return `Task` or `Task<T>` from async methods. Use `await` when calling them.",
  "linq": "**LINQ (Language Integrated Query)** lets you query data with C# syntax:\n\n```csharp\nvar numbers = new[] { 1, 2, 3, 4, 5, 6 };\n\nvar evenNumbers = numbers\n    .Where(n => n % 2 == 0)\n    .OrderByDescending(n => n)\n    .Select(n => n * 10)\n    .ToList();\n\n// evenNumbers = [60, 40, 20]\n```\n\nCommon methods: `Where`, `Select`, `OrderBy`, `GroupBy`, `Sum`, `Any`, `FirstOrDefault`",
  "tips": "**Pro tips for learning C#:**\n\n1. **Practice daily** — even 15 minutes helps\n2. **Use the Playground** to experiment with code\n3. **Build projects** — theory alone isn't enough\n4. **Read error messages** — they tell you exactly what's wrong\n5. **Use the Practices section** for hands-on exercises\n6. **Don't skip fundamentals** — they make advanced topics easier\n7. **Ask questions** — the AI Assistant is always here!",
  "help": "I can help you with:\n\n• **C# concepts**: variables, classes, LINQ, async, etc.\n• **Programming tips**: best practices and pro advice\n• **Navigation**: where to find things in the app\n• **Motivation**: encouragement when you're stuck\n\nJust type a question or click a suggested button above!",
};

function getGenericAnswer(userMessage: string, pageTitle: string): string {
  const msg = (userMessage || "").toLowerCase().trim();

  if (!msg) {
    return "I didn't catch that. Ask me anything about C#, programming, or this app!";
  }

  const greetingKeywords = ["hi", "hello", "hey", "greetings", "yo", "sup", "good morning", "good afternoon", "good evening"];
  if (greetingKeywords.some((g) => msg.includes(g))) {
    const greeting = GENERIC_GREETINGS[Math.floor(Math.random() * GENERIC_GREETINGS.length)];
    return `${greeting}\n\nWe're currently on the **${pageTitle}** page. What would you like to know?`;
  }

  const encourageKeywords = ["encourage", "motivate", "inspire", "i need help", "stuck", "struggling", "can't do", "cant do", "give up", "hard", "difficult", "tired"];
  if (encourageKeywords.some((k) => msg.includes(k))) {
    return GENERIC_ENCOURAGEMENTS[Math.floor(Math.random() * GENERIC_ENCOURAGEMENTS.length)];
  }

  const helpKeywords = ["help", "what can you do", "capabilities", "features"];
  if (helpKeywords.some((k) => msg.includes(k))) {
    return CSHARP_HELP["help"];
  }

  for (const [key, value] of Object.entries(CSHARP_HELP)) {
    if (msg.includes(key)) {
      return value;
    }
  }

  if (msg.includes("course")) {
    return "Check out the **Courses** page to browse our structured curriculum. Each course contains lessons, videos, tutorials, and practice exercises to guide your C# learning journey from beginner to advanced.";
  }

  if (msg.includes("practice") || msg.includes("exercise")) {
    return "The **Practices** page has hands-on coding exercises designed to reinforce what you learn. Each challenge has instant feedback to help you improve quickly!";
  }

  if (msg.includes("challenge")) {
    return "**Challenges** are competitive coding problems where you can test your skills against other learners. Solve them efficiently to climb the **Leaderboard**!";
  }

  if (msg.includes("playground") || msg.includes("code")) {
    return "The **Playground** lets you write and run C# code in the browser. It's a great place to experiment with ideas, test concepts, and practice without needing to set up anything locally.";
  }

  if (msg.includes("leaderboard") || msg.includes("rank")) {
    return "The **Leaderboard** shows top learners by XP. Complete lessons, solve challenges, and ace practices to earn points and climb the rankings!";
  }

  if (msg.includes("progress") || msg.includes("track")) {
    return "Your **Progress Dashboard** shows detailed analytics of your learning journey: completed lessons, earned XP, time studied, and areas where you can improve.";
  }

  if (msg.includes("profile") || msg.includes("account")) {
    return "Your **Profile** page lets you view and edit your personal information, see your learning statistics, and manage account settings.";
  }

  return `Great question about **${pageTitle}**! While I focus on lesson content during lectures, here are some general things you can try:\n\n• Browse the **Courses** page for structured learning paths\n• Practice coding in the **Playground**\n• Test your knowledge with **Practices** and **Challenges**\n• Check the **Progress** dashboard to track your growth\n\nOr ask me about specific C# topics like **LINQ**, **async/await**, **classes**, **variables**, or **loops**!`;
}

/* =========================================================================
   COMPANION GREETINGS
   ========================================================================= */
export const COMPANION_GREETINGS: string[] = [
  "Welcome. Let's begin today's lesson.",
  "Good day. I'm ready to guide you through this material.",
  "Let's proceed with today's learning objectives.",
  "Welcome back. We'll continue where we left off.",
  "I'm prepared to assist with your studies today.",
  "Let's commence today's educational session.",
];

const EMPTY_COMPANION_DATA: LessonCompanionData = {
  encouragements: ["You're making good progress."],
  celebrations: ["Excellent work."],
  greetings: COMPANION_GREETINGS,
  conceptExplanations: {},
  questionAnswers: [],
  examples: [],
  miniQuizzes: [],
  practiceHints: [],
};

export default function LessonCompanion(props: LessonCompanionProps) {
  const {
    lessonId,
    lessonTitle: lessonTitleProp,
    lessonContent: lessonContentProp,
    lessonVoiceSummary,
    lessonBestPractices,
    lessonCompleted,
    onPracticeFailed,
    onPracticeSucceeded,
    practiceFailCount = 0,
    practiceSuccessCount = 0,
    tutorialStep = 0,
    tutorialTotalSteps = 0,
    pageTitle: pageTitleProp,
    pageContext,
    genericSuggestedQuestions,
  } = props;

  const hasLessonContext = typeof lessonId === "number" && !isNaN(lessonId);
  const lessonTitle = lessonTitleProp ?? pageTitleProp ?? "Academic Mentor";
  const lessonContent = lessonContentProp ?? pageContext ?? "";
  const contextLabel = hasLessonContext ? "Lecture" : "Current page";

  const [open, setOpen] = useState(false);
  const [tab, setTab] = useState<Tab>("chat");
  const [mood, setMood] = useState<Mood>("wave");
  const [bubbleText, setBubbleText] = useState<string | null>(null);

  const voiceHook = useVoice();
  const {
    isListening,
    toggleListening,
    supported: voiceSupported,
    isVoiceRecognitionEnabled,
    registerWakeHandler,
  } = voiceHook;

  const wakeUpRef = useRef<((transcript: string) => void) | null>(null);
  const chatTabSendRef = useRef<((text?: string) => void) | null>(null);
  const chatTabMessagesRef = useRef<ChatMessage[] | null>(null);

  useEffect(() => {
    const handler = (transcript: string) => {
      wakeUpRef.current?.(transcript);
    };
    return registerWakeHandler(handler);
  }, [registerWakeHandler]);

  useEffect(() => {
    wakeUpRef.current = (transcript: string) => {
      setBubbleText(null);
      setOpen(true);
      setTab("chat");
      if (chatTabMessagesRef.current && chatTabMessagesRef.current.length === 0) {
        setTimeout(() => {
          chatTabSendRef.current?.(transcript);
        }, 250);
      }
    };
  }, []);

  const [voiceEnabled, setVoiceEnabled] = useState<boolean>(() => {
    if (typeof window === "undefined") return true;
    const stored = window.localStorage.getItem("duo-companion-voice");
    return stored === null ? true : stored === "1";
  });
  const preferredVoiceRef = useRef<SpeechSynthesisVoice | null>(getPreferredVoice());

  useEffect(() => {
    if (typeof window === "undefined") {
      return;
    }
    window.localStorage.setItem("duo-companion-voice", voiceEnabled ? "1" : "0");
  }, [voiceEnabled]);

  useEffect(() => {
    return subscribeToPreferredVoice((voice) => {
      preferredVoiceRef.current = voice;
    });
  }, []);

  useEffect(() => {
    return () => {
      if (typeof window !== "undefined") window.speechSynthesis?.cancel();
    };
  }, []);

  const speakGreeting = (text: string) => {
    if (!voiceEnabled || typeof window === "undefined" || !window.speechSynthesis) return;
    window.speechSynthesis.cancel();
    const clean = text.replace(/[*_#`>~]/g, "").trim();
    if (!clean) return;
    const utter = new SpeechSynthesisUtterance(clean);
    utter.rate = 1.02;
    utter.pitch = 1.15;
    utter.volume = 1;
    if (preferredVoiceRef.current) utter.voice = preferredVoiceRef.current;
    utter.onstart = () => setMood("speaking");
    utter.onend = () => setMood("happy");
    utter.onerror = () => setMood("idle");
    window.speechSynthesis.speak(utter);
  };

  const speakReply = (text: string) => {
    if (!voiceEnabled || typeof window === "undefined" || !window.speechSynthesis) return;
    window.speechSynthesis.cancel();
    let clean = text
      .replace(/```[\s\S]*?```/g, " ")
      .replace(/`[^`]*`/g, " ")
      .replace(/[*_#>~\[\]()!]/g, " ")
      .replace(/\s{2,}/g, " ")
      .trim();
    if (!clean) return;
    if (clean.length > 600) clean = clean.slice(0, 600) + " ... ";
    const utter = new SpeechSynthesisUtterance(clean);
    utter.rate = 1.0;
    utter.pitch = 1.12;
    utter.volume = 1;
    if (preferredVoiceRef.current) utter.voice = preferredVoiceRef.current;
    utter.onstart = () => setMood("speaking");
    utter.onend = () => setMood("idle");
    utter.onerror = () => setMood("idle");
    window.speechSynthesis.speak(utter);
  };

  const { data: companionDataRaw, isLoading, error } = useQuery({
    queryKey: ["lesson-companion-data", lessonId] as const,
    queryFn: () => fetchLessonCompanionData(lessonId!),
    staleTime: 10 * 60 * 1000,
    retry: 2,
    retryDelay: (attempt) => Math.min(1000 * attempt, 5000),
    enabled: hasLessonContext,
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
    const greetingPool = hasLessonContext ? COMPANION_GREETINGS : GENERIC_GREETINGS;
    const g = greetingPool[Math.floor(Math.random() * greetingPool.length)];
    setBubbleText(g);
    const bubbleTimer = setTimeout(() => setBubbleText(null), 5000);
    return () => {
      clearTimeout(timer);
      clearTimeout(bubbleTimer);
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [lessonTitle, hasLessonContext]);

  useEffect(() => {
    if (!open || bubbleText) return;
    const encouragements = companionData.encouragements?.length
      ? companionData.encouragements
      : EMPTY_COMPANION_DATA.encouragements;
    if (practiceFailCount > 0 && practiceFailCount % 3 === 0) {
      setMood("concerned");
      setBubbleText(
        "I notice you're finding this challenging. Shall we review the fundamentals?"
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
      const msg = "You've reached the midpoint of this tutorial. Well done.";
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
            const spoken = bubbleText;
            setBubbleText(null);
            setOpen(true);
            speakGreeting(spoken);
          }}
        >
          <span>{bubbleText}</span>
          <span className="duo-companion-bubble-arrow" />
        </div>
      )}

      <button
        type="button"
        onClick={() => {
          const opening = !open;
          setOpen(opening);
          setBubbleText(null);
          if (opening) {
            const g = COMPANION_GREETINGS[Math.floor(Math.random() * COMPANION_GREETINGS.length)];
            speakGreeting(g);
          } else if (typeof window !== "undefined") {
            window.speechSynthesis?.cancel();
            setMood("idle");
          }
        }}
        aria-label={open ? "Close learning companion" : "Open learning companion"}
        className={`duo-companion-fab duo-companion-mood-${mood} ${
          open ? "duo-companion-fab-open" : ""
        }`}
      >
        <span className="duo-companion-fab-ring" aria-hidden="true" />
        <span className="duo-companion-fab-sheen" aria-hidden="true" />
        <CompanionAvatar mood={mood} />
        <span className="duo-companion-glow" aria-hidden="true" />
      </button>

      {open && (
        <div className="duo-companion-panel" role="dialog" aria-label="Learning companion">
          <div className="duo-companion-header">
            <div className="duo-companion-header-title">
              <div className="duo-companion-title-avatar">
                <CompanionAvatar mood="happy" small />
              </div>
              <div>
                <h3>Academic Mentor</h3>
                <p>
                  <BookOpen className="w-3 h-3 inline-block mr-1" />
                  {contextLabel}: <strong>{lessonTitle}</strong>
                </p>
              </div>
            </div>
            <div className="duo-companion-header-actions">
              <button
                type="button"
                className={`duo-companion-mute ${voiceEnabled ? "" : "duo-companion-mute-off"}`}
                onClick={() => {
                  const next = !voiceEnabled;
                  setVoiceEnabled(next);
                  if (!next && typeof window !== "undefined") {
                    window.speechSynthesis?.cancel();
                    setMood("idle");
                  }
                }}
                aria-label={voiceEnabled ? "Mute companion voice" : "Unmute companion voice"}
                aria-pressed={voiceEnabled}
                title={voiceEnabled ? "Voice on" : "Voice off"}
              >
                {voiceEnabled ? <Volume2 size={16} /> : <VolumeX size={16} />}
              </button>
              <button
                type="button"
                className="duo-companion-close"
                onClick={() => {
                  setOpen(false);
                  if (typeof window !== "undefined") window.speechSynthesis?.cancel();
                }}
                aria-label="Close"
              >
                <X size={18} />
              </button>
            </div>
          </div>

          <div className="duo-companion-tabs" role="tablist">
            {([
              ["chat", "Discussion", MessageCircle],
              ...(hasLessonContext ? [
                ["lecture", "Lecture", Mic] as const,
                ["quiz", "Assessment", Brain] as const,
              ] : []),
            ] as const).map(([key, label, Icon]) => (
              <button
                key={key}
                role="tab"
                aria-selected={tab === key}
                onClick={() => {
                  setTab(key);
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
            hasLessonContext && isLoading ? (
              <LoadingState label="Preparing educational resources..." />
            ) : hasLessonContext && error ? (
              <ErrorState error={error} />
            ) : (
              <ChatTab
                companionData={companionData}
                lessonTitle={lessonTitle}
                setMood={setMood}
                hasLessonContext={hasLessonContext}
                genericSuggestedQuestions={genericSuggestedQuestions}
                speakReply={speakReply}
                sendRef={chatTabSendRef}
                messagesRef={chatTabMessagesRef}
                voiceSupported={voiceSupported && isVoiceRecognitionEnabled}
                isListening={isListening}
                toggleListening={toggleListening}
              />
            )
          )}
          {tab === "lecture" && hasLessonContext && (
            <LectureTab
              lessonContent={lessonContent}
              lessonVoiceSummary={lessonVoiceSummary}
              setMood={setMood}
            />
          )}
          {tab === "quiz" && hasLessonContext && (
            isLoading ? (
              <LoadingState label="Preparing your assessment..." />
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
        Please wait while I prepare the educational materials.
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
        <strong>Error:</strong> Unable to load educational content.
      </p>
      <p className="duo-companion-loading-sub">{msg}</p>
      <p className="duo-companion-loading-sub">
        Please verify the server connection and refresh the page.
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
  hasLessonContext,
  genericSuggestedQuestions,
  speakReply,
  sendRef,
  messagesRef,
  voiceSupported,
  isListening,
  toggleListening,
}: {
  companionData: LessonCompanionData;
  lessonTitle: string;
  setMood: (m: Mood) => void;
  hasLessonContext?: boolean;
  genericSuggestedQuestions?: string[];
  speakReply?: (text: string) => void;
  sendRef?: React.MutableRefObject<((text?: string) => void) | null>;
  messagesRef?: React.MutableRefObject<ChatMessage[] | null>;
  voiceSupported?: boolean;
  isListening?: boolean;
  toggleListening?: () => void;
}) {
  const [messages, setMessages] = useState<ChatMessage[]>([]);
  const [input, setInput] = useState("");
  const [loading, setLoading] = useState(false);
  const [chatXp, setChatXp] = useState(0);
  const bottomRef = useRef<HTMLDivElement>(null);
  const suggested = useMemo(() => {
    if (!hasLessonContext && genericSuggestedQuestions?.length) {
      return genericSuggestedQuestions;
    }
    return getSuggestedQuestions(companionData);
  }, [companionData, hasLessonContext, genericSuggestedQuestions]);

  useEffect(() => {
    bottomRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages, loading]);

  useEffect(() => {
    if (sendRef) sendRef.current = handleSend;
  }, [sendRef, loading, input, hasLessonContext, companionData, lessonTitle]);

  useEffect(() => {
    if (messagesRef) messagesRef.current = messages;
  }, [messagesRef, messages]);

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
      let reply: string;
      if (hasLessonContext) {
        reply = getAnswerFromData(text, companionData, lessonTitle);
      } else {
        reply = getGenericAnswer(text, lessonTitle);
      }
      const assistantMsg: ChatMessage = {
        role: "assistant",
        content: reply,
        id: chatIdCounter++,
      };
      setMessages((prev) => [...prev, assistantMsg]);
      setChatXp((prev) => prev + 5);
      setLoading(false);
      setMood("happy");
      if (speakReply) speakReply(reply);
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
              <CompanionAvatar mood="happy" small />
            </div>
            <h4>{hasLessonContext ? "Welcome to Academic Discussion" : "Welcome"}</h4>
            <p>
              {hasLessonContext
                ? "I'm your academic mentor for this lecture. I can help you with:"
                : `I'm your academic mentor for ${lessonTitle}. I can help you with:`}
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
                    <CompanionAvatar mood="idle" tiny />
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
                  <div className="duo-avatar user-av">S</div>
                )}
              </div>
            ))}
            {loading && (
              <div className="duo-typing">
                <div className="duo-avatar owl">
                  <CompanionAvatar mood="thinking" tiny />
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
          <Sparkles className="w-4 h-4 text-[#FFC800]" />
          <span>
            +{chatXp} XP earned from this session
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
          placeholder={hasLessonContext ? "Ask about this lecture..." : "Ask about C# or this page..."}
          value={input}
          onChange={(e) => setInput(e.target.value)}
        />
        {voiceSupported ? (
          <button
            type="button"
            onClick={toggleListening}
            className={`duo-send-btn ${isListening ? "!bg-[#FF4B4B] animate-pulse" : "!bg-neutral-500 hover:!bg-neutral-600"}`}
            title={isListening ? "Stop listening" : "Start voice input (Alt+M)"}
            aria-label={isListening ? "Stop voice input" : "Start voice input"}
          >
            {isListening ? <MicOff size={16} /> : <Mic size={16} />}
          </button>
        ) : null}
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
  const source = lessonVoiceSummary?.trim()
    ? lessonVoiceSummary
    : lessonContent;

  const { cleanText, segments } = useMemo(
    () => splitLectureContent(source),
    [source]
  );

  const { state, play, pause, stop, replay, setSpeed, jumpToSegment } =
    useLectureNarration(cleanText, segments);

  const handlePlayLecture = () => {
    if (cleanText) {
      play();
      setMood("speaking");
    }
  };

  useEffect(() => {
    if (state.speaking) setMood("speaking");
    else if (!state.paused) setMood("idle");
  }, [state.speaking, state.paused, setMood]);

  const speedOptions: LectureSpeed[] = [0.75, 1, 1.25, 1.5];

  return (
    <div className="duo-companion-tab-body duo-companion-lecture">
      <div className="duo-companion-lecture-header">
        <div className="duo-companion-lecture-header-icon">
          <Mic size={20} className="text-[#58CC02]" />
        </div>
        <div>
          <h4 className="duo-companion-lecture-title">Lecture Session</h4>
          <p className="duo-companion-lecture-subtitle">
            Academic content narration with professional presentation
          </p>
        </div>
      </div>

      <div className="duo-companion-lecture-progress">
        <div className="duo-companion-lecture-progress-row">
          <span className="duo-companion-lecture-progress-label">
            {Math.round(state.progressPercent)}% · Section{" "}
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
        {segments.length === 0 ? (
          <div className="duo-companion-empty-small">
            <FileText size={32} className="text-[#AFAFAF] mb-2" />
            <p>Lecture content will appear here.</p>
            <p className="text-sm text-[#AFAFAF]">
              The instructor will narrate each section clearly.
            </p>
          </div>
        ) : (
          segments.map((seg) => (
            <button
              key={seg.index}
              type="button"
              onClick={() => {
                jumpToSegment(seg.index);
                if (state.speaking) {
                } else {
                  handlePlayLecture();
                }
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
              <span className="duo-companion-segment-number">
                {seg.index + 1}
              </span>
              <span className="duo-companion-segment-text">{seg.text}</span>
            </button>
          ))
        )}
      </div>

      <div className="duo-companion-lecture-controls">
        <div className="duo-companion-lecture-main">
          <button
            type="button"
            className="duo-companion-round-btn duo-companion-ghost-btn"
            onClick={() => {
              replay();
            }}
            aria-label="Replay lecture"
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
              if (state.speaking) {
                pause();
                setMood("idle");
              } else {
                handlePlayLecture();
              }
            }}
            disabled={!state.supported}
            aria-label={state.speaking ? "Pause lecture" : "Start lecture"}
          >
            {state.speaking ? <Pause size={22} /> : <Play size={22} />}
          </button>

          <button
            type="button"
            className="duo-companion-round-btn duo-companion-ghost-btn"
            onClick={() => {
              stop();
              setMood("idle");
            }}
            aria-label="Stop lecture"
            disabled={!state.supported || (!state.speaking && !state.paused)}
          >
            <XCircle size={18} />
          </button>
        </div>

        <div className="duo-companion-lecture-bottom">
          <div className="duo-companion-speed-group">
            <Gauge size={14} className="text-[#AFAFAF]" />
            <span className="duo-companion-speed-label">Speed</span>
            {speedOptions.map((s) => (
              <button
                key={s}
                type="button"
                onClick={() => {
                  setSpeed(s);
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
              ⚠️ Voice narration unavailable in this browser
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
    } else {
      setMood("concerned");
    }
    setTimeout(() => setMood("idle"), 1600);
  };

  const next = () => {
    setRevealed(false);
    setSelected(null);
    if (index + 1 >= total) {
      setFinished(true);
      setMood("celebrate");
    } else {
      setIndex((i) => i + 1);
    }
  };

  if (total === 0) {
    return (
      <div className="duo-companion-tab-body duo-companion-quiz">
        <div className="duo-companion-empty-small">
          <Brain size={32} className="text-[#AFAFAF] mb-2" />
          <p>No assessments available for this lecture.</p>
          <p className="text-sm text-[#AFAFAF]">Check back for future updates.</p>
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
          Question {Math.min(index + 1, total)} of {total}
        </span>
      </div>

      {finished ? (
        <div className="duo-companion-quiz-finished">
          <div className="duo-companion-quiz-trophy">
            <GraduationCap size={46} className="text-[#58CC02]" />
          </div>
          <h4>Assessment Complete</h4>
          <p className="duo-companion-quiz-score">
            You achieved <strong>{correct}</strong> out of <strong>{total}</strong> correct
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
              ? "🌟 Exceptional performance. You've demonstrated mastery of the material."
              : correct >= total * 0.6
              ? "👍 Good understanding. Review the concepts you missed for deeper learning."
              : "📚 Consider revisiting the lecture content before attempting again."}
          </p>
          <button
            type="button"
            onClick={() => {
              reset();
            }}
            className="duo-btn3d duo-btn3d-blue"
          >
            <RefreshCw size={16} />
            Retake Assessment
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
                  <CheckCircle2 size={18} className="mt-0.5 shrink-0 text-[#58CC02]" />
                  <div>
                    <p className="font-black mb-1">Correct</p>
                    <ReactMarkdown remarkPlugins={[remarkGfm]}>
                      {q.explanation}
                    </ReactMarkdown>
                  </div>
                </div>
              ) : (
                <div className="flex items-start gap-2">
                  <XCircle size={18} className="mt-0.5 shrink-0 text-[#FF4B4B]" />
                  <div>
                    <p className="font-black mb-1">Incorrect</p>
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
                }}
                disabled={selected === null}
              >
                <CheckCircle2 size={16} />
                Submit Answer
              </button>
            ) : (
              <button
                type="button"
                className="duo-btn3d duo-btn3d-blue w-full"
                onClick={() => {
                  next();
                }}
              >
                {index + 1 >= total ? (
                  <>
                    <GraduationCap size={16} />
                    Complete Assessment
                  </>
                ) : (
                  <>
                    Next Question
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
   COMPANION AVATAR SVG - Professional academic mentor
   ========================================================================= */
function CompanionAvatar({ mood, small, tiny }: { mood: Mood; small?: boolean; tiny?: boolean }) {
  const size = tiny ? 28 : small ? 44 : 70;
  const colors = {
    primary: "#2B7A62",
    secondary: "#1A4F3F",
    accent: "#C8A96E",
    light: "#E8F5F2",
    dark: "#0D2B22",
  };

  return (
    <svg
      width={size}
      height={size}
      viewBox="0 0 70 70"
      fill="none"
      xmlns="http://www.w3.org/2000/svg"
      className="duo-companion-avatar"
      style={{ display: "block" }}
    >
      {/* Body */}
      <ellipse cx="35" cy="48" rx="22" ry="18" fill={colors.primary} />
      <ellipse cx="35" cy="48" rx="18" ry="14" fill={colors.secondary} />

      {/* Head */}
      <circle cx="35" cy="30" r="24" fill={colors.primary} />
      <circle cx="35" cy="30" r="22" fill={colors.secondary} />

      {/* Ear tufts */}
      <path d="M14 22 L8 10 L18 18" fill={colors.primary} stroke={colors.secondary} strokeWidth="1.5" />
      <path d="M56 22 L62 10 L52 18" fill={colors.primary} stroke={colors.secondary} strokeWidth="1.5" />

      {/* Eyes */}
      <ellipse cx="26" cy="27" rx="7" ry="8" fill="white" />
      <ellipse cx="44" cy="27" rx="7" ry="8" fill="white" />
      <circle cx="28" cy="27" r="4" fill={colors.dark} />
      <circle cx="42" cy="27" r="4" fill={colors.dark} />
      <circle cx="29" cy="25" r="1.5" fill="white" />
      <circle cx="43" cy="25" r="1.5" fill="white" />

      {/* Glasses */}
      <rect x="17" y="20" width="18" height="15" rx="3" stroke={colors.accent} strokeWidth="2" fill="none" />
      <rect x="35" y="20" width="18" height="15" rx="3" stroke={colors.accent} strokeWidth="2" fill="none" />
      <line x1="35" y1="27" x2="35" y2="28" stroke={colors.accent} strokeWidth="2" />

      {/* Beak */}
      <path d="M32 35 L35 40 L38 35" fill={colors.accent} stroke={colors.secondary} strokeWidth="1" />

      {/* Academic mortarboard */}
      <rect x="20" y="8" width="30" height="3" rx="1.5" fill={colors.dark} />
      <rect x="27" y="5" width="16" height="6" rx="2" fill={colors.dark} />
      <rect x="33" y="2" width="4" height="4" rx="1" fill={colors.accent} />
      <line x1="33" y1="4" x2="18" y2="4" stroke={colors.accent} strokeWidth="2" />
      <circle cx="18" cy="4" r="2" fill={colors.accent} />

      {/* Expression */}
      {mood === "happy" && (
        <path d="M28 38 L32 40 L36 38" stroke="white" strokeWidth="2" strokeLinecap="round" />
      )}
      {mood === "speaking" && (
        <path d="M28 37 L30 39 L32 37" stroke="white" strokeWidth="2" strokeLinecap="round" />
      )}
      {mood === "concerned" && (
        <path d="M28 39 L32 37 L36 39" stroke="white" strokeWidth="2" strokeLinecap="round" />
      )}

      {/* Chest marking */}
      <ellipse cx="35" cy="48" rx="10" ry="6" fill={colors.light} opacity="0.3" />
      <rect x="31" y="43" width="8" height="6" rx="1" fill={colors.accent} opacity="0.8" />
      <rect x="32" y="44" width="6" height="4" rx="0.5" fill="white" opacity="0.6" />

      {!tiny && (
        <>
          <circle cx="22" cy="18" r="3" fill={colors.accent} opacity="0.4" />
          <circle cx="48" cy="18" r="3" fill={colors.accent} opacity="0.4" />
        </>
      )}
    </svg>
  );
}

/* =========================================================================
   STYLES - With normal colorful Duolingo-style buttons
   ========================================================================= */
const STYLES = `
  .duo-companion-root {
    font-family: system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
    position: fixed;
    bottom: 24px;
    right: 24px;
    z-index: 40;
  }

  .duo-companion-fab {
    position: relative;
    width: 70px;
    height: 70px;
    border-radius: 50%;
    border: none;
    background: linear-gradient(135deg, #58CC02 0%, #46A302 100%);
    box-shadow: 0 4px 16px rgba(88, 204, 2, 0.4);
    cursor: pointer;
    transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
    display: flex;
    align-items: center;
    justify-content: center;
    padding: 0;
    color: white;
    transform: scale(1);
  }

  .duo-companion-fab:hover {
    transform: scale(1.05);
    box-shadow: 0 6px 24px rgba(88, 204, 2, 0.5);
  }

  .duo-companion-fab:active {
    transform: scale(0.95);
  }

  .duo-companion-fab-open {
    box-shadow: 0 4px 16px rgba(88, 204, 2, 0.3);
    background: linear-gradient(135deg, #46A302 0%, #2B7A00 100%);
  }

  .duo-companion-fab-ring {
    position: absolute;
    inset: -4px;
    border-radius: 50%;
    border: 2px solid rgba(88, 204, 2, 0.2);
    animation: ring-pulse 2s ease-in-out infinite;
  }

  @keyframes ring-pulse {
    0%, 100% { transform: scale(1); opacity: 0.5; }
    50% { transform: scale(1.15); opacity: 0; }
  }

  .duo-companion-fab-sheen {
    position: absolute;
    inset: 0;
    border-radius: 50%;
    background: linear-gradient(135deg, rgba(255,255,255,0.2) 0%, transparent 50%);
    pointer-events: none;
  }

  .duo-companion-glow {
    position: absolute;
    inset: -8px;
    border-radius: 50%;
    background: radial-gradient(circle, rgba(88, 204, 2, 0.15) 0%, transparent 70%);
    pointer-events: none;
    animation: glow-pulse 3s ease-in-out infinite;
  }

  @keyframes glow-pulse {
    0%, 100% { opacity: 0.5; transform: scale(1); }
    50% { opacity: 1; transform: scale(1.1); }
  }

  .duo-companion-mood-idle .duo-companion-fab-sheen {
    opacity: 0.3;
  }
  .duo-companion-mood-wave .duo-companion-fab {
    animation: float-wave 2s ease-in-out infinite;
  }
  @keyframes float-wave {
    0%, 100% { transform: translateY(0); }
    50% { transform: translateY(-6px); }
  }
  .duo-companion-mood-thinking .duo-companion-fab {
    animation: think-pulse 1.2s ease-in-out infinite;
  }
  @keyframes think-pulse {
    0%, 100% { transform: scale(1); }
    50% { transform: scale(0.95); }
  }
  .duo-companion-mood-happy .duo-companion-fab {
    animation: happy-bounce 0.6s ease-in-out 3;
  }
  @keyframes happy-bounce {
    0%, 100% { transform: scale(1); }
    50% { transform: scale(1.1); }
  }
  .duo-companion-mood-celebrate .duo-companion-fab {
    animation: celebrate-spin 1s ease-in-out;
  }
  @keyframes celebrate-spin {
    0% { transform: rotate(0deg) scale(1); }
    25% { transform: rotate(-5deg) scale(1.05); }
    75% { transform: rotate(5deg) scale(1.05); }
    100% { transform: rotate(0deg) scale(1); }
  }
  .duo-companion-mood-concerned .duo-companion-fab {
    animation: concerned-shake 0.5s ease-in-out 3;
  }
  @keyframes concerned-shake {
    0%, 100% { transform: translateX(0); }
    25% { transform: translateX(-4px); }
    75% { transform: translateX(4px); }
  }
  .duo-companion-mood-speaking .duo-companion-fab {
    animation: speak-pulse 0.8s ease-in-out infinite;
  }
  @keyframes speak-pulse {
    0%, 100% { transform: scale(1); }
    50% { transform: scale(1.03); }
  }

  .duo-companion-bubble {
    position: absolute;
    bottom: 82px;
    right: 0;
    background: white;
    border-radius: 16px;
    padding: 12px 16px;
    max-width: 260px;
    font-size: 14px;
    line-height: 1.4;
    color: #1A1A1A;
    box-shadow: 0 4px 20px rgba(0,0,0,0.12);
    cursor: pointer;
    transition: all 0.3s ease;
    border: 1px solid rgba(88, 204, 2, 0.1);
    animation: bubble-in 0.4s cubic-bezier(0.34, 1.56, 0.64, 1) forwards;
  }

  @keyframes bubble-in {
    0% { opacity: 0; transform: translateY(12px) scale(0.95); }
    100% { opacity: 1; transform: translateY(0) scale(1); }
  }

  .duo-companion-bubble-hidden {
    opacity: 0;
    transform: scale(0.9);
    pointer-events: none;
  }

  .duo-companion-bubble-arrow {
    position: absolute;
    bottom: -10px;
    right: 28px;
    width: 0;
    height: 0;
    border-left: 8px solid transparent;
    border-right: 8px solid transparent;
    border-top: 10px solid white;
  }

  .duo-companion-panel {
    position: absolute;
    bottom: 88px;
    right: 0;
    width: 380px;
    max-height: 560px;
    background: white;
    border-radius: 20px;
    box-shadow: 0 8px 40px rgba(0,0,0,0.15);
    overflow: hidden;
    display: flex;
    flex-direction: column;
    animation: panel-in 0.3s cubic-bezier(0.34, 1.56, 0.64, 1) forwards;
    border: 1px solid rgba(88, 204, 2, 0.1);
  }

  @keyframes panel-in {
    0% { opacity: 0; transform: translateY(12px) scale(0.95); }
    100% { opacity: 1; transform: translateY(0) scale(1); }
  }

  .duo-companion-header {
    padding: 16px 20px;
    background: linear-gradient(135deg, #F7FFF5 0%, #E8F5E0 100%);
    border-bottom: 1px solid #D4E8CC;
    display: flex;
    align-items: center;
    justify-content: space-between;
  }

  .duo-companion-header-title {
    display: flex;
    align-items: center;
    gap: 12px;
  }

  .duo-companion-title-avatar {
    width: 36px;
    height: 36px;
    flex-shrink: 0;
  }

  .duo-companion-header-title h3 {
    font-size: 16px;
    font-weight: 600;
    color: #1A4F3F;
    margin: 0;
  }

  .duo-companion-header-title p {
    font-size: 12px;
    color: #6B7A75;
    margin: 0;
    display: flex;
    align-items: center;
  }

  .duo-companion-header-actions {
    display: flex;
    align-items: center;
    gap: 8px;
  }

  .duo-companion-mute,
  .duo-companion-close {
    background: none;
    border: none;
    padding: 6px;
    border-radius: 8px;
    cursor: pointer;
    color: #6B7A75;
    transition: all 0.2s;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .duo-companion-mute:hover,
  .duo-companion-close:hover {
    background: #E8ECEA;
    color: #1A4F3F;
  }

  .duo-companion-mute-off {
    color: #FF4B4B;
  }

  .duo-companion-tabs {
    display: flex;
    border-bottom: 1px solid #E8ECEA;
    background: #FAFCFB;
  }

  .duo-companion-tab {
    flex: 1;
    padding: 12px 8px;
    background: none;
    border: none;
    cursor: pointer;
    font-size: 13px;
    font-weight: 500;
    color: #6B7A75;
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 6px;
    transition: all 0.2s;
    position: relative;
  }

  .duo-companion-tab:hover {
    color: #58CC02;
    background: rgba(88, 204, 2, 0.05);
  }

  .duo-companion-tab-active {
    color: #58CC02;
  }

  .duo-companion-tab-active::after {
    content: '';
    position: absolute;
    bottom: 0;
    left: 20%;
    right: 20%;
    height: 3px;
    background: #58CC02;
    border-radius: 3px 3px 0 0;
  }

  .duo-companion-tab-body {
    flex: 1;
    overflow-y: auto;
    padding: 16px 20px;
    min-height: 320px;
    max-height: 380px;
  }

  .duo-companion-loading {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100%;
    text-align: center;
    padding: 24px 0;
  }

  .duo-companion-loading p {
    margin: 8px 0 4px;
    font-size: 14px;
    color: #1A4F3F;
  }

  .duo-companion-loading-sub {
    font-size: 12px !important;
    color: #8FA3A0 !important;
  }

  .duo-companion-error {
    color: #FF4B4B;
  }

  .duo-companion-error p {
    color: #FF4B4B !important;
  }

  .duo-companion-chat {
    display: flex;
    flex-direction: column;
    padding: 0;
  }

  .duo-companion-chat-area {
    flex: 1;
    padding: 16px 20px;
    overflow-y: auto;
    max-height: 280px;
  }

  .duo-companion-empty {
    text-align: center;
    padding: 20px 0;
  }

  .duo-companion-empty-owl {
    width: 56px;
    height: 56px;
    margin: 0 auto 12px;
  }

  .duo-companion-empty h4 {
    font-size: 16px;
    font-weight: 600;
    color: #1A4F3F;
    margin: 0 0 4px;
  }

  .duo-companion-empty p {
    font-size: 13px;
    color: #6B7A75;
    margin: 0 0 16px;
  }

  .duo-companion-suggest-grid {
    display: grid;
    grid-template-columns: 1fr;
    gap: 8px;
  }

  .duo-companion-suggest-btn {
    padding: 10px 14px;
    background: #F7FFF5;
    border: 2px solid #D4E8CC;
    border-radius: 12px;
    font-size: 13px;
    color: #1A4F3F;
    cursor: pointer;
    transition: all 0.2s;
    text-align: left;
    font-weight: 500;
  }

  .duo-companion-suggest-btn:hover {
    background: #E8F5E0;
    border-color: #58CC02;
    transform: translateX(4px);
  }

  .duo-msg-row {
    display: flex;
    gap: 8px;
    margin-bottom: 12px;
    align-items: flex-start;
  }

  .duo-msg-row.user {
    flex-direction: row-reverse;
  }

  .duo-avatar {
    width: 28px;
    height: 28px;
    border-radius: 50%;
    flex-shrink: 0;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .duo-avatar.owl {
    background: #F7FFF5;
  }

  .duo-avatar.user-av {
    background: #58CC02;
    color: white;
    font-size: 12px;
    font-weight: 600;
  }

  .duo-bubble {
    padding: 10px 14px;
    border-radius: 12px;
    max-width: 78%;
    font-size: 14px;
    line-height: 1.5;
  }

  .duo-bubble.owl-bubble {
    background: #F7FFF5;
    color: #1A1A1A;
    border-bottom-left-radius: 4px;
  }

  .duo-bubble.user-bubble {
    background: #58CC02;
    color: white;
    border-bottom-right-radius: 4px;
  }

  .duo-md {
    font-size: 14px;
  }

  .duo-md p {
    margin: 4px 0;
  }

  .duo-md ul, .duo-md ol {
    margin: 4px 0;
    padding-left: 20px;
  }

  .duo-md code {
    background: rgba(0,0,0,0.06);
    padding: 1px 4px;
    border-radius: 3px;
    font-size: 13px;
  }

  .duo-md pre {
    background: #1A1A1A;
    color: #E8E8E8;
    padding: 8px 12px;
    border-radius: 8px;
    overflow-x: auto;
    font-size: 13px;
  }

  .duo-typing {
    display: flex;
    gap: 8px;
    align-items: center;
    padding: 4px 0;
  }

  .duo-typing-dots {
    display: flex;
    gap: 4px;
    padding: 8px 12px;
    background: #F7FFF5;
    border-radius: 12px;
    border-bottom-left-radius: 4px;
  }

  .duo-dot {
    width: 6px;
    height: 6px;
    border-radius: 50%;
    background: #6B7A75;
    animation: dot-bounce 1.4s ease-in-out infinite;
  }

  .duo-dot:nth-child(2) { animation-delay: 0.2s; }
  .duo-dot:nth-child(3) { animation-delay: 0.4s; }

  @keyframes dot-bounce {
    0%, 60%, 100% { transform: translateY(0); }
    30% { transform: translateY(-6px); }
  }

  .duo-companion-xpbar {
    display: flex;
    align-items: center;
    gap: 8px;
    padding: 10px 20px;
    background: #FFF8E1;
    border-top: 1px solid #FFE082;
    font-size: 13px;
    color: #F57C00;
    font-weight: 500;
  }

  .duo-input-bar {
    display: flex;
    gap: 8px;
    padding: 12px 20px;
    border-top: 1px solid #E8ECEA;
    background: #FAFCFB;
  }

  .duo-input {
    flex: 1;
    padding: 10px 14px;
    border: 2px solid #E8ECEA;
    border-radius: 12px;
    font-size: 14px;
    outline: none;
    transition: all 0.2s;
    background: white;
  }

  .duo-input:focus {
    border-color: #58CC02;
    box-shadow: 0 0 0 3px rgba(88, 204, 2, 0.1);
  }

  .duo-send-btn {
    padding: 10px 14px;
    background: #58CC02;
    color: white;
    border: none;
    border-radius: 12px;
    cursor: pointer;
    transition: all 0.2s;
    display: flex;
    align-items: center;
    justify-content: center;
    font-weight: 600;
  }

  .duo-send-btn:hover:not(:disabled) {
    background: #46A302;
    transform: scale(1.05);
  }

  .duo-send-btn:disabled {
    opacity: 0.4;
    cursor: not-allowed;
  }

  .duo-companion-lecture {
    padding: 0;
  }

  .duo-companion-lecture-header {
    display: flex;
    align-items: center;
    gap: 12px;
    padding: 0 0 12px 0;
    border-bottom: 1px solid #E8ECEA;
    margin-bottom: 12px;
  }

  .duo-companion-lecture-header-icon {
    width: 40px;
    height: 40px;
    border-radius: 50%;
    background: #F7FFF5;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .duo-companion-lecture-title {
    font-size: 15px;
    font-weight: 600;
    color: #1A4F3F;
    margin: 0;
  }

  .duo-companion-lecture-subtitle {
    font-size: 12px;
    color: #6B7A75;
    margin: 0;
  }

  .duo-companion-lecture-progress {
    padding: 8px 0 12px 0;
  }

  .duo-companion-lecture-progress-row {
    display: flex;
    justify-content: space-between;
    margin-bottom: 4px;
  }

  .duo-companion-lecture-progress-label {
    font-size: 12px;
    color: #6B7A75;
  }

  .duo-progress-track {
    width: 100%;
    height: 6px;
    background: #E8ECEA;
    border-radius: 3px;
    overflow: hidden;
  }

  .duo-progress-fill {
    height: 100%;
    background: linear-gradient(90deg, #58CC02, #7CE04A);
    border-radius: 3px;
    transition: width 0.3s ease;
  }

  .duo-companion-lecture-transcript {
    max-height: 180px;
    overflow-y: auto;
    padding: 4px 0;
    margin-bottom: 12px;
  }

  .duo-companion-segment {
    display: flex;
    align-items: flex-start;
    gap: 10px;
    padding: 8px 10px;
    width: 100%;
    background: none;
    border: none;
    border-radius: 8px;
    cursor: pointer;
    transition: all 0.2s;
    text-align: left;
    font-size: 13px;
    line-height: 1.5;
    color: #1A1A1A;
  }

  .duo-companion-segment:hover {
    background: #F7FFF5;
  }

  .duo-companion-segment-number {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    min-width: 22px;
    height: 22px;
    border-radius: 50%;
    background: #E8ECEA;
    font-size: 11px;
    font-weight: 600;
    color: #6B7A75;
    flex-shrink: 0;
  }

  .duo-companion-segment-active {
    background: #F7FFF5;
    border-left: 3px solid #58CC02;
  }

  .duo-companion-segment-active .duo-companion-segment-number {
    background: #58CC02;
    color: white;
  }

  .duo-companion-segment-done {
    opacity: 0.7;
  }

  .duo-companion-segment-done .duo-companion-segment-number {
    background: #AFAFAF;
    color: white;
  }

  .duo-companion-segment-text {
    flex: 1;
  }

  .duo-companion-empty-small {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    text-align: center;
    padding: 24px 0;
    color: #6B7A75;
  }

  .duo-companion-empty-small p {
    margin: 4px 0;
  }

  .duo-companion-lecture-controls {
    padding-top: 12px;
    border-top: 1px solid #E8ECEA;
  }

  .duo-companion-lecture-main {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: 12px;
    margin-bottom: 12px;
  }

  .duo-companion-round-btn {
    width: 38px;
    height: 38px;
    border-radius: 50%;
    border: none;
    background: none;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    color: #6B7A75;
    transition: all 0.2s;
  }

  .duo-companion-round-btn:hover:not(:disabled) {
    background: #F7FFF5;
    color: #58CC02;
  }

  .duo-companion-round-btn:disabled {
    opacity: 0.3;
    cursor: not-allowed;
  }

  .duo-companion-play-btn {
    width: 52px;
    height: 52px;
    border-radius: 50%;
    border: none;
    background: #58CC02;
    color: white;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    transition: all 0.2s;
    box-shadow: 0 2px 8px rgba(88, 204, 2, 0.3);
  }

  .duo-companion-play-btn:hover:not(:disabled) {
    transform: scale(1.05);
    box-shadow: 0 4px 16px rgba(88, 204, 2, 0.4);
    background: #46A302;
  }

  .duo-companion-play-btn:disabled {
    opacity: 0.4;
    cursor: not-allowed;
  }

  .duo-companion-ghost-btn {
    background: transparent;
  }

  .duo-companion-lecture-bottom {
    display: flex;
    align-items: center;
    justify-content: space-between;
    flex-wrap: wrap;
    gap: 8px;
  }

  .duo-companion-speed-group {
    display: flex;
    align-items: center;
    gap: 4px;
  }

  .duo-companion-speed-label {
    font-size: 12px;
    color: #6B7A75;
    margin-right: 4px;
  }

  .duo-companion-speed-btn {
    padding: 4px 8px;
    border: 2px solid #E8ECEA;
    border-radius: 6px;
    background: white;
    font-size: 12px;
    color: #6B7A75;
    cursor: pointer;
    transition: all 0.2s;
    font-weight: 500;
  }

  .duo-companion-speed-btn:hover {
    background: #F7FFF5;
    border-color: #58CC02;
    color: #58CC02;
  }

  .duo-companion-speed-active {
    background: #58CC02;
    color: white;
    border-color: #58CC02;
  }

  .duo-companion-speed-active:hover {
    background: #46A302;
    color: white;
  }

  .duo-companion-lecture-hint {
    font-size: 12px;
    color: #AFAFAF;
  }

  .duo-companion-quiz {
    padding: 0;
  }

  .duo-companion-quiz-header {
    display: flex;
    align-items: center;
    gap: 12px;
    padding-bottom: 12px;
    border-bottom: 1px solid #E8ECEA;
    margin-bottom: 16px;
  }

  .duo-companion-quiz-stats {
    display: flex;
    align-items: center;
    gap: 6px;
    font-size: 13px;
    color: #1A4F3F;
    font-weight: 500;
  }

  .duo-companion-quiz-count {
    font-size: 12px;
    color: #6B7A75;
    margin-left: auto;
  }

  .duo-companion-quiz-question {
    font-size: 15px;
    line-height: 1.5;
    color: #1A1A1A;
    margin-bottom: 16px;
  }

  .duo-companion-quiz-question strong {
    color: #58CC02;
  }

  .duo-companion-quiz-options {
    display: flex;
    flex-direction: column;
    gap: 8px;
    margin-bottom: 16px;
  }

  .duo-companion-quiz-opt {
    display: flex;
    align-items: center;
    gap: 12px;
    padding: 12px 14px;
    background: #FAFCFB;
    border: 2px solid #E8ECEA;
    border-radius: 12px;
    cursor: pointer;
    transition: all 0.2s;
    text-align: left;
    font-size: 14px;
    color: #1A1A1A;
    font-weight: 500;
  }

  .duo-companion-quiz-opt:hover:not(:disabled) {
    border-color: #58CC02;
    background: #F7FFF5;
  }

  .duo-companion-quiz-opt:disabled {
    cursor: not-allowed;
  }

  .duo-companion-quiz-opt-selected {
    border-color: #58CC02;
    background: #F7FFF5;
  }

  .duo-companion-quiz-opt-correct {
    border-color: #58CC02;
    background: #F7FFF5;
  }

  .duo-companion-quiz-opt-wrong {
    border-color: #FF4B4B;
    background: #FFF5F5;
  }

  .duo-companion-quiz-opt-letter {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    width: 24px;
    height: 24px;
    border-radius: 50%;
    background: #E8ECEA;
    font-size: 12px;
    font-weight: 600;
    color: #6B7A75;
    flex-shrink: 0;
  }

  .duo-companion-quiz-opt-text {
    flex: 1;
  }

  .duo-companion-quiz-explanation {
    padding: 12px 14px;
    border-radius: 12px;
    margin-bottom: 16px;
    font-size: 14px;
    line-height: 1.5;
  }

  .duo-companion-quiz-explanation-correct {
    background: #F7FFF5;
    border: 2px solid #58CC02;
    color: #1A4F3F;
  }

  .duo-companion-quiz-explanation-wrong {
    background: #FFF5F5;
    border: 2px solid #FF4B4B;
    color: #1A1A1A;
  }

  .duo-companion-quiz-actions {
    display: flex;
    gap: 8px;
  }

  .duo-companion-quiz-finished {
    text-align: center;
    padding: 16px 0;
  }

  .duo-companion-quiz-trophy {
    font-size: 46px;
    margin-bottom: 8px;
  }

  .duo-companion-quiz-finished h4 {
    font-size: 18px;
    color: #1A4F3F;
    margin: 0 0 4px;
  }

  .duo-companion-quiz-score {
    font-size: 14px;
    color: #6B7A75;
    margin: 0 0 12px;
  }

  .duo-companion-quiz-hearts {
    display: flex;
    justify-content: center;
    gap: 4px;
    margin-bottom: 12px;
  }

  .duo-companion-quiz-feedback {
    font-size: 14px;
    color: #1A1A1A;
    margin: 0 0 16px;
    padding: 0 8px;
  }

  .duo-btn3d {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    gap: 8px;
    padding: 10px 20px;
    border: none;
    border-radius: 12px;
    font-size: 14px;
    font-weight: 700;
    cursor: pointer;
    transition: all 0.2s;
    box-shadow: 0 4px 0 rgba(0,0,0,0.15);
    text-transform: uppercase;
    letter-spacing: 0.5px;
  }

  .duo-btn3d:active {
    transform: translateY(2px);
    box-shadow: 0 2px 0 rgba(0,0,0,0.15);
  }

  .duo-btn3d:disabled {
    opacity: 0.4;
    cursor: not-allowed;
    transform: translateY(2px);
    box-shadow: 0 2px 0 rgba(0,0,0,0.15);
  }

  .duo-btn3d-green {
    background: #58CC02;
    box-shadow: 0 4px 0 #3A8A00;
  }

  .duo-btn3d-green:hover:not(:disabled) {
    background: #46A302;
    transform: translateY(-1px);
    box-shadow: 0 5px 0 #3A8A00;
  }

  .duo-btn3d-blue {
    background: #1CB0F6;
    box-shadow: 0 4px 0 #1480B8;
  }

  .duo-btn3d-blue:hover:not(:disabled) {
    background: #1899D4;
    transform: translateY(-1px);
    box-shadow: 0 5px 0 #1480B8;
  }

  .w-full { width: 100%; }

  @media (max-width: 480px) {
    .duo-companion-panel {
      width: calc(100vw - 32px);
      right: -8px;
      max-height: 480px;
    }

    .duo-companion-bubble {
      max-width: 200px;
      font-size: 13px;
      right: -4px;
    }
  }
`;