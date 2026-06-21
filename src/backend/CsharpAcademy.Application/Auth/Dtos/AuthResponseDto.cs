namespace CsharpAcademy.Application.Auth.Dtos;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
    public List<string> Roles { get; set; } = new();
    public int Xp { get; set; }
    public int CurrentStreak { get; set; }
}
