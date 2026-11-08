namespace Smart.Avalonia;

using global::Avalonia;
using global::Avalonia.Controls;

public static class Behavior
{
    public static readonly AttachedProperty<string?> KeyProperty =
        AvaloniaProperty.RegisterAttached<Control, string?>("Key", typeof(Behavior));

    public static string? GetKey(Control control) =>
        control.GetValue(KeyProperty);

    public static void SetKey(Control control, string? value) =>
        control.SetValue(KeyProperty, value);
}
