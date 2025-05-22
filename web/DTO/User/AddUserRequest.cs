namespace web.DTO.User
{
    public record AddUserRequest(string Name, string Email, string Password, char Gender);
}
