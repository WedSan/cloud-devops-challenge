namespace web.DTO.DentalAnalysis
{
    public record DentalAnalysisResponse(
          int Id,
          Domain.Entities.User User,               
          DateTime AnalysisDate,
          float ProbabilityProblem,
          List<int> MonitoringDataIds 
      );
}
