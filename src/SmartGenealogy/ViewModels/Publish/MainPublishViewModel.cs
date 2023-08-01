using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.Publish;

/// <summary>
/// Main publish view model.
/// </summary>
public partial class MainPublishViewModel : MainViewModelBase
{
    private readonly ILogger _logger;
    private readonly ISettingService _settingService;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainPublishViewModel(ILogger logger,
        ISettingService settingService)
    {
        _logger = logger;
        _settingService = settingService;
    }
}