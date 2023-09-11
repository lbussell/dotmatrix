namespace DotMatrix.Core;

internal static class CpuUtil
{
    // TODO: this smells...
    public static CpuState SetRegister(CpuState cpuState, CpuRegister r, ushort value) => r switch
        {
            CpuRegister.A => cpuState with { AF = SetHi(cpuState.AF, (byte)value) },
            CpuRegister.F => cpuState with { AF = SetLo(cpuState.AF, (byte)value) },

            CpuRegister.B => cpuState with { BC = SetHi(cpuState.BC, (byte)value) },
            CpuRegister.C => cpuState with { BC = SetLo(cpuState.BC, (byte)value) },

            CpuRegister.D => cpuState with { DE = SetHi(cpuState.DE, (byte)value) },
            CpuRegister.E => cpuState with { DE = SetLo(cpuState.DE, (byte)value) },

            CpuRegister.H => cpuState with { HL = SetHi(cpuState.HL, (byte)value) },
            CpuRegister.L => cpuState with { HL = SetLo(cpuState.HL, (byte)value) },

            _ => throw new NotSupportedException(),
        };

    public static byte GetRegister(CpuState cpuState, CpuRegister r) => r switch
        {
            CpuRegister.A => GetHi(cpuState.AF),
            CpuRegister.F => GetLo(cpuState.AF),

            CpuRegister.B => GetHi(cpuState.BC),
            CpuRegister.C => GetLo(cpuState.BC),

            CpuRegister.D => GetHi(cpuState.DE),
            CpuRegister.E => GetLo(cpuState.DE),

            CpuRegister.H => GetHi(cpuState.HL),
            CpuRegister.L => GetLo(cpuState.HL),

            _ => throw new NotSupportedException(),
        };

    public static byte GetLo(ushort w) => (byte)w;

    public static byte GetHi(ushort w) => (byte)(w | 0xFF00 >> 8);

    public static ushort SetLo(ushort w, byte value)
    {
        int newHi = w & 0xFF00;
        int newLo = value;
        return (ushort)(newHi | newLo);
    }

    public static ushort SetHi(ushort w, byte value)
    {
        int newHi = value << 8;
        int newLo = w & 0x00FF;
        return (ushort)(newHi | newLo);
    }

    public static void Print(ushort n) => Console.WriteLine(n.ToString("X4"));

    public static void Print(byte b) => Console.WriteLine(b.ToString("X2"));

    public static void Print(CpuState cpuState) =>
        Console.WriteLine($"CPU: {{ AF:{cpuState.AF:X2} BC:{cpuState.BC:X2} DE:{cpuState.DE:X2} HL:{cpuState.HL:X2} "
                          + $"SP:{cpuState.SP:X4} PC:{cpuState.PC:X4} }}");
}
