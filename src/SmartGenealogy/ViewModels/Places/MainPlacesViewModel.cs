using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.Places;

/// <summary>
/// Main places view model.
/// </summary>
public partial class MainPlacesViewModel : MainViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainPlacesViewModel() : this(null, null) { }

    /// <summary>
    /// Ctor
    /// </summary>
    public MainPlacesViewModel(ILogger? logger,
        ISettingService? settingService)
    {
        _logger = logger;
        _settingService = settingService;

        _logger?.Information("Main places view initialized");
    }
}