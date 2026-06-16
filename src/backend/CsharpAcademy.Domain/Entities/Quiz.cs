namespace CsharpAcademy.Domain.Entities;

public class Quiz : Entity
{
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public ICollection<Question> Questions { get; set; } = new List<Question>();
}
