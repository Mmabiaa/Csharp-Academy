import { Link, useLocation } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import type { ReactNode } from "react";

const navLink = (active: boolean) =>
  `px-3 py-2 rounded-lg text-sm font-medium transition-colors ${
    active ? "bg-indigo-600 text-white" : "text-slate-300 hover:bg-slate-800 hover:text-white"
  }`;

export default function Layout({ children }: { children: ReactNode }) {
  const { user, isAuthenticated, isTeacher, isAdmin, logout } = useAuth();
  const { pathname } = useLocation();

  const isActive = (path: string) => pathname === path || pathname.startsWith(path + "/");

  return (
    <div className="min-h-screen bg-slate-950 text-slate-100 flex flex-col">
      <header className="border-b border-slate-800 bg-slate-900/80 backdrop-blur sticky top-0 z-50">
        <div className="max-w-7xl mx-auto px-4 py-3 flex items-center justify-between gap-4">
          <Link to="/" className="flex items-center gap-2 shrink-0">
            <span className="w-8 h-8 rounded-lg bg-gradient-to-br from-indigo-500 to-violet-600 flex items-center justify-center text-sm font-bold">
              C#
            </span>
            <span className="text-lg font-bold tracking-tight hidden sm:block">C# Academy</span>
          </Link>

          <nav className="flex items-center gap-1 flex-wrap justify-end">
            <Link to="/courses" className={navLink(isActive("/courses"))}>Courses</Link>
            <Link to="/challenges" className={navLink(isActive("/challenges"))}>Challenges</Link>
            <Link to="/practices" className={navLink(isActive("/practices"))}>Practices</Link>
            <Link to="/playground" className={navLink(isActive("/playground"))}>Playground</Link>
            <Link to="/assistant" className={navLink(isActive("/assistant"))}>AI Tutor</Link>
            {isAuthenticated && (
              <Link to="/progress" className={navLink(isActive("/progress"))}>Progress</Link>
            )}
            {isAuthenticated && (
              <Link to="/assignments" className={navLink(isActive("/assignments"))}>Assignments</Link>
            )}
            {isTeacher && (
              <Link to="/teacher" className={navLink(isActive("/teacher"))}>Teacher</Link>
            )}
            {isAdmin && (
              <Link to="/admin" className={navLink(isActive("/admin"))}>Admin</Link>
            )}
            <Link to="/leaderboard" className={navLink(isActive("/leaderboard"))}>Leaderboard</Link>
            {isAuthenticated && (
              <Link to="/classrooms" className={navLink(isActive("/classrooms"))}>Classrooms</Link>
            )}
            {isAuthenticated ? (
              <>
                <Link to="/profile" className={navLink(isActive("/profile"))}>
                  {user?.firstName}
                </Link>
                <button
                  onClick={logout}
                  className="px-3 py-2 text-sm text-slate-400 hover:text-white"
                >
                  Logout
                </button>
              </>
            ) : (
              <>
                <Link to="/login" className={navLink(isActive("/login"))}>Login</Link>
                <Link
                  to="/register"
                  className="ml-1 px-4 py-2 rounded-lg bg-indigo-600 hover:bg-indigo-500 text-sm font-medium"
                >
                  Get Started
                </Link>
              </>
            )}
          </nav>
        </div>
      </header>

      <main className="flex-1">{children}</main>

      <footer className="border-t border-slate-800 py-6 text-center text-sm text-slate-500">
        C# Academy — Learn like Microsoft Learn, code like Replit, practice like Codecademy.
      </footer>
    </div>
  );
}
