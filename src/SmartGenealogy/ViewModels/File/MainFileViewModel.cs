using System.Threading.Tasks;

using Avalonia.Controls;
using Avalonia.Platform.Storage;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Messages;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.File;

/// <summary>
/// Main file view model.
/// </summary>
public partial class MainFileViewModel : MainViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;

    [ObservableProperty]
    private bool _isFileOpen;

    [ObservableProperty]
    private string? _currentFile;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainFileViewModel() : this(null, null)
    {
    }

    /// <summary>
    /// Ctor
    /// </summary>
    public MainFileViewModel(ILogger? logger,
        ISettingService? settingService)
    {
        _logger = logger;
        _settingService = settingService;

        Title = "File";
        SetFileInformation();
        _logger?.Information("File Window initialized");
    }

    /// <summary>
    /// Set the file information.
    /// </summary>
    private void SetFileInformation()
    {
        CurrentFile = $"Current File: {_settingService?.Settings.FileName}";
        IsFileOpen = !string.IsNullOrEmpty(_settingService?.Settings.FileName);
    }

    /// <summary>
    /// Create a new file.
    /// </summary>
    [RelayCommand]
    private void CreateFile()
    {
        _logger?.Information("Create file button clicked");
    }

    /// <summary>
    /// Open an existing file.
    /// </summary>
    [RelayCommand]
    private async Task OpenFile()
    {
        _logger?.Information("Open file button clicked");

        var dialog = await new Window().StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Smart Genealogy file",
            AllowMultiple = false,
            FileTypeFilter = new FilePickerFileType[] { new FilePickerFileType("Smart Genealogy Databases") { Patterns = new[] { "*.sgdb" } } }
        });
        if (dialog.Count == 0)
        {
            _logger?.Information("Open file cancelled");
            return;
        }

        var file = dialog[0].Path;
        _logger?.Information("User chose file {File}", file);
        _settingService.Settings.FileName = file.ToString();
        SetFileInformation();
        WeakReferenceMessenger.Default.Send(new OpenFileChangedMessage(true));
    }

    /// <summary>
    /// Close an open file.
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private void CloseFile()
    {
        _logger.Information("Close file button clicked");
        _settingService.Settings.FileName = null;
        SetFileInformation();
        WeakReferenceMessenger.Default.Send(new OpenFileChangedMessage(false));
    }
}