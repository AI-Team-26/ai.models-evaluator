using Evaluator.UI;
using Spectre.Console;

namespace Evaluator.Settings;

public sealed class SettingsView() : View("Settings")
{
    public void Run()
    {
        Clear();

        if (SettingsManager.HasSettings)
        {
            //ShowCurrentSettings();
            ShowMenu();
        }
        else
        {
            AnsiConsole.MarkupLine("Settings are not set. Set it now.");
            EditGeneralSettings();
        }
    }


    record struct Menu
    {
        public const string EditSettings   = "Edit settings";
        public const string AddModel       = "Add model";
        public const string EditModel      = "Edit model";
        public const string RemoveModel    = "Remove model";
        public const string BackToMain     = "Back to main";
    }

    private void ShowMenu()
    {
        while (true)
        {
            Clear();
            AnsiConsole.MarkupLine($"[gray]Settings are stored in \"{SettingsManager.SettingsFilePath}\".[/]\n");

            ShowCurrentSettings();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(Menu.EditSettings, Menu.AddModel, Menu.EditModel, Menu.RemoveModel, Menu.BackToMain));

            switch (choice)
            {
                case Menu.EditSettings:
                    EditGeneralSettings();
                    break;
                case Menu.AddModel:
                    AddModel();
                    break;
                case Menu.EditModel:
                    EditModel();
                    break;
                case Menu.RemoveModel:
                    RemoveModel();
                    break;
                case Menu.BackToMain:
                    return;
            }
        }
    }

    private static void ShowCurrentSettings()
    {
        ApplicationSettings settings = SettingsManager.GetSettings(forceReload: true);

        AnsiConsole.MarkupLine("[bold cyan]╔══ Current Settings ════════════════════════════════════╗[/]\n");

        var llamaPath = string.IsNullOrEmpty(settings.LlamaCppPath) ? "(empty)" : settings.LlamaCppPath;
        AnsiConsole.MarkupLine($"[cyan]║ llama.cpp folder:[/] {llamaPath}");
        AnsiConsole.MarkupLine($"[cyan]║ Host:[/] {settings.Host}   [cyan]Server Port:[/] {settings.ServerPort}");
        var modelFolder = string.IsNullOrEmpty(settings.ModelsFolderPath) ? "(empty)" : settings.ModelsFolderPath;
        AnsiConsole.MarkupLine($"[cyan]║ Models Folder:[/] {modelFolder}");
        AnsiConsole.MarkupLine($"[cyan]║ Cache Type K/V:[/] {settings.CacheTypeK} / {settings.CacheTypeV}");

        // Sampling defaults (app-level editable)
        var smp = settings.SamplingDefaults;
        AnsiConsole.MarkupLine("\n[cyan bold]║ Sampling Defaults:[/]");
        AnsiConsole.MarkupLine($"[cyan]║   temperature:[/] {smp.Temperature}   [cyan]top-k:[/] {smp.TopK}   [cyan]top-p:[/] {smp.TopP}");
        AnsiConsole.MarkupLine($"[cyan]║   min-p:[/] {smp.MinP}   [cyan]repeat-penalty:[/] {smp.RepeatPenalty}   [cyan]repeat-last-n:[/] {smp.RepeatLastN}");

        // Read-only server defaults
        var srv = settings.ServerDefaults;
        AnsiConsole.MarkupLine("\n[cyan dim]║ Server Defaults (read-only):[/]");
        AnsiConsole.MarkupLine($"[dim]║   parallel={srv.Parallel}  prio={srv.Prio}  flash-attn={(srv.FlashAttn ? "on" : "off")}  kv-unified={srv.KvUnified.ToString().ToLowerInvariant()}[/]");
        AnsiConsole.MarkupLine($"[dim]║   load-mode={srv.LoadMode}  fit={(srv.Fit ? "on" : "off")}  cache-reuse={srv.CacheReuse}  draft-p-min={srv.DraftPMIn}[/]");
        AnsiConsole.MarkupLine($"[dim]║   log-verbosity={srv.LogVerbosity}  context-shift={srv.ContextShift.ToString().ToLowerInvariant()}  reasoning-preserve={srv.ReasoningPreserve.ToString().ToLowerInvariant()}[/]");
        AnsiConsole.MarkupLine($"[dim]║   samplers={srv.Samplers}[/]");
        AnsiConsole.MarkupLine($"[dim]║   reasoning={srv.Reasoning}  reasoning-budget={srv.ReasoningBudget}  batch-size={srv.BatchSize}  ubatch-size={srv.UbatchSize}  spec-type={srv.SpecType}[/]");
        AnsiConsole.MarkupLine($"[dim]║   reasoning-budget-message=\"{srv.ReasoningBudgetMessage}\"[/]");

        if (settings.Models.Count > 0)
        {
            AnsiConsole.MarkupLine("\n[cyan]║ Models:[/]");
            for (int i = 0; i < settings.Models.Count; i++)
            {
                var m = settings.Models[i];
                var alias = string.IsNullOrEmpty(m.Alias) ? "(auto)" : m.Alias;
                AnsiConsole.MarkupLine($"[cyan]║ #{i + 1}[/] {m.Id}: {m.GgufFileName}");
                AnsiConsole.MarkupLine($"[dim]║       alias={alias}  ctx={m.ContextSize}  gpu-layers={m.GpuLayers}  cpu-moe={m.CpuMoE}  jinja={m.Jinja.ToString().ToLowerInvariant()}[/]");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("\n[cyan]║ [red]No models configured.[/][/]");
        }

        AnsiConsole.MarkupLine("[bold cyan]╚════════════════════════════════════════════════════════╝[/]\n");
    }

    private void EditGeneralSettings()
    {
        //ShowCurrentSettings();

        ApplicationSettings newSettings = 
            SettingsManager.HasSettings ? 
                SettingsManager.GetSettings() with { } : // create a copy
                new ApplicationSettings(); // new empty

        var newPath = Helper.GetInput($"llama.cpp folder (current: \"{newSettings.LlamaCppPath}\")");

        if (!string.IsNullOrEmpty(newPath))
        {
            if (File.Exists(newPath))
                newSettings.LlamaCppPath = newPath;
            else
                AnsiConsole.MarkupLine("[red]✗ Path does not exist. Keeping current value.[/]\n");
        }

        var portStr = Helper.GetInput($"server port (current: {newSettings.ServerPort})");

        if (!string.IsNullOrWhiteSpace(portStr))
        {
            try
            {
                var port = int.Parse(portStr);
                if (port > 0 && port < 65536)
                    newSettings.ServerPort = port;
                else
                    AnsiConsole.MarkupLine("[red]✗ Must be number between 1-65535. Keeping current value.[/]\n");
            }
            catch
            {
                AnsiConsole.MarkupLine("[red]✗ Invalid number format. Keeping current value.[/]\n");
            }
        }

        var modelFolderInput = Helper.GetInput($"models folder path (current: \"{newSettings.ModelsFolderPath}\")");

        if (!string.IsNullOrEmpty(modelFolderInput))
        {
            if (Directory.Exists(modelFolderInput))
                newSettings.ModelsFolderPath = modelFolderInput;
            else
                AnsiConsole.MarkupLine("[red]\u2717 Path does not exist. Keeping current value.[/]\n");
        }

        // --- App-level editable llama-server flags ---
        var hostInput = Helper.GetInput($"host (current: \"{newSettings.Host}\")");
        if (!string.IsNullOrWhiteSpace(hostInput))
            newSettings.Host = hostInput.Trim();

        var cacheKInput = Helper.GetInput($"cache type K (current: \"{newSettings.CacheTypeK}\")");
        if (!string.IsNullOrWhiteSpace(cacheKInput))
            newSettings.CacheTypeK = cacheKInput.Trim();

        var cacheVInput = Helper.GetInput($"cache type V (current: \"{newSettings.CacheTypeV}\")");
        if (!string.IsNullOrWhiteSpace(cacheVInput))
            newSettings.CacheTypeV = cacheVInput.Trim();

        // Sampling defaults shared across all models
        var smp = newSettings.SamplingDefaults;
        if (PromptDouble("temperature", smp.Temperature) is { } t) smp.Temperature = t;
        if (PromptInt("top-k", smp.TopK) is { } k) smp.TopK = k;
        if (PromptDouble("top-p", smp.TopP) is { } p) smp.TopP = p;
        if (PromptDouble("min-p", smp.MinP) is { } mp) smp.MinP = mp;
        if (PromptDouble("repeat-penalty", smp.RepeatPenalty) is { } rp) smp.RepeatPenalty = rp;
        if (PromptInt("repeat-last-n", smp.RepeatLastN) is { } rln) smp.RepeatLastN = rln;

        try
        {
            SettingsManager.Save(newSettings);

            Clear();
            ShowCurrentSettings();

            Success("Settings saved");
            
        }
        catch (Exception exc)
        {
            Error("Failed to save Settings", exc);
        }
    }

    private void AddModel()
    {
        AnsiConsole.MarkupLine("\n[cyan]Add new model[/]\n");

        var gguf = Helper.GetInput("GGUF file (required)");
        if (string.IsNullOrEmpty(gguf)) { AnsiConsole.MarkupLine("[red]Cancelled.[/]"); return; }

        var id = Helper.GetInput("Model id (leave empty to use gguf name)");
        if (string.IsNullOrEmpty(id)) {
            id = Path.GetFileNameWithoutExtension(gguf);
            AnsiConsole.MarkupLine($"[dim]Using GGUF name as ID:[/] {id}\n");
        // Check if a model with this ID already exists
        var existingSettings = SettingsManager.GetSettings(forceReload: true);
        if (existingSettings.Models != null && existingSettings.Models.Any(m => m.Id == id))
        {
            AnsiConsole.MarkupLine($"[red]A model with ID '{id}' already exists. Please choose a different ID.[/]");
            return;
        }

        }

        // Alias (empty = auto-generate from GGUF filename)
        var aliasInput = Helper.GetInput("Alias (leave empty to use GGUF name)");
        string alias = string.IsNullOrWhiteSpace(aliasInput)
            ? Path.GetFileNameWithoutExtension(gguf)
            : aliasInput.Trim();

        // Parse context size
        var ctxSizeStr = Helper.GetInput("Context size in kilobytes (default: 64)");
        int ctxSize = 64;
        if (!string.IsNullOrWhiteSpace(ctxSizeStr) && int.TryParse(ctxSizeStr, out var parsedCtx))
        {
            ctxSize = parsedCtx;
        }
        else if (!string.IsNullOrWhiteSpace(ctxSizeStr))
        {
            AnsiConsole.MarkupLine("[red]Invalid number. Using default 64 KB.[/]");
        }
        ctxSize *= 1024;

        // Parse GPU layers
        var gpuLayersStr = Helper.GetInput("GPU layers (default: 0)");
        int gpuLayers = 0;
        if (!string.IsNullOrWhiteSpace(gpuLayersStr) && int.TryParse(gpuLayersStr, out var parsedGpu))
        {
            gpuLayers = parsedGpu;
        }
        else if (!string.IsNullOrWhiteSpace(gpuLayersStr))
        {
            AnsiConsole.MarkupLine("[red]Invalid number. Using default 0.[/]");
        }

        // Parse CPU MoE threads
        var cpuMoEInput = Helper.GetInput("CPU MoE (empty for 0)");
        int cpuMoE = 0;
        if (!string.IsNullOrWhiteSpace(cpuMoEInput) && int.TryParse(cpuMoEInput, out var parsedCpuMoE))
        {
            cpuMoE = parsedCpuMoE;
        }
        else if (!string.IsNullOrWhiteSpace(cpuMoEInput))
        {
            AnsiConsole.MarkupLine("[red]Invalid number. Using default 0.[/]");
        }

        // Parse Jinja toggle
        var jinjaInput = Helper.GetInput("Enable jinja? (y/n, empty=n)");
        bool jinja = !string.IsNullOrEmpty(jinjaInput) && jinjaInput.Trim().ToLowerInvariant().StartsWith('y');

        // Get fresh settings from disk and add the model
        ApplicationSettings settings = SettingsManager.GetSettings(forceReload: true);
        settings.Models ??= []; // defensive — should never be null but protects against corrupt state
        settings.Models.Add(new ModelSettings
        {
            Id = id,
            GgufFileName = gguf,
            ContextSize = ctxSize,
            GpuLayers = gpuLayers,
            CpuMoE = cpuMoE,
            Jinja = jinja,
            Alias = alias
        });

        try
        {
            SettingsManager.Save(settings);
        }
        catch (Exception exc)
        {
            Error("Failed to save model", exc);
            return;
        }

        Clear();
        ShowCurrentSettings();

        Success($"Model '{id}' added.");
    }

    private void EditModel()
    {
        var settings = SettingsManager.GetSettings(forceReload: true);
        if (settings.Models == null || settings.Models.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No models configured.[/]");
            return;
        }

        var ids = settings.Models.Select(m => m.Id).ToArray();
        var selectedId = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select model to edit:")
                .AddChoices(ids));

        // Find the model to edit
        var modelToEdit = settings.Models.FirstOrDefault(m => m.Id == selectedId);
        if (modelToEdit == null)
        {
            AnsiConsole.MarkupLine("[red]Error finding model to edit.[/]");
            return;
        }

        Clear();
        AnsiConsole.MarkupLine($"[green]Editing model: {modelToEdit.Id}[/]\n");

        // Get fresh input for each field with default values from existing model
        var ggufInput = Helper.GetInput($"GGUF file (current: \"{modelToEdit.GgufFileName}\")");
        if (!string.IsNullOrEmpty(ggufInput))
        {
            modelToEdit.GgufFileName = ggufInput;
        }

        var ctxSizeStr = Helper.GetInput($"Context size in kilobytes (default: {modelToEdit.ContextSize / 1024})");
        if (!string.IsNullOrWhiteSpace(ctxSizeStr) && int.TryParse(ctxSizeStr, out var parsedCtx))
        {
            modelToEdit.ContextSize = parsedCtx * 1024; // Convert KB back to bytes
        }
        else if (!string.IsNullOrWhiteSpace(ctxSizeStr))
        {
            AnsiConsole.MarkupLine("[red]Invalid number format. Keeping current value.[/]");
        }

        var gpuLayersStr = Helper.GetInput($"GPU layers (current: {modelToEdit.GpuLayers})");
        if (!string.IsNullOrWhiteSpace(gpuLayersStr) && int.TryParse(gpuLayersStr, out var parsedGpu))
        {
            modelToEdit.GpuLayers = parsedGpu;
        }
        else if (!string.IsNullOrWhiteSpace(gpuLayersStr))
        {
            AnsiConsole.MarkupLine("[red]Invalid number format. Keeping current value.[/]");
        }

        var cpuMoEInput = Helper.GetInput($"CPU MoE threads (current: {modelToEdit.CpuMoE})");
        if (!string.IsNullOrWhiteSpace(cpuMoEInput) && int.TryParse(cpuMoEInput, out var parsedCpuMoE))
        {
            modelToEdit.CpuMoE = parsedCpuMoE;
        }
        else if (!string.IsNullOrWhiteSpace(cpuMoEInput))
        {
            AnsiConsole.MarkupLine("[red]Invalid number format. Keeping current value.[/]");
        }

        var aliasInput = Helper.GetInput($"Alias (current: \"{(modelToEdit.Alias == "" ? "(auto)" : modelToEdit.Alias)}\")");
        if (!string.IsNullOrWhiteSpace(aliasInput))
            modelToEdit.Alias = aliasInput.Trim();

        var jinjaInput = Helper.GetInput($"Enable Jinja? (y/n, empty=n) (current: {(modelToEdit.Jinja ? "yes" : "no")})");
        bool jinja = !string.IsNullOrEmpty(jinjaInput) && jinjaInput.Trim().ToLowerInvariant().StartsWith('y');
        modelToEdit.Jinja = jinja;

        try
        {
            SettingsManager.Save(settings);
            Success($"Model '{selectedId}' updated.");
        }
        catch (Exception exc)
        {
            Error("Failed to save changes", exc);
        }
    }

    private void RemoveModel()
    {
        var settings = SettingsManager.GetSettings();
        if (settings.Models == null || settings.Models.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No models configured.[/]");
            return;
        }

        var ids = settings.Models.Select(m => m.Id).ToArray();
        var selectedId = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select model to remove:")
                .AddChoices(ids));

        // Confirmation
        AnsiConsole.MarkupLine($"\n[yellow]Are you sure you want to remove '{selectedId}'? (y/n)[/] ");
        var confirm = Console.ReadLine()?.Trim().ToLowerInvariant();
        if (confirm != "y")
        {
            AnsiConsole.MarkupLine("[dim]Cancelled.\n");
            return;
        }

        settings.Models.RemoveAll(m => m.Id == selectedId);
        SettingsManager.Save(settings);

        Clear();
        ShowCurrentSettings();

        Success($"Removed model '{selectedId}'.");
    }

    /// <summary>
    /// Prompts for a double; returns null when the user leaves it empty or enters an invalid value.
    /// </summary>
    private static double? PromptDouble(string label, double current)
    {
        var input = Helper.GetInput($"{label} (current: {current})");
        if (string.IsNullOrWhiteSpace(input)) return null;
        if (double.TryParse(input, System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture, out var v))
            return v;
        AnsiConsole.MarkupLine("[red]Invalid number. Keeping current value.[/]\n");
        return null;
    }

    /// <summary>
    /// Prompts for an int; returns null when the user leaves it empty or enters an invalid value.
    /// </summary>
    private static int? PromptInt(string label, int current)
    {
        var input = Helper.GetInput($"{label} (current: {current})");
        if (string.IsNullOrWhiteSpace(input)) return null;
        if (int.TryParse(input, out var v))
            return v;
        AnsiConsole.MarkupLine("[red]Invalid number. Keeping current value.[/]\n");
        return null;
    }
}
