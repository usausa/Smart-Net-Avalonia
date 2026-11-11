namespace Smart.Avalonia.Interactivity;

using global::Avalonia.Controls;
using global::Avalonia.Xaml.Interactivity;

using Smart.Avalonia.Expressions;

public sealed class CompareTriggerTests
{
    private sealed class RecordAction : StyledElementAction
    {
        public List<(object? Sender, object? Parameter)> Calls { get; } = [];

        public override object Execute(object? sender, object? parameter)
        {
            Calls.Add((sender, parameter));
            return true;
        }
    }

    private static (Border Element, CompareTrigger Trigger, RecordAction Action) Create()
    {
        var element = new Border();
        var action = new RecordAction();
        var trigger = new CompareTrigger { Actions = [action] };
        Interaction.GetBehaviors(element).Add(trigger);
        return (element, trigger, action);
    }

    [Fact]
    public void ExecutesActionsWhenBindingEqualsParameter()
    {
        // Arrange
        var (element, trigger, action) = Create();
        trigger.Parameter = 5;

        // Act
        trigger.Binding = 5;

        // Assert
        var (sender, parameter) = Assert.Single(action.Calls);
        Assert.Same(element, sender);
        Assert.Null(parameter);
    }

    [Fact]
    public void DoesNotExecuteActionsWhenBindingDiffersFromParameter()
    {
        // Arrange
        var (_, trigger, action) = Create();
        trigger.Parameter = 5;

        // Act
        trigger.Binding = 6;

        // Assert
        Assert.Empty(action.Calls);
    }

    [Fact]
    public void UsesExpression()
    {
        // Arrange
        var (_, trigger, action) = Create();
        trigger.Expression = CompareExpressions.GreaterThan;
        trigger.Parameter = 5;

        // Act
        trigger.Binding = 6;

        // Assert
        Assert.Single(action.Calls);
    }

    [Fact]
    public void ReevaluatesWhenExpressionChanges()
    {
        // Arrange
        var (_, trigger, action) = Create();
        trigger.Parameter = 5;
        trigger.Binding = 6;

        // Act
        trigger.Expression = CompareExpressions.GreaterThan;

        // Assert
        Assert.Single(action.Calls);
    }

    [Fact]
    public void DoesNotExecuteActionsWhenDetached()
    {
        // Arrange
        var (element, trigger, action) = Create();
        Interaction.GetBehaviors(element).Remove(trigger);
        trigger.Parameter = 5;

        // Act
        trigger.Binding = 5;

        // Assert
        Assert.Empty(action.Calls);
    }

    [Fact]
    public void DoesNotExecuteActionsWhenDisabled()
    {
        // Arrange
        var (_, trigger, action) = Create();
        trigger.IsEnabled = false;
        trigger.Parameter = 5;

        // Act
        trigger.Binding = 5;

        // Assert
        Assert.Empty(action.Calls);
    }

    [Fact]
    public void ExpressionIsEqualByDefault()
    {
        // Arrange & Act
        var trigger = new CompareTrigger();

        // Assert
        Assert.Same(CompareExpressions.Equal, trigger.Expression);
    }
}
