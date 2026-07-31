FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/PlaylistService/PlaylistService.Api/PlaylistService.Api.csproj", "src/PlaylistService/PlaylistService.Api/"]
COPY ["src/PlaylistService/PlaylistService.Application/PlaylistService.Application.csproj", "src/PlaylistService/PlaylistService.Application/"]
COPY ["src/PlaylistService/PlaylistService.Domain/PlaylistService.Domain.csproj", "src/PlaylistService/PlaylistService.Domain/"]
COPY ["src/PlaylistService/PlaylistService.Infrastructure/PlaylistService.Infrastructure.csproj", "src/PlaylistService/PlaylistService.Infrastructure/"]
RUN dotnet restore "src/PlaylistService/PlaylistService.Api/PlaylistService.Api.csproj"
COPY . .
WORKDIR "/src/src/PlaylistService/PlaylistService.Api"
RUN dotnet build "./PlaylistService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PlaylistService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlaylistService.Api.dll"]
