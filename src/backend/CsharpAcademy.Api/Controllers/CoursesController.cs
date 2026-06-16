using CsharpAcademy.Application.Courses.Queries;
using MediatR;
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

    [HttpGet]
    public async Task<ActionResult<List<CourseDto>>> GetCourses()
    {
        var courses = await _mediator.Send(new GetCoursesQuery());
        return Ok(courses);
    }
}
