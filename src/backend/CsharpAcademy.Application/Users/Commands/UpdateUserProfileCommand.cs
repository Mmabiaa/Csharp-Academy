using CsharpAcademy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CsharpAcademy.Application.Users.Commands;

public record UpdateUserProfileCommand(
    int UserId,
    string FirstName,
    string LastName,
    string Email,
    string? ProfileImageUrl,
    string? CurrentPassword = null,
    string? NewPassword = null) : IRequest<bool>;

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, bool>
{
    private readonly UserManager<User> _userManager;

    public UpdateUserProfileCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString())
            ?? throw new KeyNotFoundException("User not found.");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.ProfileImageUrl = request.ProfileImageUrl;

        if (user.Email != request.Email)
        {
            var existing = await _userManager.FindByEmailAsync(request.Email);
            if (existing != null)
            {
                throw new InvalidOperationException("Email is already taken.");
            }
            user.Email = request.Email;
            user.UserName = request.Email;
        }

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        if (!string.IsNullOrEmpty(request.CurrentPassword) && !string.IsNullOrEmpty(request.NewPassword))
        {
            var passResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!passResult.Succeeded)
            {
                throw new InvalidOperationException(string.Join(", ", passResult.Errors.Select(e => e.Description)));
            }
        }

        return true;
    }
}
