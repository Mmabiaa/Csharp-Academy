import { useState } from "react";
import { useParams } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import { verifyCertificate, getCertificatePdfUrl } from "../lib/api";

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
    <div className="max-w-2xl mx-auto px-4 py-12">
      <h1 className="text-3xl font-bold text-gray-900 mb-6">Verify Certificate</h1>

      <form onSubmit={handleVerify} className="flex gap-2 mb-8">
        <input
          type="text"
          value={lookupCode}
          onChange={(e) => setLookupCode(e.target.value.toUpperCase())}
          placeholder="Enter certificate code"
          className="flex-1 px-4 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 font-mono"
        />
        <button
          type="submit"
          disabled={isLoading}
          className="bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 disabled:opacity-50 font-medium"
        >
          Verify
        </button>
      </form>

      {isLoading && <p>Verifying...</p>}

      {error && (
        <p className="text-red-600">Certificate not found. Please check the code and try again.</p>
      )}

      {cert && (
        <div className="bg-gradient-to-br from-blue-50 to-indigo-100 border-2 border-blue-200 rounded-xl p-8 text-center">
          <div className="text-5xl mb-4">🎓</div>
          <h2 className="text-2xl font-bold text-gray-900 mb-2">Certificate of Completion</h2>
          <p className="text-lg text-gray-700 mb-1">Awarded to</p>
          <p className="text-xl font-semibold text-blue-800 mb-4">{cert.studentName}</p>
          <p className="text-gray-600 mb-1">For completing</p>
          <p className="text-lg font-medium text-gray-900 mb-4">{cert.courseTitle}</p>
          <p className="text-sm text-gray-500">
            Issued {new Date(cert.issuedAt).toLocaleDateString()} · Code: {cert.certificateCode}
          </p>
          <a
            href={getCertificatePdfUrl(cert.certificateCode)}
            target="_blank"
            rel="noopener noreferrer"
            className="inline-block mt-4 bg-blue-600 text-white px-6 py-2 rounded-md hover:bg-blue-700 font-medium"
          >
            Download PDF
          </a>
        </div>
      )}
    </div>
  );
}
