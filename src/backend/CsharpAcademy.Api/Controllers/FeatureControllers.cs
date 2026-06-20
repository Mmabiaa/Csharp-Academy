using System.Security.Claims;
using CsharpAcademy.Application.Assistant.Commands;
using CsharpAcademy.Application.Certificates.Queries;
using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Application.Leaderboard.Queries;
using CsharpAcademy.Application.Playground.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAcademy.Api.Controllers;

    /// <summary>
    /// Provides an interactive C# coding environment for real-time code execution.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PlaygroundController : ControllerBase
    {
        private readonly ISender _mediator;

        public PlaygroundController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Executes C# code in a secure sandboxed environment and returns the output.
        /// </summary>
        [HttpPost("run")]
        public async Task<ActionResult<CodeExecutionResult>> Run([FromBody] RunCodeRequest request)
        {
            var result = await _mediator.Send(new RunCodeCommand(request.Code));
            return Ok(result);
        }
    }

    /// <summary>
    /// Provides AI-powered learning assistance.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AssistantController : ControllerBase
    {
        private readonly ISender _mediator;

        public AssistantController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Sends a message to the AI assistant for contextual help or general inquiries.
        /// </summary>
        [HttpPost("chat")]
        public async Task<ActionResult<AiAssistantResponse>> Chat([FromBody] ChatRequest request)
        {
            var result = await _mediator.Send(new AskAssistantCommand(request.Message, request.LessonContext));
            return Ok(result);
        }
    }

    /// <summary>
    /// Manages the issuance and verification of course completion certificates.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CertificatesController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly ICertificatePdfService _pdfService;

        public CertificatesController(ISender mediator, ICertificatePdfService pdfService)
        {
            _mediator = mediator;
            _pdfService = pdfService;
        }

        /// <summary>
        /// Retrieves certificates earned by the currently authenticated user.
        /// </summary>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<CertificateDto>>> GetMyCertificates()
        {
            var userId = GetRequiredUserId();
            var certs = await _mediator.Send(new GetUserCertificatesQuery(userId));
            return Ok(certs);
        }

        /// <summary>
        /// Verifies a certificate using its unique code.
        /// </summary>
        [HttpGet("verify/{code}")]
        public async Task<ActionResult<CertificateVerificationDto>> Verify(string code)
        {
            var cert = await _mediator.Send(new GetCertificateByCodeQuery(code));
            if (cert is null)
            {
                return NotFound(new { message = "Certificate not found." });
            }

            return Ok(cert);
        }

        /// <summary>
        /// Downloads the PDF version of a certificate.
        /// </summary>
        [HttpGet("{code}/pdf")]
        public async Task<IActionResult> DownloadPdf(string code)
        {
            var cert = await _mediator.Send(new GetCertificateByCodeQuery(code));
            if (cert is null)
            {
                return NotFound(new { message = "Certificate not found." });
            }

            var pdf = await _pdfService.GeneratePdfAsync(
                cert.StudentName, cert.CourseTitle, cert.CertificateCode, cert.IssuedAt);

            return File(pdf, "application/pdf", $"certificate-{code}.pdf");
        }

        private int GetRequiredUserId()
        {
            var claim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (claim is null || !int.TryParse(claim, out var userId))
            {
                throw new UnauthorizedAccessException();
            }

            return userId;
        }
    }

    /// <summary>
    /// Manages user rankings and global XP leaderboard.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ISender _mediator;

        public LeaderboardController(ISender mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves the top users by XP.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<LeaderboardEntryDto>>> GetLeaderboard([FromQuery] int top = 10)
        {
            var entries = await _mediator.Send(new GetLeaderboardQuery(top));
            return Ok(entries);
        }
    }

public record RunCodeRequest(string Code);
public record ChatRequest(string Message, string? LessonContext);
