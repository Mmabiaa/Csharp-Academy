import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { register as apiRegister } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";

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
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Join C# Academy
        </h1>
        <p className="text-neutral-600 font-semibold">
          Create an account to start learning
        </p>
      </div>

      <form onSubmit={handleSubmit} className="space-y-5">
        {error && (
          <div className="bg-[#FF4B4B]/10 border border-[#FF4B4B]/30 text-[#FF4B4B] font-semibold px-4 py-3 rounded-xl">
            {error}
          </div>
        )}

        <div className="grid grid-cols-2 gap-4">
          <div className="space-y-2">
            <label htmlFor="firstName" className="text-sm font-bold text-neutral-700">
              First Name
            </label>
            <input
              id="firstName"
              type="text"
              required
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
              placeholder="John"
              className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
            />
          </div>
          <div className="space-y-2">
            <label htmlFor="lastName" className="text-sm font-bold text-neutral-700">
              Last Name
            </label>
            <input
              id="lastName"
              type="text"
              required
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
              placeholder="Doe"
              className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
            />
          </div>
        </div>

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
            minLength={6}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="At least 6 characters"
            className="w-full px-4 py-3 border border-[#e5e5e5] rounded-xl font-semibold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 transition-all"
          />
        </div>

        <div className="space-y-3">
          <label className="text-sm font-bold text-neutral-700">
            Register as
          </label>
          <div className="flex gap-3">
            <button
              type="button"
              onClick={() => setRole("Student")}
              className={`flex-1 py-3 rounded-xl font-bold border-2 transition-all ${
                role === "Student"
                  ? "bg-[#58CC02] text-white border-[#58CC02]"
                  : "bg-neutral-100 text-neutral-700 border-[#e5e5e5] hover:border-[#58CC02]/30"
              }`}
            >
              Student
            </button>
            <button
              type="button"
              onClick={() => setRole("Teacher")}
              className={`flex-1 py-3 rounded-xl font-bold border-2 transition-all ${
                role === "Teacher"
                  ? "bg-[#1CB0F6] text-white border-[#1CB0F6]"
                  : "bg-neutral-100 text-neutral-700 border-[#e5e5e5] hover:border-[#1CB0F6]/30"
              }`}
            >
              Teacher
            </button>
          </div>
        </div>

        <button
          type="submit"
          disabled={loading}
          className="w-full duo-btn duo-btn-primary"
        >
          {loading ? "Creating your account..." : "Sign up"}
        </button>

        <div className="text-center pt-2">
          <p className="text-neutral-600 font-semibold">
            Already have an account?{" "}
            <Link
              to="/login"
              className="text-[#58CC02] font-black hover:underline"
            >
              Log in
            </Link>
          </p>
        </div>
      </form>
    </div>
  );
}
