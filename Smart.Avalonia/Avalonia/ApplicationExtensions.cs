namespace Smart.Avalonia;

using global::Avalonia;
using global::Avalonia.Controls;
using global::Avalonia.Controls.ApplicationLifetimes;

public static class ApplicationExtensions
{
    public static Window? GetMainWindow(this Application application)
    {
        var desktopLifetime = application.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        return desktopLifetime?.MainWindow;
    }

    public static IReadOnlyList<Window> GetWindows(this Application application)
    {
        var desktopLifetime = application.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        return desktopLifetime?.Windows ?? [];
    }
}
