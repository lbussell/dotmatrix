namespace DotMatrix.Core.Tests.Unit;

public class TimerTests
{
    private readonly byte[] _memory;
    private Timer _timer;

    public TimerTests()
    {
        _memory = new byte[Memory.Size];
        _timer = new Timer(_memory);
    }

    [Fact]
    public void InitialValuesAreZero()
    {
        _memory[Memory.DIV].Should().Be(0);
        _memory[Memory.TAC].Should().Be(0);
        _timer.Tac.Should().Be(0);
        _timer.DivHigh8.Should().Be(0);
    }

    [Fact]
    public void TacCanBeSet()
    {
        byte tacValue = 0b_0101;
        _timer.Tac = tacValue;
        _memory[Memory.TAC].Should().Be(tacValue);
    }

    [Fact]
    public void DivIsZeroedOutOnWrite()
    {
        _memory[Memory.DIV].Should().Be(0);
        _timer.TickTCycles(0x0100);
        _timer.DivHigh8.Should().Be(1);
        _timer.DivHigh8 = 0xFF;
        _timer.DivHigh8.Should().Be(0);
    }

    [Fact]
    public void GbedgExample()
    {
        /*
         * From https://github.com/Hacktix/GBEDG/blob/master/timers/index.md#an-example
         */
        _timer.Tac = 0b0;
        _timer.DivHigh8.Should().Be(0);

        _timer.Tac = 0b101;
        _timer.TickTCycles(0b11111);
        byte previousTima = _timer.Tima;
        _timer.DivHigh8.Should().Be(0);

        _timer.TickTCycles(1);
        _timer.DivHigh8.Should().Be(0);
        _timer.Tima.Should().Be((byte)(previousTima + 1));
    }

    [Fact]
    public void GbedgEdgeCase()
    {
        /*
         * From https://github.com/Hacktix/GBEDG/blob/master/timers/index.md#an-example
         */
        _timer.Tac = 0b0;
        _timer.DivHigh8.Should().Be(0);

        _timer.Tac = 0b101;
        _timer.TickTCycles(0b11000);
        byte previousTima = _timer.Tima;
        _timer.TickTCycles(1);
        _timer.Tima.Should().Be(previousTima);

        _timer.Tac = 0;
        _timer.TickTCycles(1);
        _timer.Tima.Should().Be((byte)(previousTima + 1));
    }

    [Fact]
    public void DivIsIncrementedEveryTCycle()
    {
        int tCycles = 0x0100;
        byte expectedDivValue = (byte)((tCycles & 0xFF00) >> 8);

        _memory[Memory.DIV].Should().Be(0);
        _timer.TickTCycles(tCycles);
        _timer.DivHigh8.Should().Be(expectedDivValue);
        _memory[Memory.DIV].Should().Be(expectedDivValue);
    }

    [Theory]
    [InlineData([0b_1_01, 16, true])]
    [InlineData([0b_1_10, 64, true])]
    [InlineData([0b_1_11, 256, true])]
    [InlineData([0b_1_00, 1024, true])]
    [InlineData([0b_0_01, 16, false])]
    [InlineData([0b_0_10, 64, false])]
    [InlineData([0b_0_11, 256, false])]
    [InlineData([0b_0_00, 1024, false])]
    public void TacIsCorrectlyIncremented(byte tacValue, int tCycles, bool isIncremented)
    {
        byte expectedValue = (byte)(isIncremented ? 1 : 0);
        _timer.Tac = tacValue;

        for (int i = 1; i < tCycles; i += 1)
        {
            _timer.TickTCycles(1);
            _timer.Tima.Should().Be(0, $"because we have only ticked {i}/{tCycles} T-Cycles");
        }

        _timer.TickTCycles(1);
        _timer.Tima.Should().Be(expectedValue);
        _memory[Memory.TIMA].Should().Be(expectedValue);
    }

    [Fact]
    public void InterruptEnableIsReadCorrectly()
    {
        _timer.InterruptEnable.Should().Be(0);
        _memory[Memory.InterruptEnable].Should().Be(0);

        _memory[Memory.InterruptEnable] |= Memory.TimerFlag;
        _timer.InterruptEnable.Should().Be(Memory.TimerFlag);
    }

