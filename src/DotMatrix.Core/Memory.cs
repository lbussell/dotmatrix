namespace DotMatrix.Core;

internal static class Memory
{
    public const int Size = 0x10000;
    public const int BootRom = 0x0000;
    public const int BootRomEnd = 0x00FF;
    public const int ExecutionStart = 0x0100;
    public const int RomBank00 = 0x0000;
    public const int RomBank01NN = 0x4000;
    public const int RomBankEnd = 0x7FFF;
    public const int IOReg = 0xFF00;
    public const int IORegEnd = 0xFF7F;
    public const int HRam = 0xFF80;
    public const int HRamEnd = 0xFFFE;
    public const int InterruptEnable = 0xFFFF;

    #region I/O Registers
    public const int P1 = 0xFF00; // Joypad
    public const int SB = 0xFF01; // Serial transfer data
    public const int SC = 0xFF02; // Serial transfer control
    #endregion

    #region Timer Registers
    public const int TimerRegisters = DIV;
    public const int DIV = 0xFF04;
    public const int TIMA = 0xFF05;
    public const int TMA = 0xFF06;
    public const int TAC = 0xFF07;
    #endregion

    #region Interrupts
    public const ushort InterruptFlag = 0xFF0F;

    public static readonly ushort[] Interrupts =
    [
        VBlank,
        Stat,
        Timer,
        Serial,
        Joypad,
    ];

    private const ushort VBlank = 0x0040;
    private const ushort Stat = 0x0048;
    private const ushort Timer = 0x0050;
    private const ushort Serial = 0x0058;
    private const ushort Joypad = 0x0060;

    public const byte VBlankFlag = 0b_0000_0001;
    public const byte StatFlag = 0b_0000_0010;
    public const byte TimerFlag = 0b_0000_0100;
    public const byte SerialFlag = 0b_0000_1000;
    public const byte JoypadFlag = 0b_0001_0000;
    #endregion
}
