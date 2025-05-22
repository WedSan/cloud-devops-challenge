using Domain.Entities;
using Domain.Extensions;
using web.DTO.User;

namespace web.Mapper
{
    public static class UserMapper
    {
        public static UserEntityResponse ToDTO(User user)
        {
            return new UserEntityResponse
            (
              user.Id,
              user.Name,
              user.Email,
              user.Gender.ToChar()
            );
        }

        public static List<UserEntityResponse> ToDTO(IEnumerable<User> users)
        {
            return users.Select(user => new UserEntityResponse(
                    user.Id,
                    user.Name,
                    user.Email,
                    user.Gender.ToChar()
                )).ToList();
        }
    }
}
