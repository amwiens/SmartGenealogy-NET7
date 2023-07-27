using CommunityToolkit.Mvvm.Messaging.Messages;

using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.Messages;

/// <summary>
/// Place navigation message
/// </summary>
public class PlaceNavigationMessage : ValueChangedMessage<PageViewModelBase>
{
    /// <summary>
    /// Ctor
    /// </summary>
    public PlaceNavigationMessage(PageViewModelBase viewModel)
        : base(viewModel)
    { }
}