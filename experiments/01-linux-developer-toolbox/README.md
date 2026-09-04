# Experiment 01: Linux Developer Toolbox

## Question

What development tasks can Linux's CLI and tooling make easier to automate, and what shell concepts (arguments, exit codes, pipes, command substitution, permissions) are needed to build a reliable CLI tool?

## Context

I am working on Ubuntu for the first time this semester. My background is C#/.NET application development, where I rarely interact with the operating system directly. The group project (impacted test selection in GitLab CI/CD) gives me experience with CI automation, but not with the shell layer underneath it.

This is the first experiment in the planned progression: Linux -> processes -> memory -> performance -> systems programming -> compilers -> architecture. Building a small developer CLI tool is a realistic way to learn shell scripting through a real need instead of memorizing commands.

## Hypothesis

Most repetitive development tasks (getting a project overview, finding and cleaning build artifacts, timing a command) can be automated with small, reliable Bash scripts. I expect the main challenges to be error handling, safety (not deleting the wrong files), and argument parsing rather than the happy path.

## Experiment

Build a single Bash CLI tool called `devtool` with subcommands, implemented incrementally:

- **Sprint 1 (this session):** scaffold + `devtool info` - prints a project overview (name, git branch, status, recent commits, file-type counts, disk size). Also `--help` and `--version`.
- **Sprint 2 (next session):** `devtool clean` (find build artifacts, dry-run by default, require `--yes` to delete) and `devtool bench` (run a command N times, report min/mean/max).

Each subcommand is a step toward a usable tool, and each forces contact with specific shell features (recorded in notes/).

## Measurement

- Terminal output of each subcommand run against real projects (captured in `results/`).
- Exit codes verified after each invocation.
- Notes per session documenting: which shell concepts were used, where I got stuck, and what I had to look up.
- For `clean`: verify dry-run never modifies the filesystem; verify `--yes` removes only intended artifacts.
- For `bench`: compare timing distributions (min/mean/max) across runs to demonstrate reliability.

## Results

*(to be filled in during/after each sprint - evidence in `results/`)*

## Analysis

*(to be filled in)*

## Conclusion

*(to be filled in)*

## Limitations

*(to be filled in)*

## Engineering Implication

*(to be filled in)*

## Learning Outcome

- Engineering approach: measurable feedback loop (build -> run -> capture output -> reflect) per session.
- Progressive steps: subcommands added one sprint at a time, each building on shell concepts from the previous.
- Software quality: exit codes, safe defaults (dry-run), and testable behavior as first-class concerns.
- Delivery & operations: automation and command-line workflows beyond GitLab YAML.

## Next Step

*(to be filled in - candidate: Experiment 02 "What Actually Happens When I Run a Program?" per the progression, or continue the tool if a real need appears)*
