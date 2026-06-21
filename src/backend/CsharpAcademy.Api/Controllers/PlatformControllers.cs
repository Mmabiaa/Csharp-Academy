using System.Security.Claims;
using CsharpAcademy.Application.Admin.Queries;
using CsharpAcademy.Application.Assignments.Commands;
using CsharpAcademy.Application.Assignments.Queries;
using CsharpAcademy.Application.Challenges.Commands;
using CsharpAcademy.Application.Challenges.Queries;
using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CsharpAcademy.Infrastructure.Data;

namespace CsharpAcademy.Api.Controllers;

    /// <summary>
    /// Provides administrative actions for managing the platform.
    /// </summary>
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly ICourseRepository _courses;

        public AdminController(ISender mediator, ICourseRepository courses)
        {
            _mediator = mediator;
            _courses = courses;
        }

        /// <summary>
        /// Retrieves the administrative dashboard data.
        /// </summary>
        /// <returns>Dashboard metrics including users, courses, and platform activity.</returns>
        /// <response code="200">Dashboard data returned.</response>
        /// <response code="401">Unauthorized access.</response>
        /// <response code="403">Forbidden if not in Admin role.</response>
        [HttpGet("dashboard")]
        [ProducesResponseType(typeof(AdminDashboardDto), 200)]
        public async Task<ActionResult<AdminDashboardDto>> Dashboard() =>
            Ok(await _mediator.Send(new GetAdminDashboardQuery()));

        /// <summary>
        /// Retrieves a list of recent users for administrative review.
        /// </summary>
        /// <returns>List of user summary DTOs.</returns>
        /// <response code="200">User list returned.</response>
        [HttpGet("users")]
        [ProducesResponseType(typeof(List<AdminUserDto>), 200)]
        public async Task<ActionResult<List<AdminUserDto>>> Users()
        {
            var dash = await _mediator.Send(new GetAdminDashboardQuery());
            return Ok(dash.RecentUsers);
        }

        /// <summary>
        /// Creates a new course.
        /// </summary>
        /// <param name="request">The course creation details.</param>
        /// <returns>The created course entity.</returns>
        [HttpPost("courses")]
        [ProducesResponseType(typeof(Course), 200)]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] CreateCourseRequest request)
        {
            var course = await _courses.CreateAsync(new Course
            {
                Title = request.Title,
                Description = request.Description,
                Level = request.Level ?? "Beginner",
                EstimatedHours = request.EstimatedHours,
                IsPublished = true
            });
            return Ok(course);
        }

        /// <summary>
        /// Updates an existing course or creates it if it doesn't exist.
        /// </summary>
        /// <param name="id">The course identifier.</param>
        /// <param name="course">The updated course data.</param>
        /// <returns>Status only.</returns>
        [HttpPut("courses/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course course)
        {
            if (id > 0)
            {
               await _courses.UpdateAsync(course);   
            }
            else 
            {
                await _courses.CreateAsync(course);
            }
            return Ok();
        }

        /// <summary>
        /// Deletes a course.
        /// </summary>
        /// <param name="id">The course identifier.</param>
        /// <returns>Status only.</returns>
        [HttpDelete("courses/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courses.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Creates a new course module.
        /// </summary>
        /// <param name="repo">Repository dependency.</param>
        /// <param name="module">The module data.</param>
        /// <returns>The created module.</returns>
        [HttpPost("modules")]
        [ProducesResponseType(typeof(CourseModule), 200)]
        public async Task<ActionResult<CourseModule>> CreateModule([FromServices] IModuleRepository repo, [FromBody] CourseModule module) =>
            Ok(await repo.CreateAsync(module));

        /// <summary>
        /// Updates an existing course module.
        /// </summary>
        /// <param name="repo">Repository dependency.</param>
        /// <param name="module">The module data.</param>
        /// <returns>Status only.</returns>
        [HttpPut("modules/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateModule([FromServices] IModuleRepository repo, [FromBody] CourseModule module)
        {
            await repo.UpdateAsync(module);
            return Ok();
        }

        /// <summary>
        /// Deletes a course module.
        /// </summary>
        /// <param name="repo">Repository dependency.</param>
        /// <param name="id">The module identifier.</param>
        /// <returns>Status only.</returns>
        [HttpDelete("modules/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteModule([FromServices] IModuleRepository repo, int id)
        {
            await repo.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Creates a new lesson.
        /// </summary>
        /// <param name="repo">Repository dependency.</param>
        /// <param name="lesson">The lesson data.</param>
        /// <returns>The created lesson.</returns>
        [HttpPost("lessons")]
        [ProducesResponseType(typeof(Lesson), 200)]
        public async Task<ActionResult<Lesson>> CreateLesson([FromServices] ILessonRepository repo, [FromBody] Lesson lesson) =>
            Ok(await repo.CreateAsync(lesson));

        /// <summary>
        /// Updates an existing lesson.
        /// </summary>
        /// <param name="repo">Repository dependency.</param>
        /// <param name="lesson">The lesson data.</param>
        /// <returns>Status only.</returns>
        [HttpPut("lessons/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateLesson([FromServices] ILessonRepository repo, [FromBody] Lesson lesson)
        {
            await repo.UpdateAsync(lesson);
            return Ok();
        }

        /// <summary>
        /// Deletes a lesson.
        /// </summary>
        /// <param name="repo">Repository dependency.</param>
        /// <param name="id">The lesson identifier.</param>
        /// <returns>Status only.</returns>
        [HttpDelete("lessons/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteLesson([FromServices] ILessonRepository repo, int id)
        {
            await repo.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Creates a new coding practice exercise.
        /// </summary>
        /// <param name="repo">Repository dependency.</param>
        /// <param name="ex">Exercise data.</param>
        /// <returns>The created exercise.</returns>
        [HttpPost("practices")]
        [ProducesResponseType(typeof(CodingExercise), 200)]
        public async Task<ActionResult<CodingExercise>> CreatePractice([FromServices] ICodingExerciseRepository repo, [FromBody] CodingExercise ex) =>
            Ok(await repo.CreateAsync(ex));

        /// <summary>
        /// Updates an existing coding practice exercise.
        /// </summary>
        /// <param name="repo">Repository dependency.</param>
        /// <param name="ex">Exercise data.</param>
        /// <returns>Status only.</returns>
        [HttpPut("practices/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdatePractice([FromServices] ICodingExerciseRepository repo, [FromBody] CodingExercise ex)
        {
            await repo.UpdateAsync(ex);
            return Ok();
        }

        /// <summary>
        /// Deletes a coding practice exercise.
        /// </summary>
        /// <param name="repo">Repository dependency.</param>
        /// <param name="id">The exercise identifier.</param>
        /// <returns>Status only.</returns>
        [HttpDelete("practices/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeletePractice([FromServices] ICodingExerciseRepository repo, int id)
        {
            await repo.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Retrieves all lesson videos across the platform.
        /// </summary>
        /// <returns>List of video metadata.</returns>
        [HttpGet("videos")]
        [ProducesResponseType(typeof(List<CsharpAcademy.Application.Challenges.Queries.LessonVideoDto>), 200)]
        public async Task<ActionResult<List<CsharpAcademy.Application.Challenges.Queries.LessonVideoDto>>> GetAllVideos() =>
            Ok(await _mediator.Send(new CsharpAcademy.Application.Challenges.Queries.GetAllVideosQuery()));

        /// <summary>
        /// Adds a new video to a lesson.
        /// </summary>
        /// <param name="video">Video metadata.</param>
        /// <param name="context">DB context dependency.</param>
        /// <returns>Status only.</returns>
        [HttpPost("videos")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateVideo([FromBody] LessonVideo video, [FromServices] ApplicationDbContext context)
        {
            await context.LessonVideos.AddAsync(video);
            await context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Updates an existing lesson video.
        /// </summary>
        /// <param name="id">User identifier.</param>
        /// <param name="video">Video metadata.</param>
        /// <param name="context">DB context dependency.</param>
        /// <returns>Status only.</returns>
        [HttpPut("videos/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateVideo(int id, [FromBody] LessonVideo video, [FromServices] ApplicationDbContext context)
        {
            context.Entry(video).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Deletes a lesson video.
        /// </summary>
        /// <param name="id">Video identifier.</param>
        /// <param name="repo">Repository dependency.</param>
        /// <returns>Status only.</returns>
        [HttpDelete("videos/{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteVideo(int id, [FromServices] ILessonVideoRepository repo)
        {
            await repo.DeleteAsync(id);
            return Ok();
        }
    }

    /// <summary>
    /// Provides actions for teachers to manage their dashboard and students.
    /// </summary>
    [ApiController]
    [Route("api/teacher")]
    [Authorize(Roles = "Teacher,Admin")]
    public class TeacherController : ControllerBase
    {
        private readonly ISender _mediator;

        public TeacherController(ISender mediator) => _mediator = mediator;

        /// <summary>
        /// Retrieves the teacher dashboard data.
        /// </summary>
        /// <returns>Dashboard metrics for the current teacher.</returns>
        /// <response code="200">Dashboard data returned.</response>
        /// <response code="401">Unauthorized access.</response>
        [HttpGet("dashboard")]
        [ProducesResponseType(typeof(TeacherDashboardDto), 200)]
        public async Task<ActionResult<TeacherDashboardDto>> Dashboard()
        {
            var id = GetUserId();
            return Ok(await _mediator.Send(new GetTeacherDashboardQuery(id)));
        }

        private int GetUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return claim is not null && int.TryParse(claim, out var id) ? id : throw new UnauthorizedAccessException();
        }
    }

    /// <summary>
    /// Manages assignments and student submissions.
    /// </summary>
    [ApiController]
    [Route("api/assignments")]
    public class AssignmentsController : ControllerBase
    {
        private readonly ISender _mediator;

        public AssignmentsController(ISender mediator) => _mediator = mediator;

        /// <summary>
        /// Creates a new assignment.
        /// </summary>
        /// <param name="request">The assignment creation details.</param>
        /// <returns>The created assignment.</returns>
        /// <response code="200">Assignment created.</response>
        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost]
        [ProducesResponseType(typeof(AssignmentDto), 200)]
        public async Task<ActionResult<AssignmentDto>> Create([FromBody] CreateAssignmentRequest request)
        {
            var result = await _mediator.Send(new CreateAssignmentCommand(
                GetUserId(), request.Title, request.Description, request.Instructions,
                request.CourseId, request.LessonId, request.ClassroomId,
                request.DueDate, request.MaxPoints, request.RequiresCode));
            return Ok(result);
        }

        /// <summary>
        /// Retrieves assignments being taught by the current teacher.
        /// </summary>
        /// <returns>List of assignments.</returns>
        [Authorize(Roles = "Teacher,Admin")]
        [HttpGet("teaching")]
        [ProducesResponseType(typeof(List<AssignmentDto>), 200)]
        public async Task<ActionResult<List<AssignmentDto>>> Teaching() =>
            Ok(await _mediator.Send(new GetTeacherAssignmentsQuery(GetUserId())));

        /// <summary>
        /// Retrieves assignments assigned to the current student.
        /// </summary>
        /// <returns>List of assignments.</returns>
        [Authorize]
        [HttpGet("my")]
        [ProducesResponseType(typeof(List<AssignmentDto>), 200)]
        public async Task<ActionResult<List<AssignmentDto>>> MyAssignments() =>
            Ok(await _mediator.Send(new GetStudentAssignmentsQuery(GetUserId())));

        /// <summary>
        /// Retrieves assignments for the current student's classroom.
        /// </summary>
        /// <returns>List of assignments.</returns>
        [Authorize]
        [HttpGet("classroom")]
        [ProducesResponseType(typeof(List<AssignmentDto>), 200)]
        public async Task<ActionResult<List<AssignmentDto>>> ClassroomAssignments() =>
            Ok(await _mediator.Send(new GetStudentAssignmentsQuery(GetUserId())));

        /// <summary>
        /// Retrieves submissions for a specific assignment.
        /// </summary>
        /// <param name="id">The assignment ID.</param>
        /// <returns>List of submissions with student info.</returns>
        [Authorize(Roles = "Teacher,Admin")]
        [HttpGet("{id}/submissions")]
        [ProducesResponseType(typeof(List<SubmissionDto>), 200)]
        public async Task<ActionResult<List<SubmissionDto>>> Submissions(int id) =>
            Ok(await _mediator.Send(new GetAssignmentSubmissionsQuery(id, GetUserId())));

        /// <summary>
        /// Grades a student's submission.
        /// </summary>
        /// <param name="id">The submission ID.</param>
        /// <param name="body">Grade and feedback data.</param>
        /// <returns>The updated submission.</returns>
        [Authorize(Roles = "Teacher,Admin")]
        [HttpPost("submissions/{id}/grade")]
        [ProducesResponseType(typeof(SubmissionDto), 200)]
        public async Task<ActionResult<SubmissionDto>> Grade(int id, [FromBody] GradeBody body) =>
            Ok(await _mediator.Send(new GradeSubmissionCommand(GetUserId(), id, body.Grade, body.Feedback)));

        /// <summary>
        /// Submits work for an assignment.
        /// </summary>
        /// <param name="id">The assignment ID.</param>
        /// <param name="body">Submission content/code.</param>
        /// <returns>The created submission.</returns>
        [Authorize]
        [HttpPost("{id}/submit")]
        [ProducesResponseType(typeof(SubmissionDto), 200)]
        public async Task<ActionResult<SubmissionDto>> Submit(int id, [FromBody] SubmitAssignmentBody body) =>
            Ok(await _mediator.Send(new SubmitAssignmentCommand(GetUserId(), id, body.Content)));

        /// <summary>
        /// Uploads an attachment for a classroom, assignment, or submission.
        /// </summary>
        /// <param name="classroomId">Target classroom.</param>
        /// <param name="assignmentId">Target assignment.</param>
        /// <param name="submissionId">Target submission.</param>
        /// <param name="file">The binary file to upload.</param>
        /// <returns>The attachment DTO.</returns>
        [Authorize]
        [HttpPost("attachments")]
        [ProducesResponseType(typeof(AttachmentDto), 200)]
        public async Task<ActionResult<AttachmentDto>> UploadAttachment(
            [FromForm] int? classroomId, 
            [FromForm] int? assignmentId, 
            [FromForm] int? submissionId, 
            IFormFile file)
        {
            var result = await _mediator.Send(new UploadAttachmentCommand(
                GetUserId(), classroomId, assignmentId, submissionId, file));
            return Ok(result);
        }

        /// <summary>
        /// Deletes a file attachment.
        /// </summary>
        /// <param name="id">The attachment ID.</param>
        /// <returns>No content.</returns>
        [Authorize]
        [HttpDelete("attachments/{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            await _mediator.Send(new DeleteAttachmentCommand(GetUserId(), id));
            return NoContent();
        }

        private int GetUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return claim is not null && int.TryParse(claim, out var uid) ? uid : throw new UnauthorizedAccessException();
        }
    }

    /// <summary>
    /// Manages coding challenges and student participation.
    /// </summary>
    [ApiController]
    [Route("api/challenges")]
    public class ChallengesController : ControllerBase
    {
        private readonly ISender _mediator;

        public ChallengesController(ISender mediator) => _mediator = mediator;

        /// <summary>
        /// Retrieves all available challenges.
        /// </summary>
        /// <returns>List of challenges.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ChallengeDto>), 200)]
        public async Task<ActionResult<List<ChallengeDto>>> GetAll() =>
            Ok(await _mediator.Send(new GetChallengesQuery()));

        /// <summary>
        /// Submits a solution for a challenge.
        /// </summary>
        /// <param name="id">The challenge ID.</param>
        /// <param name="body">The source code.</param>
        /// <returns>Result of challenge completion.</returns>
        [Authorize]
        [HttpPost("{id}/submit")]
        [ProducesResponseType(typeof(ChallengeResultDto), 200)]
        public async Task<ActionResult<ChallengeResultDto>> Submit(int id, [FromBody] SubmitChallengeBody body)
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (claim is null || !int.TryParse(claim, out var userId))
                throw new UnauthorizedAccessException();
            return Ok(await _mediator.Send(new SubmitChallengeCommand(userId, id, body.Code)));
        }
    }

public record CreateCourseRequest(string Title, string Description, string? Level, int EstimatedHours);
public record CreateAssignmentRequest(string Title, string Description, string Instructions, int? CourseId, int? LessonId, int? ClassroomId, DateTime? DueDate, int MaxPoints, bool RequiresCode);
public record SubmitAssignmentBody(string Content);
public record GradeBody(int Grade, string Feedback);
public record SubmitChallengeBody(string Code);
