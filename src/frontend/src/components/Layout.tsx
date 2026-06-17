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
  Home,
  BarChart3,
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
  const location = useLocation();
  const navigate = useNavigate();
  const [sidebarOpen, setSidebarOpen] = useState(true);

  const navItems: NavItem[] = [
    { path: "/", label: "Dashboard", icon: Home },
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
    { path: "/admin", label: "Admin Dashboard", icon: LayoutDashboard, roles: ["admin"] },
  ];

  const isVisible = (item: NavItem) => {
    if (item.roles) {
      return item.roles.some((role) =>
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

  const getCurrentDate = () => {
    const now = new Date();
    return `${now.getDate()} ${now.toLocaleString("default", {
      month: "short",
    })} ${now.getFullYear()}`;
  };

  return (
    <div className="min-h-screen bg-gray-50 text-black flex">
      {/* Sidebar */}
      <aside className="fixed inset-y-0 left-0 z-40 w-64 bg-black text-white flex flex-col">
        <div className="h-24 flex items-center px-8 border-b border-gray-800 shrink-0">
          <Link to="/" className="flex items-center gap-3">
            <div className="w-12 h-12 rounded bg-white flex items-center justify-center text-black font-bold text-2xl shadow-lg">
              C#
            </div>
            <span className="text-2xl font-serif font-bold tracking-tight">
              Csharp Academy
            </span>
          </Link>
        </div>

        <nav className="flex-1 px-4 py-8 space-y-1 overflow-hidden flex flex-col">
          <div className="text-xs uppercase tracking-widest text-gray-500 px-4 mb-4">
            Menu
          </div>
          {navItems.filter(isVisible).map((item) => {
            const Icon = item.icon;
            const isActive = location.pathname === item.path;
            return (
              <Link
                key={item.path}
                to={item.path}
                className={`sidebar-link !px-4 !py-2.5 ${
                  isActive ? "!bg-white !text-black" : "text-gray-300 hover:bg-gray-900 hover:text-white"
                }`}
              >
                <Icon className="w-5 h-5" />
                {item.label}
              </Link>
            );
          })}
        </nav>

        {isAuthenticated && (
          <div className="p-6 border-t border-gray-800 shrink-0">
            <div className="flex items-center gap-3 mb-4">
              <div className="w-10 h-10 rounded-full bg-white flex items-center justify-center text-black font-semibold">
                {user?.firstName?.charAt(0)}
                {user?.lastName?.charAt(0)}
              </div>
              <div className="flex flex-col">
                <span className="text-sm font-medium">
                  {user?.firstName} {user?.lastName}
                </span>
                <span className="text-xs text-gray-400 uppercase tracking-wider">
                  {user?.roles?.[0]}
                </span>
              </div>
            </div>
            <button
              onClick={handleLogout}
              className="flex items-center gap-3 w-full px-4 py-2.5 text-sm font-medium text-gray-300 hover:text-white hover:bg-gray-900 rounded-md transition-all"
            >
              <LogOut className="w-5 h-5" />
              Sign out
            </button>
          </div>
        )}
      </aside>

      {/* Main Content Area */}
      <div className="flex-1 flex flex-col min-w-0 ml-64">
        {/* Top Header */}
        <header className="h-20 bg-white border-b border-gray-200 sticky top-0 z-20 flex items-center justify-between px-12">
          <div className="flex items-center gap-4">
            <button
              onClick={() => setSidebarOpen(!sidebarOpen)}
              className="p-2 hover:bg-gray-100 rounded lg:hidden"
            >
              {sidebarOpen ? <X className="w-6 h-6" /> : <Menu className="w-6 h-6" />}
            </button>
          </div>

          <div className="flex items-center gap-8">
            <div className="text-right hidden sm:block">
              <span className="text-4xl font-serif font-bold">{new Date().getDate()}</span>
              <div className="text-xs uppercase tracking-widest text-gray-500">
                {new Date().toLocaleString("default", { month: "short" })} {new Date().getFullYear()}
              </div>
            </div>

            {!isAuthenticated && (
              <div className="flex items-center gap-3">
                <Link to="/login" className="text-sm font-medium text-gray-600 hover:text-black">
                  Sign in
                </Link>
                <Link
                  to="/register"
                  className="px-5 py-2.5 text-sm font-semibold text-white bg-black rounded-md hover:bg-gray-800 transition-all"
                >
                  Get started
                </Link>
              </div>
            )}
          </div>
        </header>

        {/* Page Content */}
        <main className="flex-1 p-12 overflow-y-auto">
          {children}
        </main>
      </div>
    </div>
  );
}
