namespace Smart.Avalonia.Interactivity;

using global::Avalonia;
using global::Avalonia.Xaml.Interactivity;

using Smart.Mvvm.Expressions;

public sealed class CompareTrigger : StyledElementTrigger
{
    public static readonly StyledProperty<object?> BindingProperty =
        AvaloniaProperty.Register<CompareTrigger, object?>(nameof(Binding));

    public static readonly StyledProperty<object?> ParameterProperty =
        AvaloniaProperty.Register<CompareTrigger, object?>(nameof(Parameter));

    public static readonly StyledProperty<ICompareExpression?> ExpressionProperty =
        AvaloniaProperty.Register<CompareTrigger, ICompareExpression?>(nameof(Expression));

    public object? Binding
    {
        get => GetValue(BindingProperty);
        set => SetValue(BindingProperty, value);
    }

    public object? Parameter
    {
        get => GetValue(ParameterProperty);
        set => SetValue(ParameterProperty, value);
    }

    public ICompareExpression? Expression
    {
        get => GetValue(ExpressionProperty);
        set => SetValue(ExpressionProperty, value);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if ((change.Property == BindingProperty) ||
            (change.Property == ParameterProperty) ||
            (change.Property == ExpressionProperty))
        {
            OnValueChanged(change);
        }
    }

    private void OnValueChanged(AvaloniaPropertyChangedEventArgs args)
    {
        if (!IsEnabled)
        {
            return;
        }

        if (args.OldValue == args.NewValue)
        {
            return;
        }

        if (AssociatedObject is null)
        {
            return;
        }

        var expression = Expression ?? CompareExpressions.Equal;
        if (expression.Eval(Binding, Parameter))
        {
            Interaction.ExecuteActions(AssociatedObject, Actions, Parameter);
        }
    }
}
