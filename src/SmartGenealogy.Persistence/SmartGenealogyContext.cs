using Microsoft.EntityFrameworkCore;

using SmartGenealogy.Persistence.Models;

namespace SmartGenealogy.Persistence;

/// <summary>
/// Smart Genealogy database context
/// </summary>
public class SmartGenealogyContext : DbContext
{
    private string _databasePath;

    /// <summary>
    /// Places
    /// </summary>
    public DbSet<Place> Places { get; set; }

    /// <summary>
    /// Database path
    /// </summary>
    public string DatabasePath
    {
        get { return _databasePath; }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _databasePath = value;
                Database.Migrate();
            }
        }
    }

    /// <summary>
    /// Ctor
    /// </summary>
    public SmartGenealogyContext()
    {
        _databasePath = "SmartGenealogy.sgdb";
    }

    /// <summary>
    /// Ctor
    /// </summary>
    public SmartGenealogyContext(string databasePath)
    {
        _databasePath = databasePath;

        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite($"DataSource={_databasePath}");
}