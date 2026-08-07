# In Progress

<!-- Add tasks here when starting work on them -->

---

# Backlog

## Phase 2: Build Evaluator Orchestration Tool

### Step B: Server Process Management (`feat/03_server_management`)
Build llama.cpp process control logic before defining config schema.
- [ ] Implement start/stop `llama-server` programmatically via `ProcessStartInfo`
- [ ] Health check endpoint polling (wait until server responds on port X)
- [ ] Port conflict detection + auto-selection fallback mechanism
- [ ] Graceful shutdown handling (SIGINT/SIGTERM cleanup)
- [ ] Log output redirection (capture stderr/stdout from server process)

### Step C: Configuration System Design (`feat/04_config_schema`)
Define final schema based on discovered needs from Steps A+B.
- [ ] Create `config/models.json` with real parameter names from server management code
  - [ ] Server executable path + common params
  - [ ] Per-model specs: id, gguf file, context size, speculation settings, batch sizes, gpu_layers, cpu_moe, jinja flag
- [ ] Document usage pattern in README (how to add new model entries)
- [ ] Consider adding JSON schema validation file if needed

### Step D: Results Logging (`feat/05_results_logging`)
Design outcome recording format once evaluator has execution data.
- [ ] Define `results/evaluation_<timestamp>.json` structure
  - [ ] Track: model_id, test_case_name, pass/fail, duration_ms, timestamp, git_commit_hash
  - [ ] Include runtime metadata: evaluator_version, llama.cpp_tag, parameters_used
- [ ] Implement file writer utility class
- [ ] Ensure `.gitignore` excludes result files properly

### Future Enhancements (Post-Core)
- [ ] OpenAI-compatible HTTP client implementation
- [ ] Git automation for creating evaluation branches
- [ ] Bug-fix application pipeline (send buggy code → receive fix → apply patch)
- [ ] Advanced test runner integration beyond simple dotnet test invocation

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

## feat/02_evaluator_scaffolding — Evaluator Console App Foundation

**Goal:** Scaffold the Evaluator C# console app with domain model, server manager stub, configuration loading, and menu-driven UI.

**Steps Completed:**
- [x] Create src/Evaluator/Evaluator.csproj (.NET 10 console app)
- [x] Implement ModelEvaluation domain record
- [x] Implement LlamaServerManager stub with 4 methods (StartServer, StopServer, ServerInfo, CallApi)
- [x] Implement Evaluator skeleton with TODO markers
- [x] Add configuration loading: read ~/LlmEvaluator/Configuration.json, create if missing
- [x] Define Evaluation Process in README
- [x] Verify build succeeds
- [x] Commit changes and open PR
- [x] Address review feedback (PR #8)
  - [x] Menu-driven UI (See Results / Run Evaluation / Change Settings)
  - [x] Track total execution time and total tokens used in ModelEvaluation
  - [x] Implement evaluation flow with TODO markers for each step
  - [x] Add ServerUrl to LlamaServerManager
  - [x] Clean up comments, keep only TODO markers
  - [x] Config-driven: model lookup from configuration
  - [x] Build verified, pushed to branch
  - [x] Replied to all 27 review comments
  - [x] Requested re-review from alex-piccione
  - [x] Remove TestCaseVersion from ModelEvaluation
  - [x] Change duration from ms to seconds
  - [x] Add Spectre.Console for menu UI
  - [x] Implement API call flow with buggy code retrieval
  - [x] Reply to remaining 10 unresolved threads
  - [x] Remove CallLlamaApiAsync, GetBuggyCode, HttpClient, LlamaApiResponse, UsageInfo
  - [x] Remove all non-TODO comments
  - [x] Make Configuration and ModelSettings record types
  - [x] Remove TODO comment for step 3 (OpenAI endpoint)
  - [x] Build verified, pushed to branch

**PR:** #8 open (awaiting re-review from alex-piccione)
