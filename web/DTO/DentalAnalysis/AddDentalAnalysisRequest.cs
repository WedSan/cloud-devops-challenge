using Domain.Entities;

namespace web.DTO.DentalAnalysis
{
    public record AddDentalAnalysisRequest(int UserId, DateTime AnalysisDate, float ProbabilityProblem, List<int> MonitoringDataIdList)
    {
    }
}
