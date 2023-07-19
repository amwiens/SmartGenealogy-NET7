using CommunityToolkit.Mvvm.Messaging.Messages;

namespace SmartGenealogy.Messages;

/// <summary>
/// Open file changed message.
/// </summary>
public class OpenFileChangedMessage : ValueChangedMessage<bool>
{
    /// <summary>
    /// Ctor
    /// </summary>
    public OpenFileChangedMessage(bool fileOpen)
        : base(fileOpen)
    { }
}