    [Fact]
    public void TimaOverflowsToZero()
    {
        _memory[Memory.TIMA] = 0xFF;
        _timer.Tac = 0b101;
        _timer.TickTCycles(16);
        _timer.Tima.Should().Be(0);
    }

    [Fact]
    public void TimaOverflowRequestsInterrupt()
    {
        SetupTimaOverflow();
        _timer.TickTCycles(1 + 4); // overflow + wait for overflow to finish
        CheckTimerInterruptRequest().Should().BeTrue("because TIMA overflow should request a Timer interrupt");
    }

    [Fact]
    public void TimaOverflowResetIsDelayed()
    {
        _memory[Memory.TIMA] = 0xFF;
        _timer.Tac = 0b101;
        _timer.Tma = 1;

        _timer.TickTCycles(16);
        _timer.Tima.Should().Be(0, "because TIMA should not be reset to TMA until 4 T-Cycles after it overflows");

        _timer.TickTCycles(4);
        _timer.Tima.Should().NotBe(0, "because 4 T-Cycles have elapsed since TIMA overflowed");
    }

    [Fact]
    public void TimaOverflowResetsToTma()
    {
        const byte tmaValue = 0xAA;

        _memory[Memory.TIMA] = 0xFF;
        _timer.Tac = 0b101;
        _timer.Tma = tmaValue;

        _timer.TickTCycles(16 + 4);
        _timer.Tima.Should().Be(tmaValue, "because TIMA should be reset to TMA");
    }

    [Fact]
    public void TmaWriteOnSameCycleAsReload()
    {
        const byte tmaValue = 0xAA;
        const byte secondTmaValue = 0xBB;

        _memory[Memory.TIMA] = 0xFF;
        _timer.Tac = 0b101;
        _timer.Tma = tmaValue;

        _timer.TickTCycles(16 + 3);
        _timer.Tma = secondTmaValue;
        _timer.Tma.Should().Be(secondTmaValue, $"because we wrote 0x{secondTmaValue:X2} to TMA");
        _timer.TickTCycles(1);
        _timer.Tima.Should().Be(secondTmaValue,
            $"TMA was updated from 0x{tmaValue:X2} to 0x{secondTmaValue:X2} just before the reload to TIMA");
    }

    [Fact]
    public void TimaWriteOnSameCycleAsReload()
    {
        const byte tmaValue = 0xAA;
        const byte timaValue = 0xBB;

        _memory[Memory.TIMA] = 0xFF;
        _timer.Tac = 0b101;
        _timer.Tma = tmaValue;

        _timer.TickTCycles(16 + 3);
        _timer.Tima = timaValue;
        _timer.Tima.Should().Be(timaValue, $"because we wrote 0x{timaValue:X2} to TIMA");
        _timer.TickTCycles(1);
        _timer.Tima.Should().Be(tmaValue,
            "because writes to TIMA on the same T-cycle as the reload from TMA should be ignored");
    }

    [Fact]
    public void TimaWriteAbort()
    {
        const byte tmaValue = 0xAA;
        const byte timaValue = 0xBB;

        SetupTimaIncrement();
        _timer.Tma = tmaValue;

        // Write to TIMA while the overflow is processing to trigger the interrupt abort process.
        _timer.Tima = timaValue;

        _timer.Tima.Should().Be(timaValue,
            $"because we wrote 0x{timaValue:X2} to TIMA while waiting on overflow to complete.");

        _timer.TickTCycles(1);

        _timer.Tima.Should().Be(timaValue + 1,
            "because writes to TIMA before overflow is completed cancels TMA reload");
        CheckTimerInterruptRequest().Should().BeFalse("because the Interrupt Request was aborted by writing to TIMA");
    }

    private void SetupTimaIncrement()
    {
        _timer.Tac = 0b101;
        _timer.TickTCycles(15);
    }

    private void SetupTimaOverflow()
    {
        _memory[Memory.TIMA] = 0xFF;
        SetupTimaIncrement();
    }

    private bool CheckTimerInterruptRequest() => (_memory[Memory.InterruptFlag] & Memory.TimerFlag) > 0;
}
