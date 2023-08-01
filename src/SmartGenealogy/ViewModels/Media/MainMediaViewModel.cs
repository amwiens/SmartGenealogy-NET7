using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.Media;

/// <summary>
/// Main media view model.
/// </summary>
public partial class MainMediaViewModel : MainViewModelBase
{
    private readonly ILogger _logger;
    private readonly ISettingService _settingService;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainMediaViewModel(ILogger logger,
        ISettingService settingService)
    {
        _logger = logger;
        _settingService = settingService;
    }
}