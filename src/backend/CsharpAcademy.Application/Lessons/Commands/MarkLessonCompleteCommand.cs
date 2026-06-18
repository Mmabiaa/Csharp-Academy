using CsharpAcademy.Application.Common;
using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Lessons.Commands;

public record MarkLessonCompleteCommand(int UserId, int LessonId) : IRequest<LessonCompleteResultDto>;

public class LessonCompleteResultDto
{
    public int LessonId { get; set; }
    public double CourseCompletionPercentage { get; set; }
    public int XpEarned { get; set; }
    public int TotalXp { get; set; }
    public int CurrentStreak { get; set; }
    public List<string> NewBadges { get; set; } = new();
    public bool CourseCompleted { get; set; }
    public string? CertificateCode { get; set; }
}

public class MarkLessonCompleteCommandHandler : IRequestHandler<MarkLessonCompleteCommand, LessonCompleteResultDto>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IProgressRepository _progressRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGamificationService _gamificationService;
    private readonly ICertificateRepository _certificateRepository;

    public MarkLessonCompleteCommandHandler(
        ILessonRepository lessonRepository,
        IProgressRepository progressRepository,
        IEnrollmentRepository enrollmentRepository,
        IUserRepository userRepository,
        IGamificationService gamificationService,
        ICertificateRepository certificateRepository)
    {
        _lessonRepository = lessonRepository;
        _progressRepository = progressRepository;
        _enrollmentRepository = enrollmentRepository;
        _userRepository = userRepository;
        _gamificationService = gamificationService;
        _certificateRepository = certificateRepository;
    }

    public async Task<LessonCompleteResultDto> Handle(MarkLessonCompleteCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.LessonId, cancellationToken)
            ?? throw new KeyNotFoundException("Lesson not found.");

        var courseId = lesson.CourseModule.CourseId;
        var enrollment = await _enrollmentRepository.GetByUserAndCourseAsync(request.UserId, courseId, cancellationToken)
            ?? throw new InvalidOperationException("You must enroll in this course first.");

        var existing = await _progressRepository.GetByUserAndLessonAsync(request.UserId, request.LessonId, cancellationToken);
        var alreadyComplete = existing?.IsCompleted ?? false;

        await _progressRepository.MarkCompleteAsync(request.UserId, request.LessonId, cancellationToken);

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new KeyNotFoundException("User not found.");

        var xpEarned = 0;
        if (!alreadyComplete)
        {
            xpEarned = GamificationRewards.LessonXp;
            await _gamificationService.AwardXpAndUpdateStreakAsync(user, xpEarned, cancellationToken);
        }

        var completed = await _progressRepository.GetCompletedByUserAndCourseAsync(request.UserId, courseId, cancellationToken);
        var totalLessons = await _lessonRepository.CountByCourseIdAsync(courseId, cancellationToken);
        enrollment.CompletionPercentage = totalLessons > 0
            ? Math.Round((double)completed.Count / totalLessons * 100, 1)
            : 0;
        await _enrollmentRepository.UpdateAsync(enrollment, cancellationToken);

        var badgesBefore = (await _userRepository.GetUserBadgesAsync(request.UserId, cancellationToken))
            .Select(b => b.Name).ToHashSet();

        await _gamificationService.CheckLessonBadgesAsync(user, completed.Count, cancellationToken);

        string? certificateCode = null;
        var courseCompleted = enrollment.CompletionPercentage >= 100;

        if (courseCompleted)
        {
            await _gamificationService.CheckCourseCompletionBadgesAsync(user, cancellationToken);

            var existingCert = await _certificateRepository.GetByUserAndCourseAsync(
                request.UserId, courseId, cancellationToken);

            if (existingCert is null)
            {
                var cert = await _certificateRepository.CreateAsync(new Certificate
                {
                    UserId = request.UserId,
                    CourseId = courseId,
                    CertificateCode = Guid.NewGuid().ToString("N")[..12].ToUpperInvariant(),
                    IssuedAt = DateTime.UtcNow
                }, cancellationToken);
                certificateCode = cert.CertificateCode;
            }
            else
            {
                certificateCode = existingCert.CertificateCode;
            }
        }

        var badgesAfter = await _userRepository.GetUserBadgesAsync(request.UserId, cancellationToken);
        var newBadges = badgesAfter
            .Where(b => !badgesBefore.Contains(b.Name))
            .Select(b => b.Name)
            .ToList();

        user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)!;

        return new LessonCompleteResultDto
        {
            LessonId = request.LessonId,
            CourseCompletionPercentage = enrollment.CompletionPercentage,
            XpEarned = xpEarned,
            TotalXp = user!.Xp,
            CurrentStreak = user.CurrentStreak,
            NewBadges = newBadges,
            CourseCompleted = courseCompleted,
            CertificateCode = certificateCode
        };
    }
}
