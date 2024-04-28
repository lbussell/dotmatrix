namespace Dotmatrix;

using Generated;

public partial class Cpu(IBus bus) : ICpu
{
    private readonly IBus _bus = bus;

    private CpuState _state = new();

    [GenerateCpuInstructions]
    private partial CpuState Execute(CpuState previousState);
}
