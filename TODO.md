# In Progress

*(Nothing in progress at the moment.)*

---


# Backlog

## Phase 2: Build Evaluator Orchestration Tool

### Next Priority: Server Process Management (`feat/03_server_management`)
Implement actual llama.cpp process control logic.
- [ ] Research llama-server CLI flags and API endpoints
- [ ] Implement start/stop `llama-server` programmatically via `ProcessStartInfo`
- [ ] Health check endpoint polling (wait until server responds on port X)
- [ ] Port conflict detection + auto-selection fallback mechanism
- [ ] Graceful shutdown handling (SIGINT/SIGTERM cleanup)
- [ ] Log output redirection (capture stderr/stdout from server process)

### Results Logging (`feat/05_results_logging`)
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

## refactor/07_settings_manager — Centralized Configuration Management ✅ MERGED
Created SettingsManager singleton; eliminated config duplication across classes.

## feat/10_interactive_setup — Interactive Settings Editor ✅ MERGED
Built full TUI configuration wizard using Spectre.Console. Users can add/edit/remove models without touching JSON files directly.

~~**Step C: Configuration Schema Design**~~ ~~(Obsolete)~~
Skipped — went straight to implementation with interactive UI instead of upfront schema design.

~~**Step E: Static Settings Manager**~~ ~~(Completed differently than planned)~~
Implemented as SettingsManager singleton with interactive TUI wizard. No separate models.json created; everything lives in single Settings.json file managed through the app's menu system.
