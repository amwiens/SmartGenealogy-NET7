using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Media.Animation;
using FluentAvalonia.UI.Navigation;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Messages;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.Places;

/// <summary>
/// Main places view model.
/// </summary>
public partial class MainPlacesViewModel : MainViewModelBase, IRecipient<PlaceNavigationMessage>
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    [ObservableProperty]
    private ViewModelBase? _currentPage;

    [ObservableProperty]
    private Frame? _frame = new();

    /// <summary>
    /// Ctor
    /// </summary>
    public MainPlacesViewModel() : this(null, null) { }

    /// <summary>
    /// Ctor
    /// </summary>
    public MainPlacesViewModel(ILogger? logger,
        ISettingService? settingService)
    {
        _logger = logger;
        _settingService = settingService;

        WeakReferenceMessenger.Default.Register<PlaceNavigationMessage>(this);

        Title = "Places";
        _logger?.Information("Main Places view initialized");
    }

    /// <summary>
    /// Receive open file changed messages.
    /// </summary>
    /// <param name="message"></param>
    public void Receive(PlaceNavigationMessage message)
    {
        _logger?.Information("Place navigation message received: {Message}", message.Value);
        switch (message.Value.ViewModelType)
        {
            case "PlacesViewModel":
                var placesViewModel = Ioc.Default.GetService<PlacesViewModel>();
                Frame?.NavigateFromObject(placesViewModel, new FrameNavigationOptions { TransitionInfoOverride = new SlideNavigationTransitionInfo() });
                break;

            case "PlaceViewModel":
                var placeViewModel = Ioc.Default.GetService<PlaceViewModel>();
                placeViewModel!.PlaceId = message.Value.Id;
                Frame?.NavigateFromObject(placeViewModel, new FrameNavigationOptions { TransitionInfoOverride = new SlideNavigationTransitionInfo() });
                break;
        }
    }

    [RelayCommand]
    private void Place()
    {
        WeakReferenceMessenger.Default.Send(new PlaceNavigationMessage(new PlaceNavigationData { ViewModelType = "PlaceViewModel" }));
    }

    private void Navigate<TViewModel>() where TViewModel : ViewModelBase
    {
        var viewLocator = new ViewLocator();
        var control = viewLocator.Build(Ioc.Default.GetService<TViewModel>());
        Frame?.Navigate(control.GetType(), null, new SlideNavigationTransitionInfo());
        //Frame.NavigateFromObject(TViewModel)
    }

    [RelayCommand]
    private void GoForward()
    {
        Frame?.GoForward();
    }

    [RelayCommand]
    private void GoBack()
    {
        Frame?.GoBack(new SlideNavigationTransitionInfo());
    }
}