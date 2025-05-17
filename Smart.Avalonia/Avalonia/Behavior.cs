namespace Smart.Avalonia;

using global::Avalonia;
using global::Avalonia.Controls;

public static class Behavior
{
    public static readonly AttachedProperty<bool> KeyProperty =
        AvaloniaProperty.RegisterAttached<Control, bool>("Key", typeof(Behavior));

    public static bool GetKey(Control control) =>
        control.GetValue(KeyProperty);

    public static void SetKey(Control control, bool value) =>
        control.SetValue(KeyProperty, value);
}
