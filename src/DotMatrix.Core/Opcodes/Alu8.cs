namespace DotMatrix.Core;

internal static class Alu8
{
    public static int Xor(ref CpuState cpuState, ref byte register)
    {
        XorBase(ref cpuState, register);
        return 4;
    }

    public static int XorHLIndirect(Bus bus, ref CpuState cpuState)
    {
        XorBase(ref cpuState, bus[cpuState.HL]);
        return 8;
    }

    public static int XorImmediate(Bus bus, ref CpuState cpuState)
    {
        XorBase(ref cpuState, bus.ReadInc8(ref cpuState.PC));
        return 8;
    }

    private static void XorBase(ref CpuState cpuState, byte operand)
    {
        cpuState.A ^= operand;
        cpuState.ClearFlags();
        cpuState.SetZ(cpuState.A == 0);
    }
}
