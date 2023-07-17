namespace SmartGenealogy.Models;

public class Settings
{
    #region Main window settings

    /// <summary>
    /// Width of the main window.
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    /// Height of the main window.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Position X of the main window.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Position Y of the main window.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Is the main window maximized.
    /// </summary>
    public bool IsMaximized { get; set; }

    #endregion Main window settings

    #region Clone

    public Settings Clone()
    {
        return (Settings)MemberwiseClone();
    }

    #endregion Clone
}