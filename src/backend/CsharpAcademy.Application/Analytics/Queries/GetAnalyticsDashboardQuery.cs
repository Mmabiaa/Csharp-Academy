using CsharpAcademy.Domain.Interfaces;
using MediatR;

namespace CsharpAcademy.Application.Analytics.Queries;

public record GetAnalyticsDashboardQuery(int? TeacherId = null) : IRequest<AnalyticsSnapshot>;

public class GetAnalyticsDashboardQueryHandler : IRequestHandler<GetAnalyticsDashboardQuery, AnalyticsSnapshot>
{
    private readonly IAnalyticsRepository _analyticsRepository;

    public GetAnalyticsDashboardQueryHandler(IAnalyticsRepository analyticsRepository)
    {
        _analyticsRepository = analyticsRepository;
    }

    public Task<AnalyticsSnapshot> Handle(GetAnalyticsDashboardQuery request, CancellationToken cancellationToken)
    {
        if (request.TeacherId.HasValue)
        {
            return _analyticsRepository.GetTeachingSnapshotAsync(request.TeacherId.Value, cancellationToken);
        }
        return _analyticsRepository.GetSnapshotAsync(cancellationToken);
    }
}
