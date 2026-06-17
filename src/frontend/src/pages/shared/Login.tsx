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
    <div className="max-w-md mx-auto">
      <div className="space-y-8">
        <div className="space-y-3">
          <div className="text-xs uppercase tracking-widest text-gray-500 font-semibold">
            Account
          </div>
          <h1 className="text-5xl font-serif font-bold tracking-tight text-black">
            Sign In
          </h1>
          <p className="text-gray-600">
            Access your dashboard and continue learning.
          </p>
        </div>
        <form onSubmit={handleSubmit} className="card space-y-6">
          {error && (
            <div className="p-4 border border-gray-300 bg-gray-50 text-sm text-gray-700">
              {error}
            </div>
          )}
          <div className="space-y-2">
            <label
              htmlFor="email"
              className="block text-sm font-medium text-gray-700"
            >
              Email Address
            </label>
            <input
              id="email"
              type="email"
              required
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="input"
              placeholder="you@example.com"
            />
          </div>
          <div className="space-y-2">
            <label
              htmlFor="password"
              className="block text-sm font-medium text-gray-700"
            >
              Password
            </label>
            <input
              id="password"
              type="password"
              required
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="input"
              placeholder="Enter your password"
            />
          </div>
          <button
            type="submit"
            disabled={loading}
            className="btn btn-primary w-full"
          >
            {loading ? "Signing in..." : "Sign In"}
          </button>
          <div className="pt-4 border-t border-gray-100">
            <p className="text-sm text-gray-600 text-center">
              Don't have an account?{" "}
              <Link
                to="/register"
                className="font-medium text-black hover:underline"
              >
                Create one for free
              </Link>
            </p>
          </div>
        </form>
      </div>
    </div>
  );
}
