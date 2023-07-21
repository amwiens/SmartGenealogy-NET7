using Microsoft.EntityFrameworkCore;

namespace SmartGenealogy.Persistence;

public class SmartGenealogyContext : DbContext
{
    private string _databasePath;

    public string DatabasePath
    {
        get { return _databasePath; }
        set
        {
            _databasePath = value;
            Database.Migrate();
        }
    }

    public SmartGenealogyContext()
    {
        _databasePath = "SmartGenealogy.sgdb";
    }

    public SmartGenealogyContext(string databasePath)
    {
        _databasePath = databasePath;

        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSqlite($"DataSource={_databasePath}");
}