using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.Tools;

/// <summary>
/// Main tools view model.
/// </summary>
public partial class MainToolsViewModel : MainViewModelBase
{
    private readonly ILogger _logger;
    private readonly ISettingService _settingService;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainToolsViewModel(ILogger logger,
        ISettingService settingService)
    {
        _logger = logger;
        _settingService = settingService;
    }
}