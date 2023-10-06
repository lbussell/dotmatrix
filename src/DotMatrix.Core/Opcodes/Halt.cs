namespace DotMatrix.Core.Opcodes;

[Opcode(0x76)]
public class Halt : IOpcode
{
    public int TCycles => 4;

    public ReadType ReadType => ReadType.None;

    public string Format(string? arg = null) => "HALT";
}
