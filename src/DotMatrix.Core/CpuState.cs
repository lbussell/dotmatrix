using DotMatrix.Core.Instructions;

namespace DotMatrix.Core;

public record struct CpuState
{
    private const int MCycleLength = 4;

    public byte A;
    public byte F;
    public byte B;
    public byte C;
    public byte D;
    public byte E;
    public byte H;
    public byte L;

    /// <summary>
    /// Stack Pointer.
    /// </summary>
    public ushort Sp;

    /// <summary>
    /// Program Counter.
    /// </summary>
    public ushort Pc;

    /// <summary>
    /// Instruction Register. Contains the next instruction to be executed.
    /// </summary>
    public byte Ir;
    public bool NextInstructionCb;

    /// <summary>
    /// Interrupt enable
    /// </summary>
    public bool InterruptMasterEnable;

    /// <summary>
    /// Whether to set Ie after the next instruction
    /// </summary>
    public bool SetImeNext;

    public ulong TCycles;

    /// <summary>
    /// 16-bit combination of registers A and F
    /// </summary>
    public ushort AF
    {
        get => GetCombinedValue(hi: A, lo: F);
        set
        {
            A = (byte)((value & 0xFF00) >> 8);
            // Not a typo, bottom 4 bits of F are always 0
            F = (byte)(value & 0x00F0);
        }
    }

    /// <summary>
    /// 16-bit combination of registers B and C
    /// </summary>
    public ushort BC
    {
        get => GetCombinedValue(hi: B, lo: C);
        set
        {
            B = (byte)((value & 0xFF00) >> 8);
            C = (byte)(value & 0x00FF);
        }
    }

    /// <summary>
    /// 16-bit combination of registers D and E
    /// </summary>
    public ushort DE
    {
        get => GetCombinedValue(hi: D, lo: E);
        set
        {
            D = (byte)((value & 0xFF00) >> 8);
            E = (byte)(value & 0x00FF);
        }
    }

    /// <summary>
    /// 16-bit combination of registers H and L
    /// </summary>
    public ushort HL
    {
        get => GetCombinedValue(hi: H, lo: L);
        set
        {
            H = (byte)((value & 0xFF00) >> 8);
            L = (byte)(value & 0x00FF);
        }
    }

    public byte CarryFlag => (byte)((F & 0b_0001_0000) >> 4);

    public static CpuState GetPostBootRomState()
    {
        return new CpuState
        {
            A = 0x01,
            F = 0xB0,
            B = 0x00,
            C = 0x13,
            D = 0x00,
            E = 0xD8,
            H = 0x01,
            L = 0x4D,
            Sp = 0xFFFE,
            Pc = 0x0100,
        };
    }

    public void IncrementMCycles(int numberOfMCycles = 1)
    {
        TCycles += (ulong)(MCycleLength * numberOfMCycles);
    }

    public bool GetCondition(byte value)
    {
        return value switch
        {
            0 => !this.GetZ(),
            1 => this.GetZ(),
            2 => !this.GetC(),
            _ => this.GetC(),
        };
    }

    /*
     * Format:
     * A:00 F:11 B:22 C:33 D:44 E:55 H:66 L:77 SP:8888 PC:9999
     */
    public override string ToString() =>
        $"A:{A:X2} F:{F:X2} B:{B:X2} C:{C:X2} D:{D:X2} E:{E:X2} H:{H:X2} L:{L:X2} SP:{Sp:X4} PC:{Pc:X4}";

    /// <summary>
    /// Combines two 8-bit values into one 16-bit value
    /// </summary>
    private static ushort GetCombinedValue(byte hi, byte lo) => (ushort)((hi << 8) + lo);
}
