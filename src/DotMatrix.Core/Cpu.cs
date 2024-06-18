namespace DotMatrix.Core;

internal class Cpu(IBus bus, OpcodeHandler opcodeHandler, CpuState initialState = new())
{
    private readonly IBus _bus = bus;
    private readonly OpcodeHandler _opcodeHandler = opcodeHandler;
    private CpuState _state = initialState;

    internal CpuState State => _state;

    public void Run(int cycles)
    {
        RunInternal(cycles);
    }

    /*
     * One full cycle of "Decode, Execute, Fetch"
     */
    internal void Step()
    {
        // One decode-execute-fetch loop takes 4 T-Cycles (1 M-Cycle) for most instructions
        // If an instruction takes more time than that, it will happen during the HandleOpcode call

        _opcodeHandler.HandleOpcode(ref _state, _bus);

        // The fetch happens simultaneously with the last M-Cycle of an instruction
        // So don't add to the M-Cycles here since it was already accounted for in the instruction
        _state.Ir = Fetch();
    }

    private void RunInternal(int tCycles)
    {
        while (_state.TCycles < tCycles)
        {
        }
    }

    private byte Fetch() => _bus[_state.Pc];
}
