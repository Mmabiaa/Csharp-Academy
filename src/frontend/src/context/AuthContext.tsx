import { createContext, useContext, useState, useEffect, type ReactNode } from "react";
import { AuthResponse, fetchUserProfile } from "../lib/api";

interface User {
  userId: number;
  email: string;
  firstName: string;
  lastName: string;
  profileImageUrl?: string;
  roles: string[];
  xp: number;
  currentStreak: number;
}

interface AuthContextType {
  user: User | null;
  token: string | null;
  login: (auth: AuthResponse) => void;
  logout: () => void;
  refreshUser: () => Promise<void>;
  updateUserSettings: (data: Partial<User>) => void;
  isAuthenticated: boolean;
  isTeacher: boolean;
  isAdmin: boolean;
}

const AuthContext = createContext<AuthContextType | null>(null);

const TOKEN_KEY = "csharp_academy_token";
const USER_KEY = "csharp_academy_user";

function normalizeUser(raw: any): User | null {
  if (!raw?.userId) return null;
  return {
    userId: raw.userId,
    email: raw.email ?? "",
    firstName: raw.firstName ?? "",
    lastName: raw.lastName ?? "",
    profileImageUrl: raw.profileImageUrl ?? raw.ProfileImageUrl,
    roles: Array.isArray(raw.roles) ? raw.roles : (raw.Roles ?? []),
    xp: raw.xp ?? raw.Xp ?? 0,
    currentStreak: raw.currentStreak ?? raw.CurrentStreak ?? 0,
  };
}

export function AuthProvider({ children }: { children: ReactNode }) {
  const [token, setToken] = useState<string | null>(() => localStorage.getItem(TOKEN_KEY));
  const [user, setUser] = useState<User | null>(() => {
    const stored = localStorage.getItem(USER_KEY);
    if (!stored) return null;
    try {
      return normalizeUser(JSON.parse(stored));
    } catch {
      return null;
    }
  });

  const login = (auth: AuthResponse) => {
    setToken(auth.token);
    const userData = normalizeUser(auth)!;
    setUser(userData);
    localStorage.setItem(TOKEN_KEY, auth.token);
    localStorage.setItem(USER_KEY, JSON.stringify(userData));
  };

  const logout = () => {
    setToken(null);
    setUser(null);
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);
  };

  const refreshUser = async () => {
    if (!token) return;
    try {
      const profile = await fetchUserProfile(token);
      const userData = normalizeUser(profile)!;
      setUser(userData);
      localStorage.setItem(USER_KEY, JSON.stringify(userData));
    } catch (error) {
      console.error("Failed to refresh user profile", error);
    }
  };

  const updateUserSettings = (data: Partial<User>) => {
    if (!user) return;
    const newUser = { ...user, ...data };
    setUser(newUser);
    localStorage.setItem(USER_KEY, JSON.stringify(newUser));
  };

  // Initial refresh on mount if authenticated
  useEffect(() => {
    if (token) {
      refreshUser();
    }
  }, [token]);

  const isTeacher = (user?.roles ?? []).some((r) => r === "Teacher" || r === "Admin");
  const isAdmin = (user?.roles ?? []).some((r) => r === "Admin");

  return (
    <AuthContext.Provider value={{
      user,
      token,
      login,
      logout,
      refreshUser,
      updateUserSettings,
      isAuthenticated: !!token,
      isTeacher,
      isAdmin
    }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider");
  }
  return context;
}
