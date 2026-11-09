namespace Smart.Avalonia;

using global::Avalonia;
using global::Avalonia.Media.Imaging;
using global::Avalonia.VisualTree;

public static class VisualExtensions
{
    // ------------------------------------------------------------
    // Descendant
    // ------------------------------------------------------------

    public static IEnumerable<T> FindDescendants<T, TState>(this Visual source, TState state, Func<T, TState, bool> predicate)
        where T : Visual
    {
        foreach (var descendant in source.GetVisualDescendants())
        {
            if ((descendant is T typedDescendant) && predicate(typedDescendant, state))
            {
                yield return typedDescendant;
            }
        }
    }

    public static T? FindDescendant<T, TState>(this Visual source, TState state, Func<T, TState, bool> predicate)
        where T : Visual
    {
        foreach (var descendant in source.GetVisualDescendants())
        {
            if ((descendant is T typedDescendant) && predicate(typedDescendant, state))
            {
                return typedDescendant;
            }
        }

        return null;
    }

    public static T? FindDescendantOrSelf<T, TState>(this Visual source, TState state, Func<T, TState, bool> predicate)
        where T : Visual =>
        (source is T self) && predicate(self, state) ? self : source.FindDescendant(state, predicate);

    // ------------------------------------------------------------
    // Ascendant
    // ------------------------------------------------------------

    public static T? FindAscendant<T, TState>(this Visual source, TState state, Func<T, TState, bool> predicate)
        where T : Visual
    {
        var parent = source.GetVisualParent();
        while (parent is not null)
        {
            if ((parent is T typedParent) && predicate(typedParent, state))
            {
                return typedParent;
            }

            parent = parent.GetVisualParent();
        }

        return null;
    }

    public static T? FindAscendantOrSelf<T, TState>(this Visual source, TState state, Func<T, TState, bool> predicate)
        where T : Visual =>
        (source is T self) && predicate(self, state) ? self : source.FindAscendant(state, predicate);

    // ------------------------------------------------------------
    // Render
    // ------------------------------------------------------------

    public static RenderTargetBitmap RenderToBitmap(this Visual visual) =>
        visual.RenderToBitmap(1d);

    public static RenderTargetBitmap RenderToBitmap(this Visual visual, double scale)
    {
        var bounds = visual.Bounds;
        var renderWidth = (int)(bounds.Width * scale);
        var renderHeight = (int)(bounds.Height * scale);

        var renderTarget = new RenderTargetBitmap(new PixelSize(renderWidth, renderHeight), new Vector(96 * scale, 96 * scale));

        using var context = renderTarget.CreateDrawingContext();
        context.PushTransform(Matrix.CreateScale(scale, scale));
        visual.Render(context);

        return renderTarget;
    }
}
