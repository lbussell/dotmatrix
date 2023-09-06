namespace DotMatrix.Core;

public class AddressException : ArgumentOutOfRangeException
{
    public AddressException(ushort addr)
        : base(nameof(addr), $"Address {addr} is not writable.")
    {
    }
}