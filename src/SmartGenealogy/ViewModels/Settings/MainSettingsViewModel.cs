using Avalonia;

using CommunityToolkit.Mvvm.ComponentModel;

using Serilog;

using SmartGenealogy.Contracts;

namespace SmartGenealogy.ViewModels.Settings;

public partial class MainSettingsViewModel : ViewModelBase
{
    private readonly ILogger _logger;
    private readonly ISettingService _settingService;

    [ObservableProperty]
    private string _currentVersion = typeof(Controls.PageHeaderControl).Assembly.GetName().Version?.ToString()!;

    [ObservableProperty]
    private string _currentAvaloniaVersion = typeof(Application).Assembly.GetName().Version?.ToString()!;

    public MainSettingsViewModel(ILogger logger,
        ISettingService settingService)
    {
        _logger = logger;
        _settingService = settingService;
    }
}