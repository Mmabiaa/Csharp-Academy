using System.Security.Claims;
using CsharpAcademy.Application.Courses.Queries;
using CsharpAcademy.Application.Enrollments.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAcademy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ISender _mediator;

    public CoursesController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets all available courses in the academy.
    /// </summary>
    /// <returns>A list of course summary DTOs.</returns>
    /// <response code="200">Returns the list of courses.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<CourseDto>), 200)]
    public async Task<ActionResult<List<CourseDto>>> GetCourses()
    {
        var courses = await _mediator.Send(new GetCoursesQuery());
        return Ok(courses);
    }

    /// <summary>
    /// Gets detailed information about a specific course, including its modules and lessons.
    /// </summary>
    /// <param name="id">The unique identifier of the course.</param>
    /// <returns>Detailed course information.</returns>
    /// <response code="200">If the course exists.</response>
    /// <response code="404">If the course is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CourseDetailDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<CourseDetailDto>> GetCourse(int id)
    {
        var course = await _mediator.Send(new GetCourseByIdQuery(id));
        if (course is null)
        {
            return NotFound();
        }

        return Ok(course);
    }

    /// <summary>
    /// Enrolls the authenticated user in a specific course.
    /// </summary>
    /// <param name="id">The ID of the course to enroll in.</param>
    /// <returns>Enrollment details.</returns>
    /// <response code="200">Enrollment successful.</response>
    /// <response code="401">If the user is not authenticated.</response>
    /// <response code="404">If the course is not found.</response>
    [Authorize]
    [HttpPost("{id}/enroll")]
    [ProducesResponseType(typeof(EnrollmentDto), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<EnrollmentDto>> Enroll(int id)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdClaim is null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        try
        {
            var enrollment = await _mediator.Send(new EnrollInCourseCommand(userId, id));
            return Ok(enrollment);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
