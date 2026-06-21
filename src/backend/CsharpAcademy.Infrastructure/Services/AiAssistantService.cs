using CsharpAcademy.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CsharpAcademy.Infrastructure.Services;

public class AiAssistantService : IAiAssistantService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<AiAssistantService> _logger;

    public AiAssistantService(
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory,
        ILogger<AiAssistantService> logger)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<AiAssistantResponse> GetResponseAsync(AiAssistantRequest request, CancellationToken cancellationToken = default)
    {
        var apiKey = GeminiClient.GetApiKey(_configuration);
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _logger.LogWarning("GEMINI_API_KEY is not set — using offline tutor.");
            return OfflineResponse(request);
        }

        try
        {
            var client = _httpClientFactory.CreateClient();
            var systemPrompt = "You are a helpful C# programming tutor for beginners. " +
                "Give clear, concise explanations with short code examples when helpful. " +
                (request.LessonContext is not null ? $"The student is studying: {request.LessonContext}. " : "");

            var reply = await GeminiClient.ChatAsync(
                client,
                apiKey,
                GeminiClient.GetModel(_configuration),
                systemPrompt,
                request.Message,
                maxTokens: 1800,
                cancellationToken: cancellationToken);

            return new AiAssistantResponse { Reply = reply, UsedAiProvider = true };
        }
        catch (GeminiException ex)
        {
            _logger.LogWarning("Gemini error: {Message}", ex.Message);
            return new AiAssistantResponse
            {
                Reply = $"⚠️ **AI unavailable:** {ex.Message}\n\n---\n\n{GetLocalResponse(request.Message, request.LessonContext)}",
                UsedAiProvider = false,
                Error = ex.Message
            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Gemini request failed unexpectedly.");
            return new AiAssistantResponse
            {
                Reply = $"⚠️ **AI connection error:** {ex.Message}\n\n---\n\n{GetLocalResponse(request.Message, request.LessonContext)}",
                UsedAiProvider = false,
                Error = ex.Message
            };
        }
    }

    private static AiAssistantResponse OfflineResponse(AiAssistantRequest request) => new()
    {
        Reply = GetLocalResponse(request.Message, request.LessonContext),
        UsedAiProvider = false
    };

    private static string GetLocalResponse(string message, string? lessonContext)
    {
        var lower = message.ToLowerInvariant();
        var context = lessonContext is not null ? $"\n\nYou're currently studying **{lessonContext}**." : "";

        if (lower.Contains("variable") || lower.Contains("int") || lower.Contains("string"))
        {
            return "In C#, you declare variables with a type and name:\n\n```csharp\nint age = 25;\nstring name = \"Alice\";\n```\n\nThe type tells the compiler what kind of data the variable holds." + context;
        }

        if (lower.Contains("class") || lower.Contains("object") || lower.Contains("oop"))
        {
            return "A **class** is a blueprint for creating objects in C#:\n\n```csharp\npublic class Person\n{\n    public string Name { get; set; }\n    public int Age { get; set; }\n}\n\nvar person = new Person { Name = \"Bob\", Age = 30 };\n```" + context;
        }

        if (lower.Contains("loop") || lower.Contains("for") || lower.Contains("while"))
        {
            return "C# supports `for`, `while`, and `foreach` loops:\n\n```csharp\nfor (int i = 0; i < 5; i++)\n    Console.WriteLine(i);\n\nforeach (var item in items)\n    Console.WriteLine(item);\n```" + context;
        }

        if (lower.Contains("hello") || lower.Contains("hi"))
        {
            return "Hello! I'm your C# learning assistant. Ask me about variables, classes, loops, or any C# concept!" + context;
        }

        return "I'm here to help you learn C#! Try asking about:\n- Variables and data types\n- Classes and objects\n- Loops and conditionals\n- Methods and functions" + context;
    }
}
