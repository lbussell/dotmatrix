using DotMatrix.Core.Instructions;

namespace DotMatrix.Core;

internal class Cpu
{
    private readonly IBus _bus;
    private readonly IOpcodeHandler _opcodeHandler;
    private CpuState _state;

    public Cpu(IBus bus, IOpcodeHandler opcodeHandler, CpuState initialState = new())
    {
        _bus = bus;
        _opcodeHandler = opcodeHandler;
        _state = initialState;
    }

    public bool LoggingEnabled { get; init; } = false;

    // Needs to be internal so that state can be checked in tests
    internal CpuState State => _state;

    public void Run(CancellationToken cancellationToken, int instructions = int.MaxValue)
    {
        for (int i = 0; i < instructions; i += 1)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            LogState();
            Step();
        }
    }

    /**
     * One full decode-execute-fetch cycle. Not always 4 T-Cycles.
     */
    private void Step()
    {
        ulong previousTCycles = _state.TCycles;

        StepInternal();
        if (_state.NextInstructionCb)
        {
            // Run the next instruction immediately if it's a CB-prefix instruction
            StepInternal();
        }

        _bus.Timer.TickTCycles((int)(_state.TCycles - previousTCycles));
    }

    private void StepInternal()
    {
        bool _ = HandleInterrupts();
        _state.Pc += 1;
        // if (!handledInterrupt)
        // {
            // Opcode handler is responsible for incrementing the T-cycles.
            // A typical decode-execute-fetch loop takes 4 T-Cycles (1 M-Cycle) for most instructions
            // If an instruction takes more time than that, it will also happen during the HandleOpcode call
            _opcodeHandler.HandleOpcode(ref _state, _bus);
        // }

        // The fetch happens simultaneously with the last M-Cycle of an instruction
        // So don't add to cycles here since it was already accounted for in the instruction
        _state.Ir = Fetch();
    }

    /**
     * Returns true if we jumped due to an interrupt this cycle
     */
    private bool HandleInterrupts()
    {
        if (!_state.InterruptMasterEnable) return false;

        byte interruptFlag = _bus[Memory.InterruptFlag];
        byte interruptEnable = _bus[Memory.InterruptEnable];

        if ((interruptFlag & interruptEnable) == 0)
        {
            return false;
        }

        // Handle interrupts in priority order
        for (int i = 0; i <= 4; i += 1)
        {
            // If the current interrupt is both enabled and requested
            if (((interruptEnable & (1 << i)) > 0)
                && ((interruptFlag & (1 << i)) > 0))
            {
                // Disable interrupts and unset the interrupt flag that we're about to handle
                _state.InterruptMasterEnable = false;
                _bus[Memory.InterruptFlag] &= (byte)~(1 << i);

                // Push the current PC onto the stack, then jump to the interrupt handler
                _state.IncrementMCycles(2);
                OpcodeHandler.PushInternal(ref _state, _bus, _state.Pc);
                _state.IncrementMCycles(2);
                _state.Pc = Memory.Interrupts[i];
                _state.IncrementMCycles();

                return true;
            }
        }

        return false;
    }

    private byte Fetch()
    {
        _state.IncrementMCycles();
        return _bus[_state.Pc];
    }

    private void LogState()
    {
        if (LoggingEnabled)
        {
            Console.WriteLine($"{_state} {PcMem()}");
        }
    }

    private string PcMem() =>
        $"PCMEM:{_bus[_state.Pc]:X2},{_bus[(ushort)(_state.Pc + 1)]:X2}," +
        $"{_bus[(ushort)(_state.Pc + 2)]:X2},{_bus[(ushort)(_state.Pc + 3)]:X2}";
}
