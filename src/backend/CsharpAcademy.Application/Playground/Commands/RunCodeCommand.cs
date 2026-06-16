using CsharpAcademy.Application.Common.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Playground.Commands;

public record RunCodeCommand(string Code) : IRequest<CodeExecutionResult>;

public class RunCodeCommandHandler : IRequestHandler<RunCodeCommand, CodeExecutionResult>
{
    private readonly ICodeExecutionService _codeExecutionService;

    public RunCodeCommandHandler(ICodeExecutionService codeExecutionService)
    {
        _codeExecutionService = codeExecutionService;
    }

    public Task<CodeExecutionResult> Handle(RunCodeCommand request, CancellationToken cancellationToken)
    {
        return _codeExecutionService.ExecuteAsync(request.Code, cancellationToken);
    }
}
