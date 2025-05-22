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
    public class DentalProcedureService : IDentalProcedureService
    {
        private readonly IEntityRepository<DentalProcedure> _entityRepository;

        private readonly IDentalHistoryEntityService _historyEntityService;

        public DentalProcedureService(IEntityRepository<DentalProcedure> entityRepository, IDentalHistoryEntityService historyEntityService)
        {
            _entityRepository = entityRepository;
            _historyEntityService = historyEntityService;
        }

        public async Task<DentalProcedure> CreateDentalProcedureAsync(int monitoringDataId, string problem)
        {

            var dentalHistory = await _historyEntityService.GetDentalHistoryByIdAsync(monitoringDataId);
            if (dentalHistory == null)
            {
                throw new EntityNotFoundException("Dental history not found for the given monitoring data ID.");
            }

          
            var dentalProcedure = new DentalProcedure
            {
                Name = problem,
                DentalHistory = dentalHistory 
            };

            await _entityRepository.AddAsync(dentalProcedure);
            await _entityRepository.SaveChangesAsync();

            return dentalProcedure;
        }

        public async Task DeleteDentalProcedureAsync(int reportDentalproblemId)
        {
            var dentalProcedure = await GetDentalProcedureByIdAsync(reportDentalproblemId);
            _entityRepository.Remove(dentalProcedure);
            await _entityRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DentalProcedure>> GetDentalProcedureAsync(int pageNumber, int pageSize)
        {
            return await _entityRepository.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<DentalProcedure> GetDentalProcedureByIdAsync(int reportDentalProblemId)
        {
            var dentalProcedure = await _entityRepository.GetByIdAsync(reportDentalProblemId);
            if (dentalProcedure == null)
            {
                throw new EntityNotFoundException("Dental procedure not found.");
            }
            return dentalProcedure;
        }

        public async Task<DentalProcedure> UpdateDentalProcedureAsync(int reportDentalproblemId, string problem)
        {
            var dentalProcedure = await GetDentalProcedureByIdAsync(reportDentalproblemId);
            dentalProcedure.Name = problem; 
            _entityRepository.Update(dentalProcedure);
            await _entityRepository.SaveChangesAsync();

            return dentalProcedure;
        }
    }
}
