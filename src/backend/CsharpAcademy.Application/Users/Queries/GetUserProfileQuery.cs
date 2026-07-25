using CsharpAcademy.Domain.Interfaces;
using CsharpAcademy.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MediatR;

namespace CsharpAcademy.Application.Users.Queries;

public record GetUserProfileQuery(int UserId) : IRequest<UserProfileDto>;

public class UserProfileDto
{
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
    public bool VoiceRecognitionEnabled { get; set; }
    public int Xp { get; set; }
    public int CurrentStreak { get; set; }
    public int MaxStreak { get; set; }
    public List<string> Roles { get; set; } = new();
    public List<BadgeDto> Badges { get; set; } = new();
    public List<EnrollmentSummaryDto> Enrollments { get; set; } = new();
}

public class BadgeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class EnrollmentSummaryDto
{
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public double CompletionPercentage { get; set; }
}

public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly UserManager<User> _userManager;

    public GetUserProfileQueryHandler(
        IUserRepository userRepository,
        IEnrollmentRepository enrollmentRepository,
        UserManager<User> userManager)
    {
        _userRepository = userRepository;
        _enrollmentRepository = enrollmentRepository;
        _userManager = userManager;
    }

    public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new KeyNotFoundException("User not found.");

        var badges = await _userRepository.GetUserBadgesAsync(request.UserId, cancellationToken);
        var enrollments = await _enrollmentRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        var roles = await _userManager.GetRolesAsync(user);

        return new UserProfileDto
        {
            UserId = user.Id,
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Roles = roles.ToList(),
            ProfileImageUrl = user.ProfileImageUrl,
            VoiceRecognitionEnabled = user.VoiceRecognitionEnabled,
            Xp = user.Xp,
            CurrentStreak = user.CurrentStreak,
            MaxStreak = user.MaxStreak,
            Badges = badges.Select(b => new BadgeDto
            {
                Id = b.Id,
                Name = b.Name,
                Description = b.Description
            }).ToList(),
            Enrollments = enrollments.Select(e => new EnrollmentSummaryDto
            {
                CourseId = e.CourseId,
                CourseTitle = e.Course.Title,
                CompletionPercentage = e.CompletionPercentage
            }).ToList()
        };
    }
}
