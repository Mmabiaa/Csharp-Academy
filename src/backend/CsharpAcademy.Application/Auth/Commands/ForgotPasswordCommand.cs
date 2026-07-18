using MediatR;

namespace CsharpAcademy.Application.Auth.Commands;

public record ForgotPasswordCommand(string Email) : IRequest<Unit>;
