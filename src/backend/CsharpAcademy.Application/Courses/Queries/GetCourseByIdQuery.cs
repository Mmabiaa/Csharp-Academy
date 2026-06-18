using MediatR;

namespace CsharpAcademy.Application.Courses.Queries;

public record GetCourseByIdQuery(int Id) : IRequest<CourseDetailDto?>;

public class CourseDetailDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public int EstimatedHours { get; set; }
    public List<ModuleDto> Modules { get; set; } = new();
}

public class ModuleDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LearningObjectives { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<LessonDto> Lessons { get; set; } = new();
}

public class LessonDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public int Order { get; set; }
}
