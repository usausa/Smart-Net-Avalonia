namespace Smart.Avalonia.Interactivity;

using System.Reflection;

using global::Avalonia;
using global::Avalonia.Xaml.Interactivity;

public sealed class ResolveMethodAction : StyledElementAction
{
    public static readonly StyledProperty<object?> TargetObjectProperty =
        AvaloniaProperty.Register<ResolveMethodAction, object?>(nameof(TargetObject));

    public static readonly StyledProperty<string> MethodNameProperty =
        AvaloniaProperty.Register<ResolveMethodAction, string>(nameof(MethodName), string.Empty);

    public object? TargetObject
    {
        get => GetValue(TargetObjectProperty);
        set => SetValue(TargetObjectProperty, value);
    }

    public string MethodName
    {
        get => GetValue(MethodNameProperty);
        set => SetValue(MethodNameProperty, value);
    }

    private MethodInfo? cachedMethod;

    public override object Execute(object? sender, object? parameter)
    {
        if (!IsEnabled)
        {
            return false;
        }

        if (parameter is not Smart.Mvvm.Messaging.ResolveEventArgs args)
        {
            return false;
        }

        var target = TargetObject ?? sender;
        if (target is null)
        {
            return false;
        }

        var methodName = MethodName;
        if (String.IsNullOrEmpty(methodName))
        {
            return false;
        }

        if ((cachedMethod is null) ||
            (cachedMethod.DeclaringType != target.GetType()) ||
            (cachedMethod.Name != methodName))
        {
            cachedMethod = target.GetType().GetRuntimeMethods().FirstOrDefault(m =>
                m.Name == methodName &&
                (m.GetParameters().Length == 0));
            if (cachedMethod is null)
            {
                return false;
            }
        }

        args.Result = cachedMethod.Invoke(target, null);
        return true;
    }
}
