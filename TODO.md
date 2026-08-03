## In Progress: Create C# solution with buggy console app and NUnit tests

### Goal
Scaffold a C# project that serves as the "buggy" test subject for AI model evaluation, with 3 bugs of increasing difficulty and NUnit unit tests acting as oracles.

### Context / Mental Picture
- Central package management via `Directory.Build.props` + `Directory.Packages.props`
- Source projects under `src/`, tests under `tests/`
- Console app contains a class/library with intentional bugs; the Program.cs calls them
- Test project verifies correct behavior — if all pass, the bug is fixed

### Steps
- [ ] Create solution structure (`ai.models-evaluator.sln`)
- [ ] Add `Directory.Build.props` and `Directory.Packages.props` at repo root
- [ ] Scaffold `src/AI.Evaluator.Console` project (C#, net8.0)
- [ ] Scaffold `tests/AI.Evaluator.Tests` project (NUnit, dotnet test compatible)
- [ ] Implement Bug #1 (Easy): e.g., off-by-one error in a list/array operation
- [ ] Implement Bug #2 (Medium): e.g., incorrect string parsing/formatting logic
- [ ] Implement Bug #3 (Hard): e.g., subtle concurrency/state issue or algorithmic flaw
- [ ] Write NUnit tests that fail against current buggy code but define expected behavior
- [ ] Verify: `dotnet build` and `dotnet test` compile and run (tests should fail initially)
- [ ] Add CI workflow `.github/workflows/ci.yml`

### Notes
- Bugs should be self-contained methods so AI models can isolate and fix them cleanly.
- Each bug gets its own test class or at least clearly named test cases.
- Keep the project minimal; the focus is on reproducible bugs + clear expected output.
