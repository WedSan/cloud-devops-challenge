namespace web.DTO.DataMonitoring
{
    public record AddDataMonitoringRequest(int UserId, List<string> DentalProblems)
    {
    }
}
