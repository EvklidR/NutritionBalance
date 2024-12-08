using MealPlanService.Infrastructure.DependencyInjection;
using MealPlanService.Application.DependencyInjection;
using MealPlanService.API.DependencyInjection;
using MealPlanService.API.Middleware;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApiServices(builder.Configuration);
        builder.AddServiceDefaults();

        var app = builder.Build();

        app.UseCors("AllowSpecificOrigin");

        app.MapGrpcService<MealPlanServiceImpl>();

        app.MapDefaultEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
