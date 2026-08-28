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
        var modelFolder = string.IsNullOrEmpty(settings.ModelsFolderPath) ? "(empty)" : settings.ModelsFolderPath;
        AnsiConsole.MarkupLine($"[cyan]║ Models Folder:[/] {modelFolder}");
        AnsiConsole.MarkupLine($"[cyan]║ Cache Type K/V:[/] {settings.CacheTypeK} / {settings.CacheTypeV}");

        // Sampling defaults section
        AnsiConsole.MarkupLine("\n[cyan]║ [bold]Sampling Defaults[/][/]");
        var sd = settings.SamplingDefaults;
        AnsiConsole.MarkupLine($"[cyan]║   Temperature:[/] {sd.Temperature}");
        AnsiConsole.MarkupLine($"[cyan]║   Top-K:[/] {sd.TopK}");
        AnsiConsole.MarkupLine($"[cyan]║   Top-P:[/] {sd.TopP}");
        AnsiConsole.MarkupLine($"[cyan]║   Min-P:[/] {sd.MinP}");
        AnsiConsole.MarkupLine($"[cyan]║   Repeat Penalty:[/] {sd.RepeatPenalty}");
        AnsiConsole.MarkupLine($"[cyan]║   Repeat Last-N:[/] {sd.RepeatLastN}");

        // Server defaults section (readonly)
        AnsiConsole.MarkupLine("\n[cyan]║ [bold]Server Defaults (read-only)[/][/]");
        var srv = settings.ServerDefaults;
        AnsiConsole.MarkupLine($"[cyan]║   Parallel:[/] {srv.Parallel}");
        AnsiConsole.MarkupLine($"[cyan]║   Prio:[/] {srv.Prio}");
        AnsiConsole.MarkupLine($"[cyan]║   Flash Attention:[/] {srv.FlashAttn}");
        AnsiConsole.MarkupLine($"[cyan]║   KV Unified:[/] {srv.KvUnified}");
        AnsiConsole.MarkupLine($"[cyan]║   Load Mode:[/] {srv.LoadMode}");
        AnsiConsole.MarkupLine($"[cyan]║   Fit:[/] {srv.Fit}");
        AnsiConsole.MarkupLine($"[cyan]║   Cache Reuse:[/] {srv.CacheReuse}");
        AnsiConsole.MarkupLine($"[cyan]║   Draft P-Min:[/] {srv.DraftPMin}");
        AnsiConsole.MarkupLine($"[cyan]║   Log Verbosity:[/] {srv.LogVerbosity}");
        AnsiConsole.MarkupLine($"[cyan]║   Samplers:[/] {srv.Samplers}");
        AnsiConsole.MarkupLine($"[cyan]║   Context Shift:[/] {srv.ContextShift}");
        AnsiConsole.MarkupLine($"[cyan]║   Reasoning Preserve:[/] {srv.ReasoningPreserve}");
        AnsiConsole.MarkupLine($"[cyan]║   Reasoning:[/] {srv.Reasoning}");
        AnsiConsole.MarkupLine($"[cyan]║   Reasoning Budget:[/] {srv.ReasoningBudget}");
        AnsiConsole.MarkupLine($"[cyan]║   Batch Size:[/] {srv.BatchSize}");
        AnsiConsole.MarkupLine($"[cyan]║   Ubatch Size:[/] {srv.UbatchSize}");
        AnsiConsole.MarkupLine($"[cyan]║   Spec Type:[/] {srv.SpecType}");

        if (settings.Models.Count > 0)
        {
            AnsiConsole.MarkupLine("\n[cyan]║ Models:[/]");
            for (int i = 0; i < settings.Models.Count; i++)
            {
                var m = settings.Models[i];
                var aliasStr = string.IsNullOrEmpty(m.Alias) ? "(auto)" : $"[{m.Alias}]";
                AnsiConsole.MarkupLine($"[cyan]║ #{i + 1}[/] {m.Id}: {m.GgufFileName} {aliasStr}");
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
        ApplicationSettings newSettings = 
            SettingsManager.HasSettings ? 
                SettingsManager.GetSettings() with { } :
                new ApplicationSettings();

        var newPath = Helper.GetInput($"llama.cpp folder (current: \"{newSettings.LlamaCppPath}\")");
        if (!string.IsNullOrEmpty(newPath))
        {
            if (File.Exists(newPath))
                newSettings.LlamaCppPath = newPath;
            else
                AnsiConsole.MarkupLine("[red]\u2717 Path does not exist. Keeping current value.\n");
        }

        var hostInput = Helper.GetInput($"Host (current: \"{newSettings.Host}\")");
        if (!string.IsNullOrWhiteSpace(hostInput))
            newSettings.Host = hostInput.Trim();

        var portStr = Helper.GetInput($"server port (current: {newSettings.ServerPort})");
        if (!string.IsNullOrWhiteSpace(portStr))
        {
            try
            {
                var port = int.Parse(portStr);
                if (port > 0 && port < 65536)
                    newSettings.ServerPort = port;
                else
                    AnsiConsole.MarkupLine("[red]\u2717 Must be number between 1-65535. Keeping current value.\n");
            }
            catch
            {
                AnsiConsole.MarkupLine("[red]\u2717 Invalid number format. Keeping current value.\n");
            }
        }

        var modelFolderInput = Helper.GetInput($"models folder path (current: \"{newSettings.ModelsFolderPath}\")");
        if (!string.IsNullOrEmpty(modelFolderInput))
        {
            if (Directory.Exists(modelFolderInput))
                newSettings.ModelsFolderPath = modelFolderInput;
            else
                AnsiConsole.MarkupLine("[red]\u2717 Path does not exist. Keeping current value.\n");
        }

        // Cache type K
        var cacheTypeKInput = Helper.GetInput($"Cache Type K (current: \"{newSettings.CacheTypeK}\")");
        if (!string.IsNullOrWhiteSpace(cacheTypeKInput))
            newSettings.CacheTypeK = cacheTypeKInput.Trim();

        // Cache type V
        var cacheTypeVInput = Helper.GetInput($"Cache Type V (current: \"{newSettings.CacheTypeV}\")");
        if (!string.IsNullOrWhiteSpace(cacheTypeVInput))
            newSettings.CacheTypeV = cacheTypeVInput.Trim();

        // Sampling defaults
        AnsiConsole.MarkupLine("\n[cyan]Sampling Defaults[/]");

        var tempInput = Helper.GetInput($"  Temperature (current: {newSettings.SamplingDefaults.Temperature})");
        if (!string.IsNullOrWhiteSpace(tempInput) && double.TryParse(tempInput, out var temp))
            newSettings.SamplingDefaults = newSettings.SamplingDefaults with { Temperature = temp };

        var topKInput = Helper.GetInput($"  Top-K (current: {newSettings.SamplingDefaults.TopK})");
        if (!string.IsNullOrWhiteSpace(topKInput) && int.TryParse(topKInput, out var topK))
            newSettings.SamplingDefaults = newSettings.SamplingDefaults with { TopK = topK };

        var topPInput = Helper.GetInput($"  Top-P (current: {newSettings.SamplingDefaults.TopP})");
        if (!string.IsNullOrWhiteSpace(topPInput) && double.TryParse(topPInput, out var topP))
            newSettings.SamplingDefaults = newSettings.SamplingDefaults with { TopP = topP };

        var minPInput = Helper.GetInput($"  Min-P (current: {newSettings.SamplingDefaults.MinP})");
        if (!string.IsNullOrWhiteSpace(minPInput) && double.TryParse(minPInput, out var minP))
            newSettings.SamplingDefaults = newSettings.SamplingDefaults with { MinP = minP };

        var repeatPenaltyInput = Helper.GetInput($"  Repeat Penalty (current: {newSettings.SamplingDefaults.RepeatPenalty})");
        if (!string.IsNullOrWhiteSpace(repeatPenaltyInput) && double.TryParse(repeatPenaltyInput, out var repeatPenalty))
            newSettings.SamplingDefaults = newSettings.SamplingDefaults with { RepeatPenalty = repeatPenalty };

        var repeatLastNInput = Helper.GetInput($"  Repeat Last-N (current: {newSettings.SamplingDefaults.RepeatLastN})");
        if (!string.IsNullOrWhiteSpace(repeatLastNInput) && int.TryParse(repeatLastNInput, out var repeatLastN))
            newSettings.SamplingDefaults = newSettings.SamplingDefaults with { RepeatLastN = repeatLastN };

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

        string autoAlias = Path.GetFileNameWithoutExtension(gguf);
        var aliasInput = Helper.GetInput($"Alias (leave empty for auto: \"{autoAlias}\")");
        string alias = string.IsNullOrWhiteSpace(aliasInput) ? autoAlias : aliasInput.Trim();

        var id = Helper.GetInput("Model id (leave empty to use alias)");
        if (string.IsNullOrEmpty(id))
        {
            id = alias;
            AnsiConsole.MarkupLine($"[dim]Using alias as ID:[/] {id}\n");
        }

        var existingSettings = SettingsManager.GetSettings(forceReload: true);
        if (existingSettings.Models != null && existingSettings.Models.Any(m => m.Id == id))
        {
            AnsiConsole.MarkupLine($"[red]A model with ID '{id}' already exists. Please choose a different ID.[/]");
            return;
        }

        var ctxSizeStr = Helper.GetInput("Context size in kilobytes (default: 64)");
        int ctxSize = 64;
        if (!string.IsNullOrWhiteSpace(ctxSizeStr) && int.TryParse(ctxSizeStr, out var parsedCtx))
        {
            ctxSize = parsedCtx;
        }
        else if (!string.IsNullOrWhiteSpace(ctxSizeStr))
        {
            AnsiConsole.MarkupLine("[red]Invalid number. Using default 64 KB.\n");
        }
        ctxSize *= 1024;

        var gpuLayersStr = Helper.GetInput("GPU layers (default: 0)");
        int gpuLayers = 0;
        if (!string.IsNullOrWhiteSpace(gpuLayersStr) && int.TryParse(gpuLayersStr, out var parsedGpu))
        {
            gpuLayers = parsedGpu;
        }
        else if (!string.IsNullOrWhiteSpace(gpuLayersStr))
        {
            AnsiConsole.MarkupLine("[red]Invalid number. Using default 0.\n");
        }

        var cpuMoEInput = Helper.GetInput("CPU MoE (empty for 0)");
        int cpuMoE = 0;
        if (!string.IsNullOrWhiteSpace(cpuMoEInput) && int.TryParse(cpuMoEInput, out var parsedCpuMoE))
        {
            cpuMoE = parsedCpuMoE;
        }
        else if (!string.IsNullOrWhiteSpace(cpuMoEInput))
        {
            AnsiConsole.MarkupLine("[red]Invalid number. Using default 0.\n");
        }

        var jinjaInput = Helper.GetInput("Enable jinja? (y/n, empty=n)");
        bool jinja = !string.IsNullOrEmpty(jinjaInput) && jinjaInput.Trim().ToLowerInvariant().StartsWith('y');

        ApplicationSettings settings = SettingsManager.GetSettings(forceReload: true);
        settings.Models ??= [];
        settings.Models.Add(new ModelSettings
        {
            Id = id,
            Alias = alias,
            GgufFileName = gguf,
            ContextSize = ctxSize,
            GpuLayers = gpuLayers,
            CpuMoE = cpuMoE,
            Jinja = jinja
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

        var modelToEdit = settings.Models.FirstOrDefault(m => m.Id == selectedId);
        if (modelToEdit == null)
        {
            AnsiConsole.MarkupLine("[red]Error finding model to edit.[/]");
            return;
        }

        Clear();
        AnsiConsole.MarkupLine($"[green]Editing model: {modelToEdit.Id}[/]\n");

        // Alias input
        var autoAlias = Path.GetFileNameWithoutExtension(modelToEdit.GgufFileName);
        var aliasDisplay = string.IsNullOrEmpty(modelToEdit.Alias) ? $"(auto: \"{autoAlias}\")" : $"\"{modelToEdit.Alias}\"";
        var aliasInput2 = Helper.GetInput($"Alias (current: {aliasDisplay})");
        if (!string.IsNullOrWhiteSpace(aliasInput2))
            modelToEdit.Alias = aliasInput2.Trim();

        var ggufInput = Helper.GetInput($"GGUF file (current: \"{modelToEdit.GgufFileName}\")");
        if (!string.IsNullOrEmpty(ggufInput))
            modelToEdit.GgufFileName = ggufInput;

        var ctxSizeStr = Helper.GetInput($"Context size in kilobytes (default: {modelToEdit.ContextSize / 1024})");
        if (!string.IsNullOrWhiteSpace(ctxSizeStr) && int.TryParse(ctxSizeStr, out var parsedCtx))
        {
            modelToEdit.ContextSize = parsedCtx * 1024;
        }
        else if (!string.IsNullOrWhiteSpace(ctxSizeStr))
        {
            AnsiConsole.MarkupLine("[red]Invalid number format. Keeping current value.\n");
        }

        var gpuLayersStr = Helper.GetInput($"GPU layers (current: {modelToEdit.GpuLayers})");
        if (!string.IsNullOrWhiteSpace(gpuLayersStr) && int.TryParse(gpuLayersStr, out var parsedGpu))
        {
            modelToEdit.GpuLayers = parsedGpu;
        }
        else if (!string.IsNullOrWhiteSpace(gpuLayersStr))
        {
            AnsiConsole.MarkupLine("[red]Invalid number format. Keeping current value.\n");
        }

        var cpuMoEInput = Helper.GetInput($"CPU MoE threads (current: {modelToEdit.CpuMoE})");
        if (!string.IsNullOrWhiteSpace(cpuMoEInput) && int.TryParse(cpuMoEInput, out var parsedCpuMoE))
        {
            modelToEdit.CpuMoE = parsedCpuMoE;
        }
        else if (!string.IsNullOrWhiteSpace(cpuMoEInput))
        {
            AnsiConsole.MarkupLine("[red]Invalid number format. Keeping current value.\n");
        }

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
