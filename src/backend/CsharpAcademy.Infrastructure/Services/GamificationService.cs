using CsharpAcademy.Application.Common;
using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;

namespace CsharpAcademy.Infrastructure.Services;

public class GamificationService : IGamificationService
{
    private readonly IUserRepository _userRepository;

    public const int LessonXp = GamificationRewards.LessonXp;
    public const int QuizPassXp = GamificationRewards.QuizPassXp;

    public GamificationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task AwardXpAndUpdateStreakAsync(User user, int xpAmount, CancellationToken cancellationToken = default)
    {
        user.Xp += xpAmount;
        UpdateStreak(user);
        await _userRepository.SaveAsync(user, cancellationToken);
    }

    public async Task CheckLessonBadgesAsync(User user, int completedLessonCount, CancellationToken cancellationToken = default)
    {
        if (completedLessonCount >= 1)
        {
            await _userRepository.AwardBadgeAsync(user.Id, 1, cancellationToken);
        }

        if (completedLessonCount >= 3)
        {
            await _userRepository.AwardBadgeAsync(user.Id, 3, cancellationToken);
        }

        if (user.CurrentStreak >= 3)
        {
            await _userRepository.AwardBadgeAsync(user.Id, 2, cancellationToken);
        }
    }

    public async Task CheckQuizBadgesAsync(User user, bool passed, CancellationToken cancellationToken = default)
    {
        if (passed)
        {
            await _userRepository.AwardBadgeAsync(user.Id, 4, cancellationToken);
        }
    }

    public async Task CheckCourseCompletionBadgesAsync(User user, CancellationToken cancellationToken = default)
    {
        await _userRepository.AwardBadgeAsync(user.Id, 5, cancellationToken);
    }

    private static void UpdateStreak(User user)
    {
        var today = DateTime.UtcNow.Date;

        if (user.LastActiveDate is null)
        {
            user.CurrentStreak = 1;
        }
        else if (user.LastActiveDate.Value.Date == today)
        {
            // Already active today — keep streak
        }
        else if (user.LastActiveDate.Value.Date == today.AddDays(-1))
        {
            user.CurrentStreak += 1;
        }
        else
        {
            user.CurrentStreak = 1;
        }

        user.LastActiveDate = today;
        if (user.CurrentStreak > user.MaxStreak)
        {
            user.MaxStreak = user.CurrentStreak;
        }
    }
}
