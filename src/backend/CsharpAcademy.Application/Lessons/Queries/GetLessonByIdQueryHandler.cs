using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Lessons.Queries;

public class GetLessonByIdQueryHandler : IRequestHandler<GetLessonByIdQuery, LessonDetailDto?>
{
    private readonly ILessonRepository _lessonRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IProgressRepository _progressRepository;
    private readonly ICodingExerciseRepository _exerciseRepository;
    private readonly ITutorialStepRepository _tutorialStepRepository;
    private readonly ILessonVideoRepository _videoRepository;

    public GetLessonByIdQueryHandler(
        ILessonRepository lessonRepository,
        IQuizRepository quizRepository,
        IProgressRepository progressRepository,
        ICodingExerciseRepository exerciseRepository,
        ITutorialStepRepository tutorialStepRepository,
        ILessonVideoRepository videoRepository)
    {
        _lessonRepository = lessonRepository;
        _quizRepository = quizRepository;
        _progressRepository = progressRepository;
        _exerciseRepository = exerciseRepository;
        _tutorialStepRepository = tutorialStepRepository;
        _videoRepository = videoRepository;
    }

    public async Task<LessonDetailDto?> Handle(GetLessonByIdQuery request, CancellationToken cancellationToken)
    {
        var lesson = await _lessonRepository.GetByIdAsync(request.Id, cancellationToken);
        if (lesson is null)
        {
            return null;
        }

        var quiz = await _quizRepository.GetByLessonIdAsync(lesson.Id, cancellationToken);
        var progress = request.UserId.HasValue
            ? await _progressRepository.GetByUserAndLessonAsync(request.UserId.Value, lesson.Id, cancellationToken)
            : null;

        var exercises = await _exerciseRepository.GetByLessonIdAsync(lesson.Id, cancellationToken);
        var tutorialSteps = await _tutorialStepRepository.GetByLessonIdAsync(lesson.Id, cancellationToken);
        var videos = await _videoRepository.GetByLessonIdAsync(lesson.Id, cancellationToken);

        return new LessonDetailDto
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Content = lesson.Content,
            Order = lesson.Order,
            ModuleId = lesson.CourseModuleId,
            ModuleTitle = lesson.CourseModule.Title,
            CourseId = lesson.CourseModule.CourseId,
            CourseTitle = lesson.CourseModule.Course?.Title ?? string.Empty,
            HasQuiz = quiz is not null,
            IsCompleted = progress?.IsCompleted ?? false,
            BestPractices = lesson.BestPractices,
            VoiceSummary = lesson.VoiceSummary,
            HasTutorial = tutorialSteps.Count > 0,
            HasPractice = exercises.Count > 0,
            HasVideos = videos.Count > 0
        };
    }
}
