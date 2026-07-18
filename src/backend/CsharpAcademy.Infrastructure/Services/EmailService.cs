using CsharpAcademy.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CsharpAcademy.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly IConfiguration _configuration;

    public EmailService(ILogger<EmailService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task SendPasswordResetOtpAsync(string toEmail, string otp, string userName)
    {
        try
        {
            var smtpHost = _configuration["SMTP_HOST"] ?? "smtp.gmail.com";
            var smtpPort = int.Parse(_configuration["SMTP_PORT"] ?? "587");
            var smtpUsername = _configuration["SMTP_USERNAME"];
            var smtpPassword = _configuration["SMTP_PASSWORD"];
            var fromEmail = _configuration["SMTP_FROM_EMAIL"] ?? "noreply@csharpacademy.com";
            var fromName = _configuration["SMTP_FROM_NAME"] ?? "C# Academy";

            // Check if SMTP is configured
            if (string.IsNullOrEmpty(smtpUsername) || string.IsNullOrEmpty(smtpPassword))
            {
                _logger.LogWarning(
                    "SMTP not configured. OTP for {Email}: {Otp} (User: {UserName})",
                    toEmail, otp, userName);
                return;
            }

            // Create email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, fromEmail));
            message.To.Add(new MailboxAddress(userName, toEmail));
            message.Subject = "🔐 Password Reset OTP - C# Academy";

            // Build email body with both HTML and plain text
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = EmailTemplates.GetPasswordResetOtpTemplate(userName, otp),
                TextBody = EmailTemplates.GetPasswordResetOtpPlainText(userName, otp)
            };

            message.Body = bodyBuilder.ToMessageBody();

            // Send email via SMTP
            using var client = new SmtpClient();
            
            // Configure timeout and server certificate validation
            client.Timeout = 30000; // 30 seconds timeout
            client.ServerCertificateValidationCallback = (s, c, h, e) => true; // Accept all certificates (use cautiously)
            
            // Determine security option based on port
            var securityOption = smtpPort == 465 
                ? SecureSocketOptions.SslOnConnect 
                : SecureSocketOptions.StartTls;
            
            await client.ConnectAsync(smtpHost, smtpPort, securityOption);
            await client.AuthenticateAsync(smtpUsername, smtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation(
                "Password reset OTP email sent successfully to {Email}",
                toEmail);
        }
        catch (MailKit.Net.Smtp.SmtpProtocolException ex)
        {
            _logger.LogWarning(ex, 
                "SMTP protocol error sending password reset OTP to {Email}. Check SMTP configuration. Logging OTP for development: {Otp}",
                toEmail, otp);
            // In development, allow the flow to continue even if email fails
            // The OTP is logged for manual testing
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, 
                "Failed to send password reset OTP email to {Email}. Logging OTP for development: {Otp}",
                toEmail, otp);
            // In development, allow the flow to continue even if email fails
        }
    }
}
