# Semester Learning Lab

This repository contains my personal software experiments and learning activities for the semester — small, focused, evidence-based experiments, not one large application.

## Semester Context

My main project (approximately 4.5 days per week) is the semester-long group project:

> **Automated Impacted Test Case Selection and Execution in GitLab CI/CD**

The project selects and runs only the tests affected by a merge request's changes, using coverage data to map source files to tests, with conservative fallbacks when the data is missing or stale.

My personal track (approximately 0.5 day per week) complements the group project. It explores what a group project at this scale rarely allows:

* designing and operating CI/CD for a **microservices architecture**
* DevOps and DevSecOps practice
* discovering which technical direction fits me as a future career choice

## Research Question

> **How  is a CI/CD pipeline for a microservices application — covering DevSecOps practice designed — so that the pipeline is fast, reliable, and safe to trust?**

### Sub-questions

* **SQ1 — Pipeline design:** How should CI/CD be structured for a multi-service system — per-service builds, service dependencies, container images, and delivery steps — so changes are validated quickly without coupling the services back together?
* **SQ2 — Security:** Which security practices should a microservices CI/CD pipeline include — dependency and image scanning, secret handling, least-privilege configuration — and how do they integrate without blocking development?
* **SQ3 — Trustworthiness (reliability):** How can the pipeline be trusted in practice — reproducible environments, caching, selective runs with safe fallbacks — and how do I measure that trust instead of assuming it?

## The Microservices Project

`projects/microservices-playground/` — a tiny shop that serves as the vehicle for the research question:

| Service   | Port | Endpoints                          |
|-----------|------|------------------------------------|
| catalog   | 8081 | `/health`, `/items`                |
| inventory | 8083 | `/health`, `/stock`, `/stock/{id}` |
| orders    | 8082 | `/health`, `/orders` (GET/POST)    |
| frontend  | 5173 | React (Vite + TypeScript) SPA      |

* Three ASP.NET Core minimal APIs (.NET 10) plus one React frontend, each with its own Dockerfile
* One real cross-service dependency: `orders` checks `inventory` stock over HTTP before accepting an order — `409` when out of stock, `503` when inventory is down
* In-memory data only; deliberately no gateway, messaging, auth, or database yet — these gaps are future experiment subjects
* Docker Compose runs everything locally; GitHub Actions CI (`.github/workflows/ci.yml`) builds each component

## Learning Outcomes

The experiments support the school learning outcomes through the HBO-i framework:

* **Engineering approach:** every experiment follows Question → Hypothesis → Experiment → Measurement → Analysis → Conclusion, producing measurable evidence
* **System thinking:** investigating how pipeline configuration, runner execution, service dependencies, and security controls interact
* **Informed decision making:** design trade-offs (speed vs safety, isolation vs convenience) justified with evidence
* **Software quality:** maintainability, reliability, security, and reproducibility treated as first-class concerns
* **Professional standard:** applied research with documented limitations and appropriate uncertainty
* **Personal leadership:** using experiments to discover which professional direction fits me

## Experiment Conventions

* Each experiment lives in `experiments/NN-name/` with `README.md`, `src/`, `results/`, `notes/`
* Experiments are small and finishable within 1–2 sprints
* Results are captured as evidence (terminal output, artifacts, logs, timings), not assertions
* Every experiment ends with a conclusion and a next-step decision

## Current Experiments

* **01 — Linux Developer Toolbox** *(completed sprint 1, parked)*: a Bash CLI for project inspection
* **02 — CI Runner Under the Hood** *(in progress)*: what the GitHub Actions runner actually is — environment dump, job parallelism, first findings captured

## Next Experiment Candidates

* NuGet lock files and dependency caching
* Changed-file detection and selective per-service builds
* Tests for the services, then selective test runs with fallback rules
* Container image publishing (GHCR) and versioning
* Dependency and image vulnerability scanning
* Secrets handling and least-privilege CI configuration

## AI-Assisted Development

AI tools may be used, but AI output is a hypothesis until validated:

```text
AI suggestion → my implementation → test/experiment → evidence → accept/modify/reject
```

## Reflection

At regular intervals I ask: what did I learn? what surprised me? which area did I enjoy most? which direction do I want to investigate next? The answers steer both the experiments and my career choice.
