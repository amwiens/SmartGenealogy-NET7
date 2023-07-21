using System;
using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

using CommunityToolkit.Mvvm.DependencyInjection;

using FluentAvalonia.Styling;
using FluentAvalonia.UI.Windowing;

using SmartGenealogy.Contracts;

namespace SmartGenealogy.Views;

/// <summary>
/// Main window.s
/// </summary>
public partial class MainWindow : AppWindow
{
    private readonly ISettingService _settingService;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        _settingService = Ioc.Default.GetService<ISettingService>()!;
        TitleBar.ExtendsContentIntoTitleBar = true;
        TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;

        SetupWindow().Wait();

        this.Closing += (sender, e) => SaveWindowSizeAndPosition();
        Application.Current!.ActualThemeVariantChanged += OnActualThemeVariantChanged;
    }

    /// <summary>
    /// Setup main window.
    /// </summary>
    /// <returns>Task</returns>
    private async Task SetupWindow()
    {
        await _settingService.InitializeSettings();
        this.Position = new PixelPoint(_settingService.Settings.X, _settingService.Settings.Y);
        this.Width = _settingService.Settings.Width;
        this.Height = _settingService.Settings.Height;
        this.WindowState = _settingService.Settings.IsMaximized ? WindowState.Maximized : WindowState.Normal;
        Application.Current!.RequestedThemeVariant = _settingService.Settings.CurrentTheme switch
        {
            "Dark" => ThemeVariant.Dark,
            "Light" => ThemeVariant.Light,
            _ => ThemeVariant.Default
        };
        if (_settingService.Settings.AppAccentColorA != 0 && _settingService.Settings.AppAccentColorR != 0 && _settingService.Settings.AppAccentColorG != 0 && _settingService.Settings.AppAccentColorB != 0)
        {
            (App.Current!.Styles[0] as FluentAvaloniaTheme)!.CustomAccentColor = Color.FromArgb(_settingService.Settings.AppAccentColorA, _settingService.Settings.AppAccentColorR, _settingService.Settings.AppAccentColorG, _settingService.Settings.AppAccentColorB);
        }
        else
        {
            (App.Current!.Styles[0] as FluentAvaloniaTheme)!.CustomAccentColor = default;
        }
    }

    /// <summary>
    /// Saves window settings to settings class.
    /// </summary>
    private void SaveWindowSizeAndPosition()
    {
        _settingService.Settings.IsMaximized = this.WindowState == WindowState.Maximized;
        this.WindowState = WindowState.Normal;
        _settingService.Settings.Width = this.Width;
        _settingService.Settings.Height = this.Height;
        _settingService.Settings.X = this.Position.X;
        _settingService.Settings.Y = this.Position.Y;
    }

    private void OnActualThemeVariantChanged(object? sender, EventArgs e)
    {
        if (IsWindows11)
        {
            ClearValue(BackgroundProperty);
            ClearValue(TransparencyBackgroundFallbackProperty);
        }
    }
}