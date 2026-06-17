import { createContext, useContext, useState, type ReactNode } from "react";
import type { AuthResponse } from "../lib/api";

interface User {
  userId: number;
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
}

interface AuthContextType {
  user: User | null;
  token: string | null;
  login: (auth: AuthResponse) => void;
  logout: () => void;
  isAuthenticated: boolean;
  isTeacher: boolean;
  isAdmin: boolean;
}

const AuthContext = createContext<AuthContextType | null>(null);

const TOKEN_KEY = "csharp_academy_token";
const USER_KEY = "csharp_academy_user";

function normalizeUser(raw: Partial<User> | null): User | null {
  if (!raw?.userId) return null;
  return {
    userId: raw.userId,
    email: raw.email ?? "",
    firstName: raw.firstName ?? "",
    lastName: raw.lastName ?? "",
    roles: Array.isArray(raw.roles) ? raw.roles : [],
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
    const userData = normalizeUser({
      userId: auth.userId,
      email: auth.email,
      firstName: auth.firstName,
      lastName: auth.lastName,
      roles: auth.roles ?? [],
    })!;
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

  const isTeacher = (user?.roles ?? []).some((r) => r === "Teacher" || r === "Admin");
  const isAdmin = (user?.roles ?? []).some((r) => r === "Admin");

  return (
    <AuthContext.Provider value={{ user, token, login, logout, isAuthenticated: !!token, isTeacher, isAdmin }}>
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
