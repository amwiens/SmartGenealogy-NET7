using CommunityToolkit.Mvvm.ComponentModel;

using SmartGenealogy.ViewModels;

namespace SmartGenealogy.Models;

/// <summary>
/// Tab item.
/// </summary>
public partial class TabItem : ObservableObject
{
    public readonly string? Name;

    [ObservableProperty]
    private string? _displayName;

    public ViewModelBase ViewModel { get; set; }

    public TabItem(ViewModelBase viewModel, string displayName, string name)
    {
        ViewModel = viewModel;
        DisplayName = displayName;
        Name = name;
    }

    public override string ToString()
    {
        return DisplayName!;
    }
}