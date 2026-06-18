using CsharpAcademy.Application.Common.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Reflection;
using System.IO;
using System.Threading;

namespace CsharpAcademy.Infrastructure.Services;

public class RoslynCodeExecutionService : ICodeExecutionService
{
    private static readonly string[] BlockedPatterns =
    [
        "System.IO", "System.Net", "System.Diagnostics.Process",
        "System.Reflection", "File.", "Directory.", "Console.Read"
    ];

    private static readonly SemaphoreSlim _executionSemaphore = new SemaphoreSlim(1, 1);

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

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(TimeSpan.FromSeconds(5));

        try
        {
            await _executionSemaphore.WaitAsync(cts.Token);
            var sw = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(sw);

            try
            {
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
                var consoleOutput = sw.ToString();

                // If code printed to console, use that. Otherwise use the return value.
                var finalOutput = !string.IsNullOrWhiteSpace(consoleOutput)
                    ? consoleOutput
                    : (result?.ToString() ?? "(no output)");

                return new CodeExecutionResult { Success = true, Output = finalOutput.TrimEnd() };
            }
            finally
            {
                Console.SetOut(originalOut);
                _executionSemaphore.Release();
            }
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
