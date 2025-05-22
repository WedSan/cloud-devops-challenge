namespace web.DTO.User
{
    public record UpdateEmailRequest(string Email)
    {
        public UpdateEmailRequest() : this(string.Empty)
        {
        }
    }
}
