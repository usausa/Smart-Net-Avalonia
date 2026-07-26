namespace Smart.Avalonia.Resolver;

using global::Avalonia;
using global::Avalonia.Controls;

public static class DataContextResolver
{
    public static readonly AttachedProperty<Type> TypeProperty =
        AvaloniaProperty.RegisterAttached<Control, Type>("Type", typeof(DataContextResolver));

    private static readonly AttachedProperty<object?> ResolvedProperty =
        AvaloniaProperty.RegisterAttached<Control, object?>("Resolved", typeof(DataContextResolver));

    public static readonly AttachedProperty<bool> DisposeOnChangedProperty =
        AvaloniaProperty.RegisterAttached<Control, bool>("DisposeOnChanged", typeof(DataContextResolver));

    public static Type GetType(Control control) =>
        control.GetValue(TypeProperty);

    public static void SetType(Control control, Type value) =>
        control.SetValue(TypeProperty, value);

    public static bool GetDisposeOnChanged(Control control) =>
        control.GetValue(DisposeOnChangedProperty);

    public static void SetDisposeOnChanged(Control control, bool value) =>
        control.SetValue(DisposeOnChangedProperty, value);

    static DataContextResolver()
    {
        TypeProperty.Changed.AddClassHandler<Control>(HandleTypePropertyChanged);
    }

    private static void HandleTypePropertyChanged(Control control, AvaloniaPropertyChangedEventArgs e)
    {
        var context = e.NewValue is not null ? ResolveHelper.Resolve((Type)e.NewValue) : null;

        var resolved = control.GetValue(ResolvedProperty);
        control.SetValue(ResolvedProperty, context);
        control.DataContext = context;

        if (GetDisposeOnChanged(control) &&
            !ReferenceEquals(resolved, context) &&
            resolved is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}
