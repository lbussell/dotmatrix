namespace DotMatrix.Core.Opcodes;

internal static class Load8
{
    public static int Load(ref byte targetRegister, ref byte sourceRegister)
    {
        targetRegister = sourceRegister;
        return 4;
    }

    public static int LoadImmediate(ref byte targetRegister, Bus bus, ref ushort pc)
    {
        targetRegister = bus.ReadInc8(ref pc);
        return 4;
    }
}
