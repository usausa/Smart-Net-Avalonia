namespace Smart.Avalonia.Interactivity;

using System.Windows.Input;

using global::Avalonia;
using global::Avalonia.Controls;
using global::Avalonia.Threading;
using global::Avalonia.Xaml.Interactivity;

public sealed class TypingStoppedBehavior : Behavior<TextBox>
{
    public static readonly StyledProperty<ICommand?> CommandProperty =
        AvaloniaProperty.Register<TypingStoppedBehavior, ICommand?>(nameof(Command));

    public static readonly StyledProperty<object?> CommandParameterProperty =
        AvaloniaProperty.Register<TypingStoppedBehavior, object?>(nameof(CommandParameter));

    public static readonly StyledProperty<TimeSpan> DelayProperty =
        AvaloniaProperty.Register<TypingStoppedBehavior, TimeSpan>(nameof(Delay), TimeSpan.FromSeconds(1));

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

    public TimeSpan Delay
    {
        get => GetValue(DelayProperty);
        set => SetValue(DelayProperty, value);
    }

    private DispatcherTimer? timer;

    protected override void OnAttached()
    {
        base.OnAttached();

        if (AssociatedObject is not null)
        {
            AssociatedObject.PropertyChanged += OnPropertyChanged;
        }
    }

    protected override void OnDetaching()
    {
        if (AssociatedObject is not null)
        {
            AssociatedObject.PropertyChanged -= OnPropertyChanged;
        }
        if (timer is not null)
        {
            timer.Stop();
            timer.Tick -= OnTick;
            timer = null;
        }

        base.OnDetaching();
    }

    private void OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == TextBox.TextProperty)
        {
            if (timer is null)
            {
                timer = new DispatcherTimer();
                timer.Tick += OnTick;
            }

            timer.Stop();
            timer.Interval = Delay;
            timer.Start();
        }
    }

    private void OnTick(object? sender, EventArgs e)
    {
        timer?.Stop();

        var command = Command;
        if (command is null)
        {
            return;
        }

        var commandParameter = CommandParameter;
        var parameter = (commandParameter is not null) || IsSet(CommandParameterProperty) ? commandParameter : AssociatedObject?.Text;
        if (command.CanExecute(parameter))
        {
            command.Execute(parameter);
        }
    }
}
