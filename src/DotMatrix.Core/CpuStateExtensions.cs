namespace DotMatrix.Core;

internal static class CpuStateExtensions
{
    // Flag layout: 0b_ZNHC_0000
    public static void SetZ(this CpuState cpuState) => cpuState.F = (byte)(cpuState.F | 0b_1000_0000);
    public static void SetN(this CpuState cpuState) => cpuState.F = (byte)(cpuState.F | 0b_0100_0000);
    public static void SetH(this CpuState cpuState) => cpuState.F = (byte)(cpuState.F | 0b_0010_0000);
    public static void SetC(this CpuState cpuState) => cpuState.F = (byte)(cpuState.F | 0b_0001_0000);

    // Set if zero
    public static void SetZ(this CpuState cpuState, int r)
    {
        if (r == 0)
        {
            cpuState.SetZ();
        }
    }

    // Half-carry: set if overflow from bit 3
    public static void SetH(this CpuState cpuState, ushort result)
    {
        // check for bit #3 overflow
        if (result > 0xF)
        {
            cpuState.SetH();
        }
    }

    // Carry: set if 8 bit overflow (result of 8-bit arithmetic is 16-bit ushort)
    public static void SetC(this CpuState cpuState, ushort result)
    {
        // check for bit #7 overflow
        if (result > 0xFF)
        {
            cpuState.SetC();
        }
    }

    // Carry: set if 16 bit overflow (result of 16-bit arithmetic is 32-bit int)
    public static void SetC(this CpuState cpuState, int result)
    {
        if (result > 0xFFFF)
        {
            cpuState.SetC();
        }
    }

}