namespace Smart.Avalonia.Messaging;

public interface IReactiveMessenger
{
    IObservable<TMessage> Observe<TMessage>();

    void Send<TMessage>(TMessage message);
}
