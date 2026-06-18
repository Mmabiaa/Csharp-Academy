import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchUserProfile, fetchCertificates, getCertificatePdfUrl } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Star, Flame, Trophy, Award, BookOpen, ArrowRight } from "lucide-react";

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
      <div className="max-w-lg mx-auto py-12 text-center">
        <p className="text-neutral-600 text-lg font-semibold mb-6">
          <Link to="/login" className="text-[#58CC02] font-black hover:underline">
            Log in
          </Link>{" "}
          to view your profile
        </p>
      </div>
    );
  }

  if (isLoading) {
    return (
      <div className="max-w-3xl mx-auto py-12">
        <div className="duo-card animate-pulse">
          <div className="h-16 w-16 bg-neutral-200 rounded-full mx-auto mb-4" />
          <div className="h-8 w-1/2 bg-neutral-200 rounded-xl mx-auto mb-2" />
          <div className="h-5 w-1/3 bg-neutral-200 rounded-lg mx-auto" />
        </div>
      </div>
    );
  }

  if (error || !profile) {
    return (
      <div className="max-w-lg mx-auto py-12">
        <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
          <p className="text-[#FF4B4B] font-black text-lg">
            Failed to load profile
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      {/* Profile Header */}
      <div className="text-center mb-8">
        <div className="w-24 h-24 bg-[#58CC02] rounded-2xl flex items-center justify-center mx-auto mb-4 shadow-[0_4px_0_#46A301]">
          <span className="text-4xl font-black text-white">
            {profile.firstName[0]}
            {profile.lastName[0]}
          </span>
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1">
          {profile.firstName} {profile.lastName}
        </h1>
        <p className="text-neutral-600 font-semibold">{profile.email}</p>
      </div>

      {/* Stats Grid */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
        <div className="duo-card p-5 text-center">
          <div className="w-10 h-10 bg-[#FFC800]/10 rounded-lg flex items-center justify-center mx-auto mb-3">
            <Star className="w-6 h-6 text-[#FFC800]" fill="#FFC800" />
          </div>
          <p className="text-3xl font-black text-neutral-900 mb-1">
            {profile.xp}
          </p>
          <p className="text-xs font-bold text-neutral-500 uppercase tracking-wider">
            XP
          </p>
        </div>
        <div className="duo-card p-5 text-center">
          <div className="w-10 h-10 bg-[#FF9600]/10 rounded-lg flex items-center justify-center mx-auto mb-3">
            <Flame className="w-6 h-6 text-[#FF9600]" fill="#FF9600" />
          </div>
          <p className="text-3xl font-black text-neutral-900 mb-1">
            {profile.currentStreak}
          </p>
          <p className="text-xs font-bold text-neutral-500 uppercase tracking-wider">
            Current Streak
          </p>
        </div>
        <div className="duo-card p-5 text-center">
          <div className="w-10 h-10 bg-[#1CB0F6]/10 rounded-lg flex items-center justify-center mx-auto mb-3">
            <Trophy className="w-6 h-6 text-[#1CB0F6]" fill="#1CB0F6" />
          </div>
          <p className="text-3xl font-black text-neutral-900 mb-1">
            {profile.maxStreak}
          </p>
          <p className="text-xs font-bold text-neutral-500 uppercase tracking-wider">
            Best Streak
          </p>
        </div>
      </div>

      {/* Badges */}
      <div className="duo-card mb-6">
        <h2 className="font-black text-lg text-neutral-900 mb-4 flex items-center gap-2">
          <Award className="w-6 h-6 text-[#FFC800]" fill="#FFC800" />
          Badges
        </h2>
        {profile.badges.length === 0 ? (
          <p className="text-neutral-600 font-semibold text-sm">
            No badges yet — keep learning!
          </p>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
            {profile.badges.map((badge) => (
              <div
                key={badge.id}
                className="bg-neutral-50 p-4 rounded-xl border border-[#e5e5e5]"
              >
                <p className="font-black text-neutral-900 mb-1">
                  {badge.name}
                </p>
                <p className="text-xs font-semibold text-neutral-600">
                  {badge.description}
                </p>
              </div>
            ))}
          </div>
        )}
      </div>

      {/* Certificates */}
      <div className="duo-card mb-6">
        <h2 className="font-black text-lg text-neutral-900 mb-4 flex items-center gap-2">
          <Trophy className="w-6 h-6 text-[#58CC02]" fill="#58CC02" />
          Certificates
        </h2>
        {!certificates || certificates.length === 0 ? (
          <p className="text-neutral-600 font-semibold text-sm">
            Complete a course to earn a certificate
          </p>
        ) : (
          <div className="space-y-3">
            {certificates.map((cert) => (
              <div
                key={cert.id}
                className="bg-[#1CB0F6]/5 border border-[#1CB0F6]/20 p-4 rounded-xl"
              >
                <div className="flex items-center justify-between mb-3">
                  <div>
                    <p className="font-black text-neutral-900">
                      {cert.courseTitle}
                    </p>
                    <p className="text-xs font-semibold text-neutral-600">
                      Issued {new Date(cert.issuedAt).toLocaleDateString()}
                    </p>
                  </div>
                </div>
                <div className="flex items-center gap-2">
                  <Link
                    to={`/certificates/${cert.certificateCode}`}
                    className="duo-btn duo-btn-secondary text-xs py-2 px-4"
                  >
                    View
                  </Link>
                  <a
                    href={getCertificatePdfUrl(cert.certificateCode)}
                    target="_blank"
                    rel="noopener noreferrer"
                    className="duo-btn duo-btn-primary text-xs py-2 px-4"
                  >
                    Download PDF
                  </a>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>

      {/* My Courses */}
      <div>
        <h2 className="font-black text-lg text-neutral-900 mb-4 flex items-center gap-2">
          <BookOpen className="w-6 h-6 text-[#58CC02]" fill="#58CC02" />
          My Courses
        </h2>
        {profile.enrollments.length === 0 ? (
          <div className="duo-card text-center py-8">
            <p className="text-neutral-600 font-semibold text-sm mb-4">
              Not enrolled in any courses yet
            </p>
            <Link to="/courses" className="duo-btn duo-btn-primary">
              Browse Courses
            </Link>
          </div>
        ) : (
          <div className="space-y-3">
            {profile.enrollments.map((e) => (
              <Link
                key={e.courseId}
                to={`/courses/${e.courseId}`}
                className="duo-card hover:border-[#58CC02] hover:translate-y-[-2px] transition-all"
              >
                <div className="flex justify-between items-center mb-3">
                  <p className="font-black text-neutral-900">{e.courseTitle}</p>
                  <span className="text-lg font-black text-[#58CC02]">
                    {e.completionPercentage}%
                  </span>
                </div>
                <div className="w-full bg-neutral-100 rounded-full h-3">
                  <div
                    className="bg-[#58CC02] h-3 rounded-full transition-all"
                    style={{ width: `${e.completionPercentage}%` }}
                  />
                </div>
              </Link>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}
