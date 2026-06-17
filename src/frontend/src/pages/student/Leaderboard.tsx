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
    return <div className="max-w-2xl mx-auto px-4 py-12"><p>Loading leaderboard...</p></div>;
  }

  if (error) {
    return <div className="max-w-2xl mx-auto px-4 py-12"><p className="text-red-600">Failed to load leaderboard.</p></div>;
  }

  return (
    <div className="max-w-2xl mx-auto px-4 py-12">
      <h1 className="text-3xl font-bold text-gray-900 mb-2">Leaderboard</h1>
      <p className="text-gray-600 mb-8">Top learners ranked by XP</p>

      <div className="bg-white rounded-lg shadow-md overflow-hidden">
        <table className="w-full">
          <thead className="bg-gray-50 border-b">
            <tr>
              <th className="text-left px-4 py-3 text-sm font-medium text-gray-600">Rank</th>
              <th className="text-left px-4 py-3 text-sm font-medium text-gray-600">Learner</th>
              <th className="text-right px-4 py-3 text-sm font-medium text-gray-600">XP</th>
              <th className="text-right px-4 py-3 text-sm font-medium text-gray-600">Streak</th>
            </tr>
          </thead>
          <tbody>
            {entries?.map((entry) => {
              const isCurrentUser = user?.userId === entry.userId;
              return (
                <tr
                  key={entry.userId}
                  className={`border-b last:border-0 ${isCurrentUser ? "bg-blue-50" : ""}`}
                >
                  <td className="px-4 py-3 font-medium">
                    {entry.rank === 1 ? "🥇" : entry.rank === 2 ? "🥈" : entry.rank === 3 ? "🥉" : entry.rank}
                  </td>
                  <td className="px-4 py-3">
                    {entry.displayName}
                    {isCurrentUser && <span className="ml-2 text-xs text-blue-600">(you)</span>}
                  </td>
                  <td className="px-4 py-3 text-right font-medium text-blue-600">{entry.xp}</td>
                  <td className="px-4 py-3 text-right text-gray-600">{entry.currentStreak}d</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>
    </div>
  );
}
