using System.Runtime.InteropServices;

namespace DotMatrix.Core;

public class Memory
{
    public static (ushort Start, ushort End) RomBank00 => (0x0000, 0x3FFF);
    public static (ushort Start, ushort End) RomBank01NN => (0x4000, 0x7FFF);
    public static (ushort Start, ushort End) VRam => (0x8000, 0x9FFF);
    public static (ushort Start, ushort End) ExtRam => (0xA000, 0xBFFF);
    public static (ushort Start, ushort End) WorkRam => (0xC000, 0xCFFF);
    public static (ushort Start, ushort End) WorkRam2 => (0xD000, 0xDFFF);
    public static (ushort Start, ushort End) EchoRam => (0xE000, 0xFDFF);
    public static (ushort Start, ushort End) OAM => (0xFE00, 0xFE9F);
    public static (ushort Start, ushort End) Prohibited => (0xFEA0, 0xFEFF);
    public static (ushort Start, ushort End) IOReg => (0xFF00, 0xFEFF);
    public static (ushort Start, ushort End) HRam => (0xFF80, 0xFFFE);
    public static (ushort Start, ushort End) IEReg => (0xFFFF, 0xFFFF);

    private byte[] _memory = new byte[0xFFFF];
    // private Memory<byte> _memory = new(new byte[0xFFFF]);

    public Memory()
    {

    }

    // public void LoadCartridge(byte[] cartridgeData)
    public void LoadCartridge(ReadOnlySpan<byte> cartridgeData)
    {
        if (cartridgeData.Length > RomBank01NN.End)
        {
            cartridgeData = cartridgeData.Slice(RomBank00.Start, RomBank01NN.End - RomBank00.Start);
        }

        cartridgeData.CopyTo(_memory.AsSpan());

        // Debug: Print boot ROM
        for (int i = 0; i < 256; i += 2)
        {
            PrintShort(i);
        }
    }

    private void PrintShort(int index) =>
        Console.WriteLine(ByteToString(_memory[index]) + ByteToString(_memory[index + 1]));

    private void PrintByte(int index) =>
        Console.WriteLine(ByteToString(_memory[index]));

    private static string ByteToString(byte b) => b.ToString("X2");
}
