namespace web.DTO.DentalHistory
{
    public record UpdateDentalHistoryRequest(
        List<string> NewProcedures
    )
    {
        public UpdateDentalHistoryRequest() : this(new List<string>())
        {
        }
    }
}
