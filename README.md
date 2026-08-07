# ai.models-evaluator
[![Build](https://github.com/AI-Team-26/ai.models-evaluator/actions/workflows/build.yml/badge.svg)](https://github.com/AI-Team-26/ai.models-evaluator/actions/workflows/build.yml)

An automated system to benchmark AI language models on their ability to identify and fix bugs in source code.

## 🏗️ High-Level Architecture

| Component | Purpose |
|-----------|----------|
| **📁 `src/TargetCode/`** | Contains **intentional bug samples** (the "exam questions" given to AI models). These are buggy functions with failing tests. |
| **🔧 `src/Evaluator/`** | The **orchestration tool** being built. Starts llama-server, sends bugs to LLM via API, applies fixes, runs tests, logs results. |
| **✅ `tests/TargetCodeTests/`** | NUnit test suite acting as the **oracle** – verifies if proposed fixes actually resolve the bugs. |

```
┌─────────────┐     ┌──────────────┐     ┌─────────────┐
│ Evaluator   │────▶│ Llama Server │◀────│ TargetCode  │
│ (our tool)  │     │ + AI Model   │     │ (bugs to    │
│             │◀────│              │     │  fix)       │
└─────────────┘     └──────────────┘     └─────────────┘
```

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

## Current Status

### ✅ Phase 1 Complete: Repository Reorganization
The repository has been reorganized according to the planned structure:
- [`src/TargetCode/`](./src/TargetCode/) contains sample buggy implementations (`MathUtils.cs`) with known defects.
- [`tests/TargetCodeTests/`](./tests/TargetCodeTests/) validates those bugs exist (currently 9 failing tests).
- Infrastructure folders created: [`config/`](./config/), [`results/`](./results/).
- [`src/Evaluator/`](./src/Evaluator/) placeholder ready for implementation.

### 🔜 Next Steps
Phase 2 will implement configuration management and model metadata tracking.
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
