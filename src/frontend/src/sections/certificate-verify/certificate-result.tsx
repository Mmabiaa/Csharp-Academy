import { Download } from "lucide-react";
import { getCertificatePdfUrl } from "../../lib/api";

interface CertificateResultProps {
  cert: {
    studentName: string;
    courseTitle: string;
    issuedAt: string;
    certificateCode: string;
  };
}

export default function CertificateResult({ cert }: CertificateResultProps) {
  return (
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
  );
}
