$ErrorActionPreference = "Stop"

$composeFile = Join-Path $PSScriptRoot "docker-compose.yml"
$envFile = Join-Path $PSScriptRoot ".env"

if (-not (Test-Path $composeFile)) { throw "Compose file not found: $composeFile" }
if (-not (Test-Path $envFile)) { throw "Env file not found: $envFile" }

Write-Host "Starting SoundAlone infrastructure..." -ForegroundColor Cyan
Write-Host "Postgres, Redis, Kafka, Zookeeper, MinIO, Prometheus and Grafana"
Write-Host ""

docker compose --env-file $envFile -f $composeFile --profile infra up -d

if ($LASTEXITCODE -ne 0) {
    throw "Failed to start infrastructure."
}

$dbTimeoutSec = 120
$intervalSec = 3
$elapsed = 0

Write-Host ""
Write-Host "Waiting for PostgreSQL and Redis health checks..." -ForegroundColor Yellow

while ($elapsed -lt $dbTimeoutSec) {
    $postgresHealth = docker inspect --format "{{.State.Health.Status}}" soundalone-postgres 2>$null
    $redisHealth = docker inspect --format "{{.State.Health.Status}}" soundalone-redis 2>$null

    if ($postgresHealth -eq "healthy" -and $redisHealth -eq "healthy") {
        Write-Host "PostgreSQL and Redis are healthy." -ForegroundColor Green
        break
    }

    Start-Sleep -Seconds $intervalSec
    $elapsed += $intervalSec
}

if ($elapsed -ge $dbTimeoutSec) {
    Write-Host "Warning: timeout waiting for infrastructure health checks." -ForegroundColor Yellow
    Write-Host "Check logs with: docker compose --env-file $envFile -f $composeFile --profile infra logs"
}

Write-Host ""
Write-Host "Infrastructure endpoints:" -ForegroundColor Cyan
Write-Host "  PostgreSQL -> localhost:5432"
Write-Host "  Redis      -> localhost:6379"
Write-Host "  Kafka      -> localhost:9092"
Write-Host "  MinIO API  -> localhost:9000"
Write-Host "  MinIO UI   -> http://localhost:9001"
Write-Host "  Prometheus -> http://localhost:9090"
Write-Host "  Grafana    -> http://localhost:3000"
Write-Host ""

docker compose --env-file $envFile -f $composeFile --profile infra ps
