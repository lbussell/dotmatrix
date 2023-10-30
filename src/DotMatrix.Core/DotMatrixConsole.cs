namespace DotMatrix.Core;

public class DotMatrixConsole
{
    private readonly Memory _memory;
    private readonly Bus _bus;
    private readonly Cpu _cpu;

    public DotMatrixConsole(BootRom bootRom, Cartridge? cartridge = null)
    {
        _memory = new Memory();
        _bus = new Bus(_memory, cartridge, bootRom);
        _cpu = new Cpu(_bus);
    }

    public DotMatrixConsole(string bootRomPath, string? cartridgePath = null)
        : this(
            new BootRom(File.ReadAllBytes(bootRomPath)),
            string.IsNullOrEmpty(cartridgePath) ? null : new Cartridge(File.ReadAllBytes(cartridgePath)))
    {
    }

    public void TempExecute()
    {
        try
        {
            while (true)
            {
                _cpu.ExecuteFrames(1);
            }
        }
        finally
        {
            Console.WriteLine(_cpu.State);
        }
    }
}
