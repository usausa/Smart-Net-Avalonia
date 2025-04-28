namespace Smart.Avalonia.Interactivity;

using global::Avalonia.Xaml.Interactivity;

using Smart.Avalonia.Messaging;

public sealed class ResolveRequestTrigger : RequestTriggerBase<ResolveRequestTrigger, ResolveEventArgs>
{
    protected override void OnEventRequest(object? sender, ResolveEventArgs e)
    {
        Interaction.ExecuteActions(AssociatedObject, Actions, e);
    }
}
