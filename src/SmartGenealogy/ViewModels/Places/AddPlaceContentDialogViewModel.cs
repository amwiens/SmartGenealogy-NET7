using System;

using CommunityToolkit.Mvvm.ComponentModel;

using FluentAvalonia.UI.Controls;

namespace SmartGenealogy.ViewModels.Places;

/// <summary>
/// Add place content dialog view model.
/// </summary>
public partial class AddPlaceContentDialogViewModel : ViewModelBase
{
    private readonly ContentDialog _dialog;

    [ObservableProperty]
    private string? _userInput;

    /// <summary>
    /// Ctor
    /// </summary>
    public AddPlaceContentDialogViewModel(ContentDialog dialog)
    {
        if (dialog is null)
        {
            throw new ArgumentNullException(nameof(dialog));
        }

        _dialog = dialog;
    }
}