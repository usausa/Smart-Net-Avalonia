namespace Smart.Avalonia.Input;

using System.Reactive.Linq;
using System.Windows.Input;

public static class CommandExtensions
{
    public static IObservable<EventArgs> CanExecuteChangedAsObservable(this ICommand command)
    {
        return Observable.FromEvent<EventHandler, EventArgs>(
            static h => (_, e) => h(e),
            h => command.CanExecuteChanged += h,
            h => command.CanExecuteChanged -= h);
    }
}
