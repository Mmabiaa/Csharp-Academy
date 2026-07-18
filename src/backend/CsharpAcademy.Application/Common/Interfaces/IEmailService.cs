namespace CsharpAcademy.Application.Common.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetOtpAsync(string toEmail, string otp, string userName);
}
