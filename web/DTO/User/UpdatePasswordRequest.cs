namespace web.DTO.User
{
    public record UpdatePasswordRequest(string NewPassword)
    {
        public UpdatePasswordRequest() : this(string.Empty)
        {
            
        }
    }

}
