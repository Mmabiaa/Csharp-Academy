namespace CsharpAcademy.Domain.Entities;

public class CodingChallenge : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Difficulty { get; set; } = "Easy";
    public string StarterCode { get; set; } = string.Empty;
    public string ExpectedOutput { get; set; } = string.Empty;
    public string Hint { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    public int Order { get; set; }
    public int XpReward { get; set; } = 20;
    public bool IsPublished { get; set; } = true;
}

public class ChallengeCompletion : Entity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int ChallengeId { get; set; }
    public CodingChallenge Challenge { get; set; } = null!;
    public DateTime CompletedAt { get; set; }
}
