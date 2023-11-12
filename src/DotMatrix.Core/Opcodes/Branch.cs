namespace DotMatrix.Core.Opcodes;

internal static class Branch
{
    public static int JumpImmediate(ref CpuState cpuState, Bus bus)
    {
        ushort address = bus.ReadInc16(ref cpuState.PC);
        cpuState.PC = address;
        Console.WriteLine($"Jumped to ${cpuState.PC:X4}");
        return 4 * 4;
    }

    public static int JumpToHL(ref CpuState cpuState)
    {
        cpuState.PC = cpuState.HL;
        Console.WriteLine($"Jumped to ${cpuState.PC:X4}");
        return 1 * 4;
    }

    public static int Jump(ref CpuState cpuState, Bus bus, Func<bool> condition)
    {
        ushort address = bus.ReadInc16(ref cpuState.PC);

        if (condition())
        {
            cpuState.PC = address;
            Console.WriteLine($"Jumped to ${cpuState.PC:X4}");
            return 4 * 4;
        }

        return 3 * 4;
    }

    public static int JumpRelative(ref CpuState cpuState, Bus bus)
    {
        sbyte e = (sbyte)bus.ReadInc8(ref cpuState.PC);
        cpuState.PC = (ushort)(cpuState.PC + e);
        Console.WriteLine($"Jumped to ${cpuState.PC:X4}");
        return 3 * 4;
    }

    public static int JumpRelative(ref CpuState cpuState, Bus bus, Func<bool> condition)
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

    public static int Call(ref CpuState cpuState, Bus bus)
    {
        ushort address = bus.ReadInc16(ref cpuState.PC);
        cpuState.SP -= 2;
        bus.Write16(cpuState.SP, cpuState.PC);
        cpuState.PC = address;
        Console.WriteLine($"Called ${cpuState.PC:X4}");
        return 6 * 4;
    }

    public static int Call(ref CpuState cpuState, Bus bus, Func<bool> condition)
    {
        ushort address = bus.ReadInc16(ref cpuState.PC);

        if (condition())
        {
            cpuState.SP -= 2;
            bus.Write16(cpuState.SP, cpuState.PC);
            cpuState.PC = address;
            Console.WriteLine($"Called ${cpuState.PC:X4}");
            return 6 * 4;
        }

        return 3 * 4;
    }

    public static int Return(ref CpuState cpuState, Bus bus)
    {
        cpuState.PC = bus.ReadInc16(ref cpuState.SP);
        Console.WriteLine($"Returned to ${cpuState.PC:X4}");
        return 4 * 4;
    }

    public static int Return(ref CpuState cpuState, Bus bus, Func<bool> condition)
    {
        if (condition())
        {
            cpuState.PC = bus.ReadInc16(ref cpuState.SP);
            Console.WriteLine($"Returned to ${cpuState.PC:X4}");
            return 5 * 4;
        }

        return 2 * 4;
    }

    public static int ReturnFromInterrupt(ref CpuState cpuState, Bus bus)
    {
        cpuState.IME = true;
        return Return(ref cpuState, bus);
    }

    public static int RSTn(ref CpuState cpuState, byte n, Bus bus)
    {
        cpuState.SP -= 2;
        bus.Write16(cpuState.SP, cpuState.PC);
        cpuState.PC = n;
        Console.WriteLine($"Called ${cpuState.PC:X4}");
        return 4 * 4;
    }
}
