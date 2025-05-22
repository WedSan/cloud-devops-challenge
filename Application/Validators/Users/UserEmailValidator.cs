using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace Application.Validators.Users
{
    public class UserEmailValidator : IUserCreationValidator, IUserEmailValidator
    {
        private readonly IUserRepository _userRepository;

        public UserEmailValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task Validate(User user)
        {
            await ValidateEmail(user.Email);
        }

        public async Task ValidateEmail(string email)
        {
            if (!IsValidEmail(email))
                throw new ArgumentException("Email is invalid");

            User userFounded = await _userRepository.FindUserByEmail(email);
            if (userFounded != null)
                throw new UserEmailAlreadyExistsException("Email already registered");
        }

        public bool IsValidEmail(string emailString)
        {
            try
            {
                new MailAddress(emailString);
                return true;
            } 
            catch(Exception ex)
            {
                return false;
            }
        }



    }
}
