using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserPasswordUpdater : IUserPasswordUpdater
    {
        private readonly IUserService _userService;

        private readonly IEntityRepository<User> _entityRepository;

        public UserPasswordUpdater(IUserService userService, IEntityRepository<User> entityRepository)
        {
            _userService = userService;
            _entityRepository = entityRepository;
        }

        public async Task UpdatePassword(int userId, string newPassword)
        {
            User user = await _userService.GetUserByIdAsync(userId);
            user.Password = newPassword;
            _entityRepository.Update(user);
            await _entityRepository.SaveChangesAsync();
        }
    }
}
