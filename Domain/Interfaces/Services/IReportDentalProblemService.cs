using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IReportDentalProblemService
    {
        Task<ReportDentalProblem> CreateReportDentalProblemAsync(int monitoringDataId, string problem);

        Task<IEnumerable<ReportDentalProblem>> GetReportDentalProblemAsync(int pageNumber, int pageSize);

        Task<ReportDentalProblem> GetReportDentalProblemByIdAsync(int reportDentalProblemId);

        Task<ReportDentalProblem> UpdateReportDentalProblemAsync(int reportDentalproblemId, string problem);

        Task DeleteReportDentalProblemaAsync(int reportDentalproblemId);
    }
}
