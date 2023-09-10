namespace DotMatrix.Core;

public sealed class BootRom
{
    public const int SizeInBytes = 0x0100;

    private readonly byte[] _data = new byte[SizeInBytes];

    public BootRom(byte[] data)
    {
        if (data.Length > SizeInBytes)
        {
            throw new ArgumentOutOfRangeException(
                nameof(data),
                $"BootRom was larger than expected size {SizeInBytes}");
        }

        data.CopyTo(_data.AsSpan());
        Console.WriteLine("Loaded Boot ROM");
    }

    public byte this[uint addr]
    {
        get => _data[addr];
        set => throw new InvalidOperationException("Cannot write to Boot ROM.");
    }
}
