namespace CsharpAcademy.Domain.Entities;

public class LessonVideo : Entity
{
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public VideoProvider Provider { get; set; } = VideoProvider.YouTube;
    public int Order { get; set; }
    public int DurationMinutes { get; set; }
}

public enum VideoProvider
{
    YouTube = 0,
    Vimeo = 1
}
