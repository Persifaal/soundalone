SoundAlone - это социальная сеть про музыку

Stack приложения:
ASP.NET Core
MinIo - File Storage
Redis - Cache
PostgreSQL - Хранилище данных
Graphana - Мониторинг и анализ данных
Docker - Сборка контейнеров и запуск
Kafka - Брокер сообщений

Сервисы:
- [[Сервис Плеера]]
- [[Сервис модерации]]
- [[Сервис плейлистов]]
- [[Сервис авторизации]]
- [[Сервис уведомлений]]
- [[Социальный сервис]]

| Сервис            | URL / Адрес                                     | Логин / Пароль                                   |
| ----------------- | ----------------------------------------------- | ------------------------------------------------ |
| **Grafana**       | [http://localhost:3000](http://localhost:3000/) | `admin` / `admin`                                |
| **Prometheus**    | [http://localhost:9090](http://localhost:9090/) | Без пароля                                       |
| **MinIO Console** | [http://localhost:9001](http://localhost:9001/) | `minioadmin` / `minioadmin123`                   |
| **PostgreSQL**    | localhost:5432                                  | `soundalone_user` / `soundalone_secret_password` |
| **Redis**         | localhost:6379                                  | Без пароля (можно добавить при необходимости)    |
| **Kafka**         | localhost:9092                                  | Для внутренних нужд кластера                     |
# [[Базовый функционал]]