using System;

using CommunityToolkit.Mvvm.ComponentModel;

using FluentAvalonia.UI.Controls;

namespace SmartGenealogy.ViewModels.Places;

/// <summary>
/// Add place content dialog view model.
/// </summary>
public partial class AddPlaceDetailsContentDialogViewModel : ViewModelBase
{
    private readonly ContentDialog _dialog;

    [ObservableProperty]
    private string? _userInput;

    /// <summary>
    /// Ctor
    /// </summary>
    public AddPlaceDetailsContentDialogViewModel(ContentDialog dialog)
    {
        if (dialog is null)
        {
            throw new ArgumentNullException(nameof(dialog));
        }

        _dialog = dialog;
    }
}