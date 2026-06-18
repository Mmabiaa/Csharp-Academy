import { useQuery } from "@tanstack/react-query";
import { fetchLeaderboard } from "../../lib/api";
import { useAuth } from "../../context/AuthContext";

export default function Leaderboard() {
  const { user } = useAuth();

  const { data: entries, isLoading, error } = useQuery({
    queryKey: ["leaderboard"],
    queryFn: () => fetchLeaderboard(20),
  });

  if (isLoading) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-16 w-64 bg-neutral-200 rounded-xl" />
        <div className="duo-card animate-pulse">
          <div className="h-80" />
        </div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="space-y-6 pb-24 md:pb-0">
        <div className="h-16 w-64 bg-neutral-200 rounded-xl" />
        <div className="duo-card bg-[#FF4B4B]/10 border-[#FF4B4B]/30">
          <p className="text-[#FF4B4B] font-black">Failed to load leaderboard.</p>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6 pb-24 md:pb-0">
      <div>
        <h1 className="text-2xl md:text-3xl font-black text-neutral-900 mb-2">
          Leaderboard
        </h1>
        <p className="text-neutral-600 font-semibold">
          Top learners ranked by XP
        </p>
      </div>

      <div className="duo-card overflow-hidden">
        <table className="w-full">
          <thead className="bg-neutral-50 border-b border-[#e5e5e5]">
            <tr>
              <th className="text-left px-4 py-3 text-sm font-black text-neutral-700">
                Rank
              </th>
              <th className="text-left px-4 py-3 text-sm font-black text-neutral-700">
                Learner
              </th>
              <th className="text-right px-4 py-3 text-sm font-black text-neutral-700">
                XP
              </th>
              <th className="text-right px-4 py-3 text-sm font-black text-neutral-700">
                Streak
              </th>
            </tr>
          </thead>
          <tbody>
            {entries?.map((entry) => {
              const isCurrentUser = user?.userId === entry.userId;
              return (
                <tr
                  key={entry.userId}
                  className={`border-b border-[#e5e5e5] last:border-0 ${isCurrentUser ? "bg-[#E6F7FF]" : ""}`}
                >
                  <td className="px-4 py-3 font-black">
                    {entry.rank === 1 ? "🥇" : entry.rank === 2 ? "🥈" : entry.rank === 3 ? "🥉" : entry.rank}
                  </td>
                  <td className="px-4 py-3">
                    <span className="font-black text-neutral-900">{entry.displayName}</span>
                    {isCurrentUser && <span className="ml-2 text-xs font-bold text-[#1CB0F6]">(you)</span>}
                  </td>
                  <td className="px-4 py-3 text-right font-black text-[#58CC02]">{entry.xp}</td>
                  <td className="px-4 py-3 text-right font-bold text-neutral-700">{entry.currentStreak}d</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
}
