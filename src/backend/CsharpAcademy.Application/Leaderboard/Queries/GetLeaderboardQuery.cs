using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Leaderboard.Queries;

public record GetLeaderboardQuery(int Top = 10) : IRequest<List<LeaderboardEntryDto>>;

public class LeaderboardEntryDto
{
    public int Rank { get; set; }
    public int UserId { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public int Xp { get; set; }
    public int CurrentStreak { get; set; }
}

public class GetLeaderboardQueryHandler : IRequestHandler<GetLeaderboardQuery, List<LeaderboardEntryDto>>
{
    private readonly IUserRepository _userRepository;

    public GetLeaderboardQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<LeaderboardEntryDto>> Handle(GetLeaderboardQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetTopByXpAsync(request.Top, cancellationToken);
        return users.Select((u, i) => new LeaderboardEntryDto
        {
            Rank = i + 1,
            UserId = u.Id,
            DisplayName = $"{u.FirstName} {u.LastName}",
            Xp = u.Xp,
            CurrentStreak = u.CurrentStreak
        }).ToList();
    }
}
