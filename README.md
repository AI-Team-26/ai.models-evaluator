# ai.models-evaluator
[![Build](https://github.com/AI-Team-26/ai.models-evaluator/actions/workflows/build.yml/badge.svg)](https://github.com/AI-Team-26/ai.models-evaluator/actions/workflows/build.yml)

A project that permits to test AI models and collect results to evaluate them for coding.

## Strategy
The goal is to evaluate AI models by tasking them with fixing known bugs in source code. We aim to measure three key dimensions:
1. **Success Rate**: Did the model successfully resolve the bugs?
2. **Code Quality**: How clean/efficient is the provided solution?
3. **Time Required**: The latency/time taken for the model to generate the fix.

## Evaluation Workflow
The system uses a dedicated **Evaluator Tool** to automate the benchmarking process:
1. **Orchestration**: Starts `llama.server` with model-specific and common parameters.
2. **Isolation**: Creates a dedicated Git branch for each evaluation run (e.g., `eval/<short-name>-test<v1>-<20240514-1530>`).
3. **Execution**: Sends buggy code to the model and applies the suggested fixes to the branch.
4. **Verification**: Runs the test suite (`dotnet test`) to verify the fix.
5. **Analysis**: Logs the model, parameters, branch, and result (Pass/Fail) for later analysis.

## Proposed Project Structure
- `src/TargetCode/` — The software containing intentional bugs (the "test material").
- `src/Evaluator/` — The C# tool that orchestrates the server, git branches, and testing.
- `tests/TargetCodeTests/` — The NUnit test suite that acts as the oracle for correctness.
- `config/` — JSON configurations for model parameters (common vs. specific) and paths.
- `results/` — Logs of all evaluation runs.

## Tech Stack
- **C# / .NET** for the Evaluator and Target Code.
- **NUnit** for verification.
- **llama.cpp (llama.server, OpenAI-compatible API)** as the model provider. Supports tool calling.
- **Git** for versioning and isolation of model attempts.

## Versioning & Tracking
For reproducibility, each run tracks:
- **Evaluator Test Version** (e.g., `v1`, `v2`).
- **Evaluator Git Commit Hash**.
- **`llama.cpp` Release Tag** (e.g., `b1234`).
- **Model Short-Name** (human-readable, e.g., `Qwen3_5-27B_UD_Q4-K-M_Unsloth`).
- **Branch Name** and **Parameters Used**.

**Branch Naming Convention**: `eval/<short-name>-test<test_version>-<YYYYMMDD-hhmm>`
Example: `eval/Qwen3_5-27B_UD_Q4-K-M_Unsloth-testv1-20240514-1530`
