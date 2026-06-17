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
    <div className="max-w-md mx-auto">
      <div className="space-y-8">
        <div className="space-y-3">
          <div className="text-xs uppercase tracking-widest text-gray-500 font-semibold">
            Account
          </div>
          <h1 className="text-5xl font-serif font-bold tracking-tight text-black">
            Create Account
          </h1>
          <p className="text-gray-600">
            Join Csharp Academy and start learning C#.
          </p>
        </div>
        <form onSubmit={handleSubmit} className="card space-y-6">
          {error && (
            <div className="p-4 border border-gray-300 bg-gray-50 text-sm text-gray-700">
              {error}
            </div>
          )}
          <div className="grid grid-cols-2 gap-4">
            <div className="space-y-2">
              <label
                htmlFor="firstName"
                className="block text-sm font-medium text-gray-700"
              >
                First Name
              </label>
              <input
                id="firstName"
                type="text"
                required
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                className="input"
                placeholder="John"
              />
            </div>
            <div className="space-y-2">
              <label
                htmlFor="lastName"
                className="block text-sm font-medium text-gray-700"
              >
                Last Name
              </label>
              <input
                id="lastName"
                type="text"
                required
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
                className="input"
                placeholder="Doe"
              />
            </div>
          </div>
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
              minLength={6}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="input"
              placeholder="At least 6 characters"
            />
          </div>
          <div className="space-y-3">
            <label className="block text-sm font-medium text-gray-700">
              Register as
            </label>
            <div className="flex gap-6">
              <label className="flex items-center gap-3 cursor-pointer">
                <input
                  type="radio"
                  name="role"
                  checked={role === "Student"}
                  onChange={() => setRole("Student")}
                  className="w-4 h-4"
                />
                <span className="text-sm">Student</span>
              </label>
              <label className="flex items-center gap-3 cursor-pointer">
                <input
                  type="radio"
                  name="role"
                  checked={role === "Teacher"}
                  onChange={() => setRole("Teacher")}
                  className="w-4 h-4"
                />
                <span className="text-sm">Teacher</span>
              </label>
            </div>
          </div>
          <button
            type="submit"
            disabled={loading}
            className="btn btn-primary w-full"
          >
            {loading ? "Creating account..." : "Create Account"}
          </button>
          <div className="pt-4 border-t border-gray-100">
            <p className="text-sm text-gray-600 text-center">
              Already have an account?{" "}
              <Link
                to="/login"
                className="font-medium text-black hover:underline"
              >
                Sign in here
              </Link>
            </p>
          </div>
        </form>
      </div>
    </div>
  );
}
