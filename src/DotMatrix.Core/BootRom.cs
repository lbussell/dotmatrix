namespace DotMatrix.Core;

public sealed class BootRom : IReadableMemory
{
    private const int _length = ConsoleSpecs.BootRomLengthInBytes;

    private readonly byte[] _data = new byte[_length];

    public BootRom(byte[] data)
    {
        if (data.Length > _length)
        {
            throw new ArgumentOutOfRangeException(
                nameof(data),
                $"BootRom was larger than expected size ({data.Length} > {_length})");
        }

        data.CopyTo(_data.AsSpan());
        Console.WriteLine("Loaded Boot ROM");
    }

    public int Length => _length;

    public byte Read8(ushort addr) => _data[addr];

    public ushort Read16(ushort addr) => (ushort)(_data[addr] | _data[addr + 1] << 8);
}
