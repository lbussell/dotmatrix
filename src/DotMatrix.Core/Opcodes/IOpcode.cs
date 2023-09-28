namespace DotMatrix.Core.Opcodes;

public interface IOpcode
{
    public string Name { get; }

    public int TCycles { get; }

    public ReadType ReadType { get; }
}
