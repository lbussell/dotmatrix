namespace DotMatrix.Core.Opcodes;

[Opcode(0x10)]
public class Stop : IOpcode
{
    public string Name => "STOP";

    public int TCycles => 4;

    public ReadType ReadType => ReadType.None;
}
