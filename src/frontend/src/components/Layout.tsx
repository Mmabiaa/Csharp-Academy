import { Link, useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { ReactNode } from "react";
import {
  BookOpen,
  Trophy,
  Home,
  User,
  Flame,
  Star,
  Code2,
  Award,
  Users,
  BarChart3,
  LayoutDashboard,
  GraduationCap,
  MessageSquare,
} from "lucide-react";

type NavItem = {
  path: string;
  label: string;
  icon: React.ComponentType<{ className?: string }>;
  roles?: Array<"student" | "teacher" | "admin">;
  authOnly?: boolean;
};

export default function Layout({ children }: { children: ReactNode }) {
  const { user, isAuthenticated, logout } = useAuth();
  const location = useLocation();
  const navigate = useNavigate();

  const navItems: NavItem[] = [
    { path: "/", label: "Learn", icon: Home },
    { path: "/courses", label: "Courses", icon: BookOpen },
    { path: "/practices", label: "Practice", icon: Code2 },
    { path: "/challenges", label: "Challenges", icon: Trophy },
    { path: "/leaderboard", label: "Leaderboard", icon: Award, authOnly: true },
    { path: "/classrooms", label: "Classrooms", icon: Users, authOnly: true },
    { path: "/progress", label: "Progress", icon: BarChart3, authOnly: true },
    { path: "/assistant", label: "AI Assistant", icon: MessageSquare, authOnly: true },
    { path: "/profile", label: "Profile", icon: User, authOnly: true },
    { path: "/teacher", label: "Teacher Portal", icon: GraduationCap, roles: ["teacher", "admin"], authOnly: true },
    { path: "/assignments", label: "Assignments", icon: BookOpen, roles: ["teacher", "admin"], authOnly: true },
    { path: "/analytics", label: "Analytics", icon: BarChart3, roles: ["teacher", "admin"], authOnly: true },
    { path: "/admin", label: "Admin Dashboard", icon: LayoutDashboard, roles: ["admin"], authOnly: true },
  ];

  const isVisible = (item: NavItem) => {
    if (item.authOnly && !isAuthenticated) return false;
    if (item.roles && !item.roles.some(r => user?.role?.toLowerCase() === r)) return false;
    return true;
  };

  return (
    <div className="min-h-screen bg-[#f7f7f7] flex">
      {/* Desktop Sidebar */}
      <aside className="hidden md:flex md:flex-col md:w-64 bg-white border-r border-[#e5e5e5] shadow-[0_0_4px_#00000010]">
        {/* Logo */}
        <div className="p-5 border-b border-[#e5e5e5]">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 rounded-full bg-[#58CC02] flex items-center justify-center text-white font-black">
              C#
            </div>
            <span className="text-xl font-black text-neutral-900">
              C# Academy
            </span>
          </div>
        </div>

        {/* Navigation */}
        <nav className="flex-1 px-3 py-4 space-y-1 overflow-y-auto">
          {navItems.filter(isVisible).map((item) => {
            const Icon = item.icon;
            const isActive = location.pathname === item.path;
            return (
              <Link
                key={item.path}
                to={item.path}
                className={`flex items-center gap-3 px-4 py-3 text-sm font-bold transition-colors ${
                  isActive
                    ? "bg-[#E6F7FF] text-[#1CB0F6] rounded-xl"
                    : "text-[#787878] hover:bg-neutral-50 rounded-xl"
                }`}
              >
                <Icon className="w-6 h-6" />
                {item.label}
              </Link>
            );
          })}
        </nav>

        {/* Bottom Stats & Actions */}
        <div className="px-3 pb-4 space-y-3 border-t border-[#e5e5e5] pt-4">
          <div className="flex items-center justify-between bg-neutral-50 rounded-xl px-4 py-3 border border-[#e5e5e5]">
            <div className="flex items-center gap-2">
              <Star className="w-5 h-5 text-[#FFC800]" fill="#FFC800" />
              <span className="font-bold text-neutral-800">
                {user?.xp ?? 0}
              </span>
            </div>
            <div className="flex items-center gap-2">
              <Flame className="w-5 h-5 text-[#FF9600]" fill="#FF9600" />
              <span className="font-bold text-neutral-800">5</span>
            </div>
          </div>
          {!isAuthenticated ? (
            <Link
              to="/login"
              className="w-full bg-[#58CC02] text-white font-bold py-3 rounded-xl shadow-[0_4px_0_#46A301] text-center text-sm"
            >
              Log in
            </Link>
          ) : (
            <button
              onClick={() => {
                logout();
                navigate("/");
              }}
              className="w-full text-xs font-bold text-[#787878] hover:text-[#FF4B4B] py-2"
            >
              Log out
            </button>
          )}
        </div>
      </aside>

      {/* Main Content Area */}
      <div className="flex-1 flex flex-col">
        {/* Mobile Header */}
        <header className="md:hidden bg-white border-b border-[#e5e5e5] sticky top-0 z-50 px-4 py-3 flex items-center justify-between">
          <div className="flex items-center gap-2">
            <div className="w-8 h-8 rounded-full bg-[#58CC02] flex items-center justify-center text-white font-black">
              C#
            </div>
            <span className="font-black text-neutral-900">C# Academy</span>
          </div>
          <div className="flex items-center gap-4">
            <div className="flex items-center gap-1">
              <Star className="w-4 h-4 text-[#FFC800]" fill="#FFC800" />
              <span className="text-xs font-black text-neutral-800">
                {user?.xp ?? 0}
              </span>
            </div>
            <div className="flex items-center gap-1">
              <Flame className="w-4 h-4 text-[#FF9600]" fill="#FF9600" />
              <span className="text-xs font-black text-neutral-800">5</span>
            </div>
          </div>
        </header>

        <main className="flex-1 max-w-5xl mx-auto w-full px-4 md:px-6 py-6 md:py-8">
          {children}
        </main>

        {/* Mobile Bottom Nav */}
        <nav className="md:hidden fixed bottom-0 left-0 right-0 bg-white border-t border-[#e5e5e5] shadow-[0_-1px_0_#00000010] z-50">
          <div className="grid grid-cols-5 gap-1 px-2 py-3">
            {navItems.filter(isVisible).slice(0, 5).map((item) => {
              const Icon = item.icon;
              const isActive = location.pathname === item.path;
              return (
                <Link
                  key={item.path}
                  to={item.path}
                  className={`flex flex-col items-center justify-center py-2 ${
                    isActive
                      ? "text-[#58CC02]"
                      : "text-[#787878]"
                  }`}
                >
                  <Icon className="w-6 h-6" />
                  <span className="text-xs font-bold mt-1">{item.label}</span>
                </Link>
              );
            })}
          </div>
        </nav>
      </div>
    </div>
  );
}
