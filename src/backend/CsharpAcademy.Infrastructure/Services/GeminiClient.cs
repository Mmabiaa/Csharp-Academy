using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace CsharpAcademy.Infrastructure.Services;

internal static class GeminiClient
{
    private const string BaseUrl = "https://generativelanguage.googleapis.com/v1beta/models";

    public static string? GetApiKey(IConfiguration configuration) =>
        configuration["GEMINI_API_KEY"]?.Trim();

    public static string GetModel(IConfiguration configuration) =>
        configuration["GEMINI_MODEL"]?.Trim() ?? "gemini-2.0-flash";

    public static async Task<string> ChatAsync(
        HttpClient client,
        string apiKey,
        string model,
        string systemPrompt,
        string userMessage,
        int maxTokens = 500,
        bool jsonResponse = false,
        CancellationToken cancellationToken = default)
    {
        var url = $"{BaseUrl}/{model}:generateContent?key={Uri.EscapeDataString(apiKey)}";

        var generationConfig = new Dictionary<string, object> { ["maxOutputTokens"] = maxTokens };
        if (jsonResponse)
        {
            generationConfig["responseMimeType"] = "application/json";
        }

        var body = new
        {
            systemInstruction = new { parts = new[] { new { text = systemPrompt } } },
            contents = new[]
            {
                new { role = "user", parts = new[] { new { text = userMessage } } }
            },
            generationConfig
        };

        var response = await client.PostAsync(
            url,
            new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"),
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new GeminiException(await ParseErrorAsync(response, cancellationToken));
        }

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        using var doc = JsonDocument.Parse(json);

        if (!doc.RootElement.TryGetProperty("candidates", out var candidates) ||
            candidates.GetArrayLength() == 0)
        {
            throw new GeminiException("Gemini returned no candidates. The response may have been blocked by safety filters.");
        }

        var text = candidates[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        return text ?? "I couldn't generate a response.";
    }

    private static async Task<string> ParseErrorAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var status = (int)response.StatusCode;
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        try
        {
            using var doc = JsonDocument.Parse(body);
            if (doc.RootElement.TryGetProperty("error", out var error))
            {
                var message = error.TryGetProperty("message", out var msg) ? msg.GetString() : null;
                var statusCode = error.TryGetProperty("status", out var s) ? s.GetString() : null;

                return status switch
                {
                    400 when statusCode == "INVALID_ARGUMENT" =>
                        $"Gemini request error (400). Check GEMINI_MODEL in .env. {message}",
                    403 => $"Gemini access denied (403). Verify GEMINI_API_KEY at https://aistudio.google.com/apikey. {message}",
                    429 => $"Gemini rate limit reached (429). Wait a moment and try again. {message}",
                    _ => $"Gemini API error ({status}): {message ?? body}"
                };
            }
        }
        catch
        {
            // fall through
        }

        return status switch
        {
            403 => "Gemini authentication failed (403). Verify GEMINI_API_KEY in src/backend/.env.",
            429 => "Gemini rate limit exceeded (429). Try again shortly.",
            _ => $"Gemini API error ({status}): {body}"
        };
    }
}

internal class GeminiException : Exception
{
    public GeminiException(string message) : base(message) { }
}
