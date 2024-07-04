using DotMatrix.Core.Instructions;

namespace DotMatrix.Core;

internal class Cpu
{
    private readonly IBus _bus;
    private readonly IOpcodeHandler _opcodeHandler;
    private readonly bool _loggingEnabled;
    private CpuState _state;

    public Cpu(IBus bus, IOpcodeHandler opcodeHandler, bool enableLogging, CpuState initialState = new())
    {
        _bus = bus;
        _opcodeHandler = opcodeHandler;
        _loggingEnabled = enableLogging;
        _state = initialState;
    }

    // Needs to be internal so that state can be checked in tests
    internal CpuState State => _state;

    public void Run(int instructions)
    {
        for (int i = 0; i < instructions; i += 1)
        {
            if (_loggingEnabled)
            {
                Console.WriteLine($"{_state} {PcMem()}");
            }

            Step();

            // Run the next instruction immediately if it's a CB-prefix instruction
            if (_state.NextInstructionCb)
            {
                Step();
            }
        }
    }

    private string PcMem() =>
        $"PCMEM:{_bus[_state.Pc]:X2},{_bus[(ushort)(_state.Pc + 1)]:X2},{_bus[(ushort)(_state.Pc + 2)]:X2},{_bus[(ushort)(_state.Pc + 3)]:X2}";

    /**
     * One full decode-execute-fetch cycle. Not always 4 T-Cycles.
     */
    internal void Step()
    {
        _state.Pc += 1;

        // Opcode handler is responsible for incrementing the T-cycles.
        // A typical decode-execute-fetch loop takes 4 T-Cycles (1 M-Cycle) for most instructions
        // If an instruction takes more time than that, it will also happen during the HandleOpcode call
        _opcodeHandler.HandleOpcode(ref _state, _bus);

        // The fetch happens simultaneously with the last M-Cycle of an instruction
        // So don't add to cycles here since it was already accounted for in the instruction
        _state.Ir = Fetch();
    }

    private byte Fetch()
    {
        _state.IncrementMCycles();
        return _bus[_state.Pc];
    }
}
