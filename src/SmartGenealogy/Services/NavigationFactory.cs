using System;

using Avalonia.Controls;

using FluentAvalonia.UI.Controls;

using SmartGenealogy.ViewModels;

namespace SmartGenealogy.Services;

/// <summary>
/// Navigation factory.
/// </summary>
public class NavigationFactory : INavigationPageFactory
{
    private readonly ViewLocator _viewLocator;

    public ViewModelBase Owner { get; }

    public NavigationFactory() // ViewModelBase owner)
    {
        _viewLocator = new ViewLocator();
        //Owner = owner;
    }

    /// <inheritdoc />
    public Control? GetPage(Type srcType)
    {
        return null;
    }

    /// <inheritdoc />
    public Control GetPageFromObject(object target)
    {
        Control control = _viewLocator.Build(target);
        control.DataContext = target;
        return control;
    }
}