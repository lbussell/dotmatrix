namespace DotMatrix.Core.Opcodes;

internal static class Load16
{
    public static int LoadImmediate(Bus bus, ref ushort targetRegister, ref ushort pc)
    {
        targetRegister = bus.ReadInc16(ref pc);
        return 3 * 4;
    }

    public static int LoadFromSP(Bus bus, ushort sp, ref ushort pc)
    {
        var nn = bus.ReadInc16(ref pc);
        bus.Write16(nn, sp);
        return 5 * 4;
    }

    public static int LoadSPFromHL(ref CpuState cpuState)
    {
        cpuState.SP = cpuState.HL;
        return 4;
    }
}
