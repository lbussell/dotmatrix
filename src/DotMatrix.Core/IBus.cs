namespace DotMatrix.Core;

public interface IBus
{
    byte this[ushort address] { get; set; }
}
