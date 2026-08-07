# In Progress

<!-- Add tasks here when starting work on them -->

---

# Backlog

## Core Infrastructure (Evaluator Tool)

### 2. Configuration & Metadata
- [ ] Create `config/models.json` schema:
  - [ ] Path to `llama-server` executable
  - [ ] Common parameters (port, context window, GPU layers)
  - [ ] Model-specific parameters (model path, prompt template, temperature)
  - [ ] Model **Short-Name** (e.g., `Qwen3_5-27B_UD_Q4-K-M_Unsloth`) - used for branch naming and logs
  - [ ] Evaluator Test Version (e.g., `v1`, `v2`)
  - [ ] `llama.cpp` release tag (e.g., `b1234`)
- [ ] Define branch naming convention: `eval/<short-name>-test<test_version>-<YYYYMMDD-hhmm>`
  - Example: `eval/Qwen3_5-27B_UD_Q4-K-M_Unsloth-testv1-20240514-1530`
- [ ] Define results log schema (`results/evaluation_log.json`):
  - [ ] Branch name (following convention)
  - [ ] Model Short-Name
  - [ ] Evaluator Test Version
  - [ ] Evaluator Git Commit Hash
  - [ ] `llama.cpp` release tag
  - [ ] Parameters used
  - [ ] Pass/Fail status, duration, timestamp

### 3. Evaluator Core Implementation
- [ ] CLI argument parsing (e.g., `--model`, `--bug-id`)
- [ ] Process management: Start/Stop `llama.server` using configured executable path
- [ ] OpenAI-compatible API client (supports tool calling with GGUF models)
- [ ] Git automation: Create branch using naming convention `eval/<short-name>-test<test_version>-<YYYYMMDD-hhmm>`
- [ ] API interaction loop: Send code -> Receive fix -> Apply to file
- [ ] Test runner: Execute `dotnet test` and capture exit code/output
- [ ] Logging: Write structured evaluation results to JSON

## Target Content
- [ ] Add more complex bugs (concurrency issues, memory leaks, algorithmic errors)
- [ ] Expand test coverage with edge case scenarios
- [ ] Consider adding security vulnerability examples
- [ ] Scale difficulty levels (add Expert tier)

---

# Completed

## feat/01_project_reorganization — Repository Restructuring

**Goal:** Move existing code to proper locations and set up infrastructure folders.

**Steps Completed:**
- [x] Created feature branch `feat/01_project_reorganization`
- [x] Renamed `src/AI.Evaluator.Console` → `src/TargetCode`
- [x] Renamed `tests/AI.Evaluator.Tests` → `tests/TargetCodeTests`
- [x] Updated namespaces throughout (from `AI.Evaluator.Console` to `TargetCode`)
- [x] Created placeholder directories: `config/`, `results/`
- [x] Created empty `src/Evaluator/` folder (actual C# Console App scaffolding deferred to next phase)
- [x] Updated solution file paths (`AI.Evaluator.slnx`)
- [x] Updated `.gitignore` to exclude `results/*.json`
- [x] Verified build succeeds; test suite runs correctly (9 failing = intentional bugs in target code)

**PR:** #7 merged into main
