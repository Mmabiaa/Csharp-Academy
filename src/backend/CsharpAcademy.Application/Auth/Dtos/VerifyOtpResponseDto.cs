namespace CsharpAcademy.Application.Auth.Dtos;

public class VerifyOtpResponseDto
{
    public bool Success { get; set; }
    public string ResetToken { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
