using System.Net.Http.Headers;
using System.Text;
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
        var apiKey = OpenAiClient.GetApiKey(_configuration);
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _logger.LogWarning("OPENAI_API_KEY is not set — using fallback quiz.");
            return GenerateFallbackQuiz(lessonTitle, questionCount);
        }

        try
        {
            return await CallOpenAiAsync(apiKey, lessonTitle, lessonContent, questionCount, cancellationToken);
        }
        catch (OpenAiException ex)
        {
            _logger.LogWarning("OpenAI quiz generation failed: {Message}", ex.Message);
            return GenerateFallbackQuiz(lessonTitle, questionCount);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "AI quiz generation failed, using fallback.");
            return GenerateFallbackQuiz(lessonTitle, questionCount);
        }
    }

    private async Task<GeneratedQuizData> CallOpenAiAsync(
        string apiKey, string lessonTitle, string lessonContent, int questionCount, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var systemPrompt = """
            You generate C# programming quiz questions as JSON only.
            Return valid JSON with this shape:
            {"title":"Quiz Title","questions":[{"text":"...","type":"MultipleChoice|TrueFalse|FillInTheBlank|OutputPrediction","correctAnswer":"for text types","options":[{"text":"...","isCorrect":true/false}]}]}
            Include a mix of question types. For FillInTheBlank set correctAnswer. For OutputPrediction include code in the question text and use MultipleChoice-style options.
            """;

        var userPrompt = $"Lesson: {lessonTitle}\n\nContent:\n{lessonContent}\n\nGenerate {questionCount} questions.";

        var body = new
        {
            model = OpenAiClient.GetModel(_configuration),
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userPrompt }
            },
            response_format = new { type = "json_object" },
            max_tokens = 2000
        };

        var response = await client.PostAsync(
            "https://api.openai.com/v1/chat/completions",
            new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"),
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);
            _logger.LogWarning("OpenAI quiz API returned {Status}: {Body}", (int)response.StatusCode, errorBody);
            throw new OpenAiException($"OpenAI returned {(int)response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        using var doc = JsonDocument.Parse(json);
        var content = doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString()
            ?? throw new InvalidOperationException("Empty AI response.");

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
