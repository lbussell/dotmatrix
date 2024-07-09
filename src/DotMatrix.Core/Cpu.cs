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
        LogState();
        for (int i = 0; i < instructions; i += 1)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            Step();
        }
    }

    /**
     * One full decode-execute-fetch cycle. Not always 4 T-Cycles.
     */
    private void Step()
    {
        ulong previousTCycles = _state.TCycles;

        _state.Pc += 1;
        _opcodeHandler.HandleOpcode(ref _state, _bus);
        _state.Ir = Fetch();

        if (_state.NextInstructionCb)
        {
            // Run the next instruction immediately if it's a CB-prefix instruction
            _state.Pc += 1;
            _opcodeHandler.HandleOpcode(ref _state, _bus);
        }

        LogState();
        HandleInterrupts();
        _state.Ir = Fetch();

        _bus.Timer.TickTCycles((int)(_state.TCycles - previousTCycles));
    }

    /**
     * Returns true if we jumped due to an interrupt this cycle
     */
    private void HandleInterrupts()
    {
        if (!_state.InterruptMasterEnable)
        {
            return;
        }

        byte interruptFlag = _bus[Memory.InterruptFlag];
        byte interruptEnable = _bus[Memory.InterruptEnable];

        if ((interruptFlag & interruptEnable) == 0)
        {
            return;
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
                return;
            }
        }
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
