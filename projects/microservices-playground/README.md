# Placeholder Project: Microservices Playground

A small microservices system that serves as the concrete vehicle for this semester's personal experiments (CI/CD internals, and optionally architecture or frontend topics). It is intentionally small and stays a learning vehicle, not a product.

## The idea: a tiny shop

- `catalog` lists products
- `inventory` tracks available stock
- `orders` places orders **after checking stock via an HTTP call to inventory**
- `frontend` shows products with stock and lets you place orders

The `orders → inventory` call is the first real cross-service dependency. It creates the failure modes (inventory down, insufficient stock) that make CI/CD, fallback, security, and testing experiments meaningful.

## Structure

```text
microservices-playground/
├── services/
│   ├── catalog/          ASP.NET Core minimal API (C#)
│   ├── inventory/        ASP.NET Core minimal API (C#)
│   └── orders/           ASP.NET Core minimal API (C#)
├── frontend/             React (Vite + TypeScript)
├── docker-compose.yml    runs everything locally
└── .github/workflows/    CI (GitHub Actions)
```

## Services

| Service   | Port | Endpoints                                       |
|-----------|------|-------------------------------------------------|
| catalog   | 8081 | `/health`, `/items`                             |
| inventory | 8083 | `/health`, `/stock`, `/stock/{itemId}`          |
| orders    | 8082 | `/health`, `/orders` (GET), `/orders` (POST)    |
| frontend  | 5173 | fetches from all three services                 |

In-memory data only. No databases yet.

Known failure modes by design:

- `POST /orders` with insufficient stock → `409`
- `POST /orders` while inventory is down → `503`

## Run locally

With Docker (recommended):

```bash
docker compose up --build
```

Without Docker (three terminals):

```bash
cd services/catalog && dotnet run
cd services/inventory && dotnet run
cd services/orders && dotnet run     # needs INVENTORY_URL for the stock check (defaults to localhost:8083)
cd frontend && npm install && npm run dev
```

Requires the .NET 10 SDK and Node 22.

## CI

The GitHub Actions workflow (`.github/workflows/ci.yml`) has one build job per component. The concepts learned here transfer directly to the GitLab CI/CD pipeline in the group project.

## Intended experiments

- Runner behavior: observe what the CI runner spawns while this pipeline executes
- YAML-to-command mapping: study how each job becomes shell invocations
- Coverage instrumentation: measure instrumentation overhead on the services
- Changed-file detection: test `git diff` edge cases against this project's history
- Fallback rules: design safe test selection for this project's test suite
- Service failure handling: what happens to orders when inventory is down
- (Optional) architecture / frontend experiments as a secondary track

Each experiment lives in the top-level `experiments/` directory and references this project.
