# Experimental `feat/12` Settings-Expansion PRs

## Background

The `feat/12_settings_expansion` task (expanding the settings schema to cover all
llama-server CLI flags) was used as an **experimental testbed** for evaluating
different large language models. Each model produced a separate PR that should be
ignored.

The definitive, reviewed implementation is **[PR #22 — `feat/12h`**](https://github.com/AI-Team-26/ai.models-evaluator/pull/22).

## Redundant / Experimental PRs (Ignore)

| PR | Branch | Model Tested | Status |
|----|--------|-------------|--------|
| #16 | `feat/12c__settings_expansion` | Qwen3-Coder-Next-REAP-40B-A3B | Open — ignore |
| #17 | `feat/12e__settings_expansion` | Qwen3.5-27B-IQ4_XS_unsloth | Open — ignore |
| #18 | `feat/12f__settings_extension` | KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2 | Open — ignore |
| #20 | `feat/12g__settings_extension` | Nemotron-3.5-Lightning | Open — ignore |
| #22 | `feat/12h_settings_expansion` | mindai/macaron-v1-venti | **Open — definitive** |
| #23 | `feat/121_settings_expansion` | KAT-Coder-V2.5-Dev-Cerebellum-14GB-v2 | Open — ignore |

## What to Do

- **Reviewers:** Do **not** review or merge PRs #16, #17, #18, #20, or #23.
  They are experimental artifacts that can be safely closed.
- **PR #22** is the canonical implementation. It contains all the same changes
  (`Entities.cs`, `SettingsManager.cs`, `SettingsView.cs`) and is the PR to
  track for the settings-expansion feature.
- Once PR #22 is merged, all experimental PRs should be closed.
