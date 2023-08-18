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
    private readonly IDataRepository<Place>? _placeRepository;

    [ObservableProperty]
    private Place? _place = new();

    [ObservableProperty]
    private long? _placeId = null;

    partial void OnPlaceIdChanged(long? value)
    {
        LoadPlace();
    }

    /// <summary>
    /// Ctor
    /// </summary>
    public PlaceViewModel() : this(null, null, null) { }

    /// <summary>
    /// Ctor
    /// </summary>
    public PlaceViewModel(ILogger? logger,
        ISettingService? settingService,
        IDataRepository<Place>? placeRepository)
    {
        _logger = logger;
        _settingService = settingService;
        _placeRepository = placeRepository;

        _logger?.Information("Place view initialized");
    }

    /// <summary>
    /// Load place data.
    /// </summary>
    private void LoadPlace()
    {
        if (PlaceId is not null)
        {
            Place = _placeRepository!.Get((long)PlaceId!);
        }
    }
}