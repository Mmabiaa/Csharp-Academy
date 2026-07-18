using CsharpAcademy.Domain.Interfaces;
using CsharpAcademy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CsharpAcademy.Application.Auth.Commands;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;

    public ResetPasswordCommandHandler(IUserRepository userRepository, UserManager<User> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
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

        // Remove old password and set new one using UserManager
        var removePasswordResult = await _userManager.RemovePasswordAsync(user);
        if (!removePasswordResult.Succeeded)
        {
            throw new InvalidOperationException("Failed to remove old password");
        }

        var addPasswordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            throw new InvalidOperationException("Failed to set new password");
        }

        // Mark token as used
        resetToken.IsUsed = true;
        await _userRepository.UpdatePasswordResetTokenAsync(resetToken);

        return Unit.Value;
    }
}
