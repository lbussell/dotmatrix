using System.Text;

namespace DotMatrix.Core;

internal sealed class SerialLoggingBus(IBus innerBus, Action<string> log) : IBus
{
    private readonly IBus _innerBus = innerBus;
    private readonly Action<string> _log = log;
    private readonly StringBuilder _logBuffer = new();

    public ITimer Timer => _innerBus.Timer;

    public byte this[ushort address]
    {
        get => _innerBus[address];
        set => Write(address, value);
    }

    private void Write(ushort address, byte value)
    {
        _innerBus[address] = value;
        if (address == Memory.SB)
        {
            Log((char)value);
        }
    }

    private void Log(char c)
    {
        if (c == '\n')
        {
            _log(_logBuffer.ToString());
            _logBuffer.Clear();
        }
        else
        {
            _logBuffer.Append(c);
        }
    }
}
