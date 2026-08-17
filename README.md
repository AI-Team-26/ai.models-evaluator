# ai.models-evaluator
[![Build](https://github.com/AI-Team-26/ai.models-evaluator/actions/workflows/build.yml/badge.svg)](https://github.com/AI-Team-26/ai.models-evaluator/actions/workflows/build.yml)

An automated system to benchmark AI language models on their ability to identify and fix bugs in source code.

## 🎯 Purpose

This project contains **intentional bugs** in the `src/TargetCode/` directory. The bugs are deliberately written to test AI models' ability to:
1. Identify the root cause of failures
2. Propose correct fixes
3. Maintain code quality

The test suite in `tests/TargetCodeTests/` contains the expected correct behavior. When an AI model proposes a fix, these tests verify if the fix actually resolves the bugs.

**Important:** Do not "fix" these bugs — they are intentional test cases for AI evaluation.

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

## Evaluation Process Definition

The evaluator follows a defined process to benchmark AI models:

### 1. Configuration Setup
- Stores user settings in `~/.LlmEvaluator/Settings.json`
- First-time users are guided through an **interactive terminal UI** (Spectre.Console-based menu system)
- No manual JSON editing required — all configuration done via built-in wizard

### 2. Branch Creation
- Creates a dedicated Git branch for each evaluation run
- Branch naming convention: `eval/<model-short-name>-test<version>-<YYYYMMDD-hhmm>`
- Example: `eval/Qwen3_5-27B_UD_Q4-K-M_Unsloth-testv1-20240514-1530`

### 3. Bug Delivery
- Sends buggy code from `src/TargetCode/` to the model via the llama-server OpenAI-compatible endpoint
- The model is asked to create a fix branch and correct the bugs

### 4. Fix Application
- Applies the model's suggested code changes to the evaluation branch
- Preserves the original buggy code for comparison

### 5. Verification
- Runs `dotnet test` on the evaluation branch
- Compares test results against the oracle test suite in `tests/TargetCodeTests/`

### 6. Result Logging
- Records evaluation outcome in `results/evaluation_<timestamp>.json`
- Tracks: model_id, test_case_name, pass/fail, duration_ms, timestamp, git_commit_hash
- Includes runtime metadata: evaluator_version, llama.cpp_tag, parameters_used

## Getting Started

### Prerequisites
1. **llama.cpp installed locally**
   - Download pre-built binaries or compile from source
   - You need access to the `llama-server` executable
   - This tool does NOT install llama.cpp; you must have it already available

2. **.NET SDK 10+** 
   - Required to build and run the evaluator

3. **GGUF Model Files**
   - Place your quantized models (.gguf files) in a folder of your choice
   - Examples: Phi-4, Llama-3.2, Mistral, etc.

### Initial Setup Wizard

On first run, if no configuration exists, the app launches an interactive setup:

```bash
dotnet run --project src/Evaluator
```

The wizard guides you through:
1. **Set llama.cpp Path:** Browse to where `llama-server.exe` (or `llama-server`) is located
2. **Set Models Folder:** Specify where your `.gguf` model files are stored
3. **Configure Server Port:** Choose which port the inference server should use (default: 8080)
4. **Add Your First Model:** Enter model details:
   - Model ID (internal name, e.g., "phi-4")
   - GGUF filename (e.g., "phi-4-Q4_K_M.gguf")
   - Context size (e.g., 4096, 8192)
   - GPU layers (-1 for auto-detect, or specify count)
   - CPU MoE flag (for hybrid architectures)
   - Jinja template support (true/false)

You can always re-run the setup later by selecting **"Edit Settings"** from the main menu.

### Example Settings Structure

Your configuration lives in `~/.LlmEvaluator/Settings.json`. Here's what a typical config looks like:

```json
{
  "llamaCppPath": "/opt/llama.cpp/build/bin",
  "serverPort": 8080,
  "modelsFolderPath": "/home/user/models",
  "models": [
    {
      "id": "phi-4",
      "ggufFileName": "phi-4-Q4_K_M.gguf",
      "contextSize": 4096,
      "gpuLayers": -1,
      "cpuMoE": false,
      "jinja": true
    }
  ]
}
```

No manual editing needed — use the built-in TUI to manage everything!

---

## Development Status

### ✅ Completed Features
- Project scaffolding and structure organization
- Interactive settings editor (add/edit/remove models via TUI)
- Centralized configuration management (`SettingsManager` singleton)
- Validation and UX improvements for incomplete configs

### 🚧 In Progress / Planned
- Real `llama-server` process control (start/stop, health checks, log capture)
- Results logging format design
- Full evaluation pipeline implementation

See [TODO.md](./TODO.md) for detailed backlog.



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
