# Semester Learning Lab

Personal software experiments and learning activities for the semester — small, focused, evidence-based experiments.

## Semester Context

Group project (4.5 days/week): **Automated Impacted Test Case Selection and Execution in GitLab CI/CD** (Thermo Fisher Velox).

Individual track (1 weekday + 1 weekend day): **the X-Clone** — a Twitter-style system built as C#/.NET microservices, to explore enterprise software: distributed data, communication patterns via message brokers, security, and the CI/CD machinery that delivers it all.

Two investigation tracks:

1. **CI/CD for microservices — DevSecOps** *(complete, evidence archived)*: per-service pipelines, Trivy/gitleaks gates, OpenAPI contract drift gate, GHCR publishing, ephemeral-cluster deploy-smoke, local k3s with self-healing
2. **Enterprise software — distributed data & communication patterns** *(active)*: the X-Clone

## Research Questions

### Track 1 — DevSecOps & delivery (complete)

> How can a CI/CD pipeline for a multi-service system enforce security and reliability automatically — so that unsafe artifacts are blocked, every change is deployment-validated, and failures are detected and recoverable?

Evidence: `experiments/03-deploy-and-secure` … `06-ghcr-publishing` (see below).

### Track 2 — Enterprise software (active)

> How can distributed-data consistency problems in a multi-service system be solved with asynchronous communication patterns — so that no message is lost, no duplicate causes harm, and services converge to a consistent state?

- **SQ1 Patterns & boundaries:** When must a data change become an *event* — and what must the event carry?
- **SQ2 Message reliability:** How do outbox + durable queues + idempotent consumers + dead-lettering guarantee at-least-once without harm?
- **SQ3 Security in the data lifecycle:** What happens to data when an account is deleted — deletion events, erasure, least privilege?
- **SQ4 Frontend operability:** How can a frontend make a distributed backend operable (event feed, failure states, DLQ inspection)?

## The X-Clone (`projects/x-clone/`)

Single-purpose services, RabbitMQ between them, PostgreSQL per service:

| Service | Port | Responsibility |
|---|---|---|
| identity | 8091 | accounts, JWT auth → events: UserCreated, UserDeleted |
| post | 8092 | tweets create/delete → events: PostCreated, PostDeleted |
| follow | 8093 | social graph → events: FollowCreated, FollowDeleted |
| timeline | 8094 | materialized timelines ← consumes Post/Follow events |
| frontend | 5174 | X-style UI (React + Vite + TS) |
| rabbitmq | 5672/15672 | message broker (management UI) |
| postgres | 5433 | one database per service (dev: single instance, 4 databases) |

Enterprise patterns in scope: fan-out on write · outbox · idempotent consumers · dead-letter queues · event-carried state · eventual consistency · API gateway (later) · JWT auth.

NFRs (targets): post write p95 < 500ms · timeline read p95 < 300ms · fan-out ≥ 100 posts/s · Trivy HIGH/CRITICAL = fail · deletion purge everywhere ≤ 60s · timeline readable when a service is down · OpenAPI contracts + drift gate per service.

## Archived: the Tiny Shop (LO3 evidence)

`projects/microservices-playground/` + experiments 03–06 remain as the completed CI/CD/DevSecOps track (per-service pipelines, security gates, GHCR, deploy-smoke, k3s self-healing). The shop's k8s manifests live in `projects/microservices-playground/k8s/`. Read the shop README for how to run it.

## Experiment Conventions

Each experiment lives in `experiments/NN-name/` with `README.md`, `src/`, `results/`, `notes/` — small, finishable in 1–2 sessions, results captured as evidence, ending with a conclusion and next-step decision.

## Scrum Board

Work is tracked as GitHub Issues in user-story form (acceptance criteria + Definition of Done), with board labels `board::backlog`, `board::in-progress`, `board::done` — https://github.com/users/drm0011/projects/1

## AI-Assisted Development

AI output is a hypothesis until validated: suggestion → implementation → test/experiment → evidence → accept/modify/reject.
