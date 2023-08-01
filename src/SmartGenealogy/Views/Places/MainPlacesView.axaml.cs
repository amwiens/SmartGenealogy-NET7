using Avalonia.Controls;

using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;

using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Media.Animation;
using FluentAvalonia.UI.Navigation;

using Serilog;

using SmartGenealogy.Messages;
using SmartGenealogy.Services;
using SmartGenealogy.ViewModels.Places;
using SmartGenealogy.Views.Base;

namespace SmartGenealogy.Views.Places;

/// <summary>
/// Main place view.
/// </summary>
public partial class MainPlacesView : MainViewBase, IRecipient<PlaceNavigationMessage>
{
    private readonly ILogger? _logger;
    private Frame _frame;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainPlacesView()
    {
        InitializeComponent();
        _logger = Ioc.Default.GetService<ILogger>();

        _frame = this.FindControl<Frame>("PlacesFrame")!;
        _frame.NavigationPageFactory = new PlacePageNavigationFactory();

        WeakReferenceMessenger.Default.Register<PlaceNavigationMessage>(this);

        WeakReferenceMessenger.Default.Send(new PlaceNavigationMessage(new PlaceNavigationData { ViewModelType = "PlacesViewModel" }));
    }

    /// <summary>
    /// Receive open file changed messages.
    /// </summary>
    /// <param name="message">Message received.</param>
    public void Receive(PlaceNavigationMessage message)
    {
        _logger?.Information("Place navigation message received: {Message}", message.Value);

        var frameNavigationOptions = new FrameNavigationOptions { TransitionInfoOverride = new SlideNavigationTransitionInfo(), IsNavigationStackEnabled = true };

        switch (message.Value.ViewModelType)
        {
            case "PlacesViewModel":
                var placesViewModel = Ioc.Default.GetService<PlacesViewModel>();
                _frame.NavigateFromObject(placesViewModel, frameNavigationOptions);
                break;

            case "PlaceViewModel":
                var placeViewModel = Ioc.Default.GetService<PlaceViewModel>();
                placeViewModel!.PlaceId = message.Value.Id;
                _frame.NavigateFromObject(placeViewModel, frameNavigationOptions);
                break;
        }
    }
}