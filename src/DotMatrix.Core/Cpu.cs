namespace DotMatrix.Core;

using System.Reflection;
using System.Runtime.CompilerServices;
using DotMatrix.Core.Opcodes;

public sealed class Cpu(Bus bus, IDisplay display)
{
    private const int CyclesPerFrame = ConsoleSpecs.CpuSpeed / ConsoleSpecs.FramesPerSecond;

    private readonly IOpcode _notImplementedInstruction = new NotImplemented();
    private CpuState _cpuState;
    private int _cyclesSinceLastFrame = 0;

    public event EventHandler<CpuState>? CpuStateChanged;

    public CpuState CpuState => _cpuState;

    public long Cycles { get; private set; } = 0;

    public void ExecuteFrame(int cycles = CyclesPerFrame)
    {
        while (_cyclesSinceLastFrame < cycles)
        {
            int elapsedCycles = ExecuteCycle();
            OnCpuStateChanged();
            /* Tick PPU/APU with elapsedCycles here */
            _cyclesSinceLastFrame += elapsedCycles;
            Cycles += elapsedCycles;
        }

        /* Read inputs */
        display.RequestRefresh();
        _cyclesSinceLastFrame %= CyclesPerFrame;
    }

    private int ExecuteCycle()
    {
        throw new NotImplementedException("CPU is not implemented yet.");
    }

    private void OnCpuStateChanged() => CpuStateChanged?.Invoke(this, _cpuState);
}
