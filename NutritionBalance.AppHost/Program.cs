var builder = DistributedApplication.CreateBuilder(args);


builder.AddProject<Projects.AuthorisationService_API>("authorisationservice-api");

builder.AddProject<Projects.NutritionBalance_ApiGateway>("nutritionbalance-apigateway");


builder.AddProject<Projects.MealPlanService_API>("mealplanservice-api");


builder.AddProject<Projects.UserProfileService_API>("userprofileservice-api");


builder.AddProject<Projects.ProductService_API>("productservice-api");


builder.Build().Run();
