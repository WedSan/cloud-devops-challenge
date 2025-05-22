using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IDentalAnalysisService
    {
        Task<DentalAnalysis> CreateDentalAnalysisAsync(int userId, DateTime analysisDate, float probabilityProblem, List<int> monitoringDataIdList);

        Task<IEnumerable<DentalAnalysis>> GetDentalAnalysisAsync(int pageNumber, int pageSize);

        Task<DentalAnalysis> GetDentalAnalysisByIdAsync(int dentalAnalysisId);

        Task<DentalAnalysis> UpdateDentalAnalysisUserAsync(int dentalAnalysisId, float newProbabilityProblem);

        Task DeleteDentalAnalysisAsync(int dentalAnalysisId);
    }
}
