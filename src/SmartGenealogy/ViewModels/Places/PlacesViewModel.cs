using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Avalonia.Collections;

using AvaloniaEdit.Utils;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using FluentAvalonia.UI.Controls;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Messages;
using SmartGenealogy.Persistence.Models;
using SmartGenealogy.Views.Places;

namespace SmartGenealogy.ViewModels.Places;

public partial class PlacesViewModel : ViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;
    private readonly IMessageBoxService? _messageBoxService;
    private readonly IDataRepository<Place>? _placeRepository;

    [ObservableProperty]
    private bool _isLoading = true;

    public ObservableCollection<Place>? Places { get; } = new() { };

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
            if (Places!.Any())
            {
                SelectedPlace = Places![0];
            }
        }

        _logger?.Information("Places view initialized");
    }

    /// <summary>
    /// Load places list
    /// </summary>
    private void LoadPlaces()
    {
        Places?.Clear();
        Places?.AddRange(_placeRepository!.GetAll().ToList().OrderBy(x => x.Name));
    }

    /// <summary>
    /// Go to place
    /// </summary>
    [RelayCommand]
    private void GoToPlace()
    {
        WeakReferenceMessenger.Default.Send(new PlaceNavigationMessage(new PlaceNavigationData { ViewModelType = "PlaceViewModel", Id = SelectedPlace.Id }));
    }

    [RelayCommand]
    private async Task AddPlace()
    {
        var dialog = new ContentDialog()
        {
            Title = "Add a place",
            PrimaryButtonText = "OK",
            DefaultButton = ContentDialogButton.Primary,
            CloseButtonText = "Cancel",
        };

        var viewModel = new AddPlaceContentDialogViewModel(dialog);
        dialog.Content = new AddPlaceContentDialogView()
        {
            DataContext = viewModel
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            var view = dialog.Content as AddPlaceContentDialogView;
            var vm = view!.DataContext as AddPlaceContentDialogViewModel;

            if (!string.IsNullOrEmpty(vm!.UserInput))
            {
                var place = new Place { Name = vm.UserInput };
                _placeRepository!.Add(place);
                LoadPlaces();
            }
        }
        _logger?.Information("Place added.");
    }

    [RelayCommand]
    private async Task DeletePlace()
    {
        await _messageBoxService!.CreateNotification($"Delete place: {SelectedPlace.Name}?");
        _placeRepository!.Delete(SelectedPlace);
        Places?.Remove(SelectedPlace);
        _logger?.Information("Place deleted.");
    }
}