namespace Smart.Avalonia.Interactivity;

using System.ComponentModel;

using global::Avalonia;
using global::Avalonia.Xaml.Interactivity;

public sealed class CancelEventAction : StyledElementAction
{
    public static readonly StyledProperty<bool> CancelProperty =
        AvaloniaProperty.Register<CancelEventAction, bool>(nameof(Cancel));

    public bool Cancel
    {
        get => GetValue(CancelProperty);
        set => SetValue(CancelProperty, value);
    }

    public override object? Execute(object? sender, object? parameter)
    {
        if (!IsEnabled)
        {
            return false;
        }

        if (parameter is not CancelEventArgs args)
        {
            return false;
        }

        var cancel = Cancel;
        args.Cancel = cancel;

        return cancel;
    }
}
