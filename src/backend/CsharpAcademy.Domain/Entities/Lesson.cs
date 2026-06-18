namespace CsharpAcademy.Domain.Entities;

public class Lesson : Entity
{
    public int CourseModuleId { get; set; }
    public CourseModule CourseModule { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string BestPractices { get; set; } = string.Empty;
    public string VoiceSummary { get; set; } = string.Empty;
    public LessonType Type { get; set; } = LessonType.Reading;
    public int DurationMinutes { get; set; } = 10;
    public int Order { get; set; }
    public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    public ICollection<CodingExercise> CodingExercises { get; set; } = new List<CodingExercise>();
    public ICollection<TutorialStep> TutorialSteps { get; set; } = new List<TutorialStep>();
    public ICollection<LessonVideo> Videos { get; set; } = new List<LessonVideo>();
}

public enum LessonType
{
    Reading = 0,
    Video = 1,
    Practice = 2,
    Challenge = 3,
    Mixed = 4
}
