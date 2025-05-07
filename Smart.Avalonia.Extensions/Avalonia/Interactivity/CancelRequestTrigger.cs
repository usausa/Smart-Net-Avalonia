namespace Smart.Avalonia.Interactivity;

using System.ComponentModel;

using global::Avalonia.Xaml.Interactivity;

public sealed class CancelRequestTrigger : RequestTriggerBase<CancelRequestTrigger, CancelEventArgs>
{
    protected override void OnEventRequest(object? sender, CancelEventArgs e)
    {
        Interaction.ExecuteActions(AssociatedObject, Actions, e);
    }
}
