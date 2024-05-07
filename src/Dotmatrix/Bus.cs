namespace DotMatrix;

public interface IBus
{
    byte this[ushort addr] { get; set; }
}

public class Bus(IMemory memory, byte[]? bios, byte[]? cart) : IBus
{
    private readonly IMemory _memory = memory;
    private readonly byte[]? _bios = bios;
    private readonly byte[]? _cart = cart;

    private bool BootRomIsAttached { get; set; } = bios is not null;

    public byte this[ushort addr]
    {
        get => addr switch
        {
            <= MemoryMap.BootRom.End when BootRomIsAttached => _bios![addr],
            _ => _memory[addr],
        };

        set
        {
            Console.WriteLine($" -> Wrote ${value:X2} to address ${addr:X4}");
            _memory[addr] = value;
        }
    }
}
