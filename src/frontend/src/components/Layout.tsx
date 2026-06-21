import { Link, useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import { ReactNode } from "react";
import UserAvatar from "./UserAvatar";
import NotificationOverlay from "./NotificationOverlay";
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
  Sparkles,
} from "lucide-react";

type NavItem = {
  path: string;
  label: string;
  icon: React.ComponentType<{ className?: string }>;
  roles?: Array<"student" | "teacher" | "admin">;
  authOnly?: boolean;
};

type Accent = { bg: string; shadow: string; tint: string; text: string };

// Each route gets its own Duolingo-style colour, mirrored from the accent
// colours already used on that route's own page (e.g. /admin/courses uses
// the same blue as the course icon on the Manage Courses screen).
const navAccent: Record<string, Accent> = {
  "/": { bg: "bg-[#58CC02]", shadow: "shadow-[0_3px_0_#46A302]", tint: "bg-[#58CC02]/10", text: "text-[#46A302]" },
  "/courses": { bg: "bg-[#1CB0F6]", shadow: "shadow-[0_3px_0_#1899D6]", tint: "bg-[#1CB0F6]/10", text: "text-[#1899D6]" },
  "/practices": { bg: "bg-[#CE82FF]", shadow: "shadow-[0_3px_0_#A568CC]", tint: "bg-[#CE82FF]/10", text: "text-[#A568CC]" },
  "/challenges": { bg: "bg-[#FFC800]", shadow: "shadow-[0_3px_0_#E6B400]", tint: "bg-[#FFC800]/10", text: "text-[#946800]" },
  "/leaderboard": { bg: "bg-[#FF4B4B]", shadow: "shadow-[0_3px_0_#CC3A3A]", tint: "bg-[#FF4B4B]/10", text: "text-[#CC3A3A]" },
  "/classrooms": { bg: "bg-[#2EC4B6]", shadow: "shadow-[0_3px_0_#21A395]", tint: "bg-[#2EC4B6]/10", text: "text-[#1E8C82]" },
  "/playground": { bg: "bg-[#FF9600]", shadow: "shadow-[0_3px_0_#CC7800]", tint: "bg-[#FF9600]/10", text: "text-[#CC7800]" },
  "/progress": { bg: "bg-[#5B6EF5]", shadow: "shadow-[0_3px_0_#4453C9]", tint: "bg-[#5B6EF5]/10", text: "text-[#4453C9]" },
  "/assistant": { bg: "bg-[#FF6FAE]", shadow: "shadow-[0_3px_0_#E0488C]", tint: "bg-[#FF6FAE]/10", text: "text-[#E0488C]" },
  "/profile": { bg: "bg-[#1CB0F6]", shadow: "shadow-[0_3px_0_#1899D6]", tint: "bg-[#1CB0F6]/10", text: "text-[#1899D6]" },
  "/teacher": { bg: "bg-[#FFC800]", shadow: "shadow-[0_3px_0_#E6B400]", tint: "bg-[#FFC800]/10", text: "text-[#946800]" },
  "/assignments": { bg: "bg-[#1CB0F6]", shadow: "shadow-[0_3px_0_#1899D6]", tint: "bg-[#1CB0F6]/10", text: "text-[#1899D6]" },
  "/analytics": { bg: "bg-[#FF4B4B]", shadow: "shadow-[0_3px_0_#CC3A3A]", tint: "bg-[#FF4B4B]/10", text: "text-[#CC3A3A]" },
  "/admin": { bg: "bg-[#534AB7]", shadow: "shadow-[0_3px_0_#433A93]", tint: "bg-[#534AB7]/10", text: "text-[#534AB7]" },
  "/admin/courses": { bg: "bg-[#1CB0F6]", shadow: "shadow-[0_3px_0_#1899D6]", tint: "bg-[#1CB0F6]/10", text: "text-[#1899D6]" },
  "/admin/challenges": { bg: "bg-[#534AB7]", shadow: "shadow-[0_3px_0_#433A93]", tint: "bg-[#534AB7]/10", text: "text-[#534AB7]" },
  "/admin/practices": { bg: "bg-[#46A302]", shadow: "shadow-[0_3px_0_#3D8F01]", tint: "bg-[#46A302]/10", text: "text-[#3D8F01]" },
};
const fallbackAccent: Accent = { bg: "bg-[#1CB0F6]", shadow: "shadow-[0_3px_0_#1899D6]", tint: "bg-[#1CB0F6]/10", text: "text-[#1899D6]" };

