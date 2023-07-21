namespace SmartGenealogy.Persistence.Models;
public class BaseEntity
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Date created
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Date updated
    /// </summary>
    public DateTime DateUpdated { get; set; }
}