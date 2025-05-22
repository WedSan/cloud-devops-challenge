using Application.Interfaces;
using Application.Validators.Users;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IoC
{
    public class DependencyContainerRepositories
    {
        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();

        }

        private static void RegisterGenericRepositories(IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> entityTypes = assembly.GetTypes().
                Where(type => type.IsClass && !type.IsAbstract && type.Namespace == "Domain.Entities");

            foreach (Type entityType in entityTypes)
            {
                Type repositoryInterface = typeof(IEntityRepository<>).MakeGenericType(entityType);
                Type repositoryImplementation = typeof(EntityRepository<>).MakeGenericType(entityType);

                services.AddScoped(repositoryInterface, repositoryImplementation);
            }
        }
    }
}
