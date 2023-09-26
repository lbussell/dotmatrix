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
        get =>
            addr switch
            {
                _ when BootRomIsAttached && addr < MemoryRegion.BootRomEnd => _bootRom!.Read8(addr),
                _ => _memory[addr],
            };
        set => throw new NotImplementedException();
    }

    public byte ReadInc8(ref ushort addr)
    {
        return this[addr++];
    }

    public ushort ReadInc16(ref ushort addr)
    {
        return (ushort)(this[addr++] | this[addr++] << 8);
    }
}
