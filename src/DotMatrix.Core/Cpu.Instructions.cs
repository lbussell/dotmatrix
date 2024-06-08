namespace DotMatrix.Core;

internal partial class Cpu
{
    private int Add(byte r8, int cycles = 4)
    {
        // add two bytes => get a ushort
        ushort result = (ushort)(_cpuState.A + r8);
        _cpuState.SetZ(result);
        // leave as a ushort so we can detect overflow and cast to byte when assigning to accumulator
        _cpuState.SetC(result);
        _cpuState.A = (byte)(result & 0xFF);
        return cycles;
    }

    private int Adc(byte r8, int cycles = 4)
    {
        throw new NotImplementedException("ADD");
        return cycles;
    }

    private int Sub(byte r8, int cycles = 4)
    {
        throw new NotImplementedException("ADD");
        return cycles;
    }

    private int Sbc(byte r8, int cycles = 4)
    {
        throw new NotImplementedException("ADD");
        return cycles;
    }

    private int And(byte r8, int cycles = 4)
    {
        throw new NotImplementedException("ADD");
        return cycles;
    }

    private int Xor(byte r8, int cycles = 4)
    {
        throw new NotImplementedException("ADD");
        return cycles;
    }

    private int Or(byte r8, int cycles = 4)
    {
        throw new NotImplementedException("ADD");
        return cycles;
    }

    private int Cp(byte r8, int cycles = 4)
    {
        throw new NotImplementedException("ADD");
        return cycles;
    }

    private static int Load8ToRegister(ref byte target, byte source, int cycles = 4)
    {
        target = source;
        return cycles;
    }

    private int Load8ToMemory(ushort target, byte source, int cycles = 8)
    {
        _bus[target] = source;
        return cycles;
    }

    private static int NoOp() => 4;

    private static int Halt()
    {
        throw new NotImplementedException();
    }

    private static int Invalid(byte op) =>
        throw new InvalidOperationException($"Opcode ${op:X2} is invalid");

    private static int Stop()
    {
        throw new NotImplementedException();
    }
}
