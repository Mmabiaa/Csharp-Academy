namespace CsharpAcademy.Domain.Entities;

public class Badge : Entity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public ICollection<UserBadge> UserBadges { get; set; } = new List<UserBadge>();
}

public class UserBadge : Entity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int BadgeId { get; set; }
    public Badge Badge { get; set; } = null!;
    public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
}
