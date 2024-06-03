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
        Bus bus = new Bus(bios, rom);
        Cpu cpu = new Cpu(bus);
        return new DotMatrixConsole(cpu, bus);
    }

    public void Run()
    {
        _cpu.Run(0xFF);
    }
}
