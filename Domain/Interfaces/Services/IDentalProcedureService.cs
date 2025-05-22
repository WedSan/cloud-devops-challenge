using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IDentalProcedureService
    {
        Task<DentalProcedure> CreateDentalProcedureAsync(int monitoringDataId, string problem);

        Task<IEnumerable<DentalProcedure>> GetDentalProcedureAsync(int pageNumber, int pageSize);

        Task<DentalProcedure> GetDentalProcedureByIdAsync(int reportDentalProblemId);

        Task<DentalProcedure> UpdateDentalProcedureAsync(int reportDentalproblemId, string problem);

        Task DeleteDentalProcedureAsync(int reportDentalproblemId);
    }
}
