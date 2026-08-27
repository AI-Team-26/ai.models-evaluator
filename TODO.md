# In Progress

---

## feat/13_avalonia_ui_scaffolding — Avalonia UI Scaffolding
Create a basic Avalonia UI project scaffolding with a simple home page.

**Branch:** `feat/13_avalonia_ui_scaffolding`
**Goal:** Scaffold a new Avalonia UI project that displays a simple "Hello World" home page.

**Context / Mental Picture:**
- New C# Avalonia application project under `src/AvaloniaUI/`
- No MVVM framework yet — plain XAML with code-behind for simplicity
- Minimal setup: just verify Avalonia runs and shows a window
- Will reference the existing `Evaluator` project later once basics work
- Central package management via `Directory.Packages.props`

**Steps:**
- [x] Create new Avalonia project under `src/AvaloniaUI/`
- [x] Add Avalonia NuGet packages to `Directory.Packages.props`
- [x] Update `AI.Evaluator.slnx` to include the new project
- [x] Verify build: `dotnet build` ✅ (0 warnings, 0 errors)
- [x] Replace default content with a simple "AI Models Evaluator" label/home page
- [x] Fix: `OutputType` changed from `WinExe` to `Exe` for cross-platform CI compatibility
- [x] Commit and test again

**PR:** #19 — [feat/13: Scaffold Avalonia UI project](https://github.com/AI-Team-26/ai.models-evaluator/pull/19)

**Notes:**
- Start with Windows-only target for now (no need for multiplatform config yet)
- Keep styling minimal — system defaults are fine

---

## docs/12_experimental_prs — Document feat/12 experimental PR history
Document the parallel experimental PRs and point reviewers to the comparative evaluation.

**Branch:** `docs/12_experimental_prs`
**Goal:** Keep the feat/12 experiment history concise and current.

---

# Backlog

---

# Completed

## feat/12_KAT_9_settings_expansion — Expand llama-server settings schema ✅ MERGED (PR #71)
Expand `ApplicationSettings` and `ModelSettings` to cover all llama-server CLI flags from the reference command. Split into editable (UI) and readonly (shown in Settings view, not editable via UI). This is a prerequisite for `feat/03_server_management`.

**Branch:** `feat/12_KAT_9_settings_expansion`
**Implemented by:** KAT-Coder-V2.5-Dev-APEX-dynamic-v2_(myric)_192k

**Changes:**
- Added `Host`, `CacheTypeK`, `CacheTypeV` to `ApplicationSettings`
- Added `SamplingDefaults` record (Temperature, TopK, TopP, MinP, RepeatPenalty, RepeatLastN)
- Added `ServerDefaults` record with all readonly llama-server fields
- Added `Alias` to `ModelSettings` with auto-generation from GGUF filename
- Updated `SettingsManager.Load()` with `Normalize()` for backward compatibility
- Expanded `SettingsView` with full editing and display of all new fields

**Build & Test:** 0 warnings, 0 errors; 4 passed, 9 baseline failures unchanged.
