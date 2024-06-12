namespace DotMatrix.Core;

internal class Cpu(Bus bus, OpcodeHandler<int> opcodeHandler)
{
    private readonly Bus _bus = bus;
    private readonly OpcodeHandler<int> _opcodeHandler = opcodeHandler;

    private CpuState _cpuState;
    private ExtCpuState _extCpuState;

    public void Run(int cycles)
    {
        RunInternal(cycles);
    }

    private void RunInternal(int cycles)
    {
        while (_extCpuState.Cycles < cycles)
        {
            _extCpuState.Cycles += Step();
        }
    }

    private int Step()
    {
        byte opcode = _bus[_cpuState.PC++];
        Console.WriteLine($"Read ${opcode:X2}");

        return _opcodeHandler.HandleOpcode(opcode, ref _cpuState);
    }

    private static byte GetBlock(byte opcode) =>
        (byte)((opcode & 0b_1100_0000) >> 6);
}
