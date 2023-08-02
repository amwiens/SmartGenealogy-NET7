using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.Search;

/// <summary>
/// Main search view model.
/// </summary>
public partial class MainSearchViewModel : MainViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainSearchViewModel(ILogger logger,
        ISettingService settingService)
    {
        _logger = logger;
        _settingService = settingService;

        _logger?.Information("Main search view initialized");
    }
}