import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchUserProfile, fetchCertificates, getCertificatePdfUrl } from "../lib/api";
import { useAuth } from "../context/AuthContext";

export default function Profile() {
  const { token, isAuthenticated } = useAuth();

  const { data: profile, isLoading, error } = useQuery({
    queryKey: ["profile"],
    queryFn: () => fetchUserProfile(token!),
    enabled: isAuthenticated && !!token,
  });

  const { data: certificates } = useQuery({
    queryKey: ["certificates"],
    queryFn: () => fetchCertificates(token!),
    enabled: isAuthenticated && !!token,
  });

  if (!isAuthenticated) {
    return (
      <div className="max-w-3xl mx-auto px-4 py-12">
        <p className="text-gray-600">
          <Link to="/login" className="text-blue-600 hover:underline">Sign in</Link> to view your profile.
        </p>
      </div>
    );
  }

  if (isLoading) {
    return <div className="max-w-3xl mx-auto px-4 py-12"><p>Loading profile...</p></div>;
  }

  if (error || !profile) {
    return <div className="max-w-3xl mx-auto px-4 py-12"><p className="text-red-600">Failed to load profile.</p></div>;
  }

  return (
    <div className="max-w-3xl mx-auto px-4 py-12">
      <h1 className="text-3xl font-bold text-gray-900 mb-2">
        {profile.firstName} {profile.lastName}
      </h1>
      <p className="text-gray-600 mb-8">{profile.email}</p>

      <div className="grid grid-cols-3 gap-4 mb-8">
        <div className="bg-white p-4 rounded-lg shadow-md text-center">
          <p className="text-2xl font-bold text-blue-600">{profile.xp}</p>
          <p className="text-sm text-gray-600">Total XP</p>
        </div>
        <div className="bg-white p-4 rounded-lg shadow-md text-center">
          <p className="text-2xl font-bold text-orange-500">{profile.currentStreak}</p>
          <p className="text-sm text-gray-600">Day Streak</p>
        </div>
        <div className="bg-white p-4 rounded-lg shadow-md text-center">
          <p className="text-2xl font-bold text-purple-600">{profile.maxStreak}</p>
          <p className="text-sm text-gray-600">Best Streak</p>
        </div>
      </div>

      <section className="mb-8">
        <h2 className="text-xl font-semibold text-gray-900 mb-4">Badges</h2>
        {profile.badges.length === 0 ? (
          <p className="text-gray-500">No badges yet. Complete lessons and quizzes to earn them!</p>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
            {profile.badges.map((badge) => (
              <div key={badge.id} className="bg-white p-4 rounded-lg shadow-md border-l-4 border-yellow-400">
                <p className="font-semibold text-gray-900">{badge.name}</p>
                <p className="text-sm text-gray-600">{badge.description}</p>
              </div>
            ))}
          </div>
        )}
      </section>

      <section className="mb-8">
        <h2 className="text-xl font-semibold text-gray-900 mb-4">Certificates</h2>
        {!certificates || certificates.length === 0 ? (
          <p className="text-gray-500">Complete a course to earn a certificate.</p>
        ) : (
          <div className="space-y-3">
            {certificates.map((cert) => (
              <div
                key={cert.id}
                className="bg-gradient-to-r from-blue-50 to-indigo-50 p-4 rounded-lg border border-blue-200"
              >
                <Link
                  to={`/certificates/${cert.certificateCode}`}
                  className="block hover:opacity-90"
                >
                  <p className="font-medium text-gray-900">🎓 {cert.courseTitle}</p>
                  <p className="text-sm text-gray-600">
                    Issued {new Date(cert.issuedAt).toLocaleDateString()} · Code: {cert.certificateCode}
                  </p>
                </Link>
                <a
                  href={getCertificatePdfUrl(cert.certificateCode)}
                  target="_blank"
                  rel="noopener noreferrer"
                  className="inline-block mt-2 text-sm text-blue-600 hover:underline"
                >
                  Download PDF
                </a>
              </div>
            ))}
          </div>
        )}
      </section>

      <section>
        <h2 className="text-xl font-semibold text-gray-900 mb-4">My Courses</h2>
        {profile.enrollments.length === 0 ? (
          <p className="text-gray-500">
            Not enrolled in any courses yet.{" "}
            <Link to="/courses" className="text-blue-600 hover:underline">Browse courses</Link>
          </p>
        ) : (
          <div className="space-y-3">
            {profile.enrollments.map((e) => (
              <Link
                key={e.courseId}
                to={`/courses/${e.courseId}`}
                className="block bg-white p-4 rounded-lg shadow-md hover:shadow-lg transition-shadow"
              >
                <div className="flex justify-between items-center mb-2">
                  <p className="font-medium text-gray-900">{e.courseTitle}</p>
                  <span className="text-sm text-gray-600">{e.completionPercentage}%</span>
                </div>
                <div className="w-full bg-gray-200 rounded-full h-2">
                  <div
                    className="bg-blue-600 h-2 rounded-full transition-all"
                    style={{ width: `${e.completionPercentage}%` }}
                  />
                </div>
              </Link>
            ))}
          </div>
        )}
      </section>
    </div>
  );
}
