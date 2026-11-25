# Kubernetes (k3s via k3d)

The tiny shop runs on a local k3s cluster. **Status: deployed and verified, images pulled from GHCR.**

## Current state

- Cluster: k3s v1.35.5 via k3d v5.9.0 (single node), created with host port mappings
- All four services deployed from these manifests, all pods Ready with readiness/liveness probes
- **Images come from `ghcr.io/drm0011/playground-*:latest`** (published by the per-service pipelines after their Trivy gate) — `k3d image import` is no longer needed
- In-cluster service discovery verified: `orders` reaches `inventory` at `http://inventory:8080`
- Self-healing verified: pod deletion → brief 503 → automatic recovery
- Deployment is validated in CI: the `deploy-smoke` workflow spins up an ephemeral k3d cluster, applies these manifests, and verifies 200/409/503
- Evidence: `experiments/04-local-kubernetes/results/` and `experiments/06-ghcr-publishing/results/`

## Run it

```bash
k3d cluster list                                   # is 'playground' running?
k3d cluster start playground                       # if stopped
kubectl apply -f k8s/                              # deploys/pulls from GHCR
kubectl get pods                                    # all 4 Ready?
```

Browser: http://localhost:5173 (APIs on 8081/8082/8083 via NodePort mappings).

## Self-healing / failure demo

```bash
kubectl scale deploy/inventory --replicas=0        # browser order -> 503 (guaranteed window)
kubectl scale deploy/inventory --replicas=1        # ~10s -> order works again
kubectl delete pod -l app=inventory                # or: watch ~2s self-healing
kubectl get pods -w
```

## Files

| File           | What it defines                                  |
|----------------|--------------------------------------------------|
| `catalog.yaml` | Deployment + NodePort Service (probes on `/health`) |
| `inventory.yaml` | Deployment + NodePort Service (probes on `/health`) |
| `orders.yaml`  | Deployment + NodePort Service, `INVENTORY_URL=http://inventory:8080` |
| `frontend.yaml` | Deployment + NodePort Service (probes on `/`) |

## Known gaps (backlog)

- Images are imported locally; GHCR publishing is needed before CI can drive deployments
- NodePort instead of ingress — Traefik (bundled with k3s) is the next step
- No persistent data: in-memory state resets on pod restart
- `:latest` tags — versioning comes with GHCR
