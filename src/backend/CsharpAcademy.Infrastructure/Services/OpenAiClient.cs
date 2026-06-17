using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace CsharpAcademy.Infrastructure.Services;

internal static class OpenAiClient
{
    public static string? GetApiKey(IConfiguration configuration) =>
        configuration["OPENAI_API_KEY"]?.Trim();

    public static string GetModel(IConfiguration configuration) =>
        configuration["OPENAI_MODEL"]?.Trim() ?? "gpt-4o-mini";

    public static async Task<string> ChatAsync(
        HttpClient client,
        string apiKey,
        string model,
        string systemPrompt,
        string userMessage,
        int maxTokens = 500,
        CancellationToken cancellationToken = default)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        var body = new
        {
            model,
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = userMessage }
            },
            max_tokens = maxTokens
        };

        var response = await client.PostAsync(
            "https://api.openai.com/v1/chat/completions",
            new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json"),
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new OpenAiException(await ParseErrorAsync(response, cancellationToken));
        }

        var json = await response.Content.ReadAsStringAsync(cancellationToken);
        using var doc = JsonDocument.Parse(json);
        return doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "I couldn't generate a response.";
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
                var type = error.TryGetProperty("type", out var t) ? t.GetString() : null;
                var code = error.TryGetProperty("code", out var c) ? c.GetString() : null;

                return status switch
                {
                    401 => $"OpenAI authentication failed (401). Check that OPENAI_API_KEY in src/backend/.env is valid. {message}",
                    403 => $"OpenAI access denied (403). {message}",
                    429 when code == "insufficient_quota" =>
                        "OpenAI quota exceeded. Add billing at https://platform.openai.com/account/billing or check your usage limits.",
                    429 => $"OpenAI rate limit reached (429). Wait a moment and try again. {message}",
                    _ => $"OpenAI API error ({status}): {message ?? body}"
                };
            }
        }
        catch
        {
            // fall through
        }

        return status switch
        {
            401 => "OpenAI authentication failed (401). Verify OPENAI_API_KEY in src/backend/.env.",
            429 => "OpenAI rate limit or quota exceeded (429). Check billing at https://platform.openai.com/account/billing.",
            _ => $"OpenAI API error ({status}): {body}"
        };
    }
}

internal class OpenAiException : Exception
{
    public OpenAiException(string message) : base(message) { }
}
