using System;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;

using CommunityToolkit.Mvvm.DependencyInjection;

using FluentAvalonia.UI.Controls;

using Microsoft.Extensions.DependencyInjection;

using Serilog;

using SmartGenealogy.Contracts;
using SmartGenealogy.Persistence;
using SmartGenealogy.Persistence.Models;
using SmartGenealogy.Services;
using SmartGenealogy.ViewModels;
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
using SmartGenealogy.Views;

namespace SmartGenealogy;

public partial class App : Application
{
    public App()
    {
        ConfigureServices();
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation. Without this line you will get
        // duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Ioc.Default.GetRequiredService<MainViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = Ioc.Default.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices()
    {
        Ioc.Default.ConfigureServices(
            new ServiceCollection()
                .AddDbContext<SmartGenealogyContext>()
                // Services
                .AddSingleton<ISettingService, SettingService>()
                .AddSingleton<IMessageBoxService, MessageBoxService>()
                .AddSingleton(Log.Logger)
                .AddSingleton<Func<Type, ViewModelBase>>(serviceProvider =>
                    viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType))
                // Repositories
                .AddScoped<IDataRepository<Place>, PlaceRepository>()
                // ViewModels
                .AddTransient<MainViewModel>()
                .AddTransient<HomeViewModel>()
                .AddTransient<MainFileViewModel>()
                .AddTransient<MainPeopleViewModel>()
                .AddTransient<MainPlacesViewModel>()
                .AddTransient<PlacesViewModel>()
                .AddTransient<PlaceViewModel>()
                .AddTransient<MainSourcesViewModel>()
                .AddTransient<MainMediaViewModel>()
                .AddTransient<MainTasksViewModel>()
                .AddTransient<MainAddressesViewModel>()
                .AddTransient<MainSearchViewModel>()
                .AddTransient<MainPublishViewModel>()
                .AddTransient<MainToolsViewModel>()
                .AddTransient<MainSettingsViewModel>()
                .AddTransient<DisplaySettingsViewModel>()
                .BuildServiceProvider());
    }
}