$ErrorActionPreference = "Stop"

$composeFile = Join-Path $PSScriptRoot "docker-compose.yml"
$envFile = Join-Path $PSScriptRoot ".env"

if (-not (Test-Path $composeFile)) { throw "Compose file not found: $composeFile" }
if (-not (Test-Path $envFile)) { throw "Env file not found: $envFile" }

Write-Host "Stopping all SoundAlone containers..." -ForegroundColor Yellow

docker compose --env-file $envFile -f $composeFile --profile apps --profile infra down

if ($LASTEXITCODE -ne 0) {
    throw "Failed to stop SoundAlone."
}

Write-Host "All SoundAlone containers stopped." -ForegroundColor Green
