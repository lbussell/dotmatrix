namespace DotMatrix.Core;

internal static class CpuStateExtensions
{
    /*
     * Flag layout: 0b_ZNHC_0000
     */

    public static void ClearFlags(ref this CpuState state) => state.F = 0;

    public static void SetN(ref this CpuState cpuState) =>
        cpuState.F = (byte)(cpuState.F | 0b_0100_0000);

    public static void ClearN(ref this CpuState cpuState) =>
        cpuState.F = (byte)(cpuState.F & 0b_1011_1111);

    /**
     * Set zero flag if value is zero, otherwise unset flag
     */
    public static void SetZeroFlag(ref this CpuState state, int value)
    {
        state.F = value == 0
            ? (byte)(state.F | 0b_1000_0000)
            : (byte)(state.F & 0b_0111_1111);
    }

    /**
     * Set half-carry flag
     */
    public static void SetHalfCarryFlag(ref this CpuState state, bool value)
    {
        // Check for bit #3 overflow
        state.F = value
            ? (byte)(state.F | 0b_0010_0000)
            : (byte)(state.F & 0b_1101_1111);
    }

    /**
     * Set carry flag
     */
    public static void SetCarryFlag(ref this CpuState state, bool value)
    {
        // Check for bit #3 overflow
        state.F = value
            ? (byte)(state.F | 0b_0001_0000)
            : (byte)(state.F & 0b_1110_1111);
    }
}