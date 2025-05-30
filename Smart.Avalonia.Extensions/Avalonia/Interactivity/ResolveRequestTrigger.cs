namespace Smart.Avalonia.Interactivity;

using global::Avalonia.Xaml.Interactivity;

public sealed class ResolveRequestTrigger : RequestTriggerBase<ResolveRequestTrigger, ResolveEventArgs>
{
    protected override void OnEventRequest(object? sender, ResolveEventArgs e)
    {
        Interaction.ExecuteActions(AssociatedObject, Actions, e);
    }
}
