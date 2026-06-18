namespace CsharpAcademy.Application.Common.Interfaces;

public interface IAiQuizGenerationService
{
    Task<GeneratedQuizData> GenerateAsync(string lessonTitle, string lessonContent, int questionCount, CancellationToken cancellationToken = default);
}

public class GeneratedQuizData
{
    public string Title { get; set; } = string.Empty;
    public List<GeneratedQuestionData> Questions { get; set; } = new();
}

public class GeneratedQuestionData
{
    public string Text { get; set; } = string.Empty;
    public string Type { get; set; } = "MultipleChoice";
    public string? CorrectAnswer { get; set; }
    public List<GeneratedOptionData> Options { get; set; } = new();
}

public class GeneratedOptionData
{
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
