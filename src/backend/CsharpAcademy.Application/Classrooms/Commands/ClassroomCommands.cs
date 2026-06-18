using CsharpAcademy.Application.Assignments.Commands;
using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Classrooms.Commands;

public record CreateClassroomCommand(int TeacherId, string Name, string Description, int? CourseId) : IRequest<ClassroomDto>;

public record JoinClassroomCommand(int UserId, string JoinCode) : IRequest<ClassroomDto>;

public class ClassroomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string JoinCode { get; set; } = string.Empty;
    public int? CourseId { get; set; }
    public string? CourseTitle { get; set; }
    public string TeacherName { get; set; } = string.Empty;
    public int MemberCount { get; set; }
    public List<ClassroomMemberDto> Members { get; set; } = new();
    public List<AttachmentDto> Attachments { get; set; } = new();
}

public class ClassroomMemberDto
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime JoinedAt { get; set; }
}

public class CreateClassroomCommandHandler : IRequestHandler<CreateClassroomCommand, ClassroomDto>
{
    private readonly IClassroomRepository _classroomRepository;

    public CreateClassroomCommandHandler(IClassroomRepository classroomRepository)
    {
        _classroomRepository = classroomRepository;
    }

    public async Task<ClassroomDto> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
    {
        var joinCode = Guid.NewGuid().ToString("N")[..6].ToUpperInvariant();
        var classroom = await _classroomRepository.CreateAsync(new Classroom
        {
            Name = request.Name,
            Description = request.Description,
            TeacherId = request.TeacherId,
            CourseId = request.CourseId,
            JoinCode = joinCode
        }, cancellationToken);

        var created = await _classroomRepository.GetByIdAsync(classroom.Id, cancellationToken);
        return MapToDto(created!);
    }

    internal static ClassroomDto MapToDto(Classroom c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Description = c.Description,
        JoinCode = c.JoinCode,
        CourseId = c.CourseId,
        CourseTitle = c.Course?.Title,
        TeacherName = $"{c.Teacher.FirstName} {c.Teacher.LastName}",
        MemberCount = c.Members.Count,
        Members = c.Members.Select(m => new ClassroomMemberDto
        {
            UserId = m.UserId,
            Name = $"{m.User.FirstName} {m.User.LastName}",
            Email = m.User.Email ?? "",
            JoinedAt = m.JoinedAt
        }).ToList(),
        Attachments = c.Attachments?.Select(UploadAttachmentCommandHandler.Map).ToList() ?? new()
    };
}

public class JoinClassroomCommandHandler : IRequestHandler<JoinClassroomCommand, ClassroomDto>
{
    private readonly IClassroomRepository _classroomRepository;

    public JoinClassroomCommandHandler(IClassroomRepository classroomRepository)
    {
        _classroomRepository = classroomRepository;
    }

    public async Task<ClassroomDto> Handle(JoinClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroom = await _classroomRepository.GetByJoinCodeAsync(request.JoinCode, cancellationToken)
            ?? throw new KeyNotFoundException("Invalid join code.");

        var existing = await _classroomRepository.GetMemberAsync(classroom.Id, request.UserId, cancellationToken);
        if (existing is null)
        {
            await _classroomRepository.AddMemberAsync(new ClassroomMember
            {
                ClassroomId = classroom.Id,
                UserId = request.UserId
            }, cancellationToken);
        }

        var updated = await _classroomRepository.GetByIdAsync(classroom.Id, cancellationToken);
        return CreateClassroomCommandHandler.MapToDto(updated!);
    }
}
