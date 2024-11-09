var builder = DistributedApplication.CreateBuilder(args);


builder.AddProject<Projects.AuthorisationService_API>("authorisationservice-api");

builder.AddProject<Projects.NutritionBalance_ApiGateway>("nutritionbalance-apigateway");

builder.Build().Run();
