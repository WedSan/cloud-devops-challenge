using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IDentalHistoryEntityService
    {
        Task<DentalHistory> CreateDentalHistoryAsync(int userId, List<string> procedures, DateTime consulationDate, string toothCondition);

        Task<IEnumerable<DentalHistory>> GetDentalHistoriesAsync(int pageNumber, int pageSize);

        Task<DentalHistory> GetDentalHistoryByIdAsync(int dentalHistoryId);

        Task<DentalHistory> UpdateDentalHistoryUserAsync(int dentalHistoryId, List<string> newProcedures);

        Task DeleteDentalHistoryAsync(int dentalHistoryId);
    }
}
