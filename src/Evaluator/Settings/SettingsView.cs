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
        public const string EditSettings       = "Edit settings";
        public const string EditSamplingDefaults = "Edit sampling defaults";
        public const string AddModel           = "Add model";
        public const string EditModel          = "Edit model";
        public const string RemoveModel        = "Remove model";
        public const string BackToMain         = "Back to main";
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
                    .AddChoices(Menu.EditSettings, Menu.EditSamplingDefaults, Menu.AddModel, Menu.EditModel, Menu.RemoveModel, Menu.BackToMain));

            switch (choice)
            {
                case Menu.EditSettings:
                    EditGeneralSettings();
                    break;
                case Menu.EditSamplingDefaults:
                    EditSamplingDefaults();
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

        // App-level editable settings
        AnsiConsole.MarkupLine("[bold cyan]╠══ App Settings ════════════════════════════════════════╗[/]");
        AnsiConsole.MarkupLine($"[cyan]║ Host:[/] {settings.Host}");
        AnsiConsole.MarkupLine($"[cyan]║ Server Port:[/] {settings.ServerPort}");
        AnsiConsole.MarkupLine($"[cyan]║ Cache Type K:[/] {settings.CacheTypeK}");
        AnsiConsole.MarkupLine($"[cyan]║ Cache Type V:[/] {settings.CacheTypeV}");
        AnsiConsole.MarkupLine($"[cyan]║ llama.cpp folder:[/] {(string.IsNullOrEmpty(settings.LlamaCppPath) ? "(empty)" : settings.LlamaCppPath)}");
        AnsiConsole.MarkupLine($"[cyan]║ Models Folder:[/] {(string.IsNullOrEmpty(settings.ModelsFolderPath) ? "(empty)" : settings.ModelsFolderPath)}");
        // Sampling defaults
        AnsiConsole.MarkupLine("[bold cyan]╠══ Sampling Defaults ════════════════════════════════════╗[/]");
        AnsiConsole.MarkupLine($"[cyan]║ Temperature:[/] {settings.SamplingDefaults.Temperature}");
        AnsiConsole.MarkupLine($"[cyan]║ Top K:[/] {settings.SamplingDefaults.TopK}");
        AnsiConsole.MarkupLine($"[cyan]║ Top P:[/] {settings.SamplingDefaults.TopP}");
        AnsiConsole.MarkupLine($"[cyan]║ Min P:[/] {settings.SamplingDefaults.MinP}");
        AnsiConsole.MarkupLine($"[cyan]║ Repeat Penalty:[/] {settings.SamplingDefaults.RepeatPenalty}");
        AnsiConsole.MarkupLine($"[cyan]║ Repeat Last N:[/] {settings.SamplingDefaults.RepeatLastN}");
        // Readonly server defaults
        AnsiConsole.MarkupLine("[bold cyan]╠══ Server Defaults (readonly) ════════════════════════════╗[/]");
        AnsiConsole.MarkupLine($"[cyan]║ Parallel:[/] {settings.ServerDefaults.Parallel}");
        AnsiConsole.MarkupLine($"[cyan]║ Prio:[/] {settings.ServerDefaults.Prio}");
        AnsiConsole.MarkupLine($"[cyan]║ Flash Attn:[/] {settings.ServerDefaults.FlashAttn}");
        AnsiConsole.MarkupLine($"[cyan]║ KV Unified:[/] {(settings.ServerDefaults.KvUnified ? "true" : "false")}");
        AnsiConsole.MarkupLine($"[cyan]║ Load Mode:[/] {settings.ServerDefaults.LoadMode}");
        AnsiConsole.MarkupLine($"[cyan]║ Fit:[/] {settings.ServerDefaults.Fit}");
        AnsiConsole.MarkupLine($"[cyan]║ Cache Reuse:[/] {settings.ServerDefaults.CacheReuse}");
        AnsiConsole.MarkupLine($"[cyan]║ Draft P Min:[/] {settings.ServerDefaults.DraftPMin}");
        AnsiConsole.MarkupLine($"[cyan]║ Log Verbosity:[/] {settings.ServerDefaults.LogVerbosity}");
        AnsiConsole.MarkupLine($"[cyan]║ Samplers:[/] {settings.ServerDefaults.Samplers}");
        AnsiConsole.MarkupLine($"[cyan]║ Context Shift:[/] {(settings.ServerDefaults.ContextShift ? "true" : "false")}");
        AnsiConsole.MarkupLine($"[cyan]║ Reasoning Preserve:[/] {(settings.ServerDefaults.ReasoningPreserve ? "true" : "false")}");
        AnsiConsole.MarkupLine($"[cyan]║ Reasoning:[/] {settings.ServerDefaults.Reasoning}");
        AnsiConsole.MarkupLine($"[cyan]║ Reasoning Budget:[/] {settings.ServerDefaults.ReasoningBudget}");
        AnsiConsole.MarkupLine($"[cyan]║ Batch Size:[/] {settings.ServerDefaults.BatchSize}");
        AnsiConsole.MarkupLine($"[cyan]║ UBatch Size:[/] {settings.ServerDefaults.UbatchSize}");
        AnsiConsole.MarkupLine($"[cyan]║ Spec Type:[/] {settings.ServerDefaults.SpecType}");

        if (settings.Models.Count > 0)
        {
            AnsiConsole.MarkupLine("\n[cyan]║ Models:[/]");
            for (int i = 0; i < settings.Models.Count; i++)
            {
                var m = settings.Models[i];
                AnsiConsole.MarkupLine($"[cyan]║ #{i + 1}[/] {m.Id}: {m.GgufFileName} [Alias: {m.Alias}]");
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

        var hostInput = Helper.GetInput($"host (current: \"{newSettings.Host}\")");
        if (!string.IsNullOrWhiteSpace(hostInput))
        {
            newSettings.Host = hostInput;
        }

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

        var cacheTypeKInput = Helper.GetInput($"cache type K (current: \"{newSettings.CacheTypeK}\")");
        if (!string.IsNullOrWhiteSpace(cacheTypeKInput))
        {
            newSettings.CacheTypeK = cacheTypeKInput;
        }

        var cacheTypeVInput = Helper.GetInput($"cache type V (current: \"{newSettings.CacheTypeV}\")");
        if (!string.IsNullOrWhiteSpace(cacheTypeVInput))
        {
            newSettings.CacheTypeV = cacheTypeVInput;
        }

        var modelFolderInput = Helper.GetInput($"models folder path (current: \"{newSettings.ModelsFolderPath}\")");

        if (!string.IsNullOrEmpty(modelFolderInput))
        {
            if (Directory.Exists(modelFolderInput))
                newSettings.ModelsFolderPath = modelFolderInput;
            else
                AnsiConsole.MarkupLine("[red]\u2717 Path does not exist. Keeping current value.[/]\n");
        }

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

        // Auto-generate alias from GGUF filename if empty
        string alias = string.Empty;
        var aliasInput = Helper.GetInput("Alias (leave empty to auto-generate from GGUF filename)");
        if (!string.IsNullOrWhiteSpace(aliasInput))
        {
            alias = aliasInput;
        }
        else
        {
            // Strip .gguf extension to auto-generate alias
            alias = Path.GetFileNameWithoutExtension(gguf);
        }

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

        // Alias input — only auto-generate if current alias is empty
        string editAlias;
        var editAliasInput = Helper.GetInput($"Alias (leave empty to keep current or auto-generate from GGUF filename, current: \"{modelToEdit.Alias}\")");
        if (!string.IsNullOrWhiteSpace(editAliasInput))
        {
            editAlias = editAliasInput;
        }
        else if (string.IsNullOrEmpty(modelToEdit.Alias))
        {
            editAlias = Path.GetFileNameWithoutExtension(modelToEdit.GgufFileName);
        }
        else
        {
            editAlias = modelToEdit.Alias;
        }
        modelToEdit.Alias = editAlias;

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

    private void EditSamplingDefaults()
    {
        ApplicationSettings newSettings =
            SettingsManager.HasSettings ?
                SettingsManager.GetSettings() with { } :
                new ApplicationSettings();

        var tempStr = Helper.GetInput($"temperature (current: {newSettings.SamplingDefaults.Temperature})");
        if (!string.IsNullOrWhiteSpace(tempStr) && double.TryParse(tempStr, out var parsedTemp))
            newSettings.SamplingDefaults.Temperature = parsedTemp;

        var topKStr = Helper.GetInput($"top-k (current: {newSettings.SamplingDefaults.TopK})");
        if (!string.IsNullOrWhiteSpace(topKStr) && int.TryParse(topKStr, out var parsedTopK))
            newSettings.SamplingDefaults.TopK = parsedTopK;

        var topPStr = Helper.GetInput($"top-p (current: {newSettings.SamplingDefaults.TopP})");
        if (!string.IsNullOrWhiteSpace(topPStr) && double.TryParse(topPStr, out var parsedTopP))
            newSettings.SamplingDefaults.TopP = parsedTopP;

        var minPStr = Helper.GetInput($"min-p (current: {newSettings.SamplingDefaults.MinP})");
        if (!string.IsNullOrWhiteSpace(minPStr) && double.TryParse(minPStr, out var parsedMinP))
            newSettings.SamplingDefaults.MinP = parsedMinP;

        var repPenStr = Helper.GetInput($"repeat penalty (current: {newSettings.SamplingDefaults.RepeatPenalty})");
        if (!string.IsNullOrWhiteSpace(repPenStr) && double.TryParse(repPenStr, out var parsedRepPen))
            newSettings.SamplingDefaults.RepeatPenalty = parsedRepPen;

        var repLastNStr = Helper.GetInput($"repeat last n (current: {newSettings.SamplingDefaults.RepeatLastN})");
        if (!string.IsNullOrWhiteSpace(repLastNStr) && int.TryParse(repLastNStr, out var parsedRepLastN))
            newSettings.SamplingDefaults.RepeatLastN = parsedRepLastN;

        try
        {
            SettingsManager.Save(newSettings);
            Clear();
            ShowCurrentSettings();
            Success("Sampling defaults saved");
        }
        catch (Exception exc)
        {
            Error("Failed to save sampling defaults", exc);
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
