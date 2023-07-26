using System.ComponentModel;

using FluentAvalonia.UI.Controls;

using SmartGenealogy.ViewModels;

namespace SmartGenealogy.Contracts;

/// <summary>
/// Navigation service interface.
/// </summary>
public interface INavigationService
{
    Frame Frame { get; set; }

    bool CanGoBack { get; set; }

    INavigationPageFactory NavigationPageFactory { get; set; }

    string CurrentViewName { get; }

    ViewModelBase CurrentView { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    void NavigateTo<T>() where T : ViewModelBase;
}