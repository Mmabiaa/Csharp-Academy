using System.Security.Claims;
using CsharpAcademy.Application.Admin.Queries;
using CsharpAcademy.Application.Progress.Queries;
using CsharpAcademy.Application.Users.Commands;
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

    /// <summary>
    /// Gets the profile of the currently authenticated user.
    /// </summary>
    /// <returns>The user profile including XP and streaks.</returns>
    /// <response code="200">Profile returned successfully.</response>
    /// <response code="401">If user is not authenticated.</response>
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserProfileDto), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<UserProfileDto>> GetProfile()
    {
        var userId = GetRequiredUserId();
        var profile = await _mediator.Send(new GetUserProfileQuery(userId));
        return Ok(profile);
    }

    /// <summary>
    /// Updates the profile of the currently authenticated user.
    /// </summary>
    /// <param name="command">The update details (first name, last name, etc).</param>
    /// <returns>No content on success.</returns>
    /// <response code="204">Update successful.</response>
    /// <response code="401">If user is not authenticated.</response>
    /// <response code="403">If the command userId doesn't match authenticated user.</response>
    [Authorize]
    [HttpPut("me")]
    [ProducesResponseType(204)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<ActionResult> UpdateProfile([FromBody] UpdateUserProfileCommand command)
    {
        var userId = GetRequiredUserId();
        if (command.UserId != userId) return Forbid();

        await _mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Gets a summary of the currently authenticated user's overall progress.
    /// </summary>
    /// <returns>A summary of completed lessons, tests, and XP.</returns>
    /// <response code="200">Summary returned successfully.</response>
    /// <response code="401">If user is not authenticated.</response>
    [Authorize]
    [HttpGet("me/progress")]
    [ProducesResponseType(typeof(UserProgressSummaryDto), 200)]
    [ProducesResponseType(401)]
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

    /// <summary>
    /// Retrieves progress information for the authenticated user in a specific course.
    /// </summary>
    /// <param name="courseId">The ID of the course.</param>
    /// <returns>Progress details including percentage and completed items.</returns>
    /// <response code="200">Progress returned successfully.</response>
    /// <response code="401">If user is not authenticated.</response>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(CourseProgressDto), 200)]
    [ProducesResponseType(401)]
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
