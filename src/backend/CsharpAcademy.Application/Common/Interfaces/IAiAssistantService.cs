namespace CsharpAcademy.Application.Common.Interfaces;

public interface IAiAssistantService
{
    Task<AiAssistantResponse> GetResponseAsync(AiAssistantRequest request, CancellationToken cancellationToken = default);
}

public class AiAssistantRequest
{
    public string Message { get; set; } = string.Empty;
    public string? LessonContext { get; set; }
}

public class AiAssistantResponse
{
    public string Reply { get; set; } = string.Empty;
    public bool UsedAiProvider { get; set; }
}
