using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;


namespace Application.Services
{
    public class UserEmailUpdater : IUserEmailUpdater
    {
        private readonly IEntityRepository<User> _entityRepository;

        private readonly IUserService _userService;

        private readonly IUserEmailValidator _emailValidator;

        public UserEmailUpdater(IEntityRepository<User> repository, IUserService userService, IUserEmailValidator emailValidator)
        {
            _entityRepository = repository;
            _userService = userService;
            _emailValidator = emailValidator;
        }

        public async Task UpdateEmail(int userId, string newEmail)
        {
            await _emailValidator.ValidateEmail(newEmail);
            User user = await _userService.GetUserByIdAsync(userId);
            user.Email = newEmail;
            _entityRepository.Update(user);
            await _entityRepository.SaveChangesAsync();
        }
   

    }
}
