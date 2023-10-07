namespace DotMatrix.Core.Opcodes;

/* Target B */
[Opcode(0x40, CpuRegister.B, CpuRegister.B)]
[Opcode(0x41, CpuRegister.B, CpuRegister.C)]
[Opcode(0x42, CpuRegister.B, CpuRegister.D)]
[Opcode(0x43, CpuRegister.B, CpuRegister.E)]
[Opcode(0x44, CpuRegister.B, CpuRegister.H)]
[Opcode(0x45, CpuRegister.B, CpuRegister.L)]
[Opcode(0x47, CpuRegister.B, CpuRegister.A)]
/* Target C */
[Opcode(0x48, CpuRegister.C, CpuRegister.B)]
[Opcode(0x49, CpuRegister.C, CpuRegister.C)]
[Opcode(0x4A, CpuRegister.C, CpuRegister.D)]
[Opcode(0x4B, CpuRegister.C, CpuRegister.E)]
[Opcode(0x4C, CpuRegister.C, CpuRegister.H)]
[Opcode(0x4D, CpuRegister.C, CpuRegister.L)]
[Opcode(0x4F, CpuRegister.C, CpuRegister.A)]
/* Target D */
[Opcode(0x50, CpuRegister.D, CpuRegister.B)]
[Opcode(0x51, CpuRegister.D, CpuRegister.C)]
[Opcode(0x52, CpuRegister.D, CpuRegister.D)]
[Opcode(0x53, CpuRegister.D, CpuRegister.E)]
[Opcode(0x54, CpuRegister.D, CpuRegister.H)]
[Opcode(0x55, CpuRegister.D, CpuRegister.L)]
[Opcode(0x57, CpuRegister.D, CpuRegister.A)]
/* Target E */
[Opcode(0x58, CpuRegister.E, CpuRegister.B)]
[Opcode(0x59, CpuRegister.E, CpuRegister.C)]
[Opcode(0x5A, CpuRegister.E, CpuRegister.D)]
[Opcode(0x5B, CpuRegister.E, CpuRegister.E)]
[Opcode(0x5C, CpuRegister.E, CpuRegister.H)]
[Opcode(0x5D, CpuRegister.E, CpuRegister.L)]
[Opcode(0x5F, CpuRegister.E, CpuRegister.A)]
/* Target H */
[Opcode(0x60, CpuRegister.H, CpuRegister.B)]
[Opcode(0x61, CpuRegister.H, CpuRegister.C)]
[Opcode(0x62, CpuRegister.H, CpuRegister.D)]
[Opcode(0x63, CpuRegister.H, CpuRegister.E)]
[Opcode(0x64, CpuRegister.H, CpuRegister.H)]
[Opcode(0x65, CpuRegister.H, CpuRegister.L)]
[Opcode(0x67, CpuRegister.H, CpuRegister.A)]
/* Target L */
[Opcode(0x68, CpuRegister.L, CpuRegister.B)]
[Opcode(0x69, CpuRegister.L, CpuRegister.C)]
[Opcode(0x6A, CpuRegister.L, CpuRegister.D)]
[Opcode(0x6B, CpuRegister.L, CpuRegister.E)]
[Opcode(0x6C, CpuRegister.L, CpuRegister.H)]
[Opcode(0x6D, CpuRegister.L, CpuRegister.L)]
[Opcode(0x6F, CpuRegister.L, CpuRegister.A)]
/* Target A */
[Opcode(0x78, CpuRegister.A, CpuRegister.B)]
[Opcode(0x79, CpuRegister.A, CpuRegister.C)]
[Opcode(0x7A, CpuRegister.A, CpuRegister.D)]
[Opcode(0x7B, CpuRegister.A, CpuRegister.E)]
[Opcode(0x7C, CpuRegister.A, CpuRegister.H)]
[Opcode(0x7D, CpuRegister.A, CpuRegister.L)]
[Opcode(0x7F, CpuRegister.A, CpuRegister.A)]
internal sealed class Load8(CpuRegister target, CpuRegister source)
    : Load8Base(
        target: new OpcodeArgument { Register = target },
        source: new OpcodeArgument { Register = source },
        IndirectSpecialCase.None)
{
}

/* Load various */
[Opcode(0x0A, CpuRegister.A, CpuRegister.BC)]
[Opcode(0x1A, CpuRegister.A, CpuRegister.DE)]
/* Load HL */
[Opcode(0x46, CpuRegister.B, CpuRegister.HL)]
[Opcode(0x4E, CpuRegister.C, CpuRegister.HL)]
[Opcode(0x56, CpuRegister.D, CpuRegister.HL)]
[Opcode(0x5E, CpuRegister.E, CpuRegister.HL)]
[Opcode(0x66, CpuRegister.H, CpuRegister.HL)]
[Opcode(0x6E, CpuRegister.L, CpuRegister.HL)]
[Opcode(0x7E, CpuRegister.A, CpuRegister.HL)]
internal sealed class Load8Indirect(CpuRegister target, CpuRegister indirectSource)
    : Load8Base(
        target: new OpcodeArgument { Register = target },
        source: new OpcodeArgument { Register = indirectSource, Type = OpcodeArgumentType.Indirect },
        IndirectSpecialCase.None)
{
}

