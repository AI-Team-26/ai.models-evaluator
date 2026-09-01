# Changelog

All notable changes to this project will be documented in this file.

## [Unreleased]

### Added
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

### Evaluation
- Evaluated PR #87 (`Qwen3.8-27B_UD-IQ4-KS_Unsloth_[32k] Q8/Q8 (mtp:2/7)`) — **92/100** ★★★★.
- Evaluated PR #88 (`Qwen3.8-27B-i1-IQ4_XS-GGUF-Smaller_(jrell)_Q8-Q8_[56k] (mtp:2/7)`) — **88/100** ★★★★.
- Evaluated PR #90 (`Qwen3.8-27B-ZB4.00-MIN-v5-IQ4_XS_tooltd.gguf`) — **88/100** ★★★★.
- Confirmed PR #89 (`doc/eval_prs_87_88`) is a documentation/evaluation PR and is excluded from implementation scoring.
