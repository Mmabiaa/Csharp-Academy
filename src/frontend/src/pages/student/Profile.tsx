import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchUserProfile, fetchCertificates, getCertificatePdfUrl } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Star, Flame, Trophy, Award, BookOpen, Download, Eye, Lock } from "lucide-react";

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
        <div className="w-16 h-16 rounded-full bg-[#DDF4FF] flex items-center justify-center mx-auto mb-4">
          <Lock className="w-8 h-8 text-[#1CB0F6]" />
        </div>
        <p className="text-neutral-600 text-lg font-bold mb-2">
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
        <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-10">
          <p className="text-[#CC3A3A] font-black text-lg">Couldn't load your profile</p>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      {/* Profile Header */}
      <div className="text-center mb-8">
        <div className="w-24 h-24 bg-[#58CC02] rounded-3xl flex items-center justify-center mx-auto mb-4 shadow-[0_5px_0_#46A302] duo-pop">
          <span className="text-4xl font-black text-white">
            {profile.firstName[0]}
            {profile.lastName[0]}
          </span>
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1">
          {profile.firstName} {profile.lastName}
        </h1>
        <p className="text-neutral-500 font-bold">{profile.email}</p>
      </div>

      {/* Stats Grid */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
        <div className="duo-card text-center">
          <div className="w-12 h-12 bg-[#FFF8E1] rounded-2xl flex items-center justify-center mx-auto mb-3">
            <Star className="w-6 h-6 text-[#FFC800]" fill="#FFC800" />
          </div>
          <p className="text-3xl font-black text-neutral-900 mb-1">{profile.xp}</p>
          <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">XP</p>
        </div>
        <div className="duo-card text-center">
          <div className="w-12 h-12 bg-[#FFF1E0] rounded-2xl flex items-center justify-center mx-auto mb-3">
            <Flame className="w-6 h-6 text-[#FF9600]" fill="#FF9600" />
          </div>
          <p className="text-3xl font-black text-neutral-900 mb-1">{profile.currentStreak}</p>
          <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">
            Current streak
          </p>
        </div>
        <div className="duo-card text-center">
          <div className="w-12 h-12 bg-[#DDF4FF] rounded-2xl flex items-center justify-center mx-auto mb-3">
            <Trophy className="w-6 h-6 text-[#1CB0F6]" fill="#1CB0F6" />
          </div>
          <p className="text-3xl font-black text-neutral-900 mb-1">{profile.maxStreak}</p>
          <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">
            Best streak
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
          <p className="text-neutral-500 font-bold text-sm">No badges yet — keep learning!</p>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
            {profile.badges.map((badge) => (
              <div
                key={badge.id}
                className="bg-[#FFF8E1] p-4 rounded-2xl border-2 border-[#FFC800]/30 flex items-center gap-3"
              >
                <div className="w-9 h-9 rounded-full bg-[#FFC800] flex items-center justify-center shrink-0 shadow-[0_2px_0_#E6B400]">
                  <Award className="w-5 h-5 text-white" />
                </div>
                <div>
                  <p className="font-black text-neutral-900 mb-0.5">{badge.name}</p>
                  <p className="text-xs font-bold text-neutral-500">{badge.description}</p>
                </div>
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
          <p className="text-neutral-500 font-bold text-sm">
            Complete a course to earn a certificate
          </p>
        ) : (
          <div className="space-y-3">
            {certificates.map((cert) => (
              <div
                key={cert.id}
                className="bg-[#DDF4FF] border-2 border-[#1CB0F6]/30 p-4 rounded-2xl"
              >
                <div className="flex items-center justify-between mb-3">
                  <div>
                    <p className="font-black text-neutral-900">{cert.courseTitle}</p>
                    <p className="text-xs font-bold text-neutral-500">
                      Issued {new Date(cert.issuedAt).toLocaleDateString()}
                    </p>
                  </div>
                </div>
                <div className="flex items-center gap-2">
                  <Link
                    to={`/certificates/${cert.certificateCode}`}
                    className="duo-btn3d duo-btn3d-white !px-4 !py-2 !text-xs"
                  >
                    <Eye className="w-3.5 h-3.5" />
                    View
                  </Link>
                  <a
                    href={getCertificatePdfUrl(cert.certificateCode)}
                    target="_blank"
                    rel="noopener noreferrer"
                    className="duo-btn3d duo-btn3d-green !px-4 !py-2 !text-xs"
                  >
                    <Download className="w-3.5 h-3.5" />
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
          My courses
        </h2>
        {profile.enrollments.length === 0 ? (
          <div className="duo-panel text-center py-10">
            <p className="text-neutral-500 font-bold text-sm mb-4">
              Not enrolled in any courses yet
            </p>
            <Link to="/courses" className="duo-btn3d duo-btn3d-green inline-flex">
              Browse courses
            </Link>
          </div>
        ) : (
          <div className="space-y-3">
            {profile.enrollments.map((e) => (
              <Link
                key={e.courseId}
                to={`/courses/${e.courseId}`}
                className="duo-card duo-card-hover hover:border-[#58CC02] block"
              >
                <div className="flex justify-between items-center mb-3">
                  <p className="font-black text-neutral-900">{e.courseTitle}</p>
                  <span className="text-lg font-black text-[#46A302]">
                    {e.completionPercentage}%
                  </span>
                </div>
                <div className="duo-progress-track !h-3">
                  <div
                    className="duo-progress-fill"
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