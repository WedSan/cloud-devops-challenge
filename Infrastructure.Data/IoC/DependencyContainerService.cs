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
    public class DependencyContainerService
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserEmailUpdater, UserEmailUpdater>();

            services.AddScoped<IUserPasswordUpdater, UserPasswordUpdater>();

            services.AddScoped<IMonitoringDataService, MonitoringDataService>();

            services.AddScoped<IDentalAnalysisService, DentalAnalysisService>();

            services.AddScoped<IDentalHistoryEntityService, DentalHistoryService>();

            services.AddScoped<IReportDentalProblemService, ReportDentalProblemService>();

            services.AddScoped<IDentalProcedureService, DentalProcedureService>();
        }
    }
}
