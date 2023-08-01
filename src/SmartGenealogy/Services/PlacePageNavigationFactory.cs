using System;

using Avalonia.Controls;

using FluentAvalonia.UI.Controls;

using SmartGenealogy.ViewModels.Places;
using SmartGenealogy.Views.Places;

namespace SmartGenealogy.Services;

public class PlacePageNavigationFactory : INavigationPageFactory
{
    public Control? GetPage(Type srcType)
    {
        return null;
    }

    public Control? GetPageFromObject(object target)
    {
        if (target is PlacesViewModel)
        {
            return new PlacesView() { DataContext = target };
        }
        else if (target is PlaceViewModel)
        {
            return new PlaceView() { DataContext = target };
        }

        return null;
    }
}