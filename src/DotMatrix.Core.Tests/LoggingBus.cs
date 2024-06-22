namespace DotMatrix.Core.Tests;

internal class LoggingBus : IBus
{
    private readonly IDictionary<ushort, byte> _memory;

    public LoggingBus(int[][] ram)
    {
        _memory = new Dictionary<ushort, byte>();
        foreach (int[] pairing in ram)
        {
            _memory[(ushort)pairing[0]] = (byte)pairing[1];
        }
    }

    public List<CpuLog> Log { get; } = [];

    public byte this[ushort address]
    {
        get => Get(address);
        set => Set(address, value);
    }

    private byte Get(ushort address)
    {
        byte value = _memory[address];
        Log.Add(new CpuLog(address, value, ActivityType.Read));
        return value;
    }

    private void Set(ushort address, byte value)
    {
        _memory[address] = value;
        Log.Add(new CpuLog(address, value, ActivityType.Write));
    }
}
