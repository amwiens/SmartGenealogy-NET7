using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

using Serilog;

using SmartGenealogy.Contracts;

namespace SmartGenealogy.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly ILogger _logger;
    private readonly ISettingService _settingService;

    [ObservableProperty]
    private double _windowWidth;

    [ObservableProperty]
    private double _windowHeight;

    [ObservableProperty]
    private PixelPoint _windowPosition;

    [ObservableProperty]
    private WindowState _windowState;

    public MainViewModel()//ILogger logger, ISettingService settingService)
    {
        //_logger = logger;
        //_settingService = settingService;
        _logger = Ioc.Default.GetService<ILogger>()!;
        _settingService = Ioc.Default.GetService<ISettingService>()!;
        Initialize().Wait();

        _logger.Information("Main Window initialized");
        AppDomain.CurrentDomain.ProcessExit += OnExit!;
    }

    private async Task Initialize()
    {
        await _settingService.InitializeSettings();
        WindowWidth = _settingService.Settings.Width;
        WindowHeight = _settingService.Settings.Height;
        WindowPosition = new PixelPoint(_settingService.Settings.X, _settingService.Settings.Y);
        WindowState = _settingService.Settings.IsMaximized ? WindowState.Maximized : WindowState.Normal;
    }

    private void OnExit(object sender, EventArgs e)
    {
        var json = JsonSerializer.Serialize(_settingService.Settings, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("Settings.json", json);
        _logger.Information("Settings saved");
    }
}