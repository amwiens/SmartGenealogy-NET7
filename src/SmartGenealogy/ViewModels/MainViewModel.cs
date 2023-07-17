using System;
using System.IO;
using System.Text.Json;

using Serilog;

using SmartGenealogy.Contracts;

namespace SmartGenealogy.ViewModels;

/// <summary>
/// Main view model class.
/// </summary>
public partial class MainViewModel : ViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    public MainViewModel() : this(null, null)
    {
    }

    /// <summary>
    /// Ctor
    /// </summary>
    public MainViewModel(ILogger? logger, ISettingService? settingService)
    {
        _logger = logger; //Ioc.Default.GetService<ILogger>()!;
        _settingService = settingService; //Ioc.Default.GetService<ISettingService>()!;

        _logger?.Information("Main Window initialized");
        AppDomain.CurrentDomain.ProcessExit += OnExit!;
    }

    /// <summary>
    /// What happens when the program is exiting.
    /// </summary>
    private void OnExit(object sender, EventArgs e)
    {
        var json = JsonSerializer.Serialize(_settingService.Settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("Settings.json", json);
        _logger?.Information("Settings saved");
    }
}