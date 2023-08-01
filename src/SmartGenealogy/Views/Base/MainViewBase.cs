using System;
using System.Collections.Generic;
using System.Threading;

using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;

using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Controls.Experimental;
using FluentAvalonia.UI.Navigation;

using SmartGenealogy.ViewModels.Base;

namespace SmartGenealogy.Views.Base;

public class MainViewBase : UserControl
{
    private bool _isSmallWidth2;
    private CancellationTokenSource _cancellationTokenSource;
    private bool _hasLoaded;

    private Panel _detailsPanel;
    private StackPanel _optionsHost;
    private IconSourceElement _previewImageHost;
    private StackPanel _detailsHost;
    private ScrollViewer _scroller;

    public MainViewBase()
    {
        SizeChanged += MainViewBaseSizeChanged;
        AddHandler(Frame.NavigatingFromEvent, FrameNavigatingFrom, RoutingStrategies.Direct);
        AddHandler(Frame.NavigatedToEvent, FrameNavigatedTo, RoutingStrategies.Direct);
    }

    public static readonly StyledProperty<string> PageNameProperty =
        AvaloniaProperty.Register<MainViewBase, string>(nameof(PageName));

    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<MainViewBase, string>(nameof(Description));

    public static readonly StyledProperty<IconSource> PreviewImageProperty =
        AvaloniaProperty.Register<MainViewBase, IconSource>(nameof(PreviewImage));

    public string PageName
    {
        get => GetValue(PageNameProperty);
        set => SetValue(PageNameProperty, value);
    }

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public IconSource PreviewImage
    {
        get => GetValue(PreviewImageProperty);
        set => SetValue(PreviewImageProperty, value);
    }

    public MainViewModelBase CreationContext { get; set; }


    protected override Type StyleKeyOverride => typeof(MainViewBase);

    protected ThemeVariantScope ThemeScopeProvider { get; private set; }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        ThemeScopeProvider = e.NameScope.Find<ThemeVariantScope>("ThemeScopeProvider");
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        _hasLoaded = true;
        //SetDetailsAnimation();
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        _hasLoaded = false;
    }

    private void MainViewBaseSizeChanged(object sender, SizeChangedEventArgs e)
    {
        var sz = e.NewSize.Width;

        bool isSmallWidth2 = sz < 580;

        PseudoClasses.Set(":smallWidth", sz < 710);
        PseudoClasses.Set("smallWidth2", isSmallWidth2);

        if (isSmallWidth2 && !_isSmallWidth2)
        {
            AnimateOptions(true);
            _isSmallWidth2 = true;
        }
        else if (!isSmallWidth2 && _isSmallWidth2)
        {
            AnimateOptions(false);
            _isSmallWidth2 = false;
        }
    }

    private async void AnimateOptions(bool toSmall)
    {
        if (!_hasLoaded)
            return;

        _cancellationTokenSource?.Cancel();

        _cancellationTokenSource = new CancellationTokenSource();
        double x = toSmall ? 70 : -70;
        double y = toSmall ? -30 : 30;
        var ani = new Animation
        {
            Duration = TimeSpan.FromSeconds(0.25),
            Children =
            {
                new KeyFrame
                {
                    Cue = new Cue(0d),
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, x),
                        new Setter(TranslateTransform.YProperty, y),
                        new Setter(OpacityProperty, 0d)
                    }
                },
                new KeyFrame
                {
                    Cue = new Cue(1d),
                    Setters =
                    {
                        new Setter(TranslateTransform.XProperty, 0d),
                        new Setter(TranslateTransform.YProperty, 0d),
                        new Setter(OpacityProperty, 1d)
                    },
                    KeySpline = new KeySpline(0, 0, 0, 1)
                }
            }
        };

        await ani.RunAsync(_optionsHost, _cancellationTokenSource.Token);

        _cancellationTokenSource = null;
    }

    private void FrameNavigatingFrom(object sender, NavigatingCancelEventArgs e)
    {

    }

    private void FrameNavigatedTo(object sender, NavigationEventArgs e)
    {
        var svc = ConnectedAnimationService.GetForView(TopLevel.GetTopLevel(this));
        var animation = svc.GetAnimation("ForwardAnimation");

        if (animation != null)
        {
            var coordinated = new List<Visual>
            {
                _optionsHost,
                _detailsHost,
                _scroller
            };

            // PreviewImageHost is inside a Viewbox which can really mess with the Composition
            // animation - use the viewbox directly for the animation to ensure it works correctly
            animation.TryStart((Control)_previewImageHost.Parent, coordinated);
        }
    }
}