# ai.models-evaluator
[![Build](https://github.com/AI-Team-26/ai.models-evaluator/actions/workflows/build.yml/badge.svg)](https://github.com/AI-Team-26/ai.models-evaluator/actions/workflows/build.yml)

A project that permits to test AI models and collect results to evaluate them for coding.

## Strategy
The goal is to evaluate AI models by tasking them with fixing known bugs in source code. We aim to measure three key dimensions:
1. **Success Rate**: Did the model successfully resolve the bugs?
2. **Code Quality**: How clean/efficient is the provided solution?
3. **Time Required**: The latency/time taken for the model to generate the fix.

All evaluation runs will be documented in a `Test.md` file, tracking the Model name, Branch used, and the resulting Evaluation metrics (Success, Time, Quality).

## Project Structure
- **C# solution** with central package management (`Directory.Build.props`, `Directory.Packages.props`)
- **Console app** — contains intentional bugs (seed for AI evaluation)
- **Unit test project** (NUnit) — verifies correct behavior; acts as the oracle
- Bugs are of increasing difficulty to measure model capability tiers
