namespace DotMatrix.Core;

using DotMatrix.Core.Opcodes;

internal delegate int Instruction();

internal sealed partial class Cpu
{
    private const int CyclesPerFrame = ConsoleSpecs.CpuSpeed / ConsoleSpecs.FramesPerSecond;

    private readonly Bus _bus;
    private readonly Instruction[] _instructions;
    private CpuState _cpuState;
    private int _cycles = 0;
    private int _cyclesSinceLastFrame = 0;

    public Cpu(Bus bus)
    {
        _bus = bus;
        _instructions = CreateInstructions();
    }

    public CpuState State => _cpuState;

    public long Cycles => _cycles;

    public void ExecuteFrames(int frames = 1, int cycles = CyclesPerFrame)
    {
        while (_cyclesSinceLastFrame < cycles)
        {
            int elapsedCycles = ExecuteCycle();

            /* Tick PPU/APU with elapsedCycles here */

            _cyclesSinceLastFrame += elapsedCycles;
            _cycles += elapsedCycles;
        }

        /* Read inputs */

        /* Update display */

        _cyclesSinceLastFrame %= CyclesPerFrame;
    }

    private int ExecuteCycle()
    {
        Console.WriteLine(_cpuState);

        byte instruction = _bus.ReadInc8(ref _cpuState.PC);

        return _instructions[instruction]();
    }
}
