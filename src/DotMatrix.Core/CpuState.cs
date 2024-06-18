using System.Runtime.InteropServices;

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
}
