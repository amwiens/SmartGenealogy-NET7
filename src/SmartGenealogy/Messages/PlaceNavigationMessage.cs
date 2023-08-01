using CommunityToolkit.Mvvm.Messaging.Messages;

using SmartGenealogy.Persistence.Models;

namespace SmartGenealogy.Messages;

/// <summary>
/// Place navigation message
/// </summary>
public class PlaceNavigationMessage : ValueChangedMessage<PlaceNavigationData>
{
    /// <summary>
    /// Ctor
    /// </summary>
    public PlaceNavigationMessage(PlaceNavigationData data)
        : base(data)
    { }
}

public class PlaceNavigationData
{
    public string? ViewModelType { get; set; }

    public long Id { get; set; }
}