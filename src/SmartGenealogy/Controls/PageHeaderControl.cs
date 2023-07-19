using Avalonia;
using Avalonia.Controls.Primitives;

namespace SmartGenealogy.Controls;

/// <summary>
/// Page header control.
/// </summary>
public class PageHeaderControl : TemplatedControl
{
    /// <summary>
    /// Ctor
    /// </summary>
    public PageHeaderControl() { }

    public static readonly StyledProperty<string> TitleTextProperty =
        AvaloniaProperty.Register<PageHeaderControl, string>(nameof(TitleText));

    public string TitleText
    {
        get => GetValue(TitleTextProperty);
        set => SetValue(TitleTextProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
    }
}