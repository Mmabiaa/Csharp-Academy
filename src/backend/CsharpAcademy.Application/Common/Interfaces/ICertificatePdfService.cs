namespace CsharpAcademy.Application.Common.Interfaces;

public interface ICertificatePdfService
{
    Task<byte[]> GeneratePdfAsync(string studentName, string courseTitle, string certificateCode, DateTime issuedAt, CancellationToken cancellationToken = default);
}
