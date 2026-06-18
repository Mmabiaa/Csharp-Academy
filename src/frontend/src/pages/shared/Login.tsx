import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { login as apiLogin } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Mail, Lock, GraduationCap, XCircle } from "lucide-react";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
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
      navigate("/");
    } catch (err) {
      setError(err instanceof Error ? err.message : "Login failed");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-md mx-auto py-12 px-4">
      <div className="text-center mb-8">
        <div className="w-20 h-20 rounded-full bg-[#D7FFB8] flex items-center justify-center mx-auto mb-4 shadow-[0_4px_0_#46A302]">
          <GraduationCap className="w-10 h-10 text-[#46A302]" />
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Welcome back
        </h1>
        <p className="text-neutral-500 font-bold">Log in to continue your streak</p>
      </div>

      <div className="duo-card">
        <form onSubmit={handleSubmit} className="space-y-5">
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
              className="text-xs font-black text-neutral-500 uppercase tracking-wider flex items-center gap-1.5"
            >
              <Lock className="w-3.5 h-3.5" />
              Password
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