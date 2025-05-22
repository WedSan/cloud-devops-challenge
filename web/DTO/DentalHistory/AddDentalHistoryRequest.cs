using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;

namespace web.DTO.DentalHistory
{
    public record AddDentalHistoryRequest(
        int UserId,
        List<string> Procedures,
        DateTime ConsultationDate,
        string ToothCondition
    )
    {
        public AddDentalHistoryRequest() : this(0, new List<string>(), DateTime.Now, string.Empty)
        {
        }
    }
        
 
}
