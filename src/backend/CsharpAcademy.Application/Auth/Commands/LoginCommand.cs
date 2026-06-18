using CsharpAcademy.Application.Auth.Dtos;
using MediatR;

namespace CsharpAcademy.Application.Auth.Commands;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;
