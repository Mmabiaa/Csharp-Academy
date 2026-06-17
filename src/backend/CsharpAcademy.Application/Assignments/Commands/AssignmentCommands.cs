using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Assignments.Commands;

public record CreateAssignmentCommand(int TeacherId, string Title, string Description, string Instructions, int? CourseId, int? LessonId, int? ClassroomId, DateTime? DueDate, int MaxPoints, bool RequiresCode) : IRequest<AssignmentDto>;

public record SubmitAssignmentCommand(int UserId, int AssignmentId, string Content) : IRequest<SubmissionDto>;

public record GradeSubmissionCommand(int TeacherId, int SubmissionId, int Grade, string Feedback) : IRequest<SubmissionDto>;

public class AssignmentDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public int? CourseId { get; set; }
    public string? CourseTitle { get; set; }
    public int? LessonId { get; set; }
    public int? ClassroomId { get; set; }
    public DateTime? DueDate { get; set; }
    public int MaxPoints { get; set; }
    public bool RequiresCode { get; set; }
    public int SubmissionCount { get; set; }
    public SubmissionDto? MySubmission { get; set; }
}

public class SubmissionDto
{
    public int Id { get; set; }
    public int AssignmentId { get; set; }
    public int UserId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public int? Grade { get; set; }
    public string? Feedback { get; set; }
}

public class CreateAssignmentCommandHandler : IRequestHandler<CreateAssignmentCommand, AssignmentDto>
{
    private readonly IAssignmentRepository _repo;

    public CreateAssignmentCommandHandler(IAssignmentRepository repo) => _repo = repo;

    public async Task<AssignmentDto> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _repo.CreateAsync(new Assignment
        {
            Title = request.Title,
            Description = request.Description,
            Instructions = request.Instructions,
            CourseId = request.CourseId,
            LessonId = request.LessonId,
            ClassroomId = request.ClassroomId,
            CreatedById = request.TeacherId,
            DueDate = request.DueDate,
            MaxPoints = request.MaxPoints,
            RequiresCode = request.RequiresCode
        }, cancellationToken);

        return Map(assignment, null);
    }

    internal static AssignmentDto Map(Assignment a, SubmissionDto? mine) => new()
    {
        Id = a.Id,
        Title = a.Title,
        Description = a.Description,
        Instructions = a.Instructions,
        CourseId = a.CourseId,
        CourseTitle = a.Course?.Title,
        LessonId = a.LessonId,
        ClassroomId = a.ClassroomId,
        DueDate = a.DueDate,
        MaxPoints = a.MaxPoints,
        RequiresCode = a.RequiresCode,
        SubmissionCount = a.Submissions?.Count ?? 0,
        MySubmission = mine
    };
}

public class SubmitAssignmentCommandHandler : IRequestHandler<SubmitAssignmentCommand, SubmissionDto>
{
    private readonly IAssignmentRepository _repo;

    public SubmitAssignmentCommandHandler(IAssignmentRepository repo) => _repo = repo;

    public async Task<SubmissionDto> Handle(SubmitAssignmentCommand request, CancellationToken cancellationToken)
    {
        var assignment = await _repo.GetByIdAsync(request.AssignmentId, cancellationToken)
            ?? throw new KeyNotFoundException("Assignment not found.");

        var existing = await _repo.GetUserSubmissionAsync(request.AssignmentId, request.UserId, cancellationToken);
        if (existing is not null)
            throw new InvalidOperationException("You have already submitted this assignment.");

        var submission = await _repo.SubmitAsync(new AssignmentSubmission
        {
            AssignmentId = request.AssignmentId,
            UserId = request.UserId,
            Content = request.Content,
            SubmittedAt = DateTime.UtcNow,
            Status = SubmissionStatus.Submitted
        }, cancellationToken);

        return MapSubmission(submission, "");
    }

    internal static SubmissionDto MapSubmission(AssignmentSubmission s, string name) => new()
    {
        Id = s.Id,
        AssignmentId = s.AssignmentId,
        UserId = s.UserId,
        StudentName = name,
        Content = s.Content,
        SubmittedAt = s.SubmittedAt,
        Status = s.Status.ToString(),
        Grade = s.Grade,
        Feedback = s.Feedback
    };
}

public class GradeSubmissionCommandHandler : IRequestHandler<GradeSubmissionCommand, SubmissionDto>
{
    private readonly IAssignmentRepository _repo;

    public GradeSubmissionCommandHandler(IAssignmentRepository repo) => _repo = repo;

    public async Task<SubmissionDto> Handle(GradeSubmissionCommand request, CancellationToken cancellationToken)
    {
        var submission = await _repo.GetSubmissionAsync(request.SubmissionId, cancellationToken)
            ?? throw new KeyNotFoundException("Submission not found.");

        if (submission.Assignment.CreatedById != request.TeacherId)
            throw new UnauthorizedAccessException("Not authorized to grade this submission.");

        submission.Grade = request.Grade;
        submission.Feedback = request.Feedback;
        submission.GradedById = request.TeacherId;
        submission.GradedAt = DateTime.UtcNow;
        submission.Status = SubmissionStatus.Graded;

        await _repo.GradeSubmissionAsync(submission, cancellationToken);
        return SubmitAssignmentCommandHandler.MapSubmission(submission, $"{submission.User.FirstName} {submission.User.LastName}");
    }
}
