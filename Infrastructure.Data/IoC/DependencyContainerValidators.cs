using Application.Interfaces;
using Application.Services;
using Application.Validators.Users;
using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IoC
{
    public class DependencyContainerValidators
    {
        public static void RegisterValidators(IServiceCollection services)
        {

            services.AddScoped<IUserCreationValidator, UserEmailValidator>();
            services.AddScoped<IUserEmailValidator, UserEmailValidator>();

        }
    }
}
