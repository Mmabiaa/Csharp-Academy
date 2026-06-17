using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Application.Common.Interfaces;

public interface IGamificationService
{
    Task AwardXpAndUpdateStreakAsync(User user, int xpAmount, CancellationToken cancellationToken = default);
    Task CheckLessonBadgesAsync(User user, int completedLessonCount, CancellationToken cancellationToken = default);
    Task CheckQuizBadgesAsync(User user, bool passed, CancellationToken cancellationToken = default);
    Task CheckCourseCompletionBadgesAsync(User user, CancellationToken cancellationToken = default);
    Task<int> AwardPracticeXpAsync(int userId, int exerciseId, CancellationToken cancellationToken = default);
    Task<int> AwardChallengeXpAsync(int userId, int challengeId, int xpAmount, CancellationToken cancellationToken = default);
}
