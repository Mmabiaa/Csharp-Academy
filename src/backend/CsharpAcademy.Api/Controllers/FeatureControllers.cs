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

[ApiController]
[Route("api/[controller]")]
public class PlaygroundController : ControllerBase
{
    private readonly ISender _mediator;

    public PlaygroundController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("run")]
    public async Task<ActionResult<CodeExecutionResult>> Run([FromBody] RunCodeRequest request)
    {
        var result = await _mediator.Send(new RunCodeCommand(request.Code));
        return Ok(result);
    }
}

[ApiController]
[Route("api/[controller]")]
public class AssistantController : ControllerBase
{
    private readonly ISender _mediator;

    public AssistantController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("chat")]
    public async Task<ActionResult<AiAssistantResponse>> Chat([FromBody] ChatRequest request)
    {
        var result = await _mediator.Send(new AskAssistantCommand(request.Message, request.LessonContext));
        return Ok(result);
    }
}

[ApiController]
[Route("api/[controller]")]
public class CertificatesController : ControllerBase
{
    private readonly ISender _mediator;

    public CertificatesController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<CertificateDto>>> GetMyCertificates()
    {
        var userId = GetRequiredUserId();
        var certs = await _mediator.Send(new GetUserCertificatesQuery(userId));
        return Ok(certs);
    }

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

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly ISender _mediator;

    public LeaderboardController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeaderboardEntryDto>>> GetLeaderboard([FromQuery] int top = 10)
    {
        var entries = await _mediator.Send(new GetLeaderboardQuery(top));
        return Ok(entries);
    }
}

public record RunCodeRequest(string Code);
public record ChatRequest(string Message, string? LessonContext);
