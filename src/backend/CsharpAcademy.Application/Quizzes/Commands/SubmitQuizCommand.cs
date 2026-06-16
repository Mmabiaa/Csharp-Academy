using CsharpAcademy.Application.Common;
using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Quizzes.Commands;

public record SubmitQuizCommand(int UserId, int LessonId, Dictionary<int, int> Answers) : IRequest<QuizResultDto>;

public class QuizResultDto
{
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public bool Passed { get; set; }
    public int XpEarned { get; set; }
    public int TotalXp { get; set; }
    public List<string> NewBadges { get; set; } = new();
    public List<QuestionResultDto> QuestionResults { get; set; } = new();
}

public class QuestionResultDto
{
    public int QuestionId { get; set; }
    public bool IsCorrect { get; set; }
    public int CorrectOptionId { get; set; }
}

public class SubmitQuizCommandHandler : IRequestHandler<SubmitQuizCommand, QuizResultDto>
{
    private const double PassThreshold = 0.7;

    private readonly IQuizRepository _quizRepository;
    private readonly IUserRepository _userRepository;
    private readonly IGamificationService _gamificationService;

    public SubmitQuizCommandHandler(
        IQuizRepository quizRepository,
        IUserRepository userRepository,
        IGamificationService gamificationService)
    {
        _quizRepository = quizRepository;
        _userRepository = userRepository;
        _gamificationService = gamificationService;
    }

    public async Task<QuizResultDto> Handle(SubmitQuizCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetByLessonIdAsync(request.LessonId, cancellationToken)
            ?? throw new KeyNotFoundException("Quiz not found for this lesson.");

        var questionResults = new List<QuestionResultDto>();
        var score = 0;

        foreach (var question in quiz.Questions)
        {
            var correctOption = question.Options.FirstOrDefault(o => o.IsCorrect);
            var selectedId = request.Answers.GetValueOrDefault(question.Id);
            var isCorrect = correctOption is not null && selectedId == correctOption.Id;

            if (isCorrect)
            {
                score++;
            }

            questionResults.Add(new QuestionResultDto
            {
                QuestionId = question.Id,
                IsCorrect = isCorrect,
                CorrectOptionId = correctOption?.Id ?? 0
            });
        }

        var total = quiz.Questions.Count;
        var passed = total > 0 && (double)score / total >= PassThreshold;

        await _quizRepository.SaveAttemptAsync(new QuizAttempt
        {
            UserId = request.UserId,
            QuizId = quiz.Id,
            Score = score,
            TotalQuestions = total,
            Passed = passed,
            AttemptedAt = DateTime.UtcNow
        }, cancellationToken);

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new KeyNotFoundException("User not found.");

        var xpEarned = 0;
        if (passed)
        {
            xpEarned = GamificationRewards.QuizPassXp;
            await _gamificationService.AwardXpAndUpdateStreakAsync(user, xpEarned, cancellationToken);
        }

        var badgesBefore = (await _userRepository.GetUserBadgesAsync(request.UserId, cancellationToken))
            .Select(b => b.Name).ToHashSet();

        await _gamificationService.CheckQuizBadgesAsync(user, passed, cancellationToken);

        var badgesAfter = await _userRepository.GetUserBadgesAsync(request.UserId, cancellationToken);
        var newBadges = badgesAfter
            .Where(b => !badgesBefore.Contains(b.Name))
            .Select(b => b.Name)
            .ToList();

        user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)!;

        return new QuizResultDto
        {
            Score = score,
            TotalQuestions = total,
            Passed = passed,
            XpEarned = xpEarned,
            TotalXp = user!.Xp,
            NewBadges = newBadges,
            QuestionResults = questionResults
        };
    }
}
