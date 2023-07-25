using System;

using Avalonia.Controls;

using FluentAvalonia.UI.Controls;

namespace SmartGenealogy.Services;

/// <summary>
/// Navigation factory.
/// </summary>
public class NavigationFactory : INavigationPageFactory
{
    public Control GetPage(Type srcType)
    {
        return null;
    }

    public Control GetPageFromObject(object target)
    {
        throw new NotImplementedException();
    }
}