# ai.models-evaluator
A project that has the tools to test AI models and collect results.

## Strategy
The goal is to evaluate AI models by tasking them with fixing known bugs in source code. We aim to measure three key dimensions:
1. **Success Rate**: Did the model successfully resolve the bug?
2. **Code Quality**: How clean/efficient is the provided solution?
3. **Time Required**: The latency/time taken for the model to generate the fix.

All evaluation runs will be documented in a `Test.md` file, tracking the Model name, Branch used, and the resulting Evaluation metrics (Success, Time, Quality).

## Project Structure
- **C# solution** with central package management (`Directory.Build.props`, `Directory.Packages.props`)
- **Console app** — contains intentional bugs (seed for AI evaluation)
- **Unit test project** (NUnit) — verifies correct behavior; acts as the oracle
- Bugs are of increasing difficulty to measure model capability tiers
