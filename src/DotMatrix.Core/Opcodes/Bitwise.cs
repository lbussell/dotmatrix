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
        cpuState.ZeroFlag = (register & (1 << bit)) == 0;
        cpuState.NSubFlag = false;
        cpuState.HalfCarryFlag = true;
        return 2 * 4;
    }

    public static int RotateLeft(ref byte register, ref CpuState cpuState)
    {
        bool msb = (register & 0x80) != 0;
        byte carry = (byte)(cpuState.CarryFlag ? 1 : 0);
        byte result = (byte)(register << 1 | carry);
        register = result;
        SetRotateFlags(ref cpuState, result, msb);
        return 2 * 4;
    }

    private static void SetRotateFlags(ref CpuState cpuState, byte result, bool msb)
    {
        cpuState.ZeroFlag = result == 0;
        cpuState.NSubFlag = false;
        cpuState.HalfCarryFlag = false;
        cpuState.CarryFlag = msb;
    }

    public static int RotateLeftA(ref CpuState cpuState)
    {
        bool msb = (cpuState.A & 0x80) != 0;
        byte carry = (byte)(cpuState.CarryFlag ? 1 : 0);
        byte result = (byte)(cpuState.A << 1 | carry);
        cpuState.A = result;

        cpuState.ClearFlags();
        cpuState.CarryFlag = msb;
        return 1 * 4;
    }
}
