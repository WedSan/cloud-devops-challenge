using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Users
{
    public interface IUserCreationValidator
    {
        Task Validate(User user);
    }
}
