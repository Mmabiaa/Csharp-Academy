using CsharpAcademy.Application.Common.Interfaces;
using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Challenges.Commands;

public record SubmitChallengeCommand(int UserId, int ChallengeId, string Code) : IRequest<ChallengeResultDto>;

public class ChallengeResultDto
{
    public bool Passed { get; set; }
    public string Output { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public int XpEarned { get; set; }
}

public class SubmitChallengeCommandHandler : IRequestHandler<SubmitChallengeCommand, ChallengeResultDto>
{
    private readonly ICodingChallengeRepository _challengeRepo;
    private readonly ICodeExecutionService _codeExecution;
    private readonly IGamificationService _gamification;

    public SubmitChallengeCommandHandler(
        ICodingChallengeRepository challengeRepo,
        ICodeExecutionService codeExecution,
        IGamificationService gamification)
    {
        _challengeRepo = challengeRepo;
        _codeExecution = codeExecution;
        _gamification = gamification;
    }

    public async Task<ChallengeResultDto> Handle(SubmitChallengeCommand request, CancellationToken cancellationToken)
    {
        var challenge = await _challengeRepo.GetByIdAsync(request.ChallengeId, cancellationToken)
            ?? throw new KeyNotFoundException("Challenge not found.");

        var result = await _codeExecution.ExecuteAsync(request.Code, cancellationToken);
        if (!result.Success)
        {
            return new ChallengeResultDto { Passed = false, Output = result.Error ?? "", Message = "Code failed to run." };
        }

        var passed = result.Output.Trim() == challenge.ExpectedOutput.Trim();
        var xp = 0;
        if (passed && !await _challengeRepo.HasUserCompletedAsync(request.UserId, request.ChallengeId, cancellationToken))
        {
            await _challengeRepo.RecordCompletionAsync(request.UserId, request.ChallengeId, cancellationToken);
            xp = await _gamification.AwardChallengeXpAsync(request.UserId, request.ChallengeId, challenge.XpReward, cancellationToken);
        }

        return new ChallengeResultDto
        {
            Passed = passed,
            Output = result.Output,
            Message = passed ? "Challenge solved!" : $"Expected: {challenge.ExpectedOutput}",
            XpEarned = xp
        };
    }
}
