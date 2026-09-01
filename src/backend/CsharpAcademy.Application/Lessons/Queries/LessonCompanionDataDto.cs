namespace CsharpAcademy.Application.Lessons.Queries;

public class LessonCompanionDataDto
{
    public List<string> Encouragements { get; set; } = new();
    public List<string> Celebrations { get; set; } = new();
    public List<string> Greetings { get; set; } = new();
    public Dictionary<string, string> ConceptExplanations { get; set; } = new();
    public List<CompanionQaDto> QuestionAnswers { get; set; } = new();
    public List<CompanionExampleDto> Examples { get; set; } = new();
    public List<CompanionMiniQuizDto> MiniQuizzes { get; set; } = new();
    public List<CompanionPracticeHintDto> PracticeHints { get; set; } = new();
}

public class CompanionQaDto
{
    public List<string> Keywords { get; set; } = new();
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
}

public class CompanionExampleDto
{
    public string Concept { get; set; } = string.Empty;
    public string Analogy { get; set; } = string.Empty;
    public string CodeSample { get; set; } = string.Empty;
}

public class CompanionMiniQuizDto
{
    public string Question { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
    public int CorrectIndex { get; set; }
    public string Explanation { get; set; } = string.Empty;
}

public class CompanionPracticeHintDto
{
    public string ExerciseTitle { get; set; } = string.Empty;
    public List<string> Hints { get; set; } = new();
}
