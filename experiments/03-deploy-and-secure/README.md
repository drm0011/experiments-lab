# Experiment 03: Deploy & Secure (DevSecOps groundwork)

## Question

Can vulnerability and secret scanning be integrated into per-service CI/CD pipelines so that unsafe images and leaked secrets fail the build — without blocking development?

## Context

The research question demands DevSecOps practice, but the project had zero security evidence: Dockerfiles ran as root with unpinned base images, and no scanner existed in CI. This experiment closes that gap: harden the images, scan them, fix findings, and gate the pipelines on HIGH/CRITICAL findings.

## Hypothesis

The hardened .NET images will scan clean. The frontend image will have findings because Node toolchains bundle many third-party binaries; npm audit can fix the app-level findings, but some may remain in the base image's own tooling and need an ignore with justification.

## Experiment

1. Pin base image digests and run containers as non-root (`app` user in .NET images, `node` in the frontend image)
2. Install Trivy + Gitleaks locally; scan all four images and the repository
3. Fix findings (dependency bumps) and rebuild until scans are clean at HIGH/CRITICAL
4. Add a Trivy job (fail on HIGH/CRITICAL) to each service pipeline and a Gitleaks workflow

## Results

- All containers now run as non-root: `app` (catalog, inventory, orders), `node` (frontend) — verified with `docker exec ... whoami`
- `.dockerignore` files added (build artifacts no longer enter build contexts)
- Initial scans: catalog/inventory/orders images **clean** (0 HIGH/CRITICAL)
- Frontend image: 12 HIGH (`source-map-js`) + 29 (27 HIGH, 2 CRITICAL — `esbuild` vendored Go binary)
- Fixes: bump `vite`/`@vitejs/plugin-react` (source-map-js + esbuild resolved), `npm install -g npm@latest`, base image `node:22-alpine` → `node:24-alpine`
- After fixes: 3 HIGH remain — `undici@6.29.0` bundled inside npm itself (fixed upstream in 6.29.1, npm ships the older version). Documented in `.trivyignore` with justification; re-check on base image bumps
- Gitleaks on full repo history: **0 leaks**
- Evidence: `results/trivy-*.txt`, `results/trivy-frontend-after-fix.txt`

## Analysis

Hardened bases are not enough — the frontend's risk lives in the Node toolchain, not the app. The fix loop (scan → bump deps → rescan) worked for app-level findings, but one residual class remains: tools bundled by the base image maintainers (npm's undici pin). Ignoring requires a written justification, not silence — the `.trivyignore` file carries it.

## Conclusion

Image + secret scanning fits naturally into per-service pipelines; the gate (HIGH/CRITICAL → fail) is now enforced on every push. DevSecOps is no longer an empty claim: the evidence is committed in `results/`.

## Limitations

- Local scans ran on this laptop; CI scans run in the Actions environment (same Trivy DB versioning applies)
- LOW/MEDIUM findings are not gated yet
- The frontend runs a dev server in its image — a production frontend would use a static server (nginx/caddy), which changes the attack surface

## Engineering Implication

For the group project's pipeline: an equivalent scan step (e.g., Trivy in GitLab CI) on the Velox test images would catch vulnerable base images before they spread — same pattern, different runner.

## Learning Outcome

- Software quality/security: scanning as a pipeline gate with evidence
- Informed decision making: accept-vs-fix-vs-ignore decision for residual CVEs, documented
- HBO-i: evidence for the security competency

## Next Step

Publish images to GHCR (scanned images are worth publishing), then deploy to a kind cluster.
