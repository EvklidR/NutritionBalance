# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UserProfileService/UserProfileService.API/UserProfileService.API.csproj", "UserProfileService/UserProfileService.API/"]
COPY ["NutritionBalance.ServiceDefaults/NutritionBalance.ServiceDefaults.csproj", "NutritionBalance.ServiceDefaults/"]
COPY ["UserProfileService/UserProfileService.Application/UserProfileService.Application.csproj", "UserProfileService/UserProfileService.Application/"]
COPY ["UserProfileService/UserProfileService.Domain/UserProfileService.Domain.csproj", "UserProfileService/UserProfileService.Domain/"]
COPY ["UserProfileService/UserProfileService.Infrastructure/UserProfileService.Infrastructure.csproj", "UserProfileService/UserProfileService.Infrastructure/"]
RUN dotnet restore "./UserProfileService/UserProfileService.API/UserProfileService.API.csproj"
COPY . .
WORKDIR "/src/UserProfileService/UserProfileService.API"
RUN dotnet build "./UserProfileService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserProfileService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserProfileService.API.dll"]