namespace CsharpAcademy.Domain.Entities;

public class TutorialStep : Entity
{
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? CodeSample { get; set; }
    public int Order { get; set; }
}
