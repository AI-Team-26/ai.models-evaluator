# In Progress

## feat/01_project_reorganization — Reorganize repository structure and setup folder hierarchy

**Goal:** Move existing code to proper locations (`src/TargetCode`, `tests/TargetCodeTests`) and create infrastructure folders.

**Context / Mental Picture:**
- Current `AI.Evaluator.Console` contains intentional bug examples → becomes `TargetCode`
- Current tests validate those bugs → become `TargetCodeTests`
- New `Evaluator` console app will be created separately later
- Need `config/` for model configs, `results/` for evaluation logs

**Steps:**
- [x] Create feature branch `feat/01_project_reorganization`
- [x] Rename `src/AI.Evaluator.Console` → `src/TargetCode`
- [x] Rename `tests/AI.Evaluator.Tests` → `tests/TargetCodeTests`
- [x] Update namespace references in moved C# files
- [x] Create empty `src/Evaluator/` directory structure (placeholder for future implementation)
- [x] Create `config/` and `results/` directories
- [x] Update solution file paths (`AI.Evaluator.slnx`)
- [x] Update `.gitignore` to exclude `results/*.json`, keep existing exclusions
- [x] Verify build works after reorganization ✓ (build succeeds, tests run with expected failures due to intentional bugs)

---

## Completed

---

# Backlog

## Core Infrastructure (Evaluator Tool)

### 1. Project Structure & Setup
- [ ] Reorganize repository structure:
  - [ ] Move existing code to `src/TargetCode/`
  - [ ] Move existing tests to `tests/TargetCodeTests/`
  - [ ] Create `src/Evaluator/` (C# Console App)
  - [ ] Create `config/` and `results/` folders
- [ ] Update `.gitignore` (exclude `results/*.json`, `bin/`, `obj/`)
- [ ] Update solution file (`AI.Evaluator.slnx`) to include new paths

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
