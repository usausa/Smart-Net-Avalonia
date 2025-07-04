namespace Smart.Avalonia.ViewModels;

using System.ComponentModel;
using System.Reactive.Linq;

using Smart.Avalonia.Input;
using Smart.Avalonia.Internal;
using Smart.Mvvm.Messaging;
using Smart.Mvvm.ViewModels;

#pragma warning disable IDE0032
// ReSharper disable ReplaceWithFieldKeyword
public abstract class ExtendViewModelBase : ViewModelBase
{
    // ------------------------------------------------------------
    // Member
    // ------------------------------------------------------------

    private List<IObserveCommand>? commands;

    // ------------------------------------------------------------
    // Constructor
    // ------------------------------------------------------------

    protected ExtendViewModelBase()
    {
    }

    protected ExtendViewModelBase(IBusyState busyState)
        : base(busyState)
    {
    }

    protected ExtendViewModelBase(IMessenger messenger)
        : base(messenger)
    {
    }

    protected ExtendViewModelBase(IBusyState busyState, IMessenger messenger)
        : base(busyState, messenger)
    {
    }

    // ------------------------------------------------------------
    // Override
    // ------------------------------------------------------------

    protected override void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        base.RaisePropertyChanged(args);

        UpdateCommandState();
    }

    // ------------------------------------------------------------
    // Command helper
    // ------------------------------------------------------------

    private void AddCommandObserver(IObserveCommand command)
    {
        if (commands is null)
        {
            commands = new List<IObserveCommand>();
            BusyState.PropertyChanged += BusyStateOnPropertyChanged;
            Disposables.Add(new DelegateDisposable(() => BusyState.PropertyChanged -= BusyStateOnPropertyChanged));
        }
        commands.Add(command);
    }

    private void BusyStateOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IBusyState.IsBusy))
        {
            UpdateCommandState();
        }
    }

    private void UpdateCommandState()
    {
        if (commands is not null)
        {
            foreach (var command in commands)
            {
                command.RaiseCanExecuteChanged();
            }
        }
    }

    protected TCommand Observe<T, TCommand>(IObservable<T> observable, TCommand command)
        where TCommand : IObserveCommand
    {
        Disposables.Add(observable.Subscribe(_ => command.RaiseCanExecuteChanged()));
        return command;
    }

    protected IObserveCommand MakeDelegateCommand(Action execute) =>
        MakeDelegateCommand(execute, Functions.True);

    protected IObserveCommand MakeDelegateCommand(Action execute, Func<bool> canExecute)
    {
        var command = new DelegateCommand(() =>
        {
            execute();
            UpdateCommandState();
        }, () => !BusyState.IsBusy && canExecute());
        AddCommandObserver(command);
        return command;
    }

    protected IObserveCommand MakeDelegateCommand<TParameter>(Action<TParameter> execute) =>
        MakeDelegateCommand(execute, Functions<TParameter>.True);

    protected IObserveCommand MakeDelegateCommand<TParameter>(Action<TParameter> execute, Func<TParameter, bool> canExecute)
    {
        var command = new DelegateCommand<TParameter>(x =>
        {
            execute(x);
            UpdateCommandState();
        }, x => !BusyState.IsBusy && canExecute(x));
        AddCommandObserver(command);
        return command;
    }

    protected IObserveCommand MakeAsyncCommand(Func<Task> execute) =>
        MakeAsyncCommand(execute, Functions.True);

    protected IObserveCommand MakeAsyncCommand(Func<Task> execute, Func<bool> canExecute)
    {
        var command = new AsyncCommand(async () =>
        {
            using (BusyState.Begin())
            {
                await execute().ConfigureAwait(true);
            }
        }, () => !BusyState.IsBusy && canExecute());
        AddCommandObserver(command);
        return command;
    }

    protected IObserveCommand MakeAsyncCommand<TParameter>(Func<TParameter, Task> execute) =>
        MakeAsyncCommand(execute, Functions<TParameter>.True);

    protected IObserveCommand MakeAsyncCommand<TParameter>(Func<TParameter, Task> execute, Func<TParameter, bool> canExecute)
    {
        var command = new AsyncCommand<TParameter>(async x =>
        {
            using (BusyState.Begin())
            {
                await execute(x).ConfigureAwait(true);
            }
        }, x => !BusyState.IsBusy && canExecute(x));
        AddCommandObserver(command);
        return command;
    }

    // ------------------------------------------------------------
    // Reactive helper
    // ------------------------------------------------------------

    protected IObservable<string?> Observe(string name)
    {
        return Observable.FromEvent<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                static h => (_, e) => h(e),
                h => PropertyChanged += h,
                h => PropertyChanged -= h)
            .Where(x => x.PropertyName == name)
            .Select(x => x.PropertyName);
    }

    protected void Subscribe<T>(IObservable<T> observable, Action<T> action)
    {
        Disposables.Add(observable.Subscribe(action));
    }
}
