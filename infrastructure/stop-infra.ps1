$ErrorActionPreference = "Stop"

$composeFile = Join-Path $PSScriptRoot "docker-compose.yml"
$envFile = Join-Path $PSScriptRoot ".env"

if (-not (Test-Path $composeFile)) { throw "Compose file not found: $composeFile" }
if (-not (Test-Path $envFile)) { throw "Env file not found: $envFile" }

Write-Host "Stopping SoundAlone infrastructure containers..." -ForegroundColor Yellow

docker compose --env-file $envFile -f $composeFile --profile infra down

if ($LASTEXITCODE -ne 0) {
    throw "Failed to stop infrastructure."
}

Write-Host "Infrastructure stopped." -ForegroundColor Green
