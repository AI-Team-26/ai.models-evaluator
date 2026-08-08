# In Progress

---

## refactor/07_settings_manager — Centralized Configuration Management ✅ MERGED
Refactored configuration handling to eliminate code duplication and establish single source of truth.
- Created SettingsManager singleton with automatic load/save logic
- Enforced strict validation via GetSettings() method (throws if invalid)
- Changed defaults to empty strings instead fake values
- Updated Program.cs flow to show helpful guidance when config incomplete

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

### Step E: Static Settings Manager (`feat/06_settings_manager`)
Singleton manager for reading/writing `Settings.json`. Every other class accesses settings through it.
- [ ] Create static `SettingsManager` class
- [ ] Load `Settings.json` on startup / access
- [ ] Save method writes back to disk
- [ ] Always keeps latest settings in memory
- [ ] Replace all direct file reads across project
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
Moved source/tests to `TargetCode/TargetCodeTests`; added `config/`, `results/`, updated sln/gitignore.

**Next:** Phase 2 — start building the Evaluator tool itself.

## feat/02_evaluator_scaffolding — Evaluator Console App Foundation
Scaffolded Evaluator project: `ModelEvaluation`, `LlamaServerManager` stubs, `Configuration` + `ModelSettings` records, config loading from JSON. Still awaiting alex-piccione re-review on PR #8.

**Remaining:** implement real `StartServer()`/`StopServer()` process control; finalize config schema; add results logging.
