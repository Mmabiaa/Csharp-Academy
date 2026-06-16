using System.Security.Claims;
using CsharpAcademy.Application.Lessons.Commands;
using CsharpAcademy.Application.Lessons.Queries;
using CsharpAcademy.Application.Progress.Queries;
using CsharpAcademy.Application.Quizzes.Commands;
using CsharpAcademy.Application.Quizzes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAcademy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ISender _mediator;

    public LessonsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LessonDetailDto>> GetLesson(int id)
    {
        int? userId = GetUserId();
        var lesson = await _mediator.Send(new GetLessonByIdQuery(id, userId));
        if (lesson is null)
        {
            return NotFound();
        }

        return Ok(lesson);
    }

    [Authorize]
    [HttpPost("{id}/complete")]
    public async Task<ActionResult<LessonCompleteResultDto>> CompleteLesson(int id)
    {
        var userId = GetRequiredUserId();
        try
        {
            var result = await _mediator.Send(new MarkLessonCompleteCommand(userId, id));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{lessonId}/quiz")]
    public async Task<ActionResult<QuizDto>> GetQuiz(int lessonId)
    {
        var quiz = await _mediator.Send(new GetQuizByLessonQuery(lessonId));
        if (quiz is null)
        {
            return NotFound();
        }

        return Ok(quiz);
    }

    [Authorize]
    [HttpPost("{lessonId}/quiz/submit")]
    public async Task<ActionResult<QuizResultDto>> SubmitQuiz(int lessonId, [FromBody] SubmitQuizRequest request)
    {
        var userId = GetRequiredUserId();
        try
        {
            var result = await _mediator.Send(new SubmitQuizCommand(
                userId, lessonId, request.OptionAnswers ?? new(), request.TextAnswers ?? new()));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [Authorize(Roles = "Teacher,Admin")]
    [HttpPost("{lessonId}/quiz/generate")]
    public async Task<ActionResult<QuizGeneratedDto>> GenerateQuiz(int lessonId, [FromQuery] int count = 5)
    {
        try
        {
            var result = await _mediator.Send(new GenerateQuizCommand(lessonId, count));
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    private int? GetUserId()
    {
        var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return claim is not null && int.TryParse(claim, out var id) ? id : null;
    }

    private int GetRequiredUserId()
    {
        var userId = GetUserId();
        if (userId is null)
        {
            throw new UnauthorizedAccessException();
        }

        return userId.Value;
    }
}

public record SubmitQuizRequest(
    Dictionary<int, int>? OptionAnswers,
    Dictionary<int, string>? TextAnswers);
