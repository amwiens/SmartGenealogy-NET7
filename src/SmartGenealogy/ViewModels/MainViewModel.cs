using System;
using System.Collections.ObjectModel;
using System.Text.Json;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using FluentAvalonia.UI.Controls;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Models;
using SmartGenealogy.ViewModels.Addresses;
using SmartGenealogy.ViewModels.File;
using SmartGenealogy.ViewModels.Home;
using SmartGenealogy.ViewModels.Media;
using SmartGenealogy.ViewModels.People;
using SmartGenealogy.ViewModels.Places;
using SmartGenealogy.ViewModels.Publish;
using SmartGenealogy.ViewModels.Search;
using SmartGenealogy.ViewModels.Settings;
using SmartGenealogy.ViewModels.Sources;
using SmartGenealogy.ViewModels.Tasks;
using SmartGenealogy.ViewModels.Tools;

namespace SmartGenealogy.ViewModels;

/// <summary>
/// Main view model class.
/// </summary>
public partial class MainViewModel : ViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    private HomeViewModel? _homeViewModel;
    private MainFileViewModel? _mainFileViewModel;
    private MainPeopleViewModel? _mainPeopleViewModel;
    private MainPlacesViewModel? _mainPlacesViewModel;
    private MainSourcesViewModel? _mainSourcesViewModel;
    private MainMediaViewModel? _mainMediaViewModel;
    private MainTasksViewModel? _mainTasksViewModel;
    private MainAddressesViewModel? _mainAddressesViewModel;
    private MainSearchViewModel? _mainSearchViewModel;
    private MainPublishViewModel? _mainPublishViewModel;
    private MainToolsViewModel? _mainToolsViewModel;
    private MainSettingsViewModel? _mainSettingsViewModel;

    [ObservableProperty]
    private ViewModelBase? _currentPage;

    [ObservableProperty]
    private NavigationItem _selectedNavigationItem;

    [ObservableProperty]
    private ObservableCollection<NavigationItem> _navigationItems = new();

    [ObservableProperty]
    private ObservableCollection<NavigationItem> _footerItems = new();

    /// <summary>
    /// Ctor
    /// </summary>
    public MainViewModel() : this(null, null, null, null, null, null, null, null, null, null, null, null, null, null)
    {
    }

    /// <summary>
    /// Ctor
    /// </summary>
    public MainViewModel(ILogger? logger,
        ISettingService? settingService,
        HomeViewModel? homeViewModel,
        MainFileViewModel? mainFileViewModel,
        MainPeopleViewModel? mainPeopleViewModel,
        MainPlacesViewModel? mainPlacesViewModel,
        MainSourcesViewModel? mainSourcesViewModel,
        MainMediaViewModel? mainMediaViewModel,
        MainTasksViewModel? mainTasksViewModel,
        MainAddressesViewModel? mainAddressesViewModel,
        MainSearchViewModel? mainSearchViewModel,
        MainPublishViewModel? mainPublishViewModel,
        MainToolsViewModel? mainToolsViewModel,
        MainSettingsViewModel? mainSettingsViewModel)
    {
        _logger = logger;
        _settingService = settingService;

        _homeViewModel = homeViewModel;
        _mainFileViewModel = mainFileViewModel;
        _mainPeopleViewModel = mainPeopleViewModel;
        _mainPlacesViewModel = mainPlacesViewModel;
        _mainSourcesViewModel = mainSourcesViewModel;
        _mainMediaViewModel = mainMediaViewModel;
        _mainTasksViewModel = mainTasksViewModel;
        _mainAddressesViewModel = mainAddressesViewModel;
        _mainSearchViewModel = mainSearchViewModel;
        _mainPublishViewModel = mainPublishViewModel;
        _mainToolsViewModel = mainToolsViewModel;
        _mainSettingsViewModel = mainSettingsViewModel;

        SetupNavigation();
        SelectedNavigationItem = NavigationItems[0];
        SwitchTab();
        _logger?.Information("Main Window initialized");
        AppDomain.CurrentDomain.ProcessExit += OnExit!;
    }

    private void SetupNavigation()
    {
        NavigationItems.Clear();
        NavigationItems = new ObservableCollection<NavigationItem>
        {
            new((ViewModelBase)_homeViewModel!, "Home", "Home", Symbol.Home),
            new((ViewModelBase)_mainFileViewModel!, "File", "File", Symbol.Document),
        };

        if (!string.IsNullOrEmpty(_settingService?.Settings.FileName))
        {
            NavigationItems.Add(new((ViewModelBase)_mainPeopleViewModel!, "People", "People", Symbol.People));
            NavigationItems.Add(new((ViewModelBase)_mainPlacesViewModel!, "Places", "Places", Symbol.Earth));
            NavigationItems.Add(new((ViewModelBase)_mainSourcesViewModel!, "Sources", "Sources", Symbol.Library));
            NavigationItems.Add(new((ViewModelBase)_mainMediaViewModel!, "Media", "Media", Symbol.Image));
            NavigationItems.Add(new((ViewModelBase)_mainTasksViewModel!, "Tasks", "Tasks", Symbol.ShowResults));
            NavigationItems.Add(new((ViewModelBase)_mainAddressesViewModel!, "Addresses", "Addresses", Symbol.ContactInfo));
            NavigationItems.Add(new((ViewModelBase)_mainSearchViewModel!, "Search", "Search", Symbol.Find));
            NavigationItems.Add(new((ViewModelBase)_mainPublishViewModel!, "Publish", "Publish", Symbol.Print));
            NavigationItems.Add(new((ViewModelBase)_mainToolsViewModel!, "Tools", "Tools", Symbol.Repair));
        }

        FooterItems.Clear();
        FooterItems = new ObservableCollection<NavigationItem>
        {
            new((ViewModelBase)_mainSettingsViewModel!, "Settings", "Settings", Symbol.Settings),
        };
    }

    /// <summary>
    /// Switch tabs command.
    /// </summary>
    [RelayCommand]
    private void SwitchTab()
    {
        CurrentPage = SelectedNavigationItem.ViewModel;
        var name = SelectedNavigationItem.Name;
        _logger?.Information("Switching content to {Name}", name);
    }

    /// <summary>
    /// What happens when the program is exiting.
    /// </summary>
    private void OnExit(object sender, EventArgs e)
    {
        var json = JsonSerializer.Serialize(_settingService?.Settings, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText("Settings.json", json);
        _logger?.Information("Settings saved");
    }
}