namespace DotMatrix.Core.Opcodes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
internal sealed class OpcodeAttribute(byte opcode) : Attribute
{
    public OpcodeAttribute(byte opcode, CpuRegister r)
        : this(opcode)
    {
        R = r;
    }

    public OpcodeAttribute(byte opcode, CpuRegister r, CpuRegister r2)
        : this(opcode, r)
    {
        R2 = r2;
    }

    public bool Prefix { get; init; } = false;

    public byte Opcode => opcode;

    public CpuRegister R { get; }

    public CpuRegister R2 { get; }
}
