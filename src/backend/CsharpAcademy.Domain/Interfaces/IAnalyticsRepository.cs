namespace CsharpAcademy.Domain.Interfaces;

public interface IAnalyticsRepository
{
    Task<AnalyticsSnapshot> GetSnapshotAsync(CancellationToken cancellationToken = default);
}

public class AnalyticsSnapshot
{
    public int TotalUsers { get; set; }
    public int TotalEnrollments { get; set; }
    public int TotalQuizAttempts { get; set; }
    public double AverageCourseCompletion { get; set; }
    public double QuizPassRate { get; set; }
    public int TotalClassrooms { get; set; }
    public int CertificatesIssued { get; set; }
    public List<CourseAnalytics> CourseStats { get; set; } = new();
}

public class CourseAnalytics
{
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public int EnrollmentCount { get; set; }
    public double AverageCompletion { get; set; }
}
