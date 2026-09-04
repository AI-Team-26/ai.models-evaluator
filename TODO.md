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

# Completed

- [x] **[feat/12_Gemma-4-26B-UD-IQ4_NL_(Unsloth)_1_settings_expansion]** Expand settings schema for llama-server flags
    - **Goal:** Expand the settings schema to cover all llama-server CLI flags, with editable/readonly distinction, and update the SettingsView UI accordingly.
    - **Context / Mental Picture:**
        - Done by the model "Gemma-4-26B-UD-IQ4_NL_(Unsloth)_[104k]"
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
    - **Steps:**
        - [x] **Step 1: Expand `Entities.cs`**
            - [x] Add `Host` property to `ApplicationSettings` (default `127.0.0.1`)
            - [x] Add `CacheTypeK` and `CacheTypeV` to `ApplicationSettings` (default `q8_0`)
            - [x] Create `SamplingDefaults` record with: `Temperature` (double, `0.1`), `TopK` (int, `20`), `TopP` (double, `0.80`), `MinP` (double, `0.05`), `RepeatPenalty` (double, `1.15`), `RepeatLastN` (int, `1024`)
            - [x] Add `SamplingDefaults` property to `ApplicationSettings`
            - [x] Create `ServerDefaults` record with all readonly fields (see table above)
            - [x] Add `ServerDefaults` property to `ApplicationSettings`
            - [x] Add `Alias` property to `ModelSettings` (default `""`)
            - [x] Ensure backward compatibility: if `ServerDefaults` or `SamplingDefaults` are null after deserialization (old settings files), initialize with defaults in `SettingsManager.Load()`
        - [x] **Step 2: Update `SettingsManager.Load()`**
            - [x] After deserialization, null-coalesce `ServerDefaults` and `SamplingDefaults` with `new ServerDefaults()` / `new SamplingDefaults()`
            - [x] Null-coalesce `Host` with `"127.0.0.1"` if empty
            - [x] Null-coalesce `CacheTypeK`/`CacheTypeV` with `"q8_0"` if empty
        - [x] **Step 3: Update `SettingsView` — general settings editor**
            - [x] Add `Host` input to `EditGeneralSettings()`
            - [x] Add `CacheTypeK` and `CacheTypeV` inputs to `EditGeneralSettings()`
            - [x] Add sampling defaults editing (Temperature, TopK, TopP, MinP, RepeatPenalty, RepeatLastN) to `EditGeneralSettings()` or a new `EditSamplingDefaults()` method
        - [x] **Step 4: Update `SettingsView` — model add/edit flows**
            - [x] Add `Alias` input to `AddModel()` (leave empty = auto-gen from GGUF filename)
            - [x] Add `Alias` input to `EditModel()`
            - [x] Auto-generate alias from GGUF filename (strip `.gguf`) when alias is empty
        - [x] **Step 5: Update `ShowCurrentSettings()`**
            - [x] Display `Host` alongside `ServerPort`
            - [x] Display `CacheTypeK` / `CacheTypeV`
            - [x] Display sampling defaults section
            - [x] Display `ServerDefaults` (readonly) section — all readonly fields shown but marked as read-only
            - [x] Display `Alias` for each model
        - [x] **Step 6: Build and verify**
            - [x] `dotnet build` passes
            - [x] `dotnet run` — verify settings load/save works with new fields
            - [x] Verify old settings file (without new fields) still loads (backward compat)
    - **Notes:**
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
