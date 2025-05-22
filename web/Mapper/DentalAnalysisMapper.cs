using Domain.Entities;
using web.DTO.DentalAnalysis;

namespace web.Mapper
{
    public class DentalAnalysisMapper
    {
        public static DentalAnalysisResponse ToDTO(DentalAnalysis dentalAnalysis)
        {

            return new DentalAnalysisResponse(
                Id: dentalAnalysis.Id,
                User: dentalAnalysis.User, 
                AnalysisDate: dentalAnalysis.AnalysisDate,
                ProbabilityProblem: dentalAnalysis.ProbabilityProblem,
                MonitoringDataIds: dentalAnalysis.MonitoringDataList?.Select(md => md.Id).ToList() ?? new List<int>()
            );
        }

        public static List<DentalAnalysisResponse> ToDTO(IEnumerable<DentalAnalysis> dentalAnalysis)
        {

            return dentalAnalysis.Select(da =>  ToDTO(da)).ToList();
        }
    }
}
