namespace Smart.Avalonia.Interactivity;

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

    private bool subscribed;

    protected override void OnAttachedToVisualTree()
    {
        base.OnAttachedToVisualTree();

        if (AssociatedObject is not null)
        {
            AssociatedObject.Loaded += OnLoaded;
            AssociatedObject.Unloaded += OnUnloaded;

            if (AssociatedObject.IsLoaded)
            {
                Subscribe();
            }
        }
    }

    protected override void OnDetachedFromVisualTree()
    {
        Unsubscribe();

        if (AssociatedObject is not null)
        {
            AssociatedObject.Loaded -= OnLoaded;
            AssociatedObject.Unloaded -= OnUnloaded;
        }

        base.OnDetachedFromVisualTree();
    }

    private void OnLoaded(object? sender, RoutedEventArgs e) => Subscribe();

    private void OnUnloaded(object? sender, RoutedEventArgs e) => Unsubscribe();

    private void Subscribe()
    {
        if (subscribed)
        {
            return;
        }

        subscribed = true;
        AddReceived(Messenger);
    }

    private void Unsubscribe()
    {
        if (!subscribed)
        {
            return;
        }

        subscribed = false;
        RemoveReceived(Messenger);
    }

    private void AddReceived(IMessenger? messenger)
    {
        if (messenger is not null)
        {
            messenger.Received += MessengerOnReceived;
        }
    }

    private void RemoveReceived(IMessenger? messenger)
    {
        if (messenger is not null)
        {
            messenger.Received -= MessengerOnReceived;
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == MessengerProperty)
        {
            if (!subscribed)
            {
                return;
            }

            RemoveReceived(change.OldValue as IMessenger);
            AddReceived(change.NewValue as IMessenger);
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
