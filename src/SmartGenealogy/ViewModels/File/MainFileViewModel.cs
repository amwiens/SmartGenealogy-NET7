using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Avalonia.Controls;
using Avalonia.Platform.Storage;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Messages;
using SmartGenealogy.Persistence;
using SmartGenealogy.Persistence.Extensions;
using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.ViewModels.File;

/// <summary>
/// Main file view model.
/// </summary>
public partial class MainFileViewModel : MainViewModelBase
{
    private readonly ILogger? _logger;
    private readonly ISettingService? _settingService;
    private readonly IMessageBoxService? _messageBoxService;
    private readonly SmartGenealogyContext? _context;

    [ObservableProperty]
    private bool _isFileOpen;

    [ObservableProperty]
    private string? _currentFile;

    /// <summary>
    /// Ctor
    /// </summary>
    public MainFileViewModel() : this(null, null, null, null)
    {
    }

    /// <summary>
    /// Ctor
    /// </summary>
    public MainFileViewModel(ILogger? logger,
        ISettingService? settingService,
        IMessageBoxService? messageBoxService,
        SmartGenealogyContext? context)
    {
        _logger = logger;
        _settingService = settingService;
        _messageBoxService = messageBoxService;
        _context = context;

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
        _context!.DatabasePath = _settingService?.Settings.FileName!;
        IsFileOpen = !string.IsNullOrEmpty(_settingService?.Settings.FileName);
        WeakReferenceMessenger.Default.Send(new OpenFileChangedMessage(IsFileOpen));
    }

    /// <summary>
    /// Create a new file.
    /// </summary>
    [RelayCommand]
    private async Task CreateFile()
    {
        _logger?.Information("Create file button clicked");
        var directory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Program))?.Location);
        var fileName = Path.Combine(directory!, "test.sgdb");
        _settingService!.Settings.FileName = fileName;
        SetFileInformation();
        await _messageBoxService?.CreateNotification("Database created.")!;
        if (System.IO.File.Exists(fileName))
        {
            await _messageBoxService?.CreateNotification("File exists.")!;
        }
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

        var file = dialog[0].Path.AbsolutePath;
        _logger?.Information("User chose file {File}", file);

        if (file.ToString().IsSQLiteDatabase())
        {
            _logger?.Information("File is SQLite database.");
            _settingService!.Settings.FileName = file.ToString();
            SetFileInformation();
        }
        else
        {
            var message = "File isn't a valid SQLite database.";
            _logger?.Information(message);
            await _messageBoxService?.CreateNotification(message)!;
        }

    }

    /// <summary>
    /// Restore an existing file.
    /// </summary>
    [RelayCommand]
    private async Task RestoreFile()
    {
        _logger?.Information("Restore file button clicked");
        await _messageBoxService?.CreateNotification("Restore functionality coming soon.")!;
    }

    /// <summary>
    /// Backup an existing file.
    /// </summary>
    [RelayCommand]
    private async Task BackupFile()
    {
        _logger?.Information("Backup file button clicked");
        await _messageBoxService?.CreateNotification("Backup functionality coming soon.")!;
    }

    /// <summary>
    /// Import data.
    /// </summary>
    [RelayCommand]
    private async Task ImportData()
    {
        _logger?.Information("Import button clicked");
        await _messageBoxService?.CreateNotification("Import functionality coming soon.")!;
    }

    /// <summary>
    /// Export data.
    /// </summary>
    [RelayCommand]
    private async Task ExportData()
    {
        _logger?.Information("Export data button clicked");
        await _messageBoxService?.CreateNotification("Export functionality coming soon.")!;
    }

    /// <summary>
    /// File tools.
    /// </summary>
    [RelayCommand]
    private async Task Tools()
    {
        _logger?.Information("Tools button clicked");
        await _messageBoxService?.CreateNotification("Tools functionality coming soon.")!;
    }

    /// <summary>
    /// Close an open file.
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    private void CloseFile()
    {
        _logger?.Information("Close file button clicked");
        _settingService!.Settings.FileName = null;
        SetFileInformation();
    }
}