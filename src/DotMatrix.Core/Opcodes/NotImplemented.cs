namespace DotMatrix.Core.Opcodes;

public class NotImplemented : IOpcode
{
    public int TCycles => 4;

    public ReadType ReadType => ReadType.None;

    public string Format(string? arg) => "Not Implemented";
}
