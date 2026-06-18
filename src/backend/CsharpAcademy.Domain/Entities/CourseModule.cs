namespace CsharpAcademy.Domain.Entities;

public class CourseModule : Entity
{
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LearningObjectives { get; set; } = string.Empty;
    public int Order { get; set; }
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
