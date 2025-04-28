namespace Smart.Avalonia.Interactivity;

using global::Avalonia.Xaml.Interactivity;

using Smart.Avalonia.Messaging;

public sealed class EventRequestTrigger : RequestTriggerBase<EventRequestTrigger, ParameterEventArgs>
{
    protected override void OnEventRequest(object? sender, ParameterEventArgs e)
    {
        Interaction.ExecuteActions(AssociatedObject, Actions, e.Parameter);
    }
}
