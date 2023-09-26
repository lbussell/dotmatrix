namespace DotMatrix.Core;

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
public struct CpuState
{
    /**
        Flag explanations:
        Bit 7 = z, Zero flag.
        Bit 6 = n, Subtraction flag (BCD).
        Bit 5 = h, Half Carry flag (BCD).
        Bit 4 = c, Carry flag.
    */

    [FieldOffset(0)]
    public ushort AF;
    [FieldOffset(0)]
    public byte A;
    [FieldOffset(1)]
    public byte F;

    [FieldOffset(2)]
    public ushort BC;
    [FieldOffset(2)]
    public byte B;
    [FieldOffset(3)]
    public byte C;

    [FieldOffset(4)]
    public ushort DE;
    [FieldOffset(4)]
    public byte D;
    [FieldOffset(5)]
    public byte E;

    [FieldOffset(6)]
    public ushort HL;
    [FieldOffset(6)]
    public byte H;
    [FieldOffset(7)]
    public byte L;

    [FieldOffset(8)]
    public ushort SP;
    [FieldOffset(10)]
    public ushort PC;

    public ushort this[CpuRegister reg]
    {
        get
        {
            return reg switch
            {
                CpuRegister.A => A,
                CpuRegister.F => F,
                CpuRegister.AF => AF,
                CpuRegister.B => B,
                CpuRegister.C => C,
                CpuRegister.BC => BC,
                CpuRegister.D => D,
                CpuRegister.E => E,
                CpuRegister.DE => DE,
                CpuRegister.H => H,
                CpuRegister.L => L,
                CpuRegister.HL => HL,
                CpuRegister.SP => SP,
                CpuRegister.PC => PC,
                _ => throw new NotSupportedException(),
            };
        }

        set
        {
            switch (reg)
            {
                case CpuRegister.A:
                    A = (byte)value;
                    break;
                case CpuRegister.F:
                    F = (byte)value;
                    break;
                case CpuRegister.B:
                    B = (byte)value;
                    break;
                case CpuRegister.C:
                    C = (byte)value;
                    break;
                case CpuRegister.D:
                    D = (byte)value;
                    break;
                case CpuRegister.E:
                    E = (byte)value;
                    break;
                case CpuRegister.H:
                    H = (byte)value;
                    break;
                case CpuRegister.L:
                    L = (byte)value;
                    break;
                case CpuRegister.AF:
                    AF = value;
                    break;
                case CpuRegister.BC:
                    BC = value;
                    break;
                case CpuRegister.DE:
                    DE = value;
                    break;
                case CpuRegister.HL:
                    HL = value;
                    break;
                case CpuRegister.SP:
                    SP = value;
                    break;
                case CpuRegister.PC:
                    PC = value;
                    break;
            }
        }
    }

    public static string Name(CpuRegister reg) =>
        Enum.GetName(typeof(CpuRegister), reg) ?? string.Empty;
}
