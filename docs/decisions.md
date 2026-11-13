# Architecture decisions

A short log of decisions: what we chose, why, and what the alternatives were.

## 001. Modular monolith

**Decision:** one ASP.NET Core application; AI features are split into modules in the `Modules/` folder.

**Why:** the project has one developer and one deployment. Microservices would add network calls, separate CI pipelines and observability with no benefit. Module boundaries make it possible to extract a service later if there is a real reason.

## 002. Secrets only outside the repository

**Decision:** LLM provider keys are read from configuration (`OpenAI:ApiKey`): user-secrets locally, environment variables or Secret Manager in Cloud Run.

**Why:** a key leaked through a public repository means direct costs and a compromised account.

## 003. React for the frontend

**Decision:** the web client is built with React, TypeScript and Vite, in the `web/` folder of the same repository.

**Why:** React is my main frontend stack. Keeping the client in the same repository lets one pull request change the API and the UI together, and one Docker Compose file runs everything.
