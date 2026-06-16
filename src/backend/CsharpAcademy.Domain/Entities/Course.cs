namespace CsharpAcademy.Domain.Entities;

public class Course : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<CourseModule> Modules { get; set; } = new List<CourseModule>();
}
