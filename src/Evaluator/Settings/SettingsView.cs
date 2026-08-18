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

        var inv = System.Globalization.CultureInfo.InvariantCulture;

        var llamaPath = string.IsNullOrEmpty(settings.LlamaCppPath) ? "(empty)" : settings.LlamaCppPath;
        AnsiConsole.MarkupLine($"[cyan]║ llama.cpp folder:[/] {llamaPath}");
        AnsiConsole.MarkupLine($"[cyan]║ Host:[/] {settings.Host}");
        AnsiConsole.MarkupLine($"[cyan]║ Server Port:[/] {settings.ServerPort}");
        AnsiConsole.MarkupLine($"[cyan]║ Cache Type K/V:[/] {settings.CacheTypeK} / {settings.CacheTypeV}");
        var modelFolder = string.IsNullOrEmpty(settings.ModelsFolderPath) ? "(empty)" : settings.ModelsFolderPath;
        AnsiConsole.MarkupLine($"[cyan]║ Models Folder:[/] {modelFolder}");

        var sd = settings.SamplingDefaults ?? new SamplingDefaults();
        AnsiConsole.MarkupLine("\n[cyan]║ Sampling Defaults:[/]");
        AnsiConsole.MarkupLine($"[cyan]║   temperature:[/] {sd.Temperature.ToString(inv)}   [cyan]top-k:[/] {sd.TopK}   [cyan]top-p:[/] {sd.TopP.ToString(inv)}");
        AnsiConsole.MarkupLine($"[cyan]║   min-p:[/] {sd.MinP.ToString(inv)}   [cyan]repeat penalty:[/] {sd.RepeatPenalty.ToString(inv)}   [cyan]repeat last-n:[/] {sd.RepeatLastN}");

        var srv = settings.ServerDefaults ?? new ServerDefaults();
        AnsiConsole.MarkupLine("\n[cyan]║ Server Defaults [/dim](readonly)[/]:");
        AnsiConsole.MarkupLine($"[cyan]║   parallel:[/] {srv.Parallel}   [cyan]prio:[/] {srv.Prio}   [cyan]flash-attn:[/] {srv.FlashAttn}   [cyan]kv-unified:[/] {srv.KvUnified}");
        AnsiConsole.MarkupLine($"[cyan]║   load-mode:[/] {srv.LoadMode}   [cyan]fit:[/] {srv.Fit}   [cyan]cache-reuse:[/] {srv.CacheReuse}   [cyan]draft-p-min:[/] {srv.DraftPMIn.ToString(inv)}");
        AnsiConsole.MarkupLine($"[cyan]║   log-verbosity:[/] {srv.LogVerbosity}   [cyan]samplers:[/] {srv.Samplers}");
        AnsiConsole.MarkupLine($"[cyan]║   context-shift:[/] {srv.ContextShift}   [cyan]reasoning:[/] {srv.Reasoning} (preserve: {srv.ReasoningPreserve}, budget: {srv.ReasoningBudget})");
        AnsiConsole.MarkupLine($"[cyan]║   batch-size:[/] {srv.BatchSize}   [cyan]ubatch-size:[/] {srv.UbatchSize}   [cyan]spec-type:[/] {srv.SpecType}");

        if (settings.Models.Count > 0)
        {
            AnsiConsole.MarkupLine("\n[cyan]║ Models:[/]");
            for (int i = 0; i < settings.Models.Count; i++)
            {
                var m = settings.Models[i];
                var alias = string.IsNullOrEmpty(m.Alias) ? Path.GetFileNameWithoutExtension(m.GgufFileName) : m.Alias;
                AnsiConsole.MarkupLine($"[cyan]║ #{i + 1}[/] {m.Id}: {m.GgufFileName}  [dim](alias: {alias})[/]");
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

        // App-level editable server options
        var hostInput = Helper.GetInput($"server host (current: {newSettings.Host})");
        if (!string.IsNullOrWhiteSpace(hostInput))
            newSettings.Host = hostInput.Trim();

        var cacheKInput = Helper.GetInput($"cache-type-k (current: {newSettings.CacheTypeK})");
        if (!string.IsNullOrWhiteSpace(cacheKInput))
            newSettings.CacheTypeK = cacheKInput.Trim();

        var cacheVInput = Helper.GetInput($"cache-type-v (current: {newSettings.CacheTypeV})");
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

    /// <summary>
    /// Prompt for each sampling default; empty input keeps the current value.
    /// </summary>
    private static void EditSamplingDefaults(ApplicationSettings s)
    {
        var sd = s.SamplingDefaults ?? new SamplingDefaults();

        var tempStr = Helper.GetInput($"temperature (current: {sd.Temperature.ToString(System.Globalization.CultureInfo.InvariantCulture)})");
        if (!string.IsNullOrWhiteSpace(tempStr) && double.TryParse(tempStr, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var t))
            sd.Temperature = t;
        else if (!string.IsNullOrWhiteSpace(tempStr))
            AnsiConsole.MarkupLine("[red]✗ Invalid number. Keeping current value.[/]\n");

        var topKStr = Helper.GetInput($"top-k (current: {sd.TopK})");
        if (!string.IsNullOrWhiteSpace(topKStr) && int.TryParse(topKStr, out var tk))
            sd.TopK = tk;
        else if (!string.IsNullOrWhiteSpace(topKStr))
            AnsiConsole.MarkupLine("[red]✗ Invalid number. Keeping current value.[/]\n");

        var topPStr = Helper.GetInput($"top-p (current: {sd.TopP.ToString(System.Globalization.CultureInfo.InvariantCulture)})");
        if (!string.IsNullOrWhiteSpace(topPStr) && double.TryParse(topPStr, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var tp))
            sd.TopP = tp;
        else if (!string.IsNullOrWhiteSpace(topPStr))
            AnsiConsole.MarkupLine("[red]✗ Invalid number. Keeping current value.[/]\n");

        var minPStr = Helper.GetInput($"min-p (current: {sd.MinP.ToString(System.Globalization.CultureInfo.InvariantCulture)})");
        if (!string.IsNullOrWhiteSpace(minPStr) && double.TryParse(minPStr, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var mp))
            sd.MinP = mp;
        else if (!string.IsNullOrWhiteSpace(minPStr))
            AnsiConsole.MarkupLine("[red]✗ Invalid number. Keeping current value.[/]\n");

        var rpStr = Helper.GetInput($"repeat penalty (current: {sd.RepeatPenalty.ToString(System.Globalization.CultureInfo.InvariantCulture)})");
        if (!string.IsNullOrWhiteSpace(rpStr) && double.TryParse(rpStr, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var rp))
            sd.RepeatPenalty = rp;
        else if (!string.IsNullOrWhiteSpace(rpStr))
            AnsiConsole.MarkupLine("[red]✗ Invalid number. Keeping current value.[/]\n");

        var rlnStr = Helper.GetInput($"repeat last-n (current: {sd.RepeatLastN})");
        if (!string.IsNullOrWhiteSpace(rlnStr) && int.TryParse(rlnStr, out var rln))
            sd.RepeatLastN = rln;
        else if (!string.IsNullOrWhiteSpace(rlnStr))
            AnsiConsole.MarkupLine("[red]✗ Invalid number. Keeping current value.[/]\n");

        s.SamplingDefaults = sd;
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

        // Alias — auto-generated from GGUF filename when left empty
        var aliasInput = Helper.GetInput("Alias (leave empty to use gguf name)").Trim();
        if (aliasInput.Length == 0)
        {
            aliasInput = Path.GetFileNameWithoutExtension(gguf);
            AnsiConsole.MarkupLine($"[dim]Using GGUF name as alias:[/] {aliasInput}\n");
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
            Alias = aliasInput
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

        var aliasCurrent = string.IsNullOrEmpty(modelToEdit.Alias)
            ? "(auto: gguf name)"
            : modelToEdit.Alias;
        var aliasInput = Helper.GetInput($"Alias (current: {aliasCurrent}, empty=auto from gguf name)").Trim();
        modelToEdit.Alias = aliasInput;

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
