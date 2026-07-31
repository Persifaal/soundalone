FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SocialService/SocialService.Api/SocialService.Api.csproj", "src/SocialService/SocialService.Api/"]
COPY ["src/SocialService/SocialService.Application/SocialService.Application.csproj", "src/SocialService/SocialService.Application/"]
COPY ["src/SocialService/SocialService.Domain/SocialService.Domain.csproj", "src/SocialService/SocialService.Domain/"]
COPY ["src/SocialService/SocialService.Infrastructure/SocialService.Infrastructure.csproj", "src/SocialService/SocialService.Infrastructure/"]
RUN dotnet restore "src/SocialService/SocialService.Api/SocialService.Api.csproj"
COPY . .
WORKDIR "/src/src/SocialService/SocialService.Api"
RUN dotnet build "./SocialService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SocialService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialService.Api.dll"]
