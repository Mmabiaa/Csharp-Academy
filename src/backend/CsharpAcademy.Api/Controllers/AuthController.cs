using CsharpAcademy.Application.Auth.Commands;
using CsharpAcademy.Application.Auth.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CsharpAcademy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthController(ISender mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Registers a new user with the specified role.
    /// </summary>
    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var result = await _mediator.Send(new RegisterCommand(
                request.Email, request.Password, request.FirstName, request.LastName, request.Role ?? "Student"));
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Authenticates a user using email and password.
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await _mediator.Send(new LoginCommand(request.Email, request.Password));
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Authenticates a user using a Google ID token.
    /// </summary>
    [HttpPost("google-login")]
    public async Task<ActionResult<AuthResponseDto>> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        try
        {
            var result = await _mediator.Send(new GoogleLoginCommand(request.IdToken));
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}

public record RegisterRequest(string Email, string Password, string FirstName, string LastName, string? Role);
public record LoginRequest(string Email, string Password);
public record GoogleLoginRequest(string IdToken);
