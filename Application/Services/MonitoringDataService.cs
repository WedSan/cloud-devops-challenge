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
    public class MonitoringDataService : IMonitoringDataService
    {
        private readonly IEntityRepository<MonitoringData> _entityRepository;

        private readonly IUserService _userService;

        public MonitoringDataService(IEntityRepository<MonitoringData> entityRepository, IUserService userService)
        {
            _entityRepository = entityRepository;
            _userService = userService;
        }

        public async Task<MonitoringData> CreateMonitoringDataAsync(int userId, List<ReportDentalProblem> problems)
        { 
            User user = await _userService.GetUserByIdAsync(userId);
            MonitoringData monitoringData = new MonitoringData
            {
                User = user,
                Problems = problems,
                RegistrationDate = DateTime.Now,
            };
            await _entityRepository.AddAsync(monitoringData);
            await _entityRepository.SaveChangesAsync();
            return monitoringData;
        }

        public async Task DeleteMonitoringDataAsync(int monitoringDataId)
        {
            MonitoringData monitoringDataDeleted = await _entityRepository.GetByIdAsync(monitoringDataId);
            _entityRepository.Remove(monitoringDataDeleted);
            await _entityRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<MonitoringData>> GetMonitoringDataAsync(int pageNumber, int pageSize)
        {
            return await _entityRepository.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<MonitoringData> GetMonitoringDataByIdAsync(int monitoringDataId)
        {
            MonitoringData monitoringData = await _entityRepository.GetByIdAsync(monitoringDataId);
            if(monitoringData == null)
            {
                throw new EntityNotFoundException("Monitoring Data doesn't exist");
            }

            return monitoringData;
        }

        public async Task<MonitoringData> UpdateMonitoringDataUserAsync(int monitoringDataId, int userId)
        {
            User userFounded = await _userService.GetUserByIdAsync(userId);
            MonitoringData monitoringData = await GetMonitoringDataByIdAsync(monitoringDataId);
            monitoringData.User = userFounded;
            _entityRepository.Update(monitoringData);
            await _entityRepository.SaveChangesAsync();
            return monitoringData;
        }
    }
}
