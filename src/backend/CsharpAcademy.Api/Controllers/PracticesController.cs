using System.Security.Claims;
using CsharpAcademy.Application.Practices.Commands;
using CsharpAcademy.Application.Practices.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAcademy.Api.Controllers;

    /// <summary>
    /// Manages coding practice exercises and student submissions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PracticesController : ControllerBase
    {
        private readonly ISender _mediator;

        public PracticesController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all coding practice exercises available on the platform.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<CodingExerciseDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllPracticesQuery()));
        }

        /// <summary>
        /// Retrieves coding exercises associated with a specific lesson.
        /// </summary>
        [HttpGet("lessons/{lessonId}")]
        public async Task<ActionResult<List<CodingExerciseDto>>> GetByLesson(int lessonId)
        {
            return Ok(await _mediator.Send(new GetLessonExercisesQuery(lessonId)));
        }

        /// <summary>
        /// Submits a student's solution for a coding practice exercise.
        /// </summary>
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

    /// <summary>
    /// Manages step-by-step tutorials for specific lessons.
    /// </summary>
    [ApiController]
    [Route("api/lessons/{lessonId}/tutorial")]
    public class TutorialController : ControllerBase
    {
        private readonly ISender _mediator;

        public TutorialController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the tutorial steps for a specific lesson.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<TutorialStepDto>>> GetSteps(int lessonId)
        {
            return Ok(await _mediator.Send(new GetTutorialStepsQuery(lessonId)));
        }
    }

public record SubmitPracticeRequest(string Code);
