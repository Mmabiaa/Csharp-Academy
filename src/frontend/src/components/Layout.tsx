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
  LogOut,
  Gem,
} from "lucide-react";

type NavItem = {
  path: string;
  label: string;
  icon: React.ComponentType<{ className?: string }>;
  roles?: Array<"student" | "teacher" | "admin">;
  authOnly?: boolean;
};

export default function Layout({ children }: { children: ReactNode }) {
  const { user, isAuthenticated, logout, isAdmin, isTeacher } = useAuth();
  const location = useLocation();
  const navigate = useNavigate();

  const navItems: NavItem[] = [
    { path: "/", label: "Learn", icon: Home, roles: ["student"] },
    { path: "/courses", label: "Courses", icon: BookOpen, roles: ["student"] },
    { path: "/practices", label: "Practice", icon: Code2, roles: ["student"] },
    { path: "/challenges", label: "Challenges", icon: Trophy, roles: ["student"] },
    { path: "/leaderboard", label: "Leaderboard", icon: Award, authOnly: true, roles: ["student"] },
    { path: "/classrooms", label: "Classrooms", icon: Users, authOnly: true, roles: ["student"] },
    { path: "/playground", label: "PlayGround", icon: Flame, authOnly: true, roles: ["student"] },
    { path: "/progress", label: "Progress", icon: BarChart3, authOnly: true, roles: ["student"] },
    { path: "/assistant", label: "AI Assistant", icon: MessageSquare, authOnly: true, roles: ["student"] },
    { path: "/profile", label: "Profile", icon: User, authOnly: true },
    { path: "/teacher", label: "Teacher Portal", icon: GraduationCap, roles: ["teacher"], authOnly: true },
    { path: "/assignments", label: "Assignments", icon: BookOpen, roles: ["teacher"], authOnly: true },
    { path: "/analytics", label: "Analytics", icon: BarChart3, roles: ["teacher"], authOnly: true },
    { path: "/admin", label: "Admin Dashboard", icon: LayoutDashboard, roles: ["admin"], authOnly: true },
  ];

  const isVisible = (item: NavItem) => {
    if (item.authOnly && !isAuthenticated) return false;
    if (!item.roles) return true; // Profile and other common items

    if (isAdmin) return item.roles.includes("admin");
    if (isTeacher) return item.roles.includes("teacher");

    // Default to student items for students or guests
    return item.roles.includes("student");
  };

  return (
    <div className="min-h-screen bg-[#f7f7f7] flex">
      {/* Desktop Sidebar */}
      <aside className="hidden md:flex md:flex-col md:w-64 md:fixed md:top-0 md:left-0 md:bottom-0 bg-white border-r-2 border-[#e5e5e5]">
        {/* Logo */}
        <div className="p-5 border-b-2 border-[#e5e5e5]">
          <div className="flex items-center gap-3">
            <div className="w-11 h-11 rounded-2xl bg-[#58CC02] flex items-center justify-center text-white font-black shadow-[0_3px_0_#46A302]">
              C#
            </div>
            <span className="text-xl font-black text-neutral-900">C# Academy</span>
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
                className={`flex items-center gap-3 px-4 py-3 text-sm font-black uppercase tracking-wide rounded-2xl transition-colors ${isActive
                  ? "bg-[#DDF4FF] text-[#1CB0F6]"
                  : "text-[#777777] hover:bg-neutral-50"
                  }`}
              >
                <Icon className={`w-6 h-6 ${isActive ? "" : "text-neutral-400"}`} strokeWidth={isActive ? 2.5 : 2} />
                {item.label}
              </Link>
            );
          })}
        </nav>

        {/* Bottom Stats & Actions */}
        <div className="px-3 pb-4 space-y-3 border-t-2 border-[#e5e5e5] pt-4">
          <div className="flex items-center justify-between gap-2">
            <div className="flex-1 flex items-center justify-center gap-1.5 bg-[#FFF8E1] rounded-xl px-3 py-2 border-2 border-[#FFC800]/30">
              <Gem className="w-4 h-4 text-[#FFC800]" fill="#FFC800" />
              <span className="font-black text-[#946800] text-sm">{user?.xp ?? 0}</span>
            </div>
            <div className="flex-1 flex items-center justify-center gap-1.5 bg-[#FFF1E0] rounded-xl px-3 py-2 border-2 border-[#FF9600]/30">
              <Flame className="w-4 h-4 text-[#FF9600]" fill="#FF9600" />
              <span className="font-black text-[#CC6E00] text-sm">5</span>
            </div>
          </div>
          {!isAuthenticated ? (
            <Link to="/login" className="duo-btn3d duo-btn3d-green w-full block text-center">
              Log in
            </Link>
          ) : (
            <button
              onClick={() => {
                logout();
                navigate("/");
              }}
              className="w-full flex items-center justify-center gap-2 text-xs font-black uppercase tracking-wide text-[#777777] hover:text-[#FF4B4B] py-2 transition-colors"
            >
              <LogOut className="w-4 h-4" />
              Log out
            </button>
          )}
        </div>
      </aside>

      {/* Main Content Area */}
      <div className="flex-1 flex flex-col md:ml-64">
        {/* Mobile Header */}
        <header className="md:hidden bg-white border-b-2 border-[#e5e5e5] sticky top-0 z-50 px-4 py-3 flex items-center justify-between">
          <div className="flex items-center gap-2">
            <div className="w-9 h-9 rounded-xl bg-[#58CC02] flex items-center justify-center text-white font-black shadow-[0_3px_0_#46A302]">
              C#
            </div>
            <span className="font-black text-neutral-900">C# Academy</span>
          </div>
          <div className="flex items-center gap-2">
            <div className="flex items-center gap-1 bg-[#FFF8E1] rounded-lg px-2 py-1">
              <Gem className="w-4 h-4 text-[#FFC800]" fill="#FFC800" />
              <span className="text-xs font-black text-[#946800]">{user?.xp ?? 0}</span>
            </div>
            <div className="flex items-center gap-1 bg-[#FFF1E0] rounded-lg px-2 py-1">
              <Flame className="w-4 h-4 text-[#FF9600]" fill="#FF9600" />
              <span className="text-xs font-black text-[#CC6E00]">5</span>
            </div>
          </div>
        </header>

        <main className="flex-1 max-w-5xl mx-auto w-full px-4 md:px-6 py-6 md:py-8">
          {children}
        </main>

        {/* Mobile Bottom Nav */}
        <nav className="md:hidden fixed bottom-0 left-0 right-0 bg-white border-t-2 border-[#e5e5e5] z-50">
          <div className="grid grid-cols-5 gap-1 px-2 py-2">
            {navItems.filter(isVisible).slice(0, 5).map((item) => {
              const Icon = item.icon;
              const isActive = location.pathname === item.path;
              return (
                <Link
                  key={item.path}
                  to={item.path}
                  className={`flex flex-col items-center justify-center py-2 rounded-xl transition-colors ${isActive ? "text-[#58CC02]" : "text-[#AFAFAF]"
                    }`}
                >
                  <Icon className="w-6 h-6" strokeWidth={isActive ? 2.5 : 2} />
                  <span className="text-[10px] font-black mt-1 uppercase tracking-wide">
                    {item.label}
                  </span>
                </Link>
              );
            })}
          </div>
        </nav>
      </div>
    </div>
  );
}