$ErrorActionPreference = "Stop"

$composeFile = Join-Path $PSScriptRoot "docker-compose.yml"
$envFile = Join-Path $PSScriptRoot ".env"

if (-not (Test-Path $composeFile)) { throw "Compose file not found: $composeFile" }
if (-not (Test-Path $envFile)) { throw "Env file not found: $envFile" }

Write-Host "Building and starting SoundAlone API services (profile: apps)..." -ForegroundColor Cyan
Write-Host "Infrastructure is not started by this script."
Write-Host "Run .\start-infra.ps1 first."
Write-Host ""

docker compose --env-file $envFile -f $composeFile --profile apps up -d --build --no-deps

if ($LASTEXITCODE -ne 0) {
    throw "Failed to build/start API services."
}

Write-Host ""
Write-Host "Running API containers:" -ForegroundColor Cyan
docker compose --env-file $envFile -f $composeFile --profile apps ps

Write-Host ""
Write-Host "gRPC endpoints for Postman:" -ForegroundColor Green
Write-Host "  Auth         -> localhost:8081"
Write-Host "  Player       -> localhost:8082"
Write-Host "  Playlist     -> localhost:8083"
Write-Host "  Moderation   -> localhost:8084"
Write-Host "  Notification -> localhost:8085"
Write-Host "  Social       -> localhost:8086"
