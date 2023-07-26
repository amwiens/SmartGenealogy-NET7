using System;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;

using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Media.Animation;

using SmartGenealogy.Contracts;
using SmartGenealogy.ViewModels;
using SmartGenealogy.ViewModels.Places;

namespace SmartGenealogy.Services;

/// <summary>
/// Navigation service.
/// </summary>
public partial class NavigationService : ObservableObject, INavigationService
{
    [ObservableProperty]
    public Frame _frame;

    partial void OnFrameChanged(Frame value)
    {
        Frame!.NavigationPageFactory = Ioc.Default.GetService<INavigationPageFactory>();
        switch (value.Name)
        {
            case "PlacesFrame":
                Frame.Navigate(typeof(PlacesViewModel), null, new SlideNavigationTransitionInfo());
                break;
        }
    }

    [ObservableProperty]
    private bool _canGoBack;

    [ObservableProperty]
    private INavigationPageFactory? _navigationPageFactory;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentViewName))]
    private ViewModelBase? _currentView;

    public string CurrentViewName => CurrentView!.GetType().Name.Replace("ViewModel", "View");

    private readonly Func<Type, ViewModelBase> _viewModelFactory;

    public NavigationService(Func<Type, ViewModelBase> viewModelFactory, INavigationPageFactory navigationPageFactory)
    {
        _viewModelFactory = viewModelFactory;
        //NavigationPageFactory = Ioc.Default.GetService<INavigationPageFactory>();
        //Frame!.NavigationPageFactory = navigationPageFactory;
    }

    public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
    {
        //ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        //CurrentView = viewModel;
        Frame.Navigate(typeof(TViewModel), null, new SlideNavigationTransitionInfo());
    }
}