using System.Collections.ObjectModel;

using Avalonia;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Models;
using SmartGenealogy.ViewModels.Home;
using SmartGenealogy.ViewModels.Search;

namespace SmartGenealogy.ViewModels.Settings;

public partial class MainSettingsViewModel : ViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    private readonly HomeViewModel? _homeViewModel;
    private readonly MainSearchViewModel? _mainSearchViewModel;

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
    public MainSettingsViewModel() : this(null, null, null, null)
    {
    }

    /// <summary>
    /// Ctor
    /// </summary>
    public MainSettingsViewModel(ILogger? logger,
        ISettingService? settingService,
        HomeViewModel? homeViewModel,
        MainSearchViewModel? mainSearchViewModel)
    {
        _logger = logger;
        _settingService = settingService;

        _homeViewModel = homeViewModel;
        _mainSearchViewModel = mainSearchViewModel;

        SetupTabs();
        SelectedTab = TabItems[0];
        SwitchTab();

        _logger?.Information("Settings view initialized");
    }

    /// <summary>
    /// Set up the tabs on the settings page.
    /// </summary>
    private void SetupTabs()
    {
        TabItems.Clear();
        TabItems = new ObservableCollection<TabItem>()
        {
            new((ViewModelBase)_homeViewModel!, "Home", "Home"),
            new((ViewModelBase)_mainSearchViewModel!, "Search", "Search"),
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