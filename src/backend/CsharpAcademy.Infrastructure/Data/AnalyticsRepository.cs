using CsharpAcademy.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CsharpAcademy.Infrastructure.Data;

public class AnalyticsRepository : IAnalyticsRepository
{
    private readonly ApplicationDbContext _context;

    public AnalyticsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AnalyticsSnapshot> GetSnapshotAsync(CancellationToken cancellationToken = default)
    {
        var totalUsers = await _context.Users.CountAsync(cancellationToken);
        var totalEnrollments = await _context.Enrollments.CountAsync(cancellationToken);
        var totalQuizAttempts = await _context.QuizAttempts.CountAsync(cancellationToken);
        var passedAttempts = await _context.QuizAttempts.CountAsync(a => a.Passed, cancellationToken);
        var totalClassrooms = await _context.Classrooms.CountAsync(cancellationToken);
        var certificatesIssued = await _context.Certificates.CountAsync(cancellationToken);

        var avgCompletion = await _context.Enrollments.AnyAsync(cancellationToken)
            ? await _context.Enrollments.AverageAsync(e => e.CompletionPercentage, cancellationToken)
            : 0;

        var courseStats = await _context.Courses
            .Select(c => new CourseAnalytics
            {
                CourseId = c.Id,
                CourseTitle = c.Title,
                EnrollmentCount = _context.Enrollments.Count(e => e.CourseId == c.Id),
                AverageCompletion = _context.Enrollments
                    .Where(e => e.CourseId == c.Id)
                    .Select(e => (double?)e.CompletionPercentage)
                    .Average() ?? 0
            })
            .ToListAsync(cancellationToken);

        return new AnalyticsSnapshot
        {
            TotalUsers = totalUsers,
            TotalEnrollments = totalEnrollments,
            TotalQuizAttempts = totalQuizAttempts,
            AverageCourseCompletion = Math.Round(avgCompletion, 1),
            QuizPassRate = totalQuizAttempts > 0
                ? Math.Round((double)passedAttempts / totalQuizAttempts * 100, 1)
                : 0,
            TotalClassrooms = totalClassrooms,
            CertificatesIssued = certificatesIssued,
            CourseStats = courseStats
        };
    }

    public async Task<AnalyticsSnapshot> GetTeachingSnapshotAsync(int teacherId, CancellationToken cancellationToken = default)
    {
        var myClassrooms = _context.Classrooms
            .Where(c => c.TeacherId == teacherId)
            .Select(c => c.Id);

        var myStudentIds = await _context.Classrooms
            .Where(c => c.TeacherId == teacherId)
            .SelectMany(c => c.Members.Select(m => m.UserId))
            .Distinct()
            .ToListAsync(cancellationToken);

        var totalUsers = myStudentIds.Count;
        var totalEnrollments = await _context.Enrollments
            .CountAsync(e => myStudentIds.Contains(e.UserId), cancellationToken);
        
        var totalQuizAttempts = await _context.QuizAttempts
            .CountAsync(a => myStudentIds.Contains(a.UserId), cancellationToken);
        
        var passedAttempts = await _context.QuizAttempts
            .CountAsync(a => myStudentIds.Contains(a.UserId) && a.Passed, cancellationToken);
        
        var totalClassrooms = await myClassrooms.CountAsync(cancellationToken);
        
        var certificatesIssued = await _context.Certificates
            .CountAsync(c => myStudentIds.Contains(c.UserId), cancellationToken);

        var avgCompletion = await _context.Enrollments
            .Where(e => myStudentIds.Contains(e.UserId))
            .AnyAsync(cancellationToken)
            ? await _context.Enrollments
                .Where(e => myStudentIds.Contains(e.UserId))
                .AverageAsync(e => e.CompletionPercentage, cancellationToken)
            : 0;

        var myCourses = await _context.Classrooms
            .Where(c => c.TeacherId == teacherId && c.CourseId != null)
            .Select(c => c.CourseId)
            .Distinct()
            .ToListAsync(cancellationToken);

        var courseStats = await _context.Courses
            .Where(c => myCourses.Contains(c.Id))
            .Select(c => new CourseAnalytics
            {
                CourseId = c.Id,
                CourseTitle = c.Title,
                EnrollmentCount = _context.Enrollments.Count(e => e.CourseId == c.Id && myStudentIds.Contains(e.UserId)),
                AverageCompletion = _context.Enrollments
                    .Where(e => e.CourseId == c.Id && myStudentIds.Contains(e.UserId))
                    .Select(e => (double?)e.CompletionPercentage)
                    .Average() ?? 0
            })
            .ToListAsync(cancellationToken);

        return new AnalyticsSnapshot
        {
            TotalUsers = totalUsers,
            TotalEnrollments = totalEnrollments,
            TotalQuizAttempts = totalQuizAttempts,
            AverageCourseCompletion = Math.Round(avgCompletion, 1),
            QuizPassRate = totalQuizAttempts > 0
                ? Math.Round((double)passedAttempts / totalQuizAttempts * 100, 1)
                : 0,
            TotalClassrooms = totalClassrooms,
            CertificatesIssued = certificatesIssued,
            CourseStats = courseStats
        };
    }
}
