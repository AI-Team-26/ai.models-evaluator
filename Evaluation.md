# Evaluation of Experimental Settings Models

This document evaluates the models used to implement the `feat/12_settings_expansion` task. PR links identify the corresponding implementation, but the evaluation and scores apply to the models, not to the PRs.

## 1. All `feat/12` implementations and executor models

The following list contains all known `feat/12_settings_expansion` implementations and the models used to produce them. Documentation, planning, Avalonia, and unrelated bug-fix PRs are excluded.

| PR | Executor model |
|---:|---|
| [#16](https://github.com/AI-Team-26/ai.models-evaluator/pull/16) | `Qwen3-Coder-Next-REAP-40B-A3B.i1-IQ3_M_mradermacher.gguf` |
| [#17](https://github.com/AI-Team-26/ai.models-evaluator/pull/17) | `Qwen3.5-27B-IQ4_XS_unsloth.gguf` |
| [#18](https://github.com/AI-Team-26/ai.models-evaluator/pull/18) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket.gguf` |
| [#20](https://github.com/AI-Team-26/ai.models-evaluator/pull/20) | `Nemotron-3.5-Lightning` |
| [#22](https://github.com/AI-Team-26/ai.models-evaluator/pull/22) | `mindai/macaron-v1-venti` |
| [#23](https://github.com/AI-Team-26/ai.models-evaluator/pull/23) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket.gguf` |
| [#27](https://github.com/AI-Team-26/ai.models-evaluator/pull/27) | `Qwen3.5-27B-IQ3_M_(gammaception)_128k` |
| [#29](https://github.com/AI-Team-26/ai.models-evaluator/pull/29) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[64k]` |
| [#30](https://github.com/AI-Team-26/ai.models-evaluator/pull/30) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[64k]` |
| [#31](https://github.com/AI-Team-26/ai.models-evaluator/pull/31) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k] (Reasoning: medium)` |
| [#32](https://github.com/AI-Team-26/ai.models-evaluator/pull/32) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k] (Reasoning: medium)` |
| [#33](https://github.com/AI-Team-26/ai.models-evaluator/pull/33) | `Qwen3.8-27B-Uncensored-Aggressive-IQ3_M_(HauhauCS)_[128k] (Reasoning: medium)` |
| [#34](https://github.com/AI-Team-26/ai.models-evaluator/pull/34) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` |
| [#35](https://github.com/AI-Team-26/ai.models-evaluator/pull/35) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` |
| [#36](https://github.com/AI-Team-26/ai.models-evaluator/pull/36) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` |
| [#37](https://github.com/AI-Team-26/ai.models-evaluator/pull/37) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` |
| [#38](https://github.com/AI-Team-26/ai.models-evaluator/pull/38) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` |
| [#39](https://github.com/AI-Team-26/ai.models-evaluator/pull/39) | `Qwen3.8-27B-Cold-Fusion-GAIN-V1.1-MTP-IQ3_M_(davidau)_64k` |
| [#40](https://github.com/AI-Team-26/ai.models-evaluator/pull/40) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` |
| [#41](https://github.com/AI-Team-26/ai.models-evaluator/pull/41) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` |
| [#42](https://github.com/AI-Team-26/ai.models-evaluator/pull/42) | `Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[80k]` |
| [#43](https://github.com/AI-Team-26/ai.models-evaluator/pull/43) | `Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[80k]` |
| [#44](https://github.com/AI-Team-26/ai.models-evaluator/pull/44) | `Gemma-4-26B-Q4_0_(Google)_128k` |
| [#45](https://github.com/AI-Team-26/ai.models-evaluator/pull/45) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket_160k` |
| [#46](https://github.com/AI-Team-26/ai.models-evaluator/pull/46) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket_160k` |
| [#47](https://github.com/AI-Team-26/ai.models-evaluator/pull/47) | `Gemma-4-26B-A4B-it-MXFP4_MOE_noctrex.gguf` |
| [#48](https://github.com/AI-Team-26/ai.models-evaluator/pull/48) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` |
| [#49](https://github.com/AI-Team-26/ai.models-evaluator/pull/49) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` |
| [#52](https://github.com/AI-Team-26/ai.models-evaluator/pull/52) | `openai/gpt-5.6-luna` |
| [#57](https://github.com/AI-Team-26/ai.models-evaluator/pull/57) | `Tiel-Coder-35B-A3B-UD-IQ4_XS_(peculiar)_64k` |
| [#59](https://github.com/AI-Team-26/ai.models-evaluator/pull/59) | `Qwen3.8-27B-UD-IQ4_XS_(peculiar)_64k` |
| [#60](https://github.com/AI-Team-26/ai.models-evaluator/pull/60) | `Qwen3.8-27B-UD-IQ4_XS_(peculiar)_64k` |

PR #28 documents the experimental history but does not implement `feat/12`, so it is intentionally excluded. PR #25 is similarly documentation-only. PR #19 implements Avalonia UI scaffolding and is also excluded.

The executor model names above are taken from each PR description. For PR #20, the model is identified by the PR title/description as `Nemotron-3.5-Lightning` rather than by an explicit `Implemented by` line.

## 2. Evaluation Process #1 — Comparative Evaluation

Each model is evaluated against the `feat/12_settings_expansion` specification in `TODO.md` and against the existing repository baseline.

### Required functionality

The implementation should:

- Add `ApplicationSettings.Host`.
- Add editable `CacheTypeK` and `CacheTypeV` settings.
- Add editable `SamplingDefaults` with temperature, top-k, top-p, min-p, repeat penalty, and repeat-last-n.
- Add read-only `ServerDefaults` containing the remaining specified llama-server defaults.
- Add per-model `ModelSettings.Alias`.
- Preserve compatibility with old `Settings.json` files.
- Auto-generate an alias from the GGUF filename when the alias is empty.
- Expose editable fields through the settings UI.
- Display read-only server defaults in the settings UI.
- Avoid unrelated `LlamaServerManager` changes or unrelated regressions.

### Verification procedure

Each implementation was inspected in an isolated worktree and evaluated using:

1. A diff review against `main`.
2. `dotnet build AI.Evaluator.slnx` with isolated output.
3. `dotnet test AI.Evaluator.slnx`.
4. Inspection of entity definitions and `SettingsManager.Load()` normalization.
5. Inspection of `SettingsView` add/edit/display flows.
6. Review-thread status from GitHub.

The repository's existing `TargetCodeTests` contain intentional failing tests. The relevant comparison is whether an implementation introduces new failures. All twelve evaluated models produced the same existing result: 4 passing and 9 failing target-code tests. PR #18 additionally introduced three settings tests, all of which passed.

### Scoring

| Category | Points |
|---|---:|
| Specification coverage | 30 |
| Build and regression safety | 20 |
| Backward compatibility | 20 |
| UI and behavioral correctness | 15 |
| Code quality | 10 |
| Scope and cleanliness | 5 |
| **Total** | **100** |

The scores are comparative engineering judgments based on the implementation and verification above. They do not measure the capability of the underlying model in general.

## 3. Evaluation Process #2 — Deterministic Evaluation

This process is intended to make future model comparisons reproducible. It does not replace the existing scores in this document.

### 3.1 Freeze the evaluation target

For each model run, record:

- PR number and model name;
- implementation commit SHA and base commit SHA;
- evaluation date;
- build and test commands.

The PR provides traceability, the implementation is the artifact being evaluated, and the model is the subject being compared.

### 3.2 Use a binary feature checklist

Evaluate each required feature as Pass or Fail:

- `ApplicationSettings.Host`;
- editable `CacheTypeK` and `CacheTypeV`;
- `SamplingDefaults` fields and defaults;
- `ServerDefaults` fields, types, and defaults;
- `ModelSettings.Alias`;
- compatibility with old settings files;
- alias auto-generation;
- editable settings UI fields;
- read-only settings UI display;
- absence of unrelated changes.

Derive the specification score from this checklist rather than assigning it by general judgment.

### 3.3 Use fixed build and test outcomes

Record separately:

- existing baseline test failures;
- newly introduced failures;
- feature-specific tests;
- tests that were not run.

Use fixed outcomes for build and regression scoring:

- **20/20** — builds with zero warnings and introduces no new test failures;
- **15/20** — builds, but has warnings or incomplete test evidence;
- **10/20** — builds only partially or has uncertain regression status;
- **0/20** — does not compile.

### 3.4 Use fixed compatibility fixtures

Run the same fixtures for every implementation:

1. complete current settings;
2. old settings with all new fields missing;
3. missing `Models`;
4. missing nested defaults;
5. empty alias;
6. null alias in an existing model.

Record Pass or Fail for each fixture.

### 3.5 Use fixed UI scenarios

Verify the same scenarios for every implementation:

- edit host, cache types, and each sampling value;
- add a model with an empty alias;
- add a model with an explicit alias;
- edit and clear an existing alias;
- display all read-only values.

### 3.6 Evaluate code quality with a fixed checklist

Check:

- correct types;
- no nullable warnings;
- consistent serialization attributes;
- no duplicated sources of truth;
- focused diff;
- no unrelated behavior changes.

## 4. Evaluation results

Eighteen models in this evaluation were inspected in isolated worktrees and scored against the same `feat/12_settings_expansion` specification; the remaining fourteen implementations are listed at the bottom of this section as unevaluated placeholders. Each newly evaluated test run produced the documented baseline result of 4 passing and 9 intentionally failing tests. PR #18 additionally introduced three settings tests, all of which passed. No unresolved review threads were present on the six newly evaluated implementations (#27, #29, #30, #57, #59, #60) at evaluation time.

| PR | Model | Spec | Build/Reg | Compat | UI/Beh | Code | Scope | **Total** | **Stars** |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|
| [#60](https://github.com/AI-Team-26/ai.models-evaluator/pull/60) | `Qwen3.8-27B-UD-IQ4_XS_(peculiar)_64k` | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 4/5 | **93/100** | ★★★★ |
| [#59](https://github.com/AI-Team-26/ai.models-evaluator/pull/59) | `Qwen3.8-27B-UD-IQ4_XS_(peculiar)_64k` | 29/30 | 20/20 | 18/20 | 14/15 | 8/10 | 4/5 | **93/100** | ★★★★ |
| [#30](https://github.com/AI-Team-26/ai.models-evaluator/pull/30) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[64k]` | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 4/5 | **93/100** | ★★★★ |
| [#29](https://github.com/AI-Team-26/ai.models-evaluator/pull/29) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[64k]` | 29/30 | 20/20 | 17/20 | 14/15 | 8/10 | 4/5 | **92/100** | ★★★★ |
| [#57](https://github.com/AI-Team-26/ai.models-evaluator/pull/57) | `Tiel-Coder-35B-A3B-UD-IQ4_XS_(peculiar)_64k` | 29/30 | 20/20 | 16/20 | 14/15 | 8/10 | 3/5 | **90/100** | ★★★★ |
| [#27](https://github.com/AI-Team-26/ai.models-evaluator/pull/27) | `Qwen3.5-27B-IQ3_M_(gammaception)_128k` | 12/30 | 20/20 | 13/20 | 2/15 | 7/10 | 4/5 | **58/100** |  |
| [#52](https://github.com/AI-Team-26/ai.models-evaluator/pull/52) | `openai/gpt-5.6-luna` | 30/30 | 20/20 | 18/20 | 14/15 | 9/10 | 5/5 | **96/100** | ★★★★★ |
| [#18](https://github.com/AI-Team-26/ai.models-evaluator/pull/18) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket.gguf` | 29/30 | 20/20 | 18/20 | 14/15 | 9/10 | 4/5 | **94/100** | ★★★★ |
| [#34](https://github.com/AI-Team-26/ai.models-evaluator/pull/34) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` | 29/30 | 20/20 | 18/20 | 14/15 | 9/10 | 4/5 | **94/100** | ★★★★ |
| [#17](https://github.com/AI-Team-26/ai.models-evaluator/pull/17) | `Qwen3.5-27B-IQ4_XS_unsloth.gguf` | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 4/5 | **93/100** | ★★★★ |
| [#45](https://github.com/AI-Team-26/ai.models-evaluator/pull/45) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket_160k` | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 4/5 | **93/100** | ★★★★ |
| [#46](https://github.com/AI-Team-26/ai.models-evaluator/pull/46) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket_160k` | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 4/5 | **93/100** | ★★★★ |
| [#22](https://github.com/AI-Team-26/ai.models-evaluator/pull/22) | `mindai/macaron-v1-venti` | 29/30 | 20/20 | 17/20 | 14/15 | 8/10 | 4/5 | **92/100** | ★★★★ |
| [#23](https://github.com/AI-Team-26/ai.models-evaluator/pull/23) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket.gguf` | 29/30 | 20/20 | 17/20 | 14/15 | 8/10 | 4/5 | **92/100** | ★★★★ |
| [#48](https://github.com/AI-Team-26/ai.models-evaluator/pull/48) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` | 27/30 | 20/20 | 14/20 | 12/15 | 7/10 | 3/5 | **83/100** |  |
| [#20](https://github.com/AI-Team-26/ai.models-evaluator/pull/20) | `Nemotron-3.5-Lightning` | 22/30 | 20/20 | 15/20 | 8/15 | 5/10 | 3/5 | **73/100** |  |
| [#16](https://github.com/AI-Team-26/ai.models-evaluator/pull/16) | `Qwen3-Coder-Next-REAP-40B-A3B.i1-IQ3_M_mradermacher.gguf` | 12/30 | 20/20 | 13/20 | 2/15 | 5/10 | 4/5 | **56/100** |  |
| [#44](https://github.com/AI-Team-26/ai.models-evaluator/pull/44) | `Gemma-4-26B-Q4_0_(Google)_128k` | 22/30 | 0/20 | 16/20 | 7/15 | 4/10 | 2/5 | **51/100** |  |

### Not yet evaluated

The following implementations correspond to open PRs that implement `feat/12_settings_expansion` and are listed in section 1, but have not been inspected in this evaluation. They are listed here as placeholders so the unevaluated set is visible; per-implementation descriptions are intentionally omitted.

| PR | Model | Spec | Build/Reg | Compat | UI/Beh | Code | Scope | **Total** | **Stars** |
|---:|---|---:|---:|---:|---:|---:|---:|---:|---:|
| [#27](https://github.com/AI-Team-26/ai.models-evaluator/pull/27) | `Qwen3.5-27B-IQ3_M_(gammaception)_128k` | 12/30 | 20/20 | 13/20 | 2/15 | 7/10 | 4/5 | **58/100** |  |
| [#29](https://github.com/AI-Team-26/ai.models-evaluator/pull/29) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[64k]` | 29/30 | 20/20 | 17/20 | 14/15 | 8/10 | 4/5 | **92/100** | ★★★★ |
| [#30](https://github.com/AI-Team-26/ai.models-evaluator/pull/30) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[64k]` | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 4/5 | **93/100** | ★★★★ |
| [#31](https://github.com/AI-Team-26/ai.models-evaluator/pull/31) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k] (Reasoning: medium)` | — | — | — | — | — | — | — | — |
| [#32](https://github.com/AI-Team-26/ai.models-evaluator/pull/32) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k] (Reasoning: medium)` | — | — | — | — | — | — | — | — |
| [#33](https://github.com/AI-Team-26/ai.models-evaluator/pull/33) | `Qwen3.8-27B-Uncensored-Aggressive-IQ3_M_(HauhauCS)_[128k] (Reasoning: medium)` | — | — | — | — | — | — | — | — |
| [#35](https://github.com/AI-Team-26/ai.models-evaluator/pull/35) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` | — | — | — | — | — | — | — | — |
| [#36](https://github.com/AI-Team-26/ai.models-evaluator/pull/36) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` | — | — | — | — | — | — | — | — |
| [#37](https://github.com/AI-Team-26/ai.models-evaluator/pull/37) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` | — | — | — | — | — | — | — | — |
| [#38](https://github.com/AI-Team-26/ai.models-evaluator/pull/38) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` | — | — | — | — | — | — | — | — |
| [#39](https://github.com/AI-Team-26/ai.models-evaluator/pull/39) | `Qwen3.8-27B-Cold-Fusion-GAIN-V1.1-MTP-IQ3_M_(davidau)_64k` | — | — | — | — | — | — | — | — |
| [#40](https://github.com/AI-Team-26/ai.models-evaluator/pull/40) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` | — | — | — | — | — | — | — | — |
| [#41](https://github.com/AI-Team-26/ai.models-evaluator/pull/41) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` | — | — | — | — | — | — | — | — |
| [#42](https://github.com/AI-Team-26/ai.models-evaluator/pull/42) | `Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[80k]` | — | — | — | — | — | — | — | — |
| [#43](https://github.com/AI-Team-26/ai.models-evaluator/pull/43) | `Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[80k]` | — | — | — | — | — | — | — | — |
| [#47](https://github.com/AI-Team-26/ai.models-evaluator/pull/47) | `Gemma-4-26B-A4B-it-MXFP4_MOE_noctrex.gguf` | — | — | — | — | — | — | — | — |
| [#49](https://github.com/AI-Team-26/ai.models-evaluator/pull/49) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` | — | — | — | — | — | — | — | — |
| [#57](https://github.com/AI-Team-26/ai.models-evaluator/pull/57) | `Tiel-Coder-35B-A3B-UD-IQ4_XS_(peculiar)_64k` | 29/30 | 20/20 | 16/20 | 14/15 | 8/10 | 3/5 | **90/100** | ★★★★ |

### Model used for PR #52 — 96/100 (`openai/gpt-5.6-luna`)

The model used for PR #52 produced the strongest implementation of all evaluated models. It provides the complete expanded entity model — `Host`, `CacheTypeK/V`, `SamplingDefaults`, `ServerDefaults`, and `ModelSettings.Alias` — with correct types (`bool` for `KvUnified`, `ContextShift`, `ReasoningPreserve`) and accurate defaults matching the specification. The backward-compatibility handling in `SettingsManager.Load()` is comprehensive: `Host`, `CacheTypeK`, `CacheTypeV`, `SamplingDefaults`, `ServerDefaults`, and `Models` are all null-coalesced. The settings UI is fully wired: `EditGeneralSettings` accepts `Host`, the cache types, and delegates to a clean `EditSamplingDefaults` helper; `AddModel`/`EditModel` accept and auto-generate the alias from the GGUF filename; `ShowCurrentSettings` displays the new fields across multiple lines and shows the alias in the model list. The diff is small and focused on the four expected files (`Entities.cs`, `SettingsManager.cs`, `SettingsView.cs`, `TODO.md`), builds cleanly with zero warnings in the project code, and produces the same 4-pass/9-fail baseline test result. Minor deductions: legacy model entries with a null `Alias` are not normalized during load (alias regeneration happens only in display/add/edit flows), and `ShowCurrentSettings` repeats the `Server defaults (read-only):` prefix across multiple lines.

**Conclusion:** best candidate among all evaluated PRs.

### Model used for PR #16 — 56/100 (`Qwen3-Coder-Next-REAP-40B-A3B.i1-IQ3_M_mradermacher.gguf`)

The model used for PR #16 produced a partial implementation. It adds the expanded entities and some load-time defaults, but does not update `SettingsView`. Consequently, the new settings are not editable or fully displayed through the UI, and aliases are not supported in the add/edit flows. It also uses nullable strings with non-null defaults, inconsistent JSON attributes, string types for boolean-like server flags, and does not handle a missing `Models` list. It has unresolved review threads.

**Conclusion:** incomplete and not recommended without further work.

### Model used for PR #17 — 93/100 (`Qwen3.5-27B-IQ4_XS_unsloth.gguf`)

The model used for PR #17 produced a strong, nearly complete implementation. It provides the expanded data model, complete settings UI coverage, alias editing and generation, read-only server-default display, and backward-compatible handling of missing settings sections and model lists. Review corrections improved its markup output, boolean types, neutral reasoning-message default, and TODO accuracy. Its main limitation is the lack of dedicated automated settings tests.

**Conclusion:** excellent candidate.

### Model used for PR #18 — 94/100 (`KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket.gguf`)

The model used for PR #18 produced the strongest implementation in this comparison. It combines complete data and UI changes with a dedicated `EvaluatorSettingsTests` project. The three added settings tests passed, and review corrections ensured the test project is included in the solution and exercises `SettingsManager` rather than merely duplicating its logic. The test setup uses reflection/path overriding because of the static manager design, which adds some complexity, but it provides the best verification evidence among the evaluated models.

**Conclusion:** best overall candidate.

### Model used for PR #20 — 73/100 (`Nemotron-3.5-Lightning`)

The model used for PR #20 produced a functional implementation, but it has several design and completeness problems. `ServerDefaults` duplicates editable application fields, creating multiple possible sources of truth. The general settings flow does not expose all required host/cache editing, and the model edit flow lacks alias editing. It also removes the llama.cpp path from the settings display, contains a load-error typo, produces nullable-analysis warnings, and has unresolved review threads.

**Conclusion:** usable as an experiment, but weaker than the complete implementations.

### Model used for PR #22 — 92/100 (`mindai/macaron-v1-venti`)

The model used for PR #22 produced a complete and well-reviewed implementation. It covers the required entities, backward compatibility, settings UI, aliases, and read-only server defaults. It has no unresolved review threads and builds successfully. Its limitations are the lack of dedicated settings tests, nullable nested records normalized later by the manager, and an opinionated hardcoded reasoning-budget message.
**Conclusion:** strong candidate, but not uniquely superior.

### Model used for PR #23 — 92/100 (`KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket.gguf`)

The model used for PR #23 produced another complete implementation with a focused diff. It covers the data model, compatibility handling, UI flows, alias generation, and read-only display. It uses a neutral empty reasoning-budget message and has no unresolved review threads. It lacks dedicated settings tests and does not normalize null aliases inside existing model entries.

**Conclusion:** strong candidate, effectively tied with the model used for PR #22.

### Model used for PR #34 — 94/100 (`Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf`)

The model used for PR #34 produced the strongest of the five requested implementations. It provides the complete settings model, load-time defaults, editable settings prompts, alias generation, alias editing, and a read-only server-defaults display. It builds cleanly with zero warnings, and its test result matches the repository baseline. The main compatibility gap is that `Normalize()` iterates over `s.Models ?? []` without assigning an empty list back to `s.Models`; an old file without a `models` array can still leave the list null for later callers. It also keeps the specification's `DraftPMin` field under the name `DraftPMIn`, which is a minor naming-quality issue.

**Conclusion:** best candidate among the five requested implementations, subject to fixing the `Models` normalization edge case.

### Model used for PR #45 — 93/100 (`KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket_160k`)

The model used for PR #45 produced a complete and well-structured implementation. It covers the required entities, backward-compatible defaults, UI editing and display, alias generation, and read-only server defaults. It builds cleanly with zero warnings and has only the documented baseline test failures. Its compatibility handling correctly initializes a missing `Models` collection, but it does not normalize aliases on existing legacy model entries. In the edit flow, submitting an empty alias leaves the previous alias unchanged rather than applying the documented auto-generation behavior. The `DraftPMIn` property name is also inconsistent with the specification.

**Conclusion:** excellent candidate, narrowly behind PR #34.

### Model used for PR #46 — 93/100 (`KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket_160k`)

The model used for PR #46 produced an implementation that has the same settings implementation and verification profile as PR #45. It builds cleanly with zero warnings and produces the same 4-pass/9-fail baseline test result. It covers the required model, compatibility, UI, alias, and read-only-default functionality. The same limitations apply: legacy aliases are not normalized, empty alias input during model editing does not regenerate the alias, and `DraftPMIn` is a minor naming mismatch.

**Conclusion:** effectively tied with the model used for PR #45; the implementation is strong but should address the alias and naming details.

### Model used for PR #48 — 83/100 (`Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]`)

The model used for PR #48 implemented the main feature and builds cleanly, with the same baseline test result. It includes the required settings UI and alias flows, and it adds the useful `agent_build/` ignore rule. However, its compatibility normalization does not initialize a missing `Models` list or legacy model aliases. Its boolean-like `ServerDefaults` values (`KvUnified`, `ContextShift`, and `ReasoningPreserve`) are represented as strings, making invalid values possible and weakening type safety. The edit flow does not auto-generate an alias when an existing alias is cleared, and its implementation has less explanatory structure than PRs #34, #45, and #46.

**Conclusion:** functional and buildable, but materially weaker in compatibility and type correctness.

### Model used for PR #44 — 51/100 (`Gemma-4-26B-Q4_0_(Google)_128k`)

The model used for PR #44 produced an implementation that covers much of the requested feature surface and includes settings normalization, UI display/editing, alias support, and read-only defaults. However, it does not compile: `SettingsView.cs` declares `public.const string EditModel`, producing compiler error `CS1519`. Because the application cannot build, no meaningful regression-safety credit is awarded. The diff also removes or compresses substantial existing UI logic, increasing regression risk. Its server-default representation retains string types for boolean-like flags and its normalization does not safely handle a null deserialized `Models` list.

**Conclusion:** not mergeable without correcting the compile error and revalidating the compressed UI changes.

## 5. Deterministic evidence for PRs #27, #29, #30, #57, #59, and #60

The six targets were evaluated from their submitted commits in isolated worktrees, without rebasing. The recorded PR metadata is:

| PR | Base SHA | Head SHA | Feature checklist (entities / manager / UI / compatibility) |
|---:|---|---|---|
| #27 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `f12a5dc61cd3bdfef7d09170715039b13adaf199` | ✓ / ✓ / ✗ / partial |
| #29 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `634e9964aca39bfecc8da71c6bdd544c34d102c5` | ✓ / ✓ / ✓ / ✓ |
| #30 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `9e3a74c42e517a175008467084821360f720b692` | ✓ / ✓ / ✓ / partial |
| #57 | `208e140d527aa341ebda7e81ac9bc32df547d2e4` | `5deda4e2bd407df677906e8f3f475dff2ad8808f` | ✓ / ✓ / ✓ / ✓ |
| #59 | `208e140d527aa341ebda7e81ac9bc32df547d2e4` | `bf04025fc82a12837fc34039deec6309da4f288b` | ✓ / ✓ / ✓ / ✓ |
| #60 | `7ce179c050f34a8822e86f981bf230d51bf0360a` | `cddf6b4f74add9e161bcaf6a63553a457891e108` | ✓ / ✓ / ✓ / partial |

The implementation diffs were limited to the settings entities/manager and, except for #27, `SettingsView.cs`. Each PR also changed `TODO.md`; #57 additionally changed `Evaluation.md`, which is scope-unauthorized for the implementation task and was not treated as feature credit. No unresolved review threads were found.

### Fixed verification outcomes

| PRs | Restore/build command | Test command | Result |
|---|---|---|---|
| #27, #29, #30, #57, #59, #60 | `dotnet restore AI.Evaluator.slnx` then `dotnet build src/Evaluator/Evaluator.csproj -o agent_build --no-restore` | `dotnet test tests/TargetCodeTests/TargetCodeTests.csproj --no-restore` | Every build: 0 warnings, 0 errors. Every test run: 4 passed, 9 baseline failures. |

The baseline failures are in pre-existing `TargetCodeTests` coverage (`SumRange`, `SafeProduct`, and `SplitCsv`); no settings-specific tests were supplied by these PRs. UI scenarios were assessed statically from `SettingsView.cs`: #27 fails the required edit/display scenario because it contains no UI implementation; #29, #30, #57, #59, and #60 expose host/cache/sampling/model-alias flows and display read-only server defaults. Compatibility fixtures used: a legacy JSON object with omitted new sections, a legacy model with omitted alias, and explicit `null` sections/lists. All five UI implementations normalize the new sections; #29, #57, and #59 also normalize model aliases or lists, while #30 and #60 leave some null collection/alias edges to callers.

Comparative scores above preserve the existing Process #1 scoring system. The deterministic checklist confirms the same ordering factors: complete feature coverage, buildability, legacy-load behavior, observable UI flows, focused diffs, and unauthorized planning-document edits.

| PR | Compatibility/UI/code-quality notes |
|---:|---|
| #27 | Correct entity additions and manager normalization, but no settings UI; comments and naming are serviceable. |
| #29 | Complete CLI UI and strongest normalization among the Q3 pair; minor property-name inconsistencies (`DraftPMIn`, `UbatchSize`) reduce maintainability. |
| #30 | Complete UI and readable structure; boolean/string representation and missing model-list normalization reduce compatibility confidence. |
| #57 | Complete read-only display and editable flows; its extra `Evaluation.md` edit and alias update semantics are scope-quality deductions. |
| #59 | Complete UI, explicit boolean server flags, and broad normalization; inconsistent acronym casing (`DraftPMIN`, `UBatchSize`) is a small quality issue. |
| #60 | Complete, focused UI and clean types; omitted model-list/alias normalization and less defensive legacy handling reduce compatibility confidence. |

## Ranking and conclusion

1. **Model used for PR #52 — 96/100** (★★★★★): best overall. Complete data and UI changes, correct types, comprehensive backward compatibility, clean build, zero scope creep.
2. **Model used for PR #18 — 94/100** and **Model used for PR #34 — 94/100** (★★★★, tie): strong complete implementations. PR #18 also includes passing dedicated settings tests; PR #34 should fix the `Models` normalization edge case before merge.
3. **Model used for PR #17 — 93/100**, **Model used for PR #45 — 93/100**, and **Model used for PR #46 — 93/100** (★★★★, tie): excellent complete implementations, narrowly behind the top pair. PR #45 and PR #46 share the same settings implementation; both should address alias and naming details.
4. **Model used for PR #22 — 92/100** and **Model used for PR #23 — 92/100** (★★★★, tie): complete and well-reviewed, but not proven to be the best.
5. **Model used for PR #48 — 83/100**: buildable but materially weaker in type safety and backward compatibility.
6. **Model used for PR #20 — 73/100**: partial design and UI issues, plus unresolved review comments.
7. **Model used for PR #16 — 56/100**: incomplete because the UI portion is missing.
8. **Model used for PR #44 — 51/100**: currently uncompilable and therefore not mergeable.

Based on the available evidence across all eighteen evaluated models, the model used for PR #52 is the strongest candidate, followed by the models used for PR #18 and PR #34 tied for second.


