import { User } from "lucide-react";

interface UserAvatarProps {
    profileImageUrl?: string | null;
    firstName?: string | null;
    lastName?: string | null;
    size?: "sm" | "md" | "lg" | "xl";
    className?: string;
}

const sizeMap = {
    sm: { container: "w-9 h-9", text: "text-sm", icon: "w-4 h-4" },
    md: { container: "w-11 h-11", text: "text-base", icon: "w-5 h-5" },
    lg: { container: "w-16 h-16", text: "text-2xl", icon: "w-7 h-7" },
    xl: { container: "w-24 h-24", text: "text-4xl", icon: "w-10 h-10" },
};

export default function UserAvatar({
    profileImageUrl,
    firstName,
    lastName,
    size = "md",
    className = "",
}: UserAvatarProps) {
    const { container, text, icon } = sizeMap[size];
    const initials =
        (firstName?.[0] ?? "") + (lastName?.[0] ?? "");

    if (profileImageUrl) {
        return (
            <img
                src={profileImageUrl}
                alt={initials || "User avatar"}
                className={`${container} rounded-xl object-cover bg-white ${className}`}
                onError={(e) => {
                    // If image fails to load, hide it so the parent can show fallback
                    (e.currentTarget as HTMLImageElement).style.display = "none";
                }}
            />
        );
    }

    if (initials) {
        return (
            <div
                className={`${container} rounded-xl bg-[#58CC02] flex items-center justify-center text-white font-black shadow-[0_2px_0_#46A302] ${className}`}
            >
                <span className={text}>{initials.toUpperCase()}</span>
            </div>
        );
    }

    return (
        <div
            className={`${container} rounded-xl bg-[#58CC02] flex items-center justify-center text-white shadow-[0_2px_0_#46A302] ${className}`}
        >
            <User className={icon} />
        </div>
    );
}
