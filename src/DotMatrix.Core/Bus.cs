namespace DotMatrix.Core;

internal class Bus : IBus
{
    #region Memory Map

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

        // https://github.com/robert/gameboy-doctor?tab=readme-ov-file#2-make-2-tweaks-to-your-emulator
        // Hardcode LY Reg to make outputs more deterministic for now
        _memory[0xFF44] = 0x90;
    }

    public byte this[ushort address]
    {
        get => MMap(address)[address % MemorySize];
        set => MMap(address)[address % MemorySize] = value;
    }

    private byte[] MMap(ushort address)
    {
        return address switch
        {
            <= BootRomEnd when _bootRomIsAttached => _bios!,
            <= RomBankEnd => _rom,
            _ => _memory,
        };
    }
}
