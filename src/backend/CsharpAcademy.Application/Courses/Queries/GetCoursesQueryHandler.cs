using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Courses.Queries;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>
{
    private readonly ICourseRepository _courseRepository;

    public GetCoursesQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllAsync(cancellationToken);
        return courses.Select(c => new CourseDto
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description,
            Level = c.Level,
            EstimatedHours = c.EstimatedHours,
            LessonCount = c.Modules.Sum(m => m.Lessons.Count)
        }).ToList();
    }
}
