namespace DotMatrix.Core;

public sealed class Bus
{
    private readonly Cartridge? _cartridge;
    private readonly Memory _memory;
    private readonly BootRom? _bootRom;

    public Bus(Memory memory, Cartridge? cartridge, BootRom? bootRom)
    {
        _memory = memory;
        _cartridge = cartridge;
        _bootRom = bootRom;

        BootRomIsAttached = _bootRom != null;
    }

    public bool BootRomIsAttached { get; set; }

    public byte this[ushort addr]
    {
        get => addr switch
            {
                _ when BootRomIsAttached && addr < MemoryRegion.BootRomEnd => _bootRom!.Read8(addr),
                _ => _memory[addr],
            };

        // Naive implementation. TODO: Implement this properly.
        set
        {
            Console.WriteLine($" -> Wrote ${value:X2} to address ${addr:X4}");
            _memory[addr] = value;
        }
    }

    public byte ReadInc8(ref ushort addr) => this[addr++];

    public ushort Read16(ushort addr) => (ushort)(this[addr++] | this[addr] << 8);

    public ushort ReadInc16(ref ushort addr) => (ushort)(this[addr++] | this[addr++] << 8);

    public void Write16(ushort addr, ushort value)
    {
        this[addr++] = (byte)value;
        this[addr] = (byte)(value >> 8);
    }
}
