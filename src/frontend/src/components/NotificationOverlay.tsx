import { useEffect, useState } from "react";
import { useNotifications, GamificationNotification } from "../context/NotificationContext";
import { Gem, Flame, Trophy, CheckCircle2, Sparkles, X } from "lucide-react";

export default function NotificationOverlay() {
    const { notifications, hideNotification } = useNotifications();

    if (notifications.length === 0) return null;

    return (
        <div className="fixed inset-0 z-[9999] flex items-center justify-center p-4 bg-black/40 backdrop-blur-sm pointer-events-auto">
            <div className="flex flex-col gap-4 max-w-md w-full">
                {notifications.map((notif) => (
                    <NotificationCard key={notif.id} notification={notif} onDismiss={() => hideNotification(notif.id)} />
                ))}
            </div>
        </div>
    );
}

function NotificationCard({ notification, onDismiss }: { notification: GamificationNotification; onDismiss: () => void }) {
    const [show, setShow] = useState(false);

    useEffect(() => {
        setShow(true);
    }, []);

    const { type, title, message, xpEarned, streakDays, badges } = notification;

    return (
        <div
            className={`relative bg-white rounded-[2rem] border-4 border-[#e5e5e5] shadow-[0_8px_0_#e5e5e5] p-8 transition-all duration-500 transform ${show ? "scale-100 opacity-100 translate-y-0" : "scale-90 opacity-0 translate-y-10"
                }`}
        >
            <button
                onClick={onDismiss}
                className="absolute top-4 right-4 p-2 text-neutral-400 hover:text-neutral-600 hover:bg-neutral-100 rounded-full transition-colors"
            >
                <X className="w-5 h-5" />
            </button>

            <div className="flex flex-col items-center text-center">
                {/* Large Animated Icon */}
                <div className="mb-6 relative">
                    {type === "congrats" && (
                        <div className="w-24 h-24 bg-[#D7FFB8] rounded-[2rem] flex items-center justify-center animate-bounce shadow-[0_6px_0_#58CC02]">
                            <CheckCircle2 className="w-12 h-12 text-[#58CC02]" strokeWidth={3} />
                        </div>
                    )}
                    {type === "streak" && (
                        <div className="w-24 h-24 bg-[#FFE0BD] rounded-[2rem] flex items-center justify-center animate-pulse shadow-[0_6px_0_#FF9600]">
                            <Flame className="w-12 h-12 text-[#FF9600]" fill="#FF9600" />
                        </div>
                    )}
                    {type === "achievement" && (
                        <div className="w-24 h-24 bg-[#FFF1C2] rounded-[2rem] flex items-center justify-center animate-spin-slow shadow-[0_6px_0_#FFC800]">
                            <Trophy className="w-12 h-12 text-[#FFC800]" fill="#FFC800" />
                        </div>
                    )}
                </div>

                <h2 className="text-3xl font-black text-neutral-900 mb-2 uppercase tracking-tight">{title}</h2>
                <p className="text-neutral-600 font-bold text-lg mb-6">{message}</p>

                {/* Stats Grid */}
                <div className="grid grid-cols-2 gap-4 w-full mb-8">
                    {xpEarned !== undefined && (
                        <div className="bg-[#FFF8E1] border-2 border-[#FFC800]/40 rounded-2xl p-4 flex flex-col items-center shadow-[0_4px_0_#FFC80020]">
                            <Gem className="w-6 h-6 text-[#FFC800] mb-1" fill="#FFC800" />
                            <span className="text-2xl font-black text-[#946800]">+{xpEarned}</span>
                            <span className="text-[10px] font-black text-[#946800]/60 uppercase tracking-widest">XP gained</span>
                        </div>
                    )}
                    {streakDays !== undefined && (
                        <div className="bg-[#FFF1E0] border-2 border-[#FF9600]/40 rounded-2xl p-4 flex flex-col items-center shadow-[0_4px_0_#FF960020]">
                            <Flame className="w-6 h-6 text-[#FF9600] mb-1" fill="#FF9600" />
                            <span className="text-2xl font-black text-[#CC6E00]">{streakDays}</span>
                            <span className="text-[10px] font-black text-[#CC6E00]/60 uppercase tracking-widest">Day streak</span>
                        </div>
                    )}
                </div>

                {/* Badges Section */}
                {badges && badges.length > 0 && (
                    <div className="w-full mb-8">
                        <p className="text-xs font-black text-neutral-400 uppercase tracking-widest mb-3">Badges Earned</p>
                        <div className="flex flex-wrap justify-center gap-2">
                            {badges.map((badge, i) => (
                                <div key={i} className="bg-[#CE82FF]/10 text-[#CE82FF] px-4 py-2 rounded-xl border-2 border-[#CE82FF]/40 font-black text-sm animate-bounce" style={{ animationDelay: `${i * 0.1}s` }}>
                                    {badge}
                                </div>
                            ))}
                        </div>
                    </div>
                )}

                <button
                    onClick={onDismiss}
                    className="duo-btn3d duo-btn3d-green w-full !py-4 !text-lg !rounded-2xl"
                >
                    Continue
                </button>
            </div>
        </div>
    );
}
