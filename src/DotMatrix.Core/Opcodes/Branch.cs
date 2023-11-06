namespace DotMatrix.Core.Opcodes;

internal static class Branch
{
    public static int RelativeJump(ref CpuState cpuState, Bus bus, Func<bool> condition)
    {
        sbyte e = (sbyte)bus.ReadInc8(ref cpuState.PC);

        if (condition())
        {
            cpuState.PC = (ushort)(cpuState.PC + e);
            Console.WriteLine($"Jumped to ${cpuState.PC:X4}");
            return 3 * 4;
        }

        return 2 * 4;
    }
}
