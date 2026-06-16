namespace CsharpAcademy.Domain.Entities;

public class QuestionOption : Entity
{
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
