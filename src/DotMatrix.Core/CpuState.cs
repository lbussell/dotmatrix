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

    public ushort AF => Get16BitReg(A, F);
    public ushort BC => Get16BitReg(B, C);
    public ushort DE => Get16BitReg(D, E);
    public ushort HL => Get16BitReg(H, L);

    public override string ToString()
    {
        return $"{{ IR:${Ir:X2},PC:${Pc:X4},TC:{TCycles} R:${A:X2},{F:X2},{B:X2},{C:X2},{D:X2},{E:X2},{H:X2},{L:X2}, SP:${Sp:X4} }}";
    }

    private static ushort Get16BitReg(byte hi, byte lo) => (ushort)((hi << 8) + lo);
}
