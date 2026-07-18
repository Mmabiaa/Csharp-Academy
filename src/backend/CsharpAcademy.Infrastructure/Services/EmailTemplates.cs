namespace CsharpAcademy.Infrastructure.Services;

public static class EmailTemplates
{
    public static string GetPasswordResetOtpTemplate(string userName, string otp)
    {
        return $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Password Reset OTP</title>
</head>
<body style=""margin: 0; padding: 0; font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif; background-color: #f4f7fa;"">
    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #f4f7fa; padding: 40px 20px;"">
        <tr>
            <td align=""center"">
                <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #ffffff; border-radius: 12px; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); overflow: hidden;"">
                    <!-- Header -->
                    <tr>
                        <td style=""background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); padding: 40px 30px; text-align: center;"">
                            <h1 style=""margin: 0; color: #ffffff; font-size: 28px; font-weight: 600; letter-spacing: -0.5px;"">
                                🔐 C# Academy
                            </h1>
                            <p style=""margin: 10px 0 0 0; color: #e0e7ff; font-size: 14px;"">
                                Password Reset Request
                            </p>
                        </td>
                    </tr>
                    
                    <!-- Content -->
                    <tr>
                        <td style=""padding: 40px 30px;"">
                            <p style=""margin: 0 0 20px 0; color: #1f2937; font-size: 16px; line-height: 1.6;"">
                                Hello <strong>{userName}</strong>,
                            </p>
                            
                            <p style=""margin: 0 0 30px 0; color: #4b5563; font-size: 15px; line-height: 1.6;"">
                                We received a request to reset your password. Use the One-Time Password (OTP) below to complete the process:
                            </p>
                            
                            <!-- OTP Box -->
                            <table width=""100%"" cellpadding=""0"" cellspacing=""0"">
                                <tr>
                                    <td align=""center"" style=""padding: 20px 0;"">
                                        <div style=""background: linear-gradient(135deg, #f0f4ff 0%, #e5e7ff 100%); border: 2px dashed #667eea; border-radius: 12px; padding: 25px; display: inline-block;"">
                                            <p style=""margin: 0 0 10px 0; color: #6b7280; font-size: 13px; text-transform: uppercase; letter-spacing: 1px; font-weight: 600;"">
                                                Your OTP Code
                                            </p>
                                            <p style=""margin: 0; color: #667eea; font-size: 36px; font-weight: 700; letter-spacing: 8px; font-family: 'Courier New', monospace;"">
                                                {otp}
                                            </p>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- Warning Box -->
                            <div style=""background-color: #fef3c7; border-left: 4px solid #f59e0b; padding: 15px 20px; margin: 30px 0; border-radius: 6px;"">
                                <p style=""margin: 0; color: #92400e; font-size: 14px; line-height: 1.5;"">
                                    ⏱️ <strong>Important:</strong> This OTP will expire in <strong>15 minutes</strong>. Do not share this code with anyone.
                                </p>
                            </div>
                            
                            <p style=""margin: 0 0 20px 0; color: #4b5563; font-size: 15px; line-height: 1.6;"">
                                If you didn't request a password reset, please ignore this email or contact our support team if you have concerns.
                            </p>
                            
                            <p style=""margin: 0; color: #4b5563; font-size: 15px; line-height: 1.6;"">
                                Best regards,<br>
                                <strong style=""color: #667eea;"">The C# Academy Team</strong>
                            </p>
                        </td>
                    </tr>
                    
                    <!-- Footer -->
                    <tr>
                        <td style=""background-color: #f9fafb; padding: 30px; text-align: center; border-top: 1px solid #e5e7eb;"">
                            <p style=""margin: 0 0 10px 0; color: #9ca3af; font-size: 13px; line-height: 1.5;"">
                                This is an automated message from C# Academy.<br>
                                Please do not reply to this email.
                            </p>
                            <p style=""margin: 0; color: #d1d5db; font-size: 12px;"">
                                © {DateTime.UtcNow.Year} C# Academy. All rights reserved.
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
    }

    public static string GetPasswordResetOtpPlainText(string userName, string otp)
    {
        return $@"
Hello {userName},

We received a request to reset your password for your C# Academy account.

Your One-Time Password (OTP) is: {otp}

This OTP will expire in 15 minutes. Do not share this code with anyone.

If you didn't request a password reset, please ignore this email or contact our support team.

Best regards,
The C# Academy Team

---
This is an automated message. Please do not reply to this email.
© {DateTime.UtcNow.Year} C# Academy. All rights reserved.
";
    }
}
