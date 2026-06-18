using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Practices.Queries;

public record GetLessonExercisesQuery(int LessonId) : IRequest<List<CodingExerciseDto>>;

public record GetAllPracticesQuery : IRequest<List<CodingExerciseDto>>;

public record GetTutorialStepsQuery(int LessonId) : IRequest<List<TutorialStepDto>>;

public class CodingExerciseDto
{
    public int Id { get; set; }
    public int LessonId { get; set; }
    public string LessonTitle { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public string StarterCode { get; set; } = string.Empty;
    public string Hint { get; set; } = string.Empty;
    public int Difficulty { get; set; }
}

public class TutorialStepDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? CodeSample { get; set; }
    public int Order { get; set; }
}

public class GetLessonExercisesQueryHandler : IRequestHandler<GetLessonExercisesQuery, List<CodingExerciseDto>>
{
    private readonly ICodingExerciseRepository _repository;

    public GetLessonExercisesQueryHandler(ICodingExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CodingExerciseDto>> Handle(GetLessonExercisesQuery request, CancellationToken cancellationToken)
    {
        var exercises = await _repository.GetByLessonIdAsync(request.LessonId, cancellationToken);
        return exercises.Select(Map).ToList();
    }

    private static CodingExerciseDto Map(Domain.Entities.CodingExercise e) => new()
    {
        Id = e.Id,
        LessonId = e.LessonId,
        Title = e.Title,
        Instructions = e.Instructions,
        StarterCode = e.StarterCode,
        Hint = e.Hint,
        Difficulty = e.Difficulty
    };
}

public class GetAllPracticesQueryHandler : IRequestHandler<GetAllPracticesQuery, List<CodingExerciseDto>>
{
    private readonly ICodingExerciseRepository _repository;

    public GetAllPracticesQueryHandler(ICodingExerciseRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CodingExerciseDto>> Handle(GetAllPracticesQuery request, CancellationToken cancellationToken)
    {
        var exercises = await _repository.GetAllWithLessonAsync(cancellationToken);
        return exercises.Select(e => new CodingExerciseDto
        {
            Id = e.Id,
            LessonId = e.LessonId,
            LessonTitle = e.Lesson.Title,
            CourseTitle = e.Lesson.CourseModule.Course?.Title ?? "",
            Title = e.Title,
            Instructions = e.Instructions,
            StarterCode = e.StarterCode,
            Hint = e.Hint,
            Difficulty = e.Difficulty
        }).ToList();
    }
}

public class GetTutorialStepsQueryHandler : IRequestHandler<GetTutorialStepsQuery, List<TutorialStepDto>>
{
    private readonly ITutorialStepRepository _repository;

    public GetTutorialStepsQueryHandler(ITutorialStepRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TutorialStepDto>> Handle(GetTutorialStepsQuery request, CancellationToken cancellationToken)
    {
        var steps = await _repository.GetByLessonIdAsync(request.LessonId, cancellationToken);
        return steps.Select(s => new TutorialStepDto
        {
            Id = s.Id,
            Title = s.Title,
            Content = s.Content,
            CodeSample = s.CodeSample,
            Order = s.Order
        }).ToList();
    }
}
