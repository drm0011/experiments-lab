# experiments-lab — Working conventions

## Code style (C#)
- Use explicit types, not `var` — write out the actual type (e.g. `WebApplicationBuilder builder = ...`).
- Minimal APIs; layering only where it earns its keep (e.g. services with interfaces when tests need seams).

## Secrets
- No plaintext credentials in k8s manifests — Kubernetes Secrets via `secretKeyRef`; dev values live in GitHub repo secrets and in `k8s/secret.example.yaml`.
- Local dev defaults (compose/appsettings) are dev-only.

## Delivery (established, keep)
- Per-service GitHub Actions pipelines: path filters → build → docker build → Trivy (fail HIGH/CRITICAL) → publish GHCR (sha + latest).
- deploy-smoke: ephemeral k3d in CI validates the stack per push.
- OpenAPI contracts per service with drift gate.
- Commit style: short imperative messages. Push when asked.

## Workflow
- GitHub Projects board (https://github.com/users/drm0011/projects/1): user stories with AC + DoD; one story = vertical slice through the stack before Done.
- Experiments: `experiments/NN-name/` with README/results/notes; evidence committed.
