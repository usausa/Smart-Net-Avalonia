namespace Smart.Avalonia.Input;

using System.Runtime.CompilerServices;
using System.Windows.Input;

using Smart.Avalonia.Internal;

public sealed class DelegateCommand : IObserveCommand
{
    public event EventHandler? CanExecuteChanged;

    private readonly Action execute;

    private readonly Func<bool> canExecute;

    public DelegateCommand(Action execute)
        : this(execute, Functions.True)
    {
    }

    public DelegateCommand(Action execute, Func<bool> canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    bool ICommand.CanExecute(object? parameter) => canExecute();

    void ICommand.Execute(object? parameter) => execute();

#pragma warning disable CA1030
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
#pragma warning restore CA1030
}

public sealed class DelegateCommand<T> : IObserveCommand
{
    public event EventHandler? CanExecuteChanged;

    private readonly Action<T> execute;

    private readonly Func<T, bool> canExecute;

    public DelegateCommand(Action<T> execute)
        : this(execute, Functions<T>.True)
    {
    }

    public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    bool ICommand.CanExecute(object? parameter) => canExecute(Cast(parameter));

    void ICommand.Execute(object? parameter) => execute(Cast(parameter));

    private static T Cast(object? parameter)
    {
        if (typeof(T).IsValueType && (parameter is null))
        {
            return default!;
        }

        return (T)parameter!;
    }

#pragma warning disable CA1030
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
#pragma warning restore CA1030
}
