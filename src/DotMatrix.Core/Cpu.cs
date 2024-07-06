using DotMatrix.Core.Instructions;
using System.Threading;

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
            if (_state.NextInstructionCb)
            {
                // Run the next instruction immediately if it's a CB-prefix instruction
                Step();
            }

            UpdateTimers();

            if (_state.InterruptMasterEnable)
            {
                HandleInterrupts();
            }
        }
    }

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

    private void HandleInterrupts()
    {
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
            if (((interruptEnable & (1 << i)) == 1)
                && ((interruptFlag & (1 << i)) == 1))
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
            }
        }
    }

    private void UpdateTimers()
    {
        // TODO: Handle TIMA modulo interrupts
        UpdateDiv();
        UpdateTima();
    }

    private void UpdateDiv()
    {
        // Increment at 16384 Hz or every 64 M-cycles
        if (_state.TCycles % (64 * 4) == 0)
        {
            _bus[Memory.DIV] += 1;
        }
    }

    private void UpdateTima()
    {
        long tCycles = _state.TCycles;

        byte tac = _bus[Memory.TAC];
        byte clockSelect = (byte)(tac & 0b_0000_0011);
        bool timaEnabled = (tac & 0b_0000_0100) > 0;

        bool shouldUpdate = timaEnabled && clockSelect switch
        {
            0 => tCycles % (256 * 4) == 0,
            1 => tCycles % (4 * 4) == 0,
            2 => tCycles % (16 * 4) == 0,
            _ => tCycles % (64 * 4) == 0,
        };

        if (shouldUpdate)
        {
            byte tima = _bus[Memory.TIMA];
            tima += 1;
            if (tima == 0)
            {
                _bus[Memory.InterruptFlag] |= Memory.TimerFlag;
            }

            _bus[Memory.TIMA] = tima;
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
