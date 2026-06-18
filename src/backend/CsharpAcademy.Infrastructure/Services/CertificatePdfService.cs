using CsharpAcademy.Application.Common.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace CsharpAcademy.Infrastructure.Services;

public class CertificatePdfService : ICertificatePdfService
{
    public CertificatePdfService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public Task<byte[]> GeneratePdfAsync(
        string studentName, string courseTitle, string certificateCode, DateTime issuedAt,
        CancellationToken cancellationToken = default)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(14));

                page.Content().Border(3).BorderColor(Colors.Blue.Medium).Padding(40).Column(col =>
                {
                    col.Spacing(12);
                    col.Item().AlignCenter().Text("C# Academy").FontSize(28).Bold().FontColor(Colors.Blue.Darken2);
                    col.Item().AlignCenter().Text("Certificate of Completion").FontSize(22).SemiBold();
                    col.Item().PaddingVertical(20).AlignCenter().Text("This certifies that").FontSize(14);
                    col.Item().AlignCenter().Text(studentName).FontSize(26).Bold();
                    col.Item().AlignCenter().Text("has successfully completed").FontSize(14);
                    col.Item().AlignCenter().Text(courseTitle).FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
                    col.Item().PaddingTop(30).AlignCenter().Text($"Issued: {issuedAt:MMMM d, yyyy}").FontSize(12);
                    col.Item().AlignCenter().Text($"Verification Code: {certificateCode}").FontSize(11).FontColor(Colors.Grey.Darken1);
                });
            });
        });

        var bytes = document.GeneratePdf();
        return Task.FromResult(bytes);
    }
}
