using System.Threading.Tasks;

using FluentAvalonia.UI.Controls;

namespace SmartGenealogy.Contracts;

/// <summary>
/// IMessageBoxService interface.
/// </summary>
public interface IMessageBoxService
{
    Task<ContentDialogResult> CreateMessageBox();

    /// <summary>
    /// Create a notification box.
    /// </summary>
    /// <param name="message">Message to add.</param>
    /// <returns>Dialog result.</returns>
    Task<ContentDialogResult> CreateNotification(string message);
}