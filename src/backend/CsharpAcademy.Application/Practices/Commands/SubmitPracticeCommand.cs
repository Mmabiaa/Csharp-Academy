using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Practices.Commands;

public record SubmitPracticeCommand(int UserId, int ExerciseId, string Code) : IRequest<PracticeResultDto>;

public class PracticeResultDto
{
    public bool Passed { get; set; }
    public string Output { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public int XpEarned { get; set; }
}

public class SubmitPracticeCommandHandler : IRequestHandler<SubmitPracticeCommand, PracticeResultDto>
{
    private readonly ICodingExerciseRepository _exerciseRepository;
    private readonly ICodeExecutionService _codeExecutionService;
    private readonly IGamificationService _gamificationService;

    public SubmitPracticeCommandHandler(
        ICodingExerciseRepository exerciseRepository,
        ICodeExecutionService codeExecutionService,
        IGamificationService gamificationService)
    {
        _exerciseRepository = exerciseRepository;
        _codeExecutionService = codeExecutionService;
        _gamificationService = gamificationService;
    }

    public async Task<PracticeResultDto> Handle(SubmitPracticeCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId, cancellationToken)
            ?? throw new KeyNotFoundException("Exercise not found.");

        var result = await _codeExecutionService.ExecuteAsync(request.Code, cancellationToken: cancellationToken);
        if (!result.Success)
        {
            return new PracticeResultDto
            {
                Passed = false,
                Output = result.Error ?? "",
                Message = "Code did not run successfully. Check for errors and try again."
            };
        }

        var actual = NormalizeOutput(result.Output);
        var expected = NormalizeOutput(exercise.ExpectedOutput);
        var passed = actual == expected;

        var xpEarned = 0;
        if (passed)
        {
            xpEarned = await _gamificationService.AwardPracticeXpAsync(request.UserId, exercise.Id, cancellationToken);
        }

        return new PracticeResultDto
        {
            Passed = passed,
            Output = result.Output,
            Message = passed
                ? "Correct! Great job completing this practice."
                : $"Not quite. Expected output: \"{exercise.ExpectedOutput}\" but got: \"{result.Output.Trim()}\"",
            XpEarned = xpEarned
        };
    }

    private static string NormalizeOutput(string output) =>
        output.Trim().Replace("\r\n", "\n").Replace("\r", "\n");
}
