import { Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import {
  BookOpen,
  Code2,
  Trophy,
  FileText,
  MessageSquare,
  BarChart3
} from "lucide-react";

const features = [
  {
    to: "/courses",
    title: "Structured Learning Paths",
    desc: "Courses broken into sections and topics — like Microsoft Learn and Codecademy.",
    icon: BookOpen,
    gradient: "from-blue-50 to-indigo-50",
    border: "border-blue-100",
    iconBg: "bg-blue-100",
    iconColor: "text-blue-600"
  },
  {
    to: "/challenges",
    title: "Coding Challenges",
    desc: "Solve puzzles in a live editor. Instant feedback, XP rewards — Replit-style.",
    icon: Trophy,
    gradient: "from-purple-50 to-pink-50",
    border: "border-purple-100",
    iconBg: "bg-purple-100",
    iconColor: "text-purple-600"
  },
  {
    to: "/practices",
    title: "Hands-on Practices",
    desc: "Lesson-linked exercises with hints, run & submit — learn by doing.",
    icon: Code2,
    gradient: "from-emerald-50 to-teal-50",
    border: "border-emerald-100",
    iconBg: "bg-emerald-100",
    iconColor: "text-emerald-600"
  },
  {
    to: "/assignments",
    title: "Assignments & Grading",
    desc: "Teachers assign work, students submit code, get graded feedback.",
    icon: FileText,
    gradient: "from-amber-50 to-orange-50",
    border: "border-amber-100",
    iconBg: "bg-amber-100",
    iconColor: "text-amber-600"
  },
  {
    to: "/assistant",
    title: "AI Tutor",
    desc: "Get unstuck anytime with our AI-powered coding assistant.",
    icon: MessageSquare,
    gradient: "from-pink-50 to-rose-50",
    border: "border-pink-100",
    iconBg: "bg-pink-100",
    iconColor: "text-pink-600"
  },
  {
    to: "/progress",
    title: "Progress Tracking",
    desc: "Visual dashboards show your journey — Khan Academy inspired.",
    icon: BarChart3,
    gradient: "from-cyan-50 to-blue-50",
    border: "border-cyan-100",
    iconBg: "bg-cyan-100",
    iconColor: "text-cyan-600"
  },
];

export default function Home() {
  const { isAuthenticated } = useAuth();

  return (
    <div>
      {/* Hero Section */}
      <section className="relative overflow-hidden">
        <div className="absolute inset-0 bg-gradient-to-br from-blue-50 via-white to-indigo-50" />
        <div className="absolute top-0 left-1/4 w-96 h-96 bg-blue-200 rounded-full blur-3xl opacity-40" />
        <div className="absolute bottom-0 right-1/4 w-96 h-96 bg-indigo-200 rounded-full blur-3xl opacity-40" />
        
        <div className="relative max-w-7xl mx-auto px-6 py-20 lg:py-32 text-center">
          <p className="inline-flex items-center gap-2 px-4 py-2 rounded-full bg-blue-100 text-blue-700 text-sm font-medium mb-6">
            <span className="w-2 h-2 rounded-full bg-blue-500 animate-pulse" />
            Learn C# the modern way
          </p>
          
          <h1 className="text-4xl sm:text-5xl lg:text-7xl font-bold tracking-tight mb-6 text-slate-900">
            Master C# from{" "}
            <span className="bg-gradient-to-r from-blue-600 to-indigo-600 bg-clip-text text-transparent">
              zero to hero
            </span>
          </h1>
          
          <p className="text-lg sm:text-xl text-slate-600 max-w-3xl mx-auto mb-10 leading-relaxed">
            Interactive courses, video lessons, coding challenges, AI tutoring, and teacher tools —
            all in one professional learning platform.
          </p>
          
          <div className="flex flex-wrap justify-center gap-4">
            <Link
              to="/courses"
              className="px-10 py-4 rounded-2xl bg-gradient-to-r from-blue-600 to-indigo-600 text-white font-semibold text-lg shadow-xl shadow-blue-500/30 hover:shadow-blue-500/40 hover:-translate-y-1 transition-all"
            >
              Start Learning
            </Link>
            <Link
              to={isAuthenticated ? "/progress" : "/register"}
              className="px-10 py-4 rounded-2xl bg-white border-2 border-slate-200 text-slate-700 font-semibold text-lg hover:border-blue-200 hover:bg-blue-50 hover:text-blue-700 transition-all"
            >
              {isAuthenticated ? "View Progress" : "Create Free Account"}
            </Link>
          </div>
        </div>
      </section>

      {/* Features Section */}
      <section className="max-w-7xl mx-auto px-6 py-20">
        <div className="text-center mb-16">
          <h2 className="text-3xl sm:text-4xl font-bold text-slate-900 mb-4">
            Everything you need to learn C#
          </h2>
          <p className="text-lg text-slate-600 max-w-2xl mx-auto">
            Built with the best practices from Microsoft Learn, Codecademy, and Replit.
          </p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {features.map((f) => {
            const Icon = f.icon;
            return (
              <Link
                key={f.to}
                to={f.to}
                className={`group rounded-2xl border ${f.border} bg-gradient-to-br ${f.gradient} p-8 hover:shadow-xl hover:-translate-y-2 transition-all duration-300`}
              >
                <div className={`w-14 h-14 rounded-2xl ${f.iconBg} flex items-center justify-center mb-6 group-hover:scale-110 transition-transform`}>
                  <Icon className={`w-7 h-7 ${f.iconColor}`} />
                </div>
                <h3 className="text-xl font-bold text-slate-900 mb-3 group-hover:text-slate-800">
                  {f.title}
                </h3>
                <p className="text-slate-600 leading-relaxed">{f.desc}</p>
              </Link>
            );
          })}
        </div>
      </section>

      {/* Demo Section */}
      <section className="border-t border-slate-200 bg-white">
        <div className="max-w-7xl mx-auto px-6 py-16 text-center">
          <p className="text-slate-500 text-sm mb-3">Demo accounts</p>
          <div className="flex flex-col sm:flex-row justify-center items-center gap-6">
            <div className="bg-slate-50 border border-slate-200 rounded-2xl px-8 py-5">
              <p className="text-sm text-slate-600 mb-1">Teacher</p>
              <p className="text-slate-900 font-mono text-sm">
                teacher@academy.com / Teacher123!
              </p>
            </div>
            <div className="bg-slate-50 border border-slate-200 rounded-2xl px-8 py-5">
              <p className="text-sm text-slate-600 mb-1">Admin</p>
              <p className="text-slate-900 font-mono text-sm">
                admin@academy.com / Admin123!
              </p>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}
