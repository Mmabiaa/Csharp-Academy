using System.Text.Json;
using CsharpAcademy.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CsharpAcademy.Infrastructure.Services;

public class AiQuizGenerationService : IAiQuizGenerationService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<AiQuizGenerationService> _logger;

    public AiQuizGenerationService(
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory,
        ILogger<AiQuizGenerationService> logger)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<GeneratedQuizData> GenerateAsync(
        string lessonTitle, string lessonContent, int questionCount, CancellationToken cancellationToken = default)
    {
        var apiKey = GeminiClient.GetApiKey(_configuration);
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _logger.LogWarning("GEMINI_API_KEY is not set — using fallback quiz.");
            return GenerateFallbackQuiz(lessonTitle, questionCount);
        }

        try
        {
            return await CallGeminiAsync(apiKey, lessonTitle, lessonContent, questionCount, cancellationToken);
        }
        catch (GeminiException ex)
        {
            _logger.LogWarning("Gemini quiz generation failed: {Message}", ex.Message);
            return GenerateFallbackQuiz(lessonTitle, questionCount);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "AI quiz generation failed, using fallback.");
            return GenerateFallbackQuiz(lessonTitle, questionCount);
        }
    }

    private async Task<GeneratedQuizData> CallGeminiAsync(
        string apiKey, string lessonTitle, string lessonContent, int questionCount, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient();

        var systemPrompt = """
            You generate C# programming quiz questions as JSON only.
            Return valid JSON with this shape:
            {"title":"Quiz Title","questions":[{"text":"...","type":"MultipleChoice|TrueFalse|FillInTheBlank|OutputPrediction","correctAnswer":"for text types","options":[{"text":"...","isCorrect":true/false}]}]}
            Include a mix of question types. For FillInTheBlank set correctAnswer. For OutputPrediction include code in the question text and use MultipleChoice-style options.
            """;

        var userPrompt = $"Lesson: {lessonTitle}\n\nContent:\n{lessonContent}\n\nGenerate {questionCount} questions.";

        var content = await GeminiClient.ChatAsync(
            client,
            apiKey,
            GeminiClient.GetModel(_configuration),
            systemPrompt,
            userPrompt,
            maxTokens: 2000,
            jsonResponse: true,
            cancellationToken: cancellationToken);

        var quiz = JsonSerializer.Deserialize<GeneratedQuizData>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? throw new InvalidOperationException("Failed to parse AI quiz JSON.");

        quiz.Title ??= $"{lessonTitle} Quiz";
        return quiz;
    }

    private static GeneratedQuizData GenerateFallbackQuiz(string lessonTitle, int questionCount)
    {
        return new GeneratedQuizData
        {
            Title = $"{lessonTitle} Quiz",
            Questions = new List<GeneratedQuestionData>
            {
                new GeneratedQuestionData
                {
                    Text = $"Which statement about '{lessonTitle}' is correct?",
                    Type = "MultipleChoice",
                    Options = new List<GeneratedOptionData>
                    {
                        new() { Text = "It is an important C# concept", IsCorrect = true },
                        new() { Text = "It is unrelated to programming", IsCorrect = false },
                        new() { Text = "It only applies to Python", IsCorrect = false }
                    }
                },
                new GeneratedQuestionData
                {
                    Text = "C# is a statically typed language.",
                    Type = "TrueFalse",
                    Options = new List<GeneratedOptionData>
                    {
                        new() { Text = "True", IsCorrect = true },
                        new() { Text = "False", IsCorrect = false }
                    }
                },
                new GeneratedQuestionData
                {
                    Text = "Fill in the blank: C# code runs on the ___ platform.",
                    Type = "FillInTheBlank",
                    CorrectAnswer = ".NET"
                }
            }.Take(questionCount).ToList()
        };
    }
}
