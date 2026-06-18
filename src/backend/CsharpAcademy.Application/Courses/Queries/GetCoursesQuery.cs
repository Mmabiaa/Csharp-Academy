using CsharpAcademy.Domain.Entities;
using MediatR;

namespace CsharpAcademy.Application.Courses.Queries;

public record GetCoursesQuery : IRequest<List<CourseDto>>;

public class CourseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public int EstimatedHours { get; set; }
    public int LessonCount { get; set; }
}
