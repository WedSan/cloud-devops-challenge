using Domain.Entities;
using Infrastructure.EntityConfiguration;
using web.DTO.ReportDentalProblem;
using web.DTO.User;

namespace web.DTO.DataMonitoring
{
    public record MonitoringDataResponse(
        
    int Id,
        
    UserEntityResponse User,

    List<int> ProblemsId,

    List<int> DentalAnalysesId, 

    DateTime RegistrationDate)
    {

    }
    
}
