namespace DotMatrix.Core.Opcodes;

[Opcode(0x01, CpuRegister.BC)]
[Opcode(0x11, CpuRegister.DE)]
[Opcode(0x21, CpuRegister.HL)]
[Opcode(0x31, CpuRegister.SP)]
internal sealed class Ld16(CpuRegister targetRegister) : IOpcode
{
    public int TCycles => 12;

    public ReadType ReadType => ReadType.Read16;

    public string Format(string? arg) => $"LD {CpuState.Name(targetRegister)}";
}
