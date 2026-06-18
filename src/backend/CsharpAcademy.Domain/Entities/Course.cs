namespace CsharpAcademy.Domain.Entities;

public class Course : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Level { get; set; } = "Beginner";
    public string ThumbnailUrl { get; set; } = string.Empty;
    public int EstimatedHours { get; set; }
    public bool IsPublished { get; set; } = true;
    public ICollection<CourseModule> Modules { get; set; } = new List<CourseModule>();
}
