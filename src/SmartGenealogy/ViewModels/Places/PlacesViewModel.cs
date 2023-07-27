using System.Collections.ObjectModel;
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

public partial class PlacesViewModel : PageViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;
    private readonly IMessageBoxService? _messageBoxService;

    [ObservableProperty]
    private bool _isLoading = true;

    [ObservableProperty]
    private ObservableCollection<Place> _places = new();

    [ObservableProperty]
    private Place _selectedPlace = new();

    /// <summary>
    /// Ctor
    /// </summary>
    public PlacesViewModel() : this(null, null, null) { }

    /// <summary>
    /// Ctor
    /// </summary>
    public PlacesViewModel(ILogger? logger,
        ISettingService? settingService,
        IMessageBoxService? messageBoxService)
    {
        _logger = logger;
        _settingService = settingService;
        _messageBoxService = messageBoxService;

        LoadPlaces();
        SelectedPlace = Places[0];

        _logger?.Information("Places view initialized");
    }

    private void LoadPlaces()
    {
        Places.Clear();
        Places.Add(new Place { Name = "Munich, Cavalier, North Dakota, United States", Latitude = 92, Longitude = 94 });
        Places.Add(new Place { Name = "Langdon, Cavalier, North Dakota, United States", Latitude = 92, Longitude = 94 });
        Places.Add(new Place { Name = "Rochester, Olmsted, Minnesota, United States", Latitude = 92, Longitude = 94 });
    }

    [RelayCommand]
    private void GoToPlace()
    {
        WeakReferenceMessenger.Default.Send(new PlaceNavigationMessage(new PlaceViewModel()));
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