using UserProfileService.API.DependencyInjection;
using UserProfileService.Infrastructure.DependencyInjection;
using UserProfileService.Application.DependencyInjection;
using UserProfileService.API.Middleware;

namespace UserProfileService.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();
        builder.Services.AddApiServices(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddApplicationServices();

        var app = builder.Build();

        app.UseCors("AllowSpecificOrigin");

        app.MapDefaultEndpoints();
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseAuthorization();

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}
