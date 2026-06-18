using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Courses.Queries;

public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDetailDto?>
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<CourseDetailDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdWithModulesAsync(request.Id, cancellationToken);
        if (course is null)
        {
            return null;
        }

        return new CourseDetailDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Level = course.Level,
            EstimatedHours = course.EstimatedHours,
            Modules = course.Modules.Select(m => new ModuleDto
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                LearningObjectives = m.LearningObjectives,
                Order = m.Order,
                Lessons = m.Lessons.Select(l => new LessonDto
                {
                    Id = l.Id,
                    Title = l.Title,
                    Content = l.Content,
                    Type = l.Type.ToString(),
                    DurationMinutes = l.DurationMinutes,
                    Order = l.Order
                }).ToList()
            }).ToList()
        };
    }
}
