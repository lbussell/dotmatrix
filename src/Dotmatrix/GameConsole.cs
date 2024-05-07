namespace DotMatrix;

public interface IGameConsole
{
    void Start();
}

public sealed class GameConsole(
    IBus bus,
    ICpu cpu,
    IMemory memory) : IGameConsole
{
    private readonly IBus _bus = bus;
    private readonly IMemory _memory = memory;

    public void Start()
    {
        cpu.ExecuteFrames();
    }
}
