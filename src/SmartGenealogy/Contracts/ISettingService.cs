using System.Threading.Tasks;

using SmartGenealogy.Models;

namespace SmartGenealogy.Contracts;

/// <summary>
/// Setting service interface.
/// </summary>
public interface ISettingService
{
    /// <summary>
    /// Settings for the application.
    /// </summary>
    public Settings Settings { get; }

    /// <summary>
    /// Initializes the settings.
    /// </summary>
    /// <returns>Default settings.</returns>
    Task InitializeSettings();
}