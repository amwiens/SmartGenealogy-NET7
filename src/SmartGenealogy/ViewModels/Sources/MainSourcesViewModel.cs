using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.Sources;

/// <summary>
/// Main sources view model.
/// </summary>
public partial class MainSourcesViewModel : MainViewModelBase
{
    private readonly ILogger _logger;
    private readonly ISettingService _settingService;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainSourcesViewModel(ILogger logger,
        ISettingService settingService)
    {
        _logger = logger;
        _settingService = settingService;

        Title = "Sources";
    }
}