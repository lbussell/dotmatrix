namespace DotMatrix.Core;

public sealed class Cpu
{
    private const int CyclesPerFrame = DotMatrixConsoleSpecs.CpuSpeed / DotMatrixConsoleSpecs.FramesPerSecond;

    private Bus _bus;
    private IDisplay _display;
    private CpuState _cpuState = default(CpuState);
    private int _cyclesSinceLastFrame = 0;

    public Cpu(Bus bus, IDisplay display)
    {
        _bus = bus;
        _display = display;
    }

    /**
     * Overall number of Cycles since the CPU was started.
     */
    public long Cycles { get; set; } = 0;

    public void ExecuteFrame(int cycles = CyclesPerFrame)
    {
        while (_cyclesSinceLastFrame < cycles)
        {
            int elapsedCycles = ExecuteCycle();
            /* Tick PPU/APU with elapsedCycles here */
            _cyclesSinceLastFrame += elapsedCycles;
            Cycles += elapsedCycles;
        }

        /* Read inputs */

        _display.RequestRefresh();

        _cyclesSinceLastFrame %= CyclesPerFrame;
    }

    public int ExecuteCycle()
    {
        Console.WriteLine(_cpuState);

        ushort instruction = (ushort)((ushort)(_bus[_cpuState.PC] << 8) | _bus[(ushort)(_cpuState.PC + 1)]);
        Print(instruction);
        _cpuState.PC += DotMatrixConsoleSpecs.InstructionSizeInBytes;

        // TODO: A cycle will not always be 4 T-cycles
        return 4;
    }

    private static void Print(ushort n) => Console.WriteLine(n.ToString("X4"));

    private static void Print(byte b) => Console.WriteLine(b.ToString("X2"));
}
