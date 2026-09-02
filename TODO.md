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

### Expand Settings for llama-server flags (`feat/12_settings_expansion`) 

> **Note:** This task was used as an experimental testbed for testing multiple LLM models.
> See [docs/experimental_prs.md](./docs/experimental_prs.md) for the full history of experimental PRs.
Expand `ApplicationSettings` and `ModelSettings` to cover all llama-server CLI flags from the reference command. Split into editable (UI) and readonly (shown in Settings view, not editable via UI). This is a prerequisite for `feat/03_server_management`.

**Branch:** `feat/12_settings_expansion`
**Goal:** Expand the settings schema to cover all llama-server CLI flags, with editable/readonly distinction, and update the SettingsView UI accordingly.

**Context / Mental Picture:**
- Reference llama-server command uses ~30 CLI flags. Currently only 6 are in settings (port, model, ctx-size, n-gpu-layers, n-cpu-moe, jinja).
- Flags are categorized into three groups:
  1. **App-level editable** — shown and editable in the Settings UI (general settings editor).
  2. **App-level readonly** — shown in the Settings view (read-only display), stored in JSON with hardcoded defaults. Not editable via UI.
  3. **Per-model editable** — shown and editable in the add/edit model flows.
- Readonly fields are stored in a `ServerDefaults` record nested in `ApplicationSettings`. They have default values so old settings files without them still work (null-coalesce on load).
- Sampling params (`--temperature`, `--top-k`, `--top-p`, `--min-p`, `--repeat-penalty`, `--repeat-last-n`) are app-level editable defaults, shared across all models.
- `--cache-type-k` and `--cache-type-v` are app-level editable, default `q8_0`.
- `--alias` is per-model, editable. If left empty, auto-generate from GGUF filename (strip `.gguf`).
- `--jinja` stays per-model, editable. When `false`, the flag is omitted from the CLI.
- Reasoning flags are app-level readonly for now (all models are reasoning models).
- Skip: `--threads`, `--mlock`, `--no-mmap` (obsolete or use llama-server defaults).

**Flag categorization:**

| Flag | Group | Editable | Default |
|---|---|---|---|
| `--host` | App | ✅ | `127.0.0.1` |
| `--port` | App (existing) | ✅ | `0` |
| `--temperature` | App → `SamplingDefaults` | ✅ | `0.1` |
| `--top-k` | App → `SamplingDefaults` | ✅ | `20` |
| `--top-p` | App → `SamplingDefaults` | ✅ | `0.80` |
| `--min-p` | App → `SamplingDefaults` | ✅ | `0.05` |
| `--repeat-penalty` | App → `SamplingDefaults` | ✅ | `1.15` |
| `--repeat-last-n` | App → `SamplingDefaults` | ✅ | `1024` |
| `--cache-type-k` | App | ✅ | `q8_0` |
| `--cache-type-v` | App | ✅ | `q8_0` |
| `--parallel` | App → `ServerDefaults` | ❌ | `1` |
| `--prio` | App → `ServerDefaults` | ❌ | `3` |
| `--flash-attn` | App → `ServerDefaults` | ❌ | `on` |
| `--kv-unified` | App → `ServerDefaults` | ❌ | `true` |
| `--load-mode` | App → `ServerDefaults` | ❌ | `mmap` |
| `--fit` | App → `ServerDefaults` | ❌ | `off` |
| `--cache-reuse` | App → `ServerDefaults` | ❌ | `256` |
| `--draft-p-min` | App → `ServerDefaults` | ❌ | `0.7` |
| `--log-verbosity` | App → `ServerDefaults` | ❌ | `3` |
| `--samplers` | App → `ServerDefaults` | ❌ | `penalties;dry;top_k;top_p;min_p;temperature` |
| `--context-shift` | App → `ServerDefaults` | ❌ | `true` |
| `--reasoning-preserve` | App → `ServerDefaults` | ❌ | `true` |
| `--reasoning` | App → `ServerDefaults` | ❌ | `on` |
| `--reasoning-budget` | App → `ServerDefaults` | ❌ | `4096` |
| `--reasoning-budget-message` | App → `ServerDefaults` | ❌ | `"... Considering the limited time by the user, I have to give the solution based on the thinking directly now."` |
| `--batch-size` | App → `ServerDefaults` | ❌ | `1024` |
| `--ubatch-size` | App → `ServerDefaults` | ❌ | `512` |
| `--spec-type` | App → `ServerDefaults` | ❌ | `none` |
| `--alias` | Model | ✅ | (auto-gen from GGUF filename if empty) |
| `--jinja` | Model (existing) | ✅ | `false` |
| `--model` | Model (existing) | ✅ | — |
| `--ctx-size` | Model (existing) | ✅ | — |
| `--n-gpu-layers` | Model (existing) | ✅ | — |
| `--n-cpu-moe` | Model (existing) | ✅ | — |

