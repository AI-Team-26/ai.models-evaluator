# In Progress

<!-- Add tasks here when starting work on them -->

---

# Completed

## feat/01_project_reorganization — Repository Restructuring

**Goal:** Move existing code to proper locations and set up infrastructure folders.

**Completed Steps:**
- [x] Created feature branch `feat/01_project_reorganization`
- [x] Renamed `src/AI.Evaluator.Console` → `src/TargetCode`
- [x] Renamed `tests/AI.Evaluator.Tests` → `tests/TargetCodeTests`
- [x] Updated namespaces throughout (from `AI.Evaluator.Console` to `TargetCode`)
- [x] Created placeholder directories: `src/Evaluator/`, `config/`, `results/`
- [x] Updated solution file paths (`AI.Evaluator.slnx`)
- [x] Updated `.gitignore` to exclude `results/*.json`
- [x] Verified build succeeds; test suite runs correctly (9 failing = intentional bugs)

**PR:** #7 merged into main

---

# Backlog

## Phase 2: Configuration System

- [ ] **[feat/02_configuration_schema]** Design models.json configuration format
  - [ ] Define common parameters section (port, context window, GPU layers)
  - [ ] Define model-specific parameters (path, prompt template, temperature)
  - [ ] Include metadata fields (Model Short-Name, Test Version, llama.cpp tag)
  
- [ ] **[feat/03_branch_naming_convention]** Implement evaluation branch creation logic
  - [ ] Format: `eval/<short-name>-test<version>-<YYYYMMDD-hhmm>`
  - [ ] Auto-generate branches per evaluation run
  
- [ ] **[feat/04_results_logging]** Design evaluation result logging schema
  - [ ] Define JSON structure for `results/evaluation_log.json`
  - [ ] Track: branch, commit hash, parameters, pass/fail, duration, timestamp

## Phase 3: Evaluator Core Implementation

- [ ] CLI argument parsing (`--model`, `--bug-id`)
- [ ] Process management: Start/Stop `llama.server` programmatically
- [ ] OpenAI-compatible API client supporting tool calling
- [ ] Git automation: Create evaluation branches automatically
- [ ] Fix application loop: Send buggy code → receive fix → apply to files
- [ ] Test runner integration: Execute `dotnet test` and capture results
- [ ] Structured logging: Write evaluation outcomes to JSON

## Target Content Enhancement

- [ ] Add more complex bug scenarios (concurrency issues, memory leaks, algorithmic errors)
- [ ] Expand test coverage with edge case scenarios
- [ ] Consider adding security vulnerability examples
- [ ] Scale difficulty levels (add Expert tier beyond current Beginner/Intermediate)
