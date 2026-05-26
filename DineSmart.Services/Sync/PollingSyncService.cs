namespace DineSmart.Services.Sync;

public class PollingSyncService : IDisposable
{
    private readonly System.Timers.Timer _timer;

    public event EventHandler? Tick;

    public PollingSyncService(double intervalMs = 3000)
    {
        _timer = new System.Timers.Timer(intervalMs);
        _timer.Elapsed += (_, _) => Tick?.Invoke(this, EventArgs.Empty);
    }

    public void Start() => _timer.Start();
    public void Stop() => _timer.Stop();

    public void Dispose()
    {
        _timer.Stop();
        _timer.Dispose();
    }
}
