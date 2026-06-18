import { useQuery } from "@tanstack/react-query";
import { fetchLeaderboard } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";
import { Flame, Trophy, Medal } from "lucide-react";

const rankStyle = (rank: number) => {
  if (rank === 1) return { bg: "bg-[#FFF8E1]", border: "border-[#FFC800]", icon: "text-[#FFC800]" };
  if (rank === 2) return { bg: "bg-[#F3F3F3]", border: "border-[#C0C0C0]", icon: "text-[#A8A8A8]" };
  if (rank === 3) return { bg: "bg-[#FCEEE3]", border: "border-[#D4885A]", icon: "text-[#D4885A]" };
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
        <div className="duo-card animate-pulse">
          <div className="h-80" />
        </div>
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
      <div className="text-center">
        <div className="w-16 h-16 rounded-2xl bg-[#FFC800] flex items-center justify-center mx-auto mb-3 shadow-[0_4px_0_#E6B400]">
          <Trophy className="w-8 h-8 text-white" />
        </div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-1">
          Leaderboard
        </h1>
        <p className="text-neutral-600 font-bold">Top learners ranked by XP</p>
      </div>

      <div className="space-y-2">
        {entries?.map((entry) => {
          const isCurrentUser = user?.userId === entry.userId;
          const style = rankStyle(entry.rank);
          return (
            <div
              key={entry.userId}
              className={`duo-card !p-4 flex items-center gap-4 ${
                isCurrentUser
                  ? "!border-[#1CB0F6] bg-[#DDF4FF]"
                  : style
                  ? `${style.bg} ${style.border} !border-2`
                  : ""
              }`}
            >
              <div className="w-10 h-10 rounded-full flex items-center justify-center shrink-0 font-black text-base bg-neutral-100 text-neutral-500">
                {entry.rank <= 3 && style ? (
                  <Medal className={`w-6 h-6 ${style.icon}`} />
                ) : (
                  entry.rank
                )}
              </div>
              <div className="flex-1 min-w-0">
                <p className="font-black text-neutral-900 truncate">
                  {entry.displayName}
                  {isCurrentUser && (
                    <span className="ml-2 text-xs font-black text-[#1CB0F6] uppercase">
                      you
                    </span>
                  )}
                </p>
              </div>
              <div className="flex items-center gap-1 text-[#FF9600] font-black text-sm shrink-0">
                <Flame className="w-4 h-4" fill="currentColor" />
                {entry.currentStreak}d
              </div>
              <div className="duo-xp-pill shrink-0">{entry.xp} XP</div>
            </div>
          );
        })}
      </div>
    </div>
  );
}