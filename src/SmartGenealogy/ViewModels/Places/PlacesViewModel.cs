using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Messages;
using SmartGenealogy.Persistence.Models;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.Places;

public partial class PlacesViewModel : ViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;
    private readonly IMessageBoxService? _messageBoxService;
    private readonly IDataRepository<Place>? _placeRepository;

    [ObservableProperty]
    private bool _isLoading = true;

    //[ObservableProperty]
    public ObservableCollection<Place> Places { get; private set; }

    [ObservableProperty]
    private Place _selectedPlace = new();

    /// <summary>
    /// Ctor
    /// </summary>
    public PlacesViewModel() : this(null, null, null, null) { }

    /// <summary>
    /// Ctor
    /// </summary>
    public PlacesViewModel(ILogger? logger,
        ISettingService? settingService,
        IMessageBoxService? messageBoxService,
        IDataRepository<Place>? placeRepository)
    {
        _logger = logger;
        _settingService = settingService;
        _messageBoxService = messageBoxService;
        _placeRepository = placeRepository;

        if (placeRepository != null)
        {
            LoadPlaces();
            SelectedPlace = Places![0];
        }

        _logger?.Information("Places view initialized");
    }

    private void LoadPlaces()
    {
        //_placeRepository?.Add(new Place { Name = "Munich, Cavalier, North Dakota, United States", Latitude = 48.669516m, Longitude = -98.835677m });
        //_placeRepository?.Add(new Place { Name = "Langdon, Cavalier, North Dakota, United States", Latitude = 48.761696m, Longitude = -98.371780m });
        //_placeRepository?.Add(new Place { Name = "Rochester, Olmsted, Minnesota, United States", Latitude = 44.0234m, Longitude = -92.46295m });
        Places = new ObservableCollection<Place>(_placeRepository!.GetAll().ToList());
    }

    [RelayCommand]
    private void GoToPlace()
    {
        WeakReferenceMessenger.Default.Send(new PlaceNavigationMessage(new PlaceNavigationData { ViewModelType = "PlaceViewModel", Id = SelectedPlace.Id }));
    }

    [RelayCommand]
    private async Task AddPlace()
    {
        await _messageBoxService!.CreateNotification("This is a test!");
    }

    [RelayCommand]
    private void DoubleTapped()
    {

    }
}