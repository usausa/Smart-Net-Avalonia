namespace Smart.Avalonia.Interactivity.Messaging;

using global::Avalonia.Xaml.Interactivity;

using Smart.Mvvm.Messaging;

public sealed class EventRequestTrigger : RequestTriggerBase<EventRequestTrigger, ParameterEventArgs>
{
    protected override void OnEventRequest(object? sender, ParameterEventArgs e)
    {
        Interaction.ExecuteActions(AssociatedObject, Actions, e.Parameter);
    }
}
