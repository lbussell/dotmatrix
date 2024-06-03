namespace DotMatrix.Core;

internal record struct ExtCpuState(
    long Cycles,
    int CyclesSinceLastFrame);

internal partial class Cpu(Bus bus)
{
    private CpuState _cpuState;
    private ExtCpuState _extCpuState;
    private Bus _bus = bus;

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

    private byte ReadInc8()
    {
        return _bus[_cpuState.PC++];
    }

    private ushort ReadInc16()
    {
        throw new NotImplementedException();
    }
}
