# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]

### Added
- Settings expansion (`feat/12`): app-level editable `Host`, `CacheTypeK/V`, `SamplingDefaults`; read-only `ServerDefaults` section in the Settings view including ngram speculation settings; per-model `Alias` with GGUF-filename auto-generation; backward-compatible loading of old settings files (done by model `Qwen3.8-27B-Abliterated-IQ4-MIX-MTP_finex666_[64k]`)
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
