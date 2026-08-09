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
        bool firstLoad = true;
        while (true)
        {
            Clear();
            AnsiConsole.MarkupLine($"[gray]Settimgs are stored in \"{SettingsManager.SettingsFilePath}\".[/]\n");

            if (firstLoad)
                ShowCurrentSettings();

            firstLoad = false;

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
        ApplicationSettings settings = SettingsManager.GetSettings();

        AnsiConsole.MarkupLine("\n[bold cyan]====== Current Settings ======[/]\n");

        var llamaPath = string.IsNullOrEmpty(settings.LlamaCppPath) ? "(empty)" : settings.LlamaCppPath;
        AnsiConsole.MarkupLine($"[cyan]llama.cpp folder:[/] {llamaPath}");
        AnsiConsole.MarkupLine($"[cyan]Server Port:[/] {settings.ServerPort}");
        var modelFolder = string.IsNullOrEmpty(settings.ModelsFolderPath) ? "(empty)" : settings.ModelsFolderPath;
        AnsiConsole.MarkupLine($"[cyan]Models Folder:[/] {modelFolder}");

        if (settings.Models.Count > 0)
        {
            AnsiConsole.MarkupLine("\n[cyan]Models:[/]");
            for (int i = 0; i < settings.Models.Count; i++)
            {
                var m = settings.Models[i];
                AnsiConsole.MarkupLine($"  [cyan]#{i + 1}[/] {m.Id}: {m.GgufFileName}");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("\n[red]No models configured.[/]");
        }

        AnsiConsole.MarkupLine("[bold cyan]=============================[/]\n");
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
        AnsiConsole.MarkupLine("\nAdd new model[/]\n");

        var gguf = Helper.GetInput("gguf file (required)");
        if (string.IsNullOrEmpty(gguf)) { AnsiConsole.MarkupLine("[red]Cancelled.[/]"); return; }

        var id = Helper.GetInput("model id (leave empty to use gguf name)");
        if (string.IsNullOrEmpty(id)) {
            id = Path.GetFileNameWithoutExtension(gguf);
            AnsiConsole.MarkupLine($"[dim]Using GGUF name as ID:[/] {id}\n");
        }

        var ctxSizeStr = Helper.GetInput("context size in kilobytes (default: 64)");
        var ctxSize = (string.IsNullOrWhiteSpace(ctxSizeStr) ? 64 : int.Parse(ctxSizeStr)) * 1024;

        var gpuLayersStr = Helper.GetInput("gpu layers (default: 0)");
        var gpuLayers = string.IsNullOrWhiteSpace(gpuLayersStr) ? 0 : int.Parse(gpuLayersStr);

        var cpuMoEInput = Helper.GetInput("cpu moe threads (empty for 0)");
        var cpuMoE = string.IsNullOrWhiteSpace(cpuMoEInput) ? 0 : int.Parse(cpuMoEInput);

        var jinjaInput = Helper.GetInput("enable jinja? (y/n, empty=n)");
        bool jinja = false;
        if (!string.IsNullOrEmpty(jinjaInput))
        {
            jinja = jinjaInput.ToLower().StartsWith('y');
        }

        var settings = SettingsManager.GetSettings();

        // TODO: see how to update the settings with the new model
        settings.Models ??= [];
        settings.Models.Add(new ModelSettings
        {
            Id = id,
            GgufFileName = gguf,
            ContextSize = ctxSize,
            GpuLayers = gpuLayers,
            CpuMoE = cpuMoE,
            Jinja = jinja
        });

        SettingsManager.Save(settings);

        Clear();
        ShowCurrentSettings();

        Success($"Model '{id}' added.");
    }

    private void EditModel()
    {
        throw new Exception("not implemented");

        /*
        var settings = SettingsManager.Instance.LoadCurrent();
        if (settings == null || settings.Models.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No models to edit.[/]");
            return;
        }

        var ids = settings.Models.Select(m => m.Id).ToList();
        var selectedId = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold]Select model to edit:[/]")
                .AddChoices(ids));

        var model = settings.Models.FirstOrDefault(m => m.Id == selectedId);
        if (model == null) return;

        AnsiConsole.MarkupLine($"\n[green]Editing model: {model.Id}[/]");
        AnsiConsole.MarkupLine($"[green]Current GGUF filename:[/] {model.GgufFileName}");
        AnsiConsole.MarkupLine($"[green]Current context size:[/] {model.ContextSize}");
        AnsiConsole.MarkupLine($"[green]Current GPU layers:[/] {model.GpuLayers}");

        AnsiConsole.MarkupLine("\n[bold]Enter new GGUF filename (empty to keep current):[/]");
        var gguf = Console.ReadLine();
        if (!string.IsNullOrEmpty(gguf))
        {
            model.GgufFileName = gguf;
        }

        AnsiConsole.MarkupLine("[bold]Enter new context size (empty to keep current):[/]");
        var ctxSizeStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(ctxSizeStr))
        {
            try
            {
                model.ContextSize = int.Parse(ctxSizeStr);
            }
            catch
            {
                AnsiConsole.MarkupLine("[red]Invalid number. Keeping current value.[/]");
            }
        }

        AnsiConsole.MarkupLine("[bold]Enter new GPU layers (empty to keep current):[/]");
        var gpuLayersStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(gpuLayersStr))
        {
            try
            {
                model.GpuLayers = int.Parse(gpuLayersStr);
            }
            catch
            {
                AnsiConsole.MarkupLine("[red]Invalid number. Keeping current value.[/]");
            }
        }

        SettingsManager.Instance.Save(settings);
        AnsiConsole.MarkupLine("[green]✓ Model updated.[/]");
        */
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
