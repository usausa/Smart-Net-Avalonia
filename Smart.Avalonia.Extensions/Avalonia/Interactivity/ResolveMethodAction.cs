namespace Smart.Avalonia.Interactivity;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using global::Avalonia;
using global::Avalonia.Xaml.Interactivity;

using Smart.Linq;
using Smart.Mvvm.Messaging;

[RequiresUnreferencedCode("Uses GetRuntimeMethods to find method by name; method metadata may be trimmed.")]
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

    private MethodInvoker? cachedInvoker;

    private string cachedMethodName = string.Empty;

    private Type? cachedType;

    public override object Execute(object? sender, object? parameter)
    {
        if (!IsEnabled)
        {
            return false;
        }

        if (parameter is not ResolveEventArgs args)
        {
            return false;
        }

        var target = TargetObject ?? sender;
        var methodName = MethodName;
        if ((target is null) || String.IsNullOrEmpty(methodName))
        {
            return false;
        }

        if ((cachedInvoker is null) ||
            (cachedType != target.GetType()) ||
            (cachedMethodName != methodName))
        {
            var methodInfo = target.GetType().GetRuntimeMethods()
                .FirstOrDefault(methodName, static (m, s) => (m.Name == s) && (m.GetParameters().Length == 0));
            if (methodInfo is null)
            {
                return false;
            }

            cachedInvoker = MethodInvoker.Create(methodInfo);
            cachedMethodName = methodName;
            cachedType = target.GetType();
        }

        args.Result = cachedInvoker.Invoke(target);
        return true;
    }
}
