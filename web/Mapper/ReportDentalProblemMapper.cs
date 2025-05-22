using Domain.Entities;
using web.DTO.ReportDentalProblem;

namespace web.Mapper
{
    public class ReportDentalProblemMapper
    {
        public static ReportDentalProblemResponse ToDto(ReportDentalProblem reportDentalProblem)
        {
            if (reportDentalProblem == null)
                return null;

            return new ReportDentalProblemResponse
            (
                reportDentalProblem.Id,
                reportDentalProblem.Problem
            );
        }

        public static IEnumerable<ReportDentalProblemResponse> ToDto(IEnumerable<ReportDentalProblem> reportDentalProblems)
        {
            return reportDentalProblems.Select(ToDto);
        }

        public static ReportDentalProblem ToEntity(AddReportDentalProblemRequest request, MonitoringData monitoringData)
        {
            return new ReportDentalProblem
            {
                Problem = request.Problem,
                MonitoringData = monitoringData
            };
        }

        public static ReportDentalProblem ToEntity(UpdateReportDentalProblemRequest request, ReportDentalProblem existingReport)
        {
            existingReport.Problem = request.Problem;
            return existingReport;
        }
    }
}
