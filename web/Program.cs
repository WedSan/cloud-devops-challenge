using Infrastructure.Data;
using Infrastructure.IoC;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using web.Middleware;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
       builder.Services.AddDbContext<AppDbContext>(option =>
            option.UseLazyLoadingProxies().UseOracle(
                Environment.GetEnvironmentVariable("ConnectionString")));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddControllersWithViews();

        builder.Services.AddSwaggerGen( c =>
        {
            c.SwaggerDoc("v1", new() { Title = "web", Version = "v1" });
            var filePath = Path.Combine(System.AppContext.BaseDirectory, "web.xml");
            c.IncludeXmlComments(filePath);
        });

        AddDependency(builder.Services);

        builder.Configuration.AddEnvironmentVariables();
   
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddleware>();
        
        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapControllers();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/"
        );
        app.Run();
    }

    public static void AddDependency(IServiceCollection services)
    {
        DependencyContainerRepositories.RegisterRepositories(services);
        DependencyContainerValidators.RegisterValidators(services);
        DependencyContainerService.RegisterServices(services);
    }
}