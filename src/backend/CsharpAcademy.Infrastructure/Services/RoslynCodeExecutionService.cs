using CsharpAcademy.Application.Common.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Reflection;

namespace CsharpAcademy.Infrastructure.Services;

public class RoslynCodeExecutionService : ICodeExecutionService
{
    private static readonly string[] BlockedPatterns =
    [
        "System.IO", "System.Net", "System.Diagnostics.Process",
        "System.Reflection", "File.", "Directory.", "Console.Read"
    ];

    public async Task<CodeExecutionResult> ExecuteAsync(string code, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return new CodeExecutionResult { Success = false, Error = "Code cannot be empty." };
        }

        foreach (var pattern in BlockedPatterns)
        {
            if (code.Contains(pattern, StringComparison.OrdinalIgnoreCase))
            {
                return new CodeExecutionResult
                {
                    Success = false,
                    Error = $"For safety, '{pattern}' is not allowed in the playground."
                };
            }
        }

        try
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(TimeSpan.FromSeconds(5));

            var references = new[]
            {
                typeof(object).Assembly,
                typeof(Console).Assembly,
                typeof(Enumerable).Assembly,
                typeof(List<>).Assembly,
                Assembly.Load("System.Runtime"),
                Assembly.Load("System.Collections")
            };

            var options = ScriptOptions.Default
                .AddReferences(references)
                .AddImports(
                    "System",
                    "System.Linq",
                    "System.Collections.Generic",
                    "System.Collections"
                );

            var wrapped = code.Contains("return ", StringComparison.Ordinal) || code.Contains("Console.Write", StringComparison.Ordinal)
                ? code
                : $"return ({code});";

            var result = await CSharpScript.EvaluateAsync<object?>(wrapped, options, cancellationToken: cts.Token);
            var output = result?.ToString() ?? "(no output)";

            return new CodeExecutionResult { Success = true, Output = output };
        }
        catch (CompilationErrorException ex)
        {
            var errors = string.Join(Environment.NewLine, ex.Diagnostics.Select(d => d.ToString()));
            return new CodeExecutionResult { Success = false, Error = errors };
        }
        catch (OperationCanceledException)
        {
            return new CodeExecutionResult { Success = false, Error = "Execution timed out (5 second limit)." };
        }
        catch (Exception ex)
        {
            return new CodeExecutionResult { Success = false, Error = ex.Message };
        }
    }
}
