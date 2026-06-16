namespace CsharpAcademy.Domain.Entities;

public class Enrollment : Entity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    public double CompletionPercentage { get; set; } = 0.0;
}
