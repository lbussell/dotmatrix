namespace DotMatrix.Core.Opcodes;

/* Target B */
[Opcode(0x40, R = CpuRegister.B, R2 = CpuRegister.B)]
[Opcode(0x41, R = CpuRegister.B, R2 = CpuRegister.C)]
[Opcode(0x42, R = CpuRegister.B, R2 = CpuRegister.D)]
[Opcode(0x43, R = CpuRegister.B, R2 = CpuRegister.E)]
[Opcode(0x44, R = CpuRegister.B, R2 = CpuRegister.H)]
[Opcode(0x45, R = CpuRegister.B, R2 = CpuRegister.L)]
[Opcode(0x47, R = CpuRegister.B, R2 = CpuRegister.A)]
/* Target C */
[Opcode(0x48, R = CpuRegister.C, R2 = CpuRegister.B)]
[Opcode(0x49, R = CpuRegister.C, R2 = CpuRegister.C)]
[Opcode(0x4A, R = CpuRegister.C, R2 = CpuRegister.D)]
[Opcode(0x4B, R = CpuRegister.C, R2 = CpuRegister.E)]
[Opcode(0x4C, R = CpuRegister.C, R2 = CpuRegister.H)]
[Opcode(0x4D, R = CpuRegister.C, R2 = CpuRegister.L)]
[Opcode(0x4F, R = CpuRegister.C, R2 = CpuRegister.A)]
internal sealed class Ld8(CpuRegister targetRegister, CpuRegister sourceRegister) : IOpcode
{
    public string Name => $"LD {CpuState.Name(targetRegister)},{CpuState.Name(sourceRegister)}";

    public int TCycles => 4;

    public ReadType ReadType => ReadType.None;

    public CpuState Execute(CpuState cpuState, ushort? arg)
    {
        cpuState[targetRegister] = cpuState[sourceRegister];
        return cpuState;
    }
}
