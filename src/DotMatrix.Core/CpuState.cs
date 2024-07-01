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

    public long TCycles;

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

    public void IncrementMCycles(int numberOfMCycles = 1)
    {
        TCycles += MCycleLength * numberOfMCycles;
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

    public override string ToString()
    {
        return $"{{ IR:${Ir:X2},PC:${Pc:X4},TC:{TCycles} R:${A:X2},{F:X2},{B:X2},{C:X2},{D:X2},{E:X2},{H:X2},{L:X2}, SP:${Sp:X4} }}";
    }

    /// <summary>
    /// Combines two 8-bit values into one 16-bit value
    /// </summary>
    private static ushort GetCombinedValue(byte hi, byte lo) => (ushort)((hi << 8) + lo);
}
