namespace Smart.Avalonia.Interactivity;

using global::Avalonia;
using global::Avalonia.Controls;
using global::Avalonia.Interactivity;
using global::Avalonia.Xaml.Interactivity;

using Smart.Avalonia.Messaging;

public abstract class RequestTriggerBase<TTrigger, TEventArgs> : StyledElementTrigger<Control>
    where TTrigger : RequestTriggerBase<TTrigger, TEventArgs>
    where TEventArgs : EventArgs
{
    public static readonly StyledProperty<IEventRequest<TEventArgs>?> RequestProperty =
        AvaloniaProperty.Register<TTrigger, IEventRequest<TEventArgs>?>(nameof(Messenger));

    public IEventRequest<TEventArgs>? Request
    {
        get => GetValue(RequestProperty);
        set => SetValue(RequestProperty, value);
    }

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
            AssociatedObject.Unloaded -= OnUnloaded;
        }

        base.OnDetachedFromVisualTree();
    }

    private void OnUnloaded(object? sender, RoutedEventArgs routedEventArgs)
    {
        if (Request is not null)
        {
            Request.Requested -= EventRequestOnRequested;
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == RequestProperty)
        {
            OnRequestChanged(change);
        }
    }

    private static void OnRequestChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.OldValue == e.NewValue)
        {
            return;
        }

        if (e.Sender is not TTrigger trigger)
        {
            return;
        }

        if (e.OldValue is IEventRequest<TEventArgs> oldRequest)
        {
            oldRequest.Requested -= trigger.EventRequestOnRequested;
        }

        if (e.NewValue is IEventRequest<TEventArgs> newRequest)
        {
            newRequest.Requested += trigger.EventRequestOnRequested;
        }
    }

    private void EventRequestOnRequested(object? sender, TEventArgs e)
    {
        OnEventRequest(sender, e);
    }

    protected abstract void OnEventRequest(object? sender, TEventArgs e);
}
