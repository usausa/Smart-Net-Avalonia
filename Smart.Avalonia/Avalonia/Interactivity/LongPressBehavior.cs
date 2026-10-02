namespace Smart.Avalonia.Interactivity;

using System.Windows.Input;

using global::Avalonia;
using global::Avalonia.Controls;
using global::Avalonia.Input;
using global::Avalonia.Interactivity;
using global::Avalonia.Threading;
using global::Avalonia.Xaml.Interactivity;

public sealed class LongPressBehavior : Behavior<Control>
{
    public static readonly StyledProperty<ICommand?> CommandProperty =
        AvaloniaProperty.Register<LongPressBehavior, ICommand?>(nameof(Command));

    public static readonly StyledProperty<object?> CommandParameterProperty =
        AvaloniaProperty.Register<LongPressBehavior, object?>(nameof(CommandParameter));

    public static readonly StyledProperty<TimeSpan> DurationProperty =
        AvaloniaProperty.Register<LongPressBehavior, TimeSpan>(nameof(Duration), TimeSpan.FromMilliseconds(500));

    public ICommand? Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public TimeSpan Duration
    {
        get => GetValue(DurationProperty);
        set => SetValue(DurationProperty, value);
    }

    private DispatcherTimer? timer;

    protected override void OnAttached()
    {
        base.OnAttached();

        if (AssociatedObject is not null)
        {
            AssociatedObject.AddHandler(InputElement.PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel);
            AssociatedObject.AddHandler(InputElement.PointerReleasedEvent, OnPointerReleased, RoutingStrategies.Tunnel);
            AssociatedObject.PointerCaptureLost += OnPointerCaptureLost;
        }
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject is not null)
        {
            AssociatedObject.RemoveHandler(InputElement.PointerPressedEvent, OnPointerPressed);
            AssociatedObject.RemoveHandler(InputElement.PointerReleasedEvent, OnPointerReleased);
            AssociatedObject.PointerCaptureLost -= OnPointerCaptureLost;
        }
        if (timer is not null)
        {
            timer.Stop();
            timer.Tick -= OnTick;
            timer = null;
        }

        base.OnDetaching();
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Properties.IsLeftButtonPressed)
        {
            if (timer is null)
            {
                timer = new DispatcherTimer();
                timer.Tick += OnTick;
            }

            timer.Stop();
            timer.Interval = Duration;
            timer.Start();
        }
    }

    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        timer?.Stop();
    }

    private void OnPointerCaptureLost(object? sender, PointerCaptureLostEventArgs e)
    {
        timer?.Stop();
    }

    private void OnTick(object? sender, EventArgs e)
    {
        timer?.Stop();

        var command = Command;
        if (command is null)
        {
            return;
        }

        var parameter = CommandParameter;
        if (command.CanExecute(parameter))
        {
            command.Execute(parameter);
        }
    }
}
