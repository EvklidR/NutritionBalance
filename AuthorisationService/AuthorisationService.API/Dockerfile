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
COPY ["AuthorisationService/AuthorisationService.API/AuthorisationService.API.csproj", "AuthorisationService/AuthorisationService.API/"]
COPY ["NutritionBalance.ServiceDefaults/NutritionBalance.ServiceDefaults.csproj", "NutritionBalance.ServiceDefaults/"]
COPY ["AuthorisationService/AuthorisationService.Application/AuthorisationService.Application.csproj", "AuthorisationService/AuthorisationService.Application/"]
COPY ["AuthorisationService/AuthorisationService.Domain/AuthorisationService.Domain.csproj", "AuthorisationService/AuthorisationService.Domain/"]
COPY ["AuthorisationService/AuthorisationService.Infrastructure/AuthorisationService.Infrastructure.csproj", "AuthorisationService/AuthorisationService.Infrastructure/"]
RUN dotnet restore "./AuthorisationService/AuthorisationService.API/AuthorisationService.API.csproj"
COPY . .
WORKDIR "/src/AuthorisationService/AuthorisationService.API"
RUN dotnet build "./AuthorisationService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuthorisationService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthorisationService.API.dll"]