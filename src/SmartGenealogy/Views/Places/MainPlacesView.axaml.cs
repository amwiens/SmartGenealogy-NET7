using Avalonia;
using Avalonia.Controls;

using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;

using FluentAvalonia.UI.Controls;

using SmartGenealogy.Messages;
using SmartGenealogy.Services;
using SmartGenealogy.ViewModels.Places;

namespace SmartGenealogy.Views.Places;

/// <summary>
/// Main place view.
/// </summary>
public partial class MainPlacesView : UserControl
{
    /// <summary>
    /// Ctor
    /// </summary>
    public MainPlacesView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// OnAttached to visual tree
    /// </summary>
    /// <param name="e"></param>
    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        var vm = Ioc.Default.GetService<MainPlacesViewModel>();

        var frame = this.FindControl<Frame>("PlacesFrame");
        frame!.NavigationPageFactory = new PlacePageNavigationFactory();
        vm!.Frame = frame;
        WeakReferenceMessenger.Default.Send(new PlaceNavigationMessage(new PlaceNavigationData { ViewModelType = "PlacesViewModel" }));
    }
}