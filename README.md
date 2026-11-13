# AI Business Assistant

[![CI](https://github.com/Weretik/ai-business-assistant/actions/workflows/ci.yml/badge.svg)](https://github.com/Weretik/ai-business-assistant/actions/workflows/ci.yml)

An AI assistant for a small B2B business, built with **C# / .NET 10** and **React**. It turns customer emails into orders, answers questions about clients and orders using real data, searches company documents (RAG), and drafts commercial offers with human approval.

The project grows step by step: each stage adds one AI feature to the same application instead of starting a new one.

## Tech stack

| Area | Technologies |
|---|---|
| Backend | ASP.NET Core Web API (.NET 10), Minimal API |
| LLM | Microsoft.Extensions.AI, OpenAI .NET SDK |
| Data | EF Core, PostgreSQL + pgvector |
| Frontend | React, TypeScript, Vite |
| Agents and integrations | Microsoft Agent Framework, MCP C# SDK |
| Quality | xUnit, Testcontainers, Microsoft.Extensions.AI.Evaluation, OpenTelemetry |
| Infrastructure | Docker, GitHub Actions, Google Cloud Run |

## Architecture

A simple **modular monolith**: one ASP.NET Core host, and each AI feature lives in its own module (`src/AiBusinessAssistant.Api/Modules/<Module>`), registered with an extension method in `Program.cs`. No microservices and no extra layers: abstractions are added only when the code gets worse without them.

```
src/
  AiBusinessAssistant.Api/        ASP.NET Core host
    Modules/
      Extraction/                 Stage 1: email -> order
tests/
  AiBusinessAssistant.Api.Tests/  xUnit + WebApplicationFactory
```

The React client will live in `web/` from stage 2.

## Roadmap

- [ ] **1. AI Extractor**: the API takes a customer email, calls the LLM and returns a validated order DTO (Structured Outputs).
- [ ] **2. AI Chat**: chat through Microsoft.Extensions.AI, streaming (SSE), React UI, conversation history.
- [ ] **3. AI Tools**: Clients and Orders tables; tools `FindClient`, `GetOrders`, `GetDebt`; the model gets data only through the backend.
- [ ] **4. AI Knowledge Base**: PDF/DOCX upload, chunking, embeddings, pgvector, semantic and hybrid search, answers with source links.
- [ ] **5. Production Quality**: evals, integration tests, prompt injection protection, tracing, token and cost tracking, deployment to Cloud Run.
- [ ] **6. AI Agent**: a controlled workflow for drafting commercial offers with human-in-the-loop.
- [ ] **7. MCP Integration**: an MCP server with read tools for the catalog and orders, plus a test client.

All demo data (companies, clients, documents) is fictional.

## Run locally

You need the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

```bash
dotnet build
dotnet test
dotnet run --project src/AiBusinessAssistant.Api
```

Check: `GET /health` returns `Healthy`. Sample requests are in `src/AiBusinessAssistant.Api/AiBusinessAssistant.Api.http`.

### Secrets

API keys are **never committed**. Use user-secrets locally and environment variables in the cloud:

```bash
dotnet user-secrets --project src/AiBusinessAssistant.Api set "OpenAI:ApiKey" "<your-key>"
dotnet user-secrets --project src/AiBusinessAssistant.Api set "OpenAI:Model" "<model-name>"
```

Environment variables: `OpenAI__ApiKey`, `OpenAI__Model`.

## Decisions

Key architecture decisions and the reasons for them are in [`docs/decisions.md`](docs/decisions.md).

## License

[MIT](LICENSE)
