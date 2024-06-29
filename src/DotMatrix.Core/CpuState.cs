namespace DotMatrix.Core;

public record struct CpuState
{
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
    public bool IrIsCb;

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
            F = (byte)(value & 0x00FF);
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

    public override string ToString()
    {
        return $"{{ IR:${Ir:X2},PC:${Pc:X4},TC:{TCycles} R:${A:X2},{F:X2},{B:X2},{C:X2},{D:X2},{E:X2},{H:X2},{L:X2}, SP:${Sp:X4} }}";
    }

    /// <summary>
    /// Combines two 8-bit values into one 16-bit value
    /// </summary>
    private static ushort GetCombinedValue(byte hi, byte lo) => (ushort)((hi << 8) + lo);
}
