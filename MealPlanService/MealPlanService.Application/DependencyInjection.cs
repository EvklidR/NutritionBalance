using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MealPlanService.Application.UseCases;

namespace MealPlanService.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<CreateMealPlan>();
            services.AddScoped<UpdateMealPlan>();


            return services;

        }
    }
}
