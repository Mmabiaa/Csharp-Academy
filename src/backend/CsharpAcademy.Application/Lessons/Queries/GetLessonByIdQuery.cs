using MediatR;

namespace CsharpAcademy.Application.Lessons.Queries;

public record GetLessonByIdQuery(int Id, int? UserId = null) : IRequest<LessonDetailDto?>;

public class LessonDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Order { get; set; }
    public int ModuleId { get; set; }
    public string ModuleTitle { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public bool HasQuiz { get; set; }
    public bool IsCompleted { get; set; }
    public string BestPractices { get; set; } = string.Empty;
    public string VoiceSummary { get; set; } = string.Empty;
    public bool HasTutorial { get; set; }
    public bool HasPractice { get; set; }
    public bool HasVideos { get; set; }
}
