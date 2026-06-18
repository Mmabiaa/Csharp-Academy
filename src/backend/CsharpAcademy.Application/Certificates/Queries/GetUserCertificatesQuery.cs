using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Certificates.Queries;

public record GetUserCertificatesQuery(int UserId) : IRequest<List<CertificateDto>>;

public class CertificateDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string CertificateCode { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; }
}

public class GetUserCertificatesQueryHandler : IRequestHandler<GetUserCertificatesQuery, List<CertificateDto>>
{
    private readonly ICertificateRepository _certificateRepository;

    public GetUserCertificatesQueryHandler(ICertificateRepository certificateRepository)
    {
        _certificateRepository = certificateRepository;
    }

    public async Task<List<CertificateDto>> Handle(GetUserCertificatesQuery request, CancellationToken cancellationToken)
    {
        var certs = await _certificateRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        return certs.Select(c => new CertificateDto
        {
            Id = c.Id,
            CourseId = c.CourseId,
            CourseTitle = c.Course.Title,
            CertificateCode = c.CertificateCode,
            IssuedAt = c.IssuedAt
        }).ToList();
    }
}
