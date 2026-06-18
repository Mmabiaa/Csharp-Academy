using CsharpAcademy.Application.Common.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Assistant.Commands;

public record AskAssistantCommand(string Message, string? LessonContext) : IRequest<AiAssistantResponse>;

public class AskAssistantCommandHandler : IRequestHandler<AskAssistantCommand, AiAssistantResponse>
{
    private readonly IAiAssistantService _assistantService;

    public AskAssistantCommandHandler(IAiAssistantService assistantService)
    {
        _assistantService = assistantService;
    }

    public Task<AiAssistantResponse> Handle(AskAssistantCommand request, CancellationToken cancellationToken)
    {
        return _assistantService.GetResponseAsync(new AiAssistantRequest
        {
            Message = request.Message,
            LessonContext = request.LessonContext
        }, cancellationToken);
    }
}
