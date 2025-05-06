namespace Smart.Avalonia.Interactivity.Messaging;

using global::Avalonia;
using global::Avalonia.Controls;
using global::Avalonia.Interactivity;
using global::Avalonia.Xaml.Interactivity;

using Smart.Mvvm.Messaging;

public sealed class MessageTrigger : StyledElementTrigger<Control>
{
    public static readonly StyledProperty<IMessenger?> MessengerProperty =
        AvaloniaProperty.Register<MessageTrigger, IMessenger?>(nameof(Messenger));

    public IMessenger? Messenger
    {
        get => GetValue(MessengerProperty);
        set => SetValue(MessengerProperty, value);
    }

    public string? Label { get; set; }

    public Type? MessageType { get; set; }

    protected override void OnAttachedToVisualTree()
    {
        base.OnAttachedToVisualTree();

        if (AssociatedObject is not null)
        {
            AssociatedObject.Unloaded += OnUnloaded;
        }
    }

    protected override void OnDetachedFromVisualTree()
    {
        if (AssociatedObject is not null)
        {
            AssociatedObject!.Unloaded -= OnUnloaded;
        }

        base.OnDetachedFromVisualTree();
    }

    private void OnUnloaded(object? sender, RoutedEventArgs routedEventArgs)
    {
        if (Messenger is not null)
        {
            Messenger.Received -= MessengerOnReceived;
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == MessengerProperty)
        {
            OnMessengerChanged(change);
        }
    }

    private static void OnMessengerChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.OldValue == e.NewValue)
        {
            return;
        }

        if (e.Sender is not MessageTrigger trigger)
        {
            return;
        }

        if (e.OldValue is Messenger oldMessenger)
        {
            oldMessenger.Received -= trigger.MessengerOnReceived;
        }

        if (e.NewValue is Messenger newMessenger)
        {
            newMessenger.Received += trigger.MessengerOnReceived;
        }
    }

    private void MessengerOnReceived(object? sender, MessengerEventArgs e)
    {
        if (!IsEnabled)
        {
            return;
        }

        if (((Label is null) || Label.Equals(e.Label, StringComparison.Ordinal)) &&
            ((MessageType is null) || ((e.MessageType is not null) && MessageType.IsAssignableFrom(e.MessageType))))
        {
            Interaction.ExecuteActions(AssociatedObject, Actions, e.Message);
        }
    }
}
