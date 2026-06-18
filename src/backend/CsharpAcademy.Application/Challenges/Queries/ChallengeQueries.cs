using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Challenges.Queries;

public record GetChallengesQuery : IRequest<List<ChallengeDto>>;

public class ChallengeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public string StarterCode { get; set; } = string.Empty;
    public string Hint { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public int XpReward { get; set; }
}

public class GetChallengesQueryHandler : IRequestHandler<GetChallengesQuery, List<ChallengeDto>>
{
    private readonly ICodingChallengeRepository _repo;
    public GetChallengesQueryHandler(ICodingChallengeRepository repo) => _repo = repo;

    public async Task<List<ChallengeDto>> Handle(GetChallengesQuery request, CancellationToken cancellationToken)
    {
        var list = await _repo.GetAllPublishedAsync(cancellationToken);
        return list.Select(c => new ChallengeDto
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description,
            Difficulty = c.Difficulty,
            StarterCode = c.StarterCode,
            Hint = c.Hint,
            Tags = c.Tags,
            XpReward = c.XpReward
        }).ToList();
    }
}

public record GetLessonVideosQuery(int LessonId) : IRequest<List<LessonVideoDto>>;

public class LessonVideoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string EmbedUrl { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
}

public class GetLessonVideosQueryHandler : IRequestHandler<GetLessonVideosQuery, List<LessonVideoDto>>
{
    private readonly ILessonVideoRepository _repo;
    public GetLessonVideosQueryHandler(ILessonVideoRepository repo) => _repo = repo;

    public async Task<List<LessonVideoDto>> Handle(GetLessonVideosQuery request, CancellationToken cancellationToken)
    {
        var videos = await _repo.GetByLessonIdAsync(request.LessonId, cancellationToken);
        return videos.Select(v => new LessonVideoDto
        {
            Id = v.Id,
            Title = v.Title,
            VideoUrl = v.VideoUrl,
            Provider = v.Provider.ToString(),
            EmbedUrl = ToEmbedUrl(v.VideoUrl, v.Provider),
            DurationMinutes = v.DurationMinutes
        }).ToList();
    }

    private static string ToEmbedUrl(string url, Domain.Entities.VideoProvider provider)
    {
        if (provider == Domain.Entities.VideoProvider.YouTube)
        {
            var id = ExtractYouTubeId(url);
            return id is not null ? $"https://www.youtube.com/embed/{id}" : url;
        }
        return url;
    }

    private static string? ExtractYouTubeId(string url)
    {
        if (url.Contains("youtu.be/"))
            return url.Split("youtu.be/").Last().Split('?').First();
        if (url.Contains("v="))
            return url.Split("v=").Last().Split('&').First();
        return null;
    }
}
