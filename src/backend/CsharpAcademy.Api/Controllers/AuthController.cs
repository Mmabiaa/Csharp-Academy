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
    /// <param name="request">The registration details including email, password, and optional role.</param>
    /// <returns>Auth response containing JWT and user profile.</returns>
    /// <response code="200">Registration successful.</response>
    /// <response code="400">If email is already taken or registration fails.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), 200)]
    [ProducesResponseType(400)]
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
    /// <param name="request">Login credentials.</param>
    /// <returns>Auth response with token.</returns>
    /// <response code="200">Login successful.</response>
    /// <response code="401">Invalid credentials.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), 200)]
    [ProducesResponseType(401)]
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
    /// <param name="request">The Google identity token.</param>
    /// <returns>Auth response with token.</returns>
    /// <response code="200">Google login successful.</response>
    /// <response code="401">If the Google token is invalid or unauthorized.</response>
    [HttpPost("google-login")]
    [ProducesResponseType(typeof(AuthResponseDto), 200)]
    [ProducesResponseType(401)]
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
