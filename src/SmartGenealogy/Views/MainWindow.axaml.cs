using System.Threading.Tasks;

using Avalonia;
using Avalonia.Controls;

using CommunityToolkit.Mvvm.DependencyInjection;

using SmartGenealogy.Contracts;

namespace SmartGenealogy.Views;

/// <summary>
/// Main window.
/// </summary>
public partial class MainWindow : Window
{
    private readonly ISettingService _settingService;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        _settingService = Ioc.Default.GetService<ISettingService>()!;
        SetupWindow().Wait();

        this.Closing += (sender, e) => SaveWindowSizeAndPosition();
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
}