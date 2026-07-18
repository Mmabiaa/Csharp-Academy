import { useState } from "react";
import { Link, useNavigate, useLocation } from "react-router-dom";
import { GoogleLogin } from "@react-oauth/google";
import { login as apiLogin, googleLogin as apiGoogleLogin } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Mail, Lock, GraduationCap, XCircle, CheckCircle } from "lucide-react";

export default function Login() {
  const location = useLocation();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [successMessage, setSuccessMessage] = useState(location.state?.message || "");
  const [loading, setLoading] = useState(false);
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setLoading(true);
    try {
      const auth = await apiLogin(email, password);
      login(auth);

      // Role-based redirection - handle both roles and Roles from API
      const rawRoles = auth.roles || (auth as any).Roles || [];
      const userRoles = rawRoles.map((r: string) => r.toLowerCase());

      if (userRoles.includes("admin")) {
        navigate("/admin");
      } else if (userRoles.includes("teacher")) {
        navigate("/teacher");
      } else {
        navigate("/");
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : "Login failed");
    } finally {
      setLoading(false);
    }
  };

  const handleGoogleSuccess = async (credentialResponse: any) => {
    setError("");
    setLoading(true);
    try {
      const auth = await apiGoogleLogin(credentialResponse.credential);
      login(auth);

      // Role-based redirection - handle both roles and Roles from API
      const rawRoles = auth.roles || (auth as any).Roles || [];
      const userRoles = rawRoles.map((r: string) => r.toLowerCase());

      if (userRoles.includes("admin")) {
        navigate("/admin");
      } else if (userRoles.includes("teacher")) {
        navigate("/teacher");
      } else {
        navigate("/");
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : "Google login failed");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-md mx-auto py-12 px-4 animate-in fade-in slide-in-from-bottom-4 duration-300">
      <div className="text-center mb-8">
        <div className="w-20 h-20 rounded-full bg-[#D7FFB8] flex items-center justify-center mx-auto mb-4 shadow-[0_4px_0_#46A302]">
          <GraduationCap className="w-10 h-10 text-[#46A302] duo-bounce" />
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Welcome back
        </h1>
        <p className="text-neutral-500 font-bold">Log in to continue your streak</p>
      </div>

      <div className="duo-card">
        <form onSubmit={handleSubmit} className="space-y-5">
          {successMessage && (
            <div className="flex items-start gap-2 bg-[#D7FFB8] border-2 border-[#46A302] text-[#46A302] font-bold px-4 py-3 rounded-2xl">
              <CheckCircle className="w-5 h-5 shrink-0 mt-0.5" />
              <span>{successMessage}</span>
            </div>
          )}
          
          {error && (
            <div className="flex items-start gap-2 bg-[#FFDFE0] border-2 border-[#FFB8B8] text-[#CC3A3A] font-bold px-4 py-3 rounded-2xl">
              <XCircle className="w-5 h-5 shrink-0 mt-0.5" />
              <span>{error}</span>
            </div>
          )}

          <div className="space-y-2">
            <label
              htmlFor="email"
              className="text-xs font-black text-neutral-500 uppercase tracking-wider flex items-center gap-1.5"
            >
              <Mail className="w-3.5 h-3.5" />
              Email
            </label>
            <input
              id="email"
              type="email"
              required
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="you@example.com"
              className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#1CB0F6] focus:ring-4 focus:ring-[#1CB0F6]/15 transition-all"
            />
          </div>

          <div className="space-y-2">
            <label
              htmlFor="password"
              className="text-xs font-black text-neutral-500 uppercase tracking-wider flex items-center justify-between"
            >
              <span className="flex items-center gap-1.5">
                <Lock className="w-3.5 h-3.5" />
                Password
              </span>
              <Link 
                to="/forgot-password" 
                className="text-[#1CB0F6] font-bold normal-case tracking-normal text-xs hover:underline"
              >
                Forgot?
              </Link>
            </label>
            <input
              id="password"
              type="password"
              required
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="Enter your password"
              className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#1CB0F6] focus:ring-4 focus:ring-[#1CB0F6]/15 transition-all"
            />
          </div>

          <button
            type="submit"
            disabled={loading}
            className="w-full duo-btn3d duo-btn3d-green disabled:opacity-50"
          >
            {loading ? "Logging in..." : "Log in"}
          </button>
        </form>

        <div className="relative my-8">
          <div className="absolute inset-0 flex items-center">
            <div className="w-full border-t-2 border-[#e5e5e5]"></div>
          </div>
          <div className="relative flex justify-center text-xs uppercase font-black">
            <span className="bg-white px-4 text-neutral-400">Or continue with</span>
          </div>
        </div>

        <div className="flex justify-center">
          <GoogleLogin
            onSuccess={handleGoogleSuccess}
            onError={() => setError("Google Login Failed")}
            width="100%"
            theme="outline"
            shape="pill"
          />
        </div>
      </div>

      <div className="text-center pt-6">
        <p className="text-neutral-500 font-bold">
          Don't have an account?{" "}
          <Link to="/register" className="text-[#58CC02] font-black hover:underline">
            Sign up
          </Link>
        </p>
      </div>
    </div>
  );
}