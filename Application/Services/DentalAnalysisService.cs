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
    public class DentalAnalysisService : IDentalAnalysisService
    {
        private readonly IEntityRepository<DentalAnalysis> _repository;

        private readonly IMonitoringDataService _monitoringDataService;

        private readonly IUserService _userService;

        public DentalAnalysisService(IEntityRepository<DentalAnalysis> repository, IUserService userService, IMonitoringDataService monitoringDataService)
        {
            _repository = repository;
            _userService = userService;
            _monitoringDataService = monitoringDataService;
        }

        public async Task<DentalAnalysis> CreateDentalAnalysisAsync(int userId, DateTime analysisDate, float probabilityProblem, List<int> monitoringDataIDList)
        {
            User user = await _userService.GetUserByIdAsync(userId);

            List<MonitoringData> monitoringDataList = new List<MonitoringData>();
            foreach(int monitoringDataID in monitoringDataIDList)
            {
                MonitoringData monitoringDataFounded = await _monitoringDataService.GetMonitoringDataByIdAsync(monitoringDataID);
                monitoringDataList.Add(monitoringDataFounded);
            }

            DentalAnalysis analysis = new DentalAnalysis
            {
                User = user,
                AnalysisDate = analysisDate,
                ProbabilityProblem = probabilityProblem,
                MonitoringDataList = monitoringDataList
            };

            await _repository.AddAsync(analysis);
            await _repository.SaveChangesAsync();

            return analysis;
        }

        public async Task DeleteDentalAnalysisAsync(int dentalAnalysisId)
        {
            DentalAnalysis dentalAnalysis = await GetDentalAnalysisByIdAsync(dentalAnalysisId);
            _repository.Remove(dentalAnalysis);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DentalAnalysis>> GetDentalAnalysisAsync(int pageNumber, int pageSize)
        {
            return await _repository.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<DentalAnalysis> GetDentalAnalysisByIdAsync(int dentalAnalysisId)
        {
            DentalAnalysis dentalAnalysis = await _repository.GetByIdAsync(dentalAnalysisId);
            
            if(dentalAnalysis == null)
            {
                throw new EntityNotFoundException("Dental analysis doesn't exist exception");
            }

            return dentalAnalysis;
        }

        public async Task<DentalAnalysis> UpdateDentalAnalysisUserAsync(int dentalAnalysisId, float newProbabilityProblem)
        {
            DentalAnalysis dentalAnalysisUpdated = await GetDentalAnalysisByIdAsync(dentalAnalysisId);
            dentalAnalysisUpdated.ProbabilityProblem = newProbabilityProblem;
            _repository.Update(dentalAnalysisUpdated);
            await _repository.SaveChangesAsync();

            return dentalAnalysisUpdated;
        }
    }
}
