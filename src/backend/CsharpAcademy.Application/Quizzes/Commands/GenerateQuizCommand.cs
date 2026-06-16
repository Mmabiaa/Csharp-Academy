using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Quizzes.Commands;

public record GenerateQuizCommand(int LessonId, int QuestionCount = 5) : IRequest<QuizGeneratedDto>;

public class QuizGeneratedDto
{
    public int QuizId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int QuestionCount { get; set; }
    public bool UsedAi { get; set; }
}

public class GenerateQuizCommandHandler : IRequestHandler<GenerateQuizCommand, QuizGeneratedDto>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IAiQuizGenerationService _aiQuizGenerationService;

    public GenerateQuizCommandHandler(
        ILessonRepository lessonRepository,
        IQuizRepository quizRepository,
        IAiQuizGenerationService aiQuizGenerationService)
    {
        _lessonRepository = lessonRepository;
        _quizRepository = quizRepository;
        _aiQuizGenerationService = aiQuizGenerationService;
    }

    public async Task<QuizGeneratedDto> Handle(GenerateQuizCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.LessonId, cancellationToken)
            ?? throw new KeyNotFoundException("Lesson not found.");

        var generated = await _aiQuizGenerationService.GenerateAsync(
            lesson.Title, lesson.Content, request.QuestionCount, cancellationToken);

        var quiz = new Quiz
        {
            LessonId = lesson.Id,
            Title = generated.Title,
            Questions = generated.Questions.Select(q => new Question
            {
                Text = q.Text,
                Type = Enum.TryParse<QuestionType>(q.Type, true, out var t) ? t : QuestionType.MultipleChoice,
                CorrectAnswer = q.CorrectAnswer,
                Options = q.Options.Select(o => new QuestionOption
                {
                    Text = o.Text,
                    IsCorrect = o.IsCorrect
                }).ToList()
            }).ToList()
        };

        var saved = await _quizRepository.CreateQuizWithQuestionsAsync(quiz, cancellationToken);

        return new QuizGeneratedDto
        {
            QuizId = saved.Id,
            Title = saved.Title,
            QuestionCount = generated.Questions.Count,
            UsedAi = true
        };
    }
}
