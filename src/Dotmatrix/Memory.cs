namespace DotMatrix;

public interface IMemory
{
    byte this[uint addr] { get; set; }
}

public class Memory : IMemory
{
    private readonly byte[] _memory = new byte[0xFFFF];

    public byte this[uint addr]
    {
        get => _memory[addr];
        set => _memory[addr] = value;
    }
}
