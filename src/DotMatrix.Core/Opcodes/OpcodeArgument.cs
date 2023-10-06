namespace DotMatrix.Core.Opcodes;

public enum OpcodeArgumentType
{
    /* Read data from PC */
    Immediate,
    /* Read data from the address stored in register */
    Indirect,
    /* Read data from the register directly */
    Direct,
}

public sealed class OpcodeArgument
{
    public CpuRegister Register { get; init; } = CpuRegister.Implied;

    public OpcodeArgumentType Type { get; init; } = OpcodeArgumentType.Direct;
}