[Opcode(0x2A, CpuRegister.A, CpuRegister.HL)] // inc
internal sealed class Load8IndirectSpecialIncrement(CpuRegister target, CpuRegister indirectSource)
    : Load8Base(
        target: new OpcodeArgument { Register = target },
        source: new OpcodeArgument { Register = indirectSource, Type = OpcodeArgumentType.Indirect },
        IndirectSpecialCase.Increment)
{
}

[Opcode(0x3A, CpuRegister.A, CpuRegister.HL)] // dec
internal sealed class Load8IndirectSpecialDecrement(CpuRegister target, CpuRegister indirectSource)
    : Load8Base(
        target: new OpcodeArgument { Register = target },
        source: new OpcodeArgument { Register = indirectSource, Type = OpcodeArgumentType.Indirect },
        IndirectSpecialCase.Decrement)
{
}

/* Target address in HL */
[Opcode(0x70, CpuRegister.HL, CpuRegister.B)]
[Opcode(0x71, CpuRegister.HL, CpuRegister.C)]
[Opcode(0x72, CpuRegister.HL, CpuRegister.D)]
[Opcode(0x73, CpuRegister.HL, CpuRegister.E)]
[Opcode(0x74, CpuRegister.HL, CpuRegister.H)]
[Opcode(0x75, CpuRegister.HL, CpuRegister.L)]
[Opcode(0x77, CpuRegister.HL, CpuRegister.A)]
/* Target various */
[Opcode(0x02, CpuRegister.BC, CpuRegister.A)]
[Opcode(0x12, CpuRegister.DE, CpuRegister.A)]
internal sealed class Store8Indirect(CpuRegister targetIndirect, CpuRegister source)
    : Load8Base(
        target: new OpcodeArgument { Register = targetIndirect, Type = OpcodeArgumentType.Indirect },
        source: new OpcodeArgument { Register = source })
{
}

[Opcode(0x32, CpuRegister.HL, CpuRegister.A)]
internal sealed class Store8IndirectDecrement(CpuRegister targetIndirect, CpuRegister source)
    : Load8Base(
        target: new OpcodeArgument { Register = targetIndirect, Type = OpcodeArgumentType.Indirect },
        source: new OpcodeArgument { Register = source },
        IndirectSpecialCase.Decrement)
{
}

[Opcode(0x22, CpuRegister.HL, CpuRegister.A)]
internal sealed class Store8IndirectIncrement(CpuRegister targetIndirect, CpuRegister source)
    : Load8Base(
        target: new OpcodeArgument { Register = targetIndirect, Type = OpcodeArgumentType.Indirect },
        source: new OpcodeArgument { Register = source },
        IndirectSpecialCase.Increment)
{
}

[Opcode(0x06, CpuRegister.B)]
[Opcode(0x0E, CpuRegister.C)]
[Opcode(0x16, CpuRegister.D)]
[Opcode(0x1E, CpuRegister.E)]
[Opcode(0x26, CpuRegister.H)]
[Opcode(0x2E, CpuRegister.L)]
[Opcode(0x3E, CpuRegister.A)]
internal sealed class Load8Immediate(CpuRegister target)
    : Load8Base(
        target: new OpcodeArgument { Register = target },
        source: new OpcodeArgument { Type = OpcodeArgumentType.Immediate })
{
}

[Opcode(0x36, CpuRegister.HL)]
internal sealed class Store8Immediate(CpuRegister targetIndirect)
    : Load8Base(
        target: new OpcodeArgument { Register = targetIndirect, Type = OpcodeArgumentType.Indirect },
        source: new OpcodeArgument { Type = OpcodeArgumentType.Immediate })
{
}

internal abstract class Load8Base(
    OpcodeArgument target,
    OpcodeArgument source,
    IndirectSpecialCase indirectSpecialCase = IndirectSpecialCase.None)
    : IOpcode
{
    private OpcodeArgument _target = target;
    private OpcodeArgument _source = source;
    private IndirectSpecialCase _indirectSpecialCase = indirectSpecialCase;

    public int TCycles
    {
        get
        {
            int tCycles = 4;
            if (target.Type != OpcodeArgumentType.Direct)
            {
                tCycles += 4;
            }

            if (source.Type != OpcodeArgumentType.Direct)
            {
                tCycles += 4;
            }

            return tCycles;
        }
    }

    public ReadType ReadType =>
        target.Type == OpcodeArgumentType.Immediate
        || source.Type == OpcodeArgumentType.Immediate
            ? ReadType.Read8
            : ReadType.None;

    public string Format(string? arg)
    {
        return $"LD {FormatArgument(target, arg)},{FormatArgument(source, arg)}";
    }

    private string FormatArgument(OpcodeArgument argument, string? arg)
    {
        if (argument.Type == OpcodeArgumentType.Immediate)
        {
            return arg ?? "nn";
        }

        string sign = indirectSpecialCase switch
        {
            IndirectSpecialCase.Increment => "+",
            IndirectSpecialCase.Decrement => "-",
            _ => string.Empty,
        };

        return $"{WrapIndirect(argument, sign)}";

        string WrapIndirect(OpcodeArgument op, string sign)
            => op.Type == OpcodeArgumentType.Indirect
                ? $"({CpuState.Name(op.Register)}{sign})"
                : CpuState.Name(op.Register);
    }
}
