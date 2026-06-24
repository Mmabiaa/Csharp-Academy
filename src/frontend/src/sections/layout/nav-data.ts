export type NavItem = {
  path: string;
  label: string;
  icon: React.ComponentType<{ className?: string; strokeWidth?: number; style?: React.CSSProperties }>;
  roles?: Array<"student" | "teacher" | "admin">;
  authOnly?: boolean;
};

export type Accent = { bg: string; shadow: string; tint: string; text: string };

export const navAccent: Record<string, Accent> = {
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

export const fallbackAccent: Accent = {
  bg: "bg-[#1CB0F6]",
  shadow: "shadow-[0_3px_0_#1899D6]",
  tint: "bg-[#1CB0F6]/10",
  text: "text-[#1899D6]",
};
