namespace DotMatrix.Core.Opcodes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
internal sealed class OpcodeAttribute(byte opcode) : Attribute
{
    public byte Opcode { get; } = opcode;

    public byte Prefix { get; init; } = 0;

    public CpuRegister R { get; init; } = CpuRegister.Implied;
}
