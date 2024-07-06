using DotMatrix.Core.Instructions;

namespace DotMatrix.Core;

public class DotMatrixConsole
{
    private readonly Cpu _cpu;
    private readonly IBus _bus;

    private DotMatrixConsole(Cpu cpu, IBus bus)
    {
        _cpu = cpu;
        _bus = bus;
    }

    public static DotMatrixConsole CreateInstance(
        byte[] rom,
        byte[]? bios = null,
        LoggingType loggingType = LoggingType.None,
        Action<string>? logAction = null)
    {
        IBus bus = new Bus(rom, bios);

        if (loggingType != LoggingType.None)
        {
            logAction ??= Console.WriteLine;
            if (loggingType == LoggingType.Serial)
            {
                bus = new SerialLoggingBus(bus, logAction);
            }
        }

        OpcodeHandler opcodeHandler = new();
        Cpu cpu = new(bus, opcodeHandler, CpuState.GetPostBootRomState())
        {
            LoggingEnabled = loggingType == LoggingType.CpuState,
        };

        return new DotMatrixConsole(cpu, bus);
    }

    public void Run(CancellationToken cancellationToken)
    {
        _cpu.Run(cancellationToken);
    }
}
