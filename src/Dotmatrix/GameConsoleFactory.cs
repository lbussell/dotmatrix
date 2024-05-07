namespace DotMatrix;

public abstract class GameConsoleFactory()
{
    public static IGameConsole Create(byte[]? bios, byte[]? cart)
    {
        IMemory memory = new Memory();
        IBus bus = new Bus(memory, bios, cart);
        ICpu cpu = new Cpu(bus);
        return new GameConsole(bus, cpu, memory);
    }
}
