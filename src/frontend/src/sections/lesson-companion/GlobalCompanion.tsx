import { useLocation } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import LessonCompanion from "./LessonCompanion";

type PageConfig = {
  pageTitle: string;
  pageContext: string;
  suggestedQuestions: string[];
};

const PAGE_CONFIG: Record<string, PageConfig> = {
  "/": {
    pageTitle: "Home",
    pageContext: "The main landing page of C# Academy where you can continue learning, view your streak, and browse featured courses.",
    suggestedQuestions: [
      "Where should I start learning C#?",
      "What are the best practices for learning?",
      "How does the streak system work?",
      "Tell me about C# basics",
    ],
  },
  "/courses": {
    pageTitle: "Courses",
    pageContext: "Browse and enroll in structured C# courses organized from beginner to advanced levels.",
    suggestedQuestions: [
      "Which course should I start with?",
      "How do I enroll in a course?",
      "What topics do the courses cover?",
      "Explain C# variables",
    ],
  },
  "/practices": {
    pageTitle: "Practices",
    pageContext: "Hands-on coding exercises designed to reinforce C# concepts through practical problems.",
    suggestedQuestions: [
      "How do I solve practice problems?",
      "What if I get stuck on an exercise?",
      "How do practices earn XP?",
      "Give me some C# tips",
    ],
  },
  "/challenges": {
    pageTitle: "Challenges",
    pageContext: "Competitive coding challenges to test your skills against other learners and climb the leaderboard.",
    suggestedQuestions: [
      "How do challenges work?",
      "What's the difference between practices and challenges?",
      "How can I improve my rank?",
      "Explain C# LINQ",
    ],
  },
  "/playground": {
    pageTitle: "Code Playground",
    pageContext: "Write, run, and experiment with C# code directly in the browser without any local setup.",
    suggestedQuestions: [
      "What can I do in the playground?",
      "How do I run my code?",
      "Show me a C# code example",
      "Explain C# classes",
    ],
  },
  "/assistant": {
    pageTitle: "AI Assistant",
    pageContext: "Advanced AI-powered help for C# programming questions, code debugging, and conceptual explanations.",
    suggestedQuestions: [
      "How is this different from the companion chat?",
      "What kind of questions can I ask?",
      "Can you help me debug code?",
      "Explain async/await in C#",
    ],
  },
  "/leaderboard": {
    pageTitle: "Leaderboard",
    pageContext: "See how you rank against other learners based on XP earned from lessons, practices, and challenges.",
    suggestedQuestions: [
      "How do I climb the leaderboard?",
      "What earns the most XP?",
      "How often is the leaderboard updated?",
      "Give me motivation to keep going",
    ],
  },
  "/classrooms": {
    pageTitle: "Classrooms",
    pageContext: "Join and manage virtual classrooms to learn together with peers and track group progress.",
    suggestedQuestions: [
      "How do I join a classroom?",
      "What are the benefits of classrooms?",
      "Can I create my own classroom?",
      "Explain C# loops",
    ],
  },
  "/progress": {
    pageTitle: "Progress Dashboard",
    pageContext: "View detailed analytics of your learning journey including completed lessons, earned XP, and study patterns.",
    suggestedQuestions: [
      "How is my progress calculated?",
      "How can I improve my learning efficiency?",
      "What do the different metrics mean?",
      "How do I stay consistent?",
    ],
  },
  "/profile": {
    pageTitle: "My Profile",
    pageContext: "View and edit your personal information, learning statistics, and account preferences.",
    suggestedQuestions: [
      "How do I change my profile picture?",
      "Where can I see my certificates?",
      "How do I update my password?",
      "What are your capabilities?",
    ],
  },
  "/certificates": {
    pageTitle: "Certificates",
    pageContext: "Verify and view course completion certificates that demonstrate your C# proficiency.",
    suggestedQuestions: [
      "How do I earn a certificate?",
      "Are certificates recognized?",
      "Can I share my certificate?",
      "Explain C# concepts",
    ],
  },
};

const AUTH_PATHS = ["/login", "/register", "/forgot-password", "/verify-otp", "/reset-password"];
const TEACHER_PATHS = ["/teacher", "/assignments", "/analytics"];
const ADMIN_PREFIXES = ["/admin"];
const LESSON_PREFIX = "/lessons/";
const QUIZ_SUFFIX = "/quiz";

function isLessonPath(pathname: string): boolean {
  if (pathname.startsWith(LESSON_PREFIX)) return true;
  const normalized = pathname.endsWith("/") ? pathname.slice(0, -1) : pathname;
  if (normalized.endsWith(QUIZ_SUFFIX)) return true;
  return false;
}

function isCourseDetailPath(pathname: string): boolean {
  return /^\/courses\/\d+(\/)?$/.test(pathname);
}

function isCertificateVerifyPath(pathname: string): boolean {
  return pathname.startsWith("/certificates");
}

function getPageConfig(pathname: string): PageConfig {
  if (isCourseDetailPath(pathname)) {
    return {
      pageTitle: "Course Details",
      pageContext: "View the modules, lessons, and progress for this specific C# course.",
      suggestedQuestions: [
        "How do I start a lesson?",
        "What happens when I complete a lesson?",
        "How is progress tracked?",
        "What is a class in C#?",
      ],
    };
  }

  if (isCertificateVerifyPath(pathname)) {
    return PAGE_CONFIG["/certificates"];
  }

  const exactMatch = PAGE_CONFIG[pathname];
  if (exactMatch) return exactMatch;

  return {
    pageTitle: "C# Academy",
    pageContext: "Your C# learning platform with interactive lessons, coding practice, and gamified progression.",
    suggestedQuestions: [
      "What is C#?",
      "Where should I start?",
      "Give me some learning tips",
      "What can you help me with?",
    ],
  };
}

export default function GlobalCompanion() {
  const location = useLocation();
  const { isAdmin, isTeacher } = useAuth();
  const { pathname } = location;

  if (AUTH_PATHS.includes(pathname)) return null;

  if (isAdmin || isTeacher) {
    const isAdminPath = ADMIN_PREFIXES.some((prefix) => pathname.startsWith(prefix));
    const isTeacherPath = TEACHER_PATHS.includes(pathname);
    if (isAdminPath || isTeacherPath) return null;
  }

  if (isLessonPath(pathname)) return null;

  if (isAdmin && TEACHER_PATHS.includes(pathname)) return null;
  if (isTeacher && ADMIN_PREFIXES.some((prefix) => pathname.startsWith(prefix))) return null;

  const config = getPageConfig(pathname);

  return (
    <LessonCompanion
      pageTitle={config.pageTitle}
      pageContext={config.pageContext}
      genericSuggestedQuestions={config.suggestedQuestions}
    />
  );
}
