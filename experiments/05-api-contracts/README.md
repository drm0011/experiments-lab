# Experiment 05: API Contracts (orders ↔ inventory)

## User story

As a developer, I want a committed OpenAPI contract between orders and inventory with a CI gate that fails on drift, so cross-service API changes are detected even though path filters only see directory changes.

## Acceptance criteria

- [x] inventory and orders expose OpenAPI documents (`/openapi/v1.json`)
- [x] Committed contract snapshots in `contracts/`
- [x] `scripts/check-contracts.sh` regenerates the documents and fails on any drift
- [x] CI: `contracts.yml` runs the check; contract changes also trigger the dependent pipelines (inventory's contract triggers orders' pipeline)
- [x] Drift detection proven both ways (clean pass, simulated drift fails)

## Definition of Done

- [x] Evidence committed in `results/` (clean check + drift detection)
- [x] LO mapping recorded below
- [x] Board issue updated

## Context

Path-filtered per-service pipelines solve build isolation but miss *behavioral* dependencies: orders depends on inventory's `/stock/{id}` response shape, yet a change to inventory never triggers orders' pipeline. The committed contract makes that dependency visible to CI: the contract file is a build-system-like trigger.

## Experiment

1. Expose OpenAPI on both services (`AddOpenApi()` / `MapOpenApi()`, package `Microsoft.AspNetCore.OpenApi` 10.0.0)
2. Script that runs each service, fetches its OpenAPI document, and compares it to the committed snapshot (`--update` mode creates snapshots)
3. CI workflow `contracts.yml` runs the check on service/contract changes; `orders.yml` gains `contracts/inventory-openapi.json` as a trigger path (and `inventory.yml` its own contract)

## Results

- Contracts generated: `contracts/inventory-openapi.json`, `contracts/orders-openapi.json`
- Clean check: exit 0, both services match — `results/01-clean-check.txt`
- Simulated drift (title tampered): exit 1, diff shows the change — `results/02-drift-detected.txt`
- CI workflow green on this push

## Issues encountered & how they were solved

1. **`AddOpenApi` not found** — assumed it was in the .NET 10 shared framework (it was in .NET 9); the local `dotnet build` output masked the error. Solved by adding `Microsoft.AspNetCore.OpenApi` package, pinned to `10.0.0` (initial `dotnet add` picked 11.0.0, incompatible with the net10.0 target)
2. **Snapshot restore** — after the drift simulation, `git checkout` failed because the file was untracked; restored by re-running the script in `--update` mode

## Analysis

The contract gate converts an invisible runtime dependency into a visible, checked artifact. Two layers of protection now exist: drift fails the `contracts` pipeline, and a *committed* contract change triggers the *dependent* service's pipeline — the exact mechanism path filters were missing. This is the same class of problem as the group project's conservative fallbacks: making dependencies explicit so automation can react to them.

## Limitations

- The contract is the *provider's* description; consumer expectations (field names orders actually uses) are not yet asserted — consumer-driven contract tests remain open (backlog)
- Snapshot regeneration requires running each service (works in CI via `dotnet run` + curl)
- OpenAPI document contents can reorder between runs; byte-compare may be brittle if serialization ordering changes (observed stable so far)

## Learning Outcome mapping

- **LO1 — System thinking:** made an implicit inter-service dependency explicit (contract artifact + trigger paths); analyzed communication-pattern risk in the CI design
- **LO1 — Informed decision making:** chose provider-contract + drift gate as the first step, deferring consumer-driven tests with a recorded reason
- **LO3 — Software Delivery & Operations:** automated contract validation in the delivery process ("automate all")

## Next step

Consumer-driven contract tests (orders asserts the fields it consumes), then dependent-pipeline triggers for the full services graph.
