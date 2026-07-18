using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Entities;
using CsharpAcademy.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CsharpAcademy.Application.Auth.Commands;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;

    public ForgotPasswordCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService,
        ILogger<ForgotPasswordCommandHandler> logger)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        
        // For security, don't reveal if the email exists or not
        // Always return success to prevent email enumeration
        if (user == null)
        {
            _logger.LogWarning("Password reset requested for non-existent email: {Email}", request.Email);
            return Unit.Value;
        }

        // Generate 6-digit OTP
        var otp = GenerateOTP();
        
        // Create password reset token
        var resetToken = new PasswordResetToken
        {
            UserId = user.Id,
            Token = otp,
            ExpiresAt = DateTime.UtcNow.AddMinutes(15), // 15 minutes expiry
            IsUsed = false,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.SavePasswordResetTokenAsync(resetToken);

        // Send OTP via email
        await _emailService.SendPasswordResetOtpAsync(
            user.Email,
            otp,
            $"{user.FirstName} {user.LastName}"
        );

        _logger.LogInformation("Password reset OTP sent to {Email}", request.Email);

        return Unit.Value;
    }

    private static string GenerateOTP()
    {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }
}
