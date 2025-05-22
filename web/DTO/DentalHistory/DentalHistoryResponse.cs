namespace web.DTO.DentalHistory
{
    namespace web.DTO.DentalHistory
    {
        public record DentalHistoryResponse(
            int Id,                            
            int UserId,                        
            List<string> Procedures,        
            DateTime ConsultationDate,          
            string ToothCondition               
        );
    }
}
