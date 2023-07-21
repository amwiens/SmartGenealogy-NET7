namespace SmartGenealogy.Persistence.Models;

/// <summary>
/// Place entity
/// </summary>
public class Place : BaseEntity
{
    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Abbreviation
    /// </summary>
    public string? Abbreviation { get; set; }

    /// <summary>
    /// Normalized name
    /// </summary>
    public string? Normalized { get; set; }

    /// <summary>
    /// Latitude
    /// </summary>
    public decimal? Latitude { get; set; }

    /// <summary>
    /// Longitude
    /// </summary>
    public decimal? Longitude { get; set; }

    /// <summary>
    /// Lat/Long exact
    /// </summary>
    public string? LatLongExact { get; set; }

    /// <summary>
    /// Master Id
    /// </summary>
    public long MasterId { get; set; }

    /// <summary>
    /// Note
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Reversed place name
    /// </summary>
    public string? Reverse { get; set; }
}