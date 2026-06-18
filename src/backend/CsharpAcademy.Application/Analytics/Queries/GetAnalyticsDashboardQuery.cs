using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Analytics.Queries;

public record GetAnalyticsDashboardQuery : IRequest<AnalyticsSnapshot>;

public class GetAnalyticsDashboardQueryHandler : IRequestHandler<GetAnalyticsDashboardQuery, AnalyticsSnapshot>
{
    private readonly IAnalyticsRepository _analyticsRepository;

    public GetAnalyticsDashboardQueryHandler(IAnalyticsRepository analyticsRepository)
    {
        _analyticsRepository = analyticsRepository;
    }

    public Task<AnalyticsSnapshot> Handle(GetAnalyticsDashboardQuery request, CancellationToken cancellationToken)
    {
        return _analyticsRepository.GetSnapshotAsync(cancellationToken);
    }
}
