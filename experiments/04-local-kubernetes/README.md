# Experiment 04: Local Kubernetes (k3s via k3d)

## User story

As a developer, I want the tiny shop running on a real Kubernetes cluster on my laptop, so I can demonstrate orchestration, DNS-based service discovery, and self-healing as the deployment target of my CI/CD pipelines.

## Acceptance criteria

- [x] k3s cluster running locally (k3d, no sudo) with kubectl access
- [x] All four services deployed and healthy from `k8s/` manifests
- [x] orders reaches inventory via cluster DNS (`http://inventory:8080`) — proven by a successful order
- [x] Readiness/liveness probes on every service
- [x] Self-healing observed: deleting the inventory pod produces a 503, then automatic recovery
- [x] Browser demoable: frontend + APIs reachable from the host

## Definition of Done

- [x] Evidence committed in `results/` (deploy verification + self-healing capture)
- [x] Manifests updated in `k8s/` (local images, probes, NodePort services)
- [x] README updated with status and how-to-run

## Context

The CI/CD story ends at a published image; Kubernetes is what makes the "D" real. This experiment deploys the shop to k3s (the production-shaped runtime) without waiting for GHCR publishing, by importing locally built images into the cluster.

## Hypothesis

A single-node k3s cluster on the laptop is enough to demonstrate the orchestration behaviors that matter: service discovery by DNS name, health-probe-driven traffic and restarts, and self-healing after pod deletion.

## Experiment

1. Install kubectl + k3d as user-level binaries
2. Create a k3d cluster with host port mappings for browser access (NodePort services)
3. Update manifests: local image names + `imagePullPolicy: IfNotPresent`, readiness/liveness probes, NodePort services
4. Import the four images, `kubectl apply -f k8s/`, verify all pods Ready
5. Prove in-cluster DNS: `POST /orders` succeeds (orders calls `http://inventory:8080`)
6. Prove self-healing: delete the inventory pod, observe 503 during the gap, then 200 after automatic restart

## Results

- Cluster: k3s v1.35.5 (k3d v5.9.0), single node `Ready`
- All 4 pods `Running` and `Ready` (probes passing) — `results/01-deploy-verify.txt`
- `POST /orders` → **200** — in-cluster service discovery works (orders → inventory by DNS name)
- `POST /orders` with out-of-stock item → **409** — failure modes carry over to the cluster unchanged
- Self-healing: `kubectl delete pod` on inventory → immediate `503 "inventory unavailable"` → new pod Ready ~seconds later → **200** — `results/02-self-healing.txt`
- Browser demo works: frontend on `localhost:5173`, APIs on `8081/8082/8083` (NodePort mappings)

## Issues encountered & how they were solved

1. **Image choice:** k3d's containerd cannot see Docker images — solved with `k3d image import`, which also sidesteps the not-yet-existing GHCR publishing step
2. **Browser access:** in-cluster DNS names are unreachable from the browser — solved with NodePort services + k3d host port mappings so the frontend's existing `localhost` URLs keep working without a rebuild; an ingress (Traefik) remains the cleaner future option
3. **Port-forward vs NodePort:** port-forwarding a Service breaks when the pod restarts — NodePort is stable, which matters for the self-healing demo

## Analysis

The system behaves identically to the Compose version, but the *responsibilities moved*: Compose told Docker to run 4 containers; Kubernetes now maintains the desired state (Deployment), provides stable addressing (Service + CoreDNS), health semantics (probes), and recovery (ReplicaSet). The 503 gap in the self-healing capture is the honest observable: orchestration does not make failures impossible, it makes recovery automatic and testable.

## Limitations

- Single-node cluster: no scheduling/topology behavior demonstrated
- `latest` images and imported (not pulled) images — GHCR publishing is still required before CI can drive deployments
- NodePort is not production-style ingress; Traefik ingress is deferred
- No persistent data yet (in-memory state resets on pod restart — visible in the demo)

## Learning Outcome mapping

- **LO1 — Software Engineering, System thinking:** analyzed communication patterns (sync HTTP via cluster DNS vs Compose hostnames), system boundaries (what the browser can reach vs what pods can reach), and the impact of data complexity (state loss on restart) on design trade-offs
- **LO1 — Informed decision making:** justified choices with context — k3d over systemd k3s (no sudo, disposable), NodePort over port-forward (pod-restart stability), image import over GHCR (unblocks before publishing exists)
- **LO3 — Software Delivery & Operations:** deployed the software in a running environment and monitored desired behavior (probes, self-healing observation)

## Next step

Publish images to GHCR, then an ephemeral k3d deploy-smoke job in CI so every change is deployment-validated.
