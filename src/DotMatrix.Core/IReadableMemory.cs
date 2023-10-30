namespace DotMatrix.Core;

public interface IReadableMemory
{
    public int Length { get; }

    public byte Read8(ushort addr);

    public ushort Read16(ushort addr);
}
