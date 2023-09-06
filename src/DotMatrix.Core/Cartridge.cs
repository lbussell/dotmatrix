namespace DotMatrix.Core;

public class Cartridge
{
    private static readonly MemoryRegion TitleRegion = new(0x0134, 0x0143);
    private static readonly MemoryRegion RomSize = new(0x0148, 0x0148);

    public string Title { get; init; }
    public int SizeInBytes { get; init; }

    private readonly byte[] _data;
    private readonly byte[]? _bootRom;
    private readonly int _numBanks;

    public Cartridge(byte[] data)
    {
        // Read information from ROM header.
        Title = DecodeTitle(data);
        (SizeInBytes, _numBanks) = DecodeSize(data);

        // Copy ROM data to memory.
        _data = new byte[SizeInBytes];
        data.CopyTo(_data.AsSpan());

        Console.WriteLine($"Loaded ROM:\nTitle: {Title}\nSize: {SizeInBytes}B\nBanks: {_numBanks}");
    }

    public byte this[uint addr]
    {
        get => _data[addr];
        set => _data[addr] = value;
    }

    private static (int sizeInBytes, int numBanks) DecodeSize(byte[] data)
    {
        byte value = data[RomSize.Start];
        int sizeInBytes = 32 * 1024 * (1 << value);
        int numBanks = (int) Math.Pow(2, value + 1);
        return (sizeInBytes, numBanks);
    }

    private static string DecodeTitle(byte[] data)
    {
        // TODO: Account for manufacturer codes:
        // https://gbdev.io/pandocs/The_Cartridge_Header.html#013f-0142--manufacturer-code
        return System.Text.Encoding.ASCII.GetString(data, TitleRegion.Start, TitleRegion.End);
    }
}
