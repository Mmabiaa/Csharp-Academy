using CsharpAcademy.Application.Auth.Dtos;
using MediatR;

namespace CsharpAcademy.Application.Auth.Commands;

public record VerifyOtpCommand(string Email, string Otp) : IRequest<VerifyOtpResponseDto>;
