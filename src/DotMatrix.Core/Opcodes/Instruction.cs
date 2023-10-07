namespace DotMatrix.Core.Opcodes;

public sealed class Instruction(ushort addr, IOpcode opcode, ushort? arg = null)
{
    public override string ToString()
    {
        string formattedArg = arg == null
            ? string.Empty
            : $",${arg:X4}";

        return $"${addr:X4}: {opcode.Format($"{arg:X2}")}{formattedArg}";
    }
}
