using CsharpAcademy.Application.Auth.Dtos;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Auth.Commands;

public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, VerifyOtpResponseDto>
{
    private readonly IUserRepository _userRepository;

    public VerifyOtpCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<VerifyOtpResponseDto> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or OTP");
        }

        var resetToken = await _userRepository.GetValidPasswordResetTokenAsync(user.Id, request.Otp);
        
        if (resetToken == null || resetToken.IsUsed || resetToken.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid or expired OTP");
        }

        // Generate a temporary reset token for the next step
        var tempToken = Guid.NewGuid().ToString();
        
        return new VerifyOtpResponseDto
        {
            Success = true,
            ResetToken = tempToken,
            Email = request.Email
        };
    }
}
