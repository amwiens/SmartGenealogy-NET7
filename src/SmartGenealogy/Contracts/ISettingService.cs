using System.Threading.Tasks;

using SmartGenealogy.Models;

namespace SmartGenealogy.Contracts;

public interface ISettingService
{
    public Settings Settings { get; }

    Task InitializeSettings();
}