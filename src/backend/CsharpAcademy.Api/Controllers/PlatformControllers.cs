using System.Security.Claims;
using CsharpAcademy.Application.Admin.Queries;
using CsharpAcademy.Application.Assignments.Commands;
using CsharpAcademy.Application.Assignments.Queries;
using CsharpAcademy.Application.Challenges.Commands;
using CsharpAcademy.Application.Challenges.Queries;
using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAcademy.Api.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly ICourseRepository _courses;

    public AdminController(ISender mediator, ICourseRepository courses)
    {
        _mediator = mediator;
        _courses = courses;
    }

    [HttpGet("dashboard")]
    public async Task<ActionResult<AdminDashboardDto>> Dashboard() =>
        Ok(await _mediator.Send(new GetAdminDashboardQuery()));

    [HttpGet("users")]
    public async Task<ActionResult<List<AdminUserDto>>> Users()
    {
        var dash = await _mediator.Send(new GetAdminDashboardQuery());
        return Ok(dash.RecentUsers);
    }

    [HttpPost("courses")]
    public async Task<ActionResult<Course>> CreateCourse([FromBody] CreateCourseRequest request)
    {
        var course = await _courses.CreateAsync(new Course
        {
            Title = request.Title,
            Description = request.Description,
            Level = request.Level ?? "Beginner",
            EstimatedHours = request.EstimatedHours,
            IsPublished = true
        });
        return Ok(course);
    }
}

[ApiController]
[Route("api/teacher")]
[Authorize(Roles = "Teacher,Admin")]
public class TeacherController : ControllerBase
{
    private readonly ISender _mediator;

    public TeacherController(ISender mediator) => _mediator = mediator;

    [HttpGet("dashboard")]
    public async Task<ActionResult<TeacherDashboardDto>> Dashboard()
    {
        var id = GetUserId();
        return Ok(await _mediator.Send(new GetTeacherDashboardQuery(id)));
    }

    private int GetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return claim is not null && int.TryParse(claim, out var id) ? id : throw new UnauthorizedAccessException();
    }
}

[ApiController]
[Route("api/assignments")]
public class AssignmentsController : ControllerBase
{
    private readonly ISender _mediator;

    public AssignmentsController(ISender mediator) => _mediator = mediator;

    [Authorize(Roles = "Teacher,Admin")]
    [HttpPost]
    public async Task<ActionResult<AssignmentDto>> Create([FromBody] CreateAssignmentRequest request)
    {
        var result = await _mediator.Send(new CreateAssignmentCommand(
            GetUserId(), request.Title, request.Description, request.Instructions,
            request.CourseId, request.LessonId, request.ClassroomId,
            request.DueDate, request.MaxPoints, request.RequiresCode));
        return Ok(result);
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpGet("teaching")]
    public async Task<ActionResult<List<AssignmentDto>>> Teaching() =>
        Ok(await _mediator.Send(new GetTeacherAssignmentsQuery(GetUserId())));

    [Authorize]
    [HttpGet("my")]
    public async Task<ActionResult<List<AssignmentDto>>> MyAssignments() =>
        Ok(await _mediator.Send(new GetStudentAssignmentsQuery(GetUserId())));

    [Authorize(Roles = "Teacher,Admin")]
    [HttpGet("{id}/submissions")]
    public async Task<ActionResult<List<SubmissionDto>>> Submissions(int id) =>
        Ok(await _mediator.Send(new GetAssignmentSubmissionsQuery(id, GetUserId())));

    [Authorize]
    [HttpPost("{id}/submit")]
    public async Task<ActionResult<SubmissionDto>> Submit(int id, [FromBody] SubmitAssignmentBody body) =>
        Ok(await _mediator.Send(new SubmitAssignmentCommand(GetUserId(), id, body.Content)));

    [Authorize(Roles = "Teacher,Admin")]
    [HttpPost("submissions/{id}/grade")]
    public async Task<ActionResult<SubmissionDto>> Grade(int id, [FromBody] GradeBody body) =>
        Ok(await _mediator.Send(new GradeSubmissionCommand(GetUserId(), id, body.Grade, body.Feedback)));

    private int GetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return claim is not null && int.TryParse(claim, out var uid) ? uid : throw new UnauthorizedAccessException();
    }
}

[ApiController]
[Route("api/challenges")]
public class ChallengesController : ControllerBase
{
    private readonly ISender _mediator;

    public ChallengesController(ISender mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<List<ChallengeDto>>> GetAll() =>
        Ok(await _mediator.Send(new GetChallengesQuery()));

    [Authorize]
    [HttpPost("{id}/submit")]
    public async Task<ActionResult<ChallengeResultDto>> Submit(int id, [FromBody] SubmitChallengeBody body)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (claim is null || !int.TryParse(claim, out var userId))
            throw new UnauthorizedAccessException();
        return Ok(await _mediator.Send(new SubmitChallengeCommand(userId, id, body.Code)));
    }
}

public record CreateCourseRequest(string Title, string Description, string? Level, int EstimatedHours);
public record CreateAssignmentRequest(string Title, string Description, string Instructions, int? CourseId, int? LessonId, int? ClassroomId, DateTime? DueDate, int MaxPoints, bool RequiresCode);
public record SubmitAssignmentBody(string Content);
public record GradeBody(int Grade, string Feedback);
public record SubmitChallengeBody(string Code);
