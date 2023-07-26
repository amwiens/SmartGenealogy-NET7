using System.Collections.Generic;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Models;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.Places;

/// <summary>
/// Main places view model.
/// </summary>
public partial class MainPlacesViewModel : MainViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    [ObservableProperty]
    private bool _isVisible;

    private Stack<PageMarker> _navigationStack = new();

    public PageMarker? CurrentPage
    {
        get
        {
            if (_navigationStack.TryPeek(out var page))
                return page;

            return null;
        }
    }

    public bool CanNavigateBack => _navigationStack.Count > 1;

    public bool BackButtonEnabled { get; private set; } = true;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainPlacesViewModel() : this(null, null) { }

    /// <summary>
    /// Ctor
    /// </summary>
    public MainPlacesViewModel(ILogger? logger,
        ISettingService? settingService)
    {
        _logger = logger;
        _settingService = settingService;

        Title = "Places";
        _logger?.Information("Main Places view initialized");
    }
}