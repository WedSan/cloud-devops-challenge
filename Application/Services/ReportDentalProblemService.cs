using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReportDentalProblemService : IReportDentalProblemService
    {
        private readonly IEntityRepository<ReportDentalProblem> _entityRepository;
        private readonly IMonitoringDataService _monitoringDataService;

        public ReportDentalProblemService(IEntityRepository<ReportDentalProblem> entityRepository, IMonitoringDataService monitoringDataService)
        {
            _entityRepository = entityRepository;
            _monitoringDataService = monitoringDataService;
        }

        public async Task<ReportDentalProblem> CreateReportDentalProblemAsync(int monitoringDataId, string problem)
        {
            MonitoringData monitoringData = await _monitoringDataService.GetMonitoringDataByIdAsync(monitoringDataId);

            if (monitoringData == null)
            {
                throw new EntityNotFoundException("Monitoring data not found.");
            }

            var reportDentalProblem = new ReportDentalProblem
            {
                Problem = problem,
                MonitoringData = monitoringData
            };

            await _entityRepository.AddAsync(reportDentalProblem);
            await _entityRepository.SaveChangesAsync();

            return reportDentalProblem;
        }

        public async Task<IEnumerable<ReportDentalProblem>> GetReportDentalProblemAsync(int pageNumber, int pageSize)
        {
            return await _entityRepository.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<ReportDentalProblem> GetReportDentalProblemByIdAsync(int reportDentalProblemId)
        {
            var reportDentalProblem = await _entityRepository.GetByIdAsync(reportDentalProblemId);

            if (reportDentalProblem == null)
            {
                throw new EntityNotFoundException("Report dental problem not found.");
            }

            return reportDentalProblem;
        }

        public async Task<ReportDentalProblem> UpdateReportDentalProblemAsync(int reportDentalProblemId, string problem)
        {
            var reportDentalProblem = await GetReportDentalProblemByIdAsync(reportDentalProblemId);
            reportDentalProblem.Problem = problem;

            _entityRepository.Update(reportDentalProblem);
            await _entityRepository.SaveChangesAsync();

            return reportDentalProblem;
        }

        public async Task DeleteReportDentalProblemaAsync(int reportDentalproblemId)
        {
            var reportDentalProblem = await GetReportDentalProblemByIdAsync(reportDentalproblemId);
            _entityRepository.Remove(reportDentalProblem);
            await _entityRepository.SaveChangesAsync();
        }
    }
}
