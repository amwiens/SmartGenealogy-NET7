using CommunityToolkit.Mvvm.ComponentModel;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Persistence.Models;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.Places;

public partial class PlaceViewModel : PageViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    [ObservableProperty]
    private Place? _place = new();

    [ObservableProperty]
    private long? _placeId = null;

    /// <summary>
    /// Ctor
    /// </summary>
    public PlaceViewModel() : this(null, null) { }

    /// <summary>
    /// Ctor
    /// </summary>
    public PlaceViewModel(ILogger? logger,
        ISettingService? settingService)
    {
        _logger = logger;
        _settingService = settingService;

        _logger?.Information("Place view initialized");
    }
}