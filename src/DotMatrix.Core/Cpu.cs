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
        // LogState();
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
        bool wasHalted = _state.IsHalted;

        if (!_state.IsHalted)
        {
            _opcodeHandler.HandleOpcode(ref _state, _bus);

            if (_state.NextInstructionCb)
            {
                // Run the next instruction immediately if it's a CB-prefix instruction
                _state.Ir = Fetch();
                _opcodeHandler.HandleOpcode(ref _state, _bus);
            }

            LogState();
        }

        HandleInterrupts();

        // if (!wasHalted)
        // {
        _state.Ir = Fetch();
        // }
        // else
        // {
        //     _state.IncrementMCycles();
        // }

        _bus.Timer.TickTCycles((int)(_state.TCycles - previousTCycles));
    }

    /**
     * Returns true if we jumped due to an interrupt this cycle
     */
    private void HandleInterrupts()
    {
        if (_state is { InterruptMasterEnable: false, IsHalted: false })
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
            // If the current interrupt is both enabled and requested, then we have an interrupt pending
            if (Bit(interruptEnable, i) && Bit(interruptFlag, i))
            {
                // If CPU is halted, halt instruction should immediately exit
                if (_state.IsHalted)
                {
                    _state.IsHalted = false;
                    return;
                }

                HandlePendingInterrupt(i);
                return;
            }
        }
    }

    private void HandlePendingInterrupt(int interruptIndex)
    {
        // Disable interrupts and unset the interrupt flag that we're about to handle
        _state.InterruptMasterEnable = false;
        DisableInterrupt(interruptIndex);

        // Push the current PC onto the stack, then jump to the interrupt handler
        _state.IncrementMCycles(2);
        // _state.Pc -= 1;
        OpcodeHandler.PushInternal(ref _state, _bus, _state.Pc);
        _state.IncrementMCycles(2);
        _state.Pc = Memory.Interrupts[interruptIndex];
        _state.IncrementMCycles();
    }

    private void DisableInterrupt(int interruptIndex)
    {
        _bus[Memory.InterruptFlag] &= (byte)~(1 << interruptIndex);
    }

    private byte Fetch()
    {
        _state.IncrementMCycles();

        byte opcode = _bus[_state.Pc];

        // HALT instruction does not increment on fetch
        if (!_state.IsHalted)
        {
            _state.Pc += 1;
        }

        return opcode;
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

    private static bool Bit(int data, int bit) => (data & (1 << bit)) > 0;
}
