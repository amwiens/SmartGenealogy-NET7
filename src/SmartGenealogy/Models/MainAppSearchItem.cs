using SmartGenealogy.ViewModels;

namespace SmartGenealogy.Models;

/// <summary>
/// Main app search item.
/// </summary>
public class MainAppSearchItem
{
    public MainAppSearchItem() { }

    public MainAppSearchItem(string pageHeader, string pageType)
    {
        Header = pageHeader;
        PageType = pageType;
    }

    public string? Header { get; set; }

    public ViewModelBase? ViewModel { get; set; }

    public string? Namespace { get; set; }

    public string? PageType { get; set; }
}