namespace DotMatrix.Core.Opcodes;

/*
 * Reference: https://gekkio.fi/files/gb-docs/gbctr.pdf
 */
internal static class Load8
{
    public static int Register(ref byte targetRegister, ref byte sourceRegister)
    {
        targetRegister = sourceRegister;
        return 4;
    }

    public static int RegisterImmediate(ref byte targetRegister, ref ushort pc, Bus bus)
    {
        targetRegister = bus.ReadInc8(ref pc);
        return 2 * 4;
    }

    // Load to the 8-bit register targetRegister, data from the absolute address specified by the 16-bit register
    // addressRegister.
    public static int RegisterIndirect(ref byte targetRegister, ref ushort addressRegister, Bus bus)
    {
        targetRegister = bus.ReadInc8(ref addressRegister);
        return 2 * 4;
    }

    public static int FromRegisterIndirectHL(ref byte sourceRegister, ref CpuState cpuState, Bus bus)
    {
        bus[cpuState.HL] = sourceRegister;
        return 2 * 4;
    }

    // LD (FF00+C),A
    // write A->(FF00+C)
    // Or, write(unsigned_16(lsb=C, msb=0xFF), A)
    public static int FromAIndirect(ref CpuState cpuState, ref ushort pc, Bus bus)
    {
        bus[(ushort)(0xFF00 + cpuState.C)] = cpuState.A;
        return 2 * 4;
    }

    public static int FromADirect(ref CpuState cpuState, ref ushort pc, Bus bus)
    {
        byte addr = bus.ReadInc8(ref pc);
        bus[(ushort)(0xFF00 + addr)] = cpuState.A;
        return 3 * 4;
    }

    public static int IndirectDec(ref byte targetRegister, ref ushort address, Bus bus)
    {
        targetRegister = bus[address--];
        return 2 * 4;
    }

    public static int FromIndirectDec(ref ushort targetAddress, ref byte register, Bus bus)
    {
        bus[targetAddress--] = register;
        return 2 * 4;
    }

    public static int IndirectInc(ref byte targetRegister, ref ushort address, Bus bus)
    {
        targetRegister = bus[address++];
        return 2 * 4;
    }

    public static int FromIndirectInc(ref ushort targetAddress, ref byte register, Bus bus)
    {
        bus[targetAddress++] = register;
        return 2 * 4;
    }
}
