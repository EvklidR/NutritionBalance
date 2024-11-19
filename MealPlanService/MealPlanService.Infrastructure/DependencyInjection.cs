using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MealPlanService.Infrastructure.MSSQL;
using MealPlanService.Infrastructure.Repositories;
using MealPlanService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MealPlanService.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IMealPlanRepository, MealPlanRepository>();
            //services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            //var imagePath = Path.Combine(Directory.GetCurrentDirectory(), configuration["ImageSettings:ImagePath"]);
            //if (!Directory.Exists(imagePath))
            //{
            //    Directory.CreateDirectory(imagePath);
            //}

            //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("127.0.0.1:6379"));

            //services.AddScoped<IImageService>(provider => new ImageService(imagePath, provider.GetRequiredService<IConnectionMultiplexer>()));

            return services;
        }
    }
}
