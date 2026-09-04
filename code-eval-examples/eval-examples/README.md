# LLM code-quality eval

- dotnet-pagination
- react-search

Two self-contained tasks you can drop into your existing branch/PR pipeline.
Each folder has:

- `TASK.md` — the prompt/issue text you give the model.
- The skeleton source file(s) with `TODO` markers — give these to the model as the starting point of its branch.
- A `HIDDEN_*` test file — **never give this to the model.** Run it yourself after the model's PR is in, and just count pass/fail.

## Why hidden tests instead of (or alongside) your judge agent

A judge agent scores what it's shown, and free-form "rate this PR" prompts tend to reward clean formatting and verbosity.  
A hidden test suite you wrote *before* seeing any model's output gives you a number that's the same yardstick for every model,  
and it specifically targets the edge cases you want (no comments, modern code) rather than code style.

## How to run

1. Copy the skeleton files (not the `HIDDEN_*` file) into a fresh branch, named after the model under test.
2. Give the model `TASK.md` as the issue/prompt, same as your usual workflow.
3. Once it opens its PR, checkout the branch and drop the `HIDDEN_*` test  file into the test project.
4. .NET: `dotnet test` — React: `npx vitest run`
5. Score = tests passed / total tests. Keep the raw pass/fail matrix per model, not just the aggregate — which specific edge case a model missed
   is more informative than the percentage.

## Tips

- Run each task 2–3 times per model (fresh branch each time) at your usual sampling settings. 
  A model that finds the race condition once and misses it twice tells you something an average hides.
- Keep the task prompts under-specified on purpose (e.g. don't mention "AbortController" or "ceiling division" explicitly), 
  whether the model reaches for the right tool unprompted is the thing you're measuring.
- These are templates: adapt namespaces/entity names to your real solution rather than merging them in verbatim.
- Consider adding your own hidden tasks over time from real bugs you've hit in this codebase — those are the highest-signal edge cases you can test.
