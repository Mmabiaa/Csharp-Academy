import { useState } from "react";
import { useParams } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { verifyCertificate, getCertificatePdfUrl } from "../../lib/api";
import { Download } from "lucide-react";

export default function CertificateVerify() {
  const { code: routeCode } = useParams<{ code: string }>();
  const [lookupCode, setLookupCode] = useState(routeCode ?? "");

  const { data: cert, isLoading, error, refetch } = useQuery({
    queryKey: ["certificate", lookupCode],
    queryFn: () => verifyCertificate(lookupCode),
    enabled: false,
    retry: false,
  });

  const handleVerify = (e: React.FormEvent) => {
    e.preventDefault();
    if (lookupCode.trim()) refetch();
  };

  return (
    <div className="space-y-6 pb-24 md:pb-0 max-w-2xl mx-auto">
      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Verify Certificate
        </h1>
      </div>

      <form onSubmit={handleVerify} className="flex flex-col sm:flex-row gap-2">
        <input
          type="text"
          value={lookupCode}
          onChange={(e) => setLookupCode(e.target.value.toUpperCase())}
          placeholder="Enter certificate code"
          className="flex-1 px-4 py-3 border border-[#e5e5e5] rounded-xl focus:outline-none focus:border-[#58CC02] focus:ring-4 focus:ring-[#58CC02]/10 font-mono font-semibold"
        />
        <button
          type="submit"
          disabled={isLoading}
          className="duo-btn duo-btn-primary"
        >
          {isLoading ? "Verifying..." : "Verify"}
        </button>
      </form>

      {isLoading && (
        <div className="duo-card animate-pulse">
          <div className="h-64" />
        </div>
      )}

      {error && (
        <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
          <p className="text-[#FF4B4B] font-black">Certificate not found. Please check the code and try again.</p>
        </div>
      )}

      {cert && (
        <div className="duo-card bg-gradient-to-br from-[#58CC02]/10 to-[#1CB0F6]/10 border-2 border-[#58CC02]/30 p-8 text-center">
          <div className="text-5xl mb-4">🎓</div>
          <h2 className="text-2xl font-black text-neutral-900 mb-2">Certificate of Completion</h2>
          <p className="text-lg font-semibold text-neutral-700 mb-1">Awarded to</p>
          <p className="text-xl font-black text-[#58CC02] mb-4">{cert.studentName}</p>
          <p className="text-neutral-600 font-semibold mb-1">For completing</p>
          <p className="text-lg font-black text-neutral-900 mb-4">{cert.courseTitle}</p>
          <p className="text-sm font-bold text-neutral-500">
            Issued {new Date(cert.issuedAt).toLocaleDateString()} · Code: {cert.certificateCode}
          </p>
          <a
            href={getCertificatePdfUrl(cert.certificateCode)}
            target="_blank"
            rel="noopener noreferrer"
            className="inline-flex items-center gap-2 mt-4 duo-btn duo-btn-primary"
          >
            <Download className="w-4 h-4" />
            Download PDF
          </a>
        </div>
      )}
    </div>
  );
}
