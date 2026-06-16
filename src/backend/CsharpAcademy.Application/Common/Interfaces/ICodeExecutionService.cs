namespace CsharpAcademy.Application.Common.Interfaces;

public interface ICodeExecutionService
{
    Task<CodeExecutionResult> ExecuteAsync(string code, CancellationToken cancellationToken = default);
}

public class CodeExecutionResult
{
    public bool Success { get; set; }
    public string Output { get; set; } = string.Empty;
    public string? Error { get; set; }
}
