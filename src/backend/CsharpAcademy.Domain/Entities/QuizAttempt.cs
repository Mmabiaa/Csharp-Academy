namespace CsharpAcademy.Domain.Entities;

public class QuizAttempt : Entity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
    public int Score { get; set; }
    public int TotalQuestions { get; set; }
    public bool Passed { get; set; }
    public DateTime AttemptedAt { get; set; } = DateTime.UtcNow;
}
