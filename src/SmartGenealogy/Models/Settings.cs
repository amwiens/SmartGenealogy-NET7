﻿namespace SmartGenealogy.Models;

/// <summary>
/// Application settings.
/// </summary>
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

    #region Application settings

    /// <summary>
    /// Last used file name.
    /// </summary>
    public string? FileName { get; set; }

    #endregion Application settings

    #region Display settings

    /// <summary>
    /// Current application theme.
    /// </summary>
    public string? CurrentTheme { get; set; }

    #endregion Display settings

    #region Clone

    /// <summary>
    /// Clones the settings.
    /// </summary>
    /// <returns>Settings.</returns>
    public Settings Clone()
    {
        return (Settings)MemberwiseClone();
    }

    #endregion Clone
}