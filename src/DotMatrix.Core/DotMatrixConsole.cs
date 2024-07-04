using DotMatrix.Core.Instructions;

namespace DotMatrix.Core;

public class DotMatrixConsole
{
    private readonly Cpu _cpu;
    private readonly Bus _bus;

    private DotMatrixConsole(Cpu cpu, Bus bus)
    {
        _cpu = cpu;
        _bus = bus;
    }

    public static DotMatrixConsole CreateInstance(byte[] rom, byte[]? bios = null, bool loggingEnabled = false)
    {
        Bus bus = new(rom, bios);

        OpcodeHandler opcodeHandler = new();

        Cpu cpu = new(bus, opcodeHandler, CpuState.GetPostBootRomState())
        {
            LoggingEnabled = loggingEnabled,
        };

        return new DotMatrixConsole(cpu, bus);
    }

    public void Run()
    {
        _cpu.Run(int.MaxValue);
    }
}
