namespace SpaceCorps.Business.Engine;

public interface IGame
{
    public void Start();
    public void Tick();
    public void Stop();
}