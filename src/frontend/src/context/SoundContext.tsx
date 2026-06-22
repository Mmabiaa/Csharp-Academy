import React, { createContext, useContext, useState, useEffect } from "react";

interface SoundContextType {
    isSoundEnabled: boolean;
    setSoundEnabled: (enabled: boolean) => void;
    playSound: (type: "click" | "success" | "error" | "notification" | "complete" | "enrolled" | "quiz") => void;
}

const SoundContext = createContext<SoundContextType | undefined>(undefined);

const SOUNDS = {
    click: "/sounds/button.mp3",
    success: "/sounds/streak_and_success.mp3",
    error: "/sounds/failed.mp3",
    notification: "/sounds/notification.mp3",
    complete: "/sounds/course_completion.mp3",
    enrolled: "/sounds/enrolled.mp3",
    quiz: "/sounds/quiz.mp3",
};

export function SoundProvider({ children }: { children: React.ReactNode }) {
    const [isSoundEnabled, setSoundEnabled] = useState(() => {
        const saved = localStorage.getItem("soundEnabled");
        return saved !== null ? JSON.parse(saved) : true;
    });

    useEffect(() => {
        localStorage.setItem("soundEnabled", JSON.stringify(isSoundEnabled));
    }, [isSoundEnabled]);

    const playSound = (type: keyof typeof SOUNDS) => {
        if (!isSoundEnabled) return;

        const audio = new Audio(SOUNDS[type]);
        audio.play().catch(err => console.log("Sound play prevented:", err));
    };

    return (
        <SoundContext.Provider value={{ isSoundEnabled, setSoundEnabled, playSound }}>
            {children}
        </SoundContext.Provider>
    );
}

export function useSound() {
    const context = useContext(SoundContext);
    if (context === undefined) {
        throw new Error("useSound must be used within a SoundProvider");
    }
    return context;
}
