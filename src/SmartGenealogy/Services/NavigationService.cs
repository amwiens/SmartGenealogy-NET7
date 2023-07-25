using System;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

using FluentAvalonia.UI.Controls;

using SmartGenealogy.Contracts;
using SmartGenealogy.ViewModels;

namespace SmartGenealogy.Services;

/// <summary>
/// Navigation service.
/// </summary>
public partial class NavigationService : ObservableObject, INavigationService
{

    public Frame Frame { get; set; }

    [ObservableProperty]
    private bool _canGoBack;

    [ObservableProperty]
    private INavigationPageFactory? _navigationPageFactory;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentViewName))]
    private ViewModelBase? _currentView;

    public string CurrentViewName => CurrentView!.GetType().Name.Replace("ViewModel", "View");

    private readonly Func<Type, ViewModelBase> _viewModelFactory;

    public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
        NavigationPageFactory = Ioc.Default.GetService<INavigationPageFactory>();
        Frame!.NavigationPageFactory = NavigationPageFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
    {
        //ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        //CurrentView = viewModel;
        Frame.Navigate(typeof(TViewModel));
    }
}