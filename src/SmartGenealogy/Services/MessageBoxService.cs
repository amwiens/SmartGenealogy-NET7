using System.Threading.Tasks;

using FluentAvalonia.UI.Controls;

using SmartGenealogy.Contracts;

namespace SmartGenealogy.Services;

/// <summary>
/// MessageBoxService class.
/// </summary>
public class MessageBoxService : IMessageBoxService
{
    public async Task<ContentDialogResult> CreateMessageBox()
    {
        var cd = new ContentDialog
        {
            PrimaryButtonText = "OK",
            CloseButtonText = "Cancel",
            Title = "Smart Genealogy",
            Content = "This is a message",
            DefaultButton = ContentDialogButton.Primary,
        };

        return await cd.ShowAsync();
    }

    public async Task<ContentDialogResult> CreateNotification(string message)
    {
        var cd = new ContentDialog
        {
            CloseButtonText = "Ok",
            Title = "Smart Genealogy",
            Content = message,
            IsPrimaryButtonEnabled = false,
            IsSecondaryButtonEnabled = false,
        };

        return await cd.ShowAsync();
    }

    //private async Task<ContentDialogResult> CreateMessageBox(string primaryButtonText = "",)
}