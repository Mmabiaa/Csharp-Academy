import { Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";

const features = [
  {
    to: "/courses",
    title: "Structured Learning Paths",
    desc: "Courses broken into sections and topics — like Microsoft Learn and Codecademy.",
    icon: "📚",
    gradient: "from-blue-500/20 to-indigo-500/20",
  },
  {
    to: "/challenges",
    title: "Coding Challenges",
    desc: "Solve puzzles in a live editor. Instant feedback, XP rewards — Replit-style.",
    icon: "⚡",
    gradient: "from-violet-500/20 to-purple-500/20",
  },
  {
    to: "/practices",
    title: "Hands-on Practices",
    desc: "Lesson-linked exercises with hints, run & submit — learn by doing.",
    icon: "💻",
    gradient: "from-emerald-500/20 to-teal-500/20",
  },
  {
    to: "/assignments",
    title: "Assignments & Grading",
    desc: "Teachers assign work, students submit code, get graded feedback.",
    icon: "📝",
    gradient: "from-amber-500/20 to-orange-500/20",
  },
  {
    to: "/assistant",
    title: "AI Tutor (Gemini)",
    desc: "Get unstuck anytime with our Gemini-powered coding assistant.",
    icon: "🤖",
    gradient: "from-pink-500/20 to-rose-500/20",
  },
  {
    to: "/progress",
    title: "Progress Tracking",
    desc: "Visual dashboards show your journey — Khan Academy inspired.",
    icon: "📊",
    gradient: "from-cyan-500/20 to-blue-500/20",
  },
];

export default function Home() {
  const { isAuthenticated } = useAuth();

  return (
    <div>
      <section className="relative overflow-hidden border-b border-slate-800">
        <div className="absolute inset-0 bg-gradient-to-br from-indigo-950/50 via-slate-950 to-violet-950/30" />
        <div className="relative max-w-7xl mx-auto px-4 py-20 text-center">
          <p className="text-indigo-400 text-sm font-medium tracking-wide uppercase mb-4">
            Learn C# the modern way
          </p>
          <h1 className="text-4xl sm:text-5xl lg:text-6xl font-bold tracking-tight mb-6">
            Master C# from{" "}
            <span className="bg-gradient-to-r from-indigo-400 to-violet-400 bg-clip-text text-transparent">
              zero to hero
            </span>
          </h1>
          <p className="text-lg text-slate-400 max-w-2xl mx-auto mb-8">
            Interactive courses, YouTube video lessons, coding challenges, AI tutoring, and teacher tools —
            all in one professional learning platform.
          </p>
          <div className="flex flex-wrap justify-center gap-4">
            <Link
              to="/courses"
              className="px-8 py-3 rounded-xl bg-indigo-600 hover:bg-indigo-500 font-semibold transition-colors"
            >
              Start Learning
            </Link>
            <Link
              to={isAuthenticated ? "/progress" : "/register"}
              className="px-8 py-3 rounded-xl border border-slate-700 hover:border-slate-500 font-semibold transition-colors"
            >
              {isAuthenticated ? "View Progress" : "Create Free Account"}
            </Link>
          </div>
        </div>
      </section>

      <section className="max-w-7xl mx-auto px-4 py-16">
        <h2 className="text-2xl font-bold text-center mb-10">Everything you need to learn C#</h2>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {features.map((f) => (
            <Link
              key={f.to}
              to={f.to}
              className={`group rounded-2xl border border-slate-800 bg-gradient-to-br ${f.gradient} p-6 hover:border-indigo-700 transition-all hover:-translate-y-0.5`}
            >
              <span className="text-3xl">{f.icon}</span>
              <h3 className="text-lg font-semibold mt-4 group-hover:text-indigo-300 transition-colors">
                {f.title}
              </h3>
              <p className="text-slate-400 text-sm mt-2 leading-relaxed">{f.desc}</p>
            </Link>
          ))}
        </div>
      </section>

      <section className="border-t border-slate-800 bg-slate-900/50">
        <div className="max-w-7xl mx-auto px-4 py-12 text-center">
          <p className="text-slate-400 text-sm mb-2">Demo accounts</p>
          <p className="text-slate-300 text-sm">
            Teacher: <code className="text-indigo-400">teacher@academy.com</code> / Teacher123! ·{" "}
            Admin: <code className="text-indigo-400">admin@academy.com</code> / Admin123!
          </p>
        </div>
      </section>
    </div>
  );
}
