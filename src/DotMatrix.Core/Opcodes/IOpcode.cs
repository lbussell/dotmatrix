namespace DotMatrix.Core.Opcodes;

public interface IOpcode
{
    public int TCycles { get; }

    public ReadType ReadType { get; }

    public string Format(string? arg = null);
}