export default function Layout({ children }: { children: ReactNode }) {
  const { user, isAuthenticated, logout, isAdmin, isTeacher } = useAuth();
  const location = useLocation();
  const navigate = useNavigate();

  const navItems: NavItem[] = [
    { path: "/", label: "Learn", icon: Home, roles: ["student", "teacher", "admin"] },
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
    { path: "/classrooms", label: "Classrooms", icon: Users, roles: ["teacher"], authOnly: true },
    { path: "/analytics", label: "Analytics", icon: BarChart3, roles: ["teacher"], authOnly: true },
    { path: "/admin", label: "Admin", icon: LayoutDashboard, roles: ["admin"], authOnly: true },
    { path: "/admin/courses", label: "Manage Courses", icon: BookOpen, roles: ["admin"], authOnly: true },
    { path: "/admin/challenges", label: "Manage Challenges", icon: Trophy, roles: ["admin"], authOnly: true },
    { path: "/admin/practices", label: "Manage Practices", icon: Code2, roles: ["admin"], authOnly: true },
  ];

  const isVisible = (item: NavItem) => {
    if (item.authOnly && !isAuthenticated) return false;
    if (!item.roles) return true; // Profile and other common items

    const hasRole = (role: string) => item.roles?.some(r => r.toLowerCase() === role.toLowerCase());

    if (isAdmin) return hasRole("admin");
    if (isTeacher) return hasRole("teacher");

    // Default to student items for students or guests
    return hasRole("student");
  };

  const roleLabel = isAdmin ? "Admin" : isTeacher ? "Teacher" : "Student";

  return (
    <div className="min-h-screen bg-[#f7f7f7] flex">
      <style>{`
        @keyframes duo-logo-breathe {
          0%, 100% { transform: scale(1); }
          50% { transform: scale(1.06); }
        }
        @keyframes duo-nav-bounce {
          0%, 100% { transform: translateY(0) scale(1); }
          50% { transform: translateY(-2px) scale(1.12); }
        }
        @keyframes duo-nav-dot-pulse {
          0%, 100% { transform: scale(1); opacity: 1; }
          50% { transform: scale(1.7); opacity: 0.45; }
        }
        @keyframes duo-flame-flicker {
          0%, 100% { transform: rotate(-7deg) scale(1); }
          25% { transform: rotate(8deg) scale(1.12); }
          60% { transform: rotate(-4deg) scale(1.06); }
        }
        @keyframes duo-gem-glint {
          0%, 100% { transform: scale(1) rotate(0deg); }
          45%      { transform: scale(1.2) rotate(-10deg); }
          75%      { transform: scale(0.95) rotate(5deg); }
        }
        @keyframes duo-sparkle-pop {
          0%, 100% { transform: scale(1) rotate(0deg); opacity: 1; }
          40% { transform: scale(1.3) rotate(15deg); opacity: 0.85; }
          70% { transform: scale(0.9) rotate(-8deg); opacity: 1; }
        }
        @keyframes duo-points-shine {
          0%   { left: -60%; }
          55%  { left: 130%; }
          100% { left: 130%; }
        }
        @keyframes duo-streak-glow-pulse {
          0%, 100% { box-shadow: 0 0 0 0 rgba(255, 150, 0, 0.35); }
          50%      { box-shadow: 0 0 0 5px rgba(255, 150, 0, 0.12); }
        }
        @keyframes duo-profile-ring-pulse {
          0%   { transform: scale(0.9); opacity: 0.5; }
          100% { transform: scale(1.35); opacity: 0; }
        }
        @keyframes duo-online-pulse {
          0%, 100% { box-shadow: 0 0 0 0 rgba(88, 204, 2, 0.45); }
          50%      { box-shadow: 0 0 0 3px rgba(88, 204, 2, 0.18); }
        }

        .duo-logo-breathe  { animation: duo-logo-breathe 3.2s ease-in-out infinite; }
        .duo-nav-icon-loop { animation: duo-nav-bounce 2.4s ease-in-out infinite; }
        .duo-nav-dot       { animation: duo-nav-dot-pulse 1.6s ease-in-out infinite; }
        .duo-flame-flicker { animation: duo-flame-flicker 2.2s ease-in-out infinite; }
        .duo-gem-glint     { animation: duo-gem-glint 2s ease-in-out infinite; }
        .duo-sparkle-icon  { animation: duo-sparkle-pop 2.4s ease-in-out infinite; }
        .duo-streak-glow   { animation: duo-streak-glow-pulse 1.8s ease-in-out infinite; }
        .duo-online-pulse  { animation: duo-online-pulse 1.8s ease-in-out infinite; }

        .duo-profile-ring {
          position: absolute;
          inset: -3px;
          border-radius: 9999px;
          border: 2px solid #1CB0F6;
          animation: duo-profile-ring-pulse 2.2s ease-out infinite;
          pointer-events: none;
        }

        /* Shimmer sweep across the points chip */
        .duo-points-chip::after {
          content: "";
          position: absolute;
          top: 0;
          left: -60%;
          width: 35%;
          height: 100%;
          background: linear-gradient(120deg, transparent, rgba(255, 255, 255, 0.75), transparent);
          transform: skewX(-20deg);
          animation: duo-points-shine 3.2s ease-in-out infinite;
        }

        @media (prefers-reduced-motion: reduce) {
          .duo-logo-breathe, .duo-nav-icon-loop, .duo-nav-dot,
          .duo-flame-flicker, .duo-gem-glint, .duo-sparkle-icon,
          .duo-streak-glow, .duo-online-pulse, .duo-profile-ring,
          .duo-points-chip::after {
            animation: none;
          }
        }
      `}</style>

      {/* Desktop Sidebar */}
      <aside className="hidden md:flex md:flex-col md:w-64 md:fixed md:top-0 md:left-0 md:bottom-0 bg-gradient-to-b from-white to-[#fafbff] border-r-2 border-[#e5e5e5]">
        {/* Logo */}
        <div className="p-5 border-b-2 border-[#e5e5e5] relative overflow-hidden">
          <GraduationCap className="absolute -right-3 -top-4 w-20 h-20 text-[#58CC02] opacity-[0.06] rotate-12 pointer-events-none" />
          <div className="relative flex items-center gap-3">
            <div className="w-11 h-11 rounded-2xl bg-white flex items-center justify-center shadow-[0_3px_0_#46A302] duo-logo-breathe overflow-hidden p-1.5 shrink-0">
              <img src="/logo.png" alt="C# Academy" className="w-full h-full object-contain" />
            </div>
            <span className="text-xl font-black text-neutral-900">C# Academy</span>
          </div>
        </div>

        {/* Navigation */}
        <nav className="flex-1 px-3 py-4 space-y-1 overflow-y-auto">
          {navItems.filter(isVisible).map((item, idx) => {
            const Icon = item.icon;
            const isActive = location.pathname === item.path;
            const accent = navAccent[item.path] ?? fallbackAccent;
            return (
              <Link
                key={item.path}
                to={item.path}
                className={`group flex items-center gap-3 px-3 py-2.5 text-sm font-black uppercase tracking-wide rounded-2xl transition-all duration-150 ${isActive
                  ? `${accent.tint} ${accent.text}`
                  : "text-neutral-400 hover:bg-neutral-50 hover:text-neutral-600 hover:translate-x-0.5"
                  }`}
              >
                <span
                  className={`w-9 h-9 rounded-xl flex items-center justify-center shrink-0 transition-all duration-150 ${isActive
                    ? `${accent.bg} ${accent.shadow}`
                    : "bg-neutral-100 group-hover:bg-neutral-200"
                    }`}
                >
                  <Icon
                    className={`w-[18px] h-[18px] duo-nav-icon-loop ${isActive ? "text-white" : "text-neutral-400 group-hover:text-neutral-600"}`}
                    strokeWidth={isActive ? 2.5 : 2}
                    style={{ animationDelay: `${idx * 0.12}s` }}
                  />
                </span>
                <span className="truncate">{item.label}</span>
                {isActive && <span className={`ml-auto w-1.5 h-1.5 rounded-full shrink-0 ${accent.bg} duo-nav-dot`} />}
              </Link>
            );
          })}
        </nav>

        {/* Bottom Stats & Actions */}
        <div className="px-3 pb-4 space-y-3 border-t-2 border-[#e5e5e5] pt-4 relative overflow-hidden">
          <Star className="absolute -left-5 -bottom-5 w-24 h-24 text-[#FFC800] opacity-[0.05] -rotate-12 pointer-events-none" />

          {isAuthenticated && (
            <Link
              to="/profile"
              className="relative flex items-center gap-2.5 px-2.5 py-2.5 rounded-2xl bg-gradient-to-br from-[#F3F8FF] to-[#EAF6FF] border-2 border-[#1CB0F6]/15 hover:border-[#1CB0F6]/40 hover:-translate-y-0.5 transition-all"
            >
              <span className="relative shrink-0">
                <span className="duo-profile-ring" />
                <UserAvatar
                  profileImageUrl={user?.profileImageUrl}
                  firstName={user?.firstName}
                  lastName={user?.lastName}
                  size="sm"
                  className="relative shrink-0 ring-2 ring-white"
                />
                <span className="absolute -bottom-0.5 -right-0.5 w-3 h-3 rounded-full bg-[#58CC02] border-2 border-white duo-online-pulse" />
              </span>
              <div className="min-w-0 flex-1">
                <p className="text-xs font-black text-neutral-800 truncate">
                  {user?.firstName ? `${user.firstName} ${user.lastName ?? ""}`.trim() : user?.email}
                </p>
                <p className="text-[10px] font-bold text-[#1899D6] uppercase tracking-wide flex items-center gap-1">
                  <Sparkles className="w-2.5 h-2.5 duo-sparkle-icon" />
                  {roleLabel}
                </p>
              </div>
            </Link>
          )}

          <div className="relative flex items-center justify-between gap-2">
            <div className="flex-1 relative flex items-center justify-center gap-1.5 bg-gradient-to-br from-[#FFF8E1] to-[#FFF1C2] rounded-xl px-3 py-2 border-2 border-[#FFC800]/30 overflow-hidden duo-points-chip">
              <Gem className="w-4 h-4 text-[#FFC800] duo-gem-glint" fill="#FFC800" />
              <span className="font-black text-[#946800] text-sm">{user?.xp ?? 0}</span>
            </div>
            <div className="flex-1 relative flex items-center justify-center gap-1.5 bg-gradient-to-br from-[#FFF1E0] to-[#FFE0BD] rounded-xl px-3 py-2 border-2 border-[#FF9600]/30 overflow-hidden">
              <span className="relative flex items-center justify-center w-5 h-5 rounded-full bg-[#FF9600]/15 duo-streak-glow shrink-0">
                <Flame className="w-3.5 h-3.5 text-[#FF9600] duo-flame-flicker" fill="#FF9600" />
              </span>
              <span className="font-black text-[#CC6E00] text-sm">{user?.currentStreak ?? 0}</span>
            </div>
          </div>
          {!isAuthenticated ? (
            <Link to="/login" className="duo-btn3d duo-btn3d-green w-full block text-center relative">
              Log in
            </Link>
          ) : (
            <button
              onClick={() => {
                logout();
                navigate("/");
              }}
              className="duo-btn3d w-full !py-2.5 !px-4 !text-xs bg-[#FF4B4B] text-white shadow-[0_3px_0_#CC3A3A] hover:bg-[#FF3737] active:shadow-[0_0_0_#CC3A3A]"
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
            <div className="w-9 h-9 rounded-xl bg-white flex items-center justify-center shadow-[0_3px_0_#46A302] duo-logo-breathe overflow-hidden p-1 shrink-0">
              <img src="/logo.png" alt="C# Academy" className="w-full h-full object-contain" />
            </div>
            <span className="font-black text-neutral-900">C# Academy</span>
          </div>
          <div className="flex items-center gap-2">
            <div className="relative flex items-center gap-1 bg-gradient-to-br from-[#FFF8E1] to-[#FFF1C2] rounded-lg px-2 py-1 overflow-hidden duo-points-chip">
              <Gem className="w-4 h-4 text-[#FFC800] duo-gem-glint" fill="#FFC800" />
              <span className="text-xs font-black text-[#946800]">{user?.xp ?? 0}</span>
            </div>
            <div className="flex items-center gap-1 bg-gradient-to-br from-[#FFF1E0] to-[#FFE0BD] rounded-lg px-2 py-1">
              <span className="relative flex items-center justify-center w-4 h-4 rounded-full bg-[#FF9600]/15 duo-streak-glow shrink-0">
                <Flame className="w-3 h-3 text-[#FF9600] duo-flame-flicker" fill="#FF9600" />
              </span>
              <span className="text-xs font-black text-[#CC6E00]">{user?.currentStreak ?? 0}</span>
            </div>
            {isAuthenticated && (
              <Link to="/profile" className="relative shrink-0">
                <span className="duo-profile-ring" />
                <UserAvatar
                  profileImageUrl={user?.profileImageUrl}
                  firstName={user?.firstName}
                  lastName={user?.lastName}
                  size="sm"
                  className="relative ring-2 ring-white"
                />
                <span className="absolute -bottom-0.5 -right-0.5 w-2.5 h-2.5 rounded-full bg-[#58CC02] border-2 border-white duo-online-pulse" />
              </Link>
            )}
          </div>
        </header>

        <main className="flex-1 max-w-5xl mx-auto w-full px-4 md:px-6 py-6 md:py-8">
          {children}
        </main>

        <NotificationOverlay />

        {/* Mobile Bottom Nav */}
        <nav className="md:hidden fixed bottom-0 left-0 right-0 bg-white border-t-2 border-[#e5e5e5] z-50">
          <div className="grid grid-cols-5 gap-1 px-2 py-2">
            {navItems.filter(isVisible).slice(0, 5).map((item, idx) => {
              const Icon = item.icon;
              const isActive = location.pathname === item.path;
              const accent = navAccent[item.path] ?? fallbackAccent;
              return (
                <Link
                  key={item.path}
                  to={item.path}
                  className="flex flex-col items-center justify-center py-1.5 rounded-xl transition-all"
                >
                  <span
                    className={`w-8 h-8 rounded-xl flex items-center justify-center mb-0.5 transition-all ${isActive ? `${accent.bg} ${accent.shadow}` : ""
                      }`}
                  >
                    <Icon
                      className={`w-5 h-5 duo-nav-icon-loop ${isActive ? "text-white" : "text-[#AFAFAF]"}`}
                      strokeWidth={isActive ? 2.5 : 2}
                      style={{ animationDelay: `${idx * 0.12}s` }}
                    />
                  </span>
                  <span className={`text-[10px] font-black uppercase tracking-wide ${isActive ? accent.text : "text-[#AFAFAF]"}`}>
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