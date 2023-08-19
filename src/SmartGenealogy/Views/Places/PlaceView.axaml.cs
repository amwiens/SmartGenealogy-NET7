using Avalonia;
using Avalonia.Controls;

using Mapsui.Projections;
using Mapsui;
using Mapsui.Tiling;
using Mapsui.Extensions;

namespace SmartGenealogy.Views.Places;

/// <summary>
/// Place view.
/// </summary>
public partial class PlaceView : UserControl
{
    /// <summary>
    /// Ctor
    /// </summary>
    public PlaceView()
    {
        InitializeComponent();

        MapControl.Map?.Layers.Add(OpenStreetMap.CreateTileLayer());
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        // 48.669516, -98.835677
        var centerOfMunich = new MPoint(48.669516, -98.835677);
        var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(centerOfMunich.Y, centerOfMunich.X).ToMPoint();
        MapControl.Map.Home = n => n.CenterOnAndZoomTo(sphericalMercatorCoordinate, 9);
    }
}