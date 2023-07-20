using System.Collections.ObjectModel;

using Avalonia;
using Avalonia.Styling;

using CommunityToolkit.Mvvm.ComponentModel;

using Serilog;

using SmartGenealogy.Contracts;

namespace SmartGenealogy.ViewModels.Settings;

/// <summary>
/// Display settings view model.
/// </summary>
public partial class DisplaySettingsViewModel : ViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    [ObservableProperty]
    private ThemeVariant _currentAppTheme = Application.Current?.ActualThemeVariant!;

    [ObservableProperty]
    private ObservableCollection<ThemeVariant> _appThemes = new ObservableCollection<ThemeVariant> { ThemeVariant.Dark, ThemeVariant.Light };

    partial void OnCurrentAppThemeChanged(ThemeVariant? oldValue, ThemeVariant newValue)
    {
        if (oldValue != newValue && Application.Current?.ActualThemeVariant != newValue && newValue is not null)
        {
            _logger?.Information("Application theme changed to {Theme}", newValue.Key.ToString());
            Application.Current.RequestedThemeVariant = newValue;
            _settingService.Settings.CurrentTheme = newValue.Key.ToString();
        }
    }

    ///// <summary>
    ///// Ctor
    ///// </summary>
    //public DisplaySettingsViewModel() : this(null, null) { }

    /// <summary>
    /// Ctor
    /// </summary>
    public DisplaySettingsViewModel(ILogger? logger,
        ISettingService? settingService)
    {
        _logger = logger;
        _settingService = settingService;


        _logger?.Information("Display settings view initialized");
    }
}