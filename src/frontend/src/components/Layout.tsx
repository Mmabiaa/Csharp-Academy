import { Link, useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { ReactNode, useState } from "react";
import {
  BookOpen,
  Code2,
  Trophy,
  MessageSquare,
  LayoutDashboard,
  FileText,
  GraduationCap,
  User,
  LogOut,
  Menu,
  X,
  Home
} from "lucide-react";

type NavItem = {
  path: string;
  label: string;
  icon: React.ComponentType<{ className?: string }>;
  roles?: Array<"student" | "teacher" | "admin">;
  authOnly?: boolean;
};

export default function Layout({ children }: { children: ReactNode }) {
  const { user, isAuthenticated, isTeacher, isAdmin, logout } = useAuth();
  const { pathname } = useLocation();
  const navigate = useNavigate();
  const [sidebarOpen, setSidebarOpen] = useState(true);

  const isActive = (path: string) => pathname === path || pathname.startsWith(path + "/");

  const navItems: NavItem[] = [
    { path: "/", label: "Home", icon: Home },
    { path: "/courses", label: "Courses", icon: BookOpen },
    { path: "/challenges", label: "Challenges", icon: Trophy },
    { path: "/practices", label: "Practices", icon: Code2 },
    { path: "/playground", label: "Playground", icon: Code2 },
    { path: "/assistant", label: "AI Tutor", icon: MessageSquare },
    { path: "/progress", label: "Progress", icon: LayoutDashboard, authOnly: true },
    { path: "/assignments", label: "Assignments", icon: FileText, authOnly: true },
    { path: "/leaderboard", label: "Leaderboard", icon: Trophy },
    { path: "/classrooms", label: "Classrooms", icon: GraduationCap, authOnly: true },
    { path: "/profile", label: "Profile", icon: User, authOnly: true },
    { path: "/teacher", label: "Teacher Portal", icon: GraduationCap, roles: ["teacher", "admin"] },
    { path: "/admin", label: "Admin Dashboard", icon: LayoutDashboard, roles: ["admin"] }
  ];

  const isVisible = (item: NavItem) => {
    if (item.roles) {
      return item.roles.some(role => 
        (role === "student" && isAuthenticated) || 
        (role === "teacher" && isTeacher) || 
        (role === "admin" && isAdmin)
      );
    }
    if (item.authOnly) {
      return isAuthenticated;
    }
    return true;
  };

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  return (
    <div className="min-h-screen bg-white text-slate-900 flex">
      {/* Sidebar */}
      <aside className="fixed inset-y-0 left-0 z-40 w-64 bg-white border-r border-slate-200 flex flex-col">
        <div className="h-16 flex items-center px-6 border-b border-slate-200 shrink-0">
          <Link to="/" className="flex items-center gap-3">
            <div className="w-10 h-10 rounded-xl bg-gradient-to-br from-blue-500 to-indigo-600 flex items-center justify-center text-white font-bold text-lg shadow-lg shadow-blue-500/30">
              C#
            </div>
            <span className="text-xl font-bold bg-gradient-to-r from-blue-600 to-indigo-600 bg-clip-text text-transparent">
              C# Academy
            </span>
          </Link>
        </div>

        <nav className="flex-1 p-4 space-y-1 overflow-hidden flex flex-col">
          {navItems.filter(isVisible).map((item) => {
            const Icon = item.icon;
            return (
              <Link
                key={item.path}
                to={item.path}
                className={`flex items-center gap-3 px-4 py-3 rounded-xl text-sm font-medium transition-all duration-200 group ${
                  isActive(item.path)
                    ? "bg-blue-50 text-blue-700 border border-blue-100 shadow-sm"
                    : "text-slate-600 hover:bg-slate-50 hover:text-slate-900 border border-transparent"
                }`}
                onClick={() => setSidebarOpen(false)}
              >
                <Icon className={`w-5 h-5 ${
                  isActive(item.path) ? "text-blue-600" : "text-slate-400 group-hover:text-slate-600"
                }`} />
                {item.label}
              </Link>
            );
          })}
        </nav>

        {isAuthenticated && (
          <div className="p-4 border-t border-slate-200 shrink-0">
            <button
              onClick={handleLogout}
              className="flex items-center gap-3 w-full px-4 py-3 text-sm font-medium text-red-600 hover:bg-red-50 rounded-xl transition-colors"
            >
              <LogOut className="w-5 h-5" />
              Logout
            </button>
          </div>
        )}
      </aside>

      {/* Main content */}
      <div className="flex-1 flex flex-col min-w-0 ml-64">
        {/* Top bar */}
        <header className="h-16 bg-white border-b border-slate-200 sticky top-0 z-20 flex items-center px-6 gap-4">
          <button
            onClick={() => setSidebarOpen(!sidebarOpen)}
            className="p-2 hover:bg-slate-100 rounded-lg lg:hidden"
          >
            {sidebarOpen ? <X className="w-6 h-6" /> : <Menu className="w-6 h-6" />}
          </button>

          <div className="flex-1 flex items-center justify-end gap-4">
            {isAuthenticated ? (
              <div className="flex items-center gap-3">
                <div className="flex flex-col items-end">
                  <span className="text-sm font-semibold text-slate-900">
                    {user?.firstName} {user?.lastName}
                  </span>
                  <span className="text-xs text-slate-500">{user?.roles?.[0]}</span>
                </div>
                <div className="w-10 h-10 rounded-full bg-gradient-to-br from-blue-400 to-indigo-500 flex items-center justify-center text-white font-semibold shadow-md">
                  {user?.firstName?.charAt(0)}{user?.lastName?.charAt(0)}
                </div>
              </div>
            ) : (
              <div className="flex items-center gap-3">
                <Link
                  to="/login"
                  className="px-4 py-2 text-sm font-medium text-slate-700 hover:text-slate-900"
                >
                  Login
                </Link>
                <Link
                  to="/register"
                  className="px-5 py-2.5 text-sm font-semibold text-white bg-gradient-to-r from-blue-600 to-indigo-600 rounded-xl shadow-lg shadow-blue-500/30 hover:shadow-blue-500/40 hover:scale-105 transition-all"
                >
                  Get Started
                </Link>
              </div>
            )}
          </div>
        </header>

        <main className="flex-1 bg-slate-50 p-6 lg:p-8 overflow-y-auto">
          {children}
        </main>

        <footer className="bg-white border-t border-slate-200 py-8 shrink-0">
          <div className="max-w-7xl mx-auto px-8">
            <div className="flex flex-col md:flex-row justify-between items-center gap-4">
              <p className="text-sm text-slate-500">
                © 2026 C# Academy — Learn like Microsoft Learn, code like Replit, practice like Codecademy.
              </p>
              <div className="flex items-center gap-6 text-sm text-slate-400">
                <Link to="/courses" className="hover:text-slate-600">Courses</Link>
                <Link to="/challenges" className="hover:text-slate-600">Challenges</Link>
                <Link to="/assistant" className="hover:text-slate-600">AI Tutor</Link>
              </div>
            </div>
          </div>
        </footer>
      </div>
    </div>
  );
}
