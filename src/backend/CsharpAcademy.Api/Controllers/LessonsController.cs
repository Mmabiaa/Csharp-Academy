using System.Security.Claims;
using CsharpAcademy.Application.Challenges.Queries;
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

    /// <summary>
    /// Gets the details of a specific lesson, including content and child entities.
    /// </summary>
    /// <param name="id">The ID of the lesson.</param>
    /// <returns>Lesson details.</returns>
    /// <response code="200">Returns the lesson details.</response>
    /// <response code="404">If lesson is not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LessonDetailDto), 200)]
    [ProducesResponseType(404)]
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

    /// <summary>
    /// Gets all videos associated with a specific lesson.
    /// </summary>
    /// <param name="lessonId">The ID of the lesson.</param>
    /// <returns>A list of video DTOs.</returns>
    /// <response code="200">List of videos.</response>
    [HttpGet("{lessonId}/videos")]
    [ProducesResponseType(typeof(List<LessonVideoDto>), 200)]
    public async Task<ActionResult<List<LessonVideoDto>>> GetVideos(int lessonId) =>
        Ok(await _mediator.Send(new GetLessonVideosQuery(lessonId)));

    /// <summary>
    /// Marks a lesson as completed for the authenticated user and awards XP.
    /// </summary>
    /// <param name="id">The ID of the lesson.</param>
    /// <returns>Result containing XP earned and achievement info.</returns>
    /// <response code="200">Lesson marked complete.</response>
    /// <response code="400">If internal constraints fail.</response>
    /// <response code="404">If lesson not found.</response>
    [Authorize]
    [HttpPost("{id}/complete")]
    [ProducesResponseType(typeof(LessonCompleteResultDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
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

    /// <summary>
    /// Gets the quiz associated with a lesson.
    /// </summary>
    /// <param name="lessonId">The ID of the lesson.</param>
    /// <returns>Quiz data with questions.</returns>
    /// <response code="200">Returns the quiz.</response>
    /// <response code="404">If no quiz exists for this lesson.</response>
    [HttpGet("{lessonId}/quiz")]
    [ProducesResponseType(typeof(QuizDto), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<QuizDto>> GetQuiz(int lessonId)
    {
        var quiz = await _mediator.Send(new GetQuizByLessonQuery(lessonId));
        if (quiz is null)
        {
            return NotFound();
        }

        return Ok(quiz);
    }

    /// <summary>
    /// Submits quiz answers for evaluation and records the result.
    /// </summary>
    /// <param name="lessonId">The ID of the lesson.</param>
    /// <param name="request">The submitted answers.</param>
    /// <returns>Result including score and XP awarded.</returns>
    /// <response code="200">Quiz evaluated successfully.</response>
    /// <response code="404">If quiz/lesson not found.</response>
    [Authorize]
    [HttpPost("{lessonId}/quiz/submit")]
    [ProducesResponseType(typeof(QuizResultDto), 200)]
    [ProducesResponseType(404)]
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

    /// <summary>
    /// Generates a new AI quiz for a lesson (Teacher/Admin only).
    /// </summary>
    /// <param name="lessonId">The ID of the lesson.</param>
    /// <param name="count">Number of questions to generate (default 5).</param>
    /// <returns>The generated quiz data.</returns>
    /// <response code="200">Quiz generated successfully.</response>
    /// <response code="404">If lesson not found.</response>
    [Authorize(Roles = "Teacher,Admin")]
    [HttpPost("{lessonId}/quiz/generate")]
    [ProducesResponseType(typeof(QuizGeneratedDto), 200)]
    [ProducesResponseType(404)]
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
