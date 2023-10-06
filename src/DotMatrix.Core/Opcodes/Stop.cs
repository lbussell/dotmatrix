namespace DotMatrix.Core.Opcodes;

[Opcode(0x10)]
public class Stop : IOpcode
{
    public int TCycles => 4;

    public ReadType ReadType => ReadType.None;

    public string Format(string? arg = null) => "STOP";
}
