using System.Security.Claims;
using CsharpAcademy.Application.Admin.Queries;
using CsharpAcademy.Application.Progress.Queries;
using CsharpAcademy.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAcademy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserProfileDto>> GetProfile()
    {
        var userId = GetRequiredUserId();
        var profile = await _mediator.Send(new GetUserProfileQuery(userId));
        return Ok(profile);
    }

    [Authorize]
    [HttpGet("me/progress")]
    public async Task<ActionResult<UserProgressSummaryDto>> GetProgressSummary()
    {
        var userId = GetRequiredUserId();
        return Ok(await _mediator.Send(new GetUserProgressSummaryQuery(userId)));
    }

    private int GetRequiredUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (claim is null || !int.TryParse(claim, out var userId))
        {
            throw new UnauthorizedAccessException();
        }

        return userId;
    }
}

[ApiController]
[Route("api/courses/{courseId}/progress")]
public class ProgressController : ControllerBase
{
    private readonly ISender _mediator;

    public ProgressController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<CourseProgressDto>> GetCourseProgress(int courseId)
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (claim is null || !int.TryParse(claim, out var userId))
        {
            return Unauthorized();
        }

        var progress = await _mediator.Send(new GetCourseProgressQuery(userId, courseId));
        return Ok(progress);
    }
}
