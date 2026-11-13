# AI Business Assistant

[![CI](https://github.com/Weretik/ai-business-assistant/actions/workflows/ci.yml/badge.svg)](https://github.com/Weretik/ai-business-assistant/actions/workflows/ci.yml)

AI-ассистент для малого B2B-бизнеса на **C# / .NET 10**: разбирает письма клиентов в заказы, отвечает на вопросы по клиентам и заказам на основе реальных данных, ищет по базе документов (RAG) и готовит черновики коммерческих предложений с подтверждением человеком.

Проект растёт поэтапно: каждый этап добавляет одну AI-возможность в то же приложение, а не создаёт новое.

## Стек

| Область | Технологии |
|---|---|
| Backend | ASP.NET Core Web API (.NET 10), Minimal API |
| LLM | Microsoft.Extensions.AI, OpenAI .NET SDK |
| Данные | EF Core, PostgreSQL + pgvector |
| Агенты и интеграции | Microsoft Agent Framework, MCP C# SDK |
| Frontend | Angular |
| Качество | xUnit, Microsoft.Extensions.AI.Evaluation, OpenTelemetry |
| Инфраструктура | Docker, GitHub Actions, Google Cloud Run |

## Архитектура

Простой **модульный монолит**: один ASP.NET Core хост, каждая AI-возможность живёт в своём модуле (`src/AiBusinessAssistant.Api/Modules/<Module>`) и регистрируется методом-расширением в `Program.cs`. Без микросервисов и лишних слоёв: абстракции появляются, только когда без них становится хуже.

```
src/
  AiBusinessAssistant.Api/        ASP.NET Core хост
    Modules/
      Extraction/                 Этап 1: письмо -> заказ
tests/
  AiBusinessAssistant.Api.Tests/  xUnit + WebApplicationFactory
```

## Роадмап

- [ ] **1. AI Extractor**: API принимает письмо клиента, вызывает LLM и возвращает валидированный DTO заказа (Structured Outputs).
- [ ] **2. AI Chat**: чат через Microsoft.Extensions.AI, streaming (SSE), Angular-интерфейс, история диалогов.
- [ ] **3. AI Tools**: таблицы Clients и Orders; инструменты `FindClient`, `GetOrders`, `GetDebt`; модель получает данные только через backend.
- [ ] **4. AI Knowledge Base**: загрузка PDF/DOCX, chunking, embeddings, pgvector, semantic и hybrid search, ответы со ссылками на источники.
- [ ] **5. Production Quality**: evals, интеграционные тесты, защита от prompt injection, tracing, учёт токенов и стоимости, деплой в Cloud Run.
- [ ] **6. AI Agent**: управляемый workflow подготовки КП с human-in-the-loop.
- [ ] **7. MCP Integration**: MCP-сервер с инструментами чтения каталога и заказов, тестовый клиент.

## Запуск локально

Нужен [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

```bash
dotnet build
dotnet test
dotnet run --project src/AiBusinessAssistant.Api
```

Проверка: `GET /health` возвращает `Healthy`. Примеры запросов лежат в `src/AiBusinessAssistant.Api/AiBusinessAssistant.Api.http`.

### Секреты

Ключи API **никогда не коммитятся**. Локально используйте user-secrets, в облаке переменные окружения:

```bash
dotnet user-secrets --project src/AiBusinessAssistant.Api set "OpenAI:ApiKey" "<your-key>"
dotnet user-secrets --project src/AiBusinessAssistant.Api set "OpenAI:Model" "<model-name>"
```

Переменные окружения: `OpenAI__ApiKey`, `OpenAI__Model`.

## Решения

Ключевые архитектурные решения и их причины будут описаны в [`docs/decisions.md`](docs/decisions.md) по мере развития проекта.

## Лицензия

[MIT](LICENSE)
