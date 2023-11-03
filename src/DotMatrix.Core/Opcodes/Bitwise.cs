namespace DotMatrix.Core.Opcodes;

internal static class Bitwise
{
    // Test bit bit in register register.
    // flags:
    // zero: dependent
    // negative: unset
    // half-carry: set
    // carry: unmodified
    public static int Bit(byte bit, ref byte register, ref CpuState cpuState)
    {
        cpuState.SetZIfZero(register & (1 << bit));
        cpuState.ClearN();
        cpuState.SetHalfCarryFlag();
        return 2 * 4;
    }
}
