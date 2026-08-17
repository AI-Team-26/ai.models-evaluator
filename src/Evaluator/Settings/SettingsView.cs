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
        AnsiConsole.MarkupLine($"[cyan]║ Cache Type K:[/] {settings.CacheTypeK}");
        AnsiConsole.MarkupLine($"[cyan]║ Cache Type V:[/] {settings.CacheTypeV}");

        // Sampling defaults
        var sd = settings.SamplingDefaults!;
        AnsiConsole.MarkupLine("\n[cyan]║ Sampling Defaults:[/]");
        AnsiConsole.MarkupLine($"[cyan]║   Temperature:[/] {sd.Temperature}  [cyan]Top-K:[/] {sd.TopK}  [cyan]Top-P:[/] {sd.TopP}");
        AnsiConsole.MarkupLine($"[cyan]║   Min-P:[/] {sd.MinP}  [cyan]Repeat Penalty:[/] {sd.RepeatPenalty}  [cyan]Repeat Last N:[/] {sd.RepeatLastN}");

        // Server defaults (readonly)
        var sv = settings.ServerDefaults!;
        AnsiConsole.MarkupLine("\n[cyan]║ Server Defaults (readonly):[/]");
        AnsiConsole.MarkupLine($"[cyan]║   Parallel:[/] {sv.Parallel}  [cyan]Prio:[/] {sv.Prio}  [cyan]Flash Attn:[/] {sv.FlashAttn}  [cyan]KV Unified:[/] {sv.KvUnified}");
        AnsiConsole.MarkupLine($"[cyan]║   Load Mode:[/] {sv.LoadMode}  [cyan]Fit:[/] {sv.Fit}  [cyan]Cache Reuse:[/] {sv.CacheReuse}");
        AnsiConsole.MarkupLine($"[cyan]║   Draft P-Min:[/] {sv.DraftPMin}  [cyan]Log Verbosity:[/] {sv.LogVerbosity}");
        AnsiConsole.MarkupLine($"[cyan]║   Samplers:[/] {sv.Samplers}");
        AnsiConsole.MarkupLine($"[cyan]║   Context Shift:[/] {sv.ContextShift}  [cyan]Reasoning Preserve:[/] {sv.ReasoningPreserve}");
        AnsiConsole.MarkupLine($"[cyan]║   Reasoning:[/] {sv.Reasoning}  [cyan]Reasoning Budget:[/] {sv.ReasoningBudget}");
        AnsiConsole.MarkupLine($"[cyan]║   Reasoning Budget Message:[/] {sv.ReasoningBudgetMessage}");
        AnsiConsole.MarkupLine($"[cyan]║   Batch Size:[/] {sv.BatchSize}  [cyan]Ubatch Size:[/] {sv.UbatchSize}  [cyan]Spec Type:[/] {sv.SpecType}");

        if (settings.Models.Count > 0)
        {
            AnsiConsole.MarkupLine("\n[cyan]║ Models:[/]");
            for (int i = 0; i < settings.Models.Count; i++)
            {
                var m = settings.Models[i];
                var alias = string.IsNullOrEmpty(m.Alias) ? "(auto)" : m.Alias;
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
                AnsiConsole.MarkupLine("[red]✗ Path does not exist. Keeping current value.[/]\n");
        }

        var hostInput = Helper.GetInput($"host (current: {newSettings.Host})");
        if (!string.IsNullOrWhiteSpace(hostInput))
            newSettings.Host = hostInput;

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

        var cacheKInput = Helper.GetInput($"cache type K (current: {newSettings.CacheTypeK})");
        if (!string.IsNullOrWhiteSpace(cacheKInput))
            newSettings.CacheTypeK = cacheKInput;

        var cacheVInput = Helper.GetInput($"cache type V (current: {newSettings.CacheTypeV})");
        if (!string.IsNullOrWhiteSpace(cacheVInput))
            newSettings.CacheTypeV = cacheVInput;

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
        var sd = settings.SamplingDefaults ?? new SamplingDefaults();

        AnsiConsole.MarkupLine("\n[cyan]Sampling Defaults:[/]");

        var tempInput = Helper.GetInput($"temperature (current: {sd.Temperature})");
        if (!string.IsNullOrWhiteSpace(tempInput) && double.TryParse(tempInput, out var temp))
            sd.Temperature = temp;

        var topKInput = Helper.GetInput($"top-k (current: {sd.TopK})");
        if (!string.IsNullOrWhiteSpace(topKInput) && int.TryParse(topKInput, out var topK))
            sd.TopK = topK;

        var topPInput = Helper.GetInput($"top-p (current: {sd.TopP})");
        if (!string.IsNullOrWhiteSpace(topPInput) && double.TryParse(topPInput, out var topP))
            sd.TopP = topP;

        var minPInput = Helper.GetInput($"min-p (current: {sd.MinP})");
        if (!string.IsNullOrWhiteSpace(minPInput) && double.TryParse(minPInput, out var minP))
            sd.MinP = minP;

        var repeatPenaltyInput = Helper.GetInput($"repeat penalty (current: {sd.RepeatPenalty})");
        if (!string.IsNullOrWhiteSpace(repeatPenaltyInput) && double.TryParse(repeatPenaltyInput, out var rp))
            sd.RepeatPenalty = rp;

        var repeatLastNInput = Helper.GetInput($"repeat last-n (current: {sd.RepeatLastN})");
        if (!string.IsNullOrWhiteSpace(repeatLastNInput) && int.TryParse(repeatLastNInput, out var rln))
            sd.RepeatLastN = rln;

        settings.SamplingDefaults = sd;
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
        }
        // Check if a model with this ID already exists
        var existingSettings = SettingsManager.GetSettings(forceReload: true);
        if (existingSettings.Models != null && existingSettings.Models.Any(m => m.Id == id))
        {
            AnsiConsole.MarkupLine($"[red]A model with ID '{id}' already exists. Please choose a different ID.[/]");
            return;
        }

        var aliasInput = Helper.GetInput("Alias (leave empty to auto-generate from GGUF filename)");
        var alias = aliasInput;
        if (string.IsNullOrEmpty(alias))
        {
            alias = Path.GetFileNameWithoutExtension(gguf);
            AnsiConsole.MarkupLine($"[dim]Auto-generated alias:[/] {alias}\n");
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

        // Get fresh settings from disk and add the model
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

        var ggufInput = Helper.GetInput($"GGUF file (current: \"{modelToEdit.GgufFileName}\")");
        if (!string.IsNullOrEmpty(ggufInput))
        {
            modelToEdit.GgufFileName = ggufInput;
        }

        var aliasInput = Helper.GetInput($"Alias (current: \"{modelToEdit.Alias}\", empty = auto-gen from GGUF filename)");
        if (!string.IsNullOrEmpty(aliasInput))
        {
            modelToEdit.Alias = aliasInput;
        }
        else if (string.IsNullOrEmpty(modelToEdit.Alias))
        {
            modelToEdit.Alias = Path.GetFileNameWithoutExtension(modelToEdit.GgufFileName);
            AnsiConsole.MarkupLine($"[dim]Auto-generated alias:[/] {modelToEdit.Alias}\n");
        }

        var ctxSizeStr = Helper.GetInput($"Context size in kilobytes (default: {modelToEdit.ContextSize / 1024})");
        if (!string.IsNullOrWhiteSpace(ctxSizeStr) && int.TryParse(ctxSizeStr, out var parsedCtx))
        {
            modelToEdit.ContextSize = parsedCtx * 1024;
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
