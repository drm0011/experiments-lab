# Kubernetes (initial groundwork)

Manifests to run the tiny shop on Kubernetes. **Status: manifests only — no cluster yet, images not yet published.**

## Files

| File           | What it defines                                  |
|----------------|--------------------------------------------------|
| `catalog.yaml` | Deployment + Service for the catalog service     |
| `inventory.yaml` | Deployment + Service for the inventory service |
| `orders.yaml`  | Deployment + Service (with `INVENTORY_URL`)      |
| `frontend.yaml` | Deployment + Service for the React frontend     |

## To make this runnable (next steps)

1. **Publish images to GHCR** (`ghcr.io/drm0011/playground-*`) from the per-service pipelines
2. **Create a local cluster** — e.g. `kind` (Kubernetes in Docker) on this machine
3. `kubectl apply -f k8s/` and verify pods reach each other (`orders` → `inventory` via service DNS)

## Known gaps

- Frontend fetches `http://localhost:8081/8082/8083` at build time — inside a cluster those are wrong; needs build-time `VITE_*_URL` values or an ingress/proxy
- No readiness/liveness probes yet
- No ingress; services are ClusterIP-only
- `:latest` tags — versioning comes with the GHCR publishing step
