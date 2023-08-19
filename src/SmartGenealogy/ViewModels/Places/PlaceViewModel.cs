using System.Collections.ObjectModel;
using System.Threading.Tasks;

using AvaloniaEdit.Utils;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using FluentAvalonia.UI.Controls;
using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Messages;
using SmartGenealogy.Persistence.Models;
using SmartGenealogy.Services;
using SmartGenealogy.ViewModels.Base;
using SmartGenealogy.Views.Places;

namespace SmartGenealogy.ViewModels.Places;

public partial class PlaceViewModel : PageViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;
    private readonly IMessageBoxService? _messageBoxService;
    private readonly IPlaceRepository? _placeRepository;

    [ObservableProperty]
    private Place? _place = new();

    [ObservableProperty]
    private long _placeId;

    partial void OnPlaceIdChanged(long value)
    {
        LoadPlace();
    }

    public ObservableCollection<Place>? PlaceDetails { get; } = new() { };

    [ObservableProperty]
    private Place _selectedPlaceDetail = new();

    /// <summary>
    /// Ctor
    /// </summary>
    public PlaceViewModel() : this(null, null, null, null) { }

    /// <summary>
    /// Ctor
    /// </summary>
    public PlaceViewModel(ILogger? logger,
        ISettingService? settingService,
        IMessageBoxService? messageBoxService,
        IPlaceRepository? placeRepository)
    {
        _logger = logger;
        _settingService = settingService;
        _messageBoxService = messageBoxService;
        _placeRepository = placeRepository;

        _logger?.Information("Place view initialized");
    }

    /// <summary>
    /// Load place data.
    /// </summary>
    private void LoadPlace()
    {
        Place = _placeRepository!.Get(PlaceId);

        PlaceDetails?.Clear();
        PlaceDetails?.AddRange(_placeRepository!.GetPlaceDetails(PlaceId));
    }

    /// <summary>
    /// Go to place details
    /// </summary>
    [RelayCommand]
    private void GoToPlaceDetails()
    {
        WeakReferenceMessenger.Default.Send(new PlaceNavigationMessage(new PlaceNavigationData { ViewModelType = "PlaceViewModel", Id = SelectedPlaceDetail.Id }));
    }

    /// <summary>
    /// Add place details
    /// </summary>
    [RelayCommand]
    private async Task AddPlaceDetails()
    {
        var dialog = new ContentDialog()
        {
            Title = "Add a place",
            PrimaryButtonText = "OK",
            DefaultButton = ContentDialogButton.Primary,
            CloseButtonText = "Cancel",
        };

        var viewModel = new AddPlaceDetailsContentDialogViewModel(dialog);
        dialog.Content = new AddPlaceDetailsContentDialogView()
        {
            DataContext = viewModel
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            var view = dialog.Content as AddPlaceDetailsContentDialogView;
            var vm = view!.DataContext as AddPlaceDetailsContentDialogViewModel;

            if (!string.IsNullOrEmpty(vm!.UserInput))
            {
                var place = new Place { Name = vm.UserInput, MasterId = PlaceId };
                _placeRepository!.Add(place);
                LoadPlace();
            }
        }
        _logger?.Information("Place added.");
    }

    /// <summary>
    /// Delete place details
    /// </summary>
    [RelayCommand]
    private async Task DeletePlaceDetails()
    {
        await _messageBoxService!.CreateNotification($"Delete place: {SelectedPlaceDetail.Name}?");
        _placeRepository!.Delete(SelectedPlaceDetail);
        PlaceDetails?.Remove(SelectedPlaceDetail);
        _logger?.Information("Place deleted.");
    }
}