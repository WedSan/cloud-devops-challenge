using Domain.Entities;
using web.DTO.DataMonitoring;
using web.DTO.ReportDentalProblem;

namespace web.Mapper
{
    public class MonitoringDataMapper
    {
        public static List<ReportDentalProblem> MapToDentalProblem(IEnumerable<string> dentalProblems)
        {
            List<ReportDentalProblem> reportDentalProblems = new List<ReportDentalProblem>();
            foreach (string problem in dentalProblems)
            {
                reportDentalProblems.Add(new ReportDentalProblem { Problem = problem});
            }
            return reportDentalProblems;
        }

        public static MonitoringDataResponse ToDto(MonitoringData data)
        {
            return new MonitoringDataResponse(
                data.Id,                                  
                UserMapper.ToDTO(data.User),              
                data.Problems.Select(p => p.Id).ToList(),
                DentalAnalysesId: data.DentalAnalyses?.Select(da => da.Id).ToList(),
                data.RegistrationDate                   
                );


        }

        public static IEnumerable<MonitoringDataResponse> ToDto(IEnumerable<MonitoringData> dataList)
        {
            return dataList.Select(md => ToDto(md));


        }
    }
}
