import { createContext, useContext, useState, useEffect, useRef, type ReactNode } from "react";
import { AuthResponse, fetchUserProfile } from "../lib/api";

interface User {
  userId: number;
  email: string;
  firstName: string;
  lastName: string;
  profileImageUrl?: string;
  voiceRecognitionEnabled: boolean;
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

function normalizeUser(raw: any, existingRoles?: string[]): User | null {
  // Support both 'userId' (auth response) and 'id' (profile response)
  const id = raw?.userId ?? raw?.UserId;
  if (!id) return null;

  // Preserve existing roles if the new data has none (prevents role-stripping on profile refresh)
  const rawRoles = Array.isArray(raw.roles)
    ? raw.roles
    : Array.isArray(raw.Roles)
      ? raw.Roles
      : [];
  const roles = rawRoles.length > 0 ? rawRoles : (existingRoles ?? []);

  return {
    userId: id,
    email: raw.email ?? raw.Email ?? "",
    firstName: raw.firstName ?? raw.FirstName ?? "",
    lastName: raw.lastName ?? raw.LastName ?? "",
    profileImageUrl: raw.profileImageUrl ?? raw.ProfileImageUrl,
    voiceRecognitionEnabled: raw.voiceRecognitionEnabled ?? raw.VoiceRecognitionEnabled ?? false,
    roles,
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

  // Track whether we just logged in so we skip the redundant profile refresh
  const justLoggedIn = useRef(false);

  const login = (auth: AuthResponse) => {
    justLoggedIn.current = true;
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

  const refreshUser = async (currentToken?: string) => {
    const tok = currentToken ?? token;
    if (!tok) return;
    try {
      const profile = await fetchUserProfile(tok);
      setUser((prev) => {
        // Preserve roles from existing state if profile returns none
        const userData = normalizeUser(profile, prev?.roles)!;
        localStorage.setItem(USER_KEY, JSON.stringify(userData));
        return userData;
      });
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

  // Only run the profile refresh on initial mount (page reload with existing token),
  // NOT after a fresh login (roles are already correct from the auth response).
  useEffect(() => {
    if (!token) return;
    if (justLoggedIn.current) {
      // Skip refresh right after login — user data is already fresh from the auth response
      justLoggedIn.current = false;
      return;
    }
    refreshUser(token);
  }, [token]);

  const isTeacher = (user?.roles ?? []).some((r) => r.toLowerCase() === "teacher" || r.toLowerCase() === "admin");
  const isAdmin = (user?.roles ?? []).some((r) => r.toLowerCase() === "admin");

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
