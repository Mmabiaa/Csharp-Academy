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

    /// <summary>
    /// Initiates password reset by sending OTP to email.
    /// </summary>
    /// <param name="request">Email address for password reset.</param>
    /// <returns>Success message.</returns>
    /// <response code="200">OTP sent successfully.</response>
    [HttpPost("forgot-password")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        await _mediator.Send(new ForgotPasswordCommand(request.Email));
        return Ok(new { message = "If the email exists, an OTP has been sent." });
    }

    /// <summary>
    /// Verifies OTP for password reset.
    /// </summary>
    /// <param name="request">Email and OTP.</param>
    /// <returns>Verification response with reset token.</returns>
    /// <response code="200">OTP verified successfully.</response>
    /// <response code="401">Invalid or expired OTP.</response>
    [HttpPost("verify-otp")]
    [ProducesResponseType(typeof(VerifyOtpResponseDto), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<VerifyOtpResponseDto>> VerifyOtp([FromBody] VerifyOtpRequest request)
    {
        try
        {
            var result = await _mediator.Send(new VerifyOtpCommand(request.Email, request.Otp));
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Resets user password using OTP.
    /// </summary>
    /// <param name="request">Email, OTP, and new password.</param>
    /// <returns>Success message.</returns>
    /// <response code="200">Password reset successfully.</response>
    /// <response code="401">Invalid or expired OTP.</response>
    [HttpPost("reset-password")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        try
        {
            await _mediator.Send(new ResetPasswordCommand(request.Email, request.Otp, request.NewPassword));
            return Ok(new { message = "Password reset successfully." });
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
public record ForgotPasswordRequest(string Email);
public record VerifyOtpRequest(string Email, string Otp);
public record ResetPasswordRequest(string Email, string Otp, string NewPassword);
