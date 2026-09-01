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
        AnsiConsole.MarkupLine($"[cyan]║ Host:[/] {settings.Host}");
        AnsiConsole.MarkupLine($"[cyan]║ Server Port:[/] {settings.ServerPort}");
        AnsiConsole.MarkupLine($"[cyan]║ Cache Type K:[/] {settings.CacheTypeK}");
        AnsiConsole.MarkupLine($"[cyan]║ Cache Type V:[/] {settings.CacheTypeV}");
        var modelFolder = string.IsNullOrEmpty(settings.ModelsFolderPath) ? "(empty)" : settings.ModelsFolderPath;
        AnsiConsole.MarkupLine($"[cyan]║ Models Folder:[/] {modelFolder}");

        var s = settings.SamplingDefaults;
        AnsiConsole.MarkupLine("\n[cyan]║ Sampling Defaults:[/]");
        AnsiConsole.MarkupLine($"[cyan]║   temperature: {s.Temperature}   top-k: {s.TopK}   top-p: {s.TopP}[/]");
        AnsiConsole.MarkupLine($"[cyan]║   min-p: {s.MinP}   repeat-penalty: {s.RepeatPenalty}   repeat-last-n: {s.RepeatLastN}[/]");

        var d = settings.ServerDefaults;
        AnsiConsole.MarkupLine("\n[cyan]║ Server Defaults (read-only):[/]");
        AnsiConsole.MarkupLine($"[cyan]║   parallel: {d.Parallel}   prio: {d.Prio}   flash-attn: {d.FlashAttn}   kv-unified: {d.KvUnified}[/]");
        AnsiConsole.MarkupLine($"[cyan]║   load-mode: {d.LoadMode}   fit: {d.Fit}   cache-reuse: {d.CacheReuse}   draft-p-min: {d.DraftPM}[/]");
        AnsiConsole.MarkupLine($"[cyan]║   log-verbosity: {d.LogVerbosity}   samplers: {d.Samplers}[/]");
        AnsiConsole.MarkupLine($"[cyan]║   context-shift: {d.ContextShift}   reasoning-preserve: {d.ReasoningPreserve}   reasoning: {d.Reasoning}[/]");
        AnsiConsole.MarkupLine($"[cyan]║   reasoning-budget: {d.ReasoningBudget}   batch-size: {d.BatchSize}   ubatch-size: {d.UbatchSize}   spec-type: {d.SpecType}[/]");

        if (settings.Models.Count > 0)
        {
            AnsiConsole.MarkupLine("\n[cyan]║ Models:[/]");
            for (int i = 0; i < settings.Models.Count; i++)
            {
                var m = settings.Models[i];
                var alias = string.IsNullOrEmpty(m.Alias) ? Path.GetFileNameWithoutExtension(m.GgufFileName) : m.Alias;
                AnsiConsole.MarkupLine($"[cyan]║ #{i + 1}[/] {m.Id}: {m.GgufFileName} [dim](alias: {alias})[/]");
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

        var hostInput = Helper.GetInput($"host (current: {newSettings.Host})");
        if (!string.IsNullOrWhiteSpace(hostInput))
            newSettings.Host = hostInput.Trim();

        var cacheKInput = Helper.GetInput($"cache type K (current: {newSettings.CacheTypeK})");
        if (!string.IsNullOrWhiteSpace(cacheKInput))
            newSettings.CacheTypeK = cacheKInput.Trim();

        var cacheVInput = Helper.GetInput($"cache type V (current: {newSettings.CacheTypeV})");
        if (!string.IsNullOrWhiteSpace(cacheVInput))
            newSettings.CacheTypeV = cacheVInput.Trim();

        EditSamplingDefaults(newSettings);

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

    private static void EditSamplingDefaults(ApplicationSettings settings)
    {
        var s = settings.SamplingDefaults;

        var tempStr = Helper.GetInput($"temperature (current: {s.Temperature})");
        if (!string.IsNullOrWhiteSpace(tempStr) && double.TryParse(tempStr, out var temp))
            s.Temperature = temp;
        else if (!string.IsNullOrWhiteSpace(tempStr))
            AnsiConsole.MarkupLine("[red]\u2717 Invalid number. Keeping current value.[/]\n");

        var topKStr = Helper.GetInput($"top-k (current: {s.TopK})");
        if (!string.IsNullOrWhiteSpace(topKStr) && int.TryParse(topKStr, out var topK))
            s.TopK = topK;
        else if (!string.IsNullOrWhiteSpace(topKStr))
            AnsiConsole.MarkupLine("[red]\u2717 Invalid number. Keeping current value.[/]\n");

        var topPStr = Helper.GetInput($"top-p (current: {s.TopP})");
        if (!string.IsNullOrWhiteSpace(topPStr) && double.TryParse(topPStr, out var topP))
            s.TopP = topP;
        else if (!string.IsNullOrWhiteSpace(topPStr))
            AnsiConsole.MarkupLine("[red]\u2717 Invalid number. Keeping current value.[/]\n");

        var minPStr = Helper.GetInput($"min-p (current: {s.MinP})");
        if (!string.IsNullOrWhiteSpace(minPStr) && double.TryParse(minPStr, out var minP))
            s.MinP = minP;
        else if (!string.IsNullOrWhiteSpace(minPStr))
            AnsiConsole.MarkupLine("[red]\u2717 Invalid number. Keeping current value.[/]\n");

        var repPenStr = Helper.GetInput($"repeat-penalty (current: {s.RepeatPenalty})");
        if (!string.IsNullOrWhiteSpace(repPenStr) && double.TryParse(repPenStr, out var repPen))
            s.RepeatPenalty = repPen;
        else if (!string.IsNullOrWhiteSpace(repPenStr))
            AnsiConsole.MarkupLine("[red]\u2717 Invalid number. Keeping current value.[/]\n");

        var repLastStr = Helper.GetInput($"repeat-last-n (current: {s.RepeatLastN})");
        if (!string.IsNullOrWhiteSpace(repLastStr) && int.TryParse(repLastStr, out var repLast))
            s.RepeatLastN = repLast;
        else if (!string.IsNullOrWhiteSpace(repLastStr))
            AnsiConsole.MarkupLine("[red]\u2717 Invalid number. Keeping current value.[/]\n");
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

        // Alias: leave empty to auto-generate from GGUF filename
        var aliasInput = Helper.GetInput($"Alias (leave empty to use \"{Path.GetFileNameWithoutExtension(gguf)}\")");
        string alias = string.IsNullOrWhiteSpace(aliasInput) ? Path.GetFileNameWithoutExtension(gguf) : aliasInput.Trim();

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

        var jinjaInput = Helper.GetInput($"Enable Jinja? (y/n, empty=n) (current: {(modelToEdit.Jinja ? "yes" : "no")})");
        bool jinja = !string.IsNullOrEmpty(jinjaInput) && jinjaInput.Trim().ToLowerInvariant().StartsWith('y');
        modelToEdit.Jinja = jinja;

        var currentAlias = string.IsNullOrEmpty(modelToEdit.Alias) ? Path.GetFileNameWithoutExtension(modelToEdit.GgufFileName) : modelToEdit.Alias;
        var aliasInput = Helper.GetInput($"Alias (current: {currentAlias}, empty = auto-gen from GGUF name)");
        if (!string.IsNullOrWhiteSpace(aliasInput))
            modelToEdit.Alias = aliasInput.Trim();
        else
            modelToEdit.Alias = Path.GetFileNameWithoutExtension(modelToEdit.GgufFileName);

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
}
