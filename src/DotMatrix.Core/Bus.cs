namespace DotMatrix.Core;

internal class Bus : IBus
{
    private readonly byte[] _memory = new byte[Memory.Size];
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

        _memory[Memory.P1] = 0x0F;
    }

    public byte this[ushort address]
    {
        get => Map(address)[address % Memory.Size];
        set => Map(address)[address % Memory.Size] = value;
    }

    private byte[] Map(ushort address)
    {
        return address switch
        {
            <= Memory.BootRomEnd when _bootRomIsAttached => _bios!,
            <= Memory.RomBankEnd => _rom,
            _ => _memory,
        };
    }
}
