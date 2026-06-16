using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Quizzes.Queries;

public record GetQuizByLessonQuery(int LessonId) : IRequest<QuizDto?>;

public class QuizDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int LessonId { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
}

public class QuestionDto
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public List<QuestionOptionDto> Options { get; set; } = new();
}

public class QuestionOptionDto
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
}

public class GetQuizByLessonQueryHandler : IRequestHandler<GetQuizByLessonQuery, QuizDto?>
{
    private readonly IQuizRepository _quizRepository;

    public GetQuizByLessonQueryHandler(IQuizRepository quizRepository)
    {
        _quizRepository = quizRepository;
    }

    public async Task<QuizDto?> Handle(GetQuizByLessonQuery request, CancellationToken cancellationToken)
    {
        var quiz = await _quizRepository.GetByLessonIdAsync(request.LessonId, cancellationToken);
        if (quiz is null)
        {
            return null;
        }

        return new QuizDto
        {
            Id = quiz.Id,
            Title = quiz.Title,
            LessonId = quiz.LessonId,
            Questions = quiz.Questions.Select(q => new QuestionDto
            {
                Id = q.Id,
                Text = q.Text,
                Type = q.Type.ToString(),
                Options = q.Options.Select(o => new QuestionOptionDto
                {
                    Id = o.Id,
                    Text = o.Text
                }).ToList()
            }).ToList()
        };
    }
}
