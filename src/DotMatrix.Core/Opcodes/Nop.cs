namespace DotMatrix.Core.Opcodes;

[Opcode(0x00)]
public class Nop : IOpcode
{
    public int TCycles => 4;

    public ReadType ReadType => ReadType.None;

    public string Format(string? arg = null) => "NOP";
}
