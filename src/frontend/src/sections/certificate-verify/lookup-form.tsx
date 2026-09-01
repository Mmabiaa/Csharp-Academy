interface LookupFormProps {
  lookupCode: string;
  isLoading: boolean;
  onCodeChange: (value: string) => void;
  onSubmit: (e: React.FormEvent) => void;
}

export default function LookupForm({ lookupCode, isLoading, onCodeChange, onSubmit }: LookupFormProps) {
  return (
    <form onSubmit={onSubmit} className="flex flex-col sm:flex-row gap-2">
      <input
        type="text"
        value={lookupCode}
        onChange={(e) => onCodeChange(e.target.value.toUpperCase())}
        placeholder="Enter certificate code"
        className="flex-1 px-4 py-3 border border-[#e5e5e5] rounded-xl focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 font-mono font-semibold"
      />
      <button type="submit" disabled={isLoading} className="duo-btn duo-btn-primary">
        {isLoading ? "Verifying..." : "Verify"}
      </button>
    </form>
  );
}
