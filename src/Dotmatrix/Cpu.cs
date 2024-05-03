namespace DotMatrix;

using DotMatrix.Generated;

public partial class Cpu(IBus bus) : ICpu
{
    private readonly IBus _bus = bus;

    private CpuState _state = new();
    private ExternalState _externalState = new();

    public void ExecuteFrames(int frames = 1, int cycles = ConsoleSpecs.CyclesPerFrame)
    {
        while (_externalState.CyclesSinceLastFrame < cycles)
        {
            (_state, _externalState) = ExecuteCycle(_state, _externalState);
        }

        _externalState = _externalState with
        {
            CyclesSinceLastFrame = _externalState.CyclesSinceLastFrame % ConsoleSpecs.CyclesPerFrame,
        };
    }

    private static ValueTuple<CpuState, ExternalState> ExecuteCycle(CpuState cpuState, ExternalState externalState)
    {
        return Execute(cpuState, externalState);
    }

    [GenerateCpuInstructions]
    private static partial ValueTuple<CpuState, ExternalState> Execute(CpuState cpuState, ExternalState externalState);
}
