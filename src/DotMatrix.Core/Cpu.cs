using DotMatrix.Core.Instructions;

namespace DotMatrix.Core;

internal class Cpu(IBus bus, OpcodeHandler opcodeHandler, CpuState initialState = new())
{
    private readonly IBus _bus = bus;
    private readonly OpcodeHandler _opcodeHandler = opcodeHandler;
    private CpuState _state = initialState;

    // Needs to be internal so that state can be checked in tests
    internal CpuState State => _state;

    public void Run(int cycles)
    {
        RunInternal(cycles);
    }

    /*
     * One full decode-execute-fetch cycle. Not always 4 T-Cycles
     */
    internal void Step()
    {
        // Opcode handler is responsible for incrementing the T-cycles.
        // A typical decode-execute-fetch loop takes 4 T-Cycles (1 M-Cycle) for most instructions
        // If an instruction takes more time than that, it will also happen during the HandleOpcode call
        _opcodeHandler.HandleOpcode(ref _state, _bus);

        // The fetch happens simultaneously with the last M-Cycle of an instruction
        // So don't add to cycles here since it was already accounted for in the instruction
        _state.Ir = Fetch();
    }

    private void RunInternal(int tCycles)
    {
        while (_state.TCycles < tCycles)
        {
        }
    }

    private byte Fetch() => _bus[_state.Pc++];
}
