# X-Clone

A Twitter-style system built as C#/.NET microservices — the active project for the enterprise-software track (distributed data, communication patterns, security, frontend operability).

## Status

**Skeleton scaffolded**: 4 service skeletons (health + OpenAPI), frontend skeleton, RabbitMQ + Postgres compose, k8s manifests, per-service CI pipelines with Trivy gates and GHCR publishing, deploy-smoke in an ephemeral cluster. **Vertical slice coding starts Day 3.**

## Architecture

```
browser → frontend (5174)
              ├─ identity  (8091)  accounts, JWT → events UserCreated/UserDeleted
              ├─ post      (8092)  tweets → events PostCreated/PostDeleted
              ├─ follow    (8093)  social graph → events FollowCreated/FollowDeleted
              ├─ timeline  (8094)  materialized timelines ← consumes Post/Follow events
rabbitmq (5672, mgmt 15672) · postgres (5433: one database per service)
```

## Run locally

```bash
docker compose up --build        # full stack incl. RabbitMQ + Postgres
# browser: http://localhost:5174 · RabbitMQ UI: http://localhost:15672 (guest/guest)
```

Without Docker (services): `cd services/<name> && dotnet run` (ports 8091–8094). Frontend: `cd frontend && npm run dev`.

## CI/CD (reused from the shop track)

- One pipeline per service: path-filtered, build → docker build → Trivy gate (HIGH/CRITICAL) → publish to `ghcr.io/drm0011/xclone-*`
- `deploy-smoke`: ephemeral k3d cluster in Actions, deploys `k8s/`, health-checks all services, teardown
- `security`: gitleaks on every push

## Kubernetes

Manifests in `k8s/` (repo root): Deployment + Service per service (probes on `/health`), RabbitMQ, Postgres with PVC. The laptop k3d cluster currently runs the archived shop; the X-Clone gets deployed to it when the vertical slice lands.

## Day plan

| Day | Scope |
|---|---|
| 1 | User stories + AC on the board; NFR targets; architecture decisions |
| 2 | Cleanup + scaffold + CI port (done — this skeleton) |
| 3 | Vertical slice: identity (JWT) + post + own timeline + frontend "post a tweet" |
| 4 | follow + timeline fan-out: RabbitMQ spine, outbox, idempotent consumers |
| 5 | Failure & lifecycle: DLQ, consumer-down catch-up, UserDeleted purge, deploy-smoke assertions |
| 6 | NFR measurement vs targets, evidence docs, LO mapping |

## NFR targets

post write p95 < 500ms · timeline read p95 < 300ms · fan-out ≥ 100 posts/s · Trivy HIGH/CRITICAL = fail · UserDeleted purge everywhere ≤ 60s · timeline readable when a service is down · OpenAPI contracts + drift gate.
