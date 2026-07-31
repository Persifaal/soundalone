FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PlayerService/PlayerService.Api/PlayerService.Api.csproj", "src/PlayerService/PlayerService.Api/"]
COPY ["src/PlayerService/PlayerService.Application/PlayerService.Application.csproj", "src/PlayerService/PlayerService.Application/"]
COPY ["src/PlayerService/PlayerService.Domain/PlayerService.Domain.csproj", "src/PlayerService/PlayerService.Domain/"]
COPY ["src/PlayerService/PlayerService.Infrastructure/PlayerService.Infrastructure.csproj", "src/PlayerService/PlayerService.Infrastructure/"]
RUN dotnet restore "src/PlayerService/PlayerService.Api/PlayerService.Api.csproj"
COPY . .
WORKDIR "/src/src/PlayerService/PlayerService.Api"
RUN dotnet build "./PlayerService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PlayerService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayerService.Api.dll"]
