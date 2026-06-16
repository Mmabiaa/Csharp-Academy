using CsharpAcademy.Domain.Entities;

namespace CsharpAcademy.Application.Common.Interfaces;

public interface IGamificationService
{
    Task AwardXpAndUpdateStreakAsync(User user, int xpAmount, CancellationToken cancellationToken = default);
    Task CheckLessonBadgesAsync(User user, int completedLessonCount, CancellationToken cancellationToken = default);
    Task CheckQuizBadgesAsync(User user, bool passed, CancellationToken cancellationToken = default);
}
