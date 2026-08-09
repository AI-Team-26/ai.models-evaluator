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
        public const string Exit           = "Exit";
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
                    //.Title("\nSelect an option:")
                    .AddChoices(Menu.EditSettings, Menu.AddModel, Menu.EditModel, Menu.RemoveModel, Menu.Exit));

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
                case "Exit":
                    //SaveAndExit();
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

        AnsiConsole.MarkupLine($"\nEnter the llama.cpp folder (leave empty to keep current \"{newSettings.LlamaCppPath}\"):");
        var newPath = Console.ReadLine();

        if (!string.IsNullOrEmpty(newPath))
        {
            if (File.Exists(newPath))
                newSettings.LlamaCppPath = newPath;
            else
                AnsiConsole.MarkupLine("[red]✗ Path does not exist. Keeping current value.[/]\n");
        }

        AnsiConsole.MarkupLine($"Enter the server port (leave empty to keep current \"{newSettings.ServerPort}\"):");
        var portStr = Console.ReadLine();

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

        AnsiConsole.MarkupLine($"Enter the models folder path (leave empty to keep current \"{newSettings.ModelsFolderPath}\"):");
        var modelFolderInput = Console.ReadLine();

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
        AnsiConsole.MarkupLine("\n[dim]Adding new model configuration...[/]\n");

        AnsiConsole.MarkupLine("GGUF file (required):");
        var gguf = Console.ReadLine();
        if (string.IsNullOrEmpty(gguf)) { AnsiConsole.MarkupLine("[red]Cancelled.[/]"); return; }

        AnsiConsole.MarkupLine("Model ID (required, leave empty to use GGUF file):");
        var id = Console.ReadLine();
        if (string.IsNullOrEmpty(id)) {
            id = Path.GetFileNameWithoutExtension(gguf);
            AnsiConsole.MarkupLine($"[dim]Using GGUF name as ID:[/] {id}\n");
        }

        AnsiConsole.MarkupLine("Context size in Kilobyte (default: 64, empty for default):");
        var ctxSizeStr = Console.ReadLine();
        var ctxSize = (string.IsNullOrWhiteSpace(ctxSizeStr) ? 64 : int.Parse(ctxSizeStr)) * 1024;

        AnsiConsole.MarkupLine("GPU layers (default: 0, empty for default):");
        var gpuLayersStr = Console.ReadLine();
        var gpuLayers = string.IsNullOrWhiteSpace(gpuLayersStr) ? 0 : int.Parse(gpuLayersStr);

        AnsiConsole.MarkupLine("CPU MoE threads (empty for 0):");
        var cpuMoEInput = Console.ReadLine();
        var cpuMoE = string.IsNullOrWhiteSpace(cpuMoEInput) ? 0 : int.Parse(cpuMoEInput);

        AnsiConsole.MarkupLine("Enable Jinja? (y/n, empty=n):");
        var jinjaInput = Console.ReadLine();
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
        throw new Exception("not implemented");
        /*
        var settings = SettingsManager.Instance.LoadCurrent();
        if (settings == null || settings.Models.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No models to remove.[/]");
            return;
        }

        var ids = settings.Models.Select(m => m.Id).ToList();
        var selectedId = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold]Select model to remove:[/]")
                .AddChoices(ids));

        settings.Models.RemoveAll(m => m.Id == selectedId);
        SettingsManager.Instance.Save(settings);
        AnsiConsole.MarkupLine($"[green]✓ Removed model '[cyan]{selectedId}[/]'[/].");
        */
    }
}
