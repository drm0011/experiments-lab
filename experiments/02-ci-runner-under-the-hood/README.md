# Experiment 02: CI Runner Under the Hood

## Question

What actually happens between pushing a commit and my microservices pipeline finishing? Specifically: what does the GitHub Actions runner execute, what environment does it provide, and how do the YAML job definitions become shell commands?

## Context

The group project (impacted test selection in GitLab CI/CD) gives me pipeline-design experience, but I have never looked under the pipeline. This is the first CI/CD-internals experiment against the placeholder microservices project: catalog, inventory, orders (C#), and a React frontend, each with its own build job in GitHub Actions.

Knowing what the runner does matters because the group project's reliability requirements (conservative fallbacks, traceability) are decisions about how jobs actually execute: which machine runs them, what is installed, what is cached, and what state survives between jobs.

## Hypothesis

Each job runs in a fresh, isolated environment (a disposable VM with a fixed set of preinstalled tools). The YAML `run:` steps become shell invocations executed sequentially by a runner process, and anything not uploaded as an artifact or cached is lost when the job ends. I expect environment state (PATH, installed SDKs, working directory) to be predictable and to differ from my local machine.

## Experiment

1. Extend the playground CI with a `runner-info` job that dumps `uname`, OS release, PATH, available tools, and all environment variables, uploaded as a build artifact.
2. Enable dependency caching for the .NET and frontend jobs (NuGet global packages, npm cache).
3. Push and observe the run in the Actions tab: job ordering, parallelism, logs.
4. Download the `runner-info` artifact and compare the runner environment with my local Ubuntu machine.
5. Trigger a second run and compare job timings with and without warm caches.

## Measurement

- The captured `runner-info` artifact (stored in `results/`).
- Actions job logs and step durations (captured via `gh run view --log`).
- A diff-like comparison between runner environment and local environment.
- Build timings across runs to observe the cache effect.

## Results

*(to be filled in after the run - evidence in `results/`)*

## Analysis

*(to be filled in)*

## Conclusion

*(to be filled in)*

## Limitations

*(to be filled in)*

## Engineering Implication

*(to be filled in)*

## Learning Outcome

- System thinking: CI configuration, runner execution, and environment state as one system.
- Engineering approach: observable evidence (artifact, logs, timings) instead of assumptions about CI.
- Delivery & operations: what "a CI job" actually is underneath the YAML.
- HBO-i: contributing evidence toward the operational/engineering competencies.

## Next Step

*(candidate: YAML → command mapping in detail, or dependency caching as a follow-up; then move toward tests and changed-file detection)*
