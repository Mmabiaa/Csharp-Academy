namespace CsharpAcademy.Domain.Entities;

public class Lesson : Entity
{
    public int CourseModuleId { get; set; }
    public CourseModule CourseModule { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string BestPractices { get; set; } = string.Empty;
    public string VoiceSummary { get; set; } = string.Empty;
    public int Order { get; set; }
    public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    public ICollection<CodingExercise> CodingExercises { get; set; } = new List<CodingExercise>();
    public ICollection<TutorialStep> TutorialSteps { get; set; } = new List<TutorialStep>();
}
