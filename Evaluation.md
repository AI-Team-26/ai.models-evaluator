# Evaluation of Experimental Settings PRs

This document compares the six open experimental PRs that implement the `feat/12_settings_expansion` task.

## 1. All `feat/12` PRs and executor models

The following list contains all PRs in this repository that implement the `feat/12_settings_expansion` task. Documentation, planning, Avalonia, and unrelated bug-fix PRs are excluded.

| PR | Status | Title | Executor model |
|---:|---|---|---|
| [#16](https://github.com/AI-Team-26/ai.models-evaluator/pull/16) | Open | feat/12c: settings expansion | `Qwen3-Coder-Next-REAP-40B-A3B.i1-IQ3_M_mradermacher.gguf` |
| [#17](https://github.com/AI-Team-26/ai.models-evaluator/pull/17) | Open | feat/12e: Settings expansion | `Qwen3.5-27B-IQ4_XS_unsloth.gguf` |
| [#18](https://github.com/AI-Team-26/ai.models-evaluator/pull/18) | Open | feat/12f: Expand settings schema | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket.gguf` |
| [#20](https://github.com/AI-Team-26/ai.models-evaluator/pull/20) | Open | feat/12g: Expand settings schema for Nemotron-3.5-Lightning | `Nemotron-3.5-Lightning` |
| [#22](https://github.com/AI-Team-26/ai.models-evaluator/pull/22) | Open | feat/12h: Expand settings schema | `mindai/macaron-v1-venti` |
| [#23](https://github.com/AI-Team-26/ai.models-evaluator/pull/23) | Open | feat/121: Expand settings schema | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket.gguf` |
| [#27](https://github.com/AI-Team-26/ai.models-evaluator/pull/27) | Open | feat/12L: Expand settings schema | `Qwen3.5-27B-IQ3_M_(gammaception)_128k` |
| [#29](https://github.com/AI-Team-26/ai.models-evaluator/pull/29) | Open | feat/12M: Expand settings schema | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[64k]` |
| [#30](https://github.com/AI-Team-26/ai.models-evaluator/pull/30) | Open | feat/12N: Expand settings schema | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[64k]` |
| [#31](https://github.com/AI-Team-26/ai.models-evaluator/pull/31) | Open | feat/12O: Expand settings schema | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k] (Reasoning: medium)` |
| [#32](https://github.com/AI-Team-26/ai.models-evaluator/pull/32) | Open | feat/12J: Expand settings schema | `Qwen3.8-27B_UD-Q3-K-XL_Unsloth_[80k] (Reasoning: medium)` |
| [#33](https://github.com/AI-Team-26/ai.models-evaluator/pull/33) | Open | feat/12K: Expand settings schema | `Qwen3.8-27B-Uncensored-Aggressive-IQ3_M_(HauhauCS)_[128k] (Reasoning: medium)` |
| [#34](https://github.com/AI-Team-26/ai.models-evaluator/pull/34) | Open | feat/12P: Expand settings schema | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` |
| [#35](https://github.com/AI-Team-26/ai.models-evaluator/pull/35) | Open | feat/12Q: Expand settings schema | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` |
| [#36](https://github.com/AI-Team-26/ai.models-evaluator/pull/36) | Open | feat/12Q2: Expand settings schema | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` |
| [#37](https://github.com/AI-Team-26/ai.models-evaluator/pull/37) | Open | feat/12Q3: Expand settings schema | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` |
| [#38](https://github.com/AI-Team-26/ai.models-evaluator/pull/38) | Open | feat/12Q4: Expand settings schema | `Qwen3.8-27B-UD-IQ4_XS_unsloth.gguf` |
| [#39](https://github.com/AI-Team-26/ai.models-evaluator/pull/39) | Open | feat/12S: Expand settings schema | `Qwen3.8-27B-Cold-Fusion-GAIN-V1.1-MTP-IQ3_M_(davidau)_64k` |
| [#40](https://github.com/AI-Team-26/ai.models-evaluator/pull/40) | Open | feat/12 T1: settings expansion | `(Llama.cpp) Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` |
| [#41](https://github.com/AI-Team-26/ai.models-evaluator/pull/41) | Open | feat/12T3: Expand settings schema | `(Llama.cpp) Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` |
| [#42](https://github.com/AI-Team-26/ai.models-evaluator/pull/42) | Open | feat/12: Expand settings schema | `Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[80k]` |
| [#43](https://github.com/AI-Team-26/ai.models-evaluator/pull/43) | Open | feat/12/U4: Expand settings schema | `Qwen3.8_27B_UD-IQ4_XS_(Unsloth)_[80k]` |
| [#44](https://github.com/AI-Team-26/ai.models-evaluator/pull/44) | Open | feat/12: Expand settings schema | `Gemma-4-26B-Q4_0_(Google)_128k` |
| [#45](https://github.com/AI-Team-26/ai.models-evaluator/pull/45) | Open | feat/12/KAT-a: Expand settings schema | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket_160k` |
| [#46](https://github.com/AI-Team-26/ai.models-evaluator/pull/46) | Open | feat/12/KAT-b: Expand settings schema | `KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2_deucebucket_160k` |
| [#47](https://github.com/AI-Team-26/ai.models-evaluator/pull/47) | Open | feat/12: Expand settings schema | `Gemma-4-26B-A4B-it-MXFP4_MOE_noctrex.gguf` |
| [#48](https://github.com/AI-Team-26/ai.models-evaluator/pull/48) | Open | feat/12: Expand settings schema | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` |
| [#49](https://github.com/AI-Team-26/ai.models-evaluator/pull/49) | Open | feat/12/b: Expand settings schema | `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]` |

PR #28 documents the experimental history but does not implement `feat/12`, so it is intentionally excluded. PR #25 is similarly documentation-only. PR #19 implements Avalonia UI scaffolding and is also excluded.

The executor model names above are taken from each PR description. For PR #20, the model is identified by the PR title/description as `Nemotron-3.5-Lightning` rather than by an explicit `Implemented by` line.

## 2. Evaluation rules

Each PR is evaluated against the `feat/12_settings_expansion` specification in `TODO.md` and against the existing repository baseline.

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

Each PR was inspected in an isolated worktree and evaluated using:

1. A diff review against `main`.
2. `dotnet build AI.Evaluator.slnx` with isolated output.
3. `dotnet test AI.Evaluator.slnx`.
4. Inspection of entity definitions and `SettingsManager.Load()` normalization.
5. Inspection of `SettingsView` add/edit/display flows.
6. Review-thread status from GitHub.

The repository's existing `TargetCodeTests` contain intentional failing tests. The relevant comparison is whether a PR introduces new failures. All six PRs produced the same existing result: 4 passing and 9 failing target-code tests. PR #18 additionally introduced three settings tests, all of which passed.

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

## 3. Evaluation results

| PR | Specification | Build / regression | Compatibility | UI / behavior | Code quality | Scope | **Total** |
|---:|---:|---:|---:|---:|---:|---:|---:|
| [#16](https://github.com/AI-Team-26/ai.models-evaluator/pull/16) | 12/30 | 20/20 | 13/20 | 2/15 | 5/10 | 4/5 | **56/100** |
| [#17](https://github.com/AI-Team-26/ai.models-evaluator/pull/17) | 29/30 | 20/20 | 17/20 | 14/15 | 9/10 | 4/5 | **93/100** |
| [#18](https://github.com/AI-Team-26/ai.models-evaluator/pull/18) | 29/30 | 20/20 | 18/20 | 14/15 | 9/10 | 4/5 | **94/100** |
| [#20](https://github.com/AI-Team-26/ai.models-evaluator/pull/20) | 22/30 | 20/20 | 15/20 | 8/15 | 5/10 | 3/5 | **73/100** |
| [#22](https://github.com/AI-Team-26/ai.models-evaluator/pull/22) | 29/30 | 20/20 | 17/20 | 14/15 | 8/10 | 4/5 | **92/100** |
| [#23](https://github.com/AI-Team-26/ai.models-evaluator/pull/23) | 29/30 | 20/20 | 17/20 | 14/15 | 8/10 | 4/5 | **92/100** |

### PR #16 — 56/100

PR #16 is a partial implementation. It adds the expanded entities and some load-time defaults, but does not update `SettingsView`. Consequently, the new settings are not editable or fully displayed through the UI, and aliases are not supported in the add/edit flows. It also uses nullable strings with non-null defaults, inconsistent JSON attributes, string types for boolean-like server flags, and does not handle a missing `Models` list. It has unresolved review threads.

**Conclusion:** incomplete and not recommended without further work.

### PR #17 — 93/100

PR #17 is a strong, nearly complete implementation. It provides the expanded data model, complete settings UI coverage, alias editing and generation, read-only server-default display, and backward-compatible handling of missing settings sections and model lists. Review corrections improved its markup output, boolean types, neutral reasoning-message default, and TODO accuracy. Its main limitation is the lack of dedicated automated settings tests.

**Conclusion:** excellent candidate.

### PR #18 — 94/100

PR #18 is the strongest implementation in this comparison. It combines complete data and UI changes with a dedicated `EvaluatorSettingsTests` project. The three added settings tests passed, and review corrections ensured the test project is included in the solution and exercises `SettingsManager` rather than merely duplicating its logic. The test setup uses reflection/path overriding because of the static manager design, which adds some complexity, but it provides the best verification evidence among the six PRs.

**Conclusion:** best overall candidate.

### PR #20 — 73/100

PR #20 is functional but has several design and completeness problems. `ServerDefaults` duplicates editable application fields, creating multiple possible sources of truth. The general settings flow does not expose all required host/cache editing, and the model edit flow lacks alias editing. It also removes the llama.cpp path from the settings display, contains a load-error typo, produces nullable-analysis warnings, and has unresolved review threads.

**Conclusion:** usable as an experiment, but weaker than the complete implementations.

### PR #22 — 92/100

PR #22 is a complete and well-reviewed implementation. It covers the required entities, backward compatibility, settings UI, aliases, and read-only server defaults. It has no unresolved review threads and builds successfully. Its limitations are the lack of dedicated settings tests, nullable nested records normalized later by the manager, and an opinionated hardcoded reasoning-budget message. The PR's status as the definitive implementation is not independently confirmed by this comparison.

**Conclusion:** strong candidate, but not uniquely superior.

### PR #23 — 92/100

PR #23 is another complete implementation with a focused diff. It covers the data model, compatibility handling, UI flows, alias generation, and read-only display. It uses a neutral empty reasoning-budget message and has no unresolved review threads. It lacks dedicated settings tests and does not normalize null aliases inside existing model entries.

**Conclusion:** strong candidate, effectively tied with PR #22.

## Ranking and conclusion

1. **PR #18 — 94/100**: best overall because it is complete and includes passing dedicated settings tests.
2. **PR #17 — 93/100**: excellent complete implementation, narrowly behind PR #18.
3. **PR #22 — 92/100**: complete and well-reviewed, but not proven to be the best solely because it was selected as definitive.
4. **PR #23 — 92/100**: comparable to PR #22 and arguably cleaner in some respects.
5. **PR #20 — 73/100**: partial design and UI issues, plus unresolved review comments.
6. **PR #16 — 56/100**: incomplete because the UI portion is missing.

The evaluation does not support choosing PR #22 merely because it was the last or historically designated implementation. Based on the available evidence, PR #18 is the best candidate among these six.
