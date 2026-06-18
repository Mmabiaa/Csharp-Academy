import { useQuery } from "@tanstack/react-query";
import { fetchLeaderboard } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Flame, Trophy, Medal, Crown, Star } from "lucide-react";

// Top-3 rank config — each gets a unique icon and palette
const rankConfig = (rank: number) => {
  if (rank === 1) return {
    bg: "bg-[#FFF8E1]", border: "border-[#FFC800]",
    bubbleBg: "bg-[#FFC800]", bubbleShadow: "shadow-[0_3px_0_#E6B400]",
    icon: Crown, animClass: "duo-rank-1",
  };
  if (rank === 2) return {
    bg: "bg-[#F5F5F5]", border: "border-[#C0C0C0]",
    bubbleBg: "bg-[#A8A8A8]", bubbleShadow: "shadow-[0_3px_0_#888]",
    icon: Medal, animClass: "duo-rank-2",
  };
  if (rank === 3) return {
    bg: "bg-[#FCEEE3]", border: "border-[#D4885A]",
    bubbleBg: "bg-[#D4885A]", bubbleShadow: "shadow-[0_3px_0_#A8643A]",
    icon: Medal, animClass: "duo-rank-3",
  };
  return null;
};

export default function Leaderboard() {
  const { user } = useAuth();

  const { data: entries, isLoading, error } = useQuery({
    queryKey: ["leaderboard"],
    queryFn: () => fetchLeaderboard(20),
  });

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-16 w-64 bg-neutral-200 rounded-xl animate-pulse" />
        <div className="duo-card animate-pulse"><div className="h-80" /></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900">Leaderboard</h1>
        <div className="duo-panel border-[#FF4B4B]/40 bg-[#FFEFEF] text-center py-10">
          <p className="text-[#CC3A3A] font-black text-lg">Couldn't load the leaderboard</p>
          <p className="text-sm font-bold text-[#CC3A3A]/70 mt-1">
            Check your connection and try refreshing the page.
          </p>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <style>{`
        @keyframes duo-crown-bounce {
          0%, 100% { transform: translateY(0) scale(1) rotate(0deg); }
          30% { transform: translateY(-6px) scale(1.15) rotate(-8deg); }
          60% { transform: translateY(-2px) scale(1.07) rotate(4deg); }
        }
        @keyframes duo-medal-swing {
          0%, 100% { transform: rotate(0deg) scale(1); }
          30% { transform: rotate(-14deg) scale(1.1); }
          65% { transform: rotate(10deg) scale(1.05); }
        }
        @keyframes duo-trophy-pulse {
          0%, 100% { transform: scale(1) rotate(0deg); }
          40% { transform: scale(1.2) rotate(-8deg); }
          70% { transform: scale(0.95) rotate(5deg); }
        }
        @keyframes duo-flame-flicker {
          0%, 100% { transform: rotate(-7deg) scale(1); }
          25% { transform: rotate(9deg) scale(1.12); }
          60% { transform: rotate(-4deg) scale(1.06); }
        }
        @keyframes duo-star-burst {
          0%, 100% { transform: scale(1) rotate(0deg); }
          40% { transform: scale(1.3) rotate(20deg); }
          70% { transform: scale(0.93) rotate(-5deg); }
        }
        /* Header trophy loops */
        .duo-lb-trophy { animation: duo-trophy-pulse 2.8s ease-in-out infinite; }
        /* Rank bubble loops for top 3 */
        .duo-rank-1 { animation: duo-crown-bounce  2.2s ease-in-out infinite; }
        .duo-rank-2 { animation: duo-medal-swing   2.6s ease-in-out infinite; }
        .duo-rank-3 { animation: duo-medal-swing   3s ease-in-out infinite; }
        /* Hover on any row triggers flame flicker */
        .duo-lb-row:hover .duo-lb-flame { animation: duo-flame-flicker 0.5s ease-in-out; }
        .duo-lb-row:hover .duo-lb-xp-star { animation: duo-star-burst 0.45s ease-in-out; }
      `}</style>

      {/* Header */}
      <div className="text-center">
        <div className="w-20 h-20 rounded-3xl bg-[#FFC800] flex items-center justify-center mx-auto mb-4 shadow-[0_5px_0_#E6B400]">
          <Trophy className="w-10 h-10 text-white duo-lb-trophy" />
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1">Leaderboard</h1>
        <p className="text-neutral-600 font-bold">Top learners ranked by XP</p>
      </div>

      <div className="space-y-2">
        {entries?.map((entry) => {
          const isCurrentUser = user?.userId === entry.userId;
          const rc = rankConfig(entry.rank);
          const RankIcon = rc?.icon;

          return (
            <div
              key={entry.userId}
              className={`duo-card !p-4 flex items-center gap-4 duo-lb-row transition-all ${
                isCurrentUser
                  ? "!border-[#1CB0F6] bg-[#DDF4FF]"
                  : rc
                  ? `${rc.bg} ${rc.border} !border-2`
                  : ""
              }`}
            >
              {/* Rank bubble */}
              {rc && RankIcon ? (
                <div className={`w-11 h-11 rounded-2xl flex items-center justify-center shrink-0 ${rc.bubbleBg} ${rc.bubbleShadow}`}>
                  <RankIcon className={`w-6 h-6 text-white ${rc.animClass}`} />
                </div>
              ) : (
                <div className="w-11 h-11 rounded-full flex items-center justify-center shrink-0 font-black text-sm bg-neutral-100 text-neutral-500 border-2 border-neutral-200">
                  {entry.rank}
                </div>
              )}

              {/* Name */}
              <div className="flex-1 min-w-0">
                <p className="font-black text-neutral-900 truncate">
                  {entry.displayName}
                  {isCurrentUser && (
                    <span className="ml-2 text-xs font-black text-[#1CB0F6] bg-[#1CB0F6]/10 px-2 py-0.5 rounded-full uppercase">
                      you
                    </span>
                  )}
                </p>
              </div>

              {/* Streak */}
              <div className="flex items-center gap-1.5 shrink-0">
                <div className="w-7 h-7 bg-[#FF9600] rounded-xl flex items-center justify-center shadow-[0_2px_0_#CC7800]">
                  <Flame className="w-3.5 h-3.5 text-white duo-lb-flame" fill="white" />
                </div>
                <span className="font-black text-sm text-[#CC7800]">{entry.currentStreak}d</span>
              </div>

              {/* XP pill */}
              <div className="duo-xp-pill shrink-0 flex items-center gap-1">
                <Star className="w-3 h-3 duo-lb-xp-star" fill="currentColor" />
                {entry.xp} XP
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}