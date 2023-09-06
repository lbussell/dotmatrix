using System.ComponentModel;
using System.Runtime.InteropServices;

namespace DotMatrix.Core;

public readonly record struct MemoryRegion(ushort Start, ushort End)
{
    public bool Contains(ushort addr) => Start <= addr && addr <= End;
};

public class Memory
{
    public static MemoryRegion RomBank00 => new(0x0000, 0x3FFF);
    public static MemoryRegion RomBank01NN => new(0x4000, 0x7FFF);
    public static MemoryRegion VRam => new(0x8000, 0x9FFF);
    public static MemoryRegion ExtRam => new(0xA000, 0xBFFF);
    public static MemoryRegion WorkRam => new(0xC000, 0xCFFF);
    public static MemoryRegion WorkRam2 => new(0xD000, 0xDFFF);
    // Echo is a mirror of C000-DDFF but should not be written to.
    public static MemoryRegion EchoRam => new(0xE000, 0xFDFF);
    public static MemoryRegion OAM => new(0xFE00, 0xFE9F);
    public static MemoryRegion Prohibited => new(0xFEA0, 0xFEFF);
    public static MemoryRegion IOReg => new(0xFF00, 0xFEFF);
    public static MemoryRegion HRam => new(0xFF80, 0xFFFE);
    public static MemoryRegion IEReg => new(0xFFFF, 0xFFFF);

    private readonly byte[] _memory = new byte[0xFFFF];
    private readonly byte[] _cartridge = new byte[0x200000];

    public Memory()
    {
    }

    public void Write(ushort addr, byte data)
    {
        // Don't allow writing to ROM or Prohibited area.
        if (addr <= RomBank01NN.End || Prohibited.Contains(addr))
        {
            throw new AddressException(addr);
        }

        _memory[addr] = data;

        // Echo RAM is mirrored with the start of Work RAM.
        if (EchoRam.Contains(addr))
        {
            // EchoRam.Start - WorkRam.Start = 0x2000
            Write((ushort)(addr - 0x2000), data);
        }
    }

    private void PrintShort(int index) =>
        Console.WriteLine(ByteToString(_memory[index]) + ByteToString(_memory[index + 1]));

    private void PrintByte(int index) =>
        Console.WriteLine(ByteToString(_memory[index]));

    public static string ByteToString(byte b) => b.ToString("X2");
}
