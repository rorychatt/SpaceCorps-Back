namespace SpaceCorps.Business.Engine;

public class Game
{
    private readonly Timer _timer;
    
    public Game()
    {
        _timer = new Timer(Tick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
    }

    private void Tick(object? state)
    {
        
    }

    private void Start()
    {
        
    }

    public void Stop()
    {
        _timer.Change(Timeout.Infinite, Timeout.Infinite);
    }
}