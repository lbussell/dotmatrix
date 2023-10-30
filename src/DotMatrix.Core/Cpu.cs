namespace DotMatrix.Core;

using DotMatrix.Core.Opcodes;

internal delegate int Instruction();

internal sealed class Cpu
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

    private Instruction[] CreateInstructions()
    {
        Instruction[] i = Enumerable.Range(0, 256).Select<int, Instruction>(op => () => Control.NotImplemented((byte)op)).ToArray();

        i[0x01] = () => Load16.LoadImmediate(_bus, ref _cpuState.BC, ref _cpuState.PC);

        i[0x11] = () => Load16.LoadImmediate(_bus, ref _cpuState.DE, ref _cpuState.PC);

        i[0x21] = () => Load16.LoadImmediate(_bus, ref _cpuState.HL, ref _cpuState.PC);

        i[0x31] = () => Load16.LoadImmediate(_bus, ref _cpuState.SP, ref _cpuState.PC);

        i[0x40] = () => Load8.Load(ref _cpuState.B, ref _cpuState.B);
        i[0x41] = () => Load8.Load(ref _cpuState.B, ref _cpuState.C);
        i[0x42] = () => Load8.Load(ref _cpuState.B, ref _cpuState.D);
        i[0x43] = () => Load8.Load(ref _cpuState.B, ref _cpuState.E);
        i[0x44] = () => Load8.Load(ref _cpuState.B, ref _cpuState.H);
        i[0x45] = () => Load8.Load(ref _cpuState.B, ref _cpuState.L);

        i[0x47] = () => Load8.Load(ref _cpuState.B, ref _cpuState.A);

        i[0xF9] = () => Load16.LoadSPFromHL(ref _cpuState);

        i[0xA8] = () => Alu8.Xor(ref _cpuState, ref _cpuState.B);
        i[0xA9] = () => Alu8.Xor(ref _cpuState, ref _cpuState.C);
        i[0xAA] = () => Alu8.Xor(ref _cpuState, ref _cpuState.D);
        i[0xAB] = () => Alu8.Xor(ref _cpuState, ref _cpuState.E);
        i[0xAC] = () => Alu8.Xor(ref _cpuState, ref _cpuState.H);
        i[0xAD] = () => Alu8.Xor(ref _cpuState, ref _cpuState.L);
        i[0xAE] = () => Alu8.XorHLIndirect(_bus, ref _cpuState);
        i[0xAF] = () => Alu8.Xor(ref _cpuState, ref _cpuState.A);

        i[0xEE] = () => Alu8.XorImmediate(_bus, ref _cpuState);

        return i;
    }
}
