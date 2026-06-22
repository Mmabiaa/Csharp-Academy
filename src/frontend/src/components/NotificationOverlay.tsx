import { useEffect, useState } from "react";
import { useNotifications, GamificationNotification } from "../context/NotificationContext";
import { Gem, Flame, X } from "lucide-react";

// ─── Lottie web-component type declaration ────────────────────────────────────
declare global {
    namespace JSX {
        interface IntrinsicElements {
            "lottie-player": React.DetailedHTMLProps<
                React.HTMLAttributes<HTMLElement> & {
                    src?: string;
                    background?: string;
                    speed?: string;
                    loop?: boolean;
                    autoplay?: boolean;
                    style?: React.CSSProperties;
                },
                HTMLElement
            >;
        }
    }
}

// ─── Reusable Lottie icon component ──────────────────────────────────────────
interface LottieIconProps {
    src: string;
    size?: number;
    speed?: number;
    loop?: boolean;
    className?: string;
}

function LottieIcon({ src, size = 80, speed = 1, loop = false, className = "" }: LottieIconProps) {
    return (
        <lottie-player
            src={src}
            background="transparent"
            speed={String(speed)}
            style={{ width: size, height: size }}
            loop={loop || undefined}
            autoplay
            class={className}
        />
    );
}

// ─── Animation paths ──────────────────────────────────────────────────────────
const ANIM = {
    congrats: "/animations/badge.json",
    streak: "/animations/streak.json",
    achievement: "/animations/trophy.json",
} as const;

const NOTIF_CONFIG = {
    congrats: {
        src: ANIM.congrats,
        loop: false,
        bg: "bg-[#D7FFB8]",
        shadow: "shadow-[0_6px_0_#58CC02]",
        stripe: "bg-[#58CC02]",
    },
    streak: {
        src: ANIM.streak,
        loop: true,
        bg: "bg-[#FFE0BD]",
        shadow: "shadow-[0_6px_0_#FF9600]",
        stripe: "bg-[#FF9600]",
    },
    achievement: {
        src: ANIM.achievement,
        loop: false,
        bg: "bg-[#FFF1C2]",
        shadow: "shadow-[0_6px_0_#FFC800]",
        stripe: "bg-[#FFC800]",
    },
} as const;

// ─── Overlay ──────────────────────────────────────────────────────────────────
export default function NotificationOverlay() {
    const { notifications, hideNotification } = useNotifications();

    useEffect(() => {
        import("@lottiefiles/lottie-player");
    }, []);

    if (notifications.length === 0) return null;

    return (
        <div className="fixed inset-0 z-[9999] flex items-end sm:items-center justify-center p-4 bg-black/50 backdrop-blur-sm pointer-events-auto">
            <div className="flex flex-col gap-4 max-w-sm w-full">
                {notifications.map((notif) => (
                    <NotificationCard
                        key={notif.id}
                        notification={notif}
                        onDismiss={() => hideNotification(notif.id)}
                    />
                ))}
            </div>
        </div>
    );
}

// ─── Card ─────────────────────────────────────────────────────────────────────
function NotificationCard({
    notification,
    onDismiss,
}: {
    notification: GamificationNotification;
    onDismiss: () => void;
}) {
    const [show, setShow] = useState(false);

    useEffect(() => {
        const t = setTimeout(() => setShow(true), 10);
        return () => clearTimeout(t);
    }, []);

    const { type, title, message, xpEarned, streakDays, badges } = notification;
    const config = NOTIF_CONFIG[type as keyof typeof NOTIF_CONFIG] ?? NOTIF_CONFIG.congrats;

    return (
        <div
            className={`relative bg-white rounded-[2rem] border-4 border-[#e5e5e5] shadow-[0_8px_0_#e5e5e5] overflow-hidden transition-all duration-500 transform ${show ? "scale-100 opacity-100 translate-y-0" : "scale-90 opacity-0 translate-y-10"
                }`}
        >
            {/* Top color stripe */}
            <div className={`h-2 w-full ${config.stripe}`} />

            <div className="p-6 flex flex-col items-center text-center">
                {/* Dismiss */}
                <button
                    onClick={onDismiss}
                    className="absolute top-4 right-4 p-1.5 text-neutral-400 hover:text-neutral-600 hover:bg-neutral-100 rounded-full transition-colors"
                >
                    <X className="w-4 h-4" />
                </button>

                {/* Lottie animation */}
                <div className={`w-28 h-28 rounded-[1.5rem] flex items-center justify-center mb-5 ${config.bg} ${config.shadow}`}>
                    <LottieIcon src={config.src} size={88} speed={1} loop={config.loop} />
                </div>

                {/* Title */}
                <h2 className="text-2xl font-black text-neutral-900 uppercase tracking-tight leading-tight mb-1">
                    {title}
                </h2>

                {/* Message */}
                <p className="text-neutral-500 font-semibold text-sm mb-5 max-w-[220px]">
                    {message}
                </p>

                {/* Stats */}
                {(xpEarned !== undefined || streakDays !== undefined) && (
                    <div className="flex gap-3 w-full mb-5">
                        {xpEarned !== undefined && (
                            <div className="flex-1 bg-[#FFF8E1] border-2 border-[#FFC800]/40 rounded-2xl py-3 flex flex-col items-center gap-0.5">
                                <Gem className="w-5 h-5 text-[#FFC800]" fill="#FFC800" />
                                <span className="text-xl font-black text-[#946800] leading-none">+{xpEarned}</span>
                                <span className="text-[9px] font-black text-[#946800]/60 uppercase tracking-widest">XP</span>
                            </div>
                        )}
                        {streakDays !== undefined && (
                            <div className="flex-1 bg-[#FFF1E0] border-2 border-[#FF9600]/40 rounded-2xl py-3 flex flex-col items-center gap-0.5">
                                <Flame className="w-5 h-5 text-[#FF9600]" fill="#FF9600" />
                                <span className="text-xl font-black text-[#CC6E00] leading-none">{streakDays}</span>
                                <span className="text-[9px] font-black text-[#CC6E00]/60 uppercase tracking-widest">Day Streak</span>
                            </div>
                        )}
                    </div>
                )}

                {/* Badges */}
                {badges && badges.length > 0 && (
                    <div className="w-full mb-5">
                        <p className="text-[9px] font-black text-neutral-400 uppercase tracking-widest mb-2">
                            Badges Earned
                        </p>
                        <div className="flex flex-wrap justify-center gap-2">
                            {badges.map((badge, i) => (
                                <span
                                    key={i}
                                    className="bg-[#CE82FF]/10 text-[#9333ea] border-2 border-[#CE82FF]/40 px-3 py-1.5 rounded-xl font-black text-xs"
                                    style={{ animationDelay: `${i * 0.1}s` }}
                                >
                                    {badge}
                                </span>
                            ))}
                        </div>
                    </div>
                )}

                {/* CTA */}
                <button
                    onClick={onDismiss}
                    className="duo-btn3d duo-btn3d-green w-full !py-3.5 !text-base !rounded-2xl"
                >
                    Continue
                </button>
            </div>
        </div>
    );
}