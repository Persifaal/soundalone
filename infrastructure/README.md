# SoundAlone infrastructure scripts

Expected structure:

SoundAlone/
├── infrastructure/
│   ├── docker-compose.yml
│   ├── .env
│   ├── start-infra.ps1
│   ├── deploy.ps1
│   ├── stop-infra.ps1
│   ├── stop-apps.ps1
│   └── stop-all.ps1
└── src/
    ├── AuthService/
    ├── PlayerService/
    ├── PlaylistService/
    ├── ModerationService/
    ├── NotificationService/
    └── SocialService/

Commands from the infrastructure directory:

1. Start infrastructure:
   .\start-infra.ps1

2. Build and start API services:
   .\deploy.ps1

3. Stop API services:
   .\stop-apps.ps1

4. Stop infrastructure:
   .\stop-infra.ps1

5. Stop everything:
   .\stop-all.ps1

Ports:
- Auth: 8081
- Player: 8082
- Playlist: 8083
- Moderation: 8084
- Notification: 8085
- Social: 8086
- PostgreSQL: 5432
- Redis: 6379
- Kafka: 9092
- MinIO API: 9000
- MinIO Console: 9001
- Prometheus: 9090
- Grafana: 3000

Important:
Each gRPC API container must configure Kestrel for HTTP/2 on port 8080, for example:

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

For service-to-service communication inside Docker, use service names and port 8080:
- http://auth-service:8080
- http://player-service:8080
- http://playlist-service:8080
etc.

Do not use localhost:8081/8082/... from one container to reach another container.
