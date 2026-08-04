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
