# In Progress

## feat/12_Gemma4a_settings_expansion — Expand settings schema (all llama-server flags)
**Branch:** `feat/12_Gemma4a_settings_expansion`
**Implemented by model:** Gemma-4-26B-Q4_0_(Google)_128k

**Goal:** Implement the feat/12 settings expansion (all llama-server CLI flags in settings, editable vs readonly) as a clean standalone branch.

**Context / Mental Picture:**
- Follows the flag categorization table in the original feat/12 spec below (App editable / App readonly `ServerDefaults` / Model editable).
- New records: `SamplingDefaults`, `ServerDefaults`; new fields `Host`, `CacheTypeK/V`, `ModelSettings.Alias`.
- Backward compat: null-coalesce in `SettingsManager.Load()` so old Settings.json files still load.
- No LlamaServerManager changes (that's feat/03_server_management).

**Steps:**
- [ ] Step 1: Expand `Entities.cs` (Host, CacheTypeK/V, SamplingDefaults, ServerDefaults, Alias)
- [ ] Step 2: Update `SettingsManager.Load()` backward-compat null-coalescing
- [ ] Step 3: `EditGeneralSettings()` — inputs for Host, cache types, sampling defaults
- [ ] Step 4: `AddModel()`/`EditModel()` — alias input + auto-gen from GGUF filename
- [ ] Step 5: `ShowCurrentSettings()` — display all new fields incl. readonly ServerDefaults section
- [ ] Step 6: Build (`dotnet build -o agent_build`) and verify; move this block to Completed

---

## docs/12_experimental_prs — Document feat/12 experimental PR history
Document that feat/12 settings expansion was used to test multiple LLM models,
producing several parallel PRs that should be ignored. Definitive implementation is PR #22.

... (rest of TODO.md remains same)
