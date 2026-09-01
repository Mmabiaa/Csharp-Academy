import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { Link } from "react-router-dom";
import { useState, useEffect } from "react";
import { fetchUserProfile, fetchCertificates, getCertificatePdfUrl, updateProfile } from "../../lib/api";
import UserAvatar from "../../components/UserAvatar";
import { useAuth } from "../../context/AuthContext";
import { useSound } from "../../context/SoundContext";
import { useVoice } from "../../context/VoiceContext";
import {
  Download, Eye, Lock, Settings, User as UserIcon,
  Camera, Check, X, Loader2, Save, Volume2, Mic
} from "lucide-react";

// ─── Lottie web-component type declaration ───────────────────────────────────
declare global {
  namespace JSX {
    interface IntrinsicElements {
      "lottie-player": React.DetailedHTMLProps<
        React.HTMLAttributes<HTMLElement> & {
          src?: string;
          background?: string;
          speed?: string;
          loop?: boolean;
          autoplay?: boolean;
          style?: React.CSSProperties;
        },
        HTMLElement
      >;
    }
  }
}

// ─── Reusable Lottie icon component ──────────────────────────────────────────
interface LottieIconProps {
  src: string;
  size?: number;
  speed?: number;
  className?: string;
}

function LottieIcon({ src, size = 40, speed = 1, className = "" }: LottieIconProps) {
  return (
    <lottie-player
      src={src}
      background="transparent"
      speed={String(speed)}
      style={{ width: size, height: size }}
      loop
      autoplay
      class={className}
    />
  );
}

// ─── Animation paths (served from /public/animations/) ───────────────────────
const ANIM = {
  star: "/animations/award.json",       // XP stat
  streak: "/animations/streak.json",      // Current streak
  trophy: "/animations/trophy.json",      // Best streak
  award: "/animations/award.json",       // Badges section header
  badge: "/animations/badge.json",       // Individual badge items
  certificate: "/animations/Certificate.json", // Certificates
} as const;

// ─── Preset avatars ───────────────────────────────────────────────────────────
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

