using Avalonia.Controls;

using Mapsui;
using Mapsui.Tiling;

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

        var map = new Map();
        map!.Layers.Add(OpenStreetMap.CreateTileLayer());
        //map.Home = n => n.CenterOnAndZoomTo(new MPoint())

        MapControl.Map = map;
    }
}