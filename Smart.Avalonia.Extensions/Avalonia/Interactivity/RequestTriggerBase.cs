namespace Smart.Avalonia.Interactivity;

using global::Avalonia;
using global::Avalonia.Controls;
using global::Avalonia.Interactivity;
using global::Avalonia.Xaml.Interactivity;

using Smart.Mvvm.Messaging;

public abstract class RequestTriggerBase<TTrigger, TEventArgs> : StyledElementTrigger<Control>
    where TTrigger : RequestTriggerBase<TTrigger, TEventArgs>
    where TEventArgs : EventArgs
{
    public static readonly StyledProperty<IEventRequest<TEventArgs>?> RequestProperty =
        AvaloniaProperty.Register<TTrigger, IEventRequest<TEventArgs>?>(nameof(Request));

    public IEventRequest<TEventArgs>? Request
    {
        get => GetValue(RequestProperty);
        set => SetValue(RequestProperty, value);
    }

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
        AddRequested(Request);
    }

    private void Unsubscribe()
    {
        if (!subscribed)
        {
            return;
        }

        subscribed = false;
        RemoveRequested(Request);
    }

    private void AddRequested(IEventRequest<TEventArgs>? request)
    {
        if (request is not null)
        {
            request.Requested += EventRequestOnRequested;
        }
    }

    private void RemoveRequested(IEventRequest<TEventArgs>? request)
    {
        if (request is not null)
        {
            request.Requested -= EventRequestOnRequested;
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == RequestProperty)
        {
            if (!subscribed)
            {
                return;
            }

            RemoveRequested(change.OldValue as IEventRequest<TEventArgs>);
            AddRequested(change.NewValue as IEventRequest<TEventArgs>);
        }
    }

    private void EventRequestOnRequested(object? sender, TEventArgs e)
    {
        OnEventRequest(sender, e);
    }

    protected abstract void OnEventRequest(object? sender, TEventArgs e);
}
