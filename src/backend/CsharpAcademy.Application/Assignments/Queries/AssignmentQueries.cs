using CsharpAcademy.Application.Assignments.Commands;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Assignments.Queries;

public record GetTeacherAssignmentsQuery(int TeacherId) : IRequest<List<AssignmentDto>>;
public record GetStudentAssignmentsQuery(int UserId) : IRequest<List<AssignmentDto>>;
public record GetAssignmentSubmissionsQuery(int AssignmentId, int TeacherId) : IRequest<List<SubmissionDto>>;

public class GetTeacherAssignmentsQueryHandler : IRequestHandler<GetTeacherAssignmentsQuery, List<AssignmentDto>>
{
    private readonly IAssignmentRepository _repo;
    public GetTeacherAssignmentsQueryHandler(IAssignmentRepository repo) => _repo = repo;

    public async Task<List<AssignmentDto>> Handle(GetTeacherAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var list = await _repo.GetByTeacherIdAsync(request.TeacherId, cancellationToken);
        return list.Select(a => CreateAssignmentCommandHandler.Map(a, null)).ToList();
    }
}

public class GetStudentAssignmentsQueryHandler : IRequestHandler<GetStudentAssignmentsQuery, List<AssignmentDto>>
{
    private readonly IAssignmentRepository _repo;
    public GetStudentAssignmentsQueryHandler(IAssignmentRepository repo) => _repo = repo;

    public async Task<List<AssignmentDto>> Handle(GetStudentAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var list = await _repo.GetForStudentAsync(request.UserId, cancellationToken);
        return list.Select(a =>
        {
            var mine = a.Submissions.FirstOrDefault();
            var dto = mine is null ? null : SubmitAssignmentCommandHandler.MapSubmission(mine, "");
            return CreateAssignmentCommandHandler.Map(a, dto);
        }).ToList();
    }
}

public class GetAssignmentSubmissionsQueryHandler : IRequestHandler<GetAssignmentSubmissionsQuery, List<SubmissionDto>>
{
    private readonly IAssignmentRepository _repo;
    public GetAssignmentSubmissionsQueryHandler(IAssignmentRepository repo) => _repo = repo;

    public async Task<List<SubmissionDto>> Handle(GetAssignmentSubmissionsQuery request, CancellationToken cancellationToken)
    {
        var assignment = await _repo.GetByIdAsync(request.AssignmentId, cancellationToken)
            ?? throw new KeyNotFoundException("Assignment not found.");
        if (assignment.CreatedById != request.TeacherId)
            throw new UnauthorizedAccessException();

        var subs = await _repo.GetSubmissionsAsync(request.AssignmentId, cancellationToken);
        return subs.Select(s => SubmitAssignmentCommandHandler.MapSubmission(s, $"{s.User.FirstName} {s.User.LastName}")).ToList();
    }
}
