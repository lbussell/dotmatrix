namespace DotMatrix.Core;

public interface IBus
{
    byte this[ushort key] { get; set; }
}

internal class Bus : IBus
{
    #region MemoryMap
    private const int MemorySize = 0xFFFF;
    private const int BootRom = 0x0000;
    private const int BootRomEnd = 0x00FF;
    private const int ExecutionStart = 0x0100;
    private const int RomBank00 = 0x0000;
    private const int RomBank01NN = 0x4000;
    private const int RomBankEnd = 0x7FFF;
    #endregion

    private readonly byte[] _memory = new byte[MemorySize];
    private readonly byte[]? _bios;
    private readonly byte[] _rom;
    private bool _bootRomIsAttached;

    public Bus(byte[] rom, byte[]? bios = null)
    {
        _rom = rom;
        _bios = bios ?? null;
        _bootRomIsAttached = bios != null;
    }

    public byte this[ushort key]
    {
        get => MMap(key)[key];
        set => MMap(key)[key] = value;
    }

    private byte[] MMap(ushort address) => address switch
        {
            <= BootRomEnd when _bootRomIsAttached => _bios!,
            <= RomBankEnd => _rom,
            _ => _memory,
        };
}
