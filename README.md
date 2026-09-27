# Semester Learning Lab

## Linux, Systems Programming & Software Engineering

This repository contains my personal software experiments and learning activities for the semester.

The purpose of this repository is **not to build one large application**. Instead, it is a collection of small, focused, evidence-based experiments that help me strengthen areas of software engineering that I may not encounter deeply enough in my main group project.

The experiments should gradually move from practical software development and Linux usage toward systems programming, performance, computer architecture, and eventually the software/hardware boundary.

The central idea is:

> **Use software development as the main focus, Linux as the working environment, and progressively investigate what happens underneath software.**

---

# 1. Semester Context

I am working on a semester-long group project.

The group project concerns:

> **Automated Impacted Test Case Selection and Execution in GitLab CI/CD**

The project investigates how coverage-driven impact analysis can be integrated into a GitLab CI/CD pipeline to automatically identify and execute automated tests affected by source-code changes, while maintaining reliable and conservative validation.

The full automated test suite takes several hours to execute. The project therefore investigates whether coverage information can establish relationships between source files and test cases, allowing the CI/CD pipeline to execute a smaller set of relevant tests when appropriate.

The group project involves areas such as:

* GitLab CI/CD
* automated testing
* code coverage
* Bullseye coverage data
* test selection
* Git/Merge Request change detection
* JSON/data mappings
* CI automation
* reliability
* conservative fallback strategies
* performance
* maintainability
* traceability
* applied research
* software architecture
* stakeholder requirements

The group project is my main project and receives approximately **4.5 days per week**.

This repository represents my remaining personal development time, approximately **0.5 day per week**.

