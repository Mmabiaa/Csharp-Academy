using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Admin.Queries;

public record GetAdminDashboardQuery : IRequest<AdminDashboardDto>;

public class AdminDashboardDto
{
    public int TotalUsers { get; set; }
    public int TotalCourses { get; set; }
    public int TotalEnrollments { get; set; }
    public int TotalAssignments { get; set; }
    public int TotalChallenges { get; set; }
    public List<AdminUserDto> RecentUsers { get; set; } = new();
}

public class AdminUserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Xp { get; set; }
}

public class GetAdminDashboardQueryHandler : IRequestHandler<GetAdminDashboardQuery, AdminDashboardDto>
{
    private readonly IUserRepository _users;
    private readonly ICourseRepository _courses;
    private readonly IEnrollmentRepository _enrollments;
    private readonly IAssignmentRepository _assignments;
    private readonly ICodingChallengeRepository _challenges;

    public GetAdminDashboardQueryHandler(
        IUserRepository users, ICourseRepository courses, IEnrollmentRepository enrollments,
        IAssignmentRepository assignments, ICodingChallengeRepository challenges)
    {
        _users = users;
        _courses = courses;
        _enrollments = enrollments;
        _assignments = assignments;
        _challenges = challenges;
    }

    public async Task<AdminDashboardDto> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
    {
        var users = await _users.GetAllAsync(cancellationToken);
        return new AdminDashboardDto
        {
            TotalUsers = users.Count,
            TotalCourses = await _courses.CountAsync(cancellationToken),
            TotalEnrollments = await _enrollments.CountAsync(cancellationToken),
            TotalAssignments = await _assignments.CountAsync(cancellationToken),
            TotalChallenges = (await _challenges.GetAllPublishedAsync(cancellationToken)).Count,
            RecentUsers = users.Take(10).Select(u => new AdminUserDto
            {
                Id = u.Id,
                Email = u.Email ?? "",
                Name = $"{u.FirstName} {u.LastName}",
                Xp = u.Xp
            }).ToList()
        };
    }
}

public record GetTeacherDashboardQuery(int TeacherId) : IRequest<TeacherDashboardDto>;

public class TeacherDashboardDto
{
    public int ClassroomCount { get; set; }
    public int AssignmentCount { get; set; }
    public int PendingGrading { get; set; }
    public List<AssignmentSummaryDto> RecentAssignments { get; set; } = new();
}

public class AssignmentSummaryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int SubmissionCount { get; set; }
    public int UngradedCount { get; set; }
    public DateTime? DueDate { get; set; }
}

public class GetTeacherDashboardQueryHandler : IRequestHandler<GetTeacherDashboardQuery, TeacherDashboardDto>
{
    private readonly IClassroomRepository _classrooms;
    private readonly IAssignmentRepository _assignments;

    public GetTeacherDashboardQueryHandler(IClassroomRepository classrooms, IAssignmentRepository assignments)
    {
        _classrooms = classrooms;
        _assignments = assignments;
    }

    public async Task<TeacherDashboardDto> Handle(GetTeacherDashboardQuery request, CancellationToken cancellationToken)
    {
        var classrooms = await _classrooms.GetByTeacherIdAsync(request.TeacherId, cancellationToken);
        var assignments = await _assignments.GetByTeacherIdAsync(request.TeacherId, cancellationToken);

        return new TeacherDashboardDto
        {
            ClassroomCount = classrooms.Count,
            AssignmentCount = assignments.Count,
            PendingGrading = assignments.Sum(a => a.Submissions.Count(s => s.Status == Domain.Entities.SubmissionStatus.Submitted)),
            RecentAssignments = assignments.Take(5).Select(a => new AssignmentSummaryDto
            {
                Id = a.Id,
                Title = a.Title,
                SubmissionCount = a.Submissions.Count,
                UngradedCount = a.Submissions.Count(s => s.Status == Domain.Entities.SubmissionStatus.Submitted),
                DueDate = a.DueDate
            }).ToList()
        };
    }
}

public record GetUserProgressSummaryQuery(int UserId) : IRequest<UserProgressSummaryDto>;

public class UserProgressSummaryDto
{
    public int TotalXp { get; set; }
    public int CoursesEnrolled { get; set; }
    public int LessonsCompleted { get; set; }
    public int PracticesCompleted { get; set; }
    public int ChallengesCompleted { get; set; }
    public List<int> SolvedChallengeIds { get; set; } = new();
    public List<int> SolvedPracticeIds { get; set; } = new();
    public List<CourseProgressItemDto> Courses { get; set; } = new();
}

public class CourseProgressItemDto
{
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public double CompletionPercentage { get; set; }
    public int TotalLessons { get; set; }
    public int CompletedLessons { get; set; }
}

public class GetUserProgressSummaryQueryHandler : IRequestHandler<GetUserProgressSummaryQuery, UserProgressSummaryDto>
{
    private readonly IUserRepository _users;
    private readonly IEnrollmentRepository _enrollments;
    private readonly IProgressRepository _progress;
    private readonly ICourseRepository _courses;
    private readonly ICodingChallengeRepository _challenges;

    public GetUserProgressSummaryQueryHandler(
        IUserRepository users, IEnrollmentRepository enrollments,
        IProgressRepository progress, ICourseRepository courses,
        ICodingChallengeRepository challenges)
    {
        _users = users;
        _enrollments = enrollments;
        _progress = progress;
        _courses = courses;
        _challenges = challenges;
    }

    public async Task<UserProgressSummaryDto> Handle(GetUserProgressSummaryQuery request, CancellationToken cancellationToken)
    {
        var user = await _users.GetByIdAsync(request.UserId, cancellationToken);
        var enrollments = await _enrollments.GetByUserIdAsync(request.UserId, cancellationToken);
        var completed = await _progress.GetCompletedLessonIdsAsync(request.UserId, cancellationToken);

        var courses = new List<CourseProgressItemDto>();
        foreach (var e in enrollments)
        {
            var course = await _courses.GetByIdWithModulesAsync(e.CourseId, cancellationToken);
            if (course is null) continue;
            var total = course.Modules.Sum(m => m.Lessons.Count);
            var done = course.Modules.SelectMany(m => m.Lessons).Count(l => completed.Contains(l.Id));
            courses.Add(new CourseProgressItemDto
            {
                CourseId = e.CourseId,
                CourseTitle = course.Title,
                CompletionPercentage = e.CompletionPercentage,
                TotalLessons = total,
                CompletedLessons = done
            });
        }

        var solvedChallenges = await _challenges.GetCompletedIdsByUserAsync(request.UserId, cancellationToken);
        var solvedPractices = await _progress.GetCompletedPracticeIdsAsync(request.UserId, cancellationToken);

        return new UserProgressSummaryDto
        {
            TotalXp = user?.Xp ?? 0,
            CoursesEnrolled = enrollments.Count,
            LessonsCompleted = completed.Count,
            PracticesCompleted = solvedPractices.Count,
            ChallengesCompleted = solvedChallenges.Count,
            SolvedChallengeIds = solvedChallenges,
            SolvedPracticeIds = solvedPractices,
            Courses = courses
        };
    }
}
