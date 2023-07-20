using System.Text;

namespace SmartGenealogy.Persistence.Extensions;

/// <summary>
/// SQLite database extensions
/// </summary>
public static class SQLiteDatabaseExtensions
{

    public static bool IsSQLiteDatabase(this string filePath)
    {
        if (!File.Exists(filePath))
        {
            return false;
        }

        byte[] bytes = new byte[17];
        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            fs.Read(bytes, 0, 16);
        }

        var chkStr = ASCIIEncoding.ASCII.GetString(bytes);
        return chkStr.Contains("SQLite format");
    }
}