namespace DotMatrix.Core.Opcodes;

internal static class Control
{
    public static int NoOp() => 4;

    public static int NotImplemented(byte op) => throw new NotImplementedException($"Opcode 0x{op:X2} is not implemented yet.");
}
