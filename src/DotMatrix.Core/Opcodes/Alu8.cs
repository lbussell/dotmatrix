namespace DotMatrix.Core;

internal static class Alu8
{
    public static int Inc(ref byte register, ref CpuState cpuState)
    {
        AddSetHalfCarry(ref register, 1, ref cpuState);
        return 4;
    }

    public static int Dec(ref byte register, ref CpuState cpuState)
    {
        cpuState.SetHalfCarryFlag((register & 0xF) == 0);
        register -= 1;
        cpuState.NSubFlag = true;
        cpuState.ZeroFlag = register == 0;
        return 1 * 4;
    }

    public static int DecIndirectHL(Bus bus, ref CpuState cpuState)
    {
        ushort addr = cpuState.HL;
        cpuState.SetHalfCarryFlag((bus[addr] & 0xF) == 0);
        byte result = (byte)(bus[addr] - 1);
        cpuState.NSubFlag = true;
        cpuState.ZeroFlag = result == 0;
        return 3 * 4;
    }

    public static int Xor(ref CpuState cpuState, ref byte register)
    {
        XorBase(ref cpuState, register);
        return 1 * 4;
    }

    public static int XorHLIndirect(Bus bus, ref CpuState cpuState)
    {
        XorBase(ref cpuState, bus[cpuState.HL]);
        return 2 * 4;
    }

    public static int XorImmediate(Bus bus, ref CpuState cpuState)
    {
        XorBase(ref cpuState, bus.ReadInc8(ref cpuState.PC));
        return 2 * 4;
    }

    private static void XorBase(ref CpuState cpuState, byte operand)
    {
        cpuState.A ^= operand;
        cpuState.ClearFlags();
        cpuState.ZeroFlag = cpuState.A == 0;
    }

    private static void AddSetHalfCarry(ref byte register, byte toAdd, ref CpuState cpuState)
    {
        ushort value = (ushort)(register & 0x0F + toAdd & 0x0F);
        cpuState.SetHalfCarryFlag(register > 0x0F);
        register = (byte)(value & 0x0F);
    }
}
