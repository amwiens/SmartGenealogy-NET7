using Avalonia;
using Avalonia.Controls;

using CommunityToolkit.Mvvm.DependencyInjection;

using SmartGenealogy.Contracts;

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

        var _navigationService = Ioc.Default.GetService<INavigationService>();
        _navigationService!.Frame = PlacesFrame;
    }
}