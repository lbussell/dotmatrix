namespace DotMatrix.Core;

public static class MemoryRegion
{
    // Reference: https://gbdev.io/pandocs/Memory_Map.html
    // BootRom and RomBanks start at 0x0000.
    public const ushort BootRomEnd = 0x0100; // = 256
    public const ushort RomBank00 = 0x0000;
    public const ushort RomBank00End = 0x3FFF;
    public const ushort RomBank01 = 0x4000;
    public const ushort RomBank01End = 0x7FFF;
    public const ushort RomBankNN = RomBank01;
    public const ushort RomBankNNEnd = RomBank01End;
    public const ushort VRam = 0x8000;
    public const ushort ExtRam = 0xA000;
    public const ushort WorkRam = 0xC000;
    public const ushort WorkRam2 = 0xD000;
    public const ushort EchoRam = 0xE000;
    public const ushort EchoRamEnd = 0xFDFF;
    public const ushort OAM = 0xFE00;
    public const ushort Prohibited = 0xFEA0;
    public const ushort ProhibitedEnd = 0xFEFF;
    public const ushort IOReg = 0xFF00;
    public const ushort Timer = 0xFF05;
    public const ushort TimerModulator = 0xFF06;
    public const ushort TimerController = 0xFF07;
    public const ushort HRam = 0xFF80;
    public const ushort IEReg = 0xFFFF;
}
