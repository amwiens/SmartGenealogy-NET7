using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Models;

namespace SmartGenealogy.Services;

/// <summary>
/// Settings service.
/// </summary>
public class SettingService : ISettingService
{
    private readonly ILogger _logger;

    /// <summary>
    /// Ctor
    /// </summary>
    public SettingService(ILogger logger)
    {
        _logger = logger; //Ioc.Default.GetService<ILogger>()!;
        LoadSavedSetting().Wait();
    }

    /// <inheritdoc/>
    public Settings Settings { get; private set; }

    /// <inheritdoc/>
    public async Task InitializeSettings()
    {
        _logger.Information("Initializing settings...");
        try
        {
            if (!File.Exists("Settings.json"))
            {
                _logger.Error("Settings.json not found, creating new one");
                var newSettings = new Settings
                {
                    Width = 1300,
                    Height = 840,

                    X = 5,
                    Y = 5,
                };
                var json = JsonSerializer.Serialize(newSettings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("Settings.json", json);
            }

            var text = File.ReadAllText("Settings.json");
            var settings = JsonSerializer.Deserialize<Settings>(text)!;

            Settings = settings.Clone();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error occurred while initializing settings");
        }
    }

    /// <summary>
    /// Loads data from the saved settings file.
    /// </summary>
    /// <returns>Settings</returns>
    private async Task LoadSavedSetting()
    {
        if (!File.Exists("Settings.json"))
            return;
        var text = File.ReadAllText("Settings.json");
        var settings = JsonSerializer.Deserialize<Settings>(text)!;
        _logger.Information("Saved setting loaded from Settings.json");

        Settings = settings.Clone();
    }
}