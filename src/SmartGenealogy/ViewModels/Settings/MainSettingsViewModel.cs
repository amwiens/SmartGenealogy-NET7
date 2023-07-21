using System.Collections.ObjectModel;

using Avalonia;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Serilog;

using SmartGenealogy.Models;

namespace SmartGenealogy.ViewModels.Settings;

/// <summary>
/// Main settings view model
/// </summary>
public partial class MainSettingsViewModel : ViewModelBase
{
    private readonly ILogger? _logger;

    private readonly DisplaySettingsViewModel? _displaySettingsViewModel;

    [ObservableProperty]
    private string _currentVersion = typeof(Controls.PageHeaderControl).Assembly.GetName().Version?.ToString()!;

    [ObservableProperty]
    private string _currentAvaloniaVersion = typeof(Application).Assembly.GetName().Version?.ToString()!;

    [ObservableProperty]
    private ViewModelBase? _currentTab;

    [ObservableProperty]
    private TabItem _selectedTab;

    [ObservableProperty]
    private ObservableCollection<TabItem> _tabItems = new();

    /// <summary>
    /// Ctor
    /// </summary>
    public MainSettingsViewModel() : this(null, null)
    {
    }

    /// <summary>
    /// Ctor
    /// </summary>
    public MainSettingsViewModel(ILogger? logger,
        DisplaySettingsViewModel? displaySettingsViewModel)
    {
        _logger = logger;

        _displaySettingsViewModel = displaySettingsViewModel;

        SetupTabs();
        SelectedTab = TabItems[0];
        SwitchTab();

        _logger?.Information("Main settings view initialized");
    }

    /// <summary>
    /// Set up the tabs on the settings page.
    /// </summary>
    private void SetupTabs()
    {
        TabItems.Clear();
        TabItems = new ObservableCollection<TabItem>()
        {
            new((ViewModelBase)_displaySettingsViewModel!, "Display", "Display"),
        };
    }

    /// <summary>
    /// Switch tabs command.
    /// </summary>
    [RelayCommand]
    private void SwitchTab()
    {
        CurrentTab = SelectedTab.ViewModel;
        var name = SelectedTab.Name;
        _logger?.Information("Switching settings tab content to {Name}", name);
    }
}