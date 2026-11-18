#!/usr/bin/env bash
set -euo pipefail

REPO_ROOT="$(cd "$(dirname "$0")/.." && pwd)"
CONTRACTS_DIR="$REPO_ROOT/contracts"
SERVICES_DIR="$REPO_ROOT/projects/microservices-playground/services"
TMP_DIR="${TMPDIR:-/tmp}/contract-check"
mkdir -p "$TMP_DIR"

UPDATE_MODE="${1:-check}"

check_service() {
  local name="$1"
  local port="$2"
  local csproj
  csproj=$(ls "$SERVICES_DIR/$name"/*.Api.csproj)
  local contract="$CONTRACTS_DIR/$name-openapi.json"
  local generated="$TMP_DIR/$name-openapi.json"

  echo "== $name: generating OpenAPI document =="
  dotnet run --project "$csproj" --urls "http://localhost:$port" >"$TMP_DIR/$name-run.log" 2>&1 &
  local pid=$!

  for _ in $(seq 1 40); do
    if curl -sf "http://localhost:$port/openapi/v1.json" -o "$generated"; then
      break
    fi
    sleep 1
  done

  kill "$pid" 2>/dev/null || true
  wait "$pid" 2>/dev/null || true

  if [[ ! -s "$generated" ]]; then
    echo "ERROR: could not fetch OpenAPI document for $name"
    cat "$TMP_DIR/$name-run.log"
    return 1
  fi

  if [[ "$UPDATE_MODE" == "--update" ]]; then
    cp "$generated" "$contract"
    echo "UPDATED: $contract"
    return 0
  fi

  if ! cmp -s "$generated" "$contract"; then
    echo "ERROR: OpenAPI contract drift detected for $name"
    diff <(python3 -m json.tool "$contract") <(python3 -m json.tool "$generated") | head -60 || true
    return 1
  fi

  echo "OK: $name matches committed contract"
}

failed=0
check_service inventory 18183 || failed=1
check_service orders 18182 || failed=1

exit $failed
