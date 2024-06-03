namespace DotMatrix.Core;

internal partial class Cpu
{
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

    private byte Indirect(in ushort register) => _bus[register];

    private byte Immediate() => _bus[_cpuState.PC++];

    private int Halt()
    {
        throw new NotImplementedException();
    }
}
