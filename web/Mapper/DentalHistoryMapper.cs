using Domain.Entities;
using web.DTO.DentalHistory;
using web.DTO.DentalHistory.web.DTO.DentalHistory;

namespace web.Mapper
{
    public static class DentalHistoryMapper
    {
        public static DentalHistoryResponse ToDTO(DentalHistory dentalHistory)
        {
            return new DentalHistoryResponse(
                dentalHistory.ID,
                dentalHistory.User.Id,
                dentalHistory.Procedures.Select(p => p.Name).ToList(),
                dentalHistory.ConsultationDate,
                dentalHistory.ToothCondition
            );
        }


        public static List<DentalHistoryResponse> ToDTO(IEnumerable<DentalHistory> dentalHistories)
        {
            return dentalHistories.Select(dh => ToDTO(dh)).ToList();
        }


        public static DentalHistory ToEntity(AddDentalHistoryRequest request, User user)
        {
            return new DentalHistory
            {
                User = user,
                Procedures = request.Procedures.Select(p => new DentalProcedure { Name = p }).ToList(),
                ConsultationDate = request.ConsultationDate,
                ToothCondition = request.ToothCondition
            };
        }

        public static void UpdateEntity(DentalHistory dentalHistory, UpdateDentalHistoryRequest request)
        {
            dentalHistory.Procedures = request.NewProcedures.Select(p => new DentalProcedure { Name = p }).ToList();
        }
    }
}