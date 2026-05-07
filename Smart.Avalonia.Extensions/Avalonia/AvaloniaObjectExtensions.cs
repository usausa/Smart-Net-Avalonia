namespace Smart.Avalonia;

using System.Reactive.Linq;

using global::Avalonia;

public static class AvaloniaObjectExtensions
{
    public static IObservable<TValue> PropertyChangedAsObservable<TValue>(
        this AvaloniaObject source,
        AvaloniaProperty<TValue> property)
    {
        return Observable.Create<TValue>(observer =>
        {
            void OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
            {
                if (e.Property == property && e.NewValue is TValue newValue)
                {
                    observer.OnNext(newValue);
                }
            }

            source.PropertyChanged += OnPropertyChanged;

            return () => source.PropertyChanged -= OnPropertyChanged;
        });
    }
}
