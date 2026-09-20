# Placeholder Project: Microservices Playground

A small microservices system that serves as the concrete vehicle for this semester's personal experiments (CI/CD internals, and optionally architecture or frontend topics). It is intentionally small and stays a learning vehicle, not a product.

## Structure

```text
microservices-playground/
├── services/
│   ├── catalog/          ASP.NET Core minimal API (C#)
│   └── orders/           ASP.NET Core minimal API (C#)
├── frontend/             React (Vite + TypeScript)
├── docker-compose.yml    runs all three locally
└── .gitlab-ci.yml        CI placeholder (exercised on a GitLab instance)
```

## Services

| Service  | Port | Endpoints                          |
|----------|------|------------------------------------|
| catalog  | 8081 | `/health`, `/items`                |
| orders   | 8082 | `/health`, `/orders`               |
| frontend | 5173 | fetches from both services         |

In-memory data only. No databases yet.

## Run locally

With Docker (recommended):

```bash
docker compose up --build
```

Without Docker:

```bash
cd services/catalog && dotnet run      # needs .NET 8 SDK
cd services/orders  && dotnet run      # needs .NET 8 SDK
cd frontend && npm install && npm run dev
```

The .NET 8 SDK is currently **not installed** on this machine — install it before running the services locally, or use Docker.

## CI

The `.gitlab-ci.yml` is an initial placeholder with a build stage per service. This repository is hosted on GitHub, so CI experiments require pushing or mirroring this project to a GitLab instance (e.g. the school GitLab).

## Intended experiments

- Runner behavior: observe what GitLab Runner spawns while this pipeline executes
- YAML-to-command mapping: study how each job becomes shell invocations
- Coverage instrumentation: measure instrumentation overhead on the services
- Changed-file detection: test `git diff` edge cases against this project's history
- Fallback rules: design safe test selection for this project's test suite
- (Optional) architecture / frontend experiments as a secondary track

Each experiment lives in the top-level `experiments/` directory and references this project.
