using CsharpAcademy.Application.Auth.Dtos;
using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Entities;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CsharpAcademy.Application.Auth.Commands;

public record GoogleLoginCommand(string IdToken) : IRequest<AuthResponseDto>;

public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, AuthResponseDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _jwtTokenService;

    public GoogleLoginCommandHandler(UserManager<User> userManager, IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthResponseDto> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
    {
        GoogleJsonWebSignature.Payload payload;
        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
        }
        catch (InvalidJwtException)
        {
            throw new UnauthorizedAccessException("Invalid Google token.");
        }

        var user = await _userManager.FindByEmailAsync(payload.Email);
        if (user == null)
        {
            user = new User
            {
                UserName = payload.Email,
                Email = payload.Email,
                FirstName = payload.GivenName ?? "User",
                LastName = payload.FamilyName ?? "",
                ProfileImageUrl = payload.Picture
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            await _userManager.AddToRoleAsync(user, "Student");
        }
        else if (string.IsNullOrEmpty(user.ProfileImageUrl))
        {
            user.ProfileImageUrl = payload.Picture;
            await _userManager.UpdateAsync(user);
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenService.GenerateToken(user, roles);

        return new AuthResponseDto
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ProfileImageUrl = user.ProfileImageUrl,
            Roles = roles.ToList(),
            Xp = user.Xp,
            CurrentStreak = user.CurrentStreak
        };
    }
}
