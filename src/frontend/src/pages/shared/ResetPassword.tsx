import { useState, useEffect } from "react";
import { Link, useNavigate, useLocation } from "react-router-dom";
import { resetPassword } from "../../lib/api";
import { Lock, GraduationCap, XCircle, CheckCircle } from "lucide-react";

export default function ResetPassword() {
  const location = useLocation();
  const navigate = useNavigate();
  const email = location.state?.email || "";
  const otp = location.state?.otp || "";

  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (!email || !otp) {
      navigate("/forgot-password");
    }
  }, [email, otp, navigate]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");

    if (newPassword.length < 6) {
      setError("Password must be at least 6 characters");
      return;
    }

    if (newPassword !== confirmPassword) {
      setError("Passwords do not match");
      return;
    }

    setLoading(true);
    try {
      await resetPassword(email, otp, newPassword);
      // Show success and redirect to login
      navigate("/login", { 
        state: { message: "Password reset successfully! Please login with your new password." }
      });
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to reset password");
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
          Reset password
        </h1>
        <p className="text-neutral-500 font-bold">
          Choose a new secure password for your account
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
              htmlFor="newPassword"
              className="text-xs font-black text-neutral-500 uppercase tracking-wider flex items-center gap-1.5"
            >
              <Lock className="w-3.5 h-3.5" />
              New Password
            </label>
            <input
              id="newPassword"
              type="password"
              required
              minLength={6}
              value={newPassword}
              onChange={(e) => setNewPassword(e.target.value)}
              placeholder="At least 6 characters"
              className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
            />
          </div>

          <div className="space-y-2">
            <label
              htmlFor="confirmPassword"
              className="text-xs font-black text-neutral-500 uppercase tracking-wider flex items-center gap-1.5"
            >
              <Lock className="w-3.5 h-3.5" />
              Confirm Password
            </label>
            <input
              id="confirmPassword"
              type="password"
              required
              minLength={6}
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              placeholder="Re-enter your password"
              className="w-full px-4 py-3 border-2 border-[#e5e5e5] rounded-2xl font-bold text-neutral-800 focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/15 transition-all"
            />
          </div>

          <button
            type="submit"
            disabled={loading}
            className="w-full duo-btn3d duo-btn3d-green disabled:opacity-50 flex items-center justify-center gap-2"
          >
            {loading ? "Resetting..." : (
              <>
                <CheckCircle className="w-4 h-4" />
                Reset Password
              </>
            )}
          </button>
        </form>
      </div>

      <div className="text-center pt-6">
        <p className="text-neutral-500 font-bold">
          <Link to="/login" className="text-[#1CB0F6] font-black hover:underline">
            ← Back to login
          </Link>
        </p>
      </div>
    </div>
  );
}