**Personal focus for this semester:** My first idea was to investigate "CI/CD internals" in the abstract, but feedback showed this is too general. Instead, I will build and maintain a small **placeholder microservices project** (C# services, React frontend) and use it as the concrete vehicle for diving deeper into CI/CD: what a runner actually executes, how YAML maps to commands, how coverage instrumentation alters binaries, and how changed-file detection computes its result. **The current priority is CI/CD in a microservices architecture.** The other experiment tracks (Linux toolbox, frontend, architecture) are parked for now and will be picked up only when they serve the CI/CD direction. I will use what I learn here to work toward my goals within the HBO-i framework.

---

# 2. Why This Repository Exists

The group project already gives me significant experience with **using** CI/CD: writing GitLab pipeline jobs, configuring test selection, and integrating coverage data. My personal experiments should therefore investigate the layers **beneath** that usage:

1. What does the GitLab Runner process actually do when it executes a job?
2. How is a `.gitlab-ci.yml` file parsed and translated into executable commands?
3. How does Bullseye instrumentation modify a binary, and what is the runtime cost?
4. How does Git compute a changed-file list, and what edge cases can break it?
5. How can test-selection logic be made deterministic, traceable, and safe to fail?

These questions are not duplicating the group project — they are investigating the **technical substrate** the group project depends on. They also serve the broader theme of this repository: understanding software more deeply by exploring the layers underneath it.

These questions will not be investigated in the abstract: they are exercised against a concrete **placeholder microservices project**, which also gives room to experiment with architecture or frontend development when that becomes more useful.

---

# 3. Main Learning Direction

My current technical background is primarily based around:

* C#
* .NET
* object-oriented programming
* application development

I want to broaden this toward:

* Linux
* terminal-based development
* software tooling
* scripting
* systems programming
* C++
* Rust
* performance engineering
* memory
* processes
* networking
* compilers
* assembly
* computer architecture
* RISC-V
* software/hardware interaction
* eventually semiconductor technology

The goal is **not** to become an electrical engineer during this semester.

The goal is to understand more of the computing stack beneath normal application development.

A rough conceptual progression is:

```text
Application Software
        ↓
Programming Languages
        ↓
Compilers / Toolchains
        ↓
Operating System
        ↓
System Calls
        ↓
Processes / Memory / Files / Networking
        ↓
Machine Code / Assembly
        ↓
Instruction Set Architecture
        ↓
CPU Architecture
        ↓
Digital Logic
        ↓
Hardware
        ↓
Semiconductors
```

This is a direction, not a strict curriculum.

The actual experiments should be selected based on what I need to learn at the time.

**This semester's experiments will apply this direction through a placeholder microservices project, using CI/CD as the primary case study.** The project may later serve as a vehicle for architecture or frontend experiments. In future semesters, I will broaden to other areas such as systems programming, performance engineering, and computer architecture.

---

# 4. The Main Theme

The overarching theme of this repository is:

> **Understanding software more deeply by progressively exploring the layers underneath it.**

Linux is an important part of this because I am using **Ubuntu for the first time this semester**.

I chose Ubuntu partly because I have an interest in Linux and because I have become dissatisfied with the amount of perceived bloat and unnecessary abstraction in Windows after using it for years.

However, I do **not** want to simply reinforce the belief that "Linux is better."

I want to investigate the question properly:

> **What are the actual technical reasons a software developer might choose Linux over Windows?**

This should be answered through experience and experiments rather than ideology.

Possible areas include:

* terminal workflows
* shell scripting
* package management
* developer tooling
* process management
* filesystem behavior
* permissions
* networking
* SSH
* automation
* reproducibility
* containers
* performance
* development environments
* system observability
* software portability

Linux should therefore become a **tool and environment for learning software engineering**, rather than merely another subject to memorize.

---

# 5. What This Repository Is NOT

This repository is **not** intended to become:

* a collection of random tutorials
* a list of Linux commands
* a giant application
* a collection of copied code
* a "100 Days of Coding" challenge
* a hardware-building project
* an Arduino project
* an FPGA purchasing project
* a mathematics-heavy research project
* a replacement for the group project
* a collection of experiments with no conclusions

The goal is **depth over quantity**.

One useful experiment that produces a genuine technical insight is more valuable than ten tutorials that I simply followed.

---

# 6. Constraints

The personal work is limited to approximately **0.5 day per week**.

Therefore:

* Experiments should be small.
* Experiments should have clear questions.
* Experiments should be finishable.
* Results should be documented.
* I should avoid creating unnecessary long-term maintenance.
* Hardware should not be required.
* Advanced mathematics should generally not be required.
* The work should remain primarily focused on software development.
* Experiments should be possible on my Ubuntu development machine whenever practical.

Hardware may eventually be discussed or simulated, especially when investigating computer architecture or semiconductor technology, but purchasing hardware should **not** be a prerequisite.

---

# 7. School Learning Outcomes

The personal experiments should contribute to the learning outcomes of my software engineering education.

These outcomes are important when deciding what to work on.

---

## 7.1 Software Engineering

> I apply an engineering approach in all my activities to create software systems in progressive steps, while applying system thinking during informed decision making.

Important aspects include:

### Engineering approach

I should demonstrate measurable feedback cycles.

Experiments should therefore preferably follow a pattern such as:

```text
Question
   ↓
Hypothesis
   ↓
Small experiment
   ↓
Measurement / observation
   ↓
Analysis
   ↓
Conclusion
   ↓
Next decision
```

### Progressive steps

I should make small steps toward larger goals.

An experiment should not attempt to solve everything at once.

### System thinking

I should investigate relationships between:

* components
* processes
* data
* tools
* operating systems
* infrastructure
* developers
* stakeholders
* system boundaries
* communication patterns

### Informed decision making

Technical choices should be justified based on:

* requirements
* constraints
* maintainability
* performance
* portability
* reliability
* development effort
* context

---

# 8. Software Quality Control

The experiments should also help me understand sustainable software quality.

Important areas include:

* maintainability
* scalability
* performance
* reliability
* security
* reproducibility
* diagnostics
* portability
* testability

I should investigate not only:

> "Does this work?"

but also:

> "How well does this work, under what conditions, and how can I demonstrate that?"

This is particularly relevant to experiments involving:

* different languages
* Linux tooling
* performance
* memory
* concurrency
* compilers
* system configuration
* development environments

---

# 9. Software Delivery & Operations

My group project already provides significant CI/CD experience at the pipeline-design level. Therefore, my personal work should investigate the **technical foundations underneath CI/CD**, rather than recreate another pipeline. Potential areas:

* What the GitLab Runner actually does (process model, job isolation, artifact handling)
* YAML parsing and command generation
* Coverage instrumentation at the binary level (Bullseye and alternatives)
* `git diff` internals and changed-file detection edge cases
* Test-selection algorithms and fallback-rule design
* Caching, artifacts, and reproducibility in CI environments
* Shell automation and Linux processes as they relate to CI execution
* Resource monitoring and debugging of CI jobs
* A placeholder microservices project as the concrete vehicle for these investigations

---

# CI/CD Internals — Guiding Questions

These are not mandatory experiments. They are questions I will return to when choosing what to investigate next. They are anchored to the placeholder microservices project rather than explored in the abstract.

**Runner and execution:**
- What process does GitLab Runner spawn for a job, and how does it differ between shell, Docker, and Kubernetes executors?
- How does a job script become a sequence of shell invocations?
- What happens to environment variables, working directory, and filesystem state between jobs?

**Coverage and instrumentation:**
- How does Bullseye coverage instrumentation modify a compiled binary?
- What is the runtime and memory overhead of instrumentation?
- How is per-test coverage data extracted, and what are its limitations?

**Change detection:**
- How does `git diff --name-only` actually compute its result?
- What edge cases can cause it to miss or over-report changes (renames, submodules, merge commits)?

**Test selection and fallback:**
- How can selection logic be made deterministic and traceable?
- What is the correct fallback behavior when coverage data is incomplete or stale?
- How can I measure whether a selective run is safe?

---

# Placeholder Project — Microservices Playground

Feedback on my first plan pointed out that investigating "CI/CD internals" in general is too vague. A concrete project makes each question measurable and keeps experiments grounded.

## What

A small microservices system, intentionally kept small (lives in `projects/microservices-playground/`):

* `catalog` service — ASP.NET Core (C#) minimal API
* `orders` service — ASP.NET Core (C#) minimal API
* `frontend` — React app that calls both services
* Docker Compose to run everything locally
* GitHub Actions CI to build, test, and later select impacted tests

## How it serves the experiments

Each CI/CD-internals question is investigated against this project:

* Runner behavior is observed while the project's pipeline executes
* YAML-to-command mapping is studied in the project's GitHub Actions workflow
* Coverage instrumentation is measured on the project's services
* Changed-file detection is tested against the project's repository history
* Fallback rules are designed for the project's test suite

## Flexibility

The project can also absorb experiments in other areas when useful:

* software architecture (service boundaries, communication patterns)
* frontend development (React build pipeline, testing)

## HBO-i

I will use what I learn here to work toward my goals within the HBO-i framework. Each experiment's README will note which HBO-i competencies it exercises.

## Scope guardrails

* In-memory data only at first — no databases
* One new capability at a time, matching the 0.5 day per week budget
* The project is a vehicle for learning, not a product

---

# 10. Professional Standard

The experiments should also provide opportunities for applied research.

I should practice:

* identifying useful questions
* prioritizing what is worth investigating
* choosing appropriate research methods
* collecting evidence
* evaluating reliability and validity
* documenting limitations
* reflecting on the research process
* communicating conclusions

I should avoid treating every experiment as proof that one technology is "better."

Instead:

> **Investigate → measure → compare → explain → conclude with appropriate uncertainty.**

The repository should eventually demonstrate that I can independently investigate unfamiliar technical subjects.

---

# 11. Personal Leadership

This repository is also part of my long-term professional development.

I am currently exploring what kind of software engineer I want to become.

Possible directions include:

* software engineering
* backend development
* systems programming
* DevOps/platform engineering
* performance engineering
* infrastructure
* developer tooling
* embedded/software-hardware development
* compiler/toolchain development
* computer architecture
* low-level software
* potentially semiconductor-related technology

I do not need to decide this immediately.

Instead, the experiments should help me discover what I enjoy and what I am good at.

At regular intervals I should ask:

* What did I learn?
* What surprised me?
* What do I understand now that I didn't before?
* What is still unclear?
* What did I enjoy?
* What did I dislike?
* What technical skill improved?
* What feedback did I receive?
* What should I investigate next?
* Is this area relevant to my long-term goals?

---

# 12. Potential Technical Areas

These are possible areas of exploration, not mandatory requirements.

## Linux

Primary environment and major learning area.

Potential topics:

* Bash
* filesystem
* permissions
* processes
* signals
* environment variables
* PATH
* package management
* systemd
* networking
* SSH
* logs
* system monitoring
* debugging
* shell pipelines
* command-line tooling

---

## Systems Programming

Potential languages:

* C
* C++
* Rust

Potential topics:

* memory
* pointers/references
* processes
* threads
* concurrency
* files
* sockets
* system calls
* error handling
* resource management
* performance

The purpose is to understand concepts that are hidden by higher-level frameworks such as .NET.

---

## Rust

Rust is a potential major language to explore because it provides a different perspective from C#.

Potential topics:

* ownership
* borrowing
* lifetimes
* memory safety
* error handling
* concurrency
* traits
* generics
* tooling
* Cargo
* interoperability with C/C++
* systems programming

The purpose is not simply to "learn Rust."

The deeper question is:

> **What can I learn about software engineering by working with a language that exposes more of the underlying system while providing modern safety guarantees?**

---

## C++

C++ may be used when useful for:

* performance experiments
* systems programming
* comparing abstraction levels
* understanding memory
* understanding compilation
* comparing C++ with C#, Rust and Python

I should avoid learning C++ purely for syntax.

The focus should be on what C++ enables me to understand.

---

## Performance Engineering

Potential topics:

* CPU usage
* memory usage
* allocations
* I/O
* profiling
* benchmarking
* compiler optimization
* concurrency
* caching
* algorithmic efficiency
* language/runtime differences

A useful question is:

> **Why is this program fast or slow?**

rather than simply:

> **Which language is fastest?**

---

## Compilers and Toolchains

Potential topics:

```text
Source Code
    ↓
Lexer / Parser
    ↓
AST
    ↓
Compiler
    ↓
Intermediate Representation
    ↓
Machine Code
    ↓
Executable
    ↓
CPU
```

Possible experiments:

* inspect generated assembly
* compare compiler optimizations
* create a tiny interpreter
* create a tiny programming language
* explore LLVM
* compile C/C++/Rust
* investigate linking
* investigate binaries

---

## RISC-V

RISC-V is a possible later-stage topic.

The purpose would be to understand:

* instruction sets
* registers
* assembly
* machine code
* CPU execution
* architecture
* software/hardware boundaries

RISC-V is particularly useful because it provides an accessible and open instruction-set architecture for experimentation.

A possible eventual experiment is creating or using a simple RISC-V emulator.

---

## Computer Architecture

Potential topics:

* CPU
* ALU
* registers
* memory
* caches
* instruction execution
* pipelines
* branch prediction
* performance
* memory hierarchy

The emphasis should remain conceptual and practical rather than mathematical.

---

## Hardware / Semiconductors

This is a **later and secondary direction**, not the main focus of the semester.

The long-term curiosity is:

> **How does software eventually become physical computation?**

Potential progression:

```text
Software
 ↓
Instructions
 ↓
CPU Architecture
 ↓
Digital Logic
 ↓
Logic Gates
 ↓
Transistors
 ↓
CMOS
 ↓
Integrated Circuits
 ↓
Modern Semiconductor Manufacturing
```

This area should preferably be explored through software simulations, diagrams, experiments and research rather than requiring physical hardware.

The goal is understanding the software/hardware relationship, not becoming an electronics specialist during this semester.

---

# 13. Example Experiment Structure

Every experiment should ideally have its own directory.

Example:

```text
experiments/
├── 01-linux-workflow/
├── 02-shell-automation/
├── 03-processes/
├── 04-system-calls/
├── 05-rust-vs-csharp/
├── 06-performance/
├── 07-memory/
├── 08-assembly/
├── 09-risc-v/
└── 10-hardware-software-boundary/
```

The exact number and order will change.

Each experiment can contain something like:

```text
01-linux-workflow/
├── README.md
├── src/
├── scripts/
├── results/
└── notes/
```

---

# 14. Experiment Template

Each experiment should answer a real question.

A useful structure:

## Question

What am I trying to find out?

## Context

Why is this relevant to my development or learning?

## Hypothesis

What do I currently expect?

## Experiment

What exactly will I do?

## Measurement

How will I collect evidence?

## Results

What happened?

## Analysis

What does the result mean?

## Conclusion

What did I learn?

## Limitations

What does this experiment NOT prove?

## Engineering Implication

Would this influence a real software engineering decision?

## Learning Outcome

Which school learning outcome(s) did this contribute to?

## Next Step

What should I investigate next?

---

# 15. Examples of Good Questions

The following are examples of the type of question I should prefer.

### Linux

> What practical advantages does Linux provide for software development compared with Windows?

### Terminal

> Which development tasks become more efficient when performed through the terminal instead of a GUI?

### Shell scripting

> When is Bash a good automation tool, and when should I switch to Python or another language?

### Processes

> What actually happens when a program is executed on Linux?

### System calls

> How does a normal application interact with the Linux kernel?

### Programming languages

> What does Rust expose about software development that C# hides?

### Performance

> Which factors actually explain the performance difference between two implementations?

### Compilers

> What happens to a small C++ or Rust program between source code and execution?

### Architecture

> How does a CPU actually execute a program?

### RISC-V

> What can an open instruction-set architecture teach me about the relationship between software and hardware?

### Hardware

> How does a high-level software operation eventually become physical computation?

These questions are intentionally connected.

---

# 16. Avoiding Tutorial Hell

When learning a new technology, I should avoid spending the entire experiment following tutorials.

A good pattern is:

```text
Learn just enough
       ↓
Build something tiny
       ↓
Break it
       ↓
Investigate why
       ↓
Measure / inspect
       ↓
Document
       ↓
Try something harder
```

For example, when learning Linux:

Don't spend two weeks memorizing commands.

Instead:

> "I need to automate this task."

Try to solve it.

Get stuck.

Research the relevant Linux concepts.

Solve it.

Then document what was actually learned.

---

# 17. Relationship to the Main Project

The personal repository should remain aware of the group project, but should not duplicate it.

Useful connections may include:

### Linux

Understanding the environment in which CI/CD tooling and automation execute.

### Shell scripting

Understanding automation beyond GitLab YAML.

### Processes

Understanding how test runners and subprocesses actually execute.

### Performance

Understanding why running fewer tests can have different performance characteristics.

### Filesystems

Understanding coverage files, build artifacts, logs and generated mappings.

### Reproducibility

Understanding why development environments and CI environments can behave differently.

### Networking

Understanding communication between CI components and services.

### Containers

Understanding reproducible execution environments.

### Systems programming

Understanding the lower-level behavior behind software tools.

These connections should be used when genuinely useful, not forced.

---

# 18. Evidence and Documentation

This repository should eventually provide evidence of development rather than merely claiming that I learned something.

Useful evidence can include:

* source code
* terminal output
* benchmarks
* screenshots when useful
* diagrams
* experiment results
* comparisons
* notes
* technical explanations
* Git history
* reflections
* feedback
* links to relevant documentation
* conclusions

The goal is to make it possible for another developer or teacher to understand:

> What did I investigate?

> Why did I investigate it?

> What did I actually do?

> What evidence did I collect?

> What did I learn?

> How did the result influence my next decision?

---

# 19. AI-Assisted Development

AI tools may be used as part of the learning process.

However, AI should not replace understanding.

When using AI:

* ask for explanations
* use it to generate prototypes
* challenge its assumptions
* validate generated code
* compare alternatives
* test claims independently
* document important AI-assisted decisions
* understand code before keeping it

A useful pattern is:

```text
AI suggestion
     ↓
My implementation
     ↓
Test / experiment
     ↓
Evidence
     ↓
Accept / modify / reject
```

AI output should be treated as a hypothesis until validated.

---

# 20. Working With AI in This Repository

When an AI assistant is helping me in this repository, it should use this README as the primary context for understanding the purpose of the work.

The AI should **not automatically suggest a random new project** whenever I ask what to do.

Instead, it should consider:

1. My current semester context.
2. The group project.
3. The school learning outcomes.
4. My current technical skill level.
5. What I have already learned.
6. What gaps remain.
7. The limited time available.
8. Whether the proposed activity meaningfully contributes to software development.
9. Whether the activity can produce useful evidence.
10. Whether it connects naturally to the longer-term technical direction.

The AI should help me choose **small, high-value experiments** rather than large projects.

If I say:

> "I have 3 hours this week. What should I do?"

The answer should ideally be a focused experiment that can realistically be completed within that time.

If I say:

> "I learned X. What should I investigate next?"

The answer should follow the natural technical progression rather than jumping randomly to another technology.

---

# 21. Decision Rules for New Experiments

Before starting a new experiment, consider:

### 1. Is there a real question?

If there is no question, it may just be tutorial work.

### 2. Does it strengthen software development?

This is the primary criterion.

### 3. Does it complement the group project?

Preferably yes, but it should not simply duplicate it.

### 4. Does it strengthen a school learning outcome?

Identify which one(s).

### 5. Can it be completed within the available time?

If not, reduce the scope.

### 6. Can I produce evidence?

Prefer experiments where I can measure, compare, observe or demonstrate something.

### 7. Does it contribute to my longer-term direction?

Linux, systems, performance, architecture, Rust, C++, etc. are particularly relevant.

### 8. Will I actually learn something?

Avoid doing something just because it sounds impressive.

---

# 22. Long-Term Technical Direction

The long-term direction currently looks like:

```text
                 SOFTWARE ENGINEERING
                         │
                         ▼
                       LINUX
                         │
              ┌──────────┴──────────┐
              ▼                     ▼
          TOOLING                 SYSTEMS
              │                     │
          Bash/Python           C++ / Rust
              │                     │
              └──────────┬──────────┘
                         ▼
                    PERFORMANCE
                         │
                         ▼
                  COMPILERS / ABI
                         │
                         ▼
                    ASSEMBLY
                         │
                         ▼
                      RISC-V
                         │
                         ▼
                 CPU ARCHITECTURE
                         │
                         ▼
                   DIGITAL LOGIC
                         │
                         ▼
                     HARDWARE
                         │
                         ▼
                   SEMICONDUCTORS
```

The **software engineering layer remains the center**.

The lower layers are there because I want to understand the technology more deeply and potentially discover a future specialization.

---

# 23. Semester Success Criteria

By the end of the semester, success does **not** mean:

> "I mastered Linux, Rust, RISC-V, compilers and semiconductors."

That would be unrealistic.

A successful semester would mean that I:

* became comfortable using Ubuntu/Linux as a development environment;
* became substantially better at the terminal;
* can automate useful tasks using shell tools;
* understand Linux processes, files, permissions and basic system behavior;
* gained experience outside the C#/.NET ecosystem;
* experimented with lower-level programming;
* improved my understanding of performance and software behavior;
* investigated at least some compiler/assembly concepts;
* explored computer architecture to a meaningful depth;
* started understanding the software/hardware boundary;
* practiced evidence-based technical research;
* produced documented experiments;
* used feedback to change my direction;
* identified technical areas I may want to pursue professionally.

Most importantly:

> **I should finish the semester understanding computers and software development more deeply than when I started, while having concrete evidence of how that understanding developed.**

---

# 24. Current Starting Point

At the beginning of this track:

* My main programming background is C#/.NET.
* I have an interest in Linux but am relatively new to using Ubuntu.
* I want to become comfortable with the terminal.
* I want to understand practical reasons for using Linux in software development.
* I am interested in C++, Rust and lower-level programming.
* I am interested in computer architecture and RISC-V.
* I am curious about how software eventually becomes hardware.
* I have an interest in semiconductor technology.
* I do not want advanced mathematics to be a major requirement.
* I do not want physical hardware to be required.
* My available personal-development time is approximately 0.5 day per week.
* My main semester project already provides substantial experience with CI/CD, automated testing, applied research and software quality.

Therefore, the personal track should remain **software-development-first**, with Linux and systems knowledge forming the foundation and computer architecture/semiconductor technology becoming progressively deeper areas of exploration.

---

# 25. Guiding Principle

When deciding what to do next, ask:

> **"What can I investigate in a small software experiment that teaches me something about how software really works?"**

Then:

```text
Investigate
    ↓
Build
    ↓
Measure
    ↓
Break
    ↓
Understand
    ↓
Document
    ↓
Reflect
    ↓
Choose the next question
```

This repository is not about collecting technologies.

It is about **becoming a stronger software engineer by understanding more of the systems on which software depends.**

## Possible Experiments

Each experiment should be small enough to complete within **1–2 sprints**, with approximately **0.5 day per week** available. The goal is not to fully learn a technology, but to answer a focused technical question through a small implementation and measurable investigation.

This semester, experiments will be exercised against the **placeholder microservices project** (C# services, React frontend). **Current priority: CI/CD in a microservices architecture.** Experiments outside that track are parked until they serve the CI/CD direction.

### Linux & Systems

* **Linux Developer Toolbox**

  * Build a small CLI tool for tasks such as project information, finding errors, cleaning build files, or running benchmarks.
  * Explore Bash, processes, environment variables, filesystems, pipes, exit codes, and permissions.
  * **Question:** What development tasks can Linux's CLI and tooling make easier to automate?

* **What Actually Happens When I Run a Program?**

  * Write a tiny C++ program and investigate what happens from source code to executable to running process.
  * Use tools such as `gcc`/`clang`, `file`, `ldd`, `objdump`, `strace`, `ps`, and `/proc`.
  * **Question:** What happens between writing source code and the program actually executing?

* **Linux Process Monitor**

  * Build a simplified version of `top` or `ps` using `/proc`.
  * Display process IDs, names, CPU usage, memory usage, process lifetime, and permissions.
  * **Question:** How does Linux represent and manage running processes?

* **Linux Debugging Investigation**

  * Start with a deliberately broken C++ or Rust program containing issues such as a segmentation fault, memory leak, infinite loop, or unexpected file access.
  * Investigate it using `gdb`, `strace`, and/or Valgrind.
  * **Question:** How can Linux tooling help diagnose problems that are not obvious from source code?

### Memory, Concurrency & Performance

* **Memory Experiment**

  * Compare stack and heap allocation, pointers/references, object sizes, and allocation behaviour.
  * Optionally compare the same concept in C#, C++, and Rust.
  * **Question:** How do different programming languages expose and manage memory?

* **Multithreading Experiment**

  * Implement a small computational task using 1, 2, 4, and 8 threads.
  * Measure execution time, CPU usage, and scaling.
  * **Question:** When does adding threads improve performance, and when does it add overhead?

* **Tiny Benchmarking Tool**

  * Build a CLI such as `bench ./program`.
  * Run programs repeatedly and report minimum, maximum, average, and variation.
  * Investigate warm-up effects, outliers, compiler optimisation, and debug vs. release builds.
  * **Question:** How can software performance be measured reliably rather than guessed?

* **Rust vs C++ vs C#**

  * Implement the same small problem in all three languages, such as processing a dataset or searching a collection.
  * Compare runtime, memory usage, binary size, development effort, and error handling.
  * **Question:** How do language design choices affect software development and runtime behaviour?

### Software → Operating System

* **Tiny Shell**

  * Build a minimal Bash-like shell supporting commands such as `ls`, `pwd`, and `echo`.
  * Extend it with pipes, redirection, background processes, and signals if time allows.
  * Explore `fork`, `exec`, processes, pipes, file descriptors, and signals.
  * **Question:** How does a command-line shell interact with the operating system?

* **Tiny HTTP Server**

  * Build a minimal HTTP server in C++ or Rust.
  * Handle requests from a browser or `curl`.
  * Explore TCP, sockets, ports, HTTP, processes, and basic concurrency.
  * **Question:** What happens between an HTTP request and the application receiving it?

* **“Works on My Machine” Experiment**

  * Run the same small application in different environments, such as the local Ubuntu environment and a Docker container.
  * Investigate dependencies, environment variables, versions, filesystem differences, and reproducibility.
  * **Question:** Why does software behave differently between environments, and how can this be controlled?

### Programming Languages & Compilers

* **C# → IL → Machine Code**

  * Investigate the path from C# source code to IL, JIT compilation, and machine code.
  * Compare the result with a small C++ or Rust program.
  * Use tools to inspect assemblies and generated machine code.
  * **Question:** How does a high-level language eventually become instructions executed by a CPU?

* **Tiny Interpreter**

  * Create a very small language supporting variables, arithmetic, and `print`.
  * Implement tokenisation, parsing, an AST, and an interpreter.
  * **Question:** How does a programming language turn text into executable behaviour?

* **Tiny Compiler**

  * Extend the interpreter or create a minimal compiler that converts a tiny language into another representation, such as bytecode or simple assembly.
  * Keep the language intentionally small.
  * **Question:** What are the fundamental stages involved in compiling a programming language?

### Computer Architecture & RISC-V

* **RISC-V Emulator**

  * Implement a small software emulator supporting a limited subset of RISC-V instructions such as `ADD`, `SUB`, `LW`, `SW`, `BEQ`, and `JAL`.
  * Model registers, memory, and the program counter.
  * **Question:** How do machine instructions translate into operations performed by a CPU?

* **Tiny CPU Simulator**

  * Design a simple instruction set containing operations such as `LOAD`, `STORE`, `ADD`, `SUB`, `JUMP`, and `HALT`.
  * Simulate registers, memory, an ALU, and instruction execution.
  * **Question:** What are the basic components required for a CPU to execute software?

* **Software → Hardware**

  * Take the CPU simulator one step further and investigate how its components could be represented using digital logic.
  * Experiment with binary addition, registers, ALU operations, and simple HDL simulation.
  * No physical FPGA or hardware is required.
  * **Question:** How can software instructions ultimately be represented as digital hardware?

### Possible Progression

A possible sequence for the semester is:

1. **Linux Developer Toolbox**
2. **What Actually Happens When I Run a Program?**
3. **Linux Process Monitor**
4. **Memory Experiment**
5. **Rust vs C++ vs C#**
6. **Tiny Benchmarking Tool**
7. **Tiny Shell**
8. **C# → Assembly**
9. **RISC-V Emulator**
10. **Tiny CPU Simulator**
11. **Software → Hardware**

The sequence is intentionally progressive:

**Linux → Processes & Memory → Performance → Systems Programming → Compilers → Assembly → RISC-V → CPU Architecture → Hardware**

The experiments do not all need to be completed. The next experiment should be selected based on what is currently missing from the group project, what was learned in the previous experiment, and which technical questions are most interesting to investigate next.
