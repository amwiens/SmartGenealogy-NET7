using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

using FluentAvalonia.UI.Controls;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.Places;

/// <summary>
/// Main places view model.
/// </summary>
public partial class MainPlacesViewModel : MainViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    [ObservableProperty]
    private bool _isVisible;

    [ObservableProperty]
    private INavigationService? _navigationService;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainPlacesViewModel() : this(null, null, null) { }

    /// <summary>
    /// Ctor
    /// </summary>
    public MainPlacesViewModel(ILogger? logger,
        ISettingService? settingService,
        INavigationService? navigationService)
    {
        _logger = logger;
        _settingService = settingService;
        _navigationService = navigationService;

        //NavigationService!.CurrentView = Ioc.Default.GetService<PlacesViewModel>()!;
        //NavigationService?.NavigateTo<PlacesViewModel>();

        Title = "Places";
        _logger?.Information("Main Places view initialized");
    }

    //[RelayCommand]
    //public void Navigate()
    //{
    //    NavigationService?.NavigateTo<PlacesViewModel>();
    //}
}