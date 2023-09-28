namespace DotMatrix.Core.Opcodes;

public sealed class Instruction
{
    private readonly IOpcode _opcode;
    private readonly ushort? _arg;

    public Instruction(IOpcode opcode, ushort arg)
    {
        _opcode = opcode;
        _arg = arg;
    }

    public Instruction(IOpcode opcode)
    {
        _opcode = opcode;
        _arg = null;
    }

    public override string ToString()
    {
        return _opcode.Name + _opcode.ReadType switch
        {
            ReadType.Read8 => $", ${(byte)_arg!:X2}",
            ReadType.Read16 => $", ${(ushort)_arg!:X4}",
            _ => string.Empty,
        };
    }
}
