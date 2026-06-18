using CsharpAcademy.Application.Auth.Dtos;
using MediatR;

namespace CsharpAcademy.Application.Auth.Commands;

public record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Role = "Student") : IRequest<AuthResponseDto>;
