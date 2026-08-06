FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AuthService/AuthService.Api/AuthService.Api.csproj", "src/AuthService/AuthService.Api/"]
COPY ["src/AuthService/AuthService.Application/AuthService.Application.csproj", "src/AuthService/AuthService.Application/"]
COPY ["src/AuthService/AuthService.Domain/AuthService.Domain.csproj", "src/AuthService/AuthService.Domain/"]
COPY ["src/AuthService/AuthService.Infrastructure/AuthService.Infrastructure.csproj", "src/AuthService/AuthService.Infrastructure/"]
RUN dotnet restore "src/AuthService/AuthService.Api/AuthService.Api.csproj"
COPY . .
WORKDIR "/src/src/AuthService/AuthService.Api"
RUN dotnet build "./AuthService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AuthService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.Api.dll"]
