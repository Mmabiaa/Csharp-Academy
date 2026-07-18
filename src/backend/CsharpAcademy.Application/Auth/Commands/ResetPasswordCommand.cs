using MediatR;

namespace CsharpAcademy.Application.Auth.Commands;

public record ResetPasswordCommand(string Email, string Otp, string NewPassword) : IRequest<Unit>;
