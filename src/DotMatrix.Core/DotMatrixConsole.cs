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

    public static DotMatrixConsole CreateInstance(byte[] bios, byte[] rom)
    {
        Bus bus = new(rom, bios);

        OpcodeHandler opcodeHandler = new();
        Cpu cpu = new(bus, opcodeHandler);

        return new DotMatrixConsole(cpu, bus);
    }

    public void Run()
    {
        try
        {
            _cpu.RunUntil(0x100);
            Console.WriteLine(_cpu.State);
        }
        catch (Exception _)
        {
            Console.WriteLine(_cpu.State);
            throw;
        }
    }
}
