# Experiment 06: GHCR Publishing + Deploy-Smoke in CI

## User story

As a developer, I want each pipeline to publish its scanned image to GHCR and then deploy the whole shop into an ephemeral Kubernetes cluster to smoke-test the failure modes, so delivery is automated end-to-end: build → scan → publish → deploy → verify.

## Acceptance criteria

- [x] All 4 pipelines publish sha-tagged + `latest` images to `ghcr.io/drm0011/playground-*`
- [x] Only Trivy-passing images are published (publish runs after the scan gate)
- [x] Workflow permissions set to read/write with least-privilege `permissions:` blocks (only service pipelines get `packages: write`)
- [x] `deploy-smoke` workflow: ephemeral k3d cluster in Actions, manifests applied, 200/409/503 verified, cluster torn down
- [x] Laptop cluster pulls from GHCR (no more `k3d image import`)
- [x] Anonymous `docker pull` works (public packages)

## Definition of Done

- [x] Evidence: smoke-results artifact committed in `results/`
- [x] READMEs updated; board issues moved to Done
- [x] LO mapping recorded

## Results

- All 7 workflows green (`catalog`, `inventory`, `orders`, `frontend`, `contracts`, `security`, `deploy-smoke`)
- `docker pull ghcr.io/drm0011/playground-catalog:latest` works without authentication
- deploy-smoke run: 200 → 409 → 503 (scale 0) → recovery (scale 1) — `results/smoke-results.txt`
- Laptop cluster: manifests now point at `ghcr.io/drm0011/playground-*:latest`; rollout restarted, pods pulled from the registry, order flow verified

## Finding: transient 503 after pod replacement (stale connection pool)

The first deploy-smoke run failed its final assertion: after scaling inventory back to 1 and waiting for Ready, the first POST still returned 503. Cause: `IHttpClientFactory` pools the underlying HTTP handler indefinitely, so `orders` reuses a pooled connection to the *old* (deleted) pod IP until that connection errors and is evicted. Recovery is therefore not instant even when the pod is Ready.

- Fixed the test to assert recovery with retries (up to 10 attempts, 2s apart) — the transient 503 is expected and documented
- **This validates the resilience backlog item with real data:** adding Polly's `AddStandardResilienceHandler` (retry + pooled connection lifetime) is the proper fix, not a test workaround

## Issues encountered & how they were solved

1. **Package write denied** — repo default was `default_workflow_permissions: read`; changed via API (`PUT actions/permissions/workflow`) and scoped with per-workflow `permissions:` blocks
2. **Stale connection after pod replacement** — see finding above
3. **k3d in CI** — installed k3d/kubectl as binaries (no sudo prompts, pinned k3d v5.9.0), port-forwarded `orders` for the smoke calls (NodePort host mappings don't apply in CI)

## Analysis

The delivery loop is now closed: a change to any service ends in a published, scanned image and a real deployment into a disposable cluster with the failure modes verified. The only remaining manual step is applying manifests to the persistent laptop cluster — which is a choice (a persistent cluster is not automatable from CI without a hosted cluster).

## Limitations

- `latest` moves on every push; sha-tags are the reproducible reference but are not yet used by the manifests
- deploy-smoke runs only on `main`; pull requests are not deployment-validated (backlog option)
- The smoke test covers the HTTP failure modes, not UI behavior
- No image signing yet (Cosign backlog)

## Learning Outcome mapping

- **LO3 — Maintainable:** designed/improved an automate-all delivery process (build → scan → publish → deploy → verify)
- **LO3 — Deploy in a running environment + monitor behavior:** deployment is validated automatically per change, with failure modes observed
- **LO1 — System thinking:** diagnosed the stale-connection finding across boundaries (app HTTP pooling ↔ k8s pod replacement) with evidence
- **LO1 — Informed decision making:** least-privilege token scoping, ephemeral-cluster validation strategy, retry-assertion vs code-fix trade-off recorded

## Next step

This completes the CI/CD delivery loop for the microservices track. Remaining backlog: Cosign signing, tests + selective runs, Postgres/messaging — tracked on the scrum board. The personal track now pivots to the LO1 Binary & Coverage Foundations direction.
