using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.People;

/// <summary>
/// Main people view model.
/// </summary>
public partial class MainPeopleViewModel : MainViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainPeopleViewModel(ILogger logger,
        ISettingService settingService)
    {
        _logger = logger;
        _settingService = settingService;

        _logger?.Information("Main people view initialized");
    }
}