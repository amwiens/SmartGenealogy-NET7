using Avalonia;
using Avalonia.Controls;

using CommunityToolkit.Mvvm.DependencyInjection;

using SmartGenealogy.Contracts;

namespace SmartGenealogy.Views;

public partial class MainWindow : Window
{
    private readonly ISettingService _settingService;

    public MainWindow()
    {
        InitializeComponent();
        _settingService = Ioc.Default.GetService<ISettingService>()!;

        this.Position = new PixelPoint(_settingService.Settings.X, _settingService.Settings.Y);

        this.Closing += (sender, e) => SaveWindowSizeAndPosition();
    }

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