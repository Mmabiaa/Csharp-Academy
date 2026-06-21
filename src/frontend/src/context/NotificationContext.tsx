import React, { createContext, useContext, useState, useCallback } from "react";

export type NotificationType = "congrats" | "achievement" | "streak" | "info";

export interface GamificationNotification {
    id: string;
    type: NotificationType;
    title: string;
    message: string;
    xpEarned?: number;
    streakDays?: number;
    badges?: string[];
}

interface NotificationContextType {
    showNotification: (notification: Omit<GamificationNotification, "id">) => void;
    hideNotification: (id: string) => void;
    notifications: GamificationNotification[];
}

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

export function NotificationProvider({ children }: { children: React.ReactNode }) {
    const [notifications, setNotifications] = useState<GamificationNotification[]>([]);

    const showNotification = useCallback((notif: Omit<GamificationNotification, "id">) => {
        const id = Math.random().toString(36).substring(2, 9);
        setNotifications((prev) => [...prev, { ...notif, id }]);

        // Auto-hide after some time if it's just info, but for congrats we might want manual dismiss
        if (notif.type === "info") {
            setTimeout(() => hideNotification(id), 5000);
        }
    }, []);

    const hideNotification = useCallback((id: string) => {
        setNotifications((prev) => prev.filter((n) => n.id !== id));
    }, []);

    return (
        <NotificationContext.Provider value={{ showNotification, hideNotification, notifications }}>
            {children}
        </NotificationContext.Provider>
    );
}

export function useNotifications() {
    const context = useContext(NotificationContext);
    if (context === undefined) {
        throw new Error("useNotifications must be used within a NotificationProvider");
    }
    return context;
}
