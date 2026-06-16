namespace CsharpAcademy.Domain.Entities;

public class CodingExercise : Entity
{
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public string StarterCode { get; set; } = string.Empty;
    public string ExpectedOutput { get; set; } = string.Empty;
    public string Hint { get; set; } = string.Empty;
    public int Order { get; set; }
    public int Difficulty { get; set; } = 1;
}
