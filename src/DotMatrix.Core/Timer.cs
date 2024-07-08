namespace DotMatrix.Core;

public record struct Timer : ITimer
{
    private const sbyte TimaOverflowDelay = 4;

    private readonly byte[] _memory;

    private ushort _div16;

    private bool _previousAndResult;

    private sbyte _timaResetTCycleCountdown = -1;

    private bool _timaResetWriteFlag;

    public Timer(byte[] memory)
    {
        _memory = memory;
    }

    /**
     * Divider: 16-bit counter which is incremented every single T-cycle.
     * Only the upper 8 bits are mapped to memory.
     * Writing to $FF04 resets all 16 bits to 0.
     */
    public byte DivHigh8
    {
        get => (byte)((_div16 & 0xFF00) >> 8);
        set => _div16 = 0;
    }

    /**
     * Timer Counter: Can be configured via TimerControl
     */
    public byte Tima
    {
        get => _memory[Memory.TIMA];
        set
        {
            /*
             * The reload of the TMA value as well as the interrupt request can be aborted by writing any value
             * to TIMA during the four T-cycles until it is supposed to be reloaded. The TIMA register contains
             * whatever value was written to it even after the 4 T-cycles have elapsed and no interrupt will be
             * requested.
             *
             * Use >1 for the reset flag, since ==1 means we're about to perform the TIMA reload -
             * meaning this is the "same" cycle as the reload.
             */
            if (_timaResetTCycleCountdown > 1)
            {
                _timaResetWriteFlag = true;
            }

            _memory[Memory.TIMA] = value;
        }
    }

    /**
     * Timer Modulo: If Timer Register overflows, it is reset to the value in this register.
     */
    public byte Tma
    {
        get => _memory[Memory.TMA];

        /*
         * If TMA is written to on the same T-cycle on which reload from TMA occurs, TMA is updated before its value is
         * loaded into TIMA, meaning the reload will be carried out with the new value.
         */
        set => _memory[Memory.TMA] = value;
    }

    /**
     * Timer Control: Controls the behavior of Timer Register. Is fully readable and writeable.
     *
     * Bit 2: Timer Enable.
     * Bits 1-0 : Clock Select. Determines the rate at which TimerRegister should be incremented:
     *
     * - 0b00: CPU Clock / 1024
     * - 0b01: CPU Clock / 16
     * - 0b10: CPU Clock / 64
     * - 0b11: CPU Clock / 256
     */
    public byte Tac
    {
        get => _memory[Memory.TAC];
        set => _memory[Memory.TAC] = value;
    }

    /**
     * Bit 2 of TAC. Responsible for enabling increments to TIMA.
     */
    public bool TimerEnable => (Tac & 0b_100) > 0;

    public byte InterruptEnable => _memory[Memory.InterruptEnable];

    public void TickTCycles(int numTCycles)
    {
        for (int i = 0; i < numTCycles; i += 1)
        {
            TickTCycle();
        }
    }

    private void TickTCycle()
    {
        if (_timaResetTCycleCountdown > 0)
        {
            _timaResetTCycleCountdown -= 1;
        }

        IncrementDiv();

        // Look for "falling edge" of the AND result
        bool andResult = TimerEnable && GetDivBit(_div16, Tac);
        if (_previousAndResult && !andResult)
        {
            IncrementTima();
        }

        _previousAndResult = andResult;

        HandleTimaOverflow();
    }

    private void IncrementDiv()
    {
        _div16 += 1;
        _memory[Memory.DIV] = DivHigh8;
    }

    private void IncrementTima()
    {
        byte tima = Tima;

        // Ignore increments to TIMA during the time between overflowing and resetting the register.
        if (tima == 0 && _timaResetTCycleCountdown >= 0)
        {
            return;
        }

        byte newValue = (byte)((tima + 1) & 0xFF);
        Tima = newValue;

        // If TIMA overflowed, start the 4 T-Cycle delay to reset it.
        if (newValue == 0)
        {
            _timaResetTCycleCountdown = TimaOverflowDelay;
        }
    }

    private void HandleTimaOverflow()
    {
        /*
         * After overflowing the TIMA register contains a zero value for a duration of 4 T-cycles. Only after these have
         * elapsed it is reloaded with the value of TMA, and only then a Timer Interrupt is requested.
         */
        if (_timaResetTCycleCountdown != 0)
        {
            return;
        }

        if (_timaResetWriteFlag)
        {
            // Write aborted due to write to _timaResetWriteFlag.
            _timaResetWriteFlag = false;
            return;
        }

        _memory[Memory.TIMA] = Tma;
        RequestTimerInterrupt();
        _timaResetTCycleCountdown = -1;
    }

    private void RequestTimerInterrupt()
    {
        _memory[Memory.InterruptFlag] |= Memory.TimerFlag;
    }

    private static bool GetDivBit(ushort div, byte tac) => (div & GetDivBitMask(tac)) > 0;

    private static ushort GetDivBitMask(byte tac) => (ushort)(0b_1 << GetDivBitNumber(tac));

    private static byte GetDivBitNumber(byte tac) =>
        (tac & 0b_0011) switch
        {
            0b_01 => 3,
            0b_10 => 5,
            0b_11 => 7,
            _ => 9,
        };
}
