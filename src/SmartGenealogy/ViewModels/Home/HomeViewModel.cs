using Serilog;

using SmartGenealogy.Contracts;

namespace SmartGenealogy.ViewModels.Home;

/// <summary>
/// Home view model.
/// </summary>
public class HomeViewModel : ViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    /// <summary>
    /// Ctor
    /// </summary>
    public HomeViewModel(ILogger logger,
        ISettingService settingService)
    {
        _logger = logger;
        _settingService = settingService;

        _logger?.Information("Home view initialized");
    }
}