import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { forgotPassword } from "../../lib/api";
import { Mail, GraduationCap, XCircle, Send } from "lucide-react";

export default function ForgotPassword() {
  const [email, setEmail] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setLoading(true);
    try {
      await forgotPassword(email);
      // Navigate to OTP page with email
      navigate("/verify-otp", { state: { email } });
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to send OTP");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-md mx-auto py-12 px-4 animate-in fade-in slide-in-from-bottom-4 duration-300">
      <div className="text-center mb-8">
        <div className="w-20 h-20 rounded-full bg-[#DDF4FF] flex items-center justify-center mx-auto mb-4 shadow-[0_4px_0_#1899D6]">
          <GraduationCap className="w-10 h-10 text-[#1CB0F6] duo-bounce" />
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Forgot password?
        </h1>
        <p className="text-neutral-500 font-bold">
          No worries, we'll send you an OTP to reset it
        </p>
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

          <button
            type="submit"
            disabled={loading}
            className="w-full duo-btn3d duo-btn3d-blue disabled:opacity-50 flex items-center justify-center gap-2"
          >
            {loading ? "Sending..." : (
              <>
                <Send className="w-4 h-4" />
                Send OTP
              </>
            )}
          </button>
        </form>
      </div>

      <div className="text-center pt-6 space-y-2">
        <p className="text-neutral-500 font-bold">
          <Link to="/login" className="text-[#1CB0F6] font-black hover:underline">
            ← Back to login
          </Link>
        </p>
      </div>
    </div>
  );
}
