# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]

### Added
- feat/12 settings expansion (branch `feat/12_Qwen38-27B-Q3_K_S_1._settings_expansion`,
  done by model **Qwen3.8-27B-Q3_K_S_(Unsloth)_[64k]_Q8Q8**): `ApplicationSettings` gained
  editable llama-server flags (`Host`, `CacheTypeK/V`), a `SamplingDefaults` record
  (temperature, top-k, top-p, min-p, repeat-penalty, repeat-last-n) and a read-only
  `ServerDefaults` record (parallel, prio, flash-attn, kv-unified, load-mode, fit,
  cache-reuse, draft-p-min, log-verbosity, samplers, context-shift, reasoning*,
  batch-size, ubatch-size, spec-type). `ModelSettings` gained `Alias` (auto-generated
  from the GGUF filename when empty). Old settings files load with applied defaults
  (backward compatible); the settings TUI displays and edits all new fields.
- Initial C# console app with intentional bugs for AI model evaluation
- Three difficulty-level bugs:
  - **Bug #1 (Easy)**: Off-by-one error in `SumRange` method
  - **Bug #2 (Medium)**: Missing last field in quote-aware CSV parser  
  - **Bug #3 (Hard)**: Integer overflow in `SafeProduct` accumulator
- NUnit test suite serving as oracle for correct behavior
- CI pipeline split into separate Build and Test workflows

### Technical Setup
- Centralized NuGet package management via Directory.Build.props
- Solution structure: src/ for source code, tests/ for unit tests
- .NET 10.0 target framework

### Documentation
- Added `docs/experimental_prs.md` documenting the experimental `feat/12` settings-expansion
  history: the branch was used to test multiple LLM models, producing several parallel PRs
  (#16, #17, #18, #20, #22, #23) that are now superseded by #22 and should be ignored.
