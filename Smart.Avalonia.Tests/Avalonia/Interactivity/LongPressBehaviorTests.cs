namespace Smart.Avalonia.Interactivity;

using global::Avalonia.Controls;
using global::Avalonia.Input;
using global::Avalonia.Xaml.Interactivity;

using Smart.Avalonia.Input;

public sealed class LongPressBehaviorTests
{
    private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(5);

    private static readonly TimeSpan Wait = TimeSpan.FromMilliseconds(200);

    private static Pointer CreatePointer() => new(Pointer.GetNextFreeId(), PointerType.Mouse, true);

    private static void RaisePressed(Control control, Pointer pointer, RawInputModifiers modifiers, PointerUpdateKind kind) =>
        control.RaiseEvent(new PointerPressedEventArgs(control, pointer, control, default, 0, new PointerPointProperties(modifiers, kind), KeyModifiers.None));

    private static void RaiseReleased(Control control, Pointer pointer) =>
        control.RaiseEvent(new PointerReleasedEventArgs(control, pointer, control, default, 0, new PointerPointProperties(RawInputModifiers.None, PointerUpdateKind.LeftButtonReleased), KeyModifiers.None, MouseButton.Left));

    [Fact]
    public void ExecutesCommandWhenHeldForDuration()
    {
        // Arrange
        using var pointer = CreatePointer();
        var button = new Button();
        object? received = null;
        Interaction.GetBehaviors(button).Add(new LongPressBehavior
        {
            Command = new DelegateCommand<object?>(x => received = x),
            CommandParameter = "parameter",
            Duration = TimeSpan.FromMilliseconds(10)
        });

        // Act
        RaisePressed(button, pointer, RawInputModifiers.LeftMouseButton, PointerUpdateKind.LeftButtonPressed);
        TestDispatcher.ProcessUntil(() => received is not null, Timeout);

        // Assert
        Assert.Equal("parameter", received);
    }

    [Fact]
    public void DoesNotExecuteCommandWhenReleasedBeforeDuration()
    {
        // Arrange
        using var pointer = CreatePointer();
        var button = new Button();
        var count = 0;
        Interaction.GetBehaviors(button).Add(new LongPressBehavior
        {
            Command = new DelegateCommand(() => count++),
            Duration = TimeSpan.FromMilliseconds(50)
        });

        // Act
        RaisePressed(button, pointer, RawInputModifiers.LeftMouseButton, PointerUpdateKind.LeftButtonPressed);
        RaiseReleased(button, pointer);
        TestDispatcher.ProcessUntil(static () => false, Wait);

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void DoesNotExecuteCommandWhenPressedWithRightButton()
    {
        // Arrange
        using var pointer = CreatePointer();
        var button = new Button();
        var count = 0;
        Interaction.GetBehaviors(button).Add(new LongPressBehavior
        {
            Command = new DelegateCommand(() => count++),
            Duration = TimeSpan.FromMilliseconds(10)
        });

        // Act
        RaisePressed(button, pointer, RawInputModifiers.RightMouseButton, PointerUpdateKind.RightButtonPressed);
        TestDispatcher.ProcessUntil(static () => false, Wait);

        // Assert
        Assert.Equal(0, count);
    }

    [Fact]
    public void ExecutesCommandForControl()
    {
        // Arrange
        using var pointer = CreatePointer();
        var border = new Border();
        var count = 0;
        Interaction.GetBehaviors(border).Add(new LongPressBehavior
        {
            Command = new DelegateCommand(() => count++),
            Duration = TimeSpan.FromMilliseconds(10)
        });

        // Act
        RaisePressed(border, pointer, RawInputModifiers.LeftMouseButton, PointerUpdateKind.LeftButtonPressed);
        TestDispatcher.ProcessUntil(() => count > 0, Timeout);

        // Assert
        Assert.Equal(1, count);
    }

    [Fact]
    public void DoesNotExecuteCommandAfterDetached()
    {
        // Arrange
        using var pointer = CreatePointer();
        var button = new Button();
        var count = 0;
        var behavior = new LongPressBehavior
        {
            Command = new DelegateCommand(() => count++),
            Duration = TimeSpan.FromMilliseconds(50)
        };
        Interaction.GetBehaviors(button).Add(behavior);
        RaisePressed(button, pointer, RawInputModifiers.LeftMouseButton, PointerUpdateKind.LeftButtonPressed);

        // Act
        Interaction.GetBehaviors(button).Remove(behavior);
        TestDispatcher.ProcessUntil(static () => false, Wait);

        // Assert
        Assert.Equal(0, count);
    }
}
