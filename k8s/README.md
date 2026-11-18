# Kubernetes (k3s via k3d)

The tiny shop runs on a local k3s cluster. **Status: deployed and verified.**

## Current state

- Cluster: k3s v1.35.5 via k3d v5.9.0 (single node), created with host port mappings
- All four services deployed from these manifests, all pods Ready with readiness/liveness probes
- In-cluster service discovery verified: `orders` reaches `inventory` at `http://inventory:8080`
- Self-healing verified: pod deletion → brief 503 → automatic recovery
- Evidence: `experiments/04-local-kubernetes/results/`

## Run it

```bash
k3d cluster list                                   # is 'playground' running?
k3d cluster start playground                       # if stopped
kubectl get pods                                    # all 4 Ready?

# first time only: images must be imported into the cluster
k3d image import microservices-playground-catalog:latest \
                 microservices-playground-inventory:latest \
                 microservices-playground-orders:latest \
                 microservices-playground-frontend:latest -c playground
kubectl apply -f k8s/
```

Browser: http://localhost:5173 (APIs on 8081/8082/8083 via NodePort mappings).

## Self-healing demo

```bash
kubectl delete pod -l app=inventory
curl -X POST http://localhost:8082/orders -H "Content-Type: application/json" -d '{"itemId":1,"quantity":1}'   # 503
kubectl get pods -w                                 # watch it come back
# then the same POST returns 200
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
