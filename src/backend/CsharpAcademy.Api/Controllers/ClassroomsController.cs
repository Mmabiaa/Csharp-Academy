using System.Security.Claims;
using CsharpAcademy.Application.Analytics.Queries;
using CsharpAcademy.Application.Classrooms.Commands;
using CsharpAcademy.Application.Classrooms.Queries;
using CsharpAcademy.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAcademy.Api.Controllers;

    /// <summary>
    /// Manages virtual classrooms and student memberships.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomsController : ControllerBase
    {
        private readonly ISender _mediator;

        public ClassroomsController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new classroom. (Requires Teacher or Admin role).
        /// </summary>
        /// <param name="request">The classroom name, description and linked course.</param>
        /// <returns>The created classroom DTO including join code.</returns>
        /// <response code="200">Classroom created successfully.</response>
        /// <response code="401">Unauthorized access.</response>
        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(ClassroomDto), 200)]
        public async Task<ActionResult<ClassroomDto>> Create([FromBody] CreateClassroomRequest request)
        {
            var teacherId = GetRequiredUserId();
            var result = await _mediator.Send(new CreateClassroomCommand(
                teacherId, request.Name, request.Description, request.CourseId));
            return Ok(result);
        }

        /// <summary>
        /// Retrieves classrooms associated with the current user.
        /// </summary>
        /// <returns>A list of classrooms where user is teacher or student.</returns>
        /// <response code="200">Return list of classrooms.</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<ClassroomDto>), 200)]
        public async Task<ActionResult<List<ClassroomDto>>> GetMyClassrooms()
        {
            var userId = GetRequiredUserId();
            var isTeacher = User.IsInRole("Teacher") || User.IsInRole("Admin");
            var result = await _mediator.Send(new GetMyClassroomsQuery(userId, isTeacher));
            return Ok(result);
        }

        /// <summary>
        /// Retrieves detailed information about a specific classroom.
        /// </summary>
        /// <param name="id">The classroom ID.</param>
        /// <returns>Room data and enrolled student count.</returns>
        /// <response code="200">If found.</response>
        /// <response code="404">If not found.</response>
        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClassroomDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ClassroomDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetClassroomByIdQuery(id));
            if (result is null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Joins a classroom using a unique join code.
        /// </summary>
        /// <param name="request">The 6-character alphanumeric join code.</param>
        /// <returns>The classroom joined.</returns>
        /// <response code="200">Join successful.</response>
        /// <response code="404">If join code is invalid.</response>
        [Authorize]
        [HttpPost("join")]
        [ProducesResponseType(typeof(ClassroomDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ClassroomDto>> Join([FromBody] JoinClassroomRequest request)
        {
            try
            {
                var userId = GetRequiredUserId();
                var result = await _mediator.Send(new JoinClassroomCommand(userId, request.JoinCode));
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
    /// Provides data analytics and performance snapshots.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly ISender _mediator;

        public AnalyticsController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the global analytics dashboard for administrators.
        /// </summary>
        /// <returns>Platform-wide metrics (total XP, total users, course completion rates).</returns>
        /// <response code="200">Snapshot returned.</response>
        [Authorize(Roles = "Teacher,Admin")]
        [HttpGet("dashboard")]
        [ProducesResponseType(typeof(AnalyticsSnapshot), 200)]
        public async Task<ActionResult<AnalyticsSnapshot>> GetDashboard()
        {
            return Ok(await _mediator.Send(new GetAnalyticsDashboardQuery()));
        }

        /// <summary>
        /// Retrieves personal teaching analytics for teachers.
        /// </summary>
        /// <returns>Classroom-specific performance metrics.</returns>
        /// <response code="200">Teaching snapshot returned.</response>
        [Authorize(Roles = "Teacher,Admin")]
        [HttpGet("teaching")]
        [ProducesResponseType(typeof(AnalyticsSnapshot), 200)]
        public async Task<ActionResult<AnalyticsSnapshot>> GetTeachingAnalytics()
        {
            var teacherId = GetUserId();
            return Ok(await _mediator.Send(new GetAnalyticsDashboardQuery(teacherId)));
        }

        private int GetUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return claim is not null && int.TryParse(claim, out var id) ? id : throw new UnauthorizedAccessException();
        }
    }

public record CreateClassroomRequest(string Name, string Description, int? CourseId);
public record JoinClassroomRequest(string JoinCode);
