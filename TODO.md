## In Progress: Create C# solution with buggy console app and NUnit tests

### Goal
Scaffold a C# project that serves as the "buggy" test subject for AI model evaluation, with 3 bugs of increasing difficulty and NUnit unit tests acting as oracles.

### Context / Mental Picture
- Central package management via `Directory.Build.props` + `Directory.Packages.props`
- Source projects under `src/`, tests under `tests/`
- Console app contains a class/library with intentional bugs; the Program.cs calls them
- Test project verifies correct behavior — if all pass, the bug is fixed

### Steps
- [x] Create solution structure (`AI.Evaluator.slnx`)
- [x] Add `Directory.Build.props` and `Directory.Packages.props` at repo root
- [x] Scaffold `src/AI.Evaluator.Console` project (C#, net10.0)
- [x] Scaffold `tests/AI.Evaluator.Tests` project (NUnit, dotnet test compatible)
- [x] Verify: `dotnet build && dotnet test` compile and run (pipeline green, 0 tests until bugs+cases added)
- [x] Implement Bug #1 (Easy): off-by-one in SumRange (`i < end` vs `i <= end`) ✓
- [ ] Implement Bug #2 (Medium): e.g., incorrect string parsing/formatting logic
- [ ] Implement Bug #3 (Hard): e.g., subtle concurrency/state issue or algorithmic flaw
- [ ] Write NUnit tests that fail against current buggy code but define expected behavior
- [ ] Ensure tests fail initially when implemented
- [x] Add CI workflow `.github/workflows/ci.yml` ([PR #3](https://github.com/AI-Team-26/ai.models-evaluator/pull/3))
- [ ] Cleanup: remove redundant TargetFramework references from individual projects; remove Unquote package

### Notes
- Bugs should be self-contained methods so AI models can isolate and fix them cleanly.
- Each bug gets its own test class or at least clearly named test cases.
- Keep the project minimal; the focus is on reproducible bugs + clear expected output.
