using CsharpAcademy.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Assignments.Commands;

public record CreateAssignmentCommand(int TeacherId, string Title, string Description, string Instructions, int? CourseId, int? LessonId, int? ClassroomId, DateTime? DueDate, int MaxPoints, bool RequiresCode) : IRequest<AssignmentDto>;

public record SubmitAssignmentCommand(int UserId, int AssignmentId, string Content) : IRequest<SubmissionDto>;

public record GradeSubmissionCommand(int TeacherId, int SubmissionId, int Grade, string Feedback) : IRequest<SubmissionDto>;

public record UploadAttachmentCommand(int UserId, int? ClassroomId, int? AssignmentId, int? SubmissionId, IFormFile File) : IRequest<AttachmentDto>;

public record DeleteAttachmentCommand(int UserId, int AttachmentId) : IRequest<Unit>;

public class AttachmentDto
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public int UploadedById { get; set; }
    public DateTime CreatedAt { get; set; }
}

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
    public List<AttachmentDto> Attachments { get; set; } = new();
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
    public List<AttachmentDto> Attachments { get; set; } = new();
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
        MySubmission = mine,
        Attachments = a.Attachments?.Select(UploadAttachmentCommandHandler.Map).ToList() ?? new()
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
        Feedback = s.Feedback,
        Attachments = s.Attachments?.Select(UploadAttachmentCommandHandler.Map).ToList() ?? new()
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

public class UploadAttachmentCommandHandler : IRequestHandler<UploadAttachmentCommand, AttachmentDto>
{
    private readonly IAttachmentRepository _attachmentRepo;
    private readonly IFileService _fileService;

    public UploadAttachmentCommandHandler(IAttachmentRepository attachmentRepo, IFileService fileService)
    {
        _attachmentRepo = attachmentRepo;
        _fileService = fileService;
    }

    public async Task<AttachmentDto> Handle(UploadAttachmentCommand request, CancellationToken cancellationToken)
    {
        var folder = request.ClassroomId.HasValue ? "classrooms" :
                     request.AssignmentId.HasValue ? "assignments" : "submissions";

        var url = await _fileService.SaveFileAsync(request.File, folder);

        var attachment = new Attachment
        {
            FileName = request.File.FileName,
            FileUrl = url,
            FileType = Path.GetExtension(request.File.FileName).TrimStart('.'),
            FileSize = request.File.Length,
            ClassroomId = request.ClassroomId,
            AssignmentId = request.AssignmentId,
            SubmissionId = request.SubmissionId,
            UploadedById = request.UserId
        };

        await _attachmentRepo.AddAsync(attachment, cancellationToken);

        return Map(attachment);
    }

    internal static AttachmentDto Map(Attachment a) => new()
    {
        Id = a.Id,
        FileName = a.FileName,
        FileUrl = a.FileUrl,
        FileType = a.FileType,
        FileSize = a.FileSize,
        UploadedById = a.UploadedById,
        CreatedAt = a.CreatedAt
    };
}

public class DeleteAttachmentCommandHandler : IRequestHandler<DeleteAttachmentCommand, Unit>
{
    private readonly IAttachmentRepository _attachmentRepo;
    private readonly IFileService _fileService;

    public DeleteAttachmentCommandHandler(IAttachmentRepository attachmentRepo, IFileService fileService)
    {
        _attachmentRepo = attachmentRepo;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeleteAttachmentCommand request, CancellationToken cancellationToken)
    {
        var attachment = await _attachmentRepo.GetByIdAsync(request.AttachmentId, cancellationToken)
            ?? throw new KeyNotFoundException("Attachment not found.");

        if (attachment.UploadedById != request.UserId)
            throw new UnauthorizedAccessException("Not authorized to delete this attachment.");

        _fileService.DeleteFile(attachment.FileUrl);
        await _attachmentRepo.DeleteAsync(request.AttachmentId, cancellationToken);

        return Unit.Value;
    }
}
