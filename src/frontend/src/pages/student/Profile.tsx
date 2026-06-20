import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { useState } from "react";
import { fetchUserProfile, fetchCertificates, getCertificatePdfUrl, updateProfile } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import {
  Star, Flame, Trophy, Award, BookOpen, Download,
  Eye, Lock, Scroll, Settings, User as UserIcon,
  Camera, Check, X, Loader2, Save
} from "lucide-react";

const PRESET_AVATARS = [
  "https://api.dicebear.com/7.x/avataaars/svg?seed=Felix",
  "https://api.dicebear.com/7.x/avataaars/svg?seed=Aneka",
  "https://api.dicebear.com/7.x/avataaars/svg?seed=Buddy",
  "https://api.dicebear.com/7.x/avataaars/svg?seed=Snuggles",
  "https://api.dicebear.com/7.x/avataaars/svg?seed=Milo",
  "https://api.dicebear.com/7.x/avataaars/svg?seed=Jasper",
  "https://api.dicebear.com/7.x/avataaars/svg?seed=Cookie",
  "https://api.dicebear.com/7.x/avataaars/svg?seed=Luna",
];

export default function Profile() {
  const { token, isAuthenticated, user: authUser, updateUserSettings } = useAuth();
  const queryClient = useQueryClient();
  const [activeTab, setActiveTab] = useState<"profile" | "settings">("profile");

  // Settings form state
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    profileImageUrl: "",
    currentPassword: "",
    newPassword: "",
  });
  const [successMsg, setSuccessMsg] = useState("");
  const [errorMsg, setErrorMsg] = useState("");

  const { data: profile, isLoading, error } = useQuery({
    queryKey: ["profile"],
    queryFn: async () => {
      const data = await fetchUserProfile(token!);
      setFormData({
        firstName: data.firstName,
        lastName: data.lastName,
        email: data.email,
        profileImageUrl: data.profileImageUrl || "",
        currentPassword: "",
        newPassword: "",
      });
      return data;
    },
    enabled: isAuthenticated && !!token,
  });

  const { data: certificates } = useQuery({
    queryKey: ["certificates"],
    queryFn: () => fetchCertificates(token!),
    enabled: isAuthenticated && !!token,
  });

  const updateMutation = useMutation({
    mutationFn: (data: any) => updateProfile(token!, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["profile"] });
      updateUserSettings({
        firstName: formData.firstName,
        lastName: formData.lastName,
        email: formData.email,
        profileImageUrl: formData.profileImageUrl
      });
      setSuccessMsg("Profile updated successfully!");
      setFormData(prev => ({ ...prev, currentPassword: "", newPassword: "" }));
      setTimeout(() => setSuccessMsg(""), 3000);
    },
    onError: (err: Error) => {
      setErrorMsg(err.message);
      setTimeout(() => setErrorMsg(""), 5000);
    }
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

  const handleSave = (e: React.FormEvent) => {
    e.preventDefault();
    updateMutation.mutate({
      userId: profile.userId,
      ...formData
    });
  };

  return (
    <div className="space-y-6 pb-24 md:pb-0 max-w-4xl mx-auto">
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

        .duo-stat-star   { animation: duo-star-spin    2.2s ease-in-out infinite; }
        .duo-stat-flame  { animation: duo-flame-flicker 2.4s ease-in-out infinite; }
        .duo-stat-trophy { animation: duo-trophy-wobble 2.8s ease-in-out infinite; }
        .duo-award-icon  { animation: duo-award-bounce  2.4s ease-in-out infinite; }
        .duo-scroll-icon { animation: duo-scroll-roll   2.6s ease-in-out infinite; }
        .duo-book-icon   { animation: duo-book-flip     2.8s ease-in-out infinite; }

        .duo-badge-item:nth-child(2n) .duo-award-icon { animation-delay: 0.3s; }
        .duo-badge-item:nth-child(3n) .duo-award-icon { animation-delay: 0.6s; }
        .duo-cert-item:nth-child(2n) .duo-scroll-icon { animation-delay: 0.3s; }
        .duo-cert-item:nth-child(3n) .duo-scroll-icon { animation-delay: 0.6s; }
        .duo-course-row:nth-child(2n) .duo-book-icon  { animation-delay: 0.3s; }
        .duo-course-row:nth-child(3n) .duo-book-icon  { animation-delay: 0.6s; }
      `}</style>

      {/* Tabs */}
      <div className="flex border-b-2 border-[#e5e5e5] mb-8 gap-4 overflow-x-auto no-scrollbar">
        <button
          onClick={() => setActiveTab("profile")}
          className={`px-6 py-4 text-sm font-black uppercase tracking-wide transition-all border-b-4 -mb-[2px] whitespace-nowrap ${activeTab === "profile"
              ? "border-[#58CC02] text-[#58CC02]"
              : "border-transparent text-neutral-400 hover:text-neutral-500"
            }`}
        >
          <div className="flex items-center gap-2">
            <UserIcon className="w-5 h-5" />
            <span>Profile</span>
          </div>
        </button>
        <button
          onClick={() => setActiveTab("settings")}
          className={`px-6 py-4 text-sm font-black uppercase tracking-wide transition-all border-b-4 -mb-[2px] whitespace-nowrap ${activeTab === "settings"
              ? "border-[#1CB0F6] text-[#1CB0F6]"
              : "border-transparent text-neutral-400 hover:text-neutral-500"
            }`}
        >
          <div className="flex items-center gap-2">
            <Settings className="w-5 h-5" />
            <span>System Settings</span>
          </div>
        </button>
      </div>

      {activeTab === "profile" ? (
        <div className="space-y-8">
          {/* Profile Header */}
          <div className="text-center group">
            <div className="relative inline-block">
              {profile.profileImageUrl ? (
                <img
                  src={profile.profileImageUrl}
                  alt="Avatar"
                  className="w-24 h-24 rounded-3xl object-cover bg-white shadow-[0_5px_0_#e5e5e5] duo-pop p-1 border-2 border-[#e5e5e5]"
                />
              ) : (
                <div className="w-24 h-24 bg-[#58CC02] rounded-3xl flex items-center justify-center mx-auto shadow-[0_5px_0_#46A302] duo-pop">
                  <span className="text-4xl font-black text-white">
                    {profile.firstName[0]}{profile.lastName[0]}
                  </span>
                </div>
              )}
              <button
                onClick={() => setActiveTab("settings")}
                className="absolute -right-2 -bottom-2 w-10 h-10 bg-white border-2 border-[#e5e5e5] rounded-xl flex items-center justify-center text-neutral-400 hover:text-[#1CB0F6] shadow-[0_2px_0_#e5e5e5] hover:translate-y-[-1px] transition-all"
              >
                <Camera className="w-5 h-5" />
              </button>
            </div>
            <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1 mt-4">
              {profile.firstName} {profile.lastName}
            </h1>
            <p className="text-neutral-500 font-bold">{profile.email}</p>
          </div>

          {/* Stats Grid */}
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
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

          {/* Badges, Certificates, and Courses sections - Simplified for readability */}
          <div className="space-y-6">
            {/* Badges */}
            <div className="duo-card">
              <h2 className="font-black text-lg text-neutral-900 mb-4 flex items-center gap-2">
                <div className="w-8 h-8 bg-[#FFC800] rounded-xl flex items-center justify-center shadow-[0_3px_0_#E6B400]">
                  <Award className="w-4 h-4 text-white" />
                </div>
                Badges
              </h2>
              {profile.badges.length === 0 ? (
                <p className="text-neutral-500 font-bold text-sm">No badges yet — keep learning!</p>
              ) : (
                <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
                  {profile.badges.map((badge) => (
                    <div key={badge.id} className="bg-[#FFF8E1] p-4 rounded-2xl border-2 border-[#FFC800]/30 flex items-center gap-3 duo-badge-item">
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
            <div className="duo-card">
              <h2 className="font-black text-lg text-neutral-900 mb-4 flex items-center gap-2">
                <div className="w-8 h-8 bg-[#58CC02] rounded-xl flex items-center justify-center shadow-[0_3px_0_#46A302]">
                  <Scroll className="w-4 h-4 text-white" />
                </div>
                Certificates
              </h2>
              {!certificates || certificates.length === 0 ? (
                <p className="text-neutral-500 font-bold text-sm">Complete a course to earn a certificate</p>
              ) : (
                <div className="space-y-3">
                  {certificates.map((cert) => (
                    <div key={cert.id} className="bg-[#DDF4FF] border-2 border-[#1CB0F6]/30 p-4 rounded-2xl duo-cert-item">
                      <div className="flex items-center gap-3 mb-3">
                        <div className="w-10 h-10 bg-[#1CB0F6] rounded-2xl flex items-center justify-center shrink-0 shadow-[0_3px_0_#1899D6]">
                          <Scroll className="w-5 h-5 text-white duo-scroll-icon" />
                        </div>
                        <div>
                          <p className="font-black text-neutral-900">{cert.courseTitle}</p>
                          <p className="text-xs font-bold text-neutral-500">Issued {new Date(cert.issuedAt).toLocaleDateString()}</p>
                        </div>
                      </div>
                      <div className="flex items-center gap-2">
                        <Link to={`/certificates/${cert.certificateCode}`} className="duo-btn3d duo-btn3d-white !px-4 !py-2 !text-xs"><Eye className="w-3.5 h-3.5" />View</Link>
                        <a href={getCertificatePdfUrl(cert.certificateCode)} target="_blank" rel="noopener noreferrer" className="duo-btn3d duo-btn3d-green !px-4 !py-2 !text-xs"><Download className="w-3.5 h-3.5" />Download PDF</a>
                      </div>
                    </div>
                  ))}
                </div>
              )}
            </div>
          </div>
        </div>
      ) : (
        <div className="space-y-8 animate-in fade-in slide-in-from-bottom-4 duration-300">
          <form onSubmit={handleSave} className="space-y-8">
            {/* Avatar Selector */}
            <div className="duo-card">
              <h2 className="font-black text-lg text-neutral-900 mb-6 flex items-center gap-2">
                <div className="w-8 h-8 bg-[#1CB0F6] rounded-xl flex items-center justify-center shadow-[0_3px_0_#1899D6]">
                  <Camera className="w-4 h-4 text-white" />
                </div>
                Customize Avatar
              </h2>

              <div className="grid grid-cols-4 md:grid-cols-8 gap-3 mb-6">
                {PRESET_AVATARS.map((avatar) => (
                  <button
                    key={avatar}
                    type="button"
                    onClick={() => setFormData(prev => ({ ...prev, profileImageUrl: avatar }))}
                    className={`relative w-full aspect-square rounded-2xl overflow-hidden border-2 transition-all p-1 bg-white ${formData.profileImageUrl === avatar
                        ? "border-[#1CB0F6] shadow-[0_0_0_2px_#1CB0F6] scale-105 z-10"
                        : "border-neutral-200 hover:border-neutral-300 hover:scale-105"
                      }`}
                  >
                    <img src={avatar} alt="Avatar option" className="w-full h-full object-cover" />
                    {formData.profileImageUrl === avatar && (
                      <div className="absolute inset-0 bg-[#1CB0F6]/10 flex items-center justify-center">
                        <div className="w-6 h-6 bg-[#1CB0F6] rounded-full flex items-center justify-center text-white shadow-sm">
                          <Check className="w-4 h-4" />
                        </div>
                      </div>
                    )}
                  </button>
                ))}
              </div>

              <div>
                <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">Custom Image URL</label>
                <input
                  type="text"
                  placeholder="Paste an image URL here..."
                  className="duo-input w-full"
                  value={formData.profileImageUrl}
                  onChange={(e) => setFormData(prev => ({ ...prev, profileImageUrl: e.target.value }))}
                />
              </div>
            </div>

            {/* Personal Info */}
            <div className="duo-card">
              <h2 className="font-black text-lg text-neutral-900 mb-6 flex items-center gap-2">
                <div className="w-8 h-8 bg-[#58CC02] rounded-xl flex items-center justify-center shadow-[0_3px_0_#46A302]">
                  <UserIcon className="w-4 h-4 text-white" />
                </div>
                Personal Information
              </h2>

              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div>
                  <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">First Name</label>
                  <input
                    type="text"
                    required
                    className="duo-input w-full"
                    value={formData.firstName}
                    onChange={(e) => setFormData(prev => ({ ...prev, firstName: e.target.value }))}
                  />
                </div>
                <div>
                  <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">Last Name</label>
                  <input
                    type="text"
                    required
                    className="duo-input w-full"
                    value={formData.lastName}
                    onChange={(e) => setFormData(prev => ({ ...prev, lastName: e.target.value }))}
                  />
                </div>
                <div className="md:col-span-2">
                  <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">Email Address</label>
                  <input
                    type="email"
                    required
                    className="duo-input w-full"
                    value={formData.email}
                    onChange={(e) => setFormData(prev => ({ ...prev, email: e.target.value }))}
                  />
                </div>
              </div>
            </div>

            {/* Password Management */}
            <div className="duo-card">
              <h2 className="font-black text-lg text-neutral-900 mb-6 flex items-center gap-2">
                <div className="w-8 h-8 bg-[#FF4B4B] rounded-xl flex items-center justify-center shadow-[0_3px_0_#CC3A3A]">
                  <Lock className="w-4 h-4 text-white" />
                </div>
                Security
              </h2>

              <p className="text-sm text-neutral-500 font-bold mb-6">Leave blank if you don't want to change your password.</p>

              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div>
                  <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">Current Password</label>
                  <input
                    type="password"
                    className="duo-input w-full"
                    placeholder="••••••••"
                    value={formData.currentPassword}
                    onChange={(e) => setFormData(prev => ({ ...prev, currentPassword: e.target.value }))}
                  />
                </div>
                <div>
                  <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">New Password</label>
                  <input
                    type="password"
                    className="duo-input w-full"
                    placeholder="Min. 8 characters"
                    value={formData.newPassword}
                    onChange={(e) => setFormData(prev => ({ ...prev, newPassword: e.target.value }))}
                  />
                </div>
              </div>
            </div>

            {/* Error/Success FeedBack */}
            {errorMsg && (
              <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] flex items-center gap-3 py-4 text-[#CC3A3A]">
                <X className="w-5 h-5" />
                <span className="font-bold">{errorMsg}</span>
              </div>
            )}
            {successMsg && (
              <div className="duo-panel border-[#58CC02]/40 bg-[#F7FFF0] flex items-center gap-3 py-4 text-[#46A302]">
                <Check className="w-5 h-5" />
                <span className="font-bold">{successMsg}</span>
              </div>
            )}

            {/* Action Buttons */}
            <div className="flex items-center gap-4">
              <button
                type="submit"
                disabled={updateMutation.isPending}
                className="duo-btn3d duo-btn3d-green min-w-[200px]"
              >
                {updateMutation.isPending ? (
                  <>
                    <Loader2 className="w-5 h-5 animate-spin" />
                    Saving Changes...
                  </>
                ) : (
                  <>
                    <Save className="w-5 h-5" />
                    Save Changes
                  </>
                )}
              </button>
              <button
                type="button"
                onClick={() => setActiveTab("profile")}
                className="duo-btn3d duo-btn3d-white"
              >
                Cancel
              </button>
            </div>
          </form>
        </div>
      )}
    </div>
  );
}