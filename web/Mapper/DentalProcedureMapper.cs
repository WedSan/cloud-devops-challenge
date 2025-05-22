using Domain.Entities;
using web.DTO.DentalProcedure;

namespace web.Mapper
{
    public class DentalProcedureMapper
    {
        public static DentalProcedureResponse ToDto(DentalProcedure dentalProcedure)
        {
            return new DentalProcedureResponse(
                dentalProcedure.Id,
                dentalProcedure.Name,
                dentalProcedure.DentalHistory.ID 
            );
        }

        public static IEnumerable<DentalProcedureResponse> ToDto(IEnumerable<DentalProcedure> dentalProcedures)
        {
            List<DentalProcedureResponse> newList = new List<DentalProcedureResponse>();
            foreach (var item in dentalProcedures)
            {
                newList.Add(ToDto(item));
            }
            return newList;
        }

        public static ReportDentalProblem ToEntity(AddDentalProcedureRequest request)
        {
            return new ReportDentalProblem
            {
                Problem = request.Problem,
            };
        }

        public static ReportDentalProblem ToEntity(UpdateDentalProcedureRequest request, int id)
        {
            return new ReportDentalProblem
            {
                Id = id,
                Problem = request.Problem
            };
        }
    }
}
    