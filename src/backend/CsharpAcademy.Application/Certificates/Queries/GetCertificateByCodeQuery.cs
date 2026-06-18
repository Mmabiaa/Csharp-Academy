using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Certificates.Queries;

public record GetCertificateByCodeQuery(string Code) : IRequest<CertificateVerificationDto?>;

public class CertificateVerificationDto
{
    public string CertificateCode { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
}

public class GetCertificateByCodeQueryHandler : IRequestHandler<GetCertificateByCodeQuery, CertificateVerificationDto?>
{
    private readonly ICertificateRepository _certificateRepository;

    public GetCertificateByCodeQueryHandler(ICertificateRepository certificateRepository)
    {
        _certificateRepository = certificateRepository;
    }

    public async Task<CertificateVerificationDto?> Handle(GetCertificateByCodeQuery request, CancellationToken cancellationToken)
    {
        var cert = await _certificateRepository.GetByCodeAsync(request.Code, cancellationToken);
        if (cert is null)
        {
            return null;
        }

        return new CertificateVerificationDto
        {
            CertificateCode = cert.CertificateCode,
            StudentName = $"{cert.User.FirstName} {cert.User.LastName}",
            CourseTitle = cert.Course.Title,
            IssuedAt = cert.IssuedAt
        };
    }
}
