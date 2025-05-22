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
    public class DentalHistoryService : IDentalHistoryEntityService
    {
        private readonly IEntityRepository<DentalHistory> _entityRepository;

        private readonly IUserService _userService;


        public DentalHistoryService(IEntityRepository<DentalHistory> entityRepository, IUserService userService)
        {
            _entityRepository = entityRepository;
            _userService = userService;
        }

        public async Task<DentalHistory> CreateDentalHistoryAsync(int userId, List<string> procedures, DateTime consulationDate, string toothCondition)
        {
            User user = await _userService.GetUserByIdAsync(userId);

            List<DentalProcedure> dentalProcedures = new List<DentalProcedure>();
            foreach(string procedure in procedures)
            {
                dentalProcedures.Add(new DentalProcedure { Name = procedure });
            }

            DentalHistory dentalHistory = new DentalHistory
            {
                User = user,
                ConsultationDate = consulationDate,
                Procedures = dentalProcedures,
                ToothCondition = toothCondition
            };

            await _entityRepository.AddAsync(dentalHistory);
            await _entityRepository.SaveChangesAsync();

            return dentalHistory;
        }

        public async Task DeleteDentalHistoryAsync(int dentalHistoryId)
        {
            DentalHistory dentalHistoryFounded = await GetDentalHistoryByIdAsync(dentalHistoryId);
            _entityRepository.Remove(dentalHistoryFounded);
            await _entityRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DentalHistory>> GetDentalHistoriesAsync(int pageNumber, int pageSize)
        {
            return await _entityRepository.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<DentalHistory> GetDentalHistoryByIdAsync(int dentalHistoryId)
        {
            DentalHistory dentalHistoryFounded = await _entityRepository.GetByIdAsync(dentalHistoryId);

            if (dentalHistoryFounded == null)
                throw new EntityNotFoundException("Dental History doesn't exist");

            return dentalHistoryFounded;
        }

        public async Task<DentalHistory> UpdateDentalHistoryUserAsync(int dentalHistoryId, List<string> newProcedures)
        {
            DentalHistory dentalHistoryFounded = await GetDentalHistoryByIdAsync(dentalHistoryId);
            dentalHistoryFounded.Procedures = newProcedures.Select(procedure => new DentalProcedure { Name = procedure }).ToList();
            _entityRepository.Update(dentalHistoryFounded);
            await _entityRepository.SaveChangesAsync();
            return dentalHistoryFounded;
        }
    }
}
