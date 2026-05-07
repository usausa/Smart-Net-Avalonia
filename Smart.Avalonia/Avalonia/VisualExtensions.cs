namespace Smart.Avalonia;

using global::Avalonia;
using global::Avalonia.Media.Imaging;
using global::Avalonia.VisualTree;

public static class VisualExtensions
{
    // ------------------------------------------------------------
    // Children
    // ------------------------------------------------------------

    public static IEnumerable<T> FindDescendants<T>(this Visual source)
        where T : Visual
    {
        foreach (var child in source.GetVisualChildren())
        {
            if (child is T typedChild)
            {
                yield return typedChild;
            }

            foreach (var descendant in child.FindDescendants<T>())
            {
                yield return descendant;
            }
        }
    }

    public static IEnumerable<T> FindDescendants<T>(this Visual source, Func<T, bool> predicate)
        where T : Visual
    {
        foreach (var child in source.FindDescendants<T>())
        {
            if (predicate(child))
            {
                yield return child;
            }
        }
    }

    public static T? FindDescendant<T>(this Visual source)
        where T : Visual =>
        source.FindDescendants<T>().FirstOrDefault();

    public static T? FindDescendant<T>(this Visual source, Func<T, bool> predicate)
        where T : Visual =>
        source.FindDescendants<T>().FirstOrDefault(predicate);

    // ------------------------------------------------------------
    // Ancestry
    // ------------------------------------------------------------

    public static bool IsDescendantOf(this Visual element, Visual ancestor)
    {
        var current = element.GetVisualParent();
        while (current is not null)
        {
            if (current == ancestor)
            {
                return true;
            }

            current = current.GetVisualParent();
        }

        return false;
    }

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
