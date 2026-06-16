namespace CsharpAcademy.Domain.Entities;

public class PracticeCompletion : Entity
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int ExerciseId { get; set; }
    public CodingExercise Exercise { get; set; } = null!;
    public DateTime CompletedAt { get; set; }
}
