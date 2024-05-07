namespace DotMatrix;

using DotMatrix.Generated;

public interface ICpu
{
    void ExecuteFrames(int frames = 1);
}

public partial class Cpu(IBus bus) : ICpu
{
    private readonly IBus _bus = bus;

    private CpuState _state = new();
    private ExternalState _externalState = new();

    public void ExecuteFrames(int frames = 1)
    {
        while (_externalState.Frames <= frames /* temporary */ && _state.PC <= MemoryMap.BootRom.End)
        {
            (_state, _externalState) = ExecuteCycle(_state, _externalState);
        }
    }

    private ValueTuple<CpuState, ExternalState> ExecuteCycle(CpuState cpuState, ExternalState externalState)
    {
        (cpuState, externalState) = ReadAndExecuteNextInstruction(cpuState, externalState);

        // externalState.CyclesSinceLastFrame %= ConsoleSpecs.CyclesPerFrame;

        return (cpuState, externalState);
    }

    [GenerateCpuInstructions]
    private partial ValueTuple<CpuState, ExternalState> ReadAndExecuteNextInstruction(CpuState cpuState, ExternalState externalState);
}
