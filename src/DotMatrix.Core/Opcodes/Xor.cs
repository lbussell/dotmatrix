namespace DotMatrix.Core.Opcodes;

[Opcode(0xA8, CpuRegister.B)]
[Opcode(0xA9, CpuRegister.C)]
[Opcode(0xAA, CpuRegister.D)]
[Opcode(0xAB, CpuRegister.E)]
[Opcode(0xAC, CpuRegister.H)]
[Opcode(0xAD, CpuRegister.L)]
[Opcode(0xAF, CpuRegister.A)]
internal sealed class Xor(CpuRegister r) : IOpcode
{
    public int TCycles => 4;

    public ReadType ReadType => ReadType.None;

    public string Format(string? arg = null) => $"XOR A,{Enum.GetName(typeof(CpuRegister), r)}";
}
