using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(string name, string email, string password, Gender gender);

        Task<IEnumerable<User>> GetUsersAsync(int pageNumber, int pageSize);

        Task<User> GetUserByIdAsync(int userID);

        Task DeleteUserAsync(int userId);

    }
}
