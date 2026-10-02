namespace Smart.Avalonia.Interactivity;

using System.Diagnostics;

using global::Avalonia.Threading;

internal static class TestDispatcher
{
    public static void ProcessUntil(Func<bool> condition, TimeSpan timeout)
    {
        var frame = new DispatcherFrame();
        var watch = Stopwatch.StartNew();
        var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1) };
        timer.Tick += (_, _) =>
        {
            if (condition() || (watch.Elapsed >= timeout))
            {
                timer.Stop();
                frame.Continue = false;
            }
        };
        timer.Start();
        Dispatcher.CurrentDispatcher.PushFrame(frame);
    }
}
