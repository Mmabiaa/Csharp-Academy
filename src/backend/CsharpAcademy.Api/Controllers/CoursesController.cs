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
    [HttpGet]
    public async Task<ActionResult<List<CourseDto>>> GetCourses()
    {
        var courses = await _mediator.Send(new GetCoursesQuery());
        return Ok(courses);
    }

    /// <summary>
    /// Gets detailed information about a specific course, including its modules and lessons.
    /// </summary>
    [HttpGet("{id}")]
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
    [Authorize]
    [HttpPost("{id}/enroll")]
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
