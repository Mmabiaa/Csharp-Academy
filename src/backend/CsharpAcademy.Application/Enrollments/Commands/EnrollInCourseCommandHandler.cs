using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Enrollments.Commands;

public class EnrollInCourseCommandHandler : IRequestHandler<EnrollInCourseCommand, EnrollmentDto>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ICourseRepository _courseRepository;

    public EnrollInCourseCommandHandler(
        IEnrollmentRepository enrollmentRepository,
        ICourseRepository courseRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _courseRepository = courseRepository;
    }

    public async Task<EnrollmentDto> Handle(EnrollInCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdWithModulesAsync(request.CourseId, cancellationToken);
        if (course is null)
        {
            throw new KeyNotFoundException("Course not found.");
        }

        var existing = await _enrollmentRepository.GetByUserAndCourseAsync(
            request.UserId, request.CourseId, cancellationToken);

        if (existing is not null)
        {
            return new EnrollmentDto
            {
                Id = existing.Id,
                CourseId = existing.CourseId,
                CourseTitle = course.Title,
                EnrolledAt = existing.EnrolledAt,
                CompletionPercentage = existing.CompletionPercentage
            };
        }

        var enrollment = new Enrollment
        {
            UserId = request.UserId,
            CourseId = request.CourseId,
            EnrolledAt = DateTime.UtcNow
        };

        var created = await _enrollmentRepository.CreateAsync(enrollment, cancellationToken);

        return new EnrollmentDto
        {
            Id = created.Id,
            CourseId = created.CourseId,
            CourseTitle = course.Title,
            EnrolledAt = created.EnrolledAt,
            CompletionPercentage = created.CompletionPercentage
        };
    }
}
