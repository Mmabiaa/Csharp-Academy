namespace CsharpAcademy.Domain.Entities;

public class Question : Entity
{
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
    public QuestionType Type { get; set; }
    public string? CorrectAnswer { get; set; }
    public ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
}

public enum QuestionType
{
    MultipleChoice,
    TrueFalse,
    FillInTheBlank,
    OutputPrediction
}
