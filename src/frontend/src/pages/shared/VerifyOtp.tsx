import { useState, useEffect, useRef } from "react";
import { Link, useNavigate, useLocation } from "react-router-dom";
import { verifyOtp } from "../../lib/api";
import { GraduationCap, XCircle, CheckCircle } from "lucide-react";

export default function VerifyOtp() {
  const location = useLocation();
  const navigate = useNavigate();
  const email = location.state?.email || "";

  const [otp, setOtp] = useState(["", "", "", "", "", ""]);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const inputRefs = useRef<(HTMLInputElement | null)[]>([]);

  useEffect(() => {
    if (!email) {
      navigate("/forgot-password");
    }
  }, [email, navigate]);

  useEffect(() => {
    // Focus first input on mount
    inputRefs.current[0]?.focus();
  }, []);

  const handleChange = (index: number, value: string) => {
    if (!/^\d*$/.test(value)) return; // Only allow digits

    const newOtp = [...otp];
    newOtp[index] = value.slice(-1); // Take only last digit
    setOtp(newOtp);

    // Auto-focus next input
    if (value && index < 5) {
      inputRefs.current[index + 1]?.focus();
    }
  };

  const handleKeyDown = (index: number, e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Backspace" && !otp[index] && index > 0) {
      inputRefs.current[index - 1]?.focus();
    }
  };

  const handlePaste = (e: React.ClipboardEvent) => {
    e.preventDefault();
    const pastedData = e.clipboardData.getData("text").replace(/\D/g, "").slice(0, 6);
    const newOtp = [...otp];
    
    for (let i = 0; i < pastedData.length; i++) {
      newOtp[i] = pastedData[i];
    }
    
    setOtp(newOtp);
    const nextIndex = Math.min(pastedData.length, 5);
    inputRefs.current[nextIndex]?.focus();
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const otpString = otp.join("");
    
    if (otpString.length !== 6) {
      setError("Please enter all 6 digits");
      return;
    }

    setError("");
    setLoading(true);
    try {
      const result = await verifyOtp(email, otpString);
      // Navigate to reset password page with data
      navigate("/reset-password", { 
        state: { email, otp: otpString, resetToken: result.resetToken } 
      });
    } catch (err) {
      setError(err instanceof Error ? err.message : "Invalid or expired OTP");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-md mx-auto py-12 px-4 animate-in fade-in slide-in-from-bottom-4 duration-300">
      <div className="text-center mb-8">
        <div className="w-20 h-20 rounded-full bg-[#EEEDFE] flex items-center justify-center mx-auto mb-4 shadow-[0_4px_0_#A568CC]">
          <GraduationCap className="w-10 h-10 text-[#CE82FF] duo-bounce" />
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Enter OTP
        </h1>
        <p className="text-neutral-500 font-bold">
          We sent a 6-digit code to
        </p>
        <p className="text-[#CE82FF] font-black mt-1">{email}</p>
      </div>

      <div className="duo-card">
        <form onSubmit={handleSubmit} className="space-y-5">
          {error && (
            <div className="flex items-start gap-2 bg-[#FFDFE0] border-2 border-[#FFB8B8] text-[#CC3A3A] font-bold px-4 py-3 rounded-2xl">
              <XCircle className="w-5 h-5 shrink-0 mt-0.5" />
              <span>{error}</span>
            </div>
          )}

          <div className="space-y-3">
            <label className="text-xs font-black text-neutral-500 uppercase tracking-wider text-center block">
              Enter 6-digit OTP
            </label>
            <div className="flex gap-2 justify-center">
              {otp.map((digit, index) => (
                <input
                  key={index}
                  ref={(el) => (inputRefs.current[index] = el)}
                  type="text"
                  inputMode="numeric"
                  maxLength={1}
                  value={digit}
                  onChange={(e) => handleChange(index, e.target.value)}
                  onKeyDown={(e) => handleKeyDown(index, e)}
                  onPaste={handlePaste}
                  className="w-12 h-14 text-center text-2xl font-black border-2 border-[#e5e5e5] rounded-xl focus:outline-none focus:border-[#CE82FF] focus:ring-4 focus:ring-[#CE82FF]/15 transition-all"
                />
              ))}
            </div>
          </div>

          <button
            type="submit"
            disabled={loading || otp.join("").length !== 6}
            className="w-full duo-btn3d duo-btn3d-blue disabled:opacity-50 flex items-center justify-center gap-2"
          >
            {loading ? "Verifying..." : (
              <>
                <CheckCircle className="w-4 h-4" />
                Verify OTP
              </>
            )}
          </button>
        </form>

        <div className="mt-6 text-center">
          <p className="text-neutral-500 font-bold text-sm">
            Didn't receive the code?{" "}
            <button
              onClick={() => navigate("/forgot-password")}
              className="text-[#CE82FF] font-black hover:underline"
            >
              Resend
            </button>
          </p>
        </div>
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
