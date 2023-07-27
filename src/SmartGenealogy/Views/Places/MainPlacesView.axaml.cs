using Avalonia;
using Avalonia.Controls;

using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;

using FluentAvalonia.UI.Controls;

using SmartGenealogy.Messages;
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

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        var vm = Ioc.Default.GetService<MainPlacesViewModel>();

        var frame = this.FindControl<Frame>("PlacesFrame");
        vm!.Frame = frame;
        WeakReferenceMessenger.Default.Send(new PlaceNavigationMessage(new PlacesViewModel()));
    }
}