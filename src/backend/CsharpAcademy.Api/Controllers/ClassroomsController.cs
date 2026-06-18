using System.Security.Claims;
using CsharpAcademy.Application.Analytics.Queries;
using CsharpAcademy.Application.Classrooms.Commands;
using CsharpAcademy.Application.Classrooms.Queries;
using CsharpAcademy.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAcademy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassroomsController : ControllerBase
{
    private readonly ISender _mediator;

    public ClassroomsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpPost]
    public async Task<ActionResult<ClassroomDto>> Create([FromBody] CreateClassroomRequest request)
    {
        var teacherId = GetRequiredUserId();
        var result = await _mediator.Send(new CreateClassroomCommand(
            teacherId, request.Name, request.Description, request.CourseId));
        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ClassroomDto>>> GetMyClassrooms()
    {
        var userId = GetRequiredUserId();
        var isTeacher = User.IsInRole("Teacher") || User.IsInRole("Admin");
        var result = await _mediator.Send(new GetMyClassroomsQuery(userId, isTeacher));
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ClassroomDto>> GetById(int id)
    {
        var result = await _mediator.Send(new GetClassroomByIdQuery(id));
        if (result is null) return NotFound();
        return Ok(result);
    }

    [Authorize]
    [HttpPost("join")]
    public async Task<ActionResult<ClassroomDto>> Join([FromBody] JoinClassroomRequest request)
    {
        try
        {
            var userId = GetRequiredUserId();
            var result = await _mediator.Send(new JoinClassroomCommand(userId, request.JoinCode));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    private int GetRequiredUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (claim is null || !int.TryParse(claim, out var userId))
            throw new UnauthorizedAccessException();
        return userId;
    }
}

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly ISender _mediator;

    public AnalyticsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpGet("dashboard")]
    public async Task<ActionResult<AnalyticsSnapshot>> GetDashboard()
    {
        var result = await _mediator.Send(new GetAnalyticsDashboardQuery());
        return Ok(result);
    }
}

public record CreateClassroomRequest(string Name, string Description, int? CourseId);
public record JoinClassroomRequest(string JoinCode);
