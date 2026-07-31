FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ModerationService/ModerationService.Api/ModerationService.Api.csproj", "src/ModerationService/ModerationService.Api/"]
COPY ["src/ModerationService/ModerationService.Application/ModerationService.Application.csproj", "src/ModerationService/ModerationService.Application/"]
COPY ["src/ModerationService/ModerationService.Domain/ModerationService.Domain.csproj", "src/ModerationService/ModerationService.Domain/"]
COPY ["src/ModerationService/ModerationService.Infrastructure/ModerationService.Infrastructure.csproj", "src/ModerationService/ModerationService.Infrastructure/"]
RUN dotnet restore "src/ModerationService/ModerationService.Api/ModerationService.Api.csproj"
COPY . .
WORKDIR "/src/src/ModerationService/ModerationService.Api"
RUN dotnet build "./ModerationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ModerationService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ModerationService.Api.dll"]
