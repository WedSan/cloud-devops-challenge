using System.Runtime.CompilerServices;

namespace web.DTO.DentalHistory;

public record UpdateDentalHistoryView(string NewProcedures)
{
    public UpdateDentalHistoryView() : this(string.Empty)
    {
        
    }
}