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
        public.const string EditModel      = "Edit model";
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

        var llamaPath = string.IsNullOrEmpty(settings.LlamaCppPath) ? "(empty)" as string : settings.LlamaCppPath;
        AnsiConsole.MarkupLine($"[cyan]║ llama.cpp folder:[/] {llamaPath}");
        AnsiConsole.MarkupLine($"[cyan]║ Server Port:[/] {settings.ServerPort}");
        var host = string.IsNullOrEmpty(settings.Host) ? "(empty)" as string : settings.Host;
        AnsiConsole.MarkupLine($"[cyan]║ Host:[/] {host}");
        var cacheK = string.IsNullOrEmpty(settings.CacheTypeK) ? "(empty)" as string : settings.CacheTypeK;
        AnsiConsole.MarkupLine($"[cyan]║ Cache Type K:[/] {cacheK}");
        var cacheV = string.IsNullOrEmpty(settings.CacheTypeV) ? "(empty)" as string : settings.CacheTypeV;
        AnsiConsole.MarkupLine($"[cyan]║ Cache Type V:[/] {cacheV}");

        if (settings.SamplingDefaults != null)
        {
            var sd = settings.SamplingDefaults;
            AnsiConsole.MarkupLine("\n[yellow]║ --- Sampling Defaults ---[/]");
            AnsiConsole.MarkupLine($"[dim]║ Temp: {sd.Temperature}, TopK: {sd.TopK}, TopP: {sd.TopP}, MinP: {sd.MinP}, RepeatPenalty: {sd.RepeatPenalty}, RepeatLastN: {sd.RepeatLastN}[/]");
        }

        if (settings.ServerDefaults != null)
        {
            var sd = settings.ServerDefaults;
            AnsiConsole.MarkupLine("\n[yellow]║ --- Server Defaults (Read-Only) ---[/]");
            AnsiConsole.MarkupLine($"[dim]║ Parallel: {sd.Parallel}, Prio: {sd.Prio}, FlashAttn: {sd.FlashAttn}, KVUnified: {sd.KvUnified}, LoadMode: {sd.LoadMode}, Fit: {sd.Fit}, CacheReuse: {sd.CacheReuse}, DraftPMin: {sd.DraftPMin}, LogVerbosity: {sd.LogVerbosity}, Samplers: {sd.Samplers}, ContextShift: {sd.ContextShift}, ReasoningPreserve: {sd.ReasoningPreserve}, Reasoning: {sd.Reasoning}, ReasoningBudget: {sd.ReasoningBudget}, BatchSize: {sd.BatchSize}, UbatchSize: {sd.UbatchSize}, SpecType: {sd.SpecType}[/]");
        }

        var modelFolder = string.IsNullOrEmpty(settings.ModelsFolderPath) ? "(empty)" as string : settings.ModelsFolderPath;
        AnsiConsole.MarkupLine($"\n[cyan]║ Models Folder:[/] {modelFolder}");

        if (settings.Models.Count > 0)
        {
            AnsiConsole.MarkupLine("\n[cyan]║ Models:[/]");
            for (int i = 0; i < settings.Models.Count; i++)
            {
                var m = settings.Models[i];
                string aliasStr = string.IsNullOrEmpty(m.Alias) ? "" as string : $" [green]({m.Alias})[/]";
                AnsiConsole.MarkupLine($"[cyan]║ #{i + 1}[/] {m.Id}: {m.GgufFileName}{aliasStr}");
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
        ApplicationSettings newSettings = SettingsManager.HasSettings ? SettingsManager.GetSettings() with { } : new ApplicationSettings();

        var newPath = Helper.GetInput($"llama.cpp folder (current: \"{newSettings.LlamaCppPath}\")");
        if (!string.IsNullOrEmpty(newPath))
        {
            if (File.Exists(newPath)) newSettings.LlamaCppPath = newPath;
            else AnsiConsole.MarkupLine("[red]✗ Path does not exist. Keeping current value.[/]\n");
        }

        var portStr = Helper.GetInput($"server port (current: {newSettings.ServerPort})");
        if (!string.IsNullOrWhiteSpace(portStr))
        {
            try
            {
                var port = int.Parse(portStr);
                if (port > 0 && port < 65536) newSettings.ServerPort = port;
                else AnsiConsole.MarkupLine("[red]✗ Must be number between 1-65535. Keeping current value.[/]\n");
            }
            catch { AnsiConsole.MarkupLine("[red]✗ Invalid number format. Keeping current value.[/]\n"); }
        }

        var hostInput = Helper.GetInput($"Host (current: \"{newSettings.Host}\")");
        if (!string.IsNullOrEmpty(hostInput)) newSettings.Host = hostInput;

        var cacheKInput = Helper.GetInput($"Cache Type K (current: \"{newSettings.CacheTypeK}\")");
        if (!string.IsNullOrEmpty(cacheKInput)) newSettings.CacheTypeK = cacheKInput;

        var cacheVInput = Helper.GetInput($"Cache Type V (current: \"{newSettings.CacheTypeV}\")");
        if (!string.IsNullOrEmpty(cacheVInput)) newSettings.CacheTypeV = cacheVInput;

        AnsiConsole.MarkupLine("\n[yellow]--- Sampling Defaults ---[/]");
        var sd = newSettings.SamplingDefaults ?? new SamplingDefaults();
        sd.Temperature = ParseDouble(Helper.GetInput($"Temperature (current: {sd.Temperature})"), sd.Temperature);
        sd.TopK = ParseInt(Helper.GetInput($"TopK (current: {sd.TopK})"), sd.TopK);
        sd.TopP = ParseDouble(Helper.GetInput($"TopP (current: {sd.TopP})"), sd.TopP);
        sd.MinP = ParseDouble(Helper.GetInput($"MinP (current: {sd.MinP})"), sd.MinP);
        sd.RepeatPenalty = ParseDouble(Helper.GetInput($"Repeat Penalty (current: {sd.RepeatPenalty})"), sd.RepeatPenalty);
        sd.RepeatLastN = ParseInt(Helper.GetInput($"Repeat Last N (current: {sd.RepeatLastN})"), sd.RepeatLastN);
        newSettings.SamplingDefaults = sd;

        var modelFolderInput = Helper.GetInput($"models folder path (current: \"{newSettings.ModelsFolderPath}\")");
        if (!string.IsNullOrEmpty(modelFolderInput))
        {
            if (Directory.Exists(modelFolderInput)) newSettings.ModelsFolderPath = modelFolderInput;
            else AnsiConsole.MarkupLine("[red]\\u2717 Path does not exist. Keeping current value.[/]\\n");
        }

        try
        {
            SettingsManager.Save(newSettings);
            Clear();
            ShowCurrentSettings();
            Success("Settings saved");
        }
        catch (Exception exc) { Error("Failed to save Settings", exc); }
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
        } else {
            var existingSettings = SettingsManager.GetSettings(forceReload: true);
            if (existingSettings.Models != null && existingSettings.Models.Any(m => m.Id == id))
            {
                AnsiConsole.MarkupLine($"[red]A model with ID '{id}' already exists. Please choose a different ID.[/]");
                return;
            }
        }

        int ctxSize = ParseInt(Helper.GetInput("Context size in kilobytes (default: 64)"), 64) * 1024;
        int gpuLayers = ParseInt(Helper.GetInput("GPU layers (default: 0)"), 0);
        int cpuMoE = ParseInt(Helper.GetInput("CPU MoE (empty for 0)"), 0);
        bool jinja = !string.IsNullOrEmpty(Helper.GetInput("Enable jinja? (y/n, empty=n)")) && Helper.GetInput("Enable jinja? (y/n, empty=n)").Trim().ToLowerInvariant().StartsWith('y');
        string alias = string.IsNullOrEmpty(Helper.GetInput("Alias (leave empty to auto-generate from filename)")) ? Path.GetFileNameWithoutExtension(gguf) : Helper.GetInput("Alias (leave empty to auto-generate from filename)");

        ApplicationSettings settings = SettingsManager.GetSettings(forceReload: true);
        settings.Models ??= [];
        settings.Models.Add(new ModelSettings { Id = id, GgufFileName = gguf, ContextSize = ctxSize, GpuLayers = gpuLayers, CpuMoE = cpuMoE, Jinja = jinja, Alias = alias });

        try { SettingsManager.Save(settings); Clear(); ShowCurrentSettings(); Success($"Model '{id}' added."); }
        catch (Exception exc) { Error("Failed to save model", exc); }
    }

    private void EditModel()
    {
        var settings = SettingsManager.GetSettings(forceReload: true);
        if (settings.Models == null || settings.Models.Count == 0) { AnsiConsole.MarkupLine("[red]No models configured.[/]"); return; }

        var selectedId = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Select model to edit:").AddChoices(settings.Models.Select(m => m.Id).ToArray()));
        var modelToEdit = settings.Models.FirstOrDefault(m => m.Id == selectedId);
        if (modelToEdit == null) return;

        Clear();
        AnsiConsole.MarkupLine($"[green]Editing model: {modelToEdit.Id}[/]\n");

        modelToEdit.GgufFileName = Helper.GetInput($"GGUF file (current: \"{modelToEdit.GgufFileName}\")") ?? modelToEdit.GgufFileName;
        modelToEdit.ContextSize = ParseInt(Helper.GetInput($"Context size in kilobytes (default: {modelToEdit.ContextSize / 1024})"), modelToEdit.ContextSize / 1024) * 1024;
        modelToEdit.GpuLayers = ParseInt(Helper.GetInput($"GPU layers (current: {modelToEdit.GpuLayers})"), modelToEdit.GpuLayers);
        modelToEdit.CpuMoE = ParseInt(Helper.GetInput($"CPU MoE threads (current: {modelToEdit.CpuMoE})"), modelToEdit.CpuMoE);
        modelToEdit.Jinja = !string.IsNullOrEmpty(Helper.GetInput($"Enable Jinja? (y/n, empty=n) (current: {(modelToEdit.Jinja ? "yes" : "no")})")) && Helper.GetInput($"Enable Jinja? (y/n, empty=n) (current: {(modelToEdit.Jinja ? "yes" : "no")})").Trim().ToLowerInvariant().StartsWith('y');
        modelToEdit.Alias = Helper.GetInput($"Alias (current: \"{modelToEdit.Alias}\")") ?? modelToEdit.Alias;

        try { SettingsManager.Save(settings); Success($"Model '{selectedId}' updated."); }
        catch (Exception exc) { Error("Failed to save changes", exc); }
    }

    private void RemoveModel()
    {
        var settings = SettingsManager.GetSettings();
        if (settings.Models == null || settings.Models.Count == 0) { AnsiConsole.MarkupLine("[red]No models configured.[/]"); return; }
        var selectedId = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Select model to remove:").AddChoices(settings.Models.Select(m => m.Id).ToArray()));
        if (AnsiConsole.Ask<bool>($"Are you sure you want to remove '{selectedId}'? (y/n)") )
        {
            settings.Models.RemoveAll(m => m.Id == selectedId);
            SettingsManager.Save(settings);
            Clear(); ShowCurrentSettings(); Success($"Removed model '{selectedId}'.");
        }
    }

    private static double ParseDouble(string input, double defaultValue) => double.TryParse(input, out var result) ? result : defaultValue;
    private static int ParseInt(string input, int defaultValue) => int.TryParse(input, out var result) ? result : defaultValue;
}
