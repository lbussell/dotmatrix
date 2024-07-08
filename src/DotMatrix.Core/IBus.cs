namespace DotMatrix.Core;

public interface IBus
{
    ITimer Timer { get; }
    byte this[ushort address] { get; set; }
}
