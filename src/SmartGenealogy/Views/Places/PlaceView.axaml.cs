using Avalonia.Controls;

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

        MapControl.Map?.Layers.Add(OpenStreetMap.CreateTileLayer());
    }
}