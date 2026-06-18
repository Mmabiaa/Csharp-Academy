import { useQuery } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { fetchUserProfile, fetchCertificates, getCertificatePdfUrl } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Star, Flame, Trophy, Award, BookOpen, Download, Eye, Lock, Scroll } from "lucide-react";

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
          <Link to="/login" className="text-[#58CC02] font-black hover:underline">Log in</Link>{" "}
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
      <style>{`
        @keyframes duo-star-spin {
          0%, 100% { transform: scale(1) rotate(0deg); }
          40% { transform: scale(1.3) rotate(20deg); }
          70% { transform: scale(0.92) rotate(-5deg); }
        }
        @keyframes duo-flame-flicker {
          0%, 100% { transform: rotate(-7deg) scale(1); }
          25% { transform: rotate(8deg) scale(1.12); }
          60% { transform: rotate(-4deg) scale(1.06); }
        }
        @keyframes duo-trophy-wobble {
          0%, 100% { transform: rotate(0deg) scale(1); }
          25% { transform: rotate(-10deg) scale(1.1); }
          50% { transform: rotate(10deg) scale(1.1); }
          75% { transform: rotate(-4deg) scale(1.04); }
        }
        @keyframes duo-award-bounce {
          0%, 100% { transform: translateY(0) scale(1); }
          40% { transform: translateY(-5px) scale(1.1); }
          70% { transform: translateY(-2px) scale(1.05); }
        }
        @keyframes duo-book-flip {
          0%, 100% { transform: scaleX(1) rotate(0deg); }
          30% { transform: scaleX(0.85) rotate(-6deg); }
          65% { transform: scaleX(1.08) rotate(3deg); }
        }
        @keyframes duo-scroll-roll {
          0%, 100% { transform: rotate(0deg) scale(1); }
          40% { transform: rotate(-12deg) scale(1.1); }
          70% { transform: rotate(6deg) scale(1.05); }
        }
        .duo-stat-card:hover .duo-stat-star  { animation: duo-star-spin    0.5s ease-in-out; }
        .duo-stat-card:hover .duo-stat-flame { animation: duo-flame-flicker 0.55s ease-in-out; }
        .duo-stat-card:hover .duo-stat-trophy{ animation: duo-trophy-wobble 0.55s ease-in-out; }
        .duo-badge-item:hover .duo-award-icon { animation: duo-award-bounce 0.45s ease-in-out; }
        .duo-cert-item:hover .duo-scroll-icon { animation: duo-scroll-roll  0.5s ease-in-out; }
        .duo-course-row:hover .duo-book-icon  { animation: duo-book-flip    0.5s ease-in-out; }

        /* Continuous ambient animations for section headers */
        .duo-section-award { animation: duo-award-bounce 2.6s ease-in-out infinite; }
        .duo-section-trophy{ animation: duo-trophy-wobble 3s ease-in-out infinite; }
        .duo-section-book  { animation: duo-book-flip 3.2s ease-in-out infinite; }
      `}</style>

      {/* Profile Header */}
      <div className="text-center mb-8">
        <div className="w-24 h-24 bg-[#58CC02] rounded-3xl flex items-center justify-center mx-auto mb-4 shadow-[0_5px_0_#46A302] duo-pop">
          <span className="text-4xl font-black text-white">
            {profile.firstName[0]}{profile.lastName[0]}
          </span>
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1">
          {profile.firstName} {profile.lastName}
        </h1>
        <p className="text-neutral-500 font-bold">{profile.email}</p>
      </div>

      {/* Stats Grid */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-8">
        {/* XP */}
        <div className="duo-card text-center duo-stat-card">
          <div className="w-14 h-14 bg-[#FFF8E1] rounded-2xl flex items-center justify-center mx-auto mb-3 shadow-[0_3px_0_#FFE999]">
            <Star className="w-7 h-7 text-[#FFC800] duo-stat-star" fill="#FFC800" />
          </div>
          <p className="text-3xl font-black text-neutral-900 mb-1">{profile.xp}</p>
          <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">XP</p>
        </div>

        {/* Streak */}
        <div className="duo-card text-center duo-stat-card">
          <div className="w-14 h-14 bg-[#FFF1E0] rounded-2xl flex items-center justify-center mx-auto mb-3 shadow-[0_3px_0_#FFD9A8]">
            <Flame className="w-7 h-7 text-[#FF9600] duo-stat-flame" fill="#FF9600" />
          </div>
          <p className="text-3xl font-black text-neutral-900 mb-1">{profile.currentStreak}</p>
          <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">Current streak</p>
        </div>

        {/* Best Streak */}
        <div className="duo-card text-center duo-stat-card">
          <div className="w-14 h-14 bg-[#DDF4FF] rounded-2xl flex items-center justify-center mx-auto mb-3 shadow-[0_3px_0_#B3E6FF]">
            <Trophy className="w-7 h-7 text-[#1CB0F6] duo-stat-trophy" fill="#1CB0F6" />
          </div>
          <p className="text-3xl font-black text-neutral-900 mb-1">{profile.maxStreak}</p>
          <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">Best streak</p>
        </div>
      </div>

      {/* Badges */}
      <div className="duo-card mb-6">
        <h2 className="font-black text-lg text-neutral-900 mb-4 flex items-center gap-2">
          <div className="w-8 h-8 bg-[#FFC800] rounded-xl flex items-center justify-center shadow-[0_3px_0_#E6B400]">
            <Award className="w-4 h-4 text-white duo-section-award" />
          </div>
          Badges
        </h2>
        {profile.badges.length === 0 ? (
          <p className="text-neutral-500 font-bold text-sm">No badges yet — keep learning!</p>
        ) : (
          <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
            {profile.badges.map((badge) => (
              <div
                key={badge.id}
                className="bg-[#FFF8E1] p-4 rounded-2xl border-2 border-[#FFC800]/30 flex items-center gap-3 duo-badge-item"
              >
                <div className="w-10 h-10 rounded-2xl bg-[#FFC800] flex items-center justify-center shrink-0 shadow-[0_3px_0_#E6B400]">
                  <Award className="w-5 h-5 text-white duo-award-icon" />
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
          <div className="w-8 h-8 bg-[#58CC02] rounded-xl flex items-center justify-center shadow-[0_3px_0_#46A302]">
            <Scroll className="w-4 h-4 text-white duo-section-trophy" />
          </div>
          Certificates
        </h2>
        {!certificates || certificates.length === 0 ? (
          <p className="text-neutral-500 font-bold text-sm">Complete a course to earn a certificate</p>
        ) : (
          <div className="space-y-3">
            {certificates.map((cert) => (
              <div
                key={cert.id}
                className="bg-[#DDF4FF] border-2 border-[#1CB0F6]/30 p-4 rounded-2xl duo-cert-item"
              >
                <div className="flex items-center gap-3 mb-3">
                  <div className="w-10 h-10 bg-[#1CB0F6] rounded-2xl flex items-center justify-center shrink-0 shadow-[0_3px_0_#1899D6]">
                    <Scroll className="w-5 h-5 text-white duo-scroll-icon" />
                  </div>
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
          <div className="w-8 h-8 bg-[#58CC02] rounded-xl flex items-center justify-center shadow-[0_3px_0_#46A302]">
            <BookOpen className="w-4 h-4 text-white duo-section-book" />
          </div>
          My courses
        </h2>
        {profile.enrollments.length === 0 ? (
          <div className="duo-panel text-center py-10">
            <p className="text-neutral-500 font-bold text-sm mb-4">Not enrolled in any courses yet</p>
            <Link to="/courses" className="duo-btn3d duo-btn3d-green inline-flex">Browse courses</Link>
          </div>
        ) : (
          <div className="space-y-3">
            {profile.enrollments.map((e) => (
              <Link
                key={e.courseId}
                to={`/courses/${e.courseId}`}
                className="duo-card duo-card-hover hover:border-[#58CC02] block duo-course-row"
              >
                <div className="flex items-center gap-3 mb-3">
                  <div className="w-9 h-9 bg-[#58CC02] rounded-xl flex items-center justify-center shrink-0 shadow-[0_2px_0_#46A302]">
                    <BookOpen className="w-4 h-4 text-white duo-book-icon" />
                  </div>
                  <div className="flex-1 flex justify-between items-center">
                    <p className="font-black text-neutral-900">{e.courseTitle}</p>
                    <span className="text-lg font-black text-[#46A302]">{e.completionPercentage}%</span>
                  </div>
                </div>
                <div className="duo-progress-track !h-3">
                  <div className="duo-progress-fill" style={{ width: `${e.completionPercentage}%` }} />
                </div>
              </Link>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}