// ─── Main component ───────────────────────────────────────────────────────────
export default function Profile() {
  const { token, isAuthenticated, updateUserSettings } = useAuth();
  const { isSoundEnabled, setSoundEnabled, playSound } = useSound();
  const {
    isVoiceRecognitionEnabled,
    setVoiceRecognitionEnabled,
    isVoiceFeedbackEnabled,
    setVoiceFeedbackEnabled,
    isListening,
    toggleListening,
    supported: voiceSupported,
    speakFeedback,
  } = useVoice();
  const queryClient = useQueryClient();
  const [activeTab, setActiveTab] = useState<"profile" | "settings">("profile");

  // Dynamically load lottie-player web component once on mount
  useEffect(() => {
    import("@lottiefiles/lottie-player");
  }, []);

  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    profileImageUrl: "",
    voiceRecognitionEnabled: false,
    voiceFeedbackEnabled: true,
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
        voiceRecognitionEnabled: !!data.voiceRecognitionEnabled,
        voiceFeedbackEnabled: typeof data.voiceFeedbackEnabled === "boolean" ? data.voiceFeedbackEnabled : true,
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
    mutationFn: (data: any) => updateProfile(token!, { ...data, voiceRecognitionEnabled: formData.voiceRecognitionEnabled, voiceFeedbackEnabled: formData.voiceFeedbackEnabled }),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["profile"] });
      updateUserSettings({
        firstName: formData.firstName,
        lastName: formData.lastName,
        email: formData.email,
        profileImageUrl: formData.profileImageUrl,
        voiceRecognitionEnabled: formData.voiceRecognitionEnabled,
        voiceFeedbackEnabled: formData.voiceFeedbackEnabled,
      } as any);
      setSuccessMsg("Profile updated successfully!");
      setFormData((prev) => ({ ...prev, currentPassword: "", newPassword: "" }));
      setTimeout(() => setSuccessMsg(""), 3000);
    },
    onError: (err: Error) => {
      setErrorMsg(err.message);
      setTimeout(() => setErrorMsg(""), 5000);
    },
  });

  // ── Guards ─────────────────────────────────────────────────────────────────
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

  const handleSave = (e: React.FormEvent) => {
    e.preventDefault();
    updateMutation.mutate({ userId: profile.userId, ...formData });
  };

  // ── Render ──────────────────────────────────────────────────────────────────
  return (
    <div className="space-y-6 pb-24 md:pb-0 max-w-4xl mx-auto">

      {/* ── Tabs ── */}
      <div className="flex border-b-2 border-[#e5e5e5] mb-8 gap-4 overflow-x-auto no-scrollbar">
        <button
          onClick={() => { setActiveTab("profile"); playSound("click"); }}
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
          onClick={() => { setActiveTab("settings"); playSound("click"); }}
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

      {/* ════════════════════════════════════════════════════════════════════
          PROFILE TAB
      ════════════════════════════════════════════════════════════════════ */}
      {activeTab === "profile" ? (
        <div className="space-y-8">

          {/* ── Avatar header ── */}
          <div className="text-center">
            <div className="relative inline-block">
              <UserAvatar
                profileImageUrl={profile.profileImageUrl}
                firstName={profile.firstName}
                lastName={profile.lastName}
                size="xl"
                className="shadow-[0_5px_0_#e5e5e5] duo-pop border-4 border-white"
              />
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

          {/* ── Stats grid ── */}
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4">

            {/* XP */}
            <div className="duo-card text-center duo-stat-card group">
              <div className="w-16 h-16 bg-[#FFF8E1] rounded-2xl flex items-center justify-center mx-auto mb-3 shadow-[0_3px_0_#FFE999] transition-transform group-hover:scale-110 group-hover:-rotate-3">
                <LottieIcon src={ANIM.star} size={48} speed={0.8} />
              </div>
              <p className="text-3xl font-black text-neutral-900 mb-1">{profile.xp}</p>
              <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">XP</p>
            </div>

            {/* Current Streak */}
            <div className="duo-card text-center duo-stat-card group">
              <div className="w-16 h-16 bg-[#FFF1E0] rounded-2xl flex items-center justify-center mx-auto mb-3 shadow-[0_3px_0_#FFD9A8] transition-transform group-hover:scale-110 group-hover:-rotate-3">
                <LottieIcon src={ANIM.streak} size={48} speed={1} />
              </div>
              <p className="text-3xl font-black text-neutral-900 mb-1">{profile.currentStreak}</p>
              <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">Current streak</p>
            </div>

            {/* Best Streak */}
            <div className="duo-card text-center duo-stat-card group">
              <div className="w-16 h-16 bg-[#DDF4FF] rounded-2xl flex items-center justify-center mx-auto mb-3 shadow-[0_3px_0_#B3E6FF] transition-transform group-hover:scale-110 group-hover:-rotate-3">
                <LottieIcon src={ANIM.trophy} size={48} speed={0.7} />
              </div>
              <p className="text-3xl font-black text-neutral-900 mb-1">{profile.maxStreak}</p>
              <p className="text-xs font-black text-neutral-400 uppercase tracking-wide">Best streak</p>
            </div>
          </div>

          {/* ── Badges & Certificates ── */}
          <div className="space-y-6">

            {/* Badges */}
            <div className="duo-card">
              <h2 className="font-black text-lg text-neutral-900 mb-4 flex items-center gap-2">
                <div className="w-9 h-9 bg-[#FFC800] rounded-xl flex items-center justify-center shadow-[0_3px_0_#E6B400] overflow-hidden">
                  <LottieIcon src={ANIM.award} size={36} speed={0.8} />
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
                      className="bg-[#FFF8E1] p-4 rounded-2xl border-2 border-[#FFC800]/30 flex items-center gap-3 hover:border-[#FFC800]/60 hover:shadow-sm transition-all"
                    >
                      {/* Individual badge — larger Lottie, no background box needed */}
                      <div className="w-12 h-12 shrink-0 drop-shadow-sm">
                        <LottieIcon src={ANIM.badge} size={48} speed={0.7} />
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
                <div className="w-9 h-9 bg-[#58CC02] rounded-xl flex items-center justify-center shadow-[0_3px_0_#46A302] overflow-hidden">
                  <LottieIcon src={ANIM.certificate} size={36} speed={0.8} />
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
                      className="bg-[#DDF4FF] border-2 border-[#1CB0F6]/30 p-4 rounded-2xl hover:border-[#1CB0F6]/60 hover:shadow-sm transition-all duo-cert-item"
                    >
                      <div className="flex items-center gap-3 mb-3">
                        <div className="w-12 h-12 shrink-0">
                          <LottieIcon src={ANIM.certificate} size={48} speed={0.6} />
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
          </div>
        </div>

      ) : (
        /* ════════════════════════════════════════════════════════════════════
            SETTINGS TAB
        ════════════════════════════════════════════════════════════════════ */
        <div className="space-y-8 animate-in fade-in slide-in-from-bottom-4 duration-300">
          <form onSubmit={handleSave} className="space-y-8">

            {/* ── Avatar Selector ── */}
            <div className="duo-card">
              <h2 className="font-black text-lg text-neutral-900 mb-6 flex items-center gap-2">
                <div className="w-8 h-8 bg-[#1CB0F6] rounded-xl flex items-center justify-center shadow-[0_3px_0_#1899D6]">
                  <Camera className="w-4 h-4 text-white" />
                </div>
                Customize Avatar
              </h2>

              <div className="flex items-center gap-4 mb-6 p-4 bg-neutral-50 rounded-2xl border-2 border-neutral-200">
                <UserAvatar
                  profileImageUrl={formData.profileImageUrl || undefined}
                  firstName={formData.firstName || profile.firstName}
                  lastName={formData.lastName || profile.lastName}
                  size="lg"
                  className="border-4 border-white shadow-md flex-shrink-0"
                />
                <div>
                  <p className="font-black text-neutral-800">
                    {formData.firstName || profile.firstName}{" "}
                    {formData.lastName || profile.lastName}
                  </p>
                  <p className="text-sm text-neutral-500 font-bold">Avatar preview</p>
                </div>
              </div>

              <div className="grid grid-cols-4 md:grid-cols-8 gap-3 mb-6">
                {PRESET_AVATARS.map((avatar) => (
                  <button
                    key={avatar}
                    type="button"
                    onClick={() => setFormData((prev) => ({ ...prev, profileImageUrl: avatar }))}
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
                <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">
                  Custom Image URL
                </label>
                <input
                  type="text"
                  placeholder="Paste an image URL here..."
                  className="duo-input w-full"
                  value={formData.profileImageUrl}
                  onChange={(e) => setFormData((prev) => ({ ...prev, profileImageUrl: e.target.value }))}
                />
              </div>
            </div>

            {/* ── Personal Info ── */}
            <div className="duo-card">
              <h2 className="font-black text-lg text-neutral-900 mb-6 flex items-center gap-2">
                <div className="w-8 h-8 bg-[#58CC02] rounded-xl flex items-center justify-center shadow-[0_3px_0_#46A302]">
                  <UserIcon className="w-4 h-4 text-white" />
                </div>
                Personal Information
              </h2>

              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div>
                  <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">
                    First Name
                  </label>
                  <input
                    type="text"
                    required
                    className="duo-input w-full"
                    value={formData.firstName}
                    onChange={(e) => setFormData((prev) => ({ ...prev, firstName: e.target.value }))}
                  />
                </div>
                <div>
                  <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">
                    Last Name
                  </label>
                  <input
                    type="text"
                    required
                    className="duo-input w-full"
                    value={formData.lastName}
                    onChange={(e) => setFormData((prev) => ({ ...prev, lastName: e.target.value }))}
                  />
                </div>
                <div className="md:col-span-2">
                  <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">
                    Email Address
                  </label>
                  <input
                    type="email"
                    required
                    className="duo-input w-full"
                    value={formData.email}
                    onChange={(e) => setFormData((prev) => ({ ...prev, email: e.target.value }))}
                  />
                </div>
              </div>
            </div>

            {/* ── Security ── */}
            <div className="duo-card">
              <h2 className="font-black text-lg text-neutral-900 mb-6 flex items-center gap-2">
                <div className="w-8 h-8 bg-[#FF4B4B] rounded-xl flex items-center justify-center shadow-[0_3px_0_#CC3A3A]">
                  <Lock className="w-4 h-4 text-white" />
                </div>
                Security
              </h2>
              <p className="text-sm text-neutral-500 font-bold mb-6">
                Leave blank if you don't want to change your password.
              </p>
              <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div>
                  <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">
                    Current Password
                  </label>
                  <input
                    type="password"
                    className="duo-input w-full"
                    placeholder="••••••••"
                    value={formData.currentPassword}
                    onChange={(e) => setFormData((prev) => ({ ...prev, currentPassword: e.target.value }))}
                  />
                </div>
                <div>
                  <label className="block text-xs font-black text-neutral-400 uppercase tracking-widest mb-2">
                    New Password
                  </label>
                  <input
                    type="password"
                    className="duo-input w-full"
                    placeholder="Min. 8 characters"
                    value={formData.newPassword}
                    onChange={(e) => setFormData((prev) => ({ ...prev, newPassword: e.target.value }))}
                  />
                </div>
              </div>
            </div>

            {/* ── Sound Preferences ── */}
            <div className="duo-card">
              <h2 className="font-black text-lg text-neutral-900 mb-6 flex items-center gap-2">
                <div className="w-8 h-8 bg-[#1CB0F6] rounded-xl flex items-center justify-center shadow-[0_3px_0_#1899D6]">
                  <Volume2 className="w-4 h-4 text-white" />
                </div>
                Sound Preferences
              </h2>
              <div className="flex items-center justify-between p-4 bg-neutral-50 rounded-2xl border-2 border-neutral-200">
                <div>
                  <p className="font-black text-neutral-800">Sound Effects</p>
                  <p className="text-sm text-neutral-500 font-bold">Enable interactive learning sounds</p>
                </div>
                <button
                  type="button"
                  onClick={() => {
                    const next = !isSoundEnabled;
                    setSoundEnabled(next);
                    if (next) playSound("click");
                  }}
                  className={`w-14 h-8 rounded-full transition-all relative ${isSoundEnabled ? "bg-[#58CC02]" : "bg-neutral-300"
                    }`}
                >
                  <div
                    className={`absolute top-1 w-6 h-6 bg-white rounded-full transition-all shadow-sm ${isSoundEnabled ? "left-7" : "left-1"
                      }`}
                  />
                </button>
              </div>
            </div>

            {/* ── Voice & Speech Preferences ── */}
            <div className="duo-card">
              <h2 className="font-black text-lg text-neutral-900 mb-6 flex items-center gap-2">
                <div className="w-8 h-8 bg-[#FF6FAE] rounded-xl flex items-center justify-center shadow-[0_3px_0_#E0488C]">
                  <Mic className="w-4 h-4 text-white" />
                </div>
                Voice & Speech
              </h2>

              <div className="space-y-4">
                <div className="flex items-center justify-between p-4 bg-neutral-50 rounded-2xl border-2 border-neutral-200">
                  <div>
                    <p className="font-black text-neutral-800 flex items-center gap-2">
                      Voice Recognition
                      {!voiceSupported && (
                        <span className="text-[10px] font-black uppercase tracking-wide px-2 py-0.5 rounded-full bg-[#FF4B4B]/10 text-[#CC3A3A] border border-[#FF4B4B]/20">
                          Unsupported browser
                        </span>
                      )}
                    </p>
                    <p className="text-sm text-neutral-500 font-bold">
                      Wake the companion by saying <code className="px-1.5 py-0.5 rounded bg-white border border-neutral-200 text-[#534AB7] font-black">"C# Academy"</code>, <code className="px-1.5 py-0.5 rounded bg-white border border-neutral-200 text-[#534AB7] font-black">"hello"</code>, or <code className="px-1.5 py-0.5 rounded bg-white border border-neutral-200 text-[#534AB7] font-black">"I need help"</code>
                    </p>
                    <p className="text-xs text-neutral-400 font-bold mt-1">
                      Shortcut: press <kbd className="px-1.5 py-0.5 rounded bg-white border border-neutral-200 text-neutral-600 font-black shadow-[0_1px_0_#e5e5e5]">Alt</kbd>+<kbd className="px-1.5 py-0.5 rounded bg-white border border-neutral-200 text-neutral-600 font-black shadow-[0_1px_0_#e5e5e5]">M</kbd> to toggle microphone
                    </p>
                  </div>
                  <div className="flex flex-col items-end gap-2">
                    <button
                      type="button"
                      disabled={!voiceSupported}
                      onClick={async () => {
                        playSound("click");
                        const next = !isVoiceRecognitionEnabled;
                        await setVoiceRecognitionEnabled(next);
                        setFormData((prev) => ({ ...prev, voiceRecognitionEnabled: next }));
                      }}
                      className={`w-14 h-8 rounded-full transition-all relative ${isVoiceRecognitionEnabled && voiceSupported ? "bg-[#58CC02]" : "bg-neutral-300"
                        } ${!voiceSupported ? "opacity-50 cursor-not-allowed" : ""}`}
                    >
                      <div
                        className={`absolute top-1 w-6 h-6 bg-white rounded-full transition-all shadow-sm ${isVoiceRecognitionEnabled && voiceSupported ? "left-7" : "left-1"
                          }`}
                      />
                    </button>
                    {voiceSupported && (
                      <button
                        type="button"
                        onClick={() => {
                          playSound("click");
                          toggleListening();
                        }}
                        disabled={!isVoiceRecognitionEnabled}
                        className={`flex items-center gap-1.5 px-3 py-1.5 rounded-xl text-xs font-black transition-all border-2 ${
                          isListening
                            ? "bg-[#FF4B4B]/10 text-[#CC3A3A] border-[#FF4B4B]/30 animate-pulse"
                            : "bg-neutral-100 text-neutral-500 border-neutral-200 hover:border-neutral-300"
                        } ${!isVoiceRecognitionEnabled ? "opacity-50 cursor-not-allowed" : ""}`}
                      >
                        <Mic className="w-3.5 h-3.5" />
                        {isListening ? "Listening..." : "Test Mic"}
                      </button>
                    )}
                  </div>
                </div>
              </div>
            </div>

            {/* ── Feedback ── */}
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

            {/* ── Actions ── */}
            <div className="flex items-center gap-4">
              <button
                type="submit"
                onClick={() => playSound("click")}
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
                onClick={() => { setActiveTab("profile"); playSound("click"); }}
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