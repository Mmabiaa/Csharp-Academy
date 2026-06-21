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
        /// <returns>A list of coding exercises.</returns>
        /// <response code="200">List of exercises returned successfully.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<CodingExerciseDto>), 200)]
        public async Task<ActionResult<List<CodingExerciseDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllPracticesQuery()));
        }

        /// <summary>
        /// Retrieves coding exercises associated with a specific lesson.
        /// </summary>
        /// <param name="lessonId">The ID of the lesson.</param>
        /// <returns>A list of exercises for the lesson.</returns>
        /// <response code="200">List of exercises returned successfully.</response>
        [HttpGet("lessons/{lessonId}")]
        [ProducesResponseType(typeof(List<CodingExerciseDto>), 200)]
        public async Task<ActionResult<List<CodingExerciseDto>>> GetByLesson(int lessonId)
        {
            return Ok(await _mediator.Send(new GetLessonExercisesQuery(lessonId)));
        }

        /// <summary>
        /// Submits a student's solution for a coding practice exercise.
        /// </summary>
        /// <param name="exerciseId">The ID of the exercise.</param>
        /// <param name="request">The source code solution.</param>
        /// <returns>Result containing XP earned and status.</returns>
        /// <response code="200">Submission processed successfully.</response>
        /// <response code="404">If exercise not found.</response>
        /// <response code="401">If user is not authenticated.</response>
        [Authorize]
        [HttpPost("{exerciseId}/submit")]
        [ProducesResponseType(typeof(PracticeResultDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
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
        /// <param name="lessonId">The ID of the lesson.</param>
        /// <returns>A list of tutorial steps.</returns>
        /// <response code="200">Tutorial steps returned successfully.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<TutorialStepDto>), 200)]
        public async Task<ActionResult<List<TutorialStepDto>>> GetSteps(int lessonId)
        {
            return Ok(await _mediator.Send(new GetTutorialStepsQuery(lessonId)));
        }
    }

public record SubmitPracticeRequest(string Code);
