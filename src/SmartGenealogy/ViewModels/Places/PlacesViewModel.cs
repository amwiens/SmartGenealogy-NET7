using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Serilog;

using SmartGenealogy.Contracts;

namespace SmartGenealogy.ViewModels.Places;

public partial class PlacesViewModel : ViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    [ObservableProperty]
    private bool _isVisible;

    /// <summary>
    /// Ctor
    /// </summary>
    public PlacesViewModel() : this(null, null) { }

    /// <summary>
    /// Ctor
    /// </summary>
    public PlacesViewModel(ILogger? logger,
        ISettingService? settingService)
    {
        _logger = logger;
        _settingService = settingService;

        _logger?.Information("Places view initialized");
    }

    [RelayCommand]
    public void Place()
    {
    }
}