**Steps:**
- [ ] **Step 1: Expand `Entities.cs`**
  - [ ] Add `Host` property to `ApplicationSettings` (default `127.0.0.1`)
  - [ ] Add `CacheTypeK` and `CacheTypeV` to `ApplicationSettings` (default `q8_0`)
  - [ ] Create `SamplingDefaults` record with: `Temperature` (double, `0.1`), `TopK` (int, `20`), `TopP` (double, `0.80`), `MinP` (double, `0.05`), `RepeatPenalty` (double, `1.15`), `RepeatLastN` (int, `1024`)
  - [ ] Add `SamplingDefaults` property to `ApplicationSettings`
  - [ ] Create `ServerDefaults` record with all readonly fields (see table above)
  - [ ] Add `ServerDefaults` property to `ApplicationSettings`
  - [ ] Add `Alias` property to `ModelSettings` (default `""`)
  - [ ] Ensure backward compatibility: if `ServerDefaults` or `SamplingDefaults` are null after deserialization (old settings files), initialize with defaults in `SettingsManager.Load()`
- [ ] **Step 2: Update `SettingsManager.Load()`**
  - [ ] After deserialization, null-coalesce `ServerDefaults` and `SamplingDefaults` with `new ServerDefaults()` / `new SamplingDefaults()`
  - [ ] Null-coalesce `Host` with `"127.0.0.1"` if empty
  - [ ] Null-coalesce `CacheTypeK`/`CacheTypeV` with `"q8_0"` if empty
- [ ] **Step 3: Update `SettingsView` — general settings editor**
  - [ ] Add `Host` input to `EditGeneralSettings()`
  - [ ] Add `CacheTypeK` and `CacheTypeV` inputs to `EditGeneralSettings()`
  - [ ] Add sampling defaults editing (Temperature, TopK, TopP, MinP, RepeatPenalty, RepeatLastN) to `EditGeneralSettings()` or a new `EditSamplingDefaults()` method
- [ ] **Step 4: Update `SettingsView` — model add/edit flows**
  - [ ] Add `Alias` input to `AddModel()` (leave empty = auto-gen from GGUF filename)
  - [ ] Add `Alias` input to `EditModel()`
  - [ ] Auto-generate alias from GGUF filename (strip `.gguf`) when alias is empty
- [ ] **Step 5: Update `ShowCurrentSettings()`**
  - [ ] Display `Host` alongside `ServerPort`
  - [ ] Display `CacheTypeK` / `CacheTypeV`
  - [ ] Display sampling defaults section
  - [ ] Display `ServerDefaults` (readonly) section — all readonly fields shown but marked as read-only
  - [ ] Display `Alias` for each model
- [ ] **Step 6: Build and verify**
  - [ ] `dotnet build` passes
  - [ ] `dotnet run` — verify settings load/save works with new fields
  - [ ] Verify old settings file (without new fields) still loads (backward compat)

**Notes:**
- Do NOT implement `LlamaServerManager` changes in this branch — that's `feat/03_server_management`.
- The `ServerDefaults` record is a nested object in JSON. Example structure:
  ```json
  {
    "host": "127.0.0.1",
    "serverPort": 8001,
    "cacheTypeK": "q8_0",
    "cacheTypeV": "q8_0",
    "samplingDefaults": { "temperature": 0.1, "topK": 20, ... },
    "serverDefaults": { "parallel": 1, "prio": 3, ... },
    "models": [ { "id": "...", "alias": "...", ... } ]
  }
  ```
- `--reasoning-budget-message` is a long string with quotes — ensure proper JSON escaping.

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

## doc/12_evaluate_prs_103_104_105 — Evaluate feat/12 PRs #103, #104, #105
Evaluated PRs #103, #104, and #105 against the same 6-criterion rubric used for PRs #96-#100. Branch-based evaluation, build/test verification, full source inspection, and Evaluation.md update. PR #103 and #104 are byte-identical (two runs of the same model) and both score 83/100 ★★★. PR #105 is distinct and scores 84/100 ★★★★ — the strongest of the three, with the cleanest boolean typing in the recent batch.

## refactor/07_settings_manager — Centralized Configuration Management ✅ MERGED
Created SettingsManager singleton; eliminated config duplication across classes.

## feat/10_interactive_setup — Interactive Settings Editor ✅ MERGED
Built full TUI configuration wizard using Spectre.Console. Users can add/edit/remove models without touching JSON files directly.

~~**Step C: Configuration Schema Design**~~ ~~(Obsolete)~~
Skipped — went straight to implementation with interactive UI instead of upfront schema design.

~~**Step E: Static Settings Manager**~~ ~~(Completed differently than planned)~~
Implemented as SettingsManager singleton with interactive TUI wizard. No separate models.json created; everything lives in single Settings.json file managed through the app's menu system.
