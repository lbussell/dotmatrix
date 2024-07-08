namespace DotMatrix.Core.Tests.Opcodes;

internal class LoggingBus : IBus
{
    private readonly IDictionary<ushort, byte> _memory;

    public ITimer Timer { get; } = new MockTimer();

    public LoggingBus(IEnumerable<int[]> ram)
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
        get => GetInternal(address);
        set => SetInternal(address, value);
    }

    private byte GetInternal(ushort address)
    {
        if (!_memory.TryGetValue(address, out byte value)) return 0;
        Log.Add(new CpuLog(address, value, ActivityType.Read));
        return value;
    }

    private void SetInternal(ushort address, byte value)
    {
        _memory[address] = value;
        Log.Add(new CpuLog(address, value, ActivityType.Write));
    }

    private class MockTimer : ITimer
    {
        public void TickTCycles(int numTCycles) { }
    }
}
