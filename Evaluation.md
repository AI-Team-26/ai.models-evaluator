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
| [#63](https://github.com/AI-Team-26/ai.models-evaluator/pull/63) | `KAT-Coder-V2.5-Dev_Q2_K-AllGPU_(offmonreal)_160k` |
| [#65](https://github.com/AI-Team-26/ai.models-evaluator/pull/65) | `KAT-Coder-V2.5-Dev_Q2_K-AllGPU_(offmonreal)_160k` |
| [#67](https://github.com/AI-Team-26/ai.models-evaluator/pull/67) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k]` |
| [#68](https://github.com/AI-Team-26/ai.models-evaluator/pull/68) | `KAT-Coder-V2.5-Dev_Q3_K_M_imatrix_MTP_(offmonreal)_64k` |
| [#71](https://github.com/AI-Team-26/ai.models-evaluator/pull/71) | `KAT-Coder-V2.5-Dev-APEX-dynamic-v2_(myric)_192k` |
| [#87](https://github.com/AI-Team-26/ai.models-evaluator/pull/87) | `Qwen3.8-27B_UD-IQ4-KS_Unsloth_[32k] Q8/Q8 (mtp:2/7)` |
| [#88](https://github.com/AI-Team-26/ai.models-evaluator/pull/88) | `Qwen3.8-27B-i1-IQ4_XS-GGUF-Smaller_(jrell)_Q8-Q8_[56k] (mtp:2/7)` |
| [#90](https://github.com/AI-Team-26/ai.models-evaluator/pull/90) | `Qwen3.8-27B-ZB4.00-MIN-v5-IQ4_XS_tooltd.gguf` |
| [#96](https://github.com/AI-Team-26/ai.models-evaluator/pull/96) | `Qwen3.8-27B-UD-Q2_K_XL_(Unsloth)_[128k]` |
| [#97](https://github.com/AI-Team-26/ai.models-evaluator/pull/97) | `Qwen3.8-27B-Q3_K_S_(Unsloth)_[64k]_Q8Q8` |
| [#98](https://github.com/AI-Team-26/ai.models-evaluator/pull/98) | `Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui` |
| [#38](https://github.com/AI-Team-26/ai.models-evaluator/pull/38) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` |
| [#40](https://github.com/AI-Team-26/ai.models-evaluator/pull/40) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` |
| [#41](https://github.com/AI-Team-26/ai.models-evaluator/pull/41) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` |
| [#43](https://github.com/AI-Team-26/ai.models-evaluator/pull/43) | `Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[80k]` |
| [#49](https://github.com/AI-Team-26/ai.models-evaluator/pull/49) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` |
| [#103](https://github.com/AI-Team-26/ai.models-evaluator/pull/103) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` (run #2) |
| [#104](https://github.com/AI-Team-26/ai.models-evaluator/pull/104) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` (run #3) |
| [#105](https://github.com/AI-Team-26/ai.models-evaluator/pull/105) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` (run #3) |

PR #28 documents the experimental history but does not implement `feat/12`, so it is intentionally excluded. PR #25 is similarly documentation-only. PR #19 implements Avalonia UI scaffolding and is also excluded. PR #89 is a documentation/evaluation PR and is similarly excluded.

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

The repository's existing `TargetCodeTests` contain intentional failing tests. The relevant comparison is whether an implementation introduces new failures. All currently evaluated models produced the same existing result: 4 passing and 9 failing target-code tests. PR #18 additionally introduced three settings tests, all of which passed.

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

Forty-four models in this evaluation were inspected in isolated worktrees and scored against the same `feat/12_settings_expansion` specification; there are no remaining unevaluated implementations. Each newly evaluated test run produced the documented baseline result of 4 passing and 9 intentionally failing tests. PR #18 additionally introduced three settings tests, all of which passed. No unresolved review threads were found for the implementations checked in this re-evaluation.

| PR | Model | Time | Spec | Build/Reg | Compat | UI/Beh | Code | Scope | **Total** | **Stars** |
|---:|---|---|---:|---:|---:|---:|---:|---:|---:|---:|
| [#67](https://github.com/AI-Team-26/ai.models-evaluator/pull/67) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k]` | 00:00 |  29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 3/5 | **92/100** | ★★★★ |
| [#68](https://github.com/AI-Team-26/ai.models-evaluator/pull/68) | `KAT-Coder-V2.5-Dev_Q3_K_M_imatrix_MTP_(offmonreal)_64k` | 00:00 | 30/30 | 20/20 | 17/20 | 14/15 | 7/10 | 4/5 | **92/100** | ★★★★ |
| [#71](https://github.com/AI-Team-26/ai.models-evaluator/pull/71) | `KAT-Coder-V2.5-Dev-APEX-dynamic-v2_(myric)_192k` | 00:00 | 30/30 | 20/20 | 19/20 | 15/15 | 8/10 | 2/5 | **94/100** | ★★★★ |
| [#33](https://github.com/AI-Team-26/ai.models-evaluator/pull/33) | `Qwen3.8-27B-Uncensored-Aggressive-IQ3_M_(HauhauCS)_[128k] (Reasoning: medium)` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 7/10 | 4/5 | **91/100** | ★★★★ |
| [#39](https://github.com/AI-Team-26/ai.models-evaluator/pull/39) | `Qwen3.8-27B-Cold-Fusion-GAIN-V1.1-MTP-IQ3_M_(davidau)_64k` | 00:00 | 30/30 | 20/20 | 17/20 | 14/15 | 7/10 | 3/5 | **91/100** | ★★★★ |
| [#42](https://github.com/AI-Team-26/ai.models-evaluator/pull/42) | `Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[80k]` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 8/10 | 2/5 | **90/100** | ★★★★ |
| [#47](https://github.com/AI-Team-26/ai.models-evaluator/pull/47) | `Gemma-4-26B-A4B-it-MXFP4_MOE_noctrex.gguf` | 00:00 | 29/30 | 20/20 | 16/20 | 13/15 | 5/10 | 1/5 | **84/100** |  |
| [#65](https://github.com/AI-Team-26/ai.models-evaluator/pull/65) | `KAT-Coder-V2.5-Dev_Q2_K-AllGPU_(offmonreal)_160k` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 3/5 | **92/100** | ★★★★ |
| [#63](https://github.com/AI-Team-26/ai.models-evaluator/pull/63) | `KAT-Coder-V2.5-Dev_Q2_K-AllGPU_(offmonreal)_160k` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 3/5 | **92/100** | ★★★★ |
| [#60](https://github.com/AI-Team-26/ai.models-evaluator/pull/60) | `Qwen3.8-27B-UD-IQ4_XS_(peculiar)_64k` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 4/5 | **93/100** | ★★★★ |
| [#59](https://github.com/AI-Team-26/ai.models-evaluator/pull/59) | `Qwen3.8-27B-UD-IQ4_XS_(peculiar)_64k` | 00:00 | 29/30 | 20/20 | 18/20 | 14/15 | 8/10 | 4/5 | **93/100** | ★★★★ |
| [#30](https://github.com/AI-Team-26/ai.models-evaluator/pull/30) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[64k]` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 3/5 | **92/100** | ★★★★ |
| [#29](https://github.com/AI-Team-26/ai.models-evaluator/pull/29) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[64k]` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 8/10 | 3/5 | **91/100** | ★★★★ |
| [#57](https://github.com/AI-Team-26/ai.models-evaluator/pull/57) | `Tiel-Coder-35B-A3B-UD-IQ4_XS_(peculiar)_64k` | 00:00 | 29/30 | 20/20 | 16/20 | 14/15 | 8/10 | 2/5 | **89/100** | ★★★★ |
| [#27](https://github.com/AI-Team-26/ai.models-evaluator/pull/27) | `Qwen3.5-27B-IQ3_M_(gammaception)_128k` | 00:00 | 12/30 | 20/20 | 13/20 | 2/15 | 7/10 | 3/5 | **57/100** |  |
| [#52](https://github.com/AI-Team-26/ai.models-evaluator/pull/52) | `openai/gpt-5.6-luna` | 00:00 | 30/30 | 20/20 | 18/20 | 14/15 | 9/10 | 4/5 | **95/100** | ★★★★★ |
| [#18](https://github.com/AI-Team-26/ai.models-evaluator/pull/18) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket.gguf` | 00:00 | 29/30 | 20/20 | 18/20 | 14/15 | 9/10 | 3/5 | **93/100** | ★★★★ |
| [#34](https://github.com/AI-Team-26/ai.models-evaluator/pull/34) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` | 00:00 | 29/30 | 20/20 | 18/20 | 14/15 | 9/10 | 3/5 | **93/100** | ★★★★ |
| [#17](https://github.com/AI-Team-26/ai.models-evaluator/pull/17) | `Qwen3.5-27B-IQ4_XS_unsloth.gguf` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 3/5 | **92/100** | ★★★★ |
| [#45](https://github.com/AI-Team-26/ai.models-evaluator/pull/45) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket_160k` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 3/5 | **92/100** | ★★★★ |
| [#46](https://github.com/AI-Team-26/ai.models-evaluator/pull/46) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket_160k` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 3/5 | **92/100** | ★★★★ |
| [#22](https://github.com/AI-Team-26/ai.models-evaluator/pull/22) | `mindai/macaron-v1-venti` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 8/10 | 4/5 | **92/100** | ★★★★ |
| [#23](https://github.com/AI-Team-26/ai.models-evaluator/pull/23) | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket.gguf` | 00:00 | 29/30 | 20/20 | 17/20 | 14/15 | 8/10 | 4/5 | **92/100** | ★★★★ |
| [#48](https://github.com/AI-Team-26/ai.models-evaluator/pull/48) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` | 00:00 | 27/30 | 20/20 | 14/20 | 12/15 | 7/10 | 4/5 | **84/100** |  |
| [#20](https://github.com/AI-Team-26/ai.models-evaluator/pull/20) | `Nemotron-3.5-Lightning` | 00:00 | 22/30 | 20/20 | 15/20 | 8/15 | 5/10 | 3/5 | **73/100** |  |
| [#16](https://github.com/AI-Team-26/ai.models-evaluator/pull/16) | `Qwen3-Coder-Next-REAP-40B-A3B.i1-IQ3_M_mradermacher.gguf` | 00:00 | 12/30 | 20/20 | 13/20 | 2/15 | 5/10 | 3/5 | **55/100** |  |
| [#44](https://github.com/AI-Team-26/ai.models-evaluator/pull/44) | `Gemma-4-26B-Q4_0_(Google)_128k` | 00:00 | 22/30 | 20/20 | 16/20 | 7/15 | 4/10 | 1/5 | **50/100** |  |
| [#35](https://github.com/AI-Team-26/ai.models-evaluator/pull/35) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` | 00:00 | 29/30 | 20/20 | 19/20 | 14/15 | 9/10 | 1/5 | **92/100** | ★★★★ |
| [#31](https://github.com/AI-Team-26/ai.models-evaluator/pull/31) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k] (Reasoning: medium)` | 00:00 | 29/30 | 20/20 | 14/20 | 14/15 | 9/10 | 1/5 | **87/100** | ★★★ |
| [#32](https://github.com/AI-Team-26/ai.models-evaluator/pull/32) | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k] (Reasoning: medium)` | 00:00 | 27/30 | 20/20 | 17/20 | 14/15 | 8/10 | 1/5 | **87/100** | ★★★ |
| [#36](https://github.com/AI-Team-26/ai.models-evaluator/pull/36) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` | 00:00 | 28/30 | 20/20 | 16/20 | 14/15 | 8/10 | 1/5 | **87/100** | ★★★ |
| [#37](https://github.com/AI-Team-26/ai.models-evaluator/pull/37) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` | 00:00 | 28/30 | 20/20 | 13/20 | 13/15 | 8/10 | 1/5 | **83/100** |  |
| [#87](https://github.com/AI-Team-26/ai.models-evaluator/pull/87) | `Qwen3.8-27B_UD-IQ4-KS_Unsloth_[32k] Q8/Q8 (mtp:2/7)` | 00:04 | 30/30 | 20/20 | 17/20 | 12/15 | 9/10 | 4/5 | **92/100** | ★★★★ |
| [#88](https://github.com/AI-Team-26/ai.models-evaluator/pull/88) | `Qwen3.8-27B-i1-IQ4_XS-GGUF-Smaller_(jrell)_Q8-Q8_[56k] (mtp:2/7)` | 06:31 | 27/30 | 20/20 | 14/20 | 14/15 | 8/10 | 5/5 | **88/100** | ★★★★ |
| [#90](https://github.com/AI-Team-26/ai.models-evaluator/pull/90) | `Qwen3.8-27B-ZB4.00-MIN-v5-IQ4_XS_tooltd.gguf` | 07:01 | 27/30 | 20/20 | 14/20 | 14/15 | 8/10 | 5/5 | **88/100** | ★★★★ |
| [#96](https://github.com/AI-Team-26/ai.models-evaluator/pull/96) | `Qwen3.8-27B-UD-Q2_K_XL_(Unsloth)_[128k]` | 00:21 | 28/30 | 20/20 | 16/20 | 12/15 | 8/10 | 1/5 | **85/100** | ★★★ |
| [#97](https://github.com/AI-Team-26/ai.models-evaluator/pull/97) | `Qwen3.8-27B-Q3_K_S_(Unsloth)_[64k]_Q8Q8` | 00:12 | 30/30 | 20/20 | 16/20 | 14/15 | 9/10 | 4/5 | **93/100** | ★★★★ |
| [#98](https://github.com/AI-Team-26/ai.models-evaluator/pull/98) | `Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui` | 00:13 | 26/30 | 20/20 | 18/20 | 14/15 | 8/10 | 4/5 | **90/100** | ★★★★ |
| [#38](https://github.com/AI-Team-26/ai.models-evaluator/pull/38) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` | 00:00 | 27/30 | 20/20 | 17/20 | 14/15 | 9/10 | 1/5 | **88/100** | ★★★★ |
| [#40](https://github.com/AI-Team-26/ai.models-evaluator/pull/40) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` | 00:00 | 26/30 | 20/20 | 17/20 | 14/15 | 9/10 | 1/5 | **87/100** | ★★★ |
| [#41](https://github.com/AI-Team-26/ai.models-evaluator/pull/41) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` | 00:00 | 27/30 | 20/20 | 17/20 | 15/15 | 9/10 | 0/5 | **88/100** | ★★★★ |
| [#43](https://github.com/AI-Team-26/ai.models-evaluator/pull/43) | `Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[80k]` | 00:00 | 26/30 | 20/20 | 14/20 | 11/15 | 9/10 | 1/5 | **81/100** | ★★★ |
| [#49](https://github.com/AI-Team-26/ai.models-evaluator/pull/49) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` | 00:00 | 26/30 | 20/20 | 14/20 | 12/15 | 8/10 | 1/5 | **81/100** | ★★★ |
| [#103](https://github.com/AI-Team-26/ai.models-evaluator/pull/103) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` (run #2) | 00:00 | 27/30 | 20/20 | 14/20 | 13/15 | 8/10 | 1/5 | **83/100** | ★★★ |
| [#104](https://github.com/AI-Team-26/ai.models-evaluator/pull/104) | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` (run #3) | 00:00 | 27/30 | 20/20 | 14/20 | 13/15 | 8/10 | 1/5 | **83/100** | ★★★ |
| [#105](https://github.com/AI-Team-26/ai.models-evaluator/pull/105) | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` (run #3) | 00:00 | 28/30 | 20/20 | 15/20 | 12/15 | 8/10 | 1/5 | **84/100** | ★★★ |

### Model used for PR #71 — 94/100 (`KAT-Coder-V2.5-Dev-APEX-dynamic-v2_(myric)_192k`)

The model used for PR #71 produced a complete implementation with the strongest backward-compatibility handling of all evaluated runs: `Normalize()` null-coalesces the new sections, initializes a missing `Models` list, and additionally normalizes legacy model aliases, eagerly regenerating empty aliases from the GGUF filename on load — closing the compatibility gap that affected nearly every other implementation. It covers the full specification with correctly typed boolean server flags, correct property naming (`DraftPMin`, `UbatchSize`), alias auto-generation on add, and alias regeneration with user feedback in the edit flow. Minor quality deductions: `Entities.cs` was rewritten wholesale (BOM/whitespace churn), `EditSamplingDefaults` repeats six near-identical prompt blocks instead of using a helper, and alias input is not trimmed. Its branch made no `TODO.md` change at all, leaving the task lifecycle unrecorded, so it receives the largest scope deduction of the top-scoring group; its score is **94/100**.

### Model used for PR #35 — 92/100 (`Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf`)

The model used for PR #35 produced a complete, buildable implementation with the best backward-compatibility handling among this batch. It has a dedicated `Normalize()` method that null-coalesces `SamplingDefaults`, `ServerDefaults`, `Host`, `CacheTypeK/V`, and `Models`, and additionally auto-generates aliases from GGUF filenames for legacy model entries on load. All spec fields are present with correct types and naming (`DraftPMin`, `UbatchSize`). The `EditSamplingDefaults` helper method keeps the settings editor clean. `ShowCurrentSettings()` displays all fields including the readonly ServerDefaults section with proper alias display (showing the GGUF filename when alias is empty). Build passes with 0 warnings and the baseline 4-pass/9-fail test result. Its TODO task has a docs block but the implementation checklist is not tracked in TODO.md, so it receives the scope deduction; its score is **92/100**.

**Conclusion:** the strongest candidate in this batch and the best compatibility implementation among the five evaluated PRs.

### Model used for PR #31 — 87/100 (`Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k] (Reasoning: medium)`)

The model used for PR #31 produced a complete, buildable implementation covering all specification features. It has correct types for most fields, proper alias auto-generation in add/edit flows, and a full read-only ServerDefaults display in `ShowCurrentSettings()`. Its weaknesses are backward-compatibility gaps: `SettingsManager.Load()` does not null-coalesce `Models` (missing `Models ??= []`), does not auto-generate aliases from GGUF filenames on load, and uses inline normalization instead of a dedicated `Normalize()` method. The `DraftPMIN` property name is a naming mismatch with the spec (`DraftPMin`). The `EditGeneralSettings` method uses inline `AskDouble`/`AskInt` local functions which is acceptable but less organized than a dedicated helper. Its TODO task remains in `In Progress` with all steps unchecked, and the branch deletes `Evaluation.md` and the `Backlog` section from `TODO.md` (unrelated destructive changes). Its score is **87/100**.

### Model used for PR #32 — 87/100 (`Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k] (Reasoning: medium)`)

The model used for PR #32 produced a complete, buildable implementation with the same overall structure as PR #31. Its key differences from PR #31 are type choices: `Fit` is `bool` (default `false`) instead of string `"off"`, `Reasoning` is `bool` (default `true`) instead of string `"on"`, and `ReasoningBudgetMessage` lacks the surrounding quotes that the spec includes. These type mismatches with the spec are the primary quality concern. On the positive side, `SettingsManager.Load()` null-coalesces `Models ??= []` (better than PR #31), and the `ShowCurrentSettings()` display handles the bool-typed fields with `(d.Fit ? "on" : "off")` conversions. The `DraftPMIN` naming mismatch is also present. Its TODO task remains in `In Progress` with all steps unchecked, and the branch deletes `Evaluation.md` (unrelated). Its score is **87/100**.

### Model used for PR #36 — 87/100 (`Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf`)

The model used for PR #36 produced a complete, buildable implementation with a dedicated `Normalize()` method and alias auto-generation on load. It null-coalesces `SamplingDefaults`, `ServerDefaults`, `Host`, `CacheTypeK/V`, and auto-generates aliases for legacy models. However, it is missing `Models ??= []` in `Normalize()`, which is a backward-compatibility gap for old settings files without a `Models` array. The `DraftPMIn` property name is a naming mismatch (should be `DraftPMin`). `ReasoningBudgetMessage` lacks the surrounding quotes from the spec. The `ShowCurrentSettings()` display uses a `GetAutoAlias` helper method for clean alias display. Its TODO task has a docs block but the implementation checklist is not tracked in TODO.md, and the branch deletes `Evaluation.md` and the `Backlog` section from `TODO.md`. Its score is **87/100**.

### Model used for PR #37 — 83/100 (`Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf`)

The model used for PR #37 produced a buildable implementation covering the main feature surface. It has a `Normalize()` method and correct type choices for most fields (`Fit` as string `"off"`, `DraftPMIn` naming mismatch). However, it has the weakest backward-compatibility handling in this batch: `Normalize()` does not null-coalesce `Models` (missing `Models ??= []`), and it does not auto-generate aliases from GGUF filenames on load — it only does `m.Alias ??= ""`, leaving legacy models with empty aliases. The `ShowCurrentSettings()` display uses placeholder text `"(auto: GGUF filename)"` instead of showing the actual auto-generated alias, and it does not null-coalesce `SamplingDefaults` before accessing its properties (potential `NullReferenceException` if `Normalize()` is bypassed). Its TODO task has a docs block but the implementation checklist is not tracked in TODO.md, and the branch deletes `Evaluation.md`. Its score is **83/100**.

**Conclusion:** PR #35 is the clear winner in this batch with the best compatibility handling; PRs #31, #32, and #36 are tied at 87/100 with different trade-offs; PR #37 lags on compatibility.

### Model used for PR #33 — 91/100 (`Qwen3.8-27B-Uncensored-Aggressive-IQ3_M_(HauhauCS)_[128k] (Reasoning: medium)`)

The model used for PR #33 produced a complete, buildable implementation with a well-normalized `SettingsManager.Load()` — one of the few runs that also initializes a missing `Models` list. It provides all required entities, host/cache/sampling editing via reusable `PromptDouble`/`PromptInt` helpers, an `AutoGenerateAlias` helper used consistently in add, edit, and display flows, and a full read-only server-defaults display. Its weaknesses are the string-typed `FlashAttn` and `Fit` flags, a whole-file rewrite of `Entities.cs`, and a misleading edit prompt: it advertises "empty=auto-gen" but the code keeps the current alias when input is empty. Legacy model aliases are not normalized on load. Its TODO lifecycle is correct (moved to `Completed`), so its score is **91/100**.

**Conclusion:** strong candidate and the best of this batch on compatibility, held back by type and naming details.

### Model used for PR #39 — 91/100 (`Qwen3.8-27B-Cold-Fusion-GAIN-V1.1-MTP-IQ3_M_(davidau)_64k`)

The model used for PR #39 produced a complete implementation with full specification coverage: alias auto-generation on both add and edit (empty input in the edit flow correctly regenerates from the GGUF filename), a dedicated `ShowServerDefaults` read-only section, and clean `GetDoubleInput`/`GetIntInput` helpers. Its compatibility handling covers the new sections but not a missing `Models` list or legacy aliases. Type-quality issues remain: `FlashAttn` and `Fit` are strings, `DraftPMIN` is a naming mismatch, the `ReasoningBudgetMessage` default embeds escaped quotes, and `Entities.cs` was rewritten wholesale. Its TODO task was left in `In Progress` with Step 6 unchecked, so the scope deduction applies and its score is **91/100**.

**Conclusion:** functionally complete with the best alias-edit semantics of this batch; should fix the string-typed flags and TODO lifecycle.

### Model used for PR #42 — 90/100 (`Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[80k]`)

The model used for PR #42 produced a complete, buildable implementation with correctly typed boolean server flags, correct property naming (`DraftPMin`, `UbatchSize`), host/cache/sampling editing in a dedicated `EditSamplingDefaults` method, alias auto-generation on add, and a compact read-only display. Legacy `Models`/alias normalization is missing from `Load()`. Its main defect is scope: besides the expected TODO/CHANGELOG updates, the branch deleted the feat/13 Avalonia scaffolding blocks and the entire Backlog section (feat/03, feat/05, future enhancements, Target Content) from `TODO.md` — unrelated and destructive documentation changes. It also corrupted the flag table by escaping quotes in two default values. Its score is **90/100** with a heavy scope deduction.

**Conclusion:** solid code, but the unrelated `TODO.md` deletions are a process failure that a code review should have caught.

### Model used for PR #47 — 84/100 (`Gemma-4-26B-A4B-it-MXFP4_MOE_noctrex.gguf`)

The model used for PR #47 produced a buildable implementation covering the main feature surface: entities, load-time defaults, host/cache/sampling editing, alias input with auto-generation on add, and read-only server-defaults display. However, it is the weakest of this batch on type safety — `KvUnified`, `ContextShift`, and `ReasoningPreserve` are strings (`"true"`) in addition to string-typed `FlashAttn`/`Fit` — and on UI polish: invalid numeric input is silently ignored with no feedback, and the alias of a legacy model is displayed raw instead of falling back to an auto-generated value. Neither `Models` nor legacy aliases are normalized on load. Most seriously, its `TODO.md` change deletes the entire Backlog section (feat/03 server management, feat/05 results logging, future enhancements, Target Content) and replaces it with a two-line stub — an unrelated and destructive change. Its score is **84/100**.

**Conclusion:** functional core implementation, but weak typing and destructive `TODO.md` changes make it materially below the strong candidates.

### Model used for PR #52 — 95/100 (`openai/gpt-5.6-luna`)

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

### Model used for PR #67 — 92/100 (`Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k]`)

The model used for PR #67 produced a complete, buildable implementation with correctly typed boolean server flags (`FlashAttn`, `KvUnified`, `Fit`, `ContextShift`, `ReasoningPreserve`), host/cache/sampling settings, alias support in add and edit flows, and read-only server-defaults display. Its `EditGeneralSettings` delegates numeric input to reusable `PromptDouble`/`PromptInt` helpers with invariant-culture parsing, which is the cleanest numeric-input handling in the evaluated set. Its compatibility gaps match PRs #63 and #65: `Normalize()` does not initialize a missing `Models` list and does not normalize aliases on legacy model entries, and clearing an existing alias in the edit flow keeps the old value instead of regenerating it from the GGUF filename. The `DraftPMIn` property name is inconsistent with the specification. Its task remains in `In Progress` with sub-items unchecked, so its score is **92/100**.

**Conclusion:** strong candidate on par with PRs #63 and #65; should address the `Models`/alias normalization edge cases.

### Model used for PR #68 — 92/100 (`KAT-Coder-V2.5-Dev_Q3_K_M_imatrix_MTP_(offmonreal)_64k`)

The model used for PR #68 produced a complete implementation covering all specification features: entities, load-time defaults, host/cache/sampling editing, alias auto-generation on both add and edit (empty alias in the edit flow correctly regenerates from the GGUF filename), and a full read-only server-defaults display. It also correctly normalizes legacy model aliases during load. Its weaknesses are type and diff quality: `FlashAttn` and `Fit` are represented as strings (`"on"`/`"off"`) instead of booleans, and `Entities.cs` was rewritten wholesale (BOM/whitespace churn), inflating the diff and adding regression risk. Compatibility of a missing `Models` list is handled only defensively in `AddModel`, not normalized in `Load()`. Its TODO lifecycle is correct (own `In Progress` block with all items checked), so its score is **92/100** with the deduction landing in code quality.

**Conclusion:** functionally the most complete of the KAT runs, but the string-typed flags and whole-file rewrite keep it out of the top tier.

### Model used for PR #87 — 92/100 (`Qwen3.8-27B_UD-IQ4-KS_Unsloth_[32k] Q8/Q8`)

The model used for PR #87 produced a complete, buildable implementation with the best type correctness among the new candidates. All `ServerDefaults` fields use the correct types (`bool` for boolean flags, `int` for numeric fields) and correct property names per the specification (`DraftPMIn`, `UBatchSize`). The `Reasoning` field is correctly typed as `bool` (default `true`). The `ApplyBackwardCompatDefaults` method null-coalesces `SamplingDefaults`, `ServerDefaults`, `Host`, `CacheTypeK/V`, and also handles legacy model aliases (`m.Alias ??= ""`), which is the only evaluated run to do so. The settings UI covers host, cache types, and sampling defaults editing with culture-safe numeric parsing (`CultureInfo.InvariantCulture`, `NumberStyles.Float`). Alias auto-generation works correctly on add, and the edit flow falls back to the GGUF filename when the alias is empty. `ShowCurrentSettings()` displays all new fields including the read-only ServerDefaults section. Build passes with 0 warnings and the baseline 4-pass/9-fail test result. The TODO.md lifecycle is correct (Completed block added). Minor deductions: the alias edit flow keeps the current alias when the user submits empty input rather than regenerating from the GGUF filename (a spec mismatch), `ReasoningBudgetMessage` is not displayed in `ShowCurrentSettings()`, and boolean server flags are displayed as `True`/`False` instead of `on`/`off`.

**Conclusion:** the strongest candidate among the new PRs, with the best type correctness and the only legacy alias normalization.

### Model used for PR #88 — 88/100 (`Qwen3.8-27B-i1-IQ4_XS-GGUF-Smaller_(jrell)_Q8-Q8_[56k]`)

The model used for PR #88 produced a complete, buildable implementation with some type and naming issues. The `Reasoning` field is incorrectly typed as `string` (default `"on"`) instead of `bool`, and two property names are misspelled: `DraftPMIN` (should be `DraftPMin`) and `UbatchSize` (should be `UBatchSize`). The `ApplyBackwardCompatDefaults` method null-coalesces `SamplingDefaults`, `ServerDefaults`, `Host`, and `CacheTypeK/V` but does not handle legacy model aliases or a missing `Models` list. The settings UI covers host, cache types, and sampling defaults editing, but numeric parsing is culture-sensitive (no `CultureInfo.InvariantCulture`), which could cause issues in non-US locales. Alias auto-generation works correctly on add, and the edit flow correctly regenerates from the GGUF filename when the user submits empty input (matching the spec). `ShowCurrentSettings()` displays all new fields including the read-only ServerDefaults section via a dedicated `ShowServerDefaultsReadonly` helper method, and includes the `ReasoningBudgetMessage` display. Boolean server flags are displayed as lowercase `true`/`false` using `.ToString().ToLowerInvariant()`. Build passes with 0 warnings and the baseline 4-pass/9-fail test result. The TODO.md lifecycle keeps the task in `In Progress` with all items checked. Deductions: type and naming errors in `Entities.cs`, culture-sensitive numeric parsing, and missing legacy alias normalization.

**Conclusion:** a solid implementation held back by type correctness issues (`Reasoning` as string, `DraftPMIN`, `UbatchSize`) and culture-sensitive parsing.

### Model used for PR #90 — 88/100 (`Qwen3.8-27B-ZB4.00-MIN-v5-IQ4_XS_tooltd.gguf`)

The model used for PR #90 produced a complete, buildable implementation with the best TODO lifecycle in this batch. All spec fields are present in `Entities.cs` (with the same `Reasoning`-as-string, `DraftPMIN`, and `UbatchSize` type/naming issues as PR #88) and the settings UI covers host, cache types, sampling defaults editing, and alias flows. The `ApplyBackwardCompatDefaults` method null-coalesces `SamplingDefaults`, `ServerDefaults`, `Host`, and `CacheTypeK/V`, and additionally defends against a `null` `Alias` on existing model entries (`m.Alias = ""`). Alias auto-generation works correctly on add, and the edit flow correctly regenerates from the GGUF filename when the user submits empty input (matching the spec). `ShowCurrentSettings()` displays the sampling defaults, the read-only server-defaults section, and per-model alias (showing `(auto: <gguf-name>)` for legacy aliases with empty `Alias`). Build passes with 0 warnings and the baseline 4-pass/9-fail test result. The TODO.md lifecycle is correct: the task is added to the `Completed` section with a short description, no `In Progress` block is left behind. Deductions: same `Reasoning`/`DraftPMIN`/`UbatchSize` type and naming issues as PR #88, culture-sensitive numeric parsing (no `CultureInfo.InvariantCulture`), no `Models ??= []` in `ApplyBackwardCompatDefaults` (handled defensively only in `AddModel`), and no legacy alias auto-generation from the GGUF filename. `ReasoningBudgetMessage` is not displayed in `ShowCurrentSettings()`.

**Conclusion:** a clean, complete implementation that ties PR #88 on the scorecard. The better TODO lifecycle is offset by the same type/naming issues and the missing `ReasoningBudgetMessage` display.

### Model used for PR #96 — 85/100 (`Qwen3.8-27B-UD-Q2_K_XL_(Unsloth)_[128k]`)

The model used for PR #96 produced a buildable implementation with correct type choices (all `ServerDefaults` booleans are `bool`, including `Reasoning`; numerics are `int`/`double`; property names match the spec: `DraftPMin`, `UBatchSize`). The `ApplyDefaults` method null-coalesces `SamplingDefaults`/`ServerDefaults`/empty `Host`/empty `CacheTypeK/V` and defends against a `null` `Alias` on existing model entries (`m.Alias = ""`). The settings UI covers host, cache types, sampling defaults editing (with a dedicated `EditSamplingDefaults` method), and alias flows. `ShowCurrentSettings()` displays the new fields with consistent `on`/`off` rendering for booleans and includes the read-only `ServerDefaults` section (including `ReasoningBudgetMessage`). Build passes with 0 warnings and the baseline 4-pass/9-fail test result. The implementation has a **critical bug in `AddModel`**: the prompt advertises `Alias (empty = use GGUF filename without .gguf)` but the code does `Alias = aliasInput.Trim()`, saving an empty string when the user submits nothing. This violates the spec, which requires auto-generation from the GGUF filename. `EditModel` keeps the current alias when input is empty (also a spec mismatch), although it correctly displays `(auto: <gguf-name>)` for legacy models. The TODO.md lifecycle is destructive: the entire 111-line `feat/12_settings_expansion` task spec (including the flag table and step-by-step instructions) was deleted from the Backlog, and a new `Completed` entry was added — but inserted into the Backlog section rather than the `# Completed` section. CHANGELOG.md is also modified with a new `### Changed` section. Numeric parsing is culture-sensitive (no `CultureInfo.InvariantCulture`). Deductions: critical alias auto-generation bug in `AddModel` (12/15 UI/Beh), destructive `TODO.md` change + CHANGELOG violation (1/5 Scope), culture-sensitive numeric parsing (Code), and missing `Models ??= []` in `Load()` (Compat).

**Conclusion:** correct type choices and the right structural decisions on entities, but the destructive `TODO.md` change, the `AddModel` alias auto-generation bug, and the missing `Models ??= []` keep the score below the existing 89/100+ tier.

### Model used for PR #97 — 93/100 (`Qwen3.8-27B-Q3_K_S_(Unsloth)_[64k]_Q8Q8`)

The model used for PR #97 produced the strongest implementation of this batch. All `ServerDefaults` fields use the correct types (`bool` for `Reasoning`, `KvUnified`, `ContextShift`, `ReasoningPreserve`; `int`/`double` for numerics) and the spec property names (`DraftPMin`, `UBatchSize`). The `ApplyDefaults` method is comprehensive: it null-coalesces `SamplingDefaults`/`ServerDefaults`/empty `Host`/empty `CacheTypeK/V` and defends against `m.Alias == null`. The settings UI is well-organized: `EditGeneralSettings` covers host, cache types, and delegates to a clean `EditSamplingDefaults` helper for the sampling defaults section; `AddModel` and `EditModel` accept the alias (though `AddModel` has the same bug as PR #96 — `Alias = aliasInput.Trim()` saves an empty string instead of auto-generating from the GGUF filename; `EditModel` correctly keeps the current alias when input is empty). `ShowCurrentSettings()` displays the new fields including the read-only `ServerDefaults` section with `ReasoningBudgetMessage`. Build passes with 0 warnings and the baseline 4-pass/9-fail test result. The TODO.md lifecycle is correct: the task spec is preserved on main and only a single `## feat/12_Qwen38-27B-Q3_K_S_1._settings_expansion — Expand settings for llama-server flags ✅` entry is added to the `# Completed` section. CHANGELOG.md is also modified (a new `### Added` entry under `[Unreleased]`) — this is a Scope deduction per the post-merge review convention that says *don't change CHANGELOG*. Deductions: the `AddModel` alias auto-generation bug (UI/Beh), the CHANGELOG change (4/5 Scope), culture-sensitive numeric parsing (no `CultureInfo.InvariantCulture`), and missing `Models ??= []` in `Load()` (Compat).

**Conclusion:** the best of the recent batch — clean entities, comprehensive backward compatibility, and a correct TODO lifecycle. Tied with PR #34 at 93/100. The `AddModel` alias auto-generation bug and the CHANGELOG change are the only material defects.

### Model used for PR #98 — 90/100 (`Qwen3.8-27B-abliterated-UD-IQ4_XS_huihui`)

The model used for PR #98 produced a buildable implementation with the most thorough backward-compatibility handling: the `Load()` method null-coalesces all the new sections/fields, defends against `m.Alias == null`/empty, and additionally **auto-generates aliases from the GGUF filename on load** (`Alias = Path.GetFileNameWithoutExtension(GgufFileName)` for every model with an empty alias), closing the legacy alias gap that affected nearly every other implementation. `AddModel` and `EditModel` both correctly auto-generate the alias when the user submits empty input. The settings UI covers host, cache types, sampling defaults editing, and alias flows. `ShowCurrentSettings()` displays all new fields with a `(auto from filename)` placeholder for legacy models. Build passes with 0 warnings and the baseline 4-pass/9-fail test result. The TODO.md lifecycle is correct: the task spec is preserved on main and a `## feat/12_Qwen38-27B-UD-IQ4_XS_huihui_1_settings_expansion ✅` entry is added to the `# Completed` section. The main weakness is the same set of type issues seen in PRs #88 and #90: `Reasoning` is typed as `string` (default `"on"`) instead of `bool`, `DraftPMIn` is misspelled (should be `DraftPMin`), `UbatchSize` (should be `UBatchSize`), and `FlashAttn`/`Fit` are also strings. `Load()` uses the `with { ... }` clone pattern for each individual default, which is verbose but correct. Numeric parsing is culture-sensitive (no `CultureInfo.InvariantCulture`). CHANGELOG.md is *not* modified (the diff only removes the `### Evaluation` section that was added in PR #92, which is the correct behavior). Deductions: `Reasoning`/`DraftPMIn`/`UbatchSize`/`FlashAttn`/`Fit` type and naming issues (26/30 Spec), the `with { ... }` clone pattern is repetitive (Code), culture-sensitive numeric parsing (Code), and missing `Models ??= []` in `Load()` (Compat) — offset by the bonus for the load-time alias auto-generation (Compat).

**Conclusion:** the most complete backward-compatibility handling in this batch, with the alias auto-generation on load filling a real gap that affected nearly every other implementation. Tied with PR #87 at 90/100. Held back by the same `Reasoning`-as-string, `DraftPMIn` (capital I), `UbatchSize`, `FlashAttn`/`Fit` type and naming issues as PRs #88 and #90.

### Model used for PR #38 — 88/100 (`Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf`)

The model used for PR #38 produced a buildable implementation with the cleanest type choices among the recent batch: correct `DraftPMin` and `UBatchSize` property names (no typos), but `FlashAttn` and `Fit` are typed as `string` (`"on"`/`"off"`) instead of `bool`, and `Reasoning` is also `string` (default `"on"`). The `SettingsManager.Load()` method calls a dedicated `Normalize()` helper that null-coalesces `SamplingDefaults`, `ServerDefaults`, empty `Host`, empty `CacheTypeK/V`, and **`Models ??= []`** (one of the few evaluations to handle a missing `Models` list). The settings UI has a clean `AutoAlias` helper that's used consistently in `AddModel` (correctly auto-generates the alias from the GGUF filename when the user submits empty input), `EditModel` (keeps the current alias when input is empty), and `ShowCurrentSettings()` (displays `(auto: <gguf-name>)` for legacy models). The settings editor covers host, cache types, and sampling defaults. Build passes with 0 warnings and the baseline 4-pass/9-fail test result. Numeric parsing is culture-sensitive (no `CultureInfo.InvariantCulture`). The **TODO.md lifecycle is broken** in two ways: (1) the entire `Evaluation.md` is **deleted** (505 lines removed) — a massive scope violation that destroys the evaluation record; (2) the task is duplicated in both `In Progress` (with all items unchecked) and `Completed`. The PR also re-adds the merged `feat/13_avalonia_ui_scaffolding` entry to `# Completed` (which fixes a pre-existing TODO lifecycle defect since the entry was orphaned in Backlog on main). CHANGELOG.md is not modified. Deductions: string-typed `FlashAttn`/`Fit`/`Reasoning` (27/30 Spec), no `InvariantCulture` (Code), no alias auto-generation on load (Compat), and the Evaluation.md deletion + duplicate In-Progress entry (1/5 Scope).

**Conclusion:** correct entities and clean alias flow, but the Evaluation.md deletion is a process failure that prevents the PR from being merged. Ties PR #41 at 88/100.

### Model used for PR #40 — 87/100 (`Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]`)

The model used for PR #40 produced a buildable implementation with the same destructive `Evaluation.md` deletion as PRs #38, #41, #43, and #49. On the schema side: `FlashAttn` and `Fit` are typed as `string` (should be `bool`), `Reasoning` is `string` (should be `bool`), and the `DraftPMin` property is misspelled as `DraftPMIn` (capital I) — a naming mismatch with the spec. `UBatchSize` is correctly named. `SettingsManager.Load()` handles backward compatibility: null-coalesces `SamplingDefaults`/`ServerDefaults`/empty `Host`/empty `CacheTypeK/V` and **`Models ??= []`**, but does not auto-generate aliases for legacy models on load. The settings UI has a clean `EditSamplingDefaults` helper for the sampling defaults section; `AddModel` correctly auto-generates the alias from the GGUF filename when input is empty; `EditModel` keeps the current alias when input is empty. `ShowCurrentSettings()` displays the `ReasoningBudgetMessage` (with surrounding quotes that match the spec default), uses `.ToLower()` for boolean server flags, and shows the per-model alias. Build passes with 0 warnings and the baseline 4-pass/9-fail test result. Numeric parsing is culture-sensitive (no `CultureInfo.InvariantCulture`). The TODO.md lifecycle is broken: the entire `Evaluation.md` is **deleted** (505 lines) and the task is duplicated in both `In Progress` (unchecked) and `Completed`. CHANGELOG.md is not modified. Deductions: `FlashAttn`/`Fit`/`Reasoning` as strings, `DraftPMIn` typo (26/30 Spec), no `InvariantCulture` (Code), no alias auto-gen on load (Compat), and the Evaluation.md deletion + duplicate In-Progress entry (1/5 Scope).

**Conclusion:** the `DraftPMin` typo is the only thing keeping it below PR #38. Solid implementation, but the Evaluation.md deletion and type issues are process failures.

### Model used for PR #41 — 88/100 (`Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]`)

The model used for PR #41 produced a buildable implementation with the best boolean typing in this batch: `FlashAttn` and `Fit` are correctly typed as `bool` (the only PR in this batch to get this right), but `Reasoning` is still `string` (default `"on"`) instead of `bool`. The `DraftPMin` property is misspelled as `DraftPMIN` (all caps) — a worse typo than PR #40's `DraftPMIn`. `UBatchSize` is correctly named. The `ReasoningBudgetMessage` default embeds escaped quotes, which differs from the spec default but is parseable. `SettingsManager.Load()` calls a dedicated `Normalize()` helper that null-coalesces `SamplingDefaults`/`ServerDefaults`/empty `Host`/empty `CacheTypeK/V` and **`Models ??= []`** — comprehensive backward compatibility. The settings UI has a clean `EditSamplingDefaults` helper, and the alias flows correctly auto-generate the alias from the GGUF filename on empty input. **The edit flow regenerates the alias from the GGUF filename when input is empty** (matches the spec better than PRs #38/#40/#49 which keep the current alias on empty). `ShowCurrentSettings()` displays `ReasoningBudgetMessage` and uses `(d.Fit ? "on" : "off")` ternary for booleans. Build passes with 0 warnings and the baseline 4-pass/9-fail test result. Numeric parsing is culture-sensitive (no `CultureInfo.InvariantCulture`). The TODO.md lifecycle is broken: the entire `Evaluation.md` is **deleted** (505 lines) and the task is duplicated in both `In Progress` and `Completed`. **CHANGELOG.md is also modified** (a new `### Added` entry under `[Unreleased]`). Deductions: `Reasoning` as string, `DraftPMIN` typo (27/30 Spec), no `InvariantCulture` (Code), no alias auto-gen on load (Compat), and the Evaluation.md deletion + duplicate In-Progress entry + CHANGELOG change (0/5 Scope).

**Conclusion:** the best UI/Beh of this batch (15/15) thanks to the correct alias regeneration on empty edit, but tied with PR #38 only because the Scope column is dragged to 0 by the Evaluation.md deletion and the CHANGELOG change. The `DraftPMIN` typo is a worse defect than PR #38's string-typed `Reasoning`.

### Model used for PR #43 — 81/100 (`Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[80k]`)

The model used for PR #43 produced a buildable implementation with the worst UI/Beh in this batch. The schema has 3 type/naming defects: `FlashAttn` is correctly typed as `bool` but `Fit` is `string` (inconsistent), `Reasoning` is `string`, `DraftPMin` is misspelled as `DraftPMIn` (capital I), and `UbatchSize` (should be `UBatchSize`). `SettingsManager.Load()` null-coalesces `SamplingDefaults`/`ServerDefaults`/empty `Host`/empty `CacheTypeK/V` but is **missing `Models ??= []`** — a backward-compatibility gap for old settings files without a `Models` array. The settings UI has a critical `AddModel` alias bug: `aliasInput = Helper.GetInput(...).Trim()` is saved as `Alias = aliasInput` — if the user submits empty input, an empty string is saved, violating the spec's *empty = auto-generate from GGUF filename* requirement (the prompt itself advertises the auto-gen behavior). `EditModel` has a similar issue: it just assigns `modelToEdit.Alias = aliasInput` without checking for empty input. `ShowCurrentSettings()` uses a clean `ModelSettings.GetEffectiveAlias()` helper to display aliases, and uses `CultureInfo.InvariantCulture` for numeric display (5 occurrences) — the only PR in this batch to do so. `ShowCurrentSettings()` also uses `(d.FlashAttn ? "on" : "off")` for booleans, but does **not** display `ReasoningBudgetMessage`. Build passes with 0 warnings and the baseline 4-pass/9-fail test result. The TODO.md lifecycle is broken: the entire `Evaluation.md` is **deleted** (505 lines) and the task is duplicated in both `In Progress` and `Completed`. CHANGELOG.md is not modified. Deductions: 3 type/naming issues (26/30 Spec), missing `Models ??= []` (14/20 Compat), critical `AddModel` alias bug + no `ReasoningBudgetMessage` display (11/15 UI/Beh), and the Evaluation.md deletion (1/5 Scope).

**Conclusion:** the only PR in this batch that uses `CultureInfo.InvariantCulture`, but the `AddModel` alias bug and 3 type issues hold it back. Ties PR #49 at 81/100.

### Model used for PR #49 — 81/100 (`Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]`)

The model used for PR #49 produced a buildable implementation with the same 3 type/naming defects as PR #43: `FlashAttn`/`Fit` are correctly typed as `bool`, but `Reasoning` is `string`, `DraftPMin` is misspelled as `DraftPMIn` (capital I), and `UbatchSize` (should be `UBatchSize`). `SettingsManager.Load()` uses the verbose `with { ... }` clone pattern for each default, but is **missing `Models ??= []`** — a backward-compatibility gap. The settings UI has a critical `AddModel` alias bug: `alias = aliasInput?.Trim() ?? ""` saves an empty string when the user submits empty input (same defect as PR #43). `EditModel` correctly keeps the current alias when input is empty. `ShowCurrentSettings()` displays `ReasoningBudgetMessage` (without extra quotes) and shows `(auto: <gguf-name>)` for legacy models. Build passes with 0 warnings and the baseline 4-pass/9-fail test result. Numeric parsing is culture-sensitive (no `CultureInfo.InvariantCulture`). The TODO.md lifecycle is broken: the entire `Evaluation.md` is **deleted** (505 lines) and the task is duplicated in both `In Progress` and `Completed`. CHANGELOG.md is not modified. Deductions: 3 type/naming issues (26/30 Spec), missing `Models ??= []` (14/20 Compat), critical `AddModel` alias bug (12/15 UI/Beh), the `with { }` clone pattern repetition (8/10 Code), and the Evaluation.md deletion (1/5 Scope).

**Conclusion:** the verbose `with { }` clone pattern in `Load()` is the only Code-quality deduction. The `AddModel` alias bug and 3 type issues hold it back. Ties PR #43 at 81/100.

### Model used for PR #103 — 83/100 (`Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf`, run #2)

The model used for PR #103 produced a buildable implementation with the same 3 type/naming defects as PRs #88, #90, and #98: `FlashAttn` and `Fit` are correctly typed as `bool` (matches spec), but `Reasoning` is `string` (default `"on"`) instead of `bool`, `UbatchSize` (should be `UBatchSize`), and `DraftPMin` is misspelled as `DraftPMIN` (all caps). The `ReasoningBudgetMessage` default is the spec-correct value (with surrounding quotes). `SettingsManager.Load()` calls a dedicated `ApplyBackwardCompatDefaults` helper that null-coalesces `Host`, `CacheTypeK/V`, `SamplingDefaults`, and `ServerDefaults`, but is **missing `Models ??= []`** and does not auto-generate aliases for legacy models on load. The settings UI has a critical `AddModel` alias bug: `Alias = aliasInput` saves an empty string when the user submits empty input (the prompt advertises the auto-gen behavior but the code doesn't implement it — same defect as PRs #43, #49, and the previously-evaluated #96/#97). `EditModel` correctly regenerates the alias from the GGUF filename on empty input. `ShowCurrentSettings()` displays the readonly `ServerDefaults` section with `(d.FlashAttn ? "on" : "off")` and `(d.Fit ? "on" : "off")` ternaries for booleans, and `KvUnified.ToString().ToLowerInvariant()` is used for the boolean-to-string conversion. The `ReasoningBudgetMessage` is displayed. Build passes with 0 warnings and the baseline 4-pass/9-fail test result. Numeric display in `ShowServerDefaultsReadonly` is culture-sensitive (no `CultureInfo.InvariantCulture`). The TODO.md lifecycle is **correct** (only a `## Completed` entry, no `In Progress` duplication) and CHANGELOG.md is not modified. However, **Evaluation.md is regressed** because the branch is based on `ab0ec55` (pre-#99): the diff re-adds the "Not yet evaluated" placeholder section for PRs #38-#49, removes the #38-#49 rows from the results table, resets the count to "Thirty-six", and removes the ranking entries for #38-#49 — effectively rolling back PRs #99 and #100's work without adding any of its own entry. Deductions: 3 type/naming issues (27/30 Spec), missing `Models ??= []` and no load-time alias auto-gen (14/20 Compat), critical `AddModel` alias bug (13/15 UI/Beh), no `InvariantCulture` for numerics (8/10 Code), and the Evaluation.md regression (1/5 Scope).

**Conclusion:** a complete buildable implementation with a critical `AddModel` alias bug and an Evaluation.md regression that rolls back PRs #99/#100. Ties PR #104 at 83/100 (and PRs #103 and #104 are byte-identical in source code — two runs of the same model produced the same implementation).

### Model used for PR #104 — 83/100 (`Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf`, run #3)

The model used for PR #104 produced a buildable implementation that is **byte-identical in source code** to PR #103 (Entities.cs, SettingsManager.cs, and SettingsView.cs all match exactly). Both branches are two runs of `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` and produced the same implementation. The score is identical: **83/100 ★★★★**. See the PR #103 description for full analysis. The only difference between the two branches is the suffix in the branch name (`_2_settings_expansion` vs `_3_settings_expansion`) and the model branch listing in `TODO.md`. Both share the same 3 type/naming defects (`Reasoning` as `string`, `UbatchSize` typo, `DraftPMIN` all-caps), the same `AddModel` alias bug, the same missing `Models ??= []` in `Load()`, and the same Evaluation.md regression that rolls back PRs #99/#100.

**Conclusion:** PR #104 is functionally the same as PR #103 — merging one and not the other is purely an arbitrary choice between two byte-identical runs of the same model. Ties PR #103 at 83/100.

### Model used for PR #105 — 84/100 (`Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]`, run #3)

The model used for PR #105 produced a buildable implementation with the **cleanest boolean typing in the recent batch**: `FlashAttn` and `Fit` are correctly typed as `bool`, and — uniquely among the recent PRs — **`Reasoning` is correctly typed as `bool`** (default `true`), making it the only PR after PR #71 to get all three server-flag booleans right. The `UBatchSize` property name is correctly cased (no typo). The `DraftPMin` property is misspelled as `DraftPMIN` (all caps), and the `ReasoningBudgetMessage` default is the spec-correct value (with surrounding quotes). The schema uses nullable `SamplingDefaults?` and `ServerDefaults?` properties (cleaner than the imperative defaults used in #103/#104), so the deserializer can distinguish between "field absent" and "field present with defaults". `SettingsManager.Load()` has inline backward-compat logic that null-coalesces `Host`, `CacheTypeK/V`, `SamplingDefaults`, `ServerDefaults`, and per-model `Id`/`GgufFileName`/`Alias` — but is **missing `Models ??= []`** and does not auto-generate aliases for legacy models on load. The settings UI has **correct `AddModel` alias auto-generation**: `string alias = string.IsNullOrWhiteSpace(aliasInput) ? Path.GetFileNameWithoutExtension(gguf) : aliasInput.Trim();` — the only PR in the recent batch to get this right. **`AddModel` is also defensive with `settings.Models ??= []`** (a unique defensive pattern not seen in any other recent PR). However, `EditModel` has the critical alias bug: `modelToEdit.Alias = aliasInput.Trim()` saves an empty string when the user submits empty input (the inverse of the #103/#104 bug — which has the bug in `AddModel`). `ShowCurrentSettings()` displays the readonly `ServerDefaults` section with `(d.FlashAttn ? "on" : "off")` and `(d.Reasoning ? "on" : "off")` ternaries for booleans, shows `(auto: <gguf-name>)` for legacy models, but **does NOT display `ReasoningBudgetMessage`** — a spec violation. Build passes with 0 warnings and the baseline 4-pass/9-fail test result. Numeric parsing and display are culture-sensitive (no `CultureInfo.InvariantCulture`). The TODO.md lifecycle is **correct** (only a `## Completed` entry with 6 checked sub-items, no `In Progress` duplication) and CHANGELOG.md is not modified. However, **Evaluation.md is regressed** because the branch is based on `ab0ec55` (pre-#99): the diff re-adds the "Not yet evaluated" placeholder section for PRs #38-#49, removes the #38-#49 rows from the results table, resets the count to "Thirty-six", and removes the ranking entries for #38-#49 — effectively rolling back PRs #99 and #100's work without adding any of its own entry. Deductions: `DraftPMIN` typo (28/30 Spec), missing `Models ??= []` and no load-time alias auto-gen (15/20 Compat), `EditModel` alias bug and missing `ReasoningBudgetMessage` display (12/15 UI/Beh), no `InvariantCulture` (8/10 Code), and the Evaluation.md regression (1/5 Scope).

**Conclusion:** the strongest of the recent batch — the only PR after #71 to get `Reasoning` as `bool` and correct `UBatchSize`, and the only recent PR with correct `AddModel` alias auto-generation. Held back by the `EditModel` alias bug (inverse of the #103/#104 defect), the missing `ReasoningBudgetMessage` display, and the Evaluation.md regression. With those three fixes plus `Models ??= []` in `Load()`, this would tie PR #67 at 92/100.

### Model used for PR #48 — 84/100 (`Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]`)

The model used for PR #48 implemented the main feature and builds cleanly, with the same baseline test result. Its TODO update is valid bookkeeping and is not deducted. It includes the required settings UI and alias flows, and it adds the useful `agent_build/` ignore rule. However, its compatibility normalization does not initialize a missing `Models` list or legacy model aliases. Its boolean-like `ServerDefaults` values (`KvUnified`, `ContextShift`, and `ReasoningPreserve`) are represented as strings, making invalid values possible and weakening type safety. The edit flow does not auto-generate an alias when an existing alias is cleared, and its implementation has less explanatory structure than PRs #34, #45, and #46.

**Conclusion:** functional and buildable, but materially weaker in compatibility and type correctness.

### Model used for PR #44 — 51/100 (`Gemma-4-26B-Q4_0_(Google)_128k`)

The model used for PR #44 produced an implementation that covers much of the requested feature surface and includes settings normalization, UI display/editing, alias support, and read-only defaults. However, it does not compile: `SettingsView.cs` declares `public.const string EditModel`, producing compiler error `CS1519`. Because the application cannot build, no meaningful regression-safety credit is awarded. The diff also removes or compresses substantial existing UI logic, increasing regression risk. Its server-default representation retains string types for boolean-like flags and its normalization does not safely handle a null deserialized `Models` list.

**Conclusion:** not mergeable without correcting the compile error and revalidating the compressed UI changes.

## 4.1 Re-evaluation correction: TODO lifecycle and PR #63

This re-evaluation applies the repository TODO workflow consistently. Updating `TODO.md` is expected for implementation branches and is not itself a defect. A scope deduction is applied only when the submitted branch leaves its own completed task in `In Progress`, leaves implementation checklist items incomplete, or makes unrelated/malformed documentation changes.

| PR | TODO lifecycle finding |
|---:|---|
| #16, #17, #27, #29, #30, #34, #39, #44, #45, #46, #52, #57, #63, #65, #67, #71 | The submitted implementation task/checklist remains in `In Progress` (or, for #71, the branch made no `TODO.md` update at all); the scope deduction reflects that lifecycle defect, not the presence of `TODO.md`. |
| #18, #20, #22, #23, #33, #42, #47, #48, #59, #60, #68 | The submitted implementation task is represented as completed, so no deduction is made for the normal `TODO.md` update. For #42 and #47 the lifecycle itself is fine, but their unrelated/destructive `TODO.md` edits are penalized in the scope column. |

PR #65 is a complete, buildable implementation with correctly typed boolean server flags, host/cache/sampling settings, alias support, UI display/editing, and numeric input validation. Its compatibility gaps are that `Models` and existing aliases are not normalized during load, and clearing an existing alias on a model that already has one leaves the old alias instead of regenerating it from the GGUF filename. Its task remains in `In Progress`, so its score is **92/100**; the `TODO.md` update itself is not penalized.

PR #63 is a complete, buildable implementation with correctly typed boolean server flags, host/cache/sampling settings, alias support, UI display/editing, and numeric input validation. Its compatibility gaps are that `Models` and existing aliases are not normalized during load, and clearing an existing alias leaves the old alias instead of regenerating it from the GGUF filename. Its task remains in `In Progress`, so its revised score is **92/100**; the `TODO.md` update itself is not penalized.

## 5. Deterministic evidence for the re-evaluated implementations

The targets were evaluated from their submitted commits in isolated worktrees, without rebasing. The recorded PR metadata for the most recent batch is:

| PR | Base SHA | Head SHA | Feature checklist (entities / manager / UI / compatibility) |
|---:|---|---|---|
| #27 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `f12a5dc61cd3bdfef7d09170715039b13adaf199` | ✓ / ✓ / ✗ / partial |
| #29 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `634e9964aca39bfecc8da71c6bdd544c34d102c5` | ✓ / ✓ / ✓ / ✓ |
| #30 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `9e3a74c42e517a175008467084821360f720b692` | ✓ / ✓ / ✓ / partial |
| #57 | `208e140d527aa341ebda7e81ac9bc32df547d2e4` | `5deda4e2bd407df677906e8f3f475dff2ad8808f` | ✓ / ✓ / ✓ / ✓ |
| #59 | `208e140d527aa341ebda7e81ac9bc32df547d2e4` | `bf04025fc82a12837fc34039deec6309da4f288b` | ✓ / ✓ / ✓ / ✓ |
| #60 | `7ce179c050f34a8822e86f981bf230d51bf0360a` | `cddf6b4f74add9e161bcaf6a63553a457891e108` | ✓ / ✓ / ✓ / partial |
| #65 | `f4f3ed36c92b610575f929d5d7c118025814a6e4` | `fecdae37aadcb64c2f3d89e743f780aa57c0630f` | ✓ / ✓ / ✓ / partial |
| #33 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `2fd89b6e732b30d7abcf11637f4ac8189f87cf50` | ✓ / ✓ / ✓ / partial |
| #39 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `74de32e23e92f29c89bf5851f7962261a439314c` | ✓ / ✓ / ✓ / partial |
| #42 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `a1ac7cc754dcd5d9fb569cb9182c5f1ffb7460ca` | ✓ / ✓ / ✓ / partial |
| #47 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `3d9bde5813284324ebc521aebf038317789b2e52` | ✓ / ✓ / ✓ / partial |
| #71 | `e111993267d03c43ad21334dad20d089ca13118e` | `500c3dc14016d4f00e2810a9a8e34cb7f0f9ecc0` | ✓ / ✓ / ✓ / ✓ |
| #67 | `85e6dd055d580167c9eaa66643902efb55a600cd` | `0c2748f5f003f8902e755bf72b0e228f717cb244` | ✓ / ✓ / ✓ / partial |
| #68 | `85e6dd055d580167c9eaa66643902efb55a600cd` | `5e765a061d744c651dfee03f630b265d5d85c166` | ✓ / ✓ / ✓ / partial |
| #88 | `9e24df06356a0c455bf5de64d5aa49d3fcbbbe67` | `d7ebd5b3ef0a39158f405080993d293d2e3cda8e` | ✓ / ✓ / ✓ / partial |
| #90 | `9e24df06356a0c455bf5de64d5aa49d3fcbbbe67` | `22a189da5d48f5c25983e928a4376b923e77fb18` | ✓ / ✓ / ✓ / partial |
| #96 | `3e22637a79c5ae42253ac46874f270ab3be322c1` | `6bdf591aac7f838f6514e9eddb7b92cfc0478e1c` | ✓ / ✓ / ✓ / partial |
| #97 | `3e22637a79c5ae42253ac46874f270ab3be322c1` | `ae3586f90556ff65408b336e608cf99bedeead16` | ✓ / ✓ / ✓ / partial |
| #98 | `3e22637a79c5ae42253ac46874f270ab3be322c1` | `b901ee560ec832da930450581e1ac406621372d5` | ✓ / ✓ / ✓ / partial |
| #38 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `d3459736ac8dcbe0854bb5be79301a226ae58eae` | ✓ / ✓ / ✓ / partial |
| #40 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `75989aed11fc30acfa9ba4aa8166d474e80dbe42` | ✓ / ✓ / ✓ / partial |
| #41 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `e415abafdfcd3b95db21d33c88042ad9ba70abd2` | ✓ / ✓ / ✓ / partial |
| #43 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `dd7facf6725a5acbe7b6727d5f728e6b32f8d70e` | ✓ / ✓ / ✓ / partial |
| #49 | `96f1b41b9d1a597404e2c23d69f1c6b4d79b1c3d` | `cef6a3cf93935e47bcbba7866de48199bf9d0158` | ✓ / ✓ / ✓ / partial |
| #103 | `ab0ec553309d18fccb5075faeaa429503ca4eb7e` | `04abb02e4b7505726b3c22bf8ce77296419c7abc` | ✓ / ✓ / ✓ / partial |
| #104 | `ab0ec553309d18fccb5075faeaa429503ca4eb7e` | `a820cd871482541ef0afa73163c2661486574a42` | ✓ / ✓ / ✓ / partial |
| #105 | `ab0ec553309d18fccb5075faeaa429503ca4eb7e` | `6a1dba408f9f5d6d220a9461e265812326eb2390` | ✓ / ✓ / ✓ / partial |

The implementation diffs were limited to the settings entities/manager and, except for #27, `SettingsView.cs`. The `TODO.md` changes were audited against the required branch lifecycle and are not penalized merely for existing. PR #57 additionally changed `Evaluation.md`, which is unrelated to its implementation task and remains a scope-quality deduction. No unresolved review threads were found.

### Fixed verification outcomes

| PRs | Restore/build command | Test command | Result |
|---|---|---|---|
| #27, #29, #30, #33, #39, #42, #47, #57, #59, #60, #65, #67, #68, #71, #88, #90, #96, #97, #98, #38, #40, #41, #43, #49, #103, #104, #105 | `dotnet restore AI.Evaluator.slnx` then `dotnet build src/Evaluator/Evaluator.csproj -o agent_build --no-restore` | `dotnet test tests/TargetCodeTests/TargetCodeTests.csproj --no-restore` | Every build: 0 warnings, 0 errors. Every test run: 4 passed, 9 baseline failures. |

The baseline failures are in pre-existing `TargetCodeTests` coverage (`SumRange`, `SafeProduct`, and `SplitCsv`); no settings-specific tests were supplied by these PRs. UI scenarios were assessed statically from `SettingsView.cs`: #27 fails the required edit/display scenario because it contains no UI implementation; #29, #30, #57, #59, and #60 expose host/cache/sampling/model-alias flows and display read-only server defaults. Compatibility fixtures used: a legacy JSON object with omitted new sections, a legacy model with omitted alias, and explicit `null` sections/lists. All five UI implementations normalize the new sections; #29, #57, and #59 also normalize model aliases or lists, while #30 and #60 leave some null collection/alias edges to callers.

Comparative scores above preserve the existing Process #1 scoring system. The deterministic checklist confirms the same ordering factors: complete feature coverage, buildability, legacy-load behavior, observable UI flows, focused diffs, and correct TODO lifecycle.

| PR | Compatibility/UI/code-quality notes |
|---:|---|
| #27 | Correct entity additions and manager normalization, but no settings UI; comments and naming are serviceable. |
| #29 | Complete CLI UI and strongest normalization among the Q3 pair; minor property-name inconsistencies (`DraftPMIn`, `UbatchSize`) reduce maintainability. |
| #30 | Complete UI and readable structure; boolean/string representation and missing model-list normalization reduce compatibility confidence. |
| #57 | Complete read-only display and editable flows; its extra `Evaluation.md` edit and alias update semantics are scope-quality deductions. |
| #59 | Complete UI, explicit boolean server flags, and broad normalization; inconsistent acronym casing (`DraftPMIN`, `UBatchSize`) is a small quality issue. |
| #60 | Complete, focused UI and clean types; omitted model-list/alias normalization and less defensive legacy handling reduce compatibility confidence. |
| #65 | Complete UI with host/cache/sampling editing and read-only server defaults display; same compatibility gaps as PR #63 — `Models` list and existing aliases not normalized during load; task lifecycle remains in `In Progress`. |
| #67 | Complete UI with correctly typed boolean flags and the cleanest numeric-input handling (`PromptDouble`/`PromptInt` helpers); `Models` list and legacy aliases not normalized during load, clearing an existing alias does not regenerate it, and `DraftPMIn` is a naming mismatch. |
| #68 | Complete UI with alias regeneration on edit and legacy alias normalization; `FlashAttn`/`Fit` are string-typed instead of boolean, missing `Models` normalization in `Load()`, and a whole-file rewrite of `Entities.cs` inflates the diff. |
| #33 | Best `Load()` normalization of this batch (`Models` list initialized); misleading "empty=auto-gen" edit prompt that keeps the old alias, string-typed `FlashAttn`/`Fit`, and a whole-file `Entities.cs` rewrite. |
| #39 | Full spec coverage with alias regeneration on edit and a clean read-only section; missing `Models`/alias normalization, string-typed `FlashAttn`/`Fit`, `DraftPMIN` naming, task left in `In Progress`. |
| #42 | Correctly typed flags and naming; missing legacy normalization, and destructive unrelated `TODO.md` changes (deleted feat/13 blocks and the whole Backlog section) plus quote-escaping corruption of the flag table. |
| #47 | String-typed `KvUnified`/`ContextShift`/`ReasoningPreserve`, silent numeric-input failures, raw legacy alias display, and destructive `TODO.md` change deleting the entire Backlog section. |
| #71 | Best `Normalize()` of the evaluated set (sections, `Models` list, and legacy aliases all normalized on load); whole-file `Entities.cs` rewrite, repetitive sampling prompt blocks, untrimmed alias input, and no `TODO.md` lifecycle update. |
| #88 | Complete UI with host/cache/sampling editing and read-only server defaults display via a dedicated `ShowServerDefaultsReadonly` helper; type and naming errors (`Reasoning` as string, `DraftPMIN`, `UbatchSize`), culture-sensitive numeric parsing, and no `Models ??= []` or legacy alias normalization in `ApplyBackwardCompatDefaults`. |
| #90 | Complete UI with host/cache/sampling editing, alias regeneration on empty edit input, and per-model `(auto: <gguf-name>)` display for legacy aliases; same `Reasoning`/`DraftPMIN`/`UbatchSize` type and naming issues as PR #88, culture-sensitive numeric parsing, no `Models ??= []` normalization, no legacy alias auto-generation, and `ReasoningBudgetMessage` is not displayed in `ShowCurrentSettings()`. The TODO lifecycle is correctly placed in `Completed`. |
| #96 | Correct types throughout (`Reasoning: bool`, `DraftPMin`, `UBatchSize`); full UI with sampling-defaults helper, consistent `on`/`off` rendering, and read-only `ServerDefaults` display; critical bug: `AddModel` does `Alias = aliasInput.Trim()` and saves an empty string when the user submits nothing, violating the spec's *empty = auto-generate from GGUF filename* requirement; destructive `TODO.md` change deleting the entire 111-line `feat/12_settings_expansion` task spec (flag table + step-by-step instructions) and inserting the new `Completed` entry in the Backlog section instead of the `# Completed` section; CHANGELOG.md is also modified with a new `### Changed` entry. |
| #97 | Correct types throughout (`Reasoning: bool`, `DraftPMin`, `UBatchSize`); full UI with sampling-defaults helper, KV-cache display, and read-only `ServerDefaults` display; same `AddModel` alias auto-generation bug as PR #96; correct TODO lifecycle (single `## feat/12_…_settings_expansion ✅` entry in `# Completed`); CHANGELOG.md is modified with a new `### Added` entry. |
| #98 | Most thorough backward-compatibility: `Load()` null-coalesces all sections, defends against `m.Alias == null`, and **auto-generates aliases from GGUF filename on load** for legacy models with empty alias; `AddModel` and `EditModel` correctly auto-generate the alias when input is empty; same `Reasoning`-as-string, `DraftPMIn` (capital I), and `UbatchSize` type/naming issues as PRs #88 and #90; `FlashAttn` and `Fit` are also strings; `Load()` uses the verbose `with { }` clone pattern; CHANGELOG.md is **not** modified (the diff only removes the stale `### Evaluation` section that was added in #92). |
| #38 | Cleanest entities among the recent batch: correct `DraftPMin`/`UBatchSize` names but `FlashAttn`/`Fit`/`Reasoning` as strings; comprehensive `Normalize()` with `Models ??= []`; clean `AutoAlias` helper used in `AddModel`/`EditModel`/`ShowCurrentSettings()`; build clean; **deletes `Evaluation.md` (505 lines) and duplicates the task in both `In Progress` (unchecked) and `Completed`**. |
| #40 | `FlashAttn`/`Fit`/`Reasoning` as strings, `DraftPMin` misspelled as `DraftPMIn` (capital I); `Models ??= []` and section null-coalescing in `Load()`; `ReasoningBudgetMessage` displayed with surrounding quotes; build clean; **deletes `Evaluation.md` (505 lines) and duplicates the task in both `In Progress` and `Completed`**. |
| #41 | Best boolean typing: `FlashAttn`/`Fit` as `bool` (only PR in this batch), but `Reasoning` still `string` and `DraftPMin` misspelled as `DraftPMIN` (all caps); comprehensive `Normalize()` with `Models ??= []`; **edit flow regenerates alias from GGUF filename on empty input** (matches spec better than PRs #38/#40/#49); build clean; **deletes `Evaluation.md` (505 lines), duplicates the task, and modifies CHANGELOG.md**. |
| #43 | Inconsistent typing: `FlashAttn` as `bool` but `Fit` as `string`; `Reasoning` as `string`; `DraftPMin`/`UbatchSize` typos; `Models ??= []` is **missing** in `Load()`; `ShowCurrentSettings()` uses `CultureInfo.InvariantCulture` (5 occurrences) — the only PR in this batch to do so; **`AddModel` alias bug**: `aliasInput.Trim()` saves empty string when input is empty; build clean; **deletes `Evaluation.md` (505 lines) and duplicates the task**. |
| #49 | Correct `FlashAttn`/`Fit` as `bool`; `Reasoning` as `string`; `DraftPMin`/`UbatchSize` typos; `Models ??= []` is **missing** in `Load()` (uses verbose `with { }` clones); `ShowCurrentSettings()` displays `ReasoningBudgetMessage` and `(auto: <gguf-name>)` for legacy models; **`AddModel` alias bug**: `alias = aliasInput?.Trim() ?? ""` saves empty string; build clean; **deletes `Evaluation.md` (505 lines) and duplicates the task**. |
| #103 | `FlashAttn`/`Fit` as `bool` (correct); `Reasoning` as `string`; `UbatchSize`/`DraftPMIN` typos; `ApplyBackwardCompatDefaults` helper null-coalesces sections but **missing `Models ??= []`**; `ShowCurrentSettings()` uses `(d.FlashAttn ? "on" : "off")` ternary and `KvUnified.ToString().ToLowerInvariant()`; `ReasoningBudgetMessage` displayed; **`AddModel` alias bug**: `Alias = aliasInput` saves empty string when input is empty (same defect as #43/#49); `EditModel` correctly regenerates alias; build clean; **regresses `Evaluation.md` by rolling back PRs #99/#100** (re-adds "Not yet evaluated" section, removes #38-#49 rows, resets count to "Thirty-six"). |
| #104 | **Byte-identical to PR #103** in source code (Entities.cs, SettingsManager.cs, SettingsView.cs all match exactly): two runs of `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` produced the same implementation; same `AddModel` alias bug, same `Models ??= []` gap, same Evaluation.md regression. |
| #105 | **Cleanest boolean typing in the recent batch**: `FlashAttn`/`Fit`/`Reasoning` all as `bool` (only PR after #71 to get all three right); `UBatchSize` correct; `DraftPMIN` all-caps typo; nullable `SamplingDefaults?`/`ServerDefaults?` (cleaner pattern); inline backward-compat in `Load()` with per-model null-coalescing; `Models ??= []` **missing** in `Load()` but defensive in `AddModel`; **`AddModel` correctly auto-generates alias** on empty input; `EditModel` alias bug: `modelToEdit.Alias = aliasInput.Trim()` saves empty string; `ShowCurrentSettings()` does **NOT** display `ReasoningBudgetMessage`; build clean; **regresses `Evaluation.md`** (same as #103/#104). |

## Ranking and conclusion

1. **Model used for PR #52 — 95/100** (★★★★★): best overall. Complete data and UI changes, correct types, comprehensive backward compatibility, clean build, no implementation scope creep; its TODO lifecycle remains incomplete.
2. **Model used for PR #18 — 93/100** and **Model used for PR #34 — 94/100** (★★★★, tie): strong complete implementations. PR #18 also includes passing dedicated settings tests; PR #34 should fix the `Models` normalization edge case before merge.
3. **Model used for PR #17 — 93/100**, **Model used for PR #45 — 93/100**, and **Model used for PR #46 — 93/100** (★★★★, tie): excellent complete implementations, narrowly behind the top pair. PR #45 and PR #46 share the same settings implementation; both should address alias and naming details.
4. **Model used for PR #22 — 92/100**, **Model used for PR #23 — 92/100**, **Model used for PR #87 — 92/100**, **Model used for PR #67 — 92/100**, **Model used for PR #68 — 92/100**, **Model used for PR #63 — 92/100**, **Model used for PR #65 — 92/100**, **Model used for PR #30 — 92/100**, **Model used for PR #35 — 92/100** (★★★★, tie): complete implementations across the evaluated batch. PR #87 is the strongest of the most recent additions (best type correctness and the only legacy alias normalization), but should still fix the alias-edit regeneration behavior and display `ReasoningBudgetMessage`.
5. **Model used for PR #88 — 88/100** and **Model used for PR #90 — 88/100** (★★★★, tie): complete, buildable implementations with the same `Reasoning`-as-string, `DraftPMIN`, and `UbatchSize` type and naming issues, and culture-sensitive numeric parsing. PR #88 includes a `ReasoningBudgetMessage` display and a dedicated `ShowServerDefaultsReadonly` helper; PR #90 has a correct TODO lifecycle (task moved to `Completed`). Both should fix the type and naming issues before merge.
6. **Model used for PR #97 — 93/100** (★★★★, tie): the strongest of the most recent batch with the cleanest TODO lifecycle, correct types throughout, and comprehensive backward compatibility. Deductions: `AddModel` alias auto-generation bug, culture-sensitive numeric parsing, and the CHANGELOG change. Ties PR #34 at 93/100.
7. **Model used for PR #98 — 90/100** (★★★★, tie): most thorough backward-compatibility handling (auto-generates aliases on load, filling the legacy alias gap) but the same `Reasoning`-as-string, `DraftPMIn` (capital I), and `UbatchSize` type/naming issues as PRs #88/#90, plus `FlashAttn` and `Fit` are also strings. Ties PR #87 at 90/100.
8. **Model used for PR #96 — 85/100** (★★★): correct types throughout but held back by the `AddModel` alias auto-generation bug, the destructive `TODO.md` change (deletes the entire 111-line `feat/12_settings_expansion` spec from Backlog and inserts the new `Completed` entry in the wrong section), the CHANGELOG.md change, and missing `Models ??= []` in `Load()`. Sits below the 89/100+ tier and is closer to PR #48 (84/100) than to PR #57 (89/100).
9. **Model used for PR #38 — 88/100** and **Model used for PR #41 — 88/100** (★★★★, tie): the strongest of the recent batch of older PRs (which all delete `Evaluation.md`). PR #38 has the cleanest entities and a clean `AutoAlias` helper; PR #41 has the best boolean typing (`FlashAttn`/`Fit` as `bool`) and the only edit flow that correctly regenerates the alias on empty input. Both are tied at 88/100 because the `Evaluation.md` deletion and the duplicate `In Progress` entry in `TODO.md` drag the Scope column down.
10. **Model used for PR #40 — 87/100** (★★★): `FlashAttn`/`Fit`/`Reasoning` as strings and `DraftPMin` typo; otherwise a complete implementation. Held back by the same `Evaluation.md` deletion and the `DraftPMin` typo.
11. **Model used for PR #105 — 84/100** (★★★★, tie): the strongest of the three newest PRs, with the cleanest boolean typing in the recent batch — the only PR after #71 to get all three server-flag booleans right (`FlashAttn`/`Fit`/`Reasoning` all as `bool`) and the only recent PR with correct `UBatchSize` casing. The `AddModel` alias auto-generation is correct (the inverse of the #103/#104 bug). Held back by the `EditModel` alias bug (saves empty string on empty input), the missing `ReasoningBudgetMessage` display, and the Evaluation.md regression. With those three fixes plus `Models ??= []` in `Load()`, this would tie PR #67 at 92/100.
12. **Model used for PR #103 — 83/100** and **Model used for PR #104 — 83/100** (★★★, tie): **byte-identical in source code** — two runs of `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` produced the same implementation. Both have the critical `AddModel` alias bug (saves empty string when input is empty — the prompt advertises the auto-gen behavior but the code doesn't implement it), the missing `Models ??= []` in `Load()`, the `UbatchSize`/`DraftPMIN`/`Reasoning`-as-string defects, and the Evaluation.md regression. Merging one and not the other is purely an arbitrary choice between two byte-identical runs of the same model.
13. **Model used for PR #43 — 81/100** and **Model used for PR #49 — 81/100** (★★★, tie): both have the `AddModel` alias auto-generation bug (saves empty string when input is empty) and the `Evaluation.md` deletion. PR #43 is the only PR in this batch to use `CultureInfo.InvariantCulture` (5 occurrences) but has 3 type/naming defects; PR #49 has the verbose `with { }` clone pattern in `Load()`. Both are below PR #48 (84/100) and PR #20 (73/100).
14. **Model used for PR #48 — 84/100**: buildable but materially weaker in type safety and backward compatibility.
15. **Model used for PR #20 — 73/100**: partial design and UI issues, plus unresolved review comments.
16. **Model used for PR #16 — 56/100**: incomplete because the UI portion is missing.
17. **Model used for PR #44 — 51/100**: currently uncompilable and therefore not mergeable.

Based on the available evidence across all forty-four evaluated models (every known `feat/12_settings_expansion` implementation has been inspected), the model used for PR #52 is the strongest candidate, followed by the models used for PR #18 and PR #34 tied for second. PR #97 is the strongest of the most recent batch, tying PR #34 at 93/100. None of the five newly evaluated PRs (#38, #40, #41, #43, #49) should be merged as-is: they all delete `Evaluation.md` (505 lines) and duplicate the task in `TODO.md` (both `In Progress` and `Completed`), and PRs #43/#49 have the critical `AddModel` alias auto-generation bug. The three newest PRs (#103, #104, #105) are also disqualified: PRs #103/#104 have the `AddModel` alias bug and #105 has the `EditModel` alias bug, and all three regress `Evaluation.md` by rolling back PRs #99/#100 (they branch from `ab0ec55` and re-add the "Not yet evaluated" placeholder section for PRs #38-#49). PR #105 is the closest to the top tier — with four targeted fixes (alias `EditModel` bug, `ReasoningBudgetMessage` display, `Models ??= []` in `Load()`, rebase to add itself to the implementation list), it could tie PR #67 at 92/100.


