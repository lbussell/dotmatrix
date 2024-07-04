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

        CpuState initialState = new()
        {
            A = 0x01,
            F = 0xB0,
            B = 0x00,
            C = 0x13,
            D = 0x00,
            E = 0xD8,
            H = 0x01,
            L = 0x4D,
            Sp = 0xFFFE,
            Pc = 0x0100,
        };

        Cpu cpu = new(bus, opcodeHandler, loggingEnabled, initialState);

        return new DotMatrixConsole(cpu, bus);
    }

    public void Run()
    {
        _cpu.Run(int.MaxValue);
    }
}
