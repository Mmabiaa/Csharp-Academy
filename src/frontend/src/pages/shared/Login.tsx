import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { login as apiLogin } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";

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
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Welcome back
        </h1>
        <p className="text-neutral-600 font-semibold">
          Log in to continue your journey
        </p>
      </div>

      <form onSubmit={handleSubmit} className="space-y-5">
        {error && (
          <div className="bg-[#FF4B4B]/10 border border-[#FF4B4B]/30 text-[#FF4B4B] font-semibold px-4 py-3 rounded-xl">
            {error}
          </div>
        )}

        <div className="space-y-2">
          <label htmlFor="email" className="text-sm font-bold text-neutral-700">
            Email
          </label>
          <input
            id="email"
            type="email"
            required
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="you@example.com"
            className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
          />
        </div>

        <div className="space-y-2">
          <label htmlFor="password" className="text-sm font-bold text-neutral-700">
            Password
          </label>
          <input
            id="password"
            type="password"
            required
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Enter your password"
            className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
          />
        </div>

        <button
          type="submit"
          disabled={loading}
          className="w-full duo-btn duo-btn-primary"
        >
          {loading ? "Logging in..." : "Log in"}
        </button>

        <div className="text-center pt-2">
          <p className="text-neutral-600 font-semibold">
            Don't have an account?{" "}
            <Link
              to="/register"
              className="text-[#58CC02] font-black hover:underline"
            >
              Sign up
            </Link>
          </p>
        </div>
      </form>
    </div>
  );
}
