using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserProfileService.Infrastructure.MSSQL;
using UserProfileService.Infrastructure.Repositories;
using UserProfileService.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using UserProfileService.Domain.Interfaces.Repositories;
using UserProfileService.Application.Interfaces;
using UserProfileService.Infrastructure.Services;
using StackExchange.Redis;
using UserProfileService.Infrastructure.Grpc;

namespace UserProfileService.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUserService>(sp =>
                new UserAuthClient(configuration["GrpcServices:UserAuthUrl"]));

            services.AddSingleton<IMealPlanService>(sp =>
                new MealPlanServiceClient(configuration["GrpcServices:MealPlanUrl"]));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDayResultRepository, DayResultRepository>();
            services.AddScoped<IDishRepository, DishRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ISearchProductService, SearchProductService>();

            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), configuration["ImageSettings:ImagePath"]);
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("127.0.0.1:6379"));


            services.AddScoped<IImageService>(provider =>
                new ImageService(imagePath, provider.GetRequiredService<IConnectionMultiplexer>()));

            return services;
        }
    }
}
