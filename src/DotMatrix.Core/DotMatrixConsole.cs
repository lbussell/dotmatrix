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
        Bus bus = new(bios, rom);

        OpcodeHandler<int> opcodeHandler = new(new CpuInstructionHandler());
        Cpu cpu = new(bus, opcodeHandler);

        return new DotMatrixConsole(cpu, bus);
    }

    public void Run()
    {
        _cpu.Run(0xFF);
    }
}
