# In Progress

# Completed

## feat/12_Gemma4_b_settings_expansion — Expand settings schema (fresh implementation)
**Branch:** `feat/12_Gemma4_b_settings_expansion`
**Goal:** Implement the feat/12 settings expansion (all llama-server CLI flags in settings, editable vs readonly) as a clean standalone branch, ignoring the experimental/duplicate 12x branches.
**Implemented by model:** Gemma-4-26B-A4B-it-MXFP4_MOE_noctrex.gguf

**Steps:**
- [x] Step 1: Expand `Entities.cs` (Host, CacheTypeK/V, SamplingDefaults, ServerDefaults, Alias)
- [x] Step 2: Update `SettingsManager.Load()` backward-compat null-coalescing
- [x] Step 3: `EditGeneralSettings()` — inputs for Host, cache types, sampling defaults
- [x] Step 4: `AddModel()`/`EditModel()` — alias input + auto-gen from GGUF filename
- [x] Step 5: `ShowCurrentSettings()` — display all new fields incl. readonly ServerDefaults section
- [x] Step 6: Build (`dotnet build -o agent_build`) and verify; move this block to Completed

**Notes:**
- Command syntax, API references, links, anything an agent needs to resume work.
