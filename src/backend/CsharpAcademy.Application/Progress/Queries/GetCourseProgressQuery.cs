using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Progress.Queries;

public record GetCourseProgressQuery(int UserId, int CourseId) : IRequest<CourseProgressDto>;

public class CourseProgressDto
{
    public int CourseId { get; set; }
    public double CompletionPercentage { get; set; }
    public List<int> CompletedLessonIds { get; set; } = new();
    public bool IsEnrolled { get; set; }
}

public class GetCourseProgressQueryHandler : IRequestHandler<GetCourseProgressQuery, CourseProgressDto>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IProgressRepository _progressRepository;

    public GetCourseProgressQueryHandler(
        IEnrollmentRepository enrollmentRepository,
        IProgressRepository progressRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _progressRepository = progressRepository;
    }

    public async Task<CourseProgressDto> Handle(GetCourseProgressQuery request, CancellationToken cancellationToken)
    {
        var enrollment = await _enrollmentRepository.GetByUserAndCourseAsync(
            request.UserId, request.CourseId, cancellationToken);

        if (enrollment is null)
        {
            return new CourseProgressDto { CourseId = request.CourseId, IsEnrolled = false };
        }

        var completed = await _progressRepository.GetCompletedByUserAndCourseAsync(
            request.UserId, request.CourseId, cancellationToken);

        return new CourseProgressDto
        {
            CourseId = request.CourseId,
            CompletionPercentage = enrollment.CompletionPercentage,
            CompletedLessonIds = completed.Select(p => p.LessonId).ToList(),
            IsEnrolled = true
        };
    }
}
