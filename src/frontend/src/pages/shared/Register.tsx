import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { register as apiRegister } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Mail, Lock, User, GraduationCap, XCircle } from "lucide-react";

export default function Register() {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [role, setRole] = useState<"Student" | "Teacher">("Student");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setLoading(true);
    try {
      const auth = await apiRegister(email, password, firstName, lastName, role);
      login(auth);
      navigate("/");
    } catch (err) {
      setError(err instanceof Error ? err.message : "Registration failed");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-md mx-auto py-12 px-4">
      <div className="text-center mb-8">
        <div className="w-20 h-20 rounded-full bg-[#FFF1C2] flex items-center justify-center mx-auto mb-4 shadow-[0_4px_0_#E6B400]">
          <GraduationCap className="w-10 h-10 text-[#946800] duo-bounce" />
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Join C# Academy
        </h1>
        <p className="text-neutral-500 font-bold">Create an account to start learning</p>
      </div>

      <div className="duo-card">
        <form onSubmit={handleSubmit} className="space-y-5">
          {error && (
            <div className="flex items-start gap-2 bg-[#FFDFE0] border-2 border-[#FFB8B8] text-[#CC3A3A] font-bold px-4 py-3 rounded-2xl">
              <XCircle className="w-5 h-5 shrink-0 mt-0.5" />
              <span>{error}</span>
            </div>
          )}

          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-2">
              <label
                htmlFor="firstName"
                className="text-xs font-black text-neutral-500 uppercase tracking-wider"
              >
                First name
              </label>
              <input
                id="firstName"
                type="text"
                required
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                placeholder="John"
                className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
              />
            </div>
            <div className="space-y-2">
              <label
                htmlFor="lastName"
                className="text-xs font-black text-neutral-500 uppercase tracking-wider"
              >
                Last name
              </label>
              <input
                id="lastName"
                type="text"
                required
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
                placeholder="Doe"
                className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
              />
            </div>
          </div>

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
              className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
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
              minLength={6}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="At least 6 characters"
              className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
            />
          </div>

          <div className="space-y-3">
            <label className="text-xs font-black text-neutral-500 uppercase tracking-wider flex items-center gap-1.5">
              <User className="w-3.5 h-3.5" />
              Register as
            </label>
            <div className="flex gap-3">
              <button
                type="button"
                onClick={() => setRole("Student")}
                className={
                  role === "Student"
                    ? "flex-1 duo-btn3d duo-btn3d-green"
                    : "flex-1 duo-btn3d duo-btn3d-white"
                }
              >
                Student
              </button>
              <button
                type="button"
                onClick={() => setRole("Teacher")}
                className={
                  role === "Teacher"
                    ? "flex-1 duo-btn3d duo-btn3d-blue"
                    : "flex-1 duo-btn3d duo-btn3d-white"
                }
              >
                Teacher
              </button>
            </div>
          </div>

          <button
            type="submit"
            disabled={loading}
            className="w-full duo-btn3d duo-btn3d-green disabled:opacity-50"
          >
            {loading ? "Creating your account..." : "Sign up"}
          </button>
        </form>
      </div>

      <div className="text-center pt-6">
        <p className="text-neutral-500 font-bold">
          Already have an account?{" "}
          <Link to="/login" className="text-[#58CC02] font-black hover:underline">
            Log in
          </Link>
        </p>
      </div>
    </div>
  );
}