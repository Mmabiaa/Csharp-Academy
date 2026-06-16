using System.Security.Claims;
using CsharpAcademy.Application.Practices.Commands;
using CsharpAcademy.Application.Practices.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAcademy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PracticesController : ControllerBase
{
    private readonly ISender _mediator;

    public PracticesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CodingExerciseDto>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllPracticesQuery()));
    }

    [HttpGet("lessons/{lessonId}")]
    public async Task<ActionResult<List<CodingExerciseDto>>> GetByLesson(int lessonId)
    {
        return Ok(await _mediator.Send(new GetLessonExercisesQuery(lessonId)));
    }

    [Authorize]
    [HttpPost("{exerciseId}/submit")]
    public async Task<ActionResult<PracticeResultDto>> Submit(int exerciseId, [FromBody] SubmitPracticeRequest request)
    {
        var userId = GetRequiredUserId();
        try
        {
            var result = await _mediator.Send(new SubmitPracticeCommand(userId, exerciseId, request.Code));
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
[Route("api/lessons/{lessonId}/tutorial")]
public class TutorialController : ControllerBase
{
    private readonly ISender _mediator;

    public TutorialController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<TutorialStepDto>>> GetSteps(int lessonId)
    {
        return Ok(await _mediator.Send(new GetTutorialStepsQuery(lessonId)));
    }
}

public record SubmitPracticeRequest(string Code);
