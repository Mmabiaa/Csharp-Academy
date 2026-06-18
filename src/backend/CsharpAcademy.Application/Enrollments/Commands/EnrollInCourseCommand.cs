using MediatR;

namespace CsharpAcademy.Application.Enrollments.Commands;

public record EnrollInCourseCommand(int UserId, int CourseId) : IRequest<EnrollmentDto>;

public class EnrollmentDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public DateTime EnrolledAt { get; set; }
    public double CompletionPercentage { get; set; }
}
