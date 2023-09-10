namespace DotMatrix.Core;

public sealed class Memory
{
    private readonly byte[] _memory = new byte[0xFFFF];

    public byte this[uint addr]
    {
        get => _memory[addr];
        set => _memory[addr] = value;
    }

    // public void Write(ushort addr, byte data)
    // {
    //     // Don't allow writing to ROM or Prohibited area.
    //     if (addr is <= MemoryRegion.RomBankNNEnd or >= MemoryRegion.Prohibited and <= MemoryRegion.ProhibitedEnd)
    //     {
    //         throw new AddressException(addr);
    //     }
    //
    //     _memory[addr] = data;
    //
    //     // Echo RAM is mirrored with the start of Work RAM.
    //     if (addr is >= MemoryRegion.EchoRam and <= MemoryRegion.EchoRamEnd)
    //     {
    //         // EchoRam.Start - WorkRam.Start = 0x2000
    //         Write((ushort)(addr - 0x2000), data);
    //     }
    // }
}
