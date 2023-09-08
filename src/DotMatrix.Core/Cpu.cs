using System.Runtime.InteropServices;

namespace DotMatrix.Core;

public class Cpu
{
    private const int CyclesPerFrame = DotMatrixConsoleSpecs.CpuSpeed / DotMatrixConsoleSpecs.FramesPerSecond;

    private Bus _bus;
    private CpuState _cpuState = default(CpuState);

    public Cpu(Bus bus)
    {
        _bus = bus;
    }

    public void EmulateFrame()
    {
        for (int i = 0; i < CyclesPerFrame; i += 1)
        {
            ExecuteCycle();
        }
    }

    public void ExecuteCycle()
    {
        Console.WriteLine(_cpuState);

        ushort instruction = (ushort)(_bus[_cpuState.PC] | _bus[(ushort)(_cpuState.PC + 1)] << 2);
        Print(instruction);
        _cpuState.PC += DotMatrixConsoleSpecs.InstructionSizeInBytes;
    }

    private static void Print(ushort n) => Console.WriteLine(n.ToString("X4"));

    private static void Print(byte b) => Console.WriteLine(b.ToString("X2"));
}
