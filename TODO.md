# In Progress

---

# Backlog

## feat/13_avalonia_ui_scaffolding
Create a basic Avalonia UI project scaffolding with a simple home page.

**Branch:** `feat/13_avalonia_ui_scaffolding`
**Goal:** Scaffold a new Avalonia UI project that displays a simple "Hello World" home page.

**Context / Mental Picture:**
- New C# Avalonia application project under `src/AvaloniaUI/`
- No MVVM framework yet — plain XAML with code-behind for simplicity
- Minimal setup: just verify Avalonia runs and shows a window
- Will reference the existing `Evaluator` project later once basics work

**Steps:**
- [ ] Create new Avalonia project: `dotnet new avalonia-mvvm -o src/AvaloniaUI` (or console template if simpler)
- [ ] Add required NuGet packages via `Directory.Packages.props` (Avalonia, Avalonia.Themes.Fluent)
- [ ] Update `.slnx` to include the new project
- [ ] Verify build: `dotnet build`
- [ ] Verify run: `dotnet run --project src/AvaloniaUI/AvaloniaUI.csproj` shows a window
- [ ] Replace default content with a simple "AI Models Evaluator" label/home page
- [ ] Commit and test again

**Notes:**
- Template command may vary; check latest Avalonia docs if needed
- Start with Windows-only target for now (no need for multiplatform config yet)
- Keep styling minimal — system defaults are fine

---

## Phase 2: Build Evaluator Orchestration Tool

### Server Process Management (`feat/03_server_management`)
Implement actual llama.cpp process control logic. Depends on `feat/12_settings_expansion`.
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

## feat/12_QWEN38_UD-Q3_K_XL_3_settings_expansion — Expand llama-server settings schema ✅
Expanded `ApplicationSettings`/`ModelSettings` to cover all llama-server CLI flags with editable/readonly distinction.
- [x] Add `Host`, `CacheTypeK`, `CacheTypeV` to `ApplicationSettings`
- [x] Create `SamplingDefaults` record (Temperature, TopK, TopP, MinP, RepeatPenalty, RepeatLastN)
- [x] Create readonly `ServerDefaults` record (Parallel, Prio, FlashAttn, KVUnified, LoadMode, Fit, CacheReuse, DraftPMin, LogVerbosity, Samplers, ContextShift, ReasoningPreserve, Reasoning, ReasoningBudget, ReasoningBudgetMessage, BatchSize, UbatchSize, SpecType)
- [x] Add per-model `Alias` (auto-gen from GGUF filename when empty) in Add/Edit flows
- [x] Update `SettingsManager.Load()` for backward compatibility (null-coalesce old settings files)
- [x] Update `EditGeneralSettings()`: Host, cache types, sampling defaults inputs
- [x] Update `ShowCurrentSettings()`: new fields + read-only ServerDefaults section + per-model alias
- [x] Build passes (`dotnet build`) and backward-compat smoke test OK
**Done by the model Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k], mtp:3/5** | **Agent Work Time:** 00:07

## refactor/07_settings_manager — Centralized Configuration Management ✅ MERGED
Created SettingsManager singleton; eliminated config duplication across classes.

## feat/10_interactive_setup — Interactive Settings Editor ✅ MERGED
Built full TUI configuration wizard using Spectre.Console. Users can add/edit/remove models without touching JSON files directly.

~~**Step C: Configuration Schema Design**~~ ~~(Obsolete)~~
Skipped — went straight to implementation with interactive UI instead of upfront schema design.

~~**Step E: Static Settings Manager**~~ ~~(Completed differently than planned)~~
Implemented as SettingsManager singleton with interactive TUI wizard. No separate models.json created; everything lives in single Settings.json file managed through the app's menu system.
