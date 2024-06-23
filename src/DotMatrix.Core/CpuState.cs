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

    public ushort Sp;
    public ushort Pc;
    public byte Ir;
    public bool IrIsCb;

    public long TCycles;

    public ushort AF
    {
        get => Get16BitReg(hi: A, lo: F);
        set
        {
            A = (byte)((value & 0b_1111_0000) >> 4);
            F = (byte)(value & 0b_0000_1111);
        }
    }

    public ushort BC
    {
        get => Get16BitReg(hi: B, lo: C);
        set
        {
            B = (byte)((value & 0b_1111_0000) >> 4);
            C = (byte)(value & 0b_0000_1111);
        }
    }

    public ushort DE
    {
        get => Get16BitReg(hi: D, lo: E);
        set
        {
            D = (byte)((value & 0b_1111_0000) >> 4);
            E = (byte)(value & 0b_0000_1111);
        }
    }

    public ushort HL
    {
        get => Get16BitReg(hi: H, lo: L);
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

    private static ushort Get16BitReg(byte hi, byte lo) => (ushort)((hi << 8) + lo);
}
