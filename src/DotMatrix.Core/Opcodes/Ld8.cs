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
internal sealed class Load8(CpuRegister targetRegister, CpuRegister sourceRegister) : IOpcode
{
    // Ex. LD A,B
    public string Name => $"LD {CpuState.Name(targetRegister)},{CpuState.Name(sourceRegister)}";

    public int TCycles => 4;

    public ReadType ReadType => ReadType.None;
}

[Opcode(0x46, CpuRegister.B, CpuRegister.HL)]
[Opcode(0x4E, CpuRegister.C, CpuRegister.HL)]
[Opcode(0x56, CpuRegister.D, CpuRegister.HL)]
[Opcode(0x5E, CpuRegister.E, CpuRegister.HL)]
[Opcode(0x66, CpuRegister.H, CpuRegister.HL)]
[Opcode(0x6E, CpuRegister.L, CpuRegister.HL)]
[Opcode(0x7E, CpuRegister.A, CpuRegister.HL)]
internal sealed class Load8Indirect(CpuRegister targetRegister, CpuRegister sourceRegisterIndirect) : IOpcode
{
    // Ex. LD B,(HL)
    public string Name => $"LD {CpuState.Name(targetRegister)},({CpuState.Name(sourceRegisterIndirect)})";

    public int TCycles => 8;

    public ReadType ReadType => ReadType.Read8;
}

[Opcode(0x70, CpuRegister.HL, CpuRegister.B)]
[Opcode(0x71, CpuRegister.HL, CpuRegister.C)]
[Opcode(0x72, CpuRegister.HL, CpuRegister.D)]
[Opcode(0x73, CpuRegister.HL, CpuRegister.E)]
[Opcode(0x74, CpuRegister.HL, CpuRegister.H)]
[Opcode(0x75, CpuRegister.HL, CpuRegister.L)]
[Opcode(0x77, CpuRegister.HL, CpuRegister.A)]
internal sealed class Store8Indirect(CpuRegister targetRegisterIndirect, CpuRegister sourceRegister) : IOpcode
{
    // Ex. LD (HL),B
    public string Name => $"LD ({CpuState.Name(targetRegisterIndirect)}),({CpuState.Name(sourceRegister)})";

    public int TCycles => 8;

    public ReadType ReadType => ReadType.Read8;
}

[Opcode(0x06, CpuRegister.B)]
[Opcode(0x0E, CpuRegister.C)]
[Opcode(0x16, CpuRegister.D)]
[Opcode(0x1E, CpuRegister.E)]
[Opcode(0x26, CpuRegister.H)]
[Opcode(0x2E, CpuRegister.L)]
internal sealed class Load8Immediate(CpuRegister targetRegisterIndirect) : IOpcode
{
    // Ex. LD (HL),$FE
    public string Name => $"LD ({CpuState.Name(targetRegisterIndirect)})";

    public int TCycles => 8;

    public ReadType ReadType => ReadType.Read8;
}
