namespace Smart.Avalonia;

using System.Reactive.Linq;

using global::Avalonia.Interactivity;

public static class InteractiveExtensions
{
    public static IObservable<RoutedEventArgs> RoutedEventAsObservable(
        this Interactive source,
        RoutedEvent routedEvent,
        RoutingStrategies routes = RoutingStrategies.Direct | RoutingStrategies.Bubble,
        bool handledEventsToo = false)
    {
        return Observable.Create<RoutedEventArgs>(observer =>
        {
            EventHandler<RoutedEventArgs> handler = (_, e) => observer.OnNext(e);
            source.AddHandler(routedEvent, handler, routes, handledEventsToo);
            return () => source.RemoveHandler(routedEvent, handler);
        });
    }

    public static IObservable<TEventArgs> RoutedEventAsObservable<TEventArgs>(
        this Interactive source,
        RoutedEvent<TEventArgs> routedEvent,
        RoutingStrategies routes = RoutingStrategies.Direct | RoutingStrategies.Bubble,
        bool handledEventsToo = false)
        where TEventArgs : RoutedEventArgs
    {
        return Observable.Create<TEventArgs>(observer =>
        {
#pragma warning disable IDE0039
            EventHandler<TEventArgs> handler = (_, e) => observer.OnNext(e);
#pragma warning restore IDE0039
            source.AddHandler(routedEvent, handler, routes, handledEventsToo);
            return () => source.RemoveHandler(routedEvent, handler);
        });
    }
}
