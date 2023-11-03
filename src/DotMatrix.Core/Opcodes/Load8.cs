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

    public static int RegisterImmediate(ref byte targetRegister, Bus bus, ref ushort pc)
    {
        targetRegister = bus.ReadInc8(ref pc);
        return 2 * 4;
    }

    // Load to the 8-bit register targetRegister, data from the absolute address specified by the 16-bit register
    // addressRegister.
    public static int RegisterIndirect(ref byte targetRegister, ref ushort addressRegister, Bus bus, ref ushort pc)
    {
        targetRegister = bus.ReadInc8(ref pc);
        return 2 * 4;
    }

    public static int FromRegisterIndirect() => throw new NotImplementedException();

    public static int FromImmediateData() => throw new NotImplementedException();

    public static int AIndirect() => throw new NotImplementedException();

    public static int FromAIndirect() => throw new NotImplementedException();

    public static int FromA() => throw new NotImplementedException();